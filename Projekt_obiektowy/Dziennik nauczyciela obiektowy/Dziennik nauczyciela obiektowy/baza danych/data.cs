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
        private string dataKolumny;
        public data()
        {
            SQLite = new cSQLite();
            wylaczEdycje = true;
        }
        public data(int dataID)
        {
            //this.klasaNR = klasaID;
            this.dataID = dataID;
            SQLite = new cSQLite();
            SQLite.Zapytanie = "SELECT * FROM data WHERE dataID = " + this.dataID + " ORDER BY dzien;";
            wykonajZapytanie(rodzajZapytania.pobierz);
            wylaczEdycje = false;
        }

        /// <summary>
        /// tworzy obiekt na podstawie podanej daty i klasy
        /// </summary>
        /// <param name="dataKolumny"></param>
        /// <param name="klasaNR"></param>
        public data(string dataKolumny, int klasaNR)
        {
            // TODO: Complete member initialization
            this.dataKolumny = dataKolumny;
            this.klasaNR = klasaNR;
            SQLite = new cSQLite();
            SQLite.Zapytanie = "SELECT * FROM data WHERE klasaNR = " + klasaNR + " AND dzien = '" + dataKolumny + "';";
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

        public int DataID
        {
            get
            {
                return dataID;
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
            SQLite.Zapytanie = "SELECT * FROM data WHERE klasaNR = " + klasaNR + " ORDER BY dzien;";
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
