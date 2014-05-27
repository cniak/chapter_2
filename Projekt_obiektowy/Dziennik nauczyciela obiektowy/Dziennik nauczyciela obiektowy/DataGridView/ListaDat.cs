using System;
using System.Collections.Generic;
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
        private int klasaNR = -1;
        private DateTime zaznaczonyElement;
        private MonthCalendar mc;
        /// <summary>
        /// ta lista po niczym nie dziedziczy!
        /// </summary>
        /// <param name="Dgv">jakie Dgv beda na nowo wczytywane</param>
        public ListaDat(MonthCalendar mc, int klasaID)
        {
            this.mc = mc;
            this.klasaNR = klasaID;
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
        public int KlasaNR
        {
            get
            {
                return klasaNR;
            }
            set
            {
            }
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
            
            zbior = data.pobierzWszystkich(klasaNR);
            for (int i = 0; i < zbior.Count; i++)
            {
                daty.Add(zbior[i].Dzien);
            }
        }

    }
}
