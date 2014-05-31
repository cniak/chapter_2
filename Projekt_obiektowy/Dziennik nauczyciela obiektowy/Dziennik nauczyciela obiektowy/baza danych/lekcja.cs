using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dziennik_nauczyciela_obiektowy
{
    public class lekcja : bazadanych
    {
        private int lekcjaID = -1;
        private int klasaNR;
        private int przedmiotNR;
        private int dataNR;
        private string dataKolumny;

        public int LekcjaID
        {
            get
            {
                return lekcjaID;
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
            }
        }
        public int PrzedmiotNR
        {
            get
            {
                return przedmiotNR;
            }
            set
            {
                przedmiotNR = value;
            }
        }
        public int DataNR
        {
            get
            {
                return dataNR;
            }
            set
            {
                dataNR = value;
            }
        }
       /// <summary>
       /// tworzy obiekt lekcja
       /// </summary>
        public lekcja()
        {
            SQLite = new cSQLite();
            wylaczEdycje = false;
        }
        /// <summary>
        /// pobiera obiekt na podstawie: dataKolumny, przedmiotNR, klasaNR
        /// </summary>
        /// <param name="lekcjaID"></param>
        public lekcja(string dataKolumny, int przedmiotNR, int klasaNR)
        {
            SQLite = new cSQLite();
            SQLite.Zapytanie = "SELECT " +
                               "* " +
                               "FROM " +
                               "lekcja " +
                               "LEFT JOIN data ON lekcja.dataNR = data.dataID AND data.klasaNR = " + klasaNR + " " +
                               "WHERE " +
                               "data.dzien ='" + dataKolumny + "' AND lekcja.przedmiotNR = " + przedmiotNR + ";";
            this.dataKolumny = dataKolumny;
            
            data d = new data(dataKolumny, klasaNR);
            this.dataNR = d.DataID;

            this.przedmiotNR = przedmiotNR;
            this.klasaNR = klasaNR;
            wykonajZapytanie(ERodzajZapytania.pobierz);
            wylaczEdycje = false;
        }
        public lekcja(int dataNR, int przedmiotNR, int klasaNR)
        {
            SQLite = new cSQLite();
            SQLite.Zapytanie = "SELECT " +
                                "* " +
                                "FROM " +
                                "lekcja " +
                                "WHERE " +
                                "lekcja.dataNR = " + dataNR + " AND lekcja.przedmiotNR = " + przedmiotNR + " AND lekcja.klasaNR = " + klasaNR + ";";
            wykonajZapytanie(ERodzajZapytania.pobierz);
            wylaczEdycje = false;
        }
        /// <summary>
        /// dodaje lekcje do bazy danych
        /// </summary>
        public override void dodajDoBazy()
        {
            SQLite.Zapytanie = "INSERT INTO lekcja(klasaNR, przedmiotNR, dataNR) VALUES(@klasaNR,@przedmiotNR,@dataNR);";
            SQLite.dodajParametr("klasaNR", this.klasaNR);
            SQLite.dodajParametr("przedmiotNR", this.przedmiotNR);
            SQLite.dodajParametr("dataNR", this.dataNR);
            wykonajZapytanie(ERodzajZapytania.wyslij);
            // jak juz jest dodana klasa to konstruktor zwroci l.lekcjaID != -1, wtedy ta wartosc jest przypisana do obiektu
            lekcja l = new lekcja(dataKolumny, this.przedmiotNR, this.klasaNR);
            this.lekcjaID = l.lekcjaID;
            SQLite.zamknijPolaczenie();
        }
        /// <summary>
        /// lekcji sie nie aktualizuje!
        /// </summary>
        /// <param name="elementy"></param>
        public override void aktualizuj(params string[] elementy)
        {
        }

        static public void pobierzWszystkich(int klasaNR, int przedmiotNR)
        {
            cSQLite SQLite = new cSQLite();
            //SQLite.Zapytanie = "SELECT klasaNR, przedmiotNR, 
        }
        protected override void wykonajZapytanie(ERodzajZapytania rodzaj)
        {
            if (rodzaj == ERodzajZapytania.wyslij)
            {
                SQLite.wykonajZapytanie(rodzaj);
            }
            else
            {
                SQLite.otworzPolaczenie();
                SQLite.DataReader = SQLite.command.ExecuteReader();
                while (SQLite.DataReader.Read())
                {
                    lekcjaID = Convert.ToInt32(SQLite.DataReader["lekcjaID"].ToString());
                    klasaNR = Convert.ToInt32(SQLite.DataReader["klasaNR"].ToString());
                    przedmiotNR = Convert.ToInt32(SQLite.DataReader["przedmiotNR"].ToString());
                    dataNR = Convert.ToInt32(SQLite.DataReader["dataNR"].ToString());
                }
                SQLite.zamknijPolaczenie();
            }
        }
        /// <summary>
        /// nie mozna usunac oceny, mozna ja jedynie wykasowac (string.empty) lub ustawic na 0
        /// </summary>
        /// <returns></returns>
        public override bool usun()
        {
            throw new NotImplementedException();
        }
    }
}
