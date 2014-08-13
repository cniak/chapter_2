using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Dziennik_nauczyciela_obiektowy
{

    public class ListaDat
    {
        /// <summary>
        /// wczytuje liste wszystkich dat
        /// </summary>
        public List<data> zbior;
        private List<DateTime> daty = new List<DateTime>();
        private cSQLite SQLite;
        private klasa zalogowanaKlasa = null;
        private DateTime zaznaczonyElement;
        private MonthCalendar mc;
        private ComboBox cb_miesiac;
        /// <summary>
        /// ta lista po niczym nie dziedziczy!
        /// </summary>
        /// <param name="Dgv">jakie Dgv beda na nowo wczytywane</param>
        public ListaDat(MonthCalendar mc, klasa k, ComboBox cb_miesiac)
        {
            this.mc = mc;
            zalogowanaKlasa = k;
            if (this.cb_miesiac != null)
            {
                this.cb_miesiac = cb_miesiac;
            }
            SQLite = new cSQLite();
            this.mc.DateChanged += new System.Windows.Forms.DateRangeEventHandler(zmianaDaty);
            zaznaczonyElement = mc.SelectionStart;
            pobierzListe();
            aktualizujPogrubione();
        }

        private void zmianaDaty(object sender, DateRangeEventArgs e)
        {
            zaznaczonyElement = e.Start;
        }

        public void aktualizujPogrubione()
        {
            pobierzListe();
            mc.BeginInvoke(new Action(() =>
            {
                mc.RemoveAllBoldedDates();
                mc.BoldedDates = daty.ToArray();
            }));
        }

        public MonthCalendar Mc
        {
            get
            {
                return mc;
            }
        }
        public ComboBox Cb_miesiace
        {
            get { return cb_miesiac; }
            set 
            {
                cb_miesiac = value;
                cb_miesiac.ValueMember = "Key";
                cb_miesiac.DisplayMember = "Value";
                cb_miesiac.DataSource = new BindingSource(ustawMiesiacedlacb(zalogowanaKlasa.KlasaID), null);
                //ustawMiesiacedlacb();
            }
        }

        static public Dictionary<DateTime,string> ustawMiesiacedlacb(int klasaNR)
        {
            List<DateTime> ld = new List<DateTime>();
            List<data> zbior = data.pobierzWszystkich(klasaNR);
            foreach (data d in zbior)
            {
                ld.Add(d.Dzien);
            }

            //List<DateTime> ldd = ld.Select(d => new DateTime(2000, d.Month,1)).Where(d => d.Month == 5).Distinct().ToList();
            List<DateTime> ldd = ld.Select(d => new DateTime(d.Year, d.Month, 1)).Distinct().ToList();
            Dictionary<DateTime, string> ddt = new Dictionary<DateTime, string>();
            ddt.Add(new DateTime(1,2,3), "wszystkie");
            foreach (DateTime d in ldd)
            {
                try
                {
                    ddt.Add(new DateTime(d.Year,d.Month,1), d.ToString("MMMM", new CultureInfo("pl")) + " " + d.ToString("yyyy"));
                }
                catch (Exception) {
                    //TODO 1 jak jest juz taki miesiac to ma poprostu go nie dodawac
                }
            }
            return ddt;
            //cb_miesiac.DataSource = new BindingSource(ddt, null);
            
        }
        public DateTime ZaznaczonyElement
        {
            get
            {
                return zaznaczonyElement;
            }
        }
        private void pobierzListe()
        {
            data d = new data();
            
            zbior = data.pobierzWszystkich(zalogowanaKlasa.KlasaID);
            for (int i = 0; i < zbior.Count; i++)
            {
                daty.Add(zbior[i].Dzien);
            }
        }

    }
}
