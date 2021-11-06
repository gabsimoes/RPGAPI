using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using RpgApi.Models;
using RpgApi.Models.Enums;
using System.Linq; //responsável por fazer consultas mais rebuscadas numa lista

namespace RpgApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PersonagemExercicioController : ControllerBase //programação de definição deve ser feita
    {
        private static List<Personagem> personagens = new List<Personagem>() {
            new Personagem() { Id = 1, }, //Frodo Cavaleiro            
            new Personagem() { Id = 2, Nome = "Sam", PontosVida=100, Forca=15, Defesa=25, Inteligencia=30, Classe=ClasseEnum.Cavaleiro},    
            new Personagem() { Id = 3, Nome = "Galadriel", PontosVida=100, Forca=18, Defesa=21, Inteligencia=35, Classe=ClasseEnum.Clerigo },
            new Personagem() { Id = 4, Nome = "Gandalf", PontosVida=100, Forca=18, Defesa=18, Inteligencia=37, Classe=ClasseEnum.Mago },
            new Personagem() { Id = 5, Nome = "Hobbit", PontosVida=100, Forca=20, Defesa=17, Inteligencia=31, Classe=ClasseEnum.Cavaleiro },
            new Personagem() { Id = 6, Nome = "Celeborn", PontosVida=100, Forca=21, Defesa=13, Inteligencia=34, Classe=ClasseEnum.Clerigo },
            new Personagem() { Id = 7, Nome = "Radagast", PontosVida=100, Forca=25, Defesa=11, Inteligencia=35, Classe=ClasseEnum.Mago }      
        };

        [HttpGet("GetByClasse/{classeId}")]
        public IActionResult GetByClasse(int classeId)
        {
        List<Personagem> listaFiltrada = personagens.FindAll(p => p.Classe == (ClasseEnum)classeId);
        return Ok(listaFiltrada);
        }

        [HttpGet("GetByNome/{nome}")]
        public IActionResult GetByNome(string nome)
        {
            List<Personagem> listaFinalBusca = personagens.FindAll(p => p.Nome == nome);
            if (listaFinalBusca.Count == 0) //conta se a lista está zerada ou não.
            return NotFound("Nenhum personagem encontrado.");

            return Ok(listaFinalBusca);
        } 

       [HttpPost]
        public IActionResult AddPersonagem(Personagem novoPersonagem)
        {
            personagens.Add(novoPersonagem);

            if(novoPersonagem.Defesa < 10 || novoPersonagem.Inteligencia > 30)
            return BadRequest("Especificações incorretas.");

            return  Ok(personagens);
        }

        [HttpPost("PostValidacaoMago")]
        public IActionResult PostValidacaoMago(Personagem novoPersonagem)
        {
            if(novoPersonagem.Classe == ClasseEnum.Mago && novoPersonagem.Inteligencia > 35)
            return BadRequest("Inteligência maior que o necessário.");

            personagens.Add(novoPersonagem);
            return Ok(personagens);
        }

        /*[HttpGet("GetClerigoMago")]
        public IActionResult GetClerigoMago()
        {
            List<Personagem> listaSemCavaleiro = 
                personagens.FindAll(p => p.Classe != ClasseEnum.Cavaleiro)
                    .OrderByDescending(ord => ord.Inteligencia) //OrderByDescending using System.Linq                   
                    .ToList();

            return Ok(listaSemCavaleiro);


            Personagem pRemove = personagens.Find(p => p.Classe == ClasseEnum.Cavaleiro);
            personagens.Remove(pRemove);

            List<Personagem> listFinal = personagens.OrderByDescending(p => p.PontosVida).ToList();
            return Ok(pRemove.Nome);
            return Ok (listFinal);
        }*/

        [HttpGet("GetEstatisticas")]
        public IActionResult GetEstatisticas()
        {
            int quantidade = personagens.Count;
            int somaInteligencia = personagens.Sum(p => p.Inteligencia);

            return Ok("A lista contém " + quantidade + " personagens e somatório de inteligência é: " + somaInteligencia);
        }
    }

     

       
}
