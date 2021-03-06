﻿using Bidme.Dominio.Models;
using Bidme.MVC.Web.App_Start;
using Bidme.MVC.Web.ViewModels;
using Bidme.Persistencia.UnitsOfWork;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Bidme.MVC.Web.Controllers
{
    //Classe responsável pelo Login, Logout e Registro de Usuário
    // **** Não alterar ****
    public class UserController : Controller
    {
        #region FIELDs
        private readonly UserManager<User> userManager;
        private UnitOfWork _unit = new UnitOfWork();
        #endregion

        #region CONSTRUCTORs
        public UserController()
        {
            userManager = IdentityConfig.UserManagerFactory.Invoke();
        }
        #endregion

        #region GETs
        [HttpGet]
        public ActionResult Login()
        {
            string idUser = User.Identity.GetUserId();
            if (idUser == null || idUser == "" || idUser.Equals(""))
            {
                return View();
            }                
            else
            {
                return RedirectToAction("Compra", "Comprar");
            }                
        }

        [HttpGet]
        public ActionResult Registrar()
        {
            return View();
        }

        //método de página Inicial (index.cshtml)
        [HttpGet]
        [Authorize]        
        public ActionResult Index(string idUser)
        {
            var model = new PessoaViewModel();
            var pessoa = _unit.PessoaRepository.BuscarPor(p => p.IdUser == idUser);
            if (pessoa.Count == 1)
            {
                var p = pessoa.First();
                model = new PessoaViewModel()
                {
                    Nome = p.Nome,
                    //idUser que será usado para add produtos, compras e vendas
                    IdUser = idUser
                };
                return View(model);
            }

            model = new PessoaViewModel()
            {
                IdUser = "0"
            };
            return View(model);
        }
        #endregion

        #region POSTs
        [HttpPost]
        public async Task<ActionResult> Registrar(UsuarioViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var user = new User()
            {
                UserName = model.User.Email
            };

            //gerar um User no banco
            var result = await userManager.CreateAsync(user, model.User.Password);

            if (result.Succeeded)
            {
                //gerar a identidade
                var identity = await userManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);                

                //cadastrar a Pessoa no banco
                var pessoa = new Pessoa()
                {                    
                    Nome = model.Pessoa.Nome,
                    Cpf = model.Pessoa.Cpf,
                    Cep = model.Pessoa.Cep,
                    Numero = model.Pessoa.Numero,
                    Complemento = model.Pessoa.Complemento,
                    DataNascimento = model.Pessoa.DataNascimento,
                    Telefone = model.Pessoa.Telefone,
                    IdUser = identity.GetUserId() //coluna que será usada nas negociacoes
                };

                _unit.PessoaRepository.Cadastrar(pessoa);
                _unit.Salvar();

                //logar a identidade
                GetAuthenticationManager().SignIn(identity);

                //redirecionar para o Painel do Usuário com o idUser no PainelController
                return RedirectToAction("Comprar", "Compra", new { idUser = pessoa.IdUser });
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
            return View();
        }
        
        [HttpPost]
        public async Task<ActionResult> Login(UserViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            //encontrar o User no banco
            var user = await userManager.FindAsync(model.Email, model.Password);
            if(user == null)
            {
                ModelState.AddModelError("", "Usuário e/ou Senha inválidos.");
                return View();
            }

            //gerar identidade
            var identity = await userManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);
            GetAuthenticationManager().SignIn(identity);
            //voltar a para View que tentou acessar
            string userId = identity.GetUserId();
            return RedirectToAction("Comprar", "Compra", new { idUser = userId });
        }

        [HttpPost]
        [Authorize]
        public ActionResult Logout()
        {
            GetAuthenticationManager().SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction("Index", "User");
        }
        #endregion

        #region PRIVATE
        private IAuthenticationManager GetAuthenticationManager()
        {
            var ctx = Request.GetOwinContext();
            return ctx.Authentication;
        }

        private string GetRedirectUrl(string returnUrl)
        {
            if (string.IsNullOrEmpty(returnUrl) || !Url.IsLocalUrl(returnUrl))
            {
                return Url.Action("Painel", "User");
            }
            return returnUrl;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && userManager != null)
            {
                userManager.Dispose();
            }
            base.Dispose(disposing);
        }
        #endregion
    }
}