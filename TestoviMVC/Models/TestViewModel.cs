using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TestoviMVC.Models
{
    public class TestViewModel
    {
        public int TestId { get; set; }
        public string Naziv { get; set; }
        public string Opis { get; set; }
        [Display(Name = "Procenat bodova za polaganje")]
        [Range(0, 100)]
        public short ProcenatBodovaZaPolaganje { get; set; }
        [Display(Name = "Datum kreiranja")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public System.DateTime DatumKreiranja { get; set; }
    }
}