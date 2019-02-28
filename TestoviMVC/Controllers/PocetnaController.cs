using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;
using System.Web;
using System.Web.Mvc;
using TestoviMVC.DBTestovi;
using TestoviMVC.Models;

namespace TestoviMVC.Controllers
{
    public class PocetnaController : Controller
    {
        // GET: PocetnaAdmin
        [AllowAnonymous]
        public ActionResult Index()
        {
            {
                using (var context = new TestoviContext())
                {
                    var testovi = context.Tests.Select(t => new PocetnaAdminViewModel
                    {
                        TestId = t.TestId,
                        Naziv = t.Naziv,
                        Opis = t.Opis,
                        ProcenatBodovaZaPolaganje = t.ProcenatBodovaZaPolaganje,
                        DatumKreiranja = t.DatumKreiranja

                    }).OrderByDescending(t=> t.DatumKreiranja).ToList();

                    var najpopularnijitestovi = context.Tests
                        .Select(t => new NajpopularnijiTestoviViewModel()
                        {
                            TestId = t.TestId,
                            Naziv = t.Naziv,
                            Opis = t.Opis,
                            BrojPolaganja = t.KorisnikTests.Count
                        }).ToList().OrderByDescending(t=> t.BrojPolaganja).Take(5);

                    ViewBag.NajpopularnijiTestovi = najpopularnijitestovi;

                    var triNajboljaKandidata = context.Korisniks.Where(k => k.Uloga.Naziv == "Korisnik" && k.KorisnikTests.Count > 0)
                     .Select(k => new TriNajboljaKandidataViewModel()
                     {
                         Ime = k.Ime + " " + k.Prezime,                        
                         ProcenatOdgovorenihPitanja = k.KorisnikTests.Average(t => ((((double)t.BrojBodova / (double)(t.Test.Pitanjes.Sum(p => p.BrojBodova))) * 100.00)))
                     }).OrderByDescending(k => k.ProcenatOdgovorenihPitanja).Take(3).ToList();

                    ViewBag.triNajboljaKandidata = triNajboljaKandidata;

                    return View(testovi);
                }

            }
        }


      
    }
}