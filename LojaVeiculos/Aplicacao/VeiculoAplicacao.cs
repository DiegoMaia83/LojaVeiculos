using LojaVeiculos.Models;
using LojaVeiculos.Repositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LojaVeiculos.Aplicacao
{
    public class VeiculoAplicacao
    {

        public List<Veiculo> Listar(VeiculoPesquisa pesquisa)
        {
            try
            {
                using (var veiculos = new VeiculoRepositorio())
                {
                    var lista = veiculos.GetAll().ToList();

                    if (!String.IsNullOrEmpty(pesquisa.Valor))
                    {
                        if (pesquisa.Filtro == "codigo")
                        {
                            lista = lista.Where(x => x.VeiculoId == int.Parse(pesquisa.Valor)).ToList();
                        }
                        else if (pesquisa.Filtro == "placa")
                        {
                            lista = lista.Where(x => x.Placa.ToUpper().Contains(pesquisa.Valor.ToUpper())).ToList();
                        }
                        else if (pesquisa.Filtro == "chassi")
                        {
                            lista = lista.Where(x => x.Chassi.ToUpper().Contains(pesquisa.Valor.ToUpper())).ToList();
                        }
                        else
                        {
                            lista = new List<Veiculo>();
                        }
                    }

                    return lista;

                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public List<Veiculo> Listar(string param)
        {
            try
            {
                using (var veiculos = new VeiculoRepositorio())
                {
                    if (param == "ativos")
                    {
                        return veiculos.GetAll()
                            .Where(x => x.StatusId == 1).OrderByDescending(x => x.VeiculoId)
                            .ToList();
                    }

                    if (param == "ultimas-entradas")
                    {
                        return veiculos.GetAll()
                            .OrderByDescending(x => x.DataEntrada)
                            .ToList();
                    }
                    
                    if (param == "sinistros")
                    {
                        return veiculos.GetAll()
                            .Where(x => x.CondicaoId == 2 || x.CondicaoId == 3 || x.CondicaoId == 4)
                            .OrderByDescending(x => x.VeiculoId)
                            .ToList();
                    }

                    return new List<Veiculo>();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Veiculo Retornar(int veiculoId)
        {
            try
            {
                using(var veiculos = new VeiculoRepositorio())
                {
                    return veiculos.GetAll().Where(x => x.VeiculoId == veiculoId).FirstOrDefault();
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public int Salvar(Veiculo veiculo)
        {
            try
            {
                using(var veiculos = new VeiculoRepositorio())
                {
                    veiculos.Adicionar(veiculo);
                    veiculos.SalvarTodos();

                    return veiculo.VeiculoId;
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public int Alterar(Veiculo veiculo)
        {
            try
            {
                using(var veiculos = new VeiculoRepositorio())
                {
                    veiculos.Atualizar(veiculo);
                    veiculos.SalvarTodos();

                    return veiculo.VeiculoId;
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public List<Cor> ListarCores()
        {
            try
            {
                using(var cores = new VeiculoCorRepositorio())
                {
                    return cores.GetAll().ToList();
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public List<Combustivel> ListarCombustiveis()
        {
            try
            {
                using(var combustiveis = new VeiculoCombustivelRepositorio())
                {
                    return combustiveis.GetAll().ToList();
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public List<Condicao> ListarCondicoes()
        {
            try
            {
                using(var condicoes = new VeiculoCondicaoRepositorio())
                {
                    return condicoes.GetAll().ToList();
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public List<VeiculoOcorrencia> ListarOcorrencias(int veiculoId)
        {
            try
            {
                using(var ocorrencias = new VeiculoOcorrenciaRepositorio())
                {
                    return ocorrencias.GetAll().Where(x => x.VeiculoId == veiculoId).ToList();
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public int GravarOcorrencia(VeiculoOcorrencia ocorrencia)
        {
            try
            {
                using(var ocorrencias = new VeiculoOcorrenciaRepositorio())
                {
                    ocorrencias.Adicionar(ocorrencia);
                    ocorrencias.SalvarTodos();

                    return ocorrencia.OcorrenciaId;
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public List<VeiculoFoto> ListarFotosPorTipo(int veiculoId, string tipo)
        {
            try
            {
                using (var fotos = new VeiculoFotoRepositorio())
                {
                    return fotos.GetAll().Where(x => x.VeiculoId == veiculoId && x.Tipo == tipo && x.Excluida == false).ToList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


    }
}