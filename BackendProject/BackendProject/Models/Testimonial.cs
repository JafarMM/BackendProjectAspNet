﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BackendProject.Models
{
    public class Testimonial
    {
        public int Id { get; set; }
        public string Description { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Position { get; set; }
        public string Image { get; set; }
    }
}
