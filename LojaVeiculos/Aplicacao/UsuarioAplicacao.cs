using LojaVeiculos.Models;
using LojaVeiculos.Repositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LojaVeiculos.Aplicacao
{
    public class UsuarioAplicacao
    {
        public Usuario Retornar(int usuarioId)
        {
            try
            {
                using(var usuarios = new UsuarioRepositorio())
                {
                    return usuarios.GetAll().Where(x => x.UsuarioId == usuarioId).FirstOrDefault();
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}