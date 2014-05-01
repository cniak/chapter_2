using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SQLite;

namespace Dziennik_nauczyciela
{
    public partial class fEdycjaKlasy : Form
    {
        private int klasaID = -1;
        private int gospodarzNR = -1;
        cSQLite SQLite = null;
        public fEdycjaKlasy(int klasaID, int gospodarzNR)
        {
            InitializeComponent();
            this.klasaID = klasaID;
            SQLite = new cSQLite();
            wczytajDaneKlasy();
            this.gospodarzNR = gospodarzNR;
        }
        private void fEdycjaKlasy_Load(object sender, EventArgs e)
        {
            cb_listaUczniow.ValueMember = "Key";
            cb_listaUczniow.DisplayMember = "Value";
            cb_listaUczniow.DataSource = new BindingSource(cStatyczneMetody.stworzListeUczniow(this.klasaID), null);
            cb_listaUczniow.SelectedValue = this.gospodarzNR;
        }

        private void wczytajDaneKlasy()
        {
            try
            {
                if (SQLite.sqliteConnection.State == ConnectionState.Closed) SQLite.sqliteConnection.Open();
                SQLite.sqliteCommand = SQLite.sqliteConnection.CreateCommand();
                
                SQLite.sqliteCommand.CommandText = "select klasa.nazwa, klasa.gospodarzNR, nauczyciel.imie, nauczyciel.nazwisko " +
                                                 "FROM klasa " +
                                                 "LEFT JOIN nauczyciel " +
                                                 "WHERE klasaID = " + this.klasaID + ";";

                //SQLite.sqliteCommand.CommandText = "select * from klasa WHERE klasaID = " + 1 + ";";
                using (SQLiteDataReader dataReader = SQLite.sqliteCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        
                        t_nauczyciel.Text = dataReader["imie"].ToString() + " " + dataReader["nazwisko"].ToString();
                        t_nazwaKlasy.Text = dataReader["nazwa"].ToString();
                        if (Convert.ToInt32(dataReader["gospodarzNR"].ToString()) == -1)
                        {
                            //l_gospodarz.Text = "nie wybrany";
                        }
                    }
                };



                SQLite.sqliteConnection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void b_zapiszZmiany_Click(object sender, EventArgs e)
        {
            try
            {
                this.gospodarzNR = ((KeyValuePair<int, string>)cb_listaUczniow.SelectedItem).Key;
                if (SQLite.sqliteConnection.State == ConnectionState.Closed) SQLite.sqliteConnection.Open();
                SQLite.sqliteCommand = SQLite.sqliteConnection.CreateCommand();

                SQLite.sqliteCommand.CommandText = "UPDATE klasa " +
                                                   "SET nazwa = '" + t_nazwaKlasy.Text + "', gospodarzNR = " + this.gospodarzNR +
                                                   " WHERE klasaID = " + klasaID + ";";
                SQLite.sqliteCommand.ExecuteNonQuery();
                SQLite.sqliteConnection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void t_nazwaKlasy_TextChanged(object sender, EventArgs e)
        {
            b_zapiszZmiany.Enabled = (t_nazwaKlasy.Text.Length != 0);
        }
    }
}
