using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestoviMVC.Models
{
    public class SvaPolaganjaViewModel
    {
        public string ImeKorisnika { get; set; }
        public DateTime DatumPolaganja { get; set; }
        public short ProcenatOsvojenihBodova { get; set; }
        public short BrojOsvojenihBodova { get; set; }
    }
}