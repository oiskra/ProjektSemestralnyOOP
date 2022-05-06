using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjektSemestralnyOOP.MVVM.Model
{
    public class Race
    {
        public int Id { get; set; }
        public string PlayerOne { get; set; }
        public string CarOne { get; set; }
        public string PlayerTwo { get; set; }
        public string CarTwo { get; set; }
        public string Winner { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }
    }
}
