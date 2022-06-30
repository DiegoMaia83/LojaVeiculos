using LojaVeiculos.Models;
using LojaVeiculos.Repositorio;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace LojaVeiculos.Aplicacao
{
    public class ArquivoAplicacao
    {
        public VeiculoFoto RetornarFoto(int fotoId)
        {
            try
            {
                using (var fotos = new VeiculoFotoRepositorio())
                {
                    return fotos.GetAll().Where(x => x.FotoId == fotoId).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int SalvarFotoVeiculo(VeiculoFoto foto)
        {
            try
            {
                using (var fotos = new VeiculoFotoRepositorio())
                {
                    fotos.Adicionar(foto);
                    fotos.SalvarTodos();

                    return foto.FotoId;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int ExcluirFotoVeiculo(VeiculoFoto foto)
        {
            try
            {
                using (var fotos = new VeiculoFotoRepositorio())
                {                    
                    fotos.Atualizar(foto);
                    fotos.SalvarTodos();

                    return foto.VeiculoId;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int RetornarUltimaInserida(int veiculoId, string tipo)
        {
            try
            {
                using (var fotos = new VeiculoFotoRepositorio())
                {
                    return fotos.GetAll()
                        .Where(x => x.VeiculoId == veiculoId && x.Tipo == tipo)
                        .OrderByDescending(x => x.NumeroFoto)
                        .Select(x => x.NumeroFoto)
                        .FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int CopiarParaDiretorio(string diretorioOrigem, string diretorioDestino, VeiculoFoto arq)
        {
            try
            { 
                var ultimaInserida = RetornarUltimaInserida(arq.VeiculoId, arq.Tipo);                
                var numFoto = ultimaInserida + 1;
                var extensao = Path.GetExtension(diretorioOrigem);
                var arquivo = arq.Tipo + arq.VeiculoId.ToString("000000") + "_" + numFoto.ToString("00") + extensao;

                var newFile = Path.Combine(diretorioDestino, arquivo);

                if (!Directory.Exists(diretorioDestino))
                    Directory.CreateDirectory(diretorioDestino);


                if (!File.Exists(newFile))
                    File.Move(diretorioOrigem, newFile);


                var foto = new VeiculoFoto
                {
                    VeiculoId = arq.VeiculoId,
                    NumeroFoto = numFoto,
                    Tipo = arq.Tipo,
                    Extensao = extensao,
                    UsuCriacao = UsuarioSessao.Logado.Login,
                    DataCriacao = DateTime.Now
                };

                return SalvarFotoVeiculo(foto);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}