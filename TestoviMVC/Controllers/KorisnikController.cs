using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TestoviMVC.DBTestovi;
using System.Linq.Dynamic;
using TestoviMVC.Models;

namespace TestoviMVC.Controllers
{
    public class KorisnikController : Controller
    {
        // GET: Korisnik
        public ActionResult Index()
        {
            return View();
        }


        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public JsonResult List(int jtStartIndex = 0, int jtPageSize = 0, string jtSorting = null)
        {
            try
            {
                using (var context = new TestoviContext())
                {
                    var korisnici = context.Korisniks.Select(k => new KorisnikViewModel
                    {
                        KorisnikId = k.KorisnikId,
                        Ime= k.Ime,
                        Prezime=k.Prezime,
                        Email= k.Email,
                        KorisnickoIme = k.KorisnickoIme,
                        Lozinka = k.Lozinka,
                        UlogaId = k.UlogaId
                    }).ToList();

                    var count = korisnici.Count();
                    var records = korisnici.OrderBy(jtSorting).Skip(jtStartIndex).Take(jtPageSize).ToList();

                    //Return result to jTable
                    return Json(new { Result = "OK", Records = records, TotalRecordCount = count });
                }
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public JsonResult Create(KorisnikViewModel korisnikVM)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return Json(new { Result = "ERROR", Message = "Form is not valid! Please correct it and try again." });
                }

                using (var context = new TestoviContext())
                {
                    Korisnik korisnik = new Korisnik()
                    {
                        KorisnikId = korisnikVM.KorisnikId,
                        Ime = korisnikVM.Ime,
                        Prezime = korisnikVM.Prezime,
                        Email = korisnikVM.Email,
                        KorisnickoIme = korisnikVM.KorisnickoIme,
                        Lozinka = korisnikVM.Lozinka,
                        UlogaId = korisnikVM.UlogaId
                       
                    };

                    context.Korisniks.Add(korisnik);
                    context.SaveChanges();

                    korisnikVM.KorisnikId = korisnik.KorisnikId;

                    return Json(new { Result = "OK", Record = korisnikVM });
                }
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public JsonResult Update(KorisnikViewModel korisnikVM)
        {
           // try
           // {
                if (!ModelState.IsValid)
                {
                    return Json(new { Result = "ERROR", Message = "Form is not valid! Please correct it and try again." });
                }
                using (var context = new TestoviContext())
                {
                    Korisnik korisnikUpdate = context.Korisniks.Find(korisnikVM.KorisnikId);

                    korisnikUpdate.KorisnikId = korisnikVM.KorisnikId;
                    korisnikUpdate.Ime = korisnikVM.Ime;
                    korisnikUpdate.Prezime = korisnikVM.Prezime;
                    korisnikUpdate.Email = korisnikVM.Email;
                    korisnikUpdate.KorisnickoIme = korisnikVM.KorisnickoIme;
                    //korisnikUpdate.Lozinka = korisnikVM.Lozinka;
                    korisnikUpdate.UlogaId = korisnikVM.UlogaId;
                 
                    context.SaveChanges();
                }
                return Json(new { Result = "OK" });
            //}
           //catch (Exception ex)
            //{
                //return Json(new { Result = "ERROR", Message = ex.Message });
            //}
        }


        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public JsonResult Delete(int korisnikId)
        {
            try
            {
                using (var context = new TestoviContext())
                {

                    context.Korisniks.Remove(context.Korisniks.Find(korisnikId));
                    context.SaveChanges();
                }
                return Json(new { Result = "OK" });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        [HttpPost]
        public JsonResult VratiUlogu()
        {
            try
            {
                using (var context = new TestoviContext())
                {
                    var uloge = context.Ulogas.Select(u => new
                    {
                        Value = u.UlogaId,
                        DisplayText = u.Naziv

                    }).ToList();
                    //Return result to jTable
                    return Json(new { Result = "OK", Options = uloge });
                }
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

    }
}