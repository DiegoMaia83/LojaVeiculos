using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace LojaVeiculos.Models
{
    [Table("usuarios")]
    public class Usuario
    {
        [Key]
        public int UsuarioId { get; set; }
        public string Nome { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public bool Bloqueado { get; set; }

        public virtual ICollection<Log> Logs { get; set; }
        public virtual ICollection<VeiculoOcorrencia> Ocorrencias { get; set; }
    }
}