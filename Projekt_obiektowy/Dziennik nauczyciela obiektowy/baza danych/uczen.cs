using System;
using System.Collections.Generic;

namespace Dziennik_nauczyciela_obiektowy
{
    public class uczen : bazadanych
    {
        private string imie = string.Empty;
        private string nazwisko = string.Empty;
        private int uczenID = -1;
        private int klasaNR = -1;
        private string pesel = string.Empty;
        private string email = string.Empty;
        private string telefon_ucznia = string.Empty;
        private string telefon_rodzica = string.Empty;
        private string uwagi = "brak";

        /// <summary>
        /// tworzy obiekt uczen
        /// </summary>
        public uczen()
        {
            SQLite = new cSQLite();
        }

        public uczen(int uczenID)
        {
            this.uczenID = uczenID;
            SQLite = new cSQLite();
            SQLite.Zapytanie = "SELECT * FROM uczen WHERE uczenID = " + this.uczenID + ";";
            wykonajZapytanie(ERodzajZapytania.pobierz);
            wylaczEdycje = false;
        }
    
        public string Imie
        {
            get
            {
                return imie;
            }
            set
            {
                imie = value;
                if (!wylaczEdycje) aktualizuj("imie");

            }
        }
        public string Nazwisko
        {
            get
            {
                return nazwisko;
            }
            set
            {
                nazwisko = value;
                if (!wylaczEdycje) aktualizuj("nazwisko");
            }
        }
        public string Pesel
        {
            get
            {
                return pesel;
            }
            set
            {
                pesel = value;
               if(!wylaczEdycje) aktualizuj("pesel");
            }
        }
        public string Email
        {
            get
            {
                return email;
            }
            set
            {
                if (this.walidacjaMaila(value) == true)
                {
                    email = value;
                    if (!wylaczEdycje) aktualizuj("email");
                }
            }
        }
        public string Telefon_ucznia
        {
            get
            {
                return telefon_ucznia;
            }
            set
            {
                telefon_ucznia = value;
            }
        }
        public string Telefon_rodzica
        {
            get
            {
                return telefon_rodzica;
            }
            set
            {
                telefon_rodzica = value;
            }
        }
        public int UczenID
        {
            get
            {
                return uczenID;
            }
        }
        public string Uwagi
        {
            get { return uwagi; }
            set {
                uwagi = value;
                if (!wylaczEdycje)
                    if (uwagi.Length == 0) uwagi = "brak";
                    aktualizuj("uwagi");
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

        public override void dodajDoBazy()
        {
            SQLite.Zapytanie = "INSERT INTO uczen (klasaNR, imie, nazwisko, pesel, email, telefon_ucznia, telefon_rodzica, uwagi) " +
                                                  "VALUES(@klasaNR,@imie,@nazwisko,@pesel,@email,@telefon_ucznia,@telefon_rodzica, @uwagi);";
            SQLite.dodajParametr("klasaNR", klasaNR);
            SQLite.dodajParametr("imie", imie);
            SQLite.dodajParametr("nazwisko", nazwisko);
            SQLite.dodajParametr("pesel", pesel);
            SQLite.dodajParametr("email", email);
            SQLite.dodajParametr("telefon_ucznia", telefon_ucznia);
            SQLite.dodajParametr("telefon_rodzica", telefon_rodzica);
            SQLite.dodajParametr("uwagi", uwagi);
            SQLite.wykonajZapytanie(ERodzajZapytania.wyslij);
            wylaczEdycje = false;
        }

        public override void aktualizuj(params string[] elementy)
        {
            if (wylaczEdycje == true) throw new Exception("wlacz pierw edycje!");
            if ((elementy.Length == 1) && (elementy[0] == "*"))
            {
                SQLite.Zapytanie = "UPDATE uczen SET klasaNR = " + klasaNR + " WHERE uczenID= " + uczenID + ";"; SQLite.wykonajZapytanie(ERodzajZapytania.wyslij);
                SQLite.Zapytanie = "UPDATE uczen SET imie = '" + imie + "' WHERE uczenID= " + uczenID + ";"; SQLite.wykonajZapytanie(ERodzajZapytania.wyslij);
                SQLite.Zapytanie = "UPDATE uczen SET nazwisko = '" + nazwisko + "' WHERE uczenID= " + uczenID+ ";"; SQLite.wykonajZapytanie(ERodzajZapytania.wyslij);
                SQLite.Zapytanie = "UPDATE uczen SET pesel = '" + pesel + "' WHERE uczenID= " + uczenID + ";"; SQLite.wykonajZapytanie(ERodzajZapytania.wyslij);
                SQLite.Zapytanie = "UPDATE uczen SET email = '" + email + "' WHERE uczenID= " + uczenID + ";"; SQLite.wykonajZapytanie(ERodzajZapytania.wyslij);
                SQLite.Zapytanie = "UPDATE uczen SET telefon_ucznia = '" + telefon_ucznia + "' WHERE uczenID= " + uczenID + ";"; SQLite.wykonajZapytanie(ERodzajZapytania.wyslij);
                SQLite.Zapytanie = "UPDATE uczen SET telefon_rodzica = '" + telefon_rodzica + "' WHERE uczenID= " + uczenID + ";"; SQLite.wykonajZapytanie(ERodzajZapytania.wyslij);
                SQLite.Zapytanie = "UPDATE uczen SET uwagi = '" + uwagi + "' WHERE uczenID =  " + uczenID + ";"; SQLite.wykonajZapytanie(ERodzajZapytania.wyslij);
                return;
            }
            foreach (string element in elementy)
            {
                switch (element)
                {
                    case "klasaNR":         SQLite.Zapytanie = "UPDATE uczen SET klasaNR = " + klasaNR + " WHERE uczenID= " + uczenID + ";"; SQLite.wykonajZapytanie(ERodzajZapytania.wyslij); break;
                    case "imie":            SQLite.Zapytanie = "UPDATE uczen SET imie = '" + imie + "' WHERE uczenID= " + uczenID + ";"; SQLite.wykonajZapytanie(ERodzajZapytania.wyslij); break;
                    case "nazwisko":        SQLite.Zapytanie = "UPDATE uczen SET nazwisko = '" + nazwisko + "' WHERE uczenID= " + uczenID + ";"; SQLite.wykonajZapytanie(ERodzajZapytania.wyslij); break;
                    case "pesel":           SQLite.Zapytanie = "UPDATE uczen SET pesel = '" + pesel + "' WHERE uczenID= " + uczenID + ";"; SQLite.wykonajZapytanie(ERodzajZapytania.wyslij); break;
                    case "email":           SQLite.Zapytanie = "UPDATE uczen SET email = '" + email + "' WHERE uczenID= " + uczenID + ";"; SQLite.wykonajZapytanie(ERodzajZapytania.wyslij); break;
                    case "telefon_ucznia":  SQLite.Zapytanie = "UPDATE uczen SET telefon_ucznia = '" + telefon_ucznia + "' WHERE uczenID= " + uczenID + ";"; SQLite.wykonajZapytanie(ERodzajZapytania.wyslij); break;
                    case "telefon_rodzica": SQLite.Zapytanie = "UPDATE uczen SET telefon_rodzica = '" + telefon_rodzica + "' WHERE uczenID= " + uczenID + ";"; SQLite.wykonajZapytanie(ERodzajZapytania.wyslij); break;
                    case "uwagi":           SQLite.Zapytanie = "UPDATE uczen SET uwagi = '" + uwagi + "' WHERE uczenID =  " + uczenID + ";"; SQLite.wykonajZapytanie(ERodzajZapytania.wyslij); break;
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
                    uczenID = Convert.ToInt32(SQLite.DataReader["uczenID"].ToString());
                    klasaNR = Convert.ToInt32(SQLite.DataReader["klasaNR"].ToString());
                    imie = SQLite.DataReader["imie"].ToString();
                    nazwisko = SQLite.DataReader["nazwisko"].ToString();
                    pesel = SQLite.DataReader["pesel"].ToString();
                    email = SQLite.DataReader["email"].ToString();
                    telefon_ucznia = SQLite.DataReader["telefon_ucznia"].ToString();
                    telefon_rodzica = SQLite.DataReader["telefon_rodzica"].ToString();
                    uwagi = SQLite.DataReader["uwagi"].ToString();
                }
                SQLite.zamknijPolaczenie();
            }
        }

        public override bool usun()
        {
            SQLite.Zapytanie = "DELETE FROM uczen WHERE uczenID = " + uczenID + ";";
            SQLite.wykonajZapytanie(ERodzajZapytania.wyslij);
            return true;
        }

        public static List<uczen> pobierzWszystkich(int klasaID)
        {
            List<uczen> listaUczniow = new List<uczen>();
            cSQLite SQLite = new cSQLite();
            SQLite.otworzPolaczenie();
            SQLite.Zapytanie = "SELECT uczenID FROM uczen WHERE klasaNR = " + klasaID + ";";
            SQLite.DataReader = SQLite.command.ExecuteReader();
            while (SQLite.DataReader.Read())
            {
                int uczenID = Convert.ToInt32(SQLite.DataReader["uczenID"].ToString());
                uczen u = new uczen(uczenID);
                listaUczniow.Add(u);
            }
            return listaUczniow;
        }
    }
}
