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
    //TODO: zrownoleglic zapytania, wczytywanie dgv itd :-)
    public partial class fWidokKlasy : Form
    {
        cSQLite SQLite = null;
        private int klasaID = -1;
        private int gospodarzNR = -1;
        List<DateTime> listaDat = null;
        /* indywidualne */
        int zaznaczonyUczenID = -1;
        public fWidokKlasy(int klasaID)
        {
            InitializeComponent();
            this.klasaID = klasaID;
            SQLite = new cSQLite();
            b_dodajDzien.Enabled = false;
            mc_kalendarz.MaxSelectionCount = 1;
            //wczytajDatyAsync();
            tworzPasekInformacji();
            cb_przedmiotDziennik.ValueMember = "Key";
            cb_przedmiotWykresy.ValueMember = "Key";
            cb_przedmiotDziennik.DisplayMember = "Value";
            cb_przedmiotWykresy.DisplayMember = "Value";
            wczytajDaneIndywidualne();
        }

        private void kolorujKalendarz()
        {
            if (SQLite.sqliteConnection.State == ConnectionState.Closed) SQLite.sqliteConnection.Open();
            SQLite.sqliteCommand = SQLite.sqliteConnection.CreateCommand();
            SQLite.sqliteCommand.CommandText = "SELECT dzien FROM data WHERE klasaNR = " + this.klasaID + ";";
            SQLiteDataReader dataReader = SQLite.sqliteCommand.ExecuteReader();
            listaDat = new List<DateTime>();
            while (dataReader.Read())
            {
                listaDat.Add((DateTime)dataReader["dzien"]);
            }
            SQLite.sqliteConnection.Close();
            this.BeginInvoke(new Action(() =>
            {
                this.mc_kalendarz.RemoveAllBoldedDates();
                this.mc_kalendarz.BoldedDates = listaDat.ToArray();
            }));
        }
        
        private void fWidokKlasy_Load(object sender, EventArgs e)
        {
            if(!bg_wczytajPrzedmioty.IsBusy) bg_wczytajPrzedmioty.RunWorkerAsync();
            kolorujKalendarz();
            stworzKolumnyIndywidualne("oceny", dgv_listaOcen_indywidualne);
            stworzKolumnyIndywidualne("obecnosci", dgv_listaObecnosci_indywidualne);
            dodajPrzedmiotyIndywidualne(dgv_listaOcen_indywidualne);
            dodajPrzedmiotyIndywidualne(dgv_listaObecnosci_indywidualne);

        }
        private void tworzPasekInformacji()
        {
            try
            {
                SQLite.sqliteConnection.Open();
                SQLite.sqliteCommand = SQLite.sqliteConnection.CreateCommand();
                
                SQLite.sqliteCommand.CommandText = "select klasa.nazwa, klasa.gospodarzNR, uczen.imie as uImie, uczen.nazwisko as uNazwisko, nauczyciel.imie as nImie, nauczyciel.nazwisko as nNazwisko " +
                                                   "FROM klasa " +
                                                   "LEFT JOIN nauczyciel " +
                                                   "LEFT JOIN uczen ON uczen.uczenID = klasa.gospodarzNR " +
                                                   "WHERE klasaID = " + this.klasaID + ";";
                
                //SQLite.sqliteCommand.CommandText = "select * from klasa WHERE klasaID = " + 1 + ";";
                using (SQLiteDataReader dataReader = SQLite.sqliteCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        l_nazwaKlasy.Text = dataReader["nazwa"].ToString();
                        l_prowadzacy.Text = dataReader["nImie"].ToString() + " " + dataReader["nNazwisko"].ToString();
                        if (Convert.ToInt32(dataReader["gospodarzNR"].ToString()) == -1)
                        {
                            l_gospodarz.Text = "nie wybrany";
                        }
                        else
                        {
                            l_gospodarz.Text = dataReader["uImie"].ToString() + " " + dataReader["uNazwisko"].ToString();   
                        }
                        this.gospodarzNR = Convert.ToInt32(dataReader["gospodarzNR"].ToString());
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
            fEdycjaKlasy edycjaKlasy = new fEdycjaKlasy(this.klasaID, this.gospodarzNR);
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
            nowyWidokKlasy(this.klasaID);
        }
        private void nowyWidokKlasy(int ID)
        {
            fWidokKlasy widokKlasy = new fWidokKlasy(ID);
            this.Hide();
            widokKlasy.ShowDialog();
            this.Close();
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
            nowyWidokKlasy(this.klasaID);
        }
        //TODO 2 poprawic by nie pogrubialo zaznaczonych, tylko te po dodaniu
        //TODO 1 zrobic komunikat po dodaniu daty (odswiez wszystko!)
        private void mc_kalendarz_DateChanged(object sender, DateRangeEventArgs e)
        {
            
                b_dodajDzien.Enabled = true;
                listaDat.Add(e.Start);
            this.BeginInvoke(new Action(() => {
                this.mc_kalendarz.RemoveAllBoldedDates();
                this.mc_kalendarz.BoldedDates = listaDat.ToArray();
            }));
            
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
                    e.Result = new BindingSource(cStatyczneMetody.stworzListePrzedmiotow(this.klasaID), null);
        }
        private void wczytajPrzedmioty_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            cb_przedmiotDziennik.DataSource = e.Result;
            cb_przedmiotWykresy.DataSource = e.Result;
        }

        private void b_pokazDaneDziennik_Click(object sender, EventArgs e)
        {
            
            usunKolumny(dgv_dziennik);
            stworzKolumny_dziennik(cb_typ.Items[cb_typ.SelectedIndex].ToString(), cb_przedmiotDziennik.Items[cb_przedmiotDziennik.SelectedIndex].ToString());
            usunWiersze(dgv_dziennik);
            wczytajListeUczniowDoDziennika();
            // pobranie ID przedmiotu wybranego
            //string wybranyPrzedmiotID = ((KeyValuePair<int, string>)cb_przedmiotDziennik.SelectedItem).Key.ToString();
            
        }

        private void usunKolumny(DataGridView dgv)
        {
            while (dgv.Columns.Count != 0) dgv.Columns.RemoveAt(dgv.Columns.Count - 1);
        }
        private void usunWiersze(DataGridView dgv)
        {
            while (dgv.Rows.Count != 0) dgv.Rows.RemoveAt(dgv.Rows.Count - 1);
        }

        private void stworzKolumny_dziennik(string typ, string przedmiot)
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
                List<DateTime> listaDat = new List<DateTime>();
                while (dataReader.Read())
                {
                    DateTime dt = (DateTime)dataReader["dzien"];
                    listaDat.Add(dt);
                    if (typ == "obecnosci")
                    {
                        newCol = new DataGridViewCheckBoxColumn();
                        DataGridViewCheckBoxCell c = new DataGridViewCheckBoxCell();
                        newCol.CellTemplate = c;
                    }
                    else
                    {
                        newCol = new DataGridViewColumn();
                        newCol.CellTemplate = cell;
                    }
                    newCol.HeaderText = newCol.Name = dt.ToShortDateString();
                    dgv_dziennik.Columns.Add(newCol);
                }
                mc_kalendarz.BoldedDates = listaDat.ToArray<DateTime>();
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
        //TODO: przy porownywaniu dat nalezy jeszcze uwzglednic 00:00:00 (godziny, ktore i tak nie sa w ogole istotne, ale datetime dla struktury SQLite sie o to sapie
        private void wczytajDaneDziennik(string typ, int przedmiotID)
        {
            
        }

        private void cb_typ_SelectedIndexChanged(object sender, EventArgs e)
        {
            obslugaPokazDziennik();
        }

        private void cb_przedmiotDziennik_SelectedIndexChanged(object sender, EventArgs e)
        {
            obslugaPokazDziennik();
        }

        private void obslugaPokazDziennik()
        {
            if ((cb_przedmiotDziennik.SelectedIndex < 0) || (cb_typ.SelectedIndex < 0) || (cb_przedmiotDziennik.Text == cStatyczneMetody.pustaKolekcja)) b_pokazDaneDziennik.Enabled = false;
            else b_pokazDaneDziennik.Enabled = true;   
        }

        /* indywidualne */
        

        //na razie zbedne, pozniej moze wykorzystam
        private void bg_wczytajDaneIndywidualne_DoWork(object sender, DoWorkEventArgs e)
        {
            
        }
        private void wczytajDaneIndywidualne()
        {
            wczytajListeUczniowIndywidualne();
        }

        private void wczytajListeUczniowIndywidualne()
        {

            if (SQLite.sqliteConnection.State == ConnectionState.Closed) SQLite.sqliteConnection.Open();
            SQLite.sqliteCommand.CommandText = "SELECT uczenID, imie, nazwisko" +
                                              " FROM uczen" +
                                              " WHERE klasaNR = " + this.klasaID +
                                              " ORDER BY nazwisko;";
            SQLiteDataReader dataReader = SQLite.sqliteCommand.ExecuteReader();
            //tworzenie kolumn
            DataSet lista = new DataSet();
            DataTable tabelaListy = new DataTable("lista");
            lista.Tables.Add(tabelaListy);

            tabelaListy.Columns.Add("ID", typeof(int));
            tabelaListy.Columns.Add("dane", typeof(string));
            while (dataReader.Read())
            {
                DataRow drr = tabelaListy.NewRow();
                drr["ID"] = dataReader["uczenID"].ToString();

                drr["dane"] = dataReader["imie"].ToString() + " " + dataReader["nazwisko"].ToString();
                tabelaListy.Rows.Add(drr);
            }
            tabelaListy.AcceptChanges();
            this.dgv_listaUczniow_indywidualne.AutoGenerateColumns = false;
            DataGridViewTextBoxColumn col = new DataGridViewTextBoxColumn();
            col.DataPropertyName = "ID";
            col.Name = "ID";
            col.HeaderText = "ID";
            col.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.dgv_listaUczniow_indywidualne.Columns.Add(col);

            col = new DataGridViewTextBoxColumn();
            col.DataPropertyName = col.Name = col.HeaderText = "dane";
            col.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.dgv_listaUczniow_indywidualne.Columns.Add(col);

            this.dgv_listaUczniow_indywidualne.DataSource = lista.Tables["lista"];
            this.dgv_listaUczniow_indywidualne.Columns["ID"].Visible = false;
            this.dgv_listaUczniow_indywidualne.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

        }
        private void wczytajOcenyUczniaIndywidualne(int uczenID, string typ, DataGridView dgv)
        {
            for (int r = 0; r < dgv.Rows.Count; r++)
                {
                    for (int c = 2; c < dgv.Columns.Count - 1; c++)
                    {
                        string dataKolumny = dgv_listaOcen_indywidualne.Columns[c].HeaderText.ToString();
                        dataKolumny += " 00:00:00.000";
                        int ocena = pobierzOcene(Convert.ToInt32(dgv[0, r].Value), dataKolumny);
                        if (ocena < 0) dgv[c, r].Value = string.Empty; else dgv[c, r].Value = ocena;
                    }
                }
        }

        private int pobierzOcene(int przedmiotID, string dataKolumny)
        {
            int ocena = -1;
            int lekcjaID            = pobierzIDLekcjiLubStworz(dataKolumny, przedmiotID);
            int uczen_na_lekcjiID   = pobierzIDUczniaNaLekcjiLubStworz(this.zaznaczonyUczenID, lekcjaID);

            if (SQLite.sqliteConnection.State == ConnectionState.Closed) SQLite.sqliteConnection.Open();
            SQLite.sqliteCommand = SQLite.sqliteConnection.CreateCommand();
            SQLite.sqliteCommand.CommandText = "SELECT ocena " +
                                                "FROM uczen_na_lekcji " +
                                                "WHERE uczen_na_lekcjiID = " + uczen_na_lekcjiID + " AND ocena is not null;";
            SQLiteDataReader dataReader = SQLite.sqliteCommand.ExecuteReader();
            while (dataReader.Read())
            {
                ocena = Convert.ToInt32(dataReader["ocena"].ToString());
            }

            return ocena;
        }
        private void dodajPrzedmiotyIndywidualne(DataGridView dgv)
        {
            if (SQLite.sqliteConnection.State == ConnectionState.Closed) SQLite.sqliteConnection.Open();
            SQLite.sqliteCommand = SQLite.sqliteConnection.CreateCommand();
            SQLite.sqliteCommand.CommandText = "SELECT przedmiotID, nazwa FROM przedmiot WHERE klasaNR = " + this.klasaID + ";";
            SQLiteDataReader dataReader = SQLite.sqliteCommand.ExecuteReader();
            while (dataReader.Read())
            {
                DataGridViewRow row = (DataGridViewRow)dgv.RowTemplate.Clone();
                row.CreateCells(dgv, Convert.ToInt32(dataReader["przedmiotID"].ToString()), dataReader["nazwa"].ToString());
                dgv.Rows.Add(row);
            }
            SQLite.sqliteConnection.Close();
        }
        private void stworzKolumnyIndywidualne(string typ, DataGridView dgv)
        {
            #region ID + schowanie
            DataGridViewColumn newCol = new DataGridViewColumn();
            DataGridViewCell cell = new DataGridViewTextBoxCell();
            newCol.CellTemplate = cell;

            newCol.HeaderText = "ID";
            newCol.Name = "ID";
            newCol.Visible = true;
            dgv.Columns.Add(newCol);
            dgv.Columns[0].Visible = false;
            #endregion
            #region nazwa przedmiotu
            newCol = new DataGridViewColumn();
            cell = new DataGridViewTextBoxCell();
            newCol.CellTemplate = cell;

            newCol.HeaderText = "Nazwa przedmiotu";
            newCol.Name = "nazwa_przedmiotu";
            newCol.Visible = true;
            dgv.Columns.Add(newCol);
            #endregion

            if (this.SQLite.sqliteConnection.State == ConnectionState.Closed) SQLite.sqliteConnection.Open();
            try
            {
                SQLite.sqliteCommand = SQLite.sqliteConnection.CreateCommand();
                SQLite.sqliteCommand.CommandText = "SELECT dzien FROM data WHERE klasaNR = " + this.klasaID + " ORDER BY dzien;";
                SQLiteDataReader dataReader = SQLite.sqliteCommand.ExecuteReader();
                List<DateTime> listaDat = new List<DateTime>();
                while (dataReader.Read())
                {
                    DateTime dt = (DateTime)dataReader["dzien"];
                    listaDat.Add(dt);
                    if (typ == "obecnosci")
                    {
                        newCol = new DataGridViewCheckBoxColumn();
                        DataGridViewCheckBoxCell c = new DataGridViewCheckBoxCell();
                        newCol.CellTemplate = c;
                    }
                    else
                    {
                        newCol = new DataGridViewColumn();
                        newCol.CellTemplate = cell;
                    }
                    newCol.HeaderText = newCol.Name = dt.ToShortDateString();
                    dgv.Columns.Add(newCol);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            #region frekwencja / srednia
            string ostatniaKolumna = (typ == "obecnosci") ? "Frekwencja" : "Srednia";
            newCol = new DataGridViewColumn();
            newCol.CellTemplate = cell;
            newCol.HeaderText = newCol.Name = ostatniaKolumna;
            dgv.Columns.Add(newCol);
            #endregion
        }
        private void dgv_listaUczniow_indywidualne_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if ((e.RowIndex < 0) || (e.RowIndex >= dgv_listaUczniow_indywidualne.Rows.Count)) return;
            zaznaczonyUczenID = Convert.ToInt32(dgv_listaUczniow_indywidualne["ID", e.RowIndex].Value);
            wczytajOcenyUczniaIndywidualne(zaznaczonyUczenID,"oceny",dgv_listaOcen_indywidualne);

        }

        

        private int pobierzIDDaty(string dzien)
        {
            if (SQLite.sqliteConnection.State == ConnectionState.Closed) SQLite.sqliteConnection.Open();
            SQLite.sqliteCommand = SQLite.sqliteConnection.CreateCommand();
            SQLite.sqliteCommand.CommandText = "SELECT dataID FROM data WHERE dzien ='" + dzien + "';";
            SQLiteDataReader dataReader = SQLite.sqliteCommand.ExecuteReader();
            int ID = -1;
            while (dataReader.Read())
            {
                ID = Convert.ToInt32(dataReader["dataID"].ToString());
            }
            SQLite.sqliteConnection.Close();
            return ID;
        }

        private int pobierzIDLekcji(string dataKolumny, int przedmiotID)
        {
            if (SQLite.sqliteConnection.State == ConnectionState.Closed) SQLite.sqliteConnection.Open();

            SQLite.sqliteCommand = SQLite.sqliteConnection.CreateCommand();
            SQLite.sqliteCommand.CommandText = "SELECT " +
                                                "lekcjaID " +
                                                "FROM " +
                                                "lekcja " +
                                                "LEFT JOIN data ON lekcja.dataNR = data.dataID AND data.klasaNR = " + this.klasaID + " " +
                                                "WHERE " +
                                                "data.dzien ='" + dataKolumny + "' AND lekcja.przedmiotNR = " + przedmiotID + ";";
            SQLiteDataReader dataReader = SQLite.sqliteCommand.ExecuteReader();
            int lekcjaID = -1;
            while (dataReader.Read())
            {
                lekcjaID = Convert.ToInt32(dataReader["lekcjaID"].ToString());
            }
            SQLite.sqliteConnection.Close();
            return lekcjaID;
        }

        private int pobierzIDUczniaNaLekcji(int uczenNR, int lekcjaNR)
        {
            int ID = -1;
            if (SQLite.sqliteConnection.State == ConnectionState.Closed) SQLite.sqliteConnection.Open();
            SQLite.sqliteCommand = SQLite.sqliteConnection.CreateCommand();
            SQLite.sqliteCommand.CommandText = "SELECT uczen_na_lekcjiID FROM uczen_na_lekcji WHERE uczenNR = " + uczenNR + " AND lekcjaNR = " + lekcjaNR + ";";
            SQLiteDataReader dataReader = SQLite.sqliteCommand.ExecuteReader();
            while (dataReader.Read())
            {
                ID = Convert.ToInt32(dataReader["uczen_na_lekcjiID"].ToString());
            }

            return ID;
        }

        private int pobierzIDLekcjiLubStworz(string dataKolumny, int przedmiotID)
        {
            int lekcjaID = pobierzIDLekcji(dataKolumny, przedmiotID);
            if (lekcjaID == -1)
            {
                int dataID = pobierzIDDaty(dataKolumny);
                try
                {
                    if (SQLite.sqliteConnection.State == ConnectionState.Closed) SQLite.sqliteConnection.Open();
                    SQLite.sqliteCommand = SQLite.sqliteConnection.CreateCommand();
                    SQLite.sqliteCommand.CommandText = "INSERT INTO lekcja(klasaNR, przedmiotNR,dataNR) VALUES(@klasa,@przedmiot,@data);";
                    SQLite.sqliteCommand.Parameters.AddWithValue("klasa", this.klasaID);
                    SQLite.sqliteCommand.Parameters.AddWithValue("przedmiot", przedmiotID);
                    SQLite.sqliteCommand.Parameters.AddWithValue("data", dataID);
                    SQLite.sqliteCommand.ExecuteNonQuery();

                    lekcjaID = pobierzIDLekcji(dataKolumny, przedmiotID);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            SQLite.sqliteConnection.Close();
            return lekcjaID;
        }

        private int pobierzIDUczniaNaLekcjiLubStworz(int uczenNR, int lekcjaNR)
        {
            int ID = pobierzIDUczniaNaLekcji(uczenNR,lekcjaNR);
            if (ID < 0)
            {
                if (SQLite.sqliteConnection.State == ConnectionState.Closed) SQLite.sqliteConnection.Open();
                SQLite.sqliteCommand = SQLite.sqliteConnection.CreateCommand();
                SQLite.sqliteCommand.CommandText = "INSERT INTO uczen_na_lekcji(uczenNR, lekcjaNR, obecnosc) VALUES(@uczen,@lekcja,@obecnosc);";
                SQLite.sqliteCommand.Parameters.AddWithValue("uczen", uczenNR);
                SQLite.sqliteCommand.Parameters.AddWithValue("lekcja", lekcjaNR);
                SQLite.sqliteCommand.Parameters.AddWithValue("obecnosc", 0);
                //SQLite.sqliteCommand.Parameters.AddWithValue("ocena", 0);
                SQLite.sqliteCommand.ExecuteNonQuery();

                ID = pobierzIDUczniaNaLekcji(uczenNR, lekcjaNR);
            }
            return ID;
        }

        private void zapiszOcene(int wartosc, int uczen_na_lekcjiID)
        {
            if (SQLite.sqliteConnection.State == ConnectionState.Closed) SQLite.sqliteConnection.Open();
            SQLite.sqliteCommand = SQLite.sqliteConnection.CreateCommand();
            SQLite.sqliteCommand.CommandText = "UPDATE uczen_na_lekcji SET ocena = " + wartosc + " WHERE uczen_na_lekcjiID = " + uczen_na_lekcjiID + ";";
            SQLite.sqliteCommand.ExecuteNonQuery();
            SQLite.sqliteConnection.Close();
        }

        private void dgv_listaOcen_indywidualne_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if ((e.RowIndex < 0) || (e.RowIndex >= dgv_listaUczniow_indywidualne.Rows.Count)) return;
            string dataKolumny = dgv_listaOcen_indywidualne.Columns[e.ColumnIndex].HeaderText.ToString();
            //TODO 1 nie zapominac o tym!
            dataKolumny += " 00:00:00.000";
            int przedmiotID         = Convert.ToInt32(dgv_listaOcen_indywidualne["ID", e.RowIndex].Value);
            int lekcjaID            = pobierzIDLekcjiLubStworz(dataKolumny, przedmiotID);
            int uczen_na_lekcjiID   = pobierzIDUczniaNaLekcjiLubStworz(this.zaznaczonyUczenID, lekcjaID);
            try{
                int ocena = Convert.ToInt32(dgv_listaOcen_indywidualne[e.ColumnIndex, e.RowIndex].Value);
                zapiszOcene(ocena, uczen_na_lekcjiID);
            } catch (Exception ex){
                MessageBox.Show(ex.Message);
            }
        }

    }
}
