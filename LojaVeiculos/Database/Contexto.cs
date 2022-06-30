using LojaVeiculos.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace LojaVeiculos.Database
{
    public class Contexto : DbContext
    {
        public Contexto() : base("name=MySql")
        {
            //Arquivo ConnectionStrings.config
        }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Veiculo> Veiculos { get; set; }
        public DbSet<Cor> VeiculoCor { get; set; }
        public DbSet<Combustivel> VeiculoCombustivel { get; set; }
        public DbSet<Condicao> VeiculoCondicao { get; set; }
        public DbSet<Status> VeiculoStatus { get; set; }
        public DbSet<VeiculoOcorrencia> VeiculoOcorrencias { get; set; }
        public DbSet<VeiculoFoto> VeiculoFotos { get; set; }
        public DbSet<Log> Logs { get; set; }
    }
}