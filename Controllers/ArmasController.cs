using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RpgApi.Models;
using Microsoft.EntityFrameworkCore;
using RpgApi.Data;

namespace RpgApi.Controllers 
{ 
    [ApiController]
    [Route("[controller]")]
    public class ArmasController : ControllerBase
    {

        private readonly DataContext _context; //declaração


        public ArmasController(DataContext context)  
        {
            _context = context; //inicialização do atibuto
        }  
      [HttpGet("{id}")] 

        public async Task<IActionResult> GetSingle(int id)
        {
            try
            {
                 Arma a = await _context.Armas
                    .FirstOrDefaultAsync(aBusca => aBusca.Id == id);

                return Ok(a);

            }
            catch (System.Exception ex)
            {
                
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetAll")] 

        public async Task<IActionResult> Get()
        {
            try
            {
                 List<Arma> lista = await _context.Armas.ToListAsync();
                 return Ok(lista);
            }
            catch (System.Exception ex)
            {
                
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]

        public async Task<IActionResult> Add(Arma novaArma)
        {
            try
            {
                if(novaArma.Dano > 70) //validação para verificar os pontos de vida do personagem adicionado
                {
                    throw new System.Exception("Dano não pode ser maior que 70.");
                }

                Personagem personagem = await _context.Personagens
                    .FirstOrDefaultAsync(p => p.Id == novaArma.PersonagemId);

                if(personagem == null)
                    throw new System.Exception("Seu usuário não contém personagens com o Id do personagem informado.");

                Arma buscaArma = await _context.Armas
                    .FirstOrDefaultAsync(a => a.PersonagemId == novaArma.PersonagemId);

                if (buscaArma != null)
                    throw new System.Exception("O personagem selecionado já contém uma arma atribuída à ele.");


                 await _context.Armas.AddAsync(novaArma);
                 await _context.SaveChangesAsync();

                 return Ok(novaArma.Id);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
                
            }
        }

        [HttpPut]
        public async Task<IActionResult> Update(Arma novaArma)
        {
            try
            {
                if(novaArma.Dano > 70) //validação para verificar o dano do personagem adicionado
                {
                    throw new System.Exception("Dano não pode ser maior que 70.");
                }

                 _context.Armas.Update(novaArma);
                 int linhasAfetadas = await _context.SaveChangesAsync();

                 return Ok(linhasAfetadas);
            }
            catch (System.Exception ex)
            {
                
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]

        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                 Arma aRemover = await _context.Armas
                    .FirstOrDefaultAsync(a => a.Id == id);

                    _context.Armas.Remove(aRemover);
                    int linhasAfetadas = await _context.SaveChangesAsync();

                    return Ok(linhasAfetadas);
            }
            catch (System.Exception ex)
            {
                
                return BadRequest(ex.Message);
            }
        }


    }
    }

