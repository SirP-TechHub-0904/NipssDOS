﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NipssDOS.Data.Model
{
    public class RapidQuestion
    {
        public long Id { get; set; }
        public string Question { get; set; }
        public RapidOption RapidOptions { get; set; }
    }
}
