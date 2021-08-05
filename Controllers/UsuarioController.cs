using Biblioteca.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Http;

namespace Biblioteca.Controllers
{
    public class UsuarioController: Controller
    {
        public IActionResult ListaDeUsuarios()
        {
            Autenticacao.CheckLogin(this);
            //Autenticacao.verificaSeUsuarioEAdmin(this);

            return View(new UsuarioService().Listar());
        }

        public IActionResult editarUsuario(int id)
        {
            Usuario u = new UsuarioService().Listar(id);

            return View(u);
        }

        [HttpPost]
        public IActionResult editarUsuario(Usuario userEditado)
        {
            UsuarioService us = new UsuarioService();
            us.editarUsuario(userEditado);

            return RedirectToAction("ListaDeUsuarios");

        }

        public IActionResult RegistrarUsuario()
        {
            Autenticacao.CheckLogin(this);
            //Autenticacao.verificaSeUsuarioEAdmin(this);
            return View();
        }

        [HttpPost]
        public IActionResult RegistrarUsuario(Usuario novoUser)
        {
            Autenticacao.CheckLogin(this);
            //Autenticacao.verificaSeUsuarioEAdmin(this);

            //novoUser.Senha = Criptografo.TextoCriptografado(novoUser.Senha);
            UsuarioService us = new UsuarioService();
            us.incluirUsuario(novoUser);

            return RedirectToAction("cadastroRealizado");
        }

        public IActionResult ExcluirUsuario(int id)
        {
            return View(new UsuarioService().Listar(id));
        }

        [HttpPost]
        public IActionResult ExcluirUsuario(string decisao, int id)
        {
            if(decisao=="EXCLUIR")
            {
                new UsuarioService().excluirUsuario(id);
                return View("ListaDeUsuarios", new UsuarioService().Listar());
            }
            else
            {
                return View("ListaDeUsuarios", new UsuarioService().Listar());
            }
        }

        public IActionResult cadastroRealizado()
        {
            Autenticacao.CheckLogin(this);
            //Autenticacao.verificaSeUsuarioEAdmin(this);
            return View();
        }

        //public IActionResult NeedAdmin()
        //{
            //Autenticacao.CheckLogin(this);
            //return View();
        //}

        public IActionResult Sair()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }




    }
}