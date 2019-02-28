using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TestoviMVC.DBTestovi;
using TestoviMVC.Models;
using System.Linq.Dynamic;

namespace TestoviMVC.Controllers
{
    public class TestController : Controller
    {
        // GET: Test
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles ="Administrator")]
        public ActionResult Create(TestViewModel testVM)
        {
            using (var context = new TestoviContext())
            {
                Test test = new Test()
                {
                    Naziv = testVM.Naziv,
                    Opis = testVM.Opis,
                    ProcenatBodovaZaPolaganje = testVM.ProcenatBodovaZaPolaganje,
                    DatumKreiranja = testVM.DatumKreiranja
                };

                context.Tests.Add(test);
                context.SaveChanges();
            }

            return RedirectToAction("Index", "Pocetna");
        }

        [Authorize(Roles = "Administrator")]
        public ActionResult Edit(string id)
        {
            int testId = Convert.ToInt32(id);
            using (var context = new TestoviContext())
            {
                Test test = context.Tests.Find(testId);
                TestViewModel testVM = new TestViewModel()
                {
                    TestId = test.TestId,
                    Naziv = test.Naziv,
                    Opis = test.Opis,
                    ProcenatBodovaZaPolaganje = test.ProcenatBodovaZaPolaganje,
                    DatumKreiranja = test.DatumKreiranja
                };
                return View(testVM);
            }
        }

        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public ActionResult Edit(TestViewModel testVM)
        {
            using (var context = new TestoviContext())
            {
                Test test = context.Tests.Find(testVM.TestId);
                test.TestId = testVM.TestId;
                test.Naziv = testVM.Naziv;
                test.Opis = testVM.Opis;
                test.ProcenatBodovaZaPolaganje = testVM.ProcenatBodovaZaPolaganje;
                test.DatumKreiranja = testVM.DatumKreiranja;
                context.SaveChanges();
            }
            return RedirectToAction("Index", "Pocetna");
        }

        [Authorize(Roles = "Administrator")]
        public ActionResult Details(string id)
        {
            using (var context = new TestoviContext())
            {
                var testId = Convert.ToInt32(id);
                Test test = context.Tests.Find(testId);

                DetaljiTestaViewModel detaljiTestaVM = new DetaljiTestaViewModel()
                {
                    TestId = test.TestId,
                    Naziv = test.Naziv,
                    Opis = test.Opis,
                    ProcenatBodovaZaPolaganje = test.ProcenatBodovaZaPolaganje,
                    DatumKreiranja = test.DatumKreiranja,
                    ListaPitanja = test.Pitanjes.Select(p => new PitanjaViewModel()
                    {
                        PitanjeId = p.PitanjeId,
                        TestId = p.TestId,
                        Tekst = p.Tekst,
                        BrojBodova = p.BrojBodova,
                        ListaOdgovora = p.Odgovors.Select(o => new OdgovorViewModel()
                        {

                            OdgovorId = o.OdgovorId,
                            PitanjeId = o.PitanjeId,
                            Tekst = o.Tekst,
                            Tacan = o.Tacan
                        }).ToList()

                    }).ToList()

                };
                return View(detaljiTestaVM);
            };

        }

        [Authorize(Roles = "Korisnik")]
        public ActionResult PolaziTest(string id)
        {
            using (var context = new TestoviContext())
            {

                var testId = Convert.ToInt32(id);

                var test = context.Tests.Find(testId).Pitanjes
                    .Select(t => new UradiTestViewModel()
                    {
                        Tekst = t.Tekst,
                        PitanjeId = t.PitanjeId,
                        PrviOdgovor = t.Odgovors.ElementAt(0).Tekst,
                        DrugiOdgovor = t.Odgovors.ElementAt(1).Tekst,
                        TreciOdgovor = t.Odgovors.ElementAt(2).Tekst,
                        CetvrtiOdgovor = t.Odgovors.ElementAt(3).Tekst,
                        PrviId = t.Odgovors.ElementAt(0).OdgovorId,
                        DrugiId = t.Odgovors.ElementAt(1).OdgovorId,
                        TreciId = t.Odgovors.ElementAt(2).OdgovorId,
                        CetvrtiId = t.Odgovors.ElementAt(3).OdgovorId,
                        BrojBodova = t.BrojBodova,
                        TestId = t.TestId
                    }).ToList();

                return View(test);
            }
        }

        [HttpPost]
        [Authorize(Roles = "Korisnik")]
        public ActionResult PolaziTest(List<UradiTestViewModel> Model)
        {
            using (var context = new TestoviContext())
            {

                var korisnik = context.Korisniks.ToList().FirstOrDefault(k => k.KorisnickoIme == User.Identity.Name);

                KorisnikTest korisnikTest = new KorisnikTest()
                {
                    Datum = DateTime.Now,
                    KorisnikId = korisnik.KorisnikId,
                    TestId = Model.ElementAt(0).TestId,
                    BrojBodova = (short)Model.Where(p => context.Odgovors.Find(p.IzabraniOdgovor).Tacan != null).Sum(p => p.BrojBodova)
                };
                context.KorisnikTests.Add(korisnikTest);
                context.SaveChanges();

                var rezultat = new RezultatTestaViewModel()
                {
                    UkupanBrojBodova = korisnikTest.BrojBodova,
                    BrojTacnihOdgovora = Model.Where(p => context.Odgovors.Find(p.IzabraniOdgovor).Tacan != null).Count(),
                    BrojNetacnihOdgovora = Model.Where(p => context.Odgovors.Find(p.IzabraniOdgovor).Tacan == null).Count(),
                    ProcenatOsvojenihBodova = (short)((((double)korisnikTest.BrojBodova / context.Tests.Find(Model.ElementAt(0).TestId).Pitanjes.Sum(p => p.BrojBodova))) * 100.00)
                };

                return View("RezultatTesta", rezultat);
            }
        }

        [Authorize(Roles = "Administrator")]
        public ActionResult List(int id)
        {
            ViewBag.Id = id;
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public JsonResult ListaSvihPolaganjaTesta(string id, int jtStartIndex = 0, int jtPageSize = 0, string jtSorting = null)
        {
            try
            {
                var testId = Convert.ToInt32(id);
                using (var context = new TestoviContext())
                {
                    var polaganja = context.KorisnikTests.Where(t => t.TestId == testId).Select(t => new SvaPolaganjaViewModel
                    {
                        ImeKorisnika = t.Korisnik.Ime + " " + t.Korisnik.Prezime,
                        DatumPolaganja = t.Datum,
                        BrojOsvojenihBodova = t.BrojBodova,
                        ProcenatOsvojenihBodova = (short)(((double) t.BrojBodova / (double)(t.Test.Pitanjes.Sum(p => p.BrojBodova))) *100.00)
                    
                    }).ToList();

                    var count = polaganja.Count();
                    var records = polaganja.OrderBy(jtSorting).Skip(jtStartIndex).Take(jtPageSize).ToList();

                    //Return result to jTable
                    return Json(new { Result = "OK", Records = records, TotalRecordCount = count });
                }
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }


    }

}
