using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NipssDOS.Data.Model
{
    public class SliderCategory
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public bool Active { get; set; }
        public ICollection<Slider> Sliders { get; set; }
        public DateTime Date { get; set; }

        public long? AlumniId { get; set; }
        public Alumni Alumni { get; set; }

    }
}
