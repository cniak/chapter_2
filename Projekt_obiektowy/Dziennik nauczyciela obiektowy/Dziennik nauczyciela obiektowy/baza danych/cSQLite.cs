using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SQLite;
using System.IO;
using System.Windows.Forms;

namespace Dziennik_nauczyciela_obiektowy
{
    public class cSQLite
    {
        private string zrodloDanych = "Data Source=bazadanych.db";
        private string nazwaPliku   = "bazadanych.db";
        private string zapytanie = string.Empty;
        

        private SQLiteDataReader dataReader;
        private SQLiteConnection connection;
        public SQLiteCommand command;
    
        public cSQLite()
        {
            connection  = new SQLiteConnection(zrodloDanych);
            command     = new SQLiteCommand(connection);
        }

        public SQLiteDataReader DataReader
        {
            get
            {
                return dataReader;
            }
            set
            {
                dataReader = value;
            }
        }

        /// <summary>
        /// command.commandText = zapytanie
        /// </summary>
        public string Zapytanie
        {
            get
            {
                return zapytanie;
            }
            set
            {
                otworzPolaczenie();
                zapytanie = value.ToString();
                command = connection.CreateCommand();
                command.CommandText = zapytanie;
                //zamknijPolaczenie();
            }
        }

        public void otworzPolaczenie()
        {
            if (connection.State == ConnectionState.Closed) connection.Open();
        }
        public void zamknijPolaczenie()
        {
            connection.Close();
        }
        /// <summary>
        /// chyba to przeniose do nauczyciela
        /// </summary>
        public void wykonajZapytanie(rodzajZapytania rodzaj)
        {
            if (rodzaj == rodzajZapytania.wyslij)
            {
                otworzPolaczenie();
                    command.ExecuteNonQuery();
                zamknijPolaczenie();    
            }
            else
            {
                throw new System.NotImplementedException();
            }
        }

        public void tworzBaze()
        {
            if (File.Exists(nazwaPliku)) return;
            SQLiteConnection.CreateFile(nazwaPliku);
            otworzPolaczenie();
            StringBuilder sql = new StringBuilder();
            //struktura: nauczyciel
            sql.AppendLine("CREATE TABLE IF NOT EXISTS nauczyciel([NauczycielID] INTEGER PRIMARY KEY AUTOINCREMENT,");
            sql.AppendLine("[Login] VARCHAR(25) NOT NULL UNIQUE,");
            sql.AppendLine("[Haslo] VARCHAR(25) NOT NULL,");
            sql.AppendLine("[Imie] VARCHAR(25),");
            sql.AppendLine("[Nazwisko] VARCHAR(25),");
            sql.AppendLine("[email_haslo] VARCHAR(25),");
            sql.AppendLine("[zalogowany_mail] INTEGER,");
            sql.AppendLine("[Email] VARCHAR(25));");

            //struktura: klasa
            sql.AppendLine("CREATE TABLE IF NOT EXISTS klasa([klasaID] INTEGER PRIMARY KEY AUTOINCREMENT,");
            sql.AppendLine("[nazwa] VARCHAR(25) NOT NULL,");
            sql.AppendLine("[rocznik] VARCHAR(25),");
            sql.AppendLine("[nauczycielNR] INT,");
            sql.AppendLine("[gospodarzNR] INT,");
            sql.AppendLine("FOREIGN KEY (nauczycielNR) REFERENCES nauczyciel(NauczycielID),");
            sql.AppendLine("FOREIGN KEY (gospodarzNR) REFERENCES uczen(UczenID));");

            //struktura: przedmiot
            sql.AppendLine("CREATE TABLE IF NOT EXISTS przedmiot([przedmiotID] INTEGER PRIMARY KEY AUTOINCREMENT, ");
            sql.AppendLine("[klasaNR] INT, ");
            sql.AppendLine("[nazwa] VARCHAR(25) NOT NULL, ");
            sql.AppendLine("UNIQUE (klasaNR, nazwa), ");
            sql.AppendLine("FOREIGN KEY (klasaNR) REFERENCES klasa(ID));");

            //struktura: uczen
            sql.AppendLine("CREATE TABLE IF NOT EXISTS uczen([UczenID] INTEGER PRIMARY KEY AUTOINCREMENT, ");
            sql.AppendLine("[klasaNR] INT NOT NULL, ");
            sql.AppendLine("[Imie] VARCHAR(25) NOT NULL, ");
            sql.AppendLine("[Nazwisko] VARCHAR(25) NOT NULL, ");
            sql.AppendLine("[Pesel] VARCHAR(25) NOT NULL, ");
            sql.AppendLine("[Email] VARCHAR(25), ");
            sql.AppendLine("[telefon_ucznia] VARCHAR(25) NOT NULL, ");
            sql.AppendLine("[telefon_rodzica] VARCHAR(25) NOT NULL, ");
            sql.AppendLine("UNIQUE (klasaNR, Imie, Nazwisko), ");
            sql.AppendLine("FOREIGN KEY (klasaNR) REFERENCES klasa(ID));");

            //struktura: data
            sql.AppendLine("CREATE TABLE IF NOT EXISTS data([dataID] INTEGER PRIMARY KEY AUTOINCREMENT, ");
            sql.AppendLine("[klasaNR] INT NOT NULL, ");
            sql.AppendLine("[dzien] datetime NOT NULL, ");
            sql.AppendLine("UNIQUE(dzien, klasaNR), ");
            sql.AppendLine("FOREIGN KEY (klasaNR) REFERENCES klasa(ID));");

            //struktura: lekcja
            sql.AppendLine("CREATE TABLE IF NOT EXISTS lekcja([lekcjaID] INTEGER PRIMARY KEY AUTOINCREMENT, ");
            sql.AppendLine("[klasaNR] INT NOT NULL, ");
            sql.AppendLine("[przedmiotNR] INT NOT NULL, ");
            sql.AppendLine("[dataNR] INT NOT NULL, ");
            sql.AppendLine("FOREIGN KEY (klasaNR) REFERENCES klasa(ID), ");
            sql.AppendLine("FOREIGN KEY (przedmiotNR) REFERENCES przedmiot(przedmiotID), ");
            sql.AppendLine("FOREIGN KEY (dataNR) REFERENCES data(dataID));");

            //struktura: uczen_na_lekcji
            sql.AppendLine("CREATE TABLE IF NOT EXISTS uczen_na_lekcji([uczen_na_lekcjiID] INTEGER PRIMARY KEY AUTOINCREMENT, ");
            sql.AppendLine("[UczenNR] INT NOT NULL, ");
            sql.AppendLine("[lekcjaNR] INT NOT NULL, ");
            sql.AppendLine("[obecnosc] INT, ");
            sql.AppendLine("[ocena] INT CHECK (ocena >=0 AND ocena <= 5), ");
            sql.AppendLine("UNIQUE(UczenNR,lekcjaNR), ");
            sql.AppendLine("FOREIGN KEY (UczenNR) REFERENCES uczen(UczenID), ");
            sql.AppendLine("FOREIGN KEY (lekcjaNR) REFERENCES lekcja(lekcjaID));");

            Zapytanie = sql.ToString();
            wykonajZapytanie(rodzajZapytania.wyslij);
        }
        
        /// <summary>
        /// dodajeParametr do zapytania
        /// </summary>
        public void dodajParametr(string nazwa, object wartosc)
        {
            string valueString = wartosc as string;
            if (valueString != null)
            {
                command.Parameters.AddWithValue(nazwa, wartosc);
                return;
            }
            
            
            
            Nullable<int> valueInt = Convert.ToInt32(wartosc.ToString());
            if (valueInt != null) command.Parameters.AddWithValue(nazwa, valueInt);
            else throw new Exception("zly parametr");
             
        }
    }
}