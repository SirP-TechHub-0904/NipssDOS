﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NipssDOS.Data.Model
{
    public class ParlySubTwoCategory
    {
        public ParlySubTwoCategory()
        {
            Date = DateTime.UtcNow;
        }
        public long Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public int SortOrder { get; set; }
        public string BgColor { get; set; }

        public long? AlumniId { get; set; }
        public Alumni Alumni { get; set; }
        public long? ParlyReportSubCategoryId { get; set; }
        public ParlyReportSubCategory ParlyReportSubCategory {get;set;}
        public ICollection<ParlyReportDocument> ParlyReportDocuments { get; set; }
        public ICollection<ParlySubThreeCategory> ParlySubThreeCategories { get; set; }

    }
}
