using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TestoviMVC.Models
{
    public class LogovanjeViewModel
    {
        [Required]
        [MaxLength(20)]
        [MinLength(4)]
        [Display(Name = "Korisnicko ime")]
        public string KorisnickoIme { get; set; }
        [Required]
        [MaxLength(20)]
        [MinLength(6)]
        public string Lozinka { get; set; }
    }
}