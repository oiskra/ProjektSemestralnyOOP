using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjektSemestralnyOOP.MVVM.Model
{
    public class Statistic
    {
        public int Id { get; set; }
        public int Speed { get; set; }
        public int Acceleration { get; set; }
        public int Grip { get; set; }
        public int Braking { get; set; }


        public int CarId { get; set; }
        public Car Car { get; set; }
    }
}
