using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestoviMVC.Models
{
    public class PocetnaAdminViewModel
    {
        public int TestId { get; set; }
        public string Naziv { get; set; }
        public string Opis { get; set; }
        public short ProcenatBodovaZaPolaganje { get; set; }
        public System.DateTime DatumKreiranja { get; set; }


    }
}