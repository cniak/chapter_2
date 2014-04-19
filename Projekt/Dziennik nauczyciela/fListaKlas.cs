using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices;

namespace Dziennik_nauczyciela
{
    public partial class fListaKlas : Form
    {
        //[DllImport("user32.dll", Entrypoint = "func1", SeLastError = true)]
        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true, CallingConvention = CallingConvention.Winapi)]
        public static extern short GetKeyState(int keyCode);
        [DllImport("wininet.dll")]
        private extern static bool InternetGetConnectedState(out int conn, int val);
        cSQLite SQLite = null;

        DataSet listaKlas = null;
        DataTable tabelaListyKlas = null;
        BAZADANYCH.nauczyciel nauczyciel = null;
        private int zaznaczonaKlasaDoUsunieciaID;
        private int indexZaznaczonegoWiersza;

        public fListaKlas(BAZADANYCH.nauczyciel zalogowanyNauczyciel)
        {
            listaKlas = new DataSet();
            tabelaListyKlas = new DataTable("tabelaListyKlas");
            this.nauczyciel = zalogowanyNauczyciel;
            SQLite = new cSQLite();

            InitializeComponent();
            l_rocznikInfo.Visible = false;
            s_statusCapslock.Visible = (((ushort)GetKeyState(0x14)) & 0xffff) != 0;
            obslugaPrzyciskuDodajKlase();

            this.Text = "Wybor klasy (" + nauczyciel.login + ")";
            this.t_hasloMail.PasswordChar = '\u25CF';
            this.t_loginMail.Text = nauczyciel.email;
            this.t_hasloMail.Text = nauczyciel.email_haslo;
            this.dgv_listaKlas.BackgroundColor = this.BackColor;
            this.dgv_listaKlas.BorderStyle = BorderStyle.None;
            this.gb_mailZalogowany.Visible = false;
            this.gb_powiazanieKontaZPoczta.Visible = false;
            this.b_usunKlase.Enabled = false;

            if (!bw_polaczZMailem.IsBusy)
            {
                bw_polaczZMailem.RunWorkerAsync();
            }

            sprawdzPotrzebneDane();
            
            l_mail.Text = nauczyciel.email;
            this.dgv_listaKlas.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.b_dodaj.Enabled = false;
        }
        private void fListaKlas_Load(object sender, EventArgs e)
        {
            SQLite = new cSQLite();
            try
            {
                wczytajListeKlas();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void czyszczenieListyKlas()
        {
            while (dgv_listaKlas.Columns.Count != 0) this.dgv_listaKlas.Columns.RemoveAt(0);
        }
        private void wczytajListeKlas()
        {
            czyszczenieListyKlas();
            if (SQLite.sqliteConnection.State == ConnectionState.Closed) SQLite.sqliteConnection.Open();
            SQLite.sqliteCommand = SQLite.sqliteConnection.CreateCommand();
            SQLite.sqliteCommand.CommandText = "select * from klasa where nauczycielNR = @nauczycielID";
            SQLite.sqliteCommand.Parameters.AddWithValue("nauczycielID", this.nauczyciel.nauczycielID);
            SQLiteDataReader dr = SQLite.sqliteCommand.ExecuteReader();
            List<BAZADANYCH.klasa> BAZADANCYHListaKlas = new List<BAZADANYCH.klasa>();            
            //tworzenie listy klas
            int counter = 0;
            while (dr.Read())
            {
                BAZADANCYHListaKlas.Add(new BAZADANYCH.klasa
                {
                    klasaID = Convert.ToInt32(dr["klasaID"]),
                    nazwa = dr["nazwa"].ToString(),
                    rocznik = dr["rocznik"].ToString(),
                    nauczycielNR = Convert.ToInt32(dr["nauczycielNR"])
                });
                counter++;
            }
            if (counter == 0)
            {
                b_usunKlase.Visible = dgv_listaKlas.Visible = false;
                return;
            }
            else
            {
                b_usunKlase.Visible = dgv_listaKlas.Visible = true;
            }
            listaKlas= new DataSet();
            tabelaListyKlas = new DataTable("tabelaListyKlas");
            listaKlas.Tables.Add(tabelaListyKlas);

            tabelaListyKlas.Columns.Add("ID", typeof(int));
            tabelaListyKlas.Columns.Add("nazwa", typeof(string));
            tabelaListyKlas.Columns.Add("rocznik", typeof(string));

            for (int i = 0; i < BAZADANCYHListaKlas.Count; i++)
            {
                DataRow drr = tabelaListyKlas.NewRow();
                drr["ID"] = BAZADANCYHListaKlas[i].klasaID;
                drr["nazwa"] = BAZADANCYHListaKlas[i].nazwa;
                drr["rocznik"] = BAZADANCYHListaKlas[i].rocznik;
                tabelaListyKlas.Rows.Add(drr);
            }

            tabelaListyKlas.AcceptChanges();

            this.dgv_listaKlas.AutoGenerateColumns = false;

            DataGridViewTextBoxColumn col = new DataGridViewTextBoxColumn();
            col.DataPropertyName = "ID";
            col.Name = "ID";
            col.HeaderText = "ID";
            col.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.dgv_listaKlas.Columns.Add(col);

            col = new DataGridViewTextBoxColumn();
            col.DataPropertyName = "nazwa";
            col.Name = "nazwa";
            col.HeaderText = "nazwa";
            col.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.dgv_listaKlas.Columns.Add(col);

            col = new DataGridViewTextBoxColumn();
            col.DataPropertyName = "rocznik";
            col.Name = "rocznik";
            col.HeaderText = "rocznik";
            
            col.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            
            this.dgv_listaKlas.Columns.Add(col);

            this.dgv_listaKlas.DataSource = listaKlas.Tables["tabelaListyKlas"];
            // ustawienie czy ustatnia kolumna ma byc do konca szerokosci, czy jak pojawia sie vertical scroll bar to ma byc tylko tyle ile trzeba
            int suma = 0;
            for (int i = 0; i < dgv_listaKlas.Columns.Count; i++)
            {
                suma += dgv_listaKlas.Columns[i].Width;
            }
            if (suma < dgv_listaKlas.Size.Width)
            {
                dgv_listaKlas.Columns[dgv_listaKlas.Columns.Count - 1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }
            dgv_listaKlas.Columns["ID"].Visible = false;
        }
        private bool ustawieniePaskaIGrupy()
        {
            string lanON = "LAN: ON";
            string lanOFF = "LAN: OFF";
            string mailInfo = " | MAIL: ";
            string mailNiepolaczony = "niepolaczony";
            string mailZleHaslo = "zle haslo";

            if (sprawdzeniePolaczeniaZInternetem() == true)
            {
                
                this.s_statusInternetu.Text = lanON;
                /// TU TRZEBA SPRAWDZIC!!
                if (nauczyciel.zalogowany_mail == 1)
                {
                    
                    int result = polaczZMailem();

                    if (result == -1)
                    {
                        // jak cos poszlo nie tak
                        this.s_statusInternetu.Text += mailInfo + mailNiepolaczony;
                        return false;
                    }
                    else if (result == 0)
                    {
                        // jak zle pass
                        this.s_statusInternetu.Text += mailInfo + mailZleHaslo;
                        return false;
                    }
                    else if (result == 1)
                    {
                        // jak sie udalo
                        this.s_statusInternetu.Text += mailInfo + nauczyciel.email + @"@gmail.com";
                        //this.gb_mailZalogowany.Visible = true;
                        //this.gb_powiazanieKontaZPoczta.Visible = false;
                        return true;
                    }   
                }
                else
                {
                    this.s_statusInternetu.Text += mailInfo + mailNiepolaczony;
                    return false;
                }
            }
            else
            {
                this.s_statusInternetu.Text = lanOFF;
                this.s_statusInternetu.Text += mailInfo + mailNiepolaczony;
                return false;
            }
            return false;
        }
        private bool sprawdzeniePolaczeniaZInternetem()
        {
            //return System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable();
            int Out;
            return InternetGetConnectedState(out Out, 0);
            
        }
        private bool sprawdzenieCzyWypelnioneDane()
        {
            return ((t_nazwa.Text.Length != 0) && (t_rocznik1.Text.Length != 0) && (l_rocznikInfo.Visible == false));
        }       
        private void obslugaPrzyciskuDodajKlase()
        {
            t_nazwa.TextChanged += ((o, e) =>
            {
                b_dodaj.Enabled = sprawdzenieCzyWypelnioneDane();
                s_statusCapslock.Visible = (((ushort)GetKeyState(0x14)) & 0xffff) != 0;
            });

            t_rocznik1.TextChanged += ((o, e) =>
            {
                b_dodaj.Enabled = sprawdzenieCzyWypelnioneDane();
                s_statusCapslock.Visible = (((ushort)GetKeyState(0x14)) & 0xffff) != 0;
            });

        }
        private void b_dodaj_Click(object sender, EventArgs e)
        {
            if (sprawdzenieCzyWypelnioneDane())
            {
                    dodajKlase();
                    t_rocznik1.Text = t_nazwa.Text = string.Empty;
            }
        }
        private void dodajKlase()
        {
            try
            {
                BAZADANYCH.klasa nowaKlasa = new BAZADANYCH.klasa
                {
                    nazwa = t_nazwa.Text,
                    nauczycielNR = this.nauczyciel.nauczycielID,
                    rocznik = t_rocznik1.Text + @"/" + t_rocznik2.Text,
                    gospodarzNR = -1
                };

                if (SQLite.sqliteConnection.State == ConnectionState.Closed) SQLite.sqliteConnection.Open();
                SQLite.sqliteCommand = SQLite.sqliteConnection.CreateCommand();
                SQLite.sqliteCommand.CommandText = "INSERT INTO klasa (nazwa, nauczycielNR, rocznik, gospodarzNR) VALUES (@nazwa, @nauczycielNR, @rocznik,@gospodarzNR);";
                SQLite.sqliteCommand.Parameters.AddWithValue("nazwa", nowaKlasa.nazwa);
                SQLite.sqliteCommand.Parameters.AddWithValue("nauczycielNR", nowaKlasa.nauczycielNR);
                SQLite.sqliteCommand.Parameters.AddWithValue("rocznik", nowaKlasa.rocznik);
                SQLite.sqliteCommand.Parameters.AddWithValue("gospodarzNR", nowaKlasa.gospodarzNR);

                SQLite.sqliteCommand.ExecuteNonQuery();
                wczytajListeKlas();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                SQLite.Logger(ex.Message);
            }
        }
        private int polaczZMailem()
        {
            if (nauczyciel.email.Length == 0) return -1;
            ImapX.ImapClient klient = new ImapX.ImapClient("imap.gmail.com", 993, true, true);
            bool result = klient.Connect();
            if ((t_loginMail.Text.Length == 0) || (t_hasloMail.Text.Length == 0)) return - 1;
            if (result)
            {
                return Convert.ToInt32(klient.Login(t_loginMail.Text + @"@gmail.com", t_hasloMail.Text));
            }
            else return -1;
        }
        private void b_zalogujMail_Click(object sender, EventArgs e)
        {
            try
            {
                if (SQLite.sqliteConnection.State == ConnectionState.Closed) SQLite.sqliteConnection.Open();
                SQLite.sqliteCommand = SQLite.sqliteConnection.CreateCommand();
                SQLite.sqliteCommand.CommandText = "UPDATE nauczyciel set " +
                                                   "zalogowany_mail=1, " +
                                                   "email_haslo = '" + t_hasloMail.Text + "', " +
                                                   "email = '" + t_loginMail.Text + "' " +
                                                   "WHERE nauczycielID = " + nauczyciel.nauczycielID + ";";
                SQLite.sqliteCommand.ExecuteNonQuery();
                SQLite.sqliteConnection.Close();
                l_mail.Text = t_loginMail.Text;
                this.nauczyciel.zalogowany_mail = 1;
                this.nauczyciel.email_haslo = t_hasloMail.Text;
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
            bw_polaczZMailem.RunWorkerAsync();
        }
        private void b_edytujDane_Click(object sender, EventArgs e)
        {
            otworzOknoEdycjiDanych();
        }

        private bool sprawdzPotrzebneDane()
        {
            string wiadomoscKomunikatu = "";
            bool cosZle = false;
            if (this.nauczyciel.imie.Length == 0)
            {
                wiadomoscKomunikatu += "uzupelnij imie";
                cosZle = true;
            }
            if (this.nauczyciel.nazwisko.Length == 0)
            {
                wiadomoscKomunikatu += ",nazwisko";
                cosZle = true;
            }

            if (cosZle)
            {
                MessageBox.Show(wiadomoscKomunikatu,
                    "Informacja",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation,
                    MessageBoxDefaultButton.Button1);
                otworzOknoEdycjiDanych();
            }

            

            return ((this.nauczyciel.imie.Length != 0) && (this.nauczyciel.nazwisko.Length != 0));
        }
        private void otworzOknoEdycjiDanych()
        {
            Task edytujDane = new Task((ID) =>
            {
                int myID = Convert.ToInt32(ID);
                Application.Run(new fEdycjaNauczyciela(myID, nauczyciel));
            }, this.nauczyciel.nauczycielID);

            edytujDane.Start();
            edytujDane.Wait();
            // aktualizacja danych dla zaloguj
            if (SQLite.sqliteConnection.State == ConnectionState.Closed) SQLite.sqliteConnection.Open();
            SQLite.sqliteCommand = SQLite.sqliteConnection.CreateCommand();
            SQLite.sqliteCommand.CommandText = "select email,email_haslo from nauczyciel where nauczycielID=" + nauczyciel.nauczycielID + ";";
            using (SQLiteDataReader dr = SQLite.sqliteCommand.ExecuteReader())
            {
                dr.Read();
                nauczyciel.email= dr["email"].ToString();
                nauczyciel.email_haslo = dr["email_haslo"].ToString();
            }
            SQLite.sqliteConnection.Close();
            t_loginMail.Text = nauczyciel.email;
            t_hasloMail.Text = nauczyciel.email_haslo;
        }

        private void b_wyloguj_Click(object sender, EventArgs e)
        {
            try
            {
                if (SQLite.sqliteConnection.State == ConnectionState.Closed) SQLite.sqliteConnection.Open();
                SQLite.sqliteCommand = SQLite.sqliteConnection.CreateCommand();
                SQLite.sqliteCommand.CommandText = "UPDATE nauczyciel set " +
                                                   "zalogowany_mail=0 " +
                                                   "WHERE nauczycielID = " + nauczyciel.nauczycielID + ";";

                SQLite.sqliteCommand.ExecuteNonQuery();
                SQLite.sqliteConnection.Close();
                this.nauczyciel.zalogowany_mail = 0;
                this.nauczyciel.email_haslo = t_hasloMail.Text;

                if(!bw_polaczZMailem.IsBusy){
                    bw_polaczZMailem.RunWorkerAsync();
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }
        private void b_usunKlase_Click(object sender, EventArgs e)
        {
            /*
                SELECT nauczyciel.login, nauczyciel.haslo
                FROM nauczyciel
                LEFT JOIN klasa ON klasa.nauczycielNR = 15
                where (klasaID = 3 AND nauczyciel.nauczycielID = 15)
             */
            if (SQLite.sqliteConnection.State == ConnectionState.Closed) SQLite.sqliteConnection.Open();
            DataGridViewRow row = this.dgv_listaKlas.Rows[indexZaznaczonegoWiersza];
            SQLite.sqliteCommand = SQLite.sqliteConnection.CreateCommand();
            SQLite.sqliteCommand.CommandText = "SELECT nauczyciel.login, nauczyciel.haslo " +
                                                "FROM nauczyciel" +
                                                " LEFT JOIN klasa ON klasa.nauczycielNR = " + nauczyciel.nauczycielID +
                                                " WHERE (klasaID = @wybranaklasaID AND nauczyciel.nauczycielID = " + nauczyciel.nauczycielID + ");";
            SQLite.sqliteCommand.Parameters.AddWithValue("wybranaKlasaID", row.Cells[0].Value.ToString());
            SQLite.sqliteCommand.ExecuteNonQuery();
            SQLiteDataReader dataReader = SQLite.sqliteCommand.ExecuteReader();
            cDaneDoWatku daneDoWatku = null;
            while (dataReader.Read())
            {
                daneDoWatku = new cDaneDoWatku
                {
                    tytulOkna = dataReader["login"].ToString(),
                    haslo = dataReader["haslo"].ToString(),
                    flaga = false
                };
            }
            Task podajHaslo = new Task((dane) =>
            {
                fWalidacjaHaslaKlasy.uruchamianieNowegoWatkuDoWalidacjiHasla(daneDoWatku);
            }, daneDoWatku);

            podajHaslo.Start();
            podajHaslo.Wait();

            if (daneDoWatku.flaga == true)
            {
                usunKlase();
            }
            else MessageBox.Show("zle!");
            sprawdzenieCzyZaznaczoneDane();
            wczytajListeKlas();
        }
        private void usunKlase()
        {
            try
            {
                if (SQLite.sqliteConnection.State == ConnectionState.Closed) SQLite.sqliteConnection.Open();
                SQLite.sqliteCommand = SQLite.sqliteConnection.CreateCommand();
                SQLite.sqliteCommand.CommandText = "DELETE FROM klasa WHERE klasaID =" + zaznaczonaKlasaDoUsunieciaID + ";";
                SQLite.sqliteCommand.ExecuteNonQuery();
                SQLite.sqliteConnection.Close();
                wczytajListeKlas();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                SQLite.Logger(ex.Message);
            }
        }
        private void sprawdzenieCzyZaznaczoneDane()
        {
            if (dgv_listaKlas.SelectedRows.Count != 0)
            {
                b_usunKlase.Enabled = true;
            }
            else
            {
                b_usunKlase.Enabled = false;
            }

        }
        private void dgv_listaKlas_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < (dgv_listaKlas.Rows.Count))
            {
                zaznaczonaKlasaDoUsunieciaID = Convert.ToInt32(dgv_listaKlas[0, e.RowIndex].Value);
                indexZaznaczonegoWiersza = e.RowIndex;
            }
            sprawdzenieCzyZaznaczoneDane();   
        }
        // backgroundworker
        private void bw_polaczZMailem_DoWork(object sender, DoWorkEventArgs e)
        {
            e.Result = ustawieniePaskaIGrupy();
        }
        private void bw_polaczZMailem_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {

        }
        private void bw_polaczZMailem_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            bool status = (bool)e.Result;
            if (status == true)
            {
                this.gb_mailZalogowany.Visible = true;
                this.gb_powiazanieKontaZPoczta.Visible = false;
            }
            else
            {
                this.gb_mailZalogowany.Visible = false;
                this.gb_powiazanieKontaZPoczta.Visible = true;
            }
        }

        private void t_loginMail_TextChanged(object sender, EventArgs e)
        {
            s_statusCapslock.Visible = (((ushort)GetKeyState(0x14)) & 0xffff) != 0;
        }
        private void t_hasloMail_TextChanged(object sender, EventArgs e)
        {
            s_statusCapslock.Visible = (((ushort)GetKeyState(0x14)) & 0xffff) != 0;
        }
        private void t_rocznik1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (t_rocznik1.Text.Length == 0) t_rocznik2.Text = string.Empty;
                else t_rocznik2.Text = (Convert.ToInt32(t_rocznik1.Text) + 1).ToString();
                l_rocznikInfo.Visible = false;
            }
            catch (Exception)
            {
                l_rocznikInfo.Visible = true;
            }
        }
        private void t_rocznik2_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (t_rocznik2.Text.Length == 0) t_rocznik1.Text = string.Empty;
                else t_rocznik1.Text = (Convert.ToInt32(t_rocznik2.Text) - 1).ToString();
                l_rocznikInfo.Visible = false;
            }
            catch (Exception)
            {
                l_rocznikInfo.Visible = true;
            }
        }

        private void dgv_listaKlas_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyData & Keys.KeyCode)
            {
                case Keys.Up:
                case Keys.Right:
                case Keys.Down:
                case Keys.Left:
                    e.Handled = true;
                    e.SuppressKeyPress = true;
                    break;
            }
        }

        private void dgv_listaKlas_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < dgv_listaKlas.Rows.Count)
            {
                if ((nauczyciel.imie.Length == 0) || (nauczyciel.nazwisko.Length == 0))
                {
                    MessageBox.Show("Imie i nazwisko musi zostac uzupelnione!");
                    return;
                }
                fWidokKlasy widokKlasy = new fWidokKlasy(this.zaznaczonaKlasaDoUsunieciaID);
                this.Hide();
                widokKlasy.ShowDialog();
                this.Close();
            }
        }


    }
}
