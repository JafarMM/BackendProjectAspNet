using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BackendProject.Models
{
    public class Bio
    {
        public int Id { get; set; }
        public int Number { get; set; }

        [Required]
        public string Logo { get; set; }

        [Required]
        public string FooterLogo { get; set; }
        public string FacebookUrl { get; set; }
        public string PinterestUrl { get; set; }
        public string Vurl { get; set; }
        public string TwitterUrl { get; set; }

        [Required]
        public string Adress { get; set; }
        public int FooterNumber { get; set; }
        public string Email { get; set; }

    }
}
