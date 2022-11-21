﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NipssDOS.Data.Model
{
    public class StudyGroupMemeber
    {
        public long Id { get; set; }
        public long ProfileId { get; set; }
        public Profile Profile { get; set; }
        public int SortNumber { get; set; }
        public string Position { get; set; }
        public bool DS { get; set; }

        public long StudyGroupId { get; set; }
        public StudyGroup StudyGroup { get; set; }
    }
}
