using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RpgApi.Data;
using RpgApi.Models;

namespace RpgApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DisputasController : ControllerBase
    {
        private readonly DataContext _context;
        public DisputasController(DataContext context)
        {
            _context = context;
        }

        [HttpPost("Arma")]
        public async Task<IActionResult> AtaqueComArmaAsync(Disputa d)
        {
            try
            {
                Personagem atacante = await _context.Personagens
                    .Include(p => p.Arma)
                    .FirstOrDefaultAsync(p => p.Id == d.AtacanteId);

                Personagem oponente = await _context.Personagens
                    .FirstOrDefaultAsync(p => p.Id == d.OponenteId);

                int dano = atacante.Arma.Dano + (new Random().Next(atacante.Forca));

                dano = dano - new Random().Next(oponente.Defesa);

                if (dano > 0)
                    oponente.PontosVida = oponente.PontosVida - (int)dano;
                if (oponente.PontosVida <= 0)
                    d.Narracao = $" {oponente.Nome} foi derrotado! ";

                _context.Personagens.Update(oponente);
                await _context.SaveChangesAsync();

                StringBuilder dados = new StringBuilder();
                dados.AppendFormat(" | Atacante: {0} ", atacante.Nome);
                dados.AppendFormat(" | Oponente: {0} ", oponente.Nome);
                dados.AppendFormat(" | Pontos de Vida do atacante: {0} ", atacante.PontosVida);
                dados.AppendFormat(" | Pontos de Vida do oponente: {0} ", oponente.PontosVida);
                dados.AppendFormat(" | Arma utilizada: {0}", atacante.Arma.Nome);
                dados.AppendFormat(" | Dano: {0} ", dano);

                d.Narracao += dados.ToString();

                _context.Disputas.Add(d);
                _context.SaveChanges();

                return Ok(d);
            }
            catch (System.Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpPost("Habilidade")]
        public async Task<IActionResult> AtaqueComHabilidadeAsync(Disputa d)
        {
            try
            {
                Personagem atacante = await _context.Personagens
                    .Include(p => p.PersonagemHabilidades).ThenInclude(ph => ph.Habilidade)
                    .FirstOrDefaultAsync(p => p.Id == d.AtacanteId);

                Personagem oponente = await _context.Personagens
                    .FirstOrDefaultAsync(p => p.Id == d.OponenteId);

                PersonagemHabilidade ph = await _context.PersonagemHabilidades
                    .Include(p => p.Habilidade)
                    .FirstOrDefaultAsync(phBusca => phBusca.HabilidadeId == d.HabilidadeId);

                if (ph == null)
                    d.Narracao = $" | {atacante.Nome} não possui esta habilidade. ";
                else
                {
                    int dano = ph.Habilidade.Dano + (new Random().Next(atacante.Inteligencia));
                    dano = dano - new Random().Next(oponente.Defesa);

                    if (dano > 0)
                        oponente.PontosVida = oponente.PontosVida - (int)dano;
                    if (oponente.PontosVida <= 0)
                        d.Narracao += $" | {oponente.Nome} foi derrotado! ";

                    _context.Personagens.Update(oponente);
                    await _context.SaveChangesAsync();

                    StringBuilder dados = new StringBuilder();
                    dados.AppendFormat(" | Atacante: {0} ", atacante.Nome);
                    dados.AppendFormat(" | Oponente: {0} ", oponente.Nome);
                    dados.AppendFormat(" | Pontos de Vida do atacante: {0} ", atacante.PontosVida);
                    dados.AppendFormat(" | Habilidade utilizada: {0} ", ph.Habilidade.Nome);
                    dados.AppendFormat(" | Pontos de Vida do oponente: {0} ", oponente.PontosVida);
                    dados.AppendFormat(" | Dano: {0} ", dano);

                    d.Narracao += dados.ToString();
                    d.DataDisputa = DateTime.Now;
                    _context.Disputas.Add(d);
                    _context.SaveChanges();
                }

                return Ok(d);
            }
            catch (System.Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpGet("PersonagemRandom")]
        public async Task<IActionResult> Sorteio()
        {
            List<Personagem> personagens = await _context.Personagens.ToListAsync();

            //Sorteio com número da quantidade de personagens
            int sorteio = new Random().Next(personagens.Count);

            //Busca na lista pelo índice sorteado (não é o ID)
            Personagem p = personagens[sorteio];

            string msg = string.Format("Número sorteado {0}. Personagem: {1}", sorteio, p.Nome);

            return Ok(msg);
        }

        [HttpPost("DisputaEmGrupo")]

        public async Task<IActionResult> DisputaEmGrupoAsync(Disputa d)
        {
            try
            {   //Busca na base dos personagens informados no parâmetro incluindo Armas e Habilidades
                List<Personagem> personagens = await _context.Personagens
                   .Include(p => p.Arma)
                   .Include(p => p.PersonagemHabilidades).ThenInclude(ph => ph.Habilidade)
                   .Where(p => d.ListaIdPersonagens.Contains(p.Id)).ToListAsync();

                //Contagem de personagens vivos na lista obtida do banco de dados
                int qtdPersonagensVivos = personagens.FindAll(p => p.PontosVida > 0).Count;

                //Enquanto houver mais de um personagem vivo, haverá disputa
                while (qtdPersonagensVivos > 1)
                {
                    //Seleciona personagens com pontos de vida positivos e depois faz sorteio
                    List<Personagem> atacantes = personagens.Where(p => p.PontosVida > 0).ToList();
                    Personagem atacante = atacantes[new Random().Next(atacantes.Count)];
                    d.AtacanteId = atacante.Id;

                    //Seleciona personagens com pontos de vida positivos, exceto o atacante escolhido e depois faz o sorteio
                    List<Personagem> oponentes = personagens.Where(p => p.Id != atacante.Id && p.PontosVida > 0).ToList();
                    Personagem oponente = oponentes[new Random().Next(oponentes.Count)];
                    d.OponenteId = oponente.Id;

                    //Declara e redefine a cada passagem do while o valor das variáveis que serão usadas
                    int dano = 0;
                    string ataqueUsado = string.Empty;
                    string resultado = string.Empty;

                    //Sorteia entre 0 e 1: 0 é um ataque com arma e 1 é um ataque com habilidades
                    bool ataqueUsaArma = (new Random().Next(1) == 0);

                    if (ataqueUsaArma && atacante.Arma != null)
                    {
                        //ataque com arma
                        //Programação do ataque com arma caso o atacante possua arma (o != null) do if

                        //Sorteio da força
                        dano = atacante.Arma.Dano + (new Random().Next(atacante.Forca));
                        dano = dano - new Random().Next(oponente.Defesa); //Sorteio da defesa
                        ataqueUsado = atacante.Arma.Nome;

                        if (dano > 0)
                            oponente.PontosVida = oponente.PontosVida - (int)dano;

                        //Formata a mensagem
                        resultado =
                            string.Format("{0} atacou {1} usando {2} com o dano {3}.", atacante.Nome, oponente.Nome, ataqueUsado, dano);
                        d.Narracao += resultado; //Concatena o resultado com as narrações existentes
                        d.Resultados.Add(resultado); //Adiciona o resultado atual na lista de resultados
                    }
                    else if (atacante.PersonagemHabilidades.Count != 0) //Verifica se o personagem tem habilidades
                    {
                        //ataque com habilidade

                        //Realiza o sorteio entre as habilidades existentes e na linha seguinte a seleciona
                        int sorteioHabilidadeId = new Random().Next(atacante.PersonagemHabilidades.Count);
                        Habilidade habilidadeEscolhida = atacante.PersonagemHabilidades[sorteioHabilidadeId].Habilidade;
                        ataqueUsado = habilidadeEscolhida.Nome;

                        //Sorteio da inteligência somada ao dano
                        dano = habilidadeEscolhida.Dano + (new Random().Next(atacante.Inteligencia));
                        dano = dano - new Random().Next(oponente.Defesa); //Sorteio da defesa

                        if (dano > 0)
                            oponente.PontosVida = oponente.PontosVida - (int)dano;

                        resultado =
                            string.Format("{0} atacou {1} usando {2} com o dano {3}.", atacante.Nome, oponente.Nome, ataqueUsado, dano);
                        d.Narracao += resultado;
                        d.Resultados.Add(resultado);

                    }

                    //Verificação do ataque usado e verificação se existe mais de um personagem vivo
                    if (!string.IsNullOrEmpty(ataqueUsado))
                    {
                        //Incrementa os dados dos combates
                        atacante.Vitorias++;
                        oponente.Derrotas++;
                        atacante.Disputas++;
                        oponente.Disputas++;

                        d.Id = 0; //zera o ID para poder salvar os dados de disputa sem erro de chave
                        d.DataDisputa = DateTime.Now;
                        _context.Disputas.Add(d);
                        await _context.SaveChangesAsync();
                    }

                    qtdPersonagensVivos = personagens.FindAll(p => p.PontosVida > 0).Count;

                    if (qtdPersonagensVivos == 1) //havendo só um personagem vivo, existe um CAMPEÃO
                    {
                        string resultadoFinal = 
                            $"{atacante.Nome.ToUpper()} é CAMPEÃO com {atacante.PontosVida} pontos de vida restantes!";

                        d.Narracao += resultadoFinal; //concatena o resultado final com as demais narrações
                        d.Resultados.Add(resultadoFinal); //concatena o resultado final com os demais resultados

                        break; //parando o While 
                    }

                }

                //Código após o fechamento do While. 
                //Atualizará os pontos de vida, disputas, vitórias e derrotas de todos os personagens ao final das batalhas
                _context.Personagens.UpdateRange(personagens);
                await _context.SaveChangesAsync();

                return Ok(d); //retorna os dados de disputa


            }
            catch (System.Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }



    }

}