using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SQLite;
using System.Threading.Tasks;

namespace Dziennik_nauczyciela
{
    public partial class fWidokKlasy : Form
    {
        cSQLite SQLite = null;
        private int klasaID = -1;
        public fWidokKlasy(int klasaID)
        {
            InitializeComponent();
            this.klasaID = klasaID;
            SQLite = new cSQLite();

            tworzPasekInformacji();
        }

        private void tworzPasekInformacji()
        {
            try
            {
                SQLite.sqliteConnection.Open();
                SQLite.sqliteCommand = SQLite.sqliteConnection.CreateCommand();
                /*
                SELECT nauczyciel.imie, nauczyciel.nazwisko
                FROM klasa
                LEFT JOIN nauczyciel
                WHERE klasa.nauczycielNR = 1 
                */
                SQLite.sqliteCommand.CommandText = "select klasa.nazwa, klasa.gospodarzNR, nauczyciel.imie, nauczyciel.nazwisko " +
                                                   "FROM klasa " +
                                                   "LEFT JOIN nauczyciel " +
                                                   "WHERE klasaID = " + this.klasaID + ";";
                 
                //SQLite.sqliteCommand.CommandText = "select * from klasa WHERE klasaID = " + 1 + ";";
                using (SQLiteDataReader dataReader = SQLite.sqliteCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        l_nazwaKlasy.Text = dataReader["nazwa"].ToString();
                        l_prowadzacy.Text = dataReader["imie"].ToString() + " " + dataReader["nazwisko"].ToString();
                        if (Convert.ToInt32(dataReader["gospodarzNR"].ToString()) == -1)
                        {
                            l_gospodarz.Text = "nie wybrany";
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

        private void edytujToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            fEdycjaKlasy edycjaKlasy = new fEdycjaKlasy(this.klasaID);
            Task t = new Task(() =>
            {
                edycjaKlasy.ShowDialog();
            });
            t.Start();
            t.Wait();
            tworzPasekInformacji();
        }

        private void przedmiotToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fWidokPrzedmiotu widokPrzedmiotu = new fWidokPrzedmiotu(this.klasaID);
            Task t = new Task(() =>
            {
                widokPrzedmiotu.ShowDialog();
            });
            t.Start();
            t.Wait();
        }

        private void uczeńToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fWidokUcznia widokUcznia = new fWidokUcznia(this.klasaID);
            Task t = new Task(() =>
                {
                    widokUcznia.ShowDialog();
                });
            t.Start();
            t.Wait();
        }
    }
}
