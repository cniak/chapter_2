using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dziennik_nauczyciela
{
    class cStaleProgramu
    {
        public static string nazwaBazyDanych = @"database.db";
        //public static string nazwaUzytkownika = null;

        public string NazwaBazyDanych
        {
            set
            {
                if(nazwaBazyDanych == null) nazwaBazyDanych = value;
            }
            get
            {
                return nazwaBazyDanych;
            }
        }

    }
}
