using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dziennik_nauczyciela
{
    class cStaleProgramu
    {
        public static string nazwaPliku = @"bazadanych.db";
        //public static string nazwaUzytkownika = null;

        public string NazwaBazyDanych
        {
            set
            {
                if(nazwaPliku == null) nazwaPliku = value;
            }
            get
            {
                return nazwaPliku;
            }
        }

    }
}
