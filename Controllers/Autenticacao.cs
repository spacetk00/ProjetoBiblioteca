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
                verificaSeUsuarioAdminExiste(bc);

                senha = Criptografo.TextoCriptografado(senha);

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

        public static void verificaSeUsuarioAdminExiste(BibliotecaContext bc)
        {
            IQueryable<Usuario> userEncontrado = bc.Usuarios.Where(u => u.Login=="admin");

            if(userEncontrado.ToList().Count==0)
            {
                Usuario admin = new Usuario();
                admin.Login = "admin";
                admin.Senha = Criptografo.TextoCriptografado("123");
                admin.Tipo = 1;
                admin.Nome = "Administador";

                bc.Usuarios.Add(admin);
                bc.SaveChanges();
            }
        }

        public static void verificaSeUsuarioEAdmin(Controller controller)
        {
            if( (controller.HttpContext.Session.GetInt32("Tipo")==0))
            {
                controller.Request.HttpContext.Response.Redirect("/Usuario/NeedAdmin");
            }
        }
    }
}