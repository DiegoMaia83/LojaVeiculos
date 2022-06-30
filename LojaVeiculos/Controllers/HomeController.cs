using LojaVeiculos.Aplicacao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LojaVeiculos.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            if (!UsuarioSessao.ValidaToken()) Response.Redirect("/Login");

            return View();
        }
    }
}