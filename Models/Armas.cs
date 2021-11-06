using RpgApi.Models.Enums;

namespace RpgApi.Models
{
    public class Arma
    {
        public int Id { get; set; }
        public string Nome { get; set; } /*= "Zweihander";*/
        public int Dano { get; set; } /*= 50;*/

        public ClasseEnum Personagem { get; set; } /*= ClasseEnum.Cavaleiro;*/
        public Personagem Personagens { get; set; }
        public int PersonagemId { get; set; }
    }
}