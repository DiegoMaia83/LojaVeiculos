using LojaVeiculos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LojaVeiculos.Controllers
{
    public class ComumController : Controller
    {
        // GET: Comum
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Error(ResultadoPost obj)
        {  
            return View("_Error", obj);
        }
    }
}