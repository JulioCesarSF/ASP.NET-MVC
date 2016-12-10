using Leilao.Dominio.Models;
using Leilao.MVC.Web.App_Start;
using Leilao.MVC.Web.ViewModels;
using Leilao.Persistencia.UnitsOfWork;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Leilao.MVC.Web.Controllers
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
            return View();
        }

        [HttpGet]
        public ActionResult Registrar()
        {
            return View();
        }

        //método para testes de login, ver PainelController
        [HttpGet]
        [Authorize]
        public ActionResult Painel(string idUser)
        {
            var model = new PessoaViewModel()
            {
                //idUser que será usado para add produtos, compras e vendas
                IdUser = idUser
            };
            return View(model);
        }

        [HttpGet]
        public ActionResult Logout()
        {
            GetAuthenticationManager().SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction("Painel", "User");            
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
                return RedirectToAction("Painel", "Painel", new { idUser = pessoa.IdUser });
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
            return RedirectToAction("Painel", "Painel", new { idUser = identity.GetUserId() });
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