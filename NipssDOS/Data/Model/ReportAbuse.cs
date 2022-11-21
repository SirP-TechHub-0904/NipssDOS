using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NipssDOS.Data.Model
{
    public class ReportAbuse
    {
        public long Id { get; set; }
        public long QuestionnerId { get; set; }
        public String Note { get; set; }
    }
}
