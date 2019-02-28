using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TestoviMVC.Models
{
    public class RezultatTestaViewModel
    {
        [Display(Name = "Broj tacnih odgovora")]
        public int BrojTacnihOdgovora { get; set; }
        [Display(Name = "Broj pogresnih odgovora")]
        public int BrojNetacnihOdgovora { get; set; }
        [Display(Name = "Ukupan broj bodova")]
        public int UkupanBrojBodova { get; set; }
        [Display(Name = "Procenat osvojenih bodova")]
        public int ProcenatOsvojenihBodova { get; set; }
    }
}