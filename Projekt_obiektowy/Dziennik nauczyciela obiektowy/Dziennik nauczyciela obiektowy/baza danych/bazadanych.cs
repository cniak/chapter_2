using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Dziennik_nauczyciela_obiektowy
{
    public abstract class bazadanych
    {
        protected cSQLite SQLite;
        public bool wylaczEdycje = true;
    
        public abstract void dodajDoBazy();
        public abstract void aktualizuj(params string[] elementy);
        protected abstract void wykonajZapytanie(ERodzajZapytania rodzaj);
        public abstract bool usun();
        protected bool walidacjaMaila(string mail)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(mail);
                return true;
            }
            catch
            {
                MessageBox.Show("niepoprawny email, nie zostanie on zapisany");
                return false;
            }
        }
    }
}
