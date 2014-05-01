using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SQLite;
using System.Data;
namespace Dziennik_nauczyciela
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //TODO 1 na jutro: zrobic dodawanie ocen (INDYWIDUALNE), wczytac liste uczniow do INDYWIDUALNE
            //TODO: przy otwieraniu okna uczen / przedmiot -> nalezy wyczyscic wszystkie dgv, albo inaczej nad tym pomyslec
            //TODO: dodac gospodarza
            //TODO: korzystajac z: http://stackoverflow.com/questions/1606477/how-to-make-all-sundays-red-on-month-calendar-in-c zrobic kolorowanie tych krotek, ktore juz sa w bazie danych :-)
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new fStart());
            //przejscie od razu do konkretnego uzytkownika
            /*
            try
            {
                cSQLite SQLite = new cSQLite();
                if (SQLite.sqliteConnection.State == ConnectionState.Closed) SQLite.sqliteConnection.Open();
                SQLite.sqliteCommand = SQLite.sqliteConnection.CreateCommand();
                SQLite.sqliteCommand.CommandText = "select * from nauczyciel where nauczycielID=@wybranyUzytkownikID";
                SQLite.sqliteCommand.Parameters.AddWithValue("wybranyUzytkownikID", 1);
                SQLite.sqliteCommand.ExecuteNonQuery();
                SQLiteDataReader dataReader = SQLite.sqliteCommand.ExecuteReader();
                BAZADANYCH.nauczyciel logujacyUzytkownik = null;
                while (dataReader.Read())
                {
                    logujacyUzytkownik = new BAZADANYCH.nauczyciel
                    {
                        nauczycielID = Convert.ToInt32(dataReader["nauczycielID"]),
                        imie = dataReader["imie"].ToString(),
                        nazwisko = dataReader["nazwisko"].ToString(),
                        email = dataReader["email"].ToString(),
                        login = dataReader["login"].ToString(),
                        haslo = dataReader["haslo"].ToString(),
                        email_haslo = dataReader["email_haslo"].ToString(),
                        zalogowany_mail = (Convert.ToInt32(dataReader["zalogowany_mail"]))
                    };
                }
                Application.Run(new fListaKlas(logujacyUzytkownik));
                SQLite.sqliteConnection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
           */ 
        }
    }
}
