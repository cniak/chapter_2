using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Dziennik_nauczyciela_obiektowy
{
    public class przedmiot : bazadanych
    {
        private string nazwa;
        private int przedmiotID;
        private int klasaNR;

        public przedmiot()
        {
            SQLite = new cSQLite();
            //wylaczEdycje = false;
        }
        public przedmiot(int przedmiotID)
        {
            this.przedmiotID = przedmiotID;
            SQLite = new cSQLite();
            SQLite.Zapytanie = "SELECT * FROM przedmiot WHERE przedmiotID = " + przedmiotID + ";";
            wykonajZapytanie(ERodzajZapytania.pobierz);
            wylaczEdycje = false;
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

        public string Nazwa
        {
            get
            {
                return nazwa;
            }
            set
            {
                nazwa = value;
                if (!wylaczEdycje) aktualizuj("nazwa");
            }
        }

        public int PrzedmiotID
        {
            get
            {
                return przedmiotID;
            }            
        }
    
        public override void dodajDoBazy()
        {
            SQLite.Zapytanie = "INSERT INTO przedmiot (klasaNR, nazwa) VALUES (@klasaNR, @nazwa);";
            SQLite.dodajParametr("klasaNR", klasaNR);
            SQLite.dodajParametr("nazwa", nazwa);
            SQLite.wykonajZapytanie(ERodzajZapytania.wyslij);
            wylaczEdycje = false;
        }

        public override void aktualizuj(params string[] elementy)
        {
            if (wylaczEdycje == true) throw new Exception("wlacz pierw edycje!");
            if ((elementy.Length == 1) && (elementy[0] == "*"))
            {
                SQLite.Zapytanie = "UPDATE przedmiot SET nazwa = '" + nazwa + "' WHERE przedmiotID = " + przedmiotID + ";"; SQLite.wykonajZapytanie(ERodzajZapytania.wyslij);
                return;
            }
            foreach (string element in elementy)
            {
                switch (element)
                {
                    case "nazwa": SQLite.Zapytanie = "UPDATE przedmiot SET nazwa = '" + nazwa + "' WHERE przedmiotID = " + przedmiotID + ";"; SQLite.wykonajZapytanie(ERodzajZapytania.wyslij); break;
                    default: throw new Exception("niepoprawny parametr do aktualizacji danych");
                }
            }
        }

        protected override void wykonajZapytanie(ERodzajZapytania rodzaj)
        {
            if (rodzaj == ERodzajZapytania.wyslij) SQLite.wykonajZapytanie(rodzaj);
            else
            {
                SQLite.otworzPolaczenie();
                SQLite.DataReader = SQLite.command.ExecuteReader();
                while (SQLite.DataReader.Read())
                {
                    przedmiotID = Convert.ToInt32(SQLite.DataReader["przedmiotID"].ToString());
                    klasaNR = Convert.ToInt32(SQLite.DataReader["klasaNR"].ToString());
                    nazwa = SQLite.DataReader["nazwa"].ToString();
                }
                SQLite.zamknijPolaczenie();
            }
        }

        public override bool usun()
        {
            SQLite.Zapytanie = "DELETE FROM przedmiot WHERE przedmiotID = " + przedmiotID + ";";
            SQLite.wykonajZapytanie(ERodzajZapytania.wyslij);
            return true;
        }

        public static List<przedmiot> pobierzWszystkich(int klasaID)
        {
            List<przedmiot> listaPrzedmiotow = new List<przedmiot>();
            cSQLite SQLite = new cSQLite();
            SQLite.Zapytanie = "SELECT przedmiotID FROM przedmiot WHERE klasaNR = " + klasaID + ";";
            SQLite.DataReader = SQLite.command.ExecuteReader();
            while (SQLite.DataReader.Read())
            {
                int przedmiotID = Convert.ToInt32(SQLite.DataReader["przedmiotID"].ToString());
                przedmiot p = new przedmiot(przedmiotID);
                listaPrzedmiotow.Add(p);
            }
            SQLite.zamknijPolaczenie();
            return listaPrzedmiotow;
        }
    }
}
