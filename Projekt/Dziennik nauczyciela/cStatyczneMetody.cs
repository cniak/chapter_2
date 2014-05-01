using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SQLite;

namespace Dziennik_nauczyciela
{
    class cStatyczneMetody
    {
        public static void ustawZawszeWidoczneKolumny(DataGridViewBand band)
        {
            band.Frozen = true;
            DataGridViewCellStyle style = new DataGridViewCellStyle();
            style.BackColor = Color.WhiteSmoke;
            band.DefaultCellStyle = style;
        }

        public static Dictionary<int,string> stworzListePrzedmiotow(int klasaID)
        {
            
            //to add
            Dictionary<int, string> listaPrzedmiotow = new Dictionary<int, string>();
            cSQLite SQLite = new cSQLite();
            if(SQLite.sqliteConnection.State == ConnectionState.Closed) SQLite.sqliteConnection.Open();
            SQLite.sqliteCommand = SQLite.sqliteConnection.CreateCommand();
            SQLite.sqliteCommand.CommandText = "SELECT przedmiotID, nazwa FROM przedmiot WHERE klasaNR = " + klasaID + ";";
            SQLiteDataReader dataReader = SQLite.sqliteCommand.ExecuteReader();
            while (dataReader.Read())
            {
                listaPrzedmiotow.Add(Convert.ToInt32(dataReader["przedmiotID"].ToString()), dataReader["nazwa"].ToString());
            }
            //cb.Items.Add(new KeyValuePair<int,string>(2, "This text is displayed"));
            //to access the 'tag' property 
            SQLite.sqliteConnection.Close();
            /*
            string tag = ((KeyValuePair<string, string>)cb.SelectedItem).Key;
            MessageBox.Show(tag);
            */
            return listaPrzedmiotow;
        }
        public static Dictionary<int, string> stworzListeUczniow(int klasaID)
        {
            Dictionary<int, string> listaUczniow = new Dictionary<int, string>();
            cSQLite SQLite = new cSQLite();
            if (SQLite.sqliteConnection.State == ConnectionState.Closed) SQLite.sqliteConnection.Open();
            SQLite.sqliteCommand = SQLite.sqliteConnection.CreateCommand();
            SQLite.sqliteCommand.CommandText = "SELECT uczenID, imie, nazwisko FROM uczen WHERE klasaNR = " + klasaID + " ORDER BY nazwisko;";
            SQLiteDataReader dataReader = SQLite.sqliteCommand.ExecuteReader();
            while (dataReader.Read())
            {
                listaUczniow.Add(Convert.ToInt32(dataReader["uczenID"].ToString()),
                                dataReader["imie"].ToString() + " " + dataReader["nazwisko"].ToString());
            }
            return listaUczniow;
        }
    }
}
