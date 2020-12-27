using System;
using System.Collections.Generic;
using System.Text;


namespace DalApi
{
    internal static class DalFactory
    {
        public static IDal GetDal()
        {
            return new DalObject.DALObject;//TODO
        }
    }
}
