using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Data.SQLite;

namespace Dziennik_nauczyciela
{
    class cSQLite
    {
        private static string zrodloDanych = "Data Source=bazadanych.db";
        private static string nazwaPliku = "bazadanych.db";

        public SQLiteConnection sqliteConnection = null;
        public SQLiteCommand sqliteCommand = null;

        public cSQLite()
        {
            sqliteConnection = new SQLiteConnection(zrodloDanych);
            sqliteCommand = new SQLiteCommand(sqliteConnection);

        }
        public void Logger(String lines)
        {

            System.IO.StreamWriter file = new System.IO.StreamWriter("logs.txt", true);
            DateTime data = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day,DateTime.Now.Hour, DateTime.Now.Minute,0);
            file.WriteLine("[" + lines + "]\t" +  + data.Hour + ":" + data.Minute + " " + data.ToString("dd-MM-yyy"));

            file.Close();
        }

        public void tworzenieBazyDanych()
        {
            if (!File.Exists(cSQLite.nazwaPliku))
            {
                SQLiteConnection.CreateFile(cSQLite.nazwaPliku);

                sqliteConnection.Open();

                
                StringBuilder sql = new StringBuilder();

                sql.AppendLine("CREATE TABLE IF NOT EXISTS nauczyciel([nauczycielID] INTEGER PRIMARY KEY AUTOINCREMENT,");
                sql.AppendLine("[login] VARCHAR(25) NOT NULL UNIQUE,");
                sql.AppendLine("[haslo] VARCHAR(25) NOT NULL,");
                sql.AppendLine("[imie] VARCHAR(25),");
                sql.AppendLine("[nazwisko] VARCHAR(25),");
                sql.AppendLine("[email_haslo] VARCHAR(25),");
                sql.AppendLine("[zalogowany_mail] INTEGER,");
                sql.AppendLine("[email] VARCHAR(25));");
                
                sql.AppendLine("CREATE TABLE IF NOT EXISTS klasa([klasaID] INTEGER PRIMARY KEY AUTOINCREMENT,");
                sql.AppendLine("[nazwa] VARCHAR(25) NOT NULL,");
                sql.AppendLine("[rocznik] VARCHAR(25),");
                sql.AppendLine("[nauczycielNR] INT,");
                sql.AppendLine("[gospodarzNR] INT,");
                sql.AppendLine("FOREIGN KEY (nauczycielNR) REFERENCES nauczyciel(nauczycielID),");
                sql.AppendLine("FOREIGN KEY (gospodarzNR) REFERENCES uczen(uczenID));");


                /*
                 CREATE TABLE IF NOT EXISTS przedmiot (
                    przedmiotID INTEGER PRIMARY KEY NOT NULL,
                    nauczycielNR INTEGER,
                    nazwa VARCHAR(30),
                    FOREIGN KEY (nauczycielNR) references nauczyciel
                 );
                */
                sqliteCommand = sqliteConnection.CreateCommand();
                sqliteCommand.CommandText = sql.ToString();
                sqliteCommand.ExecuteNonQuery();

                sql = new StringBuilder();
                sql.AppendLine("CREATE TABLE IF NOT EXISTS przedmiot([przedmiotID] INTEGER PRIMARY KEY AUTOINCREMENT, ");
                sql.AppendLine("[klasaNR] INT, ");
                sql.AppendLine("[nazwa] VARCHAR(25) NOT NULL, ");
                sql.AppendLine("UNIQUE (klasaNR, nazwa), ");
                sql.AppendLine("FOREIGN KEY (klasaNR) REFERENCES klasa(klasaID));");


                sqliteCommand = sqliteConnection.CreateCommand();
                sqliteCommand.CommandText = sql.ToString();
                sqliteCommand.ExecuteNonQuery();

                Logger("stworzono baze danych.");
                try
                {
                    sqliteCommand.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Logger(ex.Message);
                }
                sqliteConnection.Close();
            }
        }



    }
}
