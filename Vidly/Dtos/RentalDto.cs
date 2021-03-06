﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Vidly.Dtos
{
    public class RentalDto
    {
        [Required]
        public int CustomerId { get; set; }

        [Required]
        public List<int> Movies { get; set; }
    }
}