﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NipssDOS.Data.Model
{
    public class News
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string ShortDescription { get; set; }
        public string SortOrder { get; set; }
        public string Image { get; set; }
        public string Source { get; set; }
        public DateTime Date { get; set; }
        public long? AlumniId { get; set; }
        public Alumni Alumni { get; set; }
        public ICollection<Comment> Comments { get; set; }
    }
}
