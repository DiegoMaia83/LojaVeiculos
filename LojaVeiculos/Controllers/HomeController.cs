using LojaVeiculos.Aplicacao;
using LojaVeiculos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LojaVeiculos.Controllers
{
    public class HomeController : Controller
    {
        private readonly VeiculoAplicacao _veiculoAplicacao = new VeiculoAplicacao();
        
        public ActionResult Index()
        {
            if (!UsuarioSessao.ValidaToken()) Response.Redirect("/Login");

            return View();
        }

        public ActionResult ListarVeiculos(string param)
        {
            if (!UsuarioSessao.ValidaToken())
                return Json(new ResultadoPost { Id = 0, Sucesso = false, Mensagem = "Houve um erro ao retornar os dados. Tente efetuar o login novamente." });

            try
            {
                var lista = _veiculoAplicacao.Listar(param);

                return View("_ListarVeiculos", lista);
            }
            catch
            {
                return Json(new ResultadoPost() { Id = 0, Sucesso = false, Mensagem = "Houve um erro ao processar a rotina!" }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}