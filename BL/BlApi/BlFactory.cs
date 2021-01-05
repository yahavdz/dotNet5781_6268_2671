using System;
using System.Collections.Generic;
using System.Text;

namespace BL.BlApi
{
    public static class BLFactory
    {
        public static IBL GetBL(string type)
        {
            switch (type)
            {
                case "1":
                    return BLImp.Instance;
                default:
                    return BLImp.Instance;
            }
        }
    }
}
