﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Models.Animal
{
    public class HealthLibrary
    {
        public int Id { get; set; }
        public Enum Status { get; set; }
        public bool Vacination { get; set; }
    }
}
