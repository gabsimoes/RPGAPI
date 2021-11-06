using Microsoft.AspNetCore.Mvc;
using RpgApi.Data;
using System.Threading.Tasks;
using RpgApi.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using RpgApi.Models.Enums;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace RpgApi.Controllers
{
    [Authorize(Roles = "Jogador, Admin")]
    [ApiController]
    [Route("[controller]")]
    public class PersonagensController : ControllerBase
    {
        private readonly DataContext _context; //declaração
        private readonly IHttpContextAccessor _httpContextAccessor;

        private int ObterUsuarioId()
        {
            return int.Parse(_httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));
        }

        public PersonagensController(DataContext context, IHttpContextAccessor httpContextAccessor)  
        {
            _context = context; //inicialização do atibuto
            _httpContextAccessor = httpContextAccessor;
        }   

        [HttpGet("{id}")] //buscando o personagem pelo id

        public async Task<IActionResult> GetSingle(int id)
        {
            try
            {
                 Personagem p = await _context.Personagens
                    .Include(ar => ar.Arma) //navegando pelas propriedades
                    .Include(us => us.Usuario)
                    .Include(ph => ph.PersonagemHabilidades)
                        .ThenInclude(h => h.Habilidade) //'JOIN' do C#
                    .FirstOrDefaultAsync(pBusca => pBusca.Id == id);

                return Ok(p);

            }
            catch (System.Exception ex)
            {
                
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetAll")] //pegar todos os personagens

        public async Task<IActionResult> Get()
        {
            try
            {
                 List<Personagem> lista = await _context.Personagens.ToListAsync();
                 return Ok(lista);
            }
            catch (System.Exception ex)
            {
                
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]

        public async Task<IActionResult> Add(Personagem novoPersonagem)
        {
            try
            {
                if(novoPersonagem.PontosVida > 100) //validação para verificar os pontos de vida do personagem adicionado
                {
                    throw new System.Exception("Pontos de Vida não podem ser maiores que 100.");
                }

                /*int usuarioId = int.Parse(_httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));
                novoPersonagem.Usuario = _context.Usuarios.FirstOrDefault(uBusca => uBusca.Id == usuarioId);*/

                novoPersonagem.Usuario = _context.Usuarios.FirstOrDefault(uBusca => uBusca.Id == ObterUsuarioId());

                 await _context.Personagens.AddAsync(novoPersonagem);
                 await _context.SaveChangesAsync();

                 return Ok(novoPersonagem.Id);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
                
            }
        }

        [HttpPut]
        public async Task<IActionResult> Update(Personagem novoPersonagem)
        {
            try
            {
                if(novoPersonagem.PontosVida > 100) //validação para verificar os pontos de vida do personagem adicionado
                {
                    throw new System.Exception("Pontos de Vida não podem ser maiores que 100.");
                }

                /*int usuarioId = int.Parse(_httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));
                novoPersonagem.Usuario = _context.Usuarios.FirstOrDefault(uBusca => uBusca.Id == usuarioId);*/

                novoPersonagem.Usuario = _context.Usuarios.FirstOrDefault(uBusca => uBusca.Id == ObterUsuarioId());

                 _context.Personagens.Update(novoPersonagem);
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
                 Personagem pRemover = await _context.Personagens
                    .FirstOrDefaultAsync(p => p.Id == id);

                    _context.Personagens.Remove(pRemover);
                    int linhasAfetadas = await _context.SaveChangesAsync();

                    return Ok(linhasAfetadas);
            }
            catch (System.Exception ex)
            {
                
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetByUser")]
        public async Task<IActionResult> GetByUserAsync()
        {
            try
            {
                 int id = int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value);
                 List<Personagem> lista = await _context.Personagens
                    .Where(u => u.Usuario.Id == id).ToListAsync();

                    return Ok(lista);
            }
            catch (System.Exception ex)
            {
                
                return BadRequest(ex.Message);
                            
            }
        }
        
        private string ObterPerfilUsuario()
        {
            return _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Role);
        }
        [HttpGet("GetByPerfil")]
        public async Task<IActionResult> GetByPerfilAsync()
        {
            try
            {
                 List<Personagem> lista = new List<Personagem>();

                 if(ObterPerfilUsuario() == "Admin")
                    lista = await _context.Personagens.ToListAsync();
                else
                {
                    lista = await _context.Personagens
                            .Where(p => p.Usuario.Id == ObterUsuarioId()).ToListAsync();
                }
                    return Ok(lista);

            }
            catch (System.Exception ex)
            {
                
               return BadRequest(ex.Message);
            }
        }

    }
}