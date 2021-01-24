using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class User
    {
        public string UserName { get; set; }
        public int HashPassword { get; set; }
        public bool Admin { get; set; }
        public bool Active { get; set; }
    }
}
