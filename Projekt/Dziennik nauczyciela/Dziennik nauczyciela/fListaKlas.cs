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

namespace Dziennik_nauczyciela
{
    public partial class fListaKlas : Form
    {
        cSQLite SQLite = null;

        DataSet listaKlas = null;
        DataTable tabelaListyKlas = null;
        BAZADANYCH.nauczyciel nauczyciel = null;

        public fListaKlas(BAZADANYCH.nauczyciel zalogowanyNauczyciel)
        {
            listaKlas = new DataSet();
            tabelaListyKlas = new DataTable("tabelaListyKlas");
            this.nauczyciel = zalogowanyNauczyciel;
            SQLite = new cSQLite();

            InitializeComponent();

            obslugaPrzyciskuDodajKlase();

            this.Text = "Wybor klasy (" + nauczyciel.login + ")";
            this.t_haslo.PasswordChar = '\u25CF';
            this.t_hasloMail.PasswordChar = '\u25CF';
            this.t_powtorzHaslo.PasswordChar = '\u25CF';
            this.t_loginMail.Text = nauczyciel.email;
            this.t_hasloMail.Text = nauczyciel.email_haslo;

            this.gb_mailZalogowany.Visible = false;
            this.gb_powiazanieKontaZPoczta.Visible = false;
            try
            {
                Task <bool> task = Task<bool>.Factory.StartNew(() =>
                {
                    return ustawieniePaskaIGrupy();
                });

                Task UITask = task.ContinueWith((tt) =>
                {
                    if (task.Result == true)
                    {
                        this.gb_mailZalogowany.Visible = true;
                        this.gb_powiazanieKontaZPoczta.Visible = false;
                    }
                    else
                    {
                        this.gb_mailZalogowany.Visible = false;
                        this.gb_powiazanieKontaZPoczta.Visible = true;
                    }
                    //this.t_nazwa.Text = "Complete";
                }, TaskScheduler.FromCurrentSynchronizationContext());
                
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
            sprawdzPotrzebneDane();
            //ustawieniePaskaIGrupy();
            
            l_mail.Text = nauczyciel.email;
            this.dgv_listaKlas.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.b_dodaj.Enabled = false;
            //polaczZMailem();
            //obslugaPrzyciskuDodajKlase();
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

            while (dr.Read())
            {
                BAZADANCYHListaKlas.Add(new BAZADANYCH.klasa
                {
                    klasaID = Convert.ToInt32(dr["klasaID"]),
                    nazwa = dr["nazwa"].ToString(),
                    rocznik = dr["rocznik"].ToString(),
                    nauczycielNR = Convert.ToInt32(dr["nauczycielNR"])
                });
            }
            
            listaKlas= new DataSet();
            tabelaListyKlas = new DataTable("tabelaListyKlas");
            listaKlas.Tables.Add(tabelaListyKlas);

            tabelaListyKlas.Columns.Add("ID", typeof(int));
            tabelaListyKlas.Columns.Add("nazwa", typeof(string));

            for (int i = 0; i < BAZADANCYHListaKlas.Count; i++)
            {
                DataRow drr = tabelaListyKlas.NewRow();
                drr["ID"] = BAZADANCYHListaKlas[i].klasaID;
                drr["nazwa"] = BAZADANCYHListaKlas[i].nazwa;
                tabelaListyKlas.Rows.Add(drr);
            }

            tabelaListyKlas.AcceptChanges();

            this.dgv_listaKlas.AutoGenerateColumns = false;

            DataGridViewTextBoxColumn col = new DataGridViewTextBoxColumn();
            col.DataPropertyName = "ID";
            col.Name = "ID";
            col.HeaderText = "ID";
            col.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCellsExceptHeader;
            this.dgv_listaKlas.Columns.Add(col);

            col = new DataGridViewTextBoxColumn();
            col.DataPropertyName = "nazwa";
            col.Name = "nazwa";
            col.HeaderText = "nazwa";
            col.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            this.dgv_listaKlas.Columns.Add(col);

            this.dgv_listaKlas.DataSource = listaKlas.Tables["tabelaListyKlas"];
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
            return System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable();
            
        }
        private bool sprawdzenieCzyWypelnioneDane()
        {
            return ((t_nazwa.Text.Length != 0) && (t_haslo.Text.Length != 0) && (t_powtorzHaslo.Text.Length != 0));
        }
        private bool sprawdzenieCzyPoprawneHasla()
        {
            return ((t_nazwa.Text.Length != 0) && (t_haslo.Text == t_powtorzHaslo.Text));
        }
        private void obslugaPrzyciskuDodajKlase()
        {
            t_nazwa.TextChanged += ((o, e) =>
            {
                b_dodaj.Enabled = sprawdzenieCzyWypelnioneDane();
            });

            t_haslo.TextChanged += ((o, e) =>
            {
                b_dodaj.Enabled = sprawdzenieCzyWypelnioneDane();
            });

            t_powtorzHaslo.TextChanged += ((o, e) =>
            {
                b_dodaj.Enabled = sprawdzenieCzyWypelnioneDane();
            });
        }
        private void b_dodaj_Click(object sender, EventArgs e)
        {
            if (sprawdzenieCzyWypelnioneDane())
            {
                if (sprawdzenieCzyPoprawneHasla())
                {
                    dodajKlase();
                    t_haslo.Text = t_powtorzHaslo.Text = t_nazwa.Text = string.Empty;
                }
                else
                {
                    MessageBox.Show("rozne hasla!");
                }
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
                    rocznik = string.Empty,
                    haslo = t_haslo.Text
                };

                if (SQLite.sqliteConnection.State == ConnectionState.Closed) SQLite.sqliteConnection.Open();
                SQLite.sqliteCommand = SQLite.sqliteConnection.CreateCommand();
                SQLite.sqliteCommand.CommandText = "INSERT INTO klasa (nazwa, nauczycielNR, rocznik, haslo) VALUES (@nazwa, @nauczycielNR, @rocznik, @haslo);";
                SQLite.sqliteCommand.Parameters.AddWithValue("nazwa", nowaKlasa.nazwa);
                SQLite.sqliteCommand.Parameters.AddWithValue("nauczycielNR", nowaKlasa.nauczycielNR);
                SQLite.sqliteCommand.Parameters.AddWithValue("rocznik", nowaKlasa.rocznik);
                SQLite.sqliteCommand.Parameters.AddWithValue("haslo", nowaKlasa.haslo);

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
                                                   "email_haslo = '" + t_hasloMail.Text + "' " +
                                                   "WHERE nauczycielID = " + nauczyciel.nauczycielID + ";";
                SQLite.sqliteCommand.ExecuteNonQuery();
                SQLite.sqliteConnection.Close();
                this.nauczyciel.zalogowany_mail = 1;
                this.nauczyciel.email_haslo = t_hasloMail.Text;
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }

            Task<bool> task = Task<bool>.Factory.StartNew(() =>
            {
                return ustawieniePaskaIGrupy();
            });

            Task UITask = task.ContinueWith((tt) =>
            {
                if (task.Result == true)
                {
                    this.gb_mailZalogowany.Visible = true;
                    this.gb_powiazanieKontaZPoczta.Visible = false;
                }
                else
                {
                    this.gb_mailZalogowany.Visible = false;
                    this.gb_powiazanieKontaZPoczta.Visible = true;
                }
            }, TaskScheduler.FromCurrentSynchronizationContext());

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
                Application.Run(new fEdycjaNauczyciela(myID));
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



                Task<bool> task = Task<bool>.Factory.StartNew(() =>
                {
                    return ustawieniePaskaIGrupy();
                });

                Task UITask = task.ContinueWith((tt) =>
                {
                    if (task.Result == true)
                    {
                        this.gb_mailZalogowany.Visible = true;
                        this.gb_powiazanieKontaZPoczta.Visible = false;
                    }
                    else
                    {
                        this.gb_mailZalogowany.Visible = false;
                        this.gb_powiazanieKontaZPoczta.Visible = true;
                    }
                    //this.t_nazwa.Text = "Complete";
                }, TaskScheduler.FromCurrentSynchronizationContext());







            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

    }
}
