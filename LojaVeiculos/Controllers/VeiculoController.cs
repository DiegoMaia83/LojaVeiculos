using LojaVeiculos.Aplicacao;
using LojaVeiculos.Models;
using System;
using System.IO;
using System.Web;
using System.Web.Mvc;

namespace LojaVeiculos.Controllers
{
    public class VeiculoController : Controller
    {
        private readonly VeiculoAplicacao _veiculoAplicacao = new VeiculoAplicacao();
        private readonly LogAplicacao _logAplicacao = new LogAplicacao();
        private readonly ArquivoAplicacao _arquivoAplicacao = new ArquivoAplicacao();

        public ActionResult Home()
        {
            if (!UsuarioSessao.ValidaToken()) Response.Redirect("/Login");
            
            return View();
        }

        public ActionResult Index()
        {
            if (!UsuarioSessao.ValidaToken()) Response.Redirect("/Login");

            return View();
        }

        // Retorna View Parcial com dados do veículo
        public ActionResult RetornarDados(int id = 0)
        {
            if (!UsuarioSessao.ValidaToken())
                return Json(new ResultadoPost { Id = 0, Mensagem = "Houve um erro ao processa a rotina! Tente efetuar o login novamente!" });

            var veiculo = new Veiculo();

            if (id > 0)
            {
                veiculo = _veiculoAplicacao.Retornar(id);

                var log = new Log()
                {
                    CodReferencia = veiculo.VeiculoId,
                    Processo = "Veiculo",
                    UsuarioId = UsuarioSessao.Logado.UsuarioId,
                    Ip = Request.ServerVariables["REMOTE_ADDR"],
                    DataLog = DateTime.Now,
                    Descricao = "Acessou os dados do veículo"
                };

                _logAplicacao.GravarLog(log);
            }

            ViewBag.ListaCores = _veiculoAplicacao.ListarCores();
            ViewBag.ListaCombustiveis = _veiculoAplicacao.ListarCombustiveis();
            ViewBag.ListaCondicoes = _veiculoAplicacao.ListarCondicoes();

            return View("_RetornarDados", veiculo);
        }

        // Retorna View Parcial com logs do veículo
        public ActionResult RetornarLogs(int id = 0)
        {
            if (!UsuarioSessao.ValidaToken())
                return Json(new ResultadoPost { Id = 0, Mensagem = "Houve um erro ao processa a rotina! Tente efetuar o login novamente!" });

            var lista = _logAplicacao.ListarLogs(id, "Veiculo");

            return View("_RetornarLogs", lista);
        }

        // Retorna View Parcial com ocorrencias do veículo
        public ActionResult RetornarOcorrencias(int id = 0)
        {
            if (!UsuarioSessao.ValidaToken())
                return Json(new ResultadoPost { Id = 0, Mensagem = "Houve um erro ao processa a rotina! Tente efetuar o login novamente!" });

            var lista = _veiculoAplicacao.ListarOcorrencias(id);

            ViewBag.VeiculoId = id;

            return View("_RetornarOcorrencias", lista);
        }

        public ActionResult RetornarFotos(int id)
        {
            if (!UsuarioSessao.ValidaToken())
                return Json(new ResultadoPost { Id = 0, Mensagem = "Houve um erro ao processa a rotina! Tente efetuar o login novamente!" });

            ViewBag.VeiculoId = id;

            return View("_RetornarFotos");
        }

        public ActionResult ListarFotos(int id, string tipo)
        {
            if (!UsuarioSessao.ValidaToken())
                return Json(new ResultadoPost { Id = 0, Mensagem = "Houve um erro ao processa a rotina! Tente efetuar o login novamente!" });

            var lista = _veiculoAplicacao.ListarFotosPorTipo(id, tipo);

            ViewBag.Tipo = tipo;

            return View("_ListarFotos", lista);
        }

        [HttpPost]
        public JsonResult SalvarVeiculo(Veiculo veiculo)
        {
            if (!UsuarioSessao.ValidaToken())
                return Json(new ResultadoPost { Id = 0, Sucesso = false, Mensagem = "Houve um erro ao efetuar a operação. Tente efetuar o login novamente." });

            try
            {
                int veiculoId;

                var log = new Log()
                {                    
                    Processo = "Veiculo",
                    UsuarioId = UsuarioSessao.Logado.UsuarioId,
                    Ip = Request.ServerVariables["REMOTE_ADDR"],
                    DataLog = DateTime.Now
                };

                if (veiculo.VeiculoId > 0)
                {
                    veiculoId = _veiculoAplicacao.Alterar(veiculo);

                    log.CodReferencia = veiculoId;
                    log.Descricao = "Alterou a ficha do veículo";
                }
                else
                {
                    veiculo.StatusId = 1;
                    veiculoId = _veiculoAplicacao.Salvar(veiculo);

                    log.CodReferencia = veiculoId;
                    log.Descricao = "Inseriu a ficha do veículo";
                }                

                _logAplicacao.GravarLog(log);

                return Json(new ResultadoPost { Id = veiculo.VeiculoId, Sucesso = true, Mensagem = "Operação realizada com sucesso!" });
            }
            catch(Exception ex)
            {
                return Json(new ResultadoPost { Id = veiculo.VeiculoId, Sucesso = false, Mensagem = "Houve um erro ao processar a rotina! Server message: " + ex.Message });
            } 
        }

        public ActionResult ListarVeiculos(VeiculoPesquisa pesquisa)
        {
            if (!UsuarioSessao.ValidaToken())
                return Json(new ResultadoPost { Id = 0, Sucesso = false, Mensagem = "Houve um erro ao retornar os dados. Tente efetuar o login novamente." });

            try
            {
                var lista = _veiculoAplicacao.Listar(pesquisa);

                return View("_ListarVeiculos", lista);
            }
            catch
            {
                return Json(new ResultadoPost() { Id = 0, Sucesso = false, Mensagem = "Houve um erro ao processar a rotina!" }, JsonRequestBehavior.AllowGet);
            }            
        }

        [HttpPost]
        public JsonResult SalvarOcorrencia(VeiculoOcorrencia ocorrencia)
        {
            if (!UsuarioSessao.ValidaToken())
                return Json(new ResultadoPost { Id = 0, Sucesso = false, Mensagem = "Houve um erro ao efetuar a operação. Tente efetuar o login novamente." });

            try
            {
                if (ocorrencia.VeiculoId > 0)
                {
                    ocorrencia.DataOcorrencia = DateTime.Now;
                    ocorrencia.UsuarioId = UsuarioSessao.Logado.UsuarioId;

                    _veiculoAplicacao.GravarOcorrencia(ocorrencia);

                    var log = new Log()
                    {
                        CodReferencia = ocorrencia.VeiculoId,
                        Processo = "Veiculo",
                        UsuarioId = UsuarioSessao.Logado.UsuarioId,
                        Ip = Request.ServerVariables["REMOTE_ADDR"],
                        DataLog = DateTime.Now,
                        Descricao = "Inseriu uma ocorrência para o veículo"
                    };

                    _logAplicacao.GravarLog(log);

                    return Json(new ResultadoPost { Id = ocorrencia.VeiculoId, Sucesso = true, Mensagem = "Operação realizada com sucesso!" });
                }
                else
                {
                    return Json(new ResultadoPost { Id = 0, Sucesso = false, Mensagem = "Veículo não localizado!" });
                }
            }
            catch
            {
                return Json(new ResultadoPost { Id = 0, Sucesso = false, Mensagem = "Houve um erro ao processar a rotina!" });
            }
        }

        [HttpPost]
        public JsonResult SalvarFotos(int veiculoId, string tipo)
        {
            try
            {
                int arquivosSalvos = 0;
                for (int i = 0; i < Request.Files.Count; i++)
                {
                    HttpPostedFileBase arquivo = Request.Files[i];

                    //Suas validações ......

                    //Salva o arquivo
                    if (arquivo.ContentLength > 0)
                    {
                        var uploadPath = Server.MapPath("~/Content/Uploads");
                        string caminhoArquivo = Path.Combine(@uploadPath, Path.GetFileName(arquivo.FileName));
                        string diretorioDestino = Server.MapPath("~/Content/Imagens/" + veiculoId);

                        arquivo.SaveAs(caminhoArquivo);
                        arquivosSalvos++;

                        var fotoVeiculo = new VeiculoFoto
                        {
                            VeiculoId = veiculoId,
                            Tipo = tipo
                        };

                        var fotoId = _arquivoAplicacao.CopiarParaDiretorio(caminhoArquivo, diretorioDestino, fotoVeiculo);

                        var log = new Log()
                        {
                            CodReferencia = veiculoId,
                            Processo = "Veiculo",
                            UsuarioId = UsuarioSessao.Logado.UsuarioId,
                            Ip = Request.ServerVariables["REMOTE_ADDR"],
                            DataLog = DateTime.Now,
                            Descricao = "Inseriu o arquivo id [ " + fotoId + " ]"
                        };

                        _logAplicacao.GravarLog(log);
                    }
                }

                return Json(new ResultadoPost { Id = veiculoId, Sucesso = true, Mensagem = arquivosSalvos + " arquivos salvos com sucesso!" });
            }
            catch
            {
                return Json(new ResultadoPost { Id = 0, Sucesso = false, Mensagem = "Houve um erro ao processar a rotina!" });
            }
            
        }

        [HttpPost]
        public JsonResult ExcluirFoto(int fotoId)
        {
            if (!UsuarioSessao.ValidaToken())
                return Json(new ResultadoPost { Id = 0, Sucesso = false, Mensagem = "Houve um erro ao efetuar a operação. Tente efetuar o login novamente." });

            try
            {
                if (fotoId > 0)
                {
                    var foto = _arquivoAplicacao.RetornarFoto(fotoId);
                    foto.Excluida = true;
                    foto.DataExclusao = DateTime.Now;
                    foto.UsuExclusao = UsuarioSessao.Logado.Login;

                    var result = _arquivoAplicacao.ExcluirFotoVeiculo(foto);

                    if (result > 0)
                    {
                        var log = new Log()
                        {
                            CodReferencia = 2,
                            Processo = "Veiculo",
                            UsuarioId = UsuarioSessao.Logado.UsuarioId,
                            Ip = Request.ServerVariables["REMOTE_ADDR"],
                            DataLog = DateTime.Now,
                            Descricao = "Removeu o arquivo id [ " + foto.FotoId + " ]"
                        };

                        _logAplicacao.GravarLog(log);
                    }

                    return Json(new ResultadoPost { Id = foto.VeiculoId, Sucesso = true, Mensagem = "Operação realizada com sucesso!" });
                }
                else
                {
                    return Json(new ResultadoPost { Id = 0, Sucesso = false, Mensagem = "Arquivo não localizado!" });
                }
            }
            catch
            {
                return Json(new ResultadoPost { Id = 0, Sucesso = false, Mensagem = "Houve um erro ao processar a rotina!" });
            }
        }

    }
}