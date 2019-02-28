using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TestoviMVC.DBTestovi;
using TestoviMVC.Models;

namespace TestoviMVC.Controllers
{
    public class PitanjaController : Controller
    {
        // GET: Pitanja
        [Authorize(Roles = "Administrator")]
        public ActionResult Create(string id)
        {
            return View(new PitanjeCreateViewModel() {TestId= Convert.ToInt32(id) });
        }

        [HttpPost, ValidateInput(false)]
        [Authorize(Roles = "Administrator")]
        public ActionResult Create(PitanjeCreateViewModel pitanjeCreateVM)
        {
            using (var context = new TestoviContext())
            {
                Pitanje pitanje = new Pitanje()
                {
                    PitanjeId = pitanjeCreateVM.PitanjeId,
                    TestId = pitanjeCreateVM.TestId,
                    Tekst = pitanjeCreateVM.Tekst,
                    BrojBodova = pitanjeCreateVM.BrojBodova,

                };

                pitanje.Odgovors.Add(new Odgovor() { Tekst = pitanjeCreateVM.PrviOdgovor });
                pitanje.Odgovors.Add(new Odgovor() { Tekst = pitanjeCreateVM.DrugiOdgovor });
                pitanje.Odgovors.Add(new Odgovor() { Tekst = pitanjeCreateVM.TreciOdgovor });
                pitanje.Odgovors.Add(new Odgovor() { Tekst = pitanjeCreateVM.CetvrtiOdgovor });

                for (int i = 0; i < pitanje.Odgovors.Count; i++)
                {
                    if ((i + 1) == pitanjeCreateVM.TacanOdgovor)
                        pitanje.Odgovors.ElementAt(i).Tacan = true;
                }

                context.Pitanjes.Add(pitanje);
                context.SaveChanges();
            }

                return RedirectToAction("Details", "Test", new {id = pitanjeCreateVM.TestId });
        }



    }
}