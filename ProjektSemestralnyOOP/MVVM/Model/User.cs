using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjektSemestralnyOOP.MVVM.Model
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public int Money { get; set; }

        public List<Car> Cars { get; set; }
    }
}
