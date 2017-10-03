using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HikeShare.Models
{
    public class Hike
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public float LengthInMiles { get; set; }
    }
}
