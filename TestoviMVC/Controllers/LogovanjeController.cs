using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using TestoviMVC.DBTestovi;
using TestoviMVC.Models;

namespace TestoviMVC.Controllers
{
    public class LogovanjeController : Controller
    {
        // GET: Logovanje
        public ActionResult UlogujSe()
        {
            return View();
        }

        // POST: Account/Create
        [AllowAnonymous]
        [HttpPost]
        public ActionResult UlogujSe(LogovanjeViewModel viewModel, string returnUrl)
        {
            using (var context = new TestoviContext())
            {
                var korisnik = context.Korisniks.FirstOrDefault(k => k.KorisnickoIme == viewModel.KorisnickoIme && k.Lozinka == viewModel.Lozinka);

                if (korisnik != null)
                {
                    var authTicket = new FormsAuthenticationTicket(
                                                         1,
                                                         viewModel.KorisnickoIme,
                                                         DateTime.Now,
                                                         DateTime.Now.AddMinutes(30),
                                                         false,
                                                         //"User",     ovaj ne moze jer ne mogu 2 user-a u 2 stringa
                                                          korisnik.Uloga.Naziv      //ako moze imati samo jednu ulogu

                                                         //Primjer dodavanja uloga za korisnika
                                                         //string.Join(",", korisnik.KorisnikUlogas.Select(o => o.Uloga.Naziv).Distinct()) ako moze imati vise uloga
                                                         );

                    var encTicket = FormsAuthentication.Encrypt(authTicket);
                    var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encTicket);
                    Response.Cookies.Add(cookie);

                    if (Url.IsLocalUrl(returnUrl) && returnUrl.Length > 1 && returnUrl.StartsWith("/") && !returnUrl.StartsWith("//") && !returnUrl.StartsWith("/\\"))
                    {
                        return Redirect(returnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Pocetna");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Pogresno korisnicko ime ili lozinka");
                    return View();
                }
            }
        }

        [Authorize(Roles = "Administrator, Korisnik")]
        public ActionResult Odjava()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("UlogujSe", "Logovanje");
        }
    }
}