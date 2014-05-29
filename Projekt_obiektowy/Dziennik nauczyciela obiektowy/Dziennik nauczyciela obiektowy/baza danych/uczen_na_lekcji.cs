using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dziennik_nauczyciela_obiektowy
{
    public class uczen_na_lekcji : bazadanych
    {
        private int uczen_na_lekcjiID = -1;
        private int uczenNR;
        private int lekcjaNR;
        private int obecnosc;
        private int ocena;

        public uczen_na_lekcji()
        {
            SQLite = new cSQLite();
            wylaczEdycje = false;
        }
        /// <summary>
        /// tworzy obiekt na podstawie podanych danych w properties
        /// </summary>
        public uczen_na_lekcji(int uczenNR, int lekcjaNR)
        {
            this.uczenNR = uczenNR;
            this.lekcjaNR = lekcjaNR;
            SQLite = new cSQLite();
            SQLite.Zapytanie = "SELECT * FROM uczen_na_lekcji WHERE uczenNR = " + uczenNR + " AND lekcjaNR = " + lekcjaNR + ";";
            wykonajZapytanie(rodzajZapytania.pobierz);
            wylaczEdycje = false;
        }

        /// <summary>
        /// zwraca obiekt, ktory ma ocene i czy byl obecny uczen na lekcji // obiekt na podstawie podanego ID
        /// </summary>
        public uczen_na_lekcji(int uczen_na_lekcjiID)
        {
            this.uczen_na_lekcjiID = uczen_na_lekcjiID;
            SQLite = new cSQLite();
            SQLite.Zapytanie = "SELECT * FROM uczen_na_lekcji WHERE uczen_na_lekcjiID = " + uczen_na_lekcjiID + ";";
            wykonajZapytanie(rodzajZapytania.pobierz);
            wylaczEdycje = false;
        }

        public int Uczen_na_lekcjiID
        {
            get
            {
                return uczen_na_lekcjiID;
            }
            set
            {
            }
        }

        public int UczenNR
        {
            get
            {
                return uczenNR;
            }
            set
            {
                uczenNR = value;
            }
        }

        public int LekcjaNR
        {
            get
            {
                return lekcjaNR;
            }
            set
            {
                lekcjaNR = value;
            }
        }

        public int Obecnosc
        {
            get
            {
                return obecnosc;
            }
            set
            {
                obecnosc = value;
                if (!wylaczEdycje) aktualizuj("obecnosc");
            }
        }

        public int Ocena
        {
            get
            {
                return ocena;
            }
            set
            {
                ocena = value;
                if (!wylaczEdycje){
                    aktualizuj("ocena");
                }
            }
        }

        public override void dodajDoBazy()
        {
            SQLite.Zapytanie = "INSERT INTO uczen_na_lekcji(uczenNR, lekcjaNR, obecnosc) VALUES(@uczen,@lekcja,@obecnosc);";
            //SQLite.Zapytanie = "INSERT INTO uczen_na_lekcji(uczenNR, lekcjaNR, obecnosc) VALUES(" + uczenNR + ", " +lekcjaNR + ", " + obecnosc + ");";
            SQLite.dodajParametr("uczen", uczenNR);
            SQLite.dodajParametr("lekcja", lekcjaNR);
            SQLite.dodajParametr("obecnosc", obecnosc);
            wykonajZapytanie(rodzajZapytania.wyslij);
            // jak juz jest dany uczen to mozna mu przypisac ID;
            uczen_na_lekcji unl = new uczen_na_lekcji(this.uczenNR, this.lekcjaNR);
            this.uczen_na_lekcjiID = unl.Uczen_na_lekcjiID;
        }

        public override void aktualizuj(params string[] elementy)
        {
            if (wylaczEdycje == true) throw new Exception("wlacz pierw edycje!");
            if ((elementy.Length == 1) && (elementy[0] == "*"))
            {
                SQLite.Zapytanie = "UPDATE uczen_na_lekcji SET ocena = '" + ocena + "' WHERE uczen_na_lekcjiID= " + uczen_na_lekcjiID + ";"; SQLite.wykonajZapytanie(rodzajZapytania.wyslij);
                SQLite.Zapytanie = "UPDATE uczen_na_lekcji SET obecnosc = '" + obecnosc + "' WHERE uczen_na_lekcjiID= " + uczen_na_lekcjiID + ";"; SQLite.wykonajZapytanie(rodzajZapytania.wyslij);
                return;
            }
            foreach (string element in elementy)
            {
                switch(element){
                    case "ocena": SQLite.Zapytanie = "UPDATE uczen_na_lekcji SET ocena = '" + ocena + "' WHERE uczen_na_lekcjiID= " + uczen_na_lekcjiID + ";"; SQLite.wykonajZapytanie(rodzajZapytania.wyslij); break;
                    case "obecnosc": SQLite.Zapytanie = "UPDATE uczen_na_lekcji SET obecnosc = '" + obecnosc + "' WHERE uczen_na_lekcjiID= " + uczen_na_lekcjiID + ";"; SQLite.wykonajZapytanie(rodzajZapytania.wyslij); break;
                    default: throw new Exception("niepoprawny parametr do aktualizacji danych");
                }
            }
        }

        protected override void wykonajZapytanie(rodzajZapytania rodzaj)
        {
            if (rodzaj == rodzajZapytania.wyslij)
            {
                SQLite.wykonajZapytanie(rodzaj);
            }
            else
            {
                SQLite.otworzPolaczenie();
                SQLite.DataReader = SQLite.command.ExecuteReader();
                while (SQLite.DataReader.Read())
                {
                    uczen_na_lekcjiID = Convert.ToInt32(SQLite.DataReader["uczen_na_lekcjiID"].ToString());
                    uczenNR = Convert.ToInt32(SQLite.DataReader["uczenNR"].ToString());
                    lekcjaNR = Convert.ToInt32(SQLite.DataReader["lekcjaNR"].ToString());
                    obecnosc = Convert.ToInt32(SQLite.DataReader["obecnosc"].ToString());
                    //System.Windows.Forms.MessageBox.Show(SQLite.DataReader["ocena"].ToString().Length.ToString());
                    if(SQLite.DataReader["ocena"].ToString().Length != 0) ocena = Convert.ToInt32(SQLite.DataReader["ocena"].ToString());
                }
            }
        }

        public override bool usun()
        {
            // nie da sie usuwac, jedynie mozna napisac ocene 0
            throw new NotImplementedException();
        }

        public static List<uczen_na_lekcji> pobierzWszystkich(int klasaNR)
        {
            List<uczen_na_lekcji> listaUnl = new List<uczen_na_lekcji>();
            cSQLite SQLite = new cSQLite();
            SQLite.Zapytanie = "SELECT * FROM uczen_na_lekcji WHERE uczenNR in (SELECT uczenNR FROM uczen WHERE klasaNR = " + klasaNR + ");";
            SQLite.DataReader = SQLite.command.ExecuteReader();
            while (SQLite.DataReader.Read())
            {
                int uczen_na_lekcjiID = Convert.ToInt32(SQLite.DataReader["uczen_na_lekcjiID"].ToString());
                uczen_na_lekcji unl = new uczen_na_lekcji(uczen_na_lekcjiID);
                listaUnl.Add(unl);
            }
            return listaUnl;
        }
    }
}
