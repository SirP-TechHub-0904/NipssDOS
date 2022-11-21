﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NipssDOS.Data.Model
{
    public class State
    {
        public long Id { get; set; }


        public string StateName { get; set; }

        public virtual ICollection<LocalGoverment> LocalGov { get; set; }

    }
}
