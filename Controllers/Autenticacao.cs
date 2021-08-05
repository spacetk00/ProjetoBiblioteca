using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Biblioteca.Models;
using System.Linq;
using System.Collections.Generic;


namespace Biblioteca.Controllers
{
    public class Autenticacao
    {
        public static void CheckLogin(Controller controller)
        {   
            if(string.IsNullOrEmpty(controller.HttpContext.Session.GetString("Login")))
            {
                controller.Request.HttpContext.Response.Redirect("/Home/Login");
            }
        }

        public static bool verificaLoginSenha(string login, string senha, Controller controller)
        {
            using(BibliotecaContext bc = new BibliotecaContext())
            {
                //verificaSeUsuarioAdminExiste(bc);

                //senha = Criptografo.TextoCriptogtrafado(senha);

                IQueryable<Usuario> UsuarioEncontrado = bc.Usuarios.Where(u => u.Login==login && u.Senha==senha);
                List<Usuario> ListarUsuarioEcontrado = UsuarioEncontrado.ToList();

                if(ListarUsuarioEcontrado.Count==0)
                {
                    return false;
                }
                else
                {
                    controller.HttpContext.Session.SetString("Login", ListarUsuarioEcontrado[0].Login);
                    controller.HttpContext.Session.SetString("Nome", ListarUsuarioEcontrado[0].Nome);
                    controller.HttpContext.Session.SetInt32("Tipo", ListarUsuarioEcontrado[0].Tipo);
                    return true;
                }
            }
        }
    }
}