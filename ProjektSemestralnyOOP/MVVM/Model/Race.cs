using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjektSemestralnyOOP.MVVM.Model
{
    public class Race
    {
        public int Id { get; set; }
        public string RacerOne { get; set; }
        public string CarOne { get; set; }
        public string RacerTwo { get; set; }
        public string CarTwo { get; set; }
        public string Winner { get; set; }
    }
}
