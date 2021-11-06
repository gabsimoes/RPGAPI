using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RpgApi.Models
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] Pass { get; set; }
        public byte[] PasswordSalt { get; set; }
        public byte[] Foto { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public DateTime? MyProperty { get; set; } //'?' a variável aceitará nulo, o banco calcula mesmo com ela vazia

        [NotMapped]
        public string PasswordString { get; set; }

        public List<Personagem> Personagens { get; set; }
        public DateTime DataAcesso { get; internal set; }

        //[Required]
        public string Perfil { get; set; }
    }
}