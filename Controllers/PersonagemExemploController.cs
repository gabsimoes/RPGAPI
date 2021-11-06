using Microsoft.AspNetCore.Mvc;
using RpgApi.Models;
using RpgApi.Models.Enums;
using System.Collections.Generic;
using System.Linq; //responsável por fazer consultas mais rebuscadas numa lista

namespace RpgApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PersonagemExemploController : ControllerBase
    {
        /*private Personagem p = new Personagem();

        List<Personagem> personagens = new List<Personagem>();

        public IActionResult Get()
        {
            personagens.Add(p); //personagem adicionado

            Personagem p2 = new Personagem();
            p2.Nome = "Personagem 2";
            p2.Forca = 50;
            personagens.Add(p2);

            Personagem p3 = new Personagem();
            p3.Nome = "Personagem 3";
            p3.Forca = 50;
            personagens.Add(p3);
            
            return Ok(personagens);
        }*/

        private static List<Personagem> personagens = new List<Personagem>() {
            new Personagem() { Id = 1, }, //Frodo Cavaleiro            
            new Personagem() { Id = 2, Nome = "Sam", PontosVida=100, Forca=15, Defesa=25, Inteligencia=30, Classe=ClasseEnum.Cavaleiro},    
            new Personagem() { Id = 3, Nome = "Galadriel", PontosVida=100, Forca=18, Defesa=21, Inteligencia=35, Classe=ClasseEnum.Clerigo },
            new Personagem() { Id = 4, Nome = "Gandalf", PontosVida=100, Forca=18, Defesa=18, Inteligencia=37, Classe=ClasseEnum.Mago },
            new Personagem() { Id = 5, Nome = "Hobbit", PontosVida=100, Forca=20, Defesa=17, Inteligencia=31, Classe=ClasseEnum.Cavaleiro },
            new Personagem() { Id = 6, Nome = "Celeborn", PontosVida=100, Forca=21, Defesa=13, Inteligencia=34, Classe=ClasseEnum.Clerigo },
            new Personagem() { Id = 7, Nome = "Radagast", PontosVida=100, Forca=25, Defesa=11, Inteligencia=35, Classe=ClasseEnum.Mago }      
        };

        [HttpGet("GetOrdenado")]
        public IActionResult GetOrdem()
        {
            List<Personagem> listaFinal = personagens.OrderBy(p => p.Forca).ToList();
            return Ok(listaFinal);
        }

        [HttpGet("GetAll")] //rota
        public IActionResult Get()
        {
            return Ok(personagens);
        }
        [HttpGet("GetSingle")] //rota
        public IActionResult GetSingle() //nome do método (GetSingle)
        {
            return Ok(personagens[2]);
        }

        [HttpGet("BuscaPorId/{id}")]
        public IActionResult GetById(int id) //chamando a rota pelo id (número)
        {
            return Ok(personagens.FirstOrDefault( pe => pe.Id == id ));
        }

        [HttpPost]
        public IActionResult AddPersonagem(Personagem novoPersonagem)
        {
            personagens.Add(novoPersonagem);
            return Ok(personagens);
        }

        [HttpGet("GetContagem")]
        public IActionResult GetQuantidade()
        {
            return Ok("Quantidade de Personagens: " + personagens.Count);
        }

        [HttpGet("GetSomaForca")]

        public IActionResult GetSomaForca()
        {
            return Ok(personagens.Sum(p => p.Forca));
        }
        [HttpGet("GetSemCavaleiro")]
        public IActionResult GetSemCavaleiro()
        {
            List<Personagem> listaBusca = personagens.FindAll( p => p.Classe != ClasseEnum.Cavaleiro );

            return Ok(listaBusca);
        }

        /*[HttpGet("GetByNomeAproximado")] //filtrar por nome aproximado

        public IActionResult GetByNomeAproximado(string nome)
        {
            List<Personagem> listaBusca = personagens.FindAll(p => p.nome.Contains(nome));
            return Ok(listaBusca);
        }*/

        [HttpGet("GetRemovendoMago")]

        public IActionResult GetRemovendoMagos() //filtrando personagem por algum critério e o removendo da lista
        {
            Personagem pRemove = personagens.Find(p => p.Classe == ClasseEnum.Mago);
            personagens.Remove(pRemove);
            return Ok("Personagem removido: " + pRemove.Nome);
        }

        [HttpGet("GetByForca/{força}")]
        public IActionResult Get(int forca)
        {
            List<Personagem> listaFinal = personagens.FindAll(p => p.Forca == forca);
            return Ok(listaFinal);
        }

        [HttpPost]
        public IActionResult AddPersonagem2(Personagem novoPersonagem)
        {
            if(novoPersonagem.Inteligencia == 0)
                return BadRequest("Inteligência não pode ter o valor igual a 0");

                return Ok(personagens);
        }

        [HttpPut]

        public IActionResult UpdatePersonagem(Personagem p)
        {
            Personagem personagemAlterado = personagens.Find(pers => pers.Id == p.Id);
            personagemAlterado.Nome = p.Nome;
            personagemAlterado.PontosVida = p.PontosVida;
            personagemAlterado.Forca = p.Forca;
            personagemAlterado.Defesa = p.Defesa;
            personagemAlterado.Inteligencia = p.Inteligencia;
            personagemAlterado.Classe = p.Classe;

            return Ok(personagens);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            personagens.RemoveAll(pers => pers.Id == id);
            return Ok(personagens);
        }

    }
}