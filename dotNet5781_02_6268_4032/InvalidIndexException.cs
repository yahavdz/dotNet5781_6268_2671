using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNet5781_02_6268_4032
{
    class InvalidIndexException: Exception
    {
        public InvalidIndexException() { }
        public InvalidIndexException(int msg) : base(String.Format("Invalid Index : {0}", msg)) { }
    }
}
