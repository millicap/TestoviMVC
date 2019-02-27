using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TestoviMVC.Models
{
    public class PitanjaViewModel
    {
        public int PitanjeId { get; set; }
        public int TestId { get; set; }
        public string Tekst { get; set; }
        [Display(Name ="Broj bodova")]
        public short BrojBodova { get; set; }

        public List<OdgovorViewModel> ListaOdgovora { get; set; }
    }
}