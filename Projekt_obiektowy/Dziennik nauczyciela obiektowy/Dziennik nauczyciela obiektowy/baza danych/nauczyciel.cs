using Dziennik_nauczyciela_obiektowy.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Dziennik_nauczyciela_obiektowy
{
    public class nauczyciel : bazadanych
    {
        private bool czyNowy;
        private string email        = string.Empty;
        private string email_haslo  = string.Empty;
        private string haslo        = string.Empty;
        private string imie         = string.Empty;
        private string login;
        private int nauczycielID    = -1;
        private string nazwisko     = string.Empty;
        private int zalogowany_mail = 0;
        /// <summary>
        /// tworzy obiekt nauczyciel
        /// </summary>
        public nauczyciel()
        {
            SQLite = new cSQLite();
            wylaczEdycje = false;
        }
        /// <summary>
        /// dodaje nauczyciela do bazy danych
        /// </summary>
        public override void dodajDoBazy()
        {
            //SQLite.Zapytanie = "INSERT INTO nauczyciel (login, haslo, imie, nazwisko, Email, email_haslo, zalogowany_mail) VALUES (" + login + ", " + haslo + ", " + imie + ", " + nazwisko + ", " + Email + ", " + email_haslo + ", " + zalogowany_mail + ");";
            SQLite.Zapytanie = "INSERT INTO nauczyciel (login, haslo, imie, nazwisko, Email, email_haslo, zalogowany_mail) VALUES (@login, @haslo, @imie, @nazwisko, @Email, @email_haslo, @zalogowany_mail);";
            SQLite.dodajParametr("login", login);
            SQLite.dodajParametr("haslo", haslo);
            SQLite.dodajParametr("imie", imie);
            SQLite.dodajParametr("nazwisko", nazwisko);
            SQLite.dodajParametr("Email", email);
            SQLite.dodajParametr("email_haslo", email_haslo);
            SQLite.dodajParametr("zalogowany_mail", zalogowany_mail);
            //SQLite.sqliteCommand.Parameters.AddWithValue("zalogowany_mail", nowyNauczyciel.zalogowany_mail);
            wykonajZapytanie(ERodzajZapytania.wyslij);
            wylaczEdycje = false;
        }

        /// <summary>
        /// tworzy obiekt na podstawie ID
        /// </summary>
        /// <param name="NauczycielID">pobiera dane po ID i ustawia do struktury nauczyciela</param>
        public nauczyciel(int nauczycielID)
        {
            this.nauczycielID = nauczycielID;
            SQLite = new cSQLite();
            SQLite.Zapytanie = "SELECT * FROM nauczyciel WHERE nauczycielID =" + nauczycielID + ";";
            wykonajZapytanie(ERodzajZapytania.pobierz);
            sprawdzCzyNowy();
            wylaczEdycje = false;
        }

        public string Email
        {
            get
            {
                return email;
            }
            set
            {
                if (walidacjaMaila(value + @"@gmail.com") == true)
                {
                    email = value;
                    if (!wylaczEdycje) aktualizuj("email");
                }
            }
        }
        public string Haslo
        {
            get
            {
                return haslo;
            }
            set
            {
                haslo = value;
                if (!wylaczEdycje) aktualizuj("haslo");
            }
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
        public string Login
        {
            get
            {
                return login;
            }
            set
            {
                login = value.ToString();
                if (!wylaczEdycje) aktualizuj("login");
            }
        }
        public int NauczycielID
        {
            get
            {
                return nauczycielID;
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

        /// <summary>
        /// pobiera wszystkich nauczycieli
        /// </summary>
        public static List<nauczyciel> pobierzWszystkich()
        {
            List<nauczyciel> listaNauczycieli = new List<nauczyciel>();
            cSQLite SQLite = new cSQLite();
            SQLite.Zapytanie = "SELECT * FROM nauczyciel;";
            SQLite.DataReader = SQLite.command.ExecuteReader();
            while (SQLite.DataReader.Read())
            {
                int nauczycielID = Convert.ToInt32(SQLite.DataReader["nauczycielID"].ToString());
                nauczyciel n = new nauczyciel(nauczycielID);
                listaNauczycieli.Add(n);
            }
            return listaNauczycieli;
        }
        /// <summary>
        /// sprawdza czy Imie i Nazwisko nie jest puste
        /// </summary>
        private void sprawdzCzyNowy()
        {
            czyNowy = (imie.Length != 0) && (nazwisko.Length != 0);
        }

        /// <summary>
        /// usuwa nauczyciela z bazy danych
        /// </summary>
        public override bool usun()
        {
            if (podajHaslo() == false)
            {
                return false;
            }
                SQLite.Zapytanie = "DELETE FROM nauczyciel WHERE nauczycielID =" + nauczycielID + ";";
                SQLite.wykonajZapytanie(ERodzajZapytania.wyslij);
            return true;
        }

        /// <summary>
        /// wyslij : this.wykonajZapytanie = SQLite.wykonajZapytanie;
        /// pobierz: ustawia DataReader oraz ustawia wszystkie elementy obiektu nauczyciel
        /// </summary>
        /// <param name="rodzaj">typ zapytania: wyslij, pobierz</param>
        protected override void wykonajZapytanie(ERodzajZapytania rodzaj)
        {
            if (rodzaj == ERodzajZapytania.wyslij)
            {
                SQLite.wykonajZapytanie(rodzaj);
                return;
            }
           
                SQLite.otworzPolaczenie();
                SQLite.DataReader = SQLite.command.ExecuteReader();
                while (SQLite.DataReader.Read())
                {
                    nauczycielID = Convert.ToInt32(SQLite.DataReader["nauczycielID"].ToString());
                    login = SQLite.DataReader["login"].ToString();
                    haslo = SQLite.DataReader["haslo"].ToString();
                    imie = SQLite.DataReader["imie"].ToString();
                    nazwisko = SQLite.DataReader["nazwisko"].ToString();
                    email = SQLite.DataReader["Email"].ToString();
                    email_haslo = SQLite.DataReader["email_haslo"].ToString();
                    zalogowany_mail = Convert.ToInt32(SQLite.DataReader["zalogowany_mail"].ToString());
                }
                SQLite.zamknijPolaczenie();
           
        }

        /// <summary>
        /// aktualizuje dane w bazie danych
        /// </summary>
        /// <param name="elementy">elementy z bazy danych, * = wszystkie</param>
        public override void aktualizuj(params string[] elementy)
        {
            if (wylaczEdycje == true) throw new Exception("wlacz pierw mozliwosc edycji!");
            if ((elementy.Length == 1) && (elementy[0] == "*"))
            {
                SQLite.Zapytanie = "UPDATE nauczyciel SET login = '" + login + "' WHERE nauczycielID = " + nauczycielID + ";"; SQLite.wykonajZapytanie(ERodzajZapytania.wyslij);
                SQLite.Zapytanie = "UPDATE nauczyciel SET haslo = '" + haslo + "' WHERE nauczycielID = " + nauczycielID + ";"; SQLite.wykonajZapytanie(ERodzajZapytania.wyslij);
                SQLite.Zapytanie = "UPDATE nauczyciel SET imie = '" + imie + "' WHERE nauczycielID = " + nauczycielID + ";"; SQLite.wykonajZapytanie(ERodzajZapytania.wyslij);
                SQLite.Zapytanie = "UPDATE nauczyciel SET nazwisko = '" + nazwisko + "' WHERE nauczycielID = " + nauczycielID + ";"; SQLite.wykonajZapytanie(ERodzajZapytania.wyslij);
                SQLite.Zapytanie = "UPDATE nauczyciel SET Email = '" + email + "' WHERE nauczycielID = " + nauczycielID + ";"; SQLite.wykonajZapytanie(ERodzajZapytania.wyslij);
                SQLite.Zapytanie = "UPDATE nauczyciel SET email_haslo = '" + email_haslo + "' WHERE nauczycielID = " + nauczycielID + ";"; SQLite.wykonajZapytanie(ERodzajZapytania.wyslij);
                SQLite.Zapytanie = "UPDATE nauczyciel SET zalogowany_mail = '" + zalogowany_mail + "' WHERE nauczycielID = " + nauczycielID + ";"; SQLite.wykonajZapytanie(ERodzajZapytania.wyslij);
                return;
            }
            foreach (string element in elementy)
            {
                switch (element)
                {
                    case "login":
                        SQLite.Zapytanie = "UPDATE nauczyciel SET login = '" + login + "' WHERE nauczycielID = " + nauczycielID + ";";
                        try
                        {
                            SQLite.wykonajZapytanie(ERodzajZapytania.wyslij);
                        }
                        catch (Exception)
                        {
                            MessageBox.Show(string.Empty);
                        }
                        break;
                    case "haslo": SQLite.Zapytanie = "UPDATE nauczyciel SET haslo = '" + haslo + "' WHERE nauczycielID = " + nauczycielID + ";"; SQLite.wykonajZapytanie(ERodzajZapytania.wyslij); break;
                    case "imie": SQLite.Zapytanie = "UPDATE nauczyciel SET imie = '" + imie + "' WHERE nauczycielID = " + nauczycielID + ";"; SQLite.wykonajZapytanie(ERodzajZapytania.wyslij); break;
                    case "nazwisko": SQLite.Zapytanie = "UPDATE nauczyciel SET nazwisko = '" + nazwisko + "' WHERE nauczycielID = " + nauczycielID + ";"; SQLite.wykonajZapytanie(ERodzajZapytania.wyslij); break;
                    case "email":
                        if (walidacjaMaila(email) == false) break;
                        SQLite.Zapytanie = "UPDATE nauczyciel SET email = '" + email + "' WHERE nauczycielID = " + nauczycielID + ";"; SQLite.wykonajZapytanie(ERodzajZapytania.wyslij); break;
                    case "email_haslo": SQLite.Zapytanie = "UPDATE nauczyciel SET email_haslo = '" + email_haslo + "' WHERE nauczycielID = " + nauczycielID + ";"; SQLite.wykonajZapytanie(ERodzajZapytania.wyslij); break;
                    case "zalogowany_mail": SQLite.Zapytanie = "UPDATE nauczyciel SET zalogowany_mail = " + ZalogowanyMail + " WHERE nauczycielID = " + nauczycielID + ";"; SQLite.wykonajZapytanie(ERodzajZapytania.wyslij); break;
                    default: throw new Exception("niepoprawny parametr do aktualizacji danych");
                }
            }
            
        }

        public bool podajHaslo()
        {
            fWalidacjaHasla walidacjaHasla = new fWalidacjaHasla(haslo);
            walidacjaHasla.ShowDialog();
            if (walidacjaHasla.czyPoprawne == false)
            {
                MessageBox.Show("zle haslo!");
            }
            return walidacjaHasla.czyPoprawne;
        }
        /// <summary>
        /// loguje sie na wybranego nauczyciela (przechodzi do widoku klas)
        /// </summary>
        /// <param name="klasaNR">podaj formę, ktora ma zostać zamknieta(this = najczesciej)</param>
        public void zaloguj(Form f)
        {
            if (podajHaslo() == true)
            {
                fListaKlas listaKlas = new fListaKlas(this);
                f.Hide();
                listaKlas.ShowDialog();
                f.Close();
            }
        }

        public int ZalogowanyMail
        {
            get
            {
                return zalogowany_mail;
            }
            set
            {
                zalogowany_mail = value;
                if (!wylaczEdycje)
                {
                    aktualizuj("zalogowany_mail");
                }
            }
        }

        public string Email_haslo
        {
            get
            {
                return email_haslo;
            }
            set
            {
                email_haslo = value;
                if (!wylaczEdycje) aktualizuj("email_haslo");
            }
        }

        /// <summary>
        /// wyloguje z maila
        /// </summary>
        public void wylogujMail(BackgroundWorker bw)
        {
            wylaczEdycje = false;
            ZalogowanyMail = 0;
            if (!bw.IsBusy)
            {
                bw.RunWorkerAsync();
            }
        }
        /// <summary>
        /// wyloguje z maila
        /// </summary>
        public void zalogujMail(BackgroundWorker bw)
        {
            wylaczEdycje = false;
            ZalogowanyMail = 1;
            if (!bw.IsBusy)
            {
                bw.RunWorkerAsync();
            }
        }

    }
}
