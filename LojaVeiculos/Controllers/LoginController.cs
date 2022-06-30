using LojaVeiculos.Aplicacao;
using LojaVeiculos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LojaVeiculos.Controllers
{
    public class LoginController : Controller
    {
        private readonly UsuarioAplicacao _usuarioAplicacao = new UsuarioAplicacao();

        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(UsuarioLogin usuarioLogin)
        {
            try
            {
                if(UsuarioSessao.Login(usuarioLogin))
                {
                    var usuario = _usuarioAplicacao.Retornar(UsuarioSessao.Logado.UsuarioId);

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ViewBag.Mensagem = "Usuário não localizado!";
                }
            }
            catch
            {
                ViewBag.Mensagem = "Erro ao tentar efetuar o login";
            }

            return View();
        }

        public ActionResult Logoff()
        {
            UsuarioSessao.Logoff();

            return RedirectToAction("Index", "Login");
        }
    }
}