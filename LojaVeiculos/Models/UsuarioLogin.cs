using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LojaVeiculos.Models
{
    public class UsuarioLogin
    {
        public int UsuarioId { get; set; }
        public string Nome { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Token { get; set; }
    }
}