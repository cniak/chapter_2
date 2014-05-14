using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dziennik_nauczyciela_obiektowy
{
    public abstract class bazadanych
    {
        protected cSQLite SQLite;
        public bool wylaczEdycje = true;
    
        public abstract void dodajDoBazy();
        public abstract void aktualizuj(params string[] elementy);
        protected abstract void wykonajZapytanie(rodzajZapytania rodzaj);
        public abstract bool usun();
    }
}
