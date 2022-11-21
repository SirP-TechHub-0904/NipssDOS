using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NipssDOS.Data.Model
{
    public class MaintainaceSetting
    {
        public long Id { get; set; }
        public int FirstResponceTimeAfterSubmit { get; set; }
        public int SecondResponceTimeAfterApproval { get; set; }
    }
}
