using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TestoviMVC.Models
{
    public class DetaljiTestaViewModel
    {
        public int TestId { get; set; }
        public string Naziv { get; set; }
        public string Opis { get; set; }
        [Display(Name = "Procenat bodova za polaganje")]
        public short ProcenatBodovaZaPolaganje { get; set; }
        [Display(Name = "Datum kreiranja")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public System.DateTime DatumKreiranja { get; set; }

        public List<PitanjaViewModel> ListaPitanja { get; set; }

    }
}