using LojaVeiculos.Models;
using LojaVeiculos.Repositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace LojaVeiculos.Aplicacao
{
    public class UsuarioSessao
    {
        public static UsuarioLogin Logado
        {
            get
            {
                return GetCookie();
            }
        }

        public static UsuarioLogin GetCookie()
        {
            var adminLogin = new UsuarioLogin();

            try
            {
                if(HttpContext.Current.Request.Cookies["c"] != null)
                {
                    adminLogin.Token = HttpContext.Current.Request.Cookies["c"]["key"];
                    adminLogin.UsuarioId = Convert.ToInt32(HttpContext.Current.Request.Cookies["c"]["id"]);
                    adminLogin.Nome = HttpContext.Current.Request.Cookies["c"]["usr"];
                    adminLogin.Login = HttpContext.Current.Request.Cookies["c"]["ref"];

                }
            }
            catch(Exception ex)
            {
                throw ex;
            }

            return adminLogin;
        }

        public static void SetCookie(UsuarioLogin value)
        {
            HttpContext.Current.Response.Cookies["c"]["key"] = value.Token;
            HttpContext.Current.Response.Cookies["c"]["id"] = value.UsuarioId.ToString();
            HttpContext.Current.Response.Cookies["c"]["usr"] = value.Nome;
            HttpContext.Current.Response.Cookies["c"]["ref"] = value.Login;
        }

        public static bool Login(UsuarioLogin login)
        {
            try
            {
                if(login.Login != "" && login.Password != "")
                {
                    using(var usuarios = new UsuarioRepositorio())
                    {
                        // Busca o usuário
                        var usuario = usuarios.GetAll().Where(x => x.Login == login.Login && x.Password == login.Password).FirstOrDefault();

                        // Não localizou o usuário
                        if (usuario == null) return false;

                        // Localizou. Armazena os cookies na sessão
                        SetCookie(new UsuarioLogin
                        {
                            UsuarioId = usuario.UsuarioId,
                            Nome = usuario.Nome,
                            Login = usuario.Login,
                            Token = EncodeString(usuario.Login + ":" + usuario.Password)
                        });

                        return usuario.UsuarioId > 0;
                    }
                }
                else
                {
                    return false;
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public static bool ValidaToken()
        {
            try
            {
                if(Logado.Token != "")
                {
                    var uncodedToken = DecodeString(Logado.Token);
                    var login = uncodedToken.Split(':')[0].ToString();
                    var passwordHash = uncodedToken.Split(':')[1].ToString();

                    using(var usuarios = new UsuarioRepositorio())
                    {
                        var usuario = usuarios.GetAll().Where(x => x.Login == login && x.Password == passwordHash).FirstOrDefault();

                        // Não localizou
                        if (usuario == null) return false;

                        return usuario.UsuarioId > 0;
                    }
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }

        public static void Logoff()
        {
            if ((HttpContext.Current.Request.Cookies["c"] != null))
            {
                HttpContext.Current.Response.Cookies["c"].Expires = DateTime.Now.AddDays(-1);
            }
        }

        private static string EncodeString(string str)
        {
            return Convert.ToBase64String(Encoding.UTF8.GetBytes(str));
        }

        private static string DecodeString(string str)
        {
            return Encoding.UTF8.GetString(Convert.FromBase64String(str));
        }
    }
}