﻿using System;
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

                    return View(testovi);
                }

            }
        }
    }
}