using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NipssDOS.Data.Model
{
    public class SecProject
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string SubTitle { get; set; }
        public string Description { get; set; }
        public string SideViewImage { get; set; }
        public string FrontViewImage { get; set; }
        public string TopViewImage { get; set; }
        public long? AlumniId { get; set; }
        public Alumni Alumni { get; set; }

        public ICollection<SecProjectExecutive> SecProjectExecutives { get; set; }
    }
}
