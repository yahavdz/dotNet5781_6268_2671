using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace BL.BlApi
{
    public static class BLFactory
    {
        public static IBL GetBL(string type)
        {
            switch (type)
            {
                case "1":
                    return new BLImp(); //BLImp.Instance;
                default:
                    return new BLImp(); //BLImp.Instance;
            }
        }
    }
}
