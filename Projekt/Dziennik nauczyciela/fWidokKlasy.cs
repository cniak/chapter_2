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
using System.Threading;

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
            b_dodajDzien.Enabled = false;
            mc_kalendarz.MaxSelectionCount = 1;
            //wczytajDatyAsync();
            tworzPasekInformacji();
        }
        private void fWidokKlasy_Load(object sender, EventArgs e)
        {
            wczytajPrzedmioty.RunWorkerAsync();
        }
        private void tworzPasekInformacji()
        {
            try
            {
                SQLite.sqliteConnection.Open();
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

        private void mc_kalendarz_DateChanged(object sender, DateRangeEventArgs e)
        {
            b_dodajDzien.Enabled = true;
        }

        private void b_dodajDzien_Click(object sender, EventArgs e)
        {
            dodajDzien();
            b_dodajDzien.Enabled = false;
        }

        private void dodajDzien()
        {
            if (SQLite.sqliteConnection.State == ConnectionState.Closed) SQLite.sqliteConnection.Open();
            try
            {
                SQLite.sqliteCommand = SQLite.sqliteConnection.CreateCommand();
                SQLite.sqliteCommand.CommandText = "INSERT INTO data(dzien, klasaNR) VALUES('" + mc_kalendarz.SelectionStart.ToString("yyyy-MM-dd HH:mm:ss.fff") + "', " + this.klasaID + ");";
                SQLite.sqliteCommand.ExecuteNonQuery();
                SQLite.sqliteConnection.Close();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        
        private void wczytajPrzedmioty_DoWork(object sender, DoWorkEventArgs e)
        {
            List<string> listaPrzedmiotow = new List<string>();
                    if (SQLite.sqliteConnection.State == ConnectionState.Closed) SQLite.sqliteConnection.Open();
                    try
                    {
                        SQLite.sqliteCommand = SQLite.sqliteConnection.CreateCommand();
                        SQLite.sqliteCommand.CommandText = "SELECT nazwa FROM przedmiot WHERE klasaNR = " + this.klasaID + ";";
                        SQLiteDataReader dataReader = SQLite.sqliteCommand.ExecuteReader();
                        while (dataReader.Read())
                        {
                            listaPrzedmiotow.Add(dataReader["nazwa"].ToString());
                        }
                        SQLite.sqliteConnection.Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                    e.Result = listaPrzedmiotow;         
        }

        private void wczytajPrzedmioty_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            List<string> listaPrzedmiotow = (List<string>)e.Result;
            foreach (string przedmiot in listaPrzedmiotow)
            {
                //MessageBox.Show(przedmiot);
                cb_przedmiotDziennik.Items.Add(przedmiot);
                cb_przedmiotWykresy.Items.Add(przedmiot);
            }
             
        }

        private void b_pokazDaneDziennik_Click(object sender, EventArgs e)
        {
            usunKolumny();
            stworzKolumny(cb_typ.Items[cb_typ.SelectedIndex].ToString(), cb_przedmiotDziennik.Items[cb_przedmiotDziennik.SelectedIndex].ToString());
            usunWiersze();
            wczytajListeUczniowDoDziennika();
        }

        private void usunKolumny()
        {
            while (dgv_dziennik.Columns.Count != 0) dgv_dziennik.Columns.RemoveAt(dgv_dziennik.Columns.Count - 1);
        }
        private void usunWiersze()
        {
            while (dgv_dziennik.Rows.Count != 0) dgv_dziennik.Rows.RemoveAt(dgv_dziennik.Rows.Count - 1);
        }
        private void stworzKolumny(string typ, string przedmiot)
        {
            #region ID + schowanie
            DataGridViewColumn newCol = new DataGridViewColumn();
            DataGridViewCell cell = new DataGridViewTextBoxCell();
            newCol.CellTemplate = cell;

            newCol.HeaderText = "ID";
            newCol.Name = "ID";
            newCol.Visible = true;
            dgv_dziennik.Columns.Add(newCol);
            dgv_dziennik.Columns[0].Visible = false;
            #endregion
            #region imie i nazwisko
            newCol = new DataGridViewColumn();
            cell = new DataGridViewTextBoxCell();
            newCol.CellTemplate = cell;

            newCol.HeaderText = "Imie i nazwisko";
            newCol.Name = "imie_i_nazwisko";
            newCol.Visible = true;
            dgv_dziennik.Columns.Add(newCol);
            #endregion 
            #region daty
            if (this.SQLite.sqliteConnection.State == ConnectionState.Closed) SQLite.sqliteConnection.Open();
            try
            {
                SQLite.sqliteCommand = SQLite.sqliteConnection.CreateCommand();
                SQLite.sqliteCommand.CommandText = "SELECT dzien FROM data WHERE klasaNR = " + this.klasaID + " ORDER BY dzien;";
                SQLiteDataReader dataReader = SQLite.sqliteCommand.ExecuteReader();
                while (dataReader.Read())
                {
                    DateTime dt = (DateTime)dataReader["dzien"];
                    newCol = new DataGridViewColumn();
                    newCol.CellTemplate = cell;
                    newCol.HeaderText = newCol.Name = dt.ToShortDateString();
                    dgv_dziennik.Columns.Add(newCol);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
#endregion 
            #region frekwencja / srednia
            string ostatniaKolumna = (typ == "obecnosci") ? "Frekwencja" : "Srednia";
            newCol = new DataGridViewColumn();
            newCol.CellTemplate = cell;
            newCol.HeaderText = newCol.Name = ostatniaKolumna;
            dgv_dziennik.Columns.Add(newCol);
            #endregion
        }

        private void wczytajListeUczniowDoDziennika()
        {
            try
            {
                if (SQLite.sqliteConnection.State == ConnectionState.Closed) SQLite.sqliteConnection.Open();
                SQLite.sqliteCommand = SQLite.sqliteConnection.CreateCommand();
                SQLite.sqliteCommand.CommandText = "SELECT uczenID, imie, nazwisko FROM uczen WHERE klasaNR = " + this.klasaID + ";";
                SQLiteDataReader dataReader = SQLite.sqliteCommand.ExecuteReader();
                
                while (dataReader.Read())
                {
                    //dgv_dziennik.Columns["imie_i_nazwisko"] = "hehe";

                    DataGridViewRow row = (DataGridViewRow)dgv_dziennik.RowTemplate.Clone();
                    row.CreateCells(dgv_dziennik, Convert.ToInt32(dataReader["uczenID"].ToString()), dataReader["imie"].ToString() + " " + dataReader["nazwisko"].ToString());
                    dgv_dziennik.Rows.Add(row);
                }
                SQLite.sqliteConnection.Close();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private int wczytajIDPrzedmiotu()
        {
            if (SQLite.sqliteConnection.State == ConnectionState.Closed) SQLite.sqliteConnection.Open();
            SQLite.sqliteCommand = SQLite.sqliteConnection.CreateCommand();
            SQLite.sqliteCommand.CommandText = "SELECT przedmiotID FROM przedmiot WHERE nazwa = '" + cb_przedmiotDziennik.Items[cb_przedmiotDziennik.SelectedIndex].ToString() + "';";
            SQLiteDataReader dataReader = SQLite.sqliteCommand.ExecuteReader();
            int ID = 0;
            while (dataReader.Read())
            {
                ID = Convert.ToInt32(dataReader["przedmiotID"].ToString());
            }
            SQLite.sqliteConnection.Close();
            return ID;
        }

        private void wczytajDaneDziennik(string typ, int przedmiotID)
        {
            
        }

    }
}
