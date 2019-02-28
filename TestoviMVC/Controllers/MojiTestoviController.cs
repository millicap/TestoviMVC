using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TestoviMVC.DBTestovi;
using TestoviMVC.Models;

namespace TestoviMVC.Controllers
{
    public class MojiTestoviController : Controller
    {
        // GET: MojiTestovi
        [Authorize(Roles = "Korisnik")]
        public ActionResult Index()
        {
            using (var context = new TestoviContext())
            {
                var korisnik = context.Korisniks.FirstOrDefault(k => k.KorisnickoIme == User.Identity.Name);  

                var mojiTestovi = korisnik.KorisnikTests.Select(kt => new MojiTestoviViewModel()
                {
                    NazivTesta = kt.Test.Naziv,
                    DatumPolaganja = kt.Datum,
                    BrojOsvojenihBodova = kt.BrojBodova

                }).OrderByDescending(t => t.BrojOsvojenihBodova).ToList();
                return View(mojiTestovi);
            }
               
        }
    }
}