using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Dziennik_nauczyciela_obiektowy
{
    public class data : bazadanych
    {
        private int dataID = -1;
        private int klasaNR = -1;
        private DateTime dzien;
        public data()
        {
            SQLite = new cSQLite();
            wylaczEdycje = true;
        }
        public data(int klasaID)
        {
            this.klasaNR = klasaID;
            SQLite = new cSQLite();
            SQLite.Zapytanie = "SELECT * FROM data WHERE dataID = " + this.klasaNR + ";";
            wykonajZapytanie(rodzajZapytania.pobierz);
            wylaczEdycje = false;
        }

        public DateTime Dzien
        {
            get
            {
                return dzien;
            }
            set
            {
                dzien = value;
                //if (!wylaczEdycje) aktualizuj("dzien");
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
                klasaNR = value;
                //if (!wylaczEdycje) aktualizuj("Mc");
            }
        }
        public override void dodajDoBazy()
        {
            SQLite.Zapytanie = "INSERT INTO data(dzien, klasaNR) VALUES('" + dzien.ToString("yyyy-MM-dd HH:mm:ss.fff") + "', " + this.klasaNR + ");";
            SQLite.wykonajZapytanie(rodzajZapytania.wyslij);
        }

        public override void aktualizuj(params string[] elementy)
        {
            // nie bedzie
            throw new NotImplementedException();
        }

        protected override void wykonajZapytanie(rodzajZapytania rodzaj)
        {
            if (rodzaj == rodzajZapytania.wyslij) SQLite.wykonajZapytanie(rodzaj);
            else
            {
                SQLite.otworzPolaczenie();
                SQLite.DataReader = SQLite.command.ExecuteReader();
                while (SQLite.DataReader.Read())
                {
                    dataID = Convert.ToInt32(SQLite.DataReader["dataID"].ToString());
                    klasaNR = Convert.ToInt32(SQLite.DataReader["klasaNR"].ToString());
                    dzien = (DateTime)SQLite.DataReader["dzien"];
                }
            }
        }
        public static List<data> pobierzWszystkich(int klasaNR)
        {
            List<data> listaDat = new List<data>();
            cSQLite SQLite = new cSQLite();
            SQLite.Zapytanie = "SELECT * FROM data WHERE klasaNR = " + klasaNR + ";";
            SQLite.DataReader = SQLite.command.ExecuteReader();
            while (SQLite.DataReader.Read())
            {
                int dataID = Convert.ToInt32(SQLite.DataReader["dataID"].ToString());
                data d = new data(dataID);
                listaDat.Add(d);
            }
            return listaDat;
        }
        public override bool usun()
        {
            // nie bedzie
            throw new NotImplementedException();
        }
    }
}
