using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LojaVeiculos.Models
{
    public class ResultadoPost
    {
        public int Id { get; set; }
        public bool Sucesso { get; set; }
        public string Mensagem { get; set; }
    }
}