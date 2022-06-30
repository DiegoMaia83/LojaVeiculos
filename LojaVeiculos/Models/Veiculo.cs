using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace LojaVeiculos.Models
{
    [Table("veiculos")]
    public class Veiculo
    {
        [Key]
        public int VeiculoId { get; set; }
        public string Marca { get; set; }
        public string Placa { get; set; }
        public string Chassi { get; set; }
        public string Renavam { get; set; }
        public int CombustivelId { get; set; }
        public int CorId { get; set; }
        public int CondicaoId { get; set; }
        public int StatusId { get; set; }
        public DateTime DataEntrada { get; set; }
        public DateTime DataSaida { get; set; }
        public string UsuCriacao { get; set; }
        public DateTime DataCriacao { get; set; }
        public string Obs { get; set; }

        [ForeignKey("CorId")]
        public virtual Cor Cor { get; set; }

        [ForeignKey("CombustivelId")]
        public virtual Combustivel Combustivel { get; set; }

        [ForeignKey("CondicaoId")]
        public virtual Condicao Condicao { get; set; }

        [ForeignKey("StatusId")]
        public virtual Status Status { get; set; }
    }

    [Table("veiculos_cor")]
    public class Cor
    {
        [Key]
        public int CorId { get; set; }
        public string Nome { get; set; }

        public virtual ICollection<Veiculo> Veiculos { get; set; }
    }

    [Table("veiculos_combustivel")]
    public class Combustivel
    {
        [Key]
        public int CombustivelId { get; set; }
        public string Nome { get; set; }

        public virtual ICollection<Veiculo> Veiculos { get; set; }
    }

    [Table("veiculos_condicao")]
    public class Condicao
    {
        [Key]
        public int CondicaoId { get; set; }
        public string Nome { get; set; }

        public virtual ICollection<Veiculo> Veiculos { get; set; }
    }

    [Table("veiculos_status")]
    public class Status
    {
        [Key]
        public int StatusId { get; set; }
        public string Nome { get; set; }

        public virtual ICollection<Veiculo> Veiculos { get; set; }
    }

    [Table("veiculos_ocorrencia")]
    public class VeiculoOcorrencia
    {
        [Key]
        public int OcorrenciaId { get; set; }
        public int VeiculoId { get; set; }
        public int UsuarioId { get; set; }
        public DateTime DataOcorrencia { get; set; }
        public string Descricao { get; set; }

        [ForeignKey("UsuarioId")]
        public virtual Usuario Usuario { get; set; }
    }

    [Table("veiculos_fotos")]
    public class VeiculoFoto
    {
        [Key]
        public int FotoId { get; set; }
        public int VeiculoId { get; set; }
        public int NumeroFoto { get; set; }
        public string Tipo { get; set; }
        public string Extensao { get; set; }
        public string UsuCriacao { get; set; }
        public DateTime DataCriacao { get; set; }
        public bool Excluida { get; set; }
        public string UsuExclusao { get; set; }
        public DateTime? DataExclusao { get; set; }
    }

    public class VeiculoPesquisa
    {
        public string Valor { get; set; }
        public string Filtro { get; set; }
    }
}