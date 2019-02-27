using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TestoviMVC.DBTestovi;
using TestoviMVC.Models;

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

        public ActionResult PolaziTest(string id)
        {
            using (var context = new TestoviContext())
            {
                var testId = Convert.ToInt32(id);



            }
        }

    }

}
