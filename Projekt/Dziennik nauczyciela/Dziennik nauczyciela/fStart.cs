using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Dziennik_nauczyciela
{
    public partial class fStart : Form
    {
        cSQLite SQLite = null;

        DataSet listaNauczycieli = null;
        DataTable tabelaListyNauczycieli = null;

        public fStart()
        {
            InitializeComponent();
            listaNauczycieli = new DataSet();
            tabelaListyNauczycieli = new DataTable("tabelaListyNauczycieli");
            this.t_haslo.PasswordChar = '\u25CF';
            this.dgv_listaUzytkownikow.BackgroundColor = this.BackColor;
            this.dgv_listaUzytkownikow.BorderStyle = BorderStyle.None;
            this.dgv_listaUzytkownikow.AllowUserToAddRows = false;
            
        }

        private void fStart_Load(object sender, EventArgs e)
        {
            SQLite = new cSQLite();
            SQLite.tworzenieBazyDanych();
            try
            {
                wczytajListeUzytkownikow();
                //wczytajListeUzytkownikow();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            dgv_listaUzytkownikow.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }


        private void wczytajListeUzytkownikow()
        {
            czyszczenieListyUzytkownikow();
            if (SQLite.sqliteConnection.State == ConnectionState.Closed) SQLite.sqliteConnection.Open();
            SQLite.sqliteCommand = SQLite.sqliteConnection.CreateCommand();
            SQLite.sqliteCommand.CommandText = "select * from nauczyciel";
            SQLiteDataReader dr = SQLite.sqliteCommand.ExecuteReader();
            // stworzenie listy nauczycieli
            List<BAZADANYCH.nauczyciel> listaUzytkownikow = new List<BAZADANYCH.nauczyciel>();

            while (dr.Read())
            {
                listaUzytkownikow.Add(new BAZADANYCH.nauczyciel
                {
                    nauczycielID = Convert.ToInt32(dr["nauczycielID"]),
                    imie = dr["imie"].ToString(),
                    nazwisko = dr["nazwisko"].ToString(),
                    email = dr["email"].ToString(),
                    login = dr["login"].ToString(),
                    haslo = dr["haslo"].ToString(),
                    email_haslo = dr["email_haslo"].ToString()
                });
            }

            listaNauczycieli = new DataSet();
            tabelaListyNauczycieli = new DataTable("tabelaListyNauczycieli");
            listaNauczycieli.Tables.Add(tabelaListyNauczycieli);

            tabelaListyNauczycieli.Columns.Add("ID", typeof(int));
            tabelaListyNauczycieli.Columns.Add("login", typeof(string));

            for (int i = 0; i < listaUzytkownikow.Count; i++)
            {
                DataRow drr = tabelaListyNauczycieli.NewRow();
                drr["ID"] = listaUzytkownikow[i].nauczycielID;
                drr["login"] = listaUzytkownikow[i].login;
                tabelaListyNauczycieli.Rows.Add(drr);
            }
            
            tabelaListyNauczycieli.AcceptChanges();

            this.dgv_listaUzytkownikow.AutoGenerateColumns = false;

            DataGridViewTextBoxColumn col = new DataGridViewTextBoxColumn();
            col.DataPropertyName = "ID";
            col.Name = "ID";
            col.HeaderText = "ID";
            col.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCellsExceptHeader;
            this.dgv_listaUzytkownikow.Columns.Add(col);

            col = new DataGridViewTextBoxColumn();
            col.DataPropertyName = "login";
            col.Name = "login";
            col.HeaderText = "login";
            col.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            this.dgv_listaUzytkownikow.Columns.Add(col);

            this.dgv_listaUzytkownikow.DataSource = listaNauczycieli.Tables["tabelaListyNauczycieli"];    
        }

        private void b_dodaj_Click(object sender, EventArgs e)
        {
            dodajUzytkownika();
            t_haslo.Text = string.Empty;
            t_nazwaUzytkownika.Text = string.Empty;
        }

        private void dodajUzytkownika()
        {

            try
            {
            BAZADANYCH.nauczyciel nowyNauczyciel = new BAZADANYCH.nauczyciel
            {
                login = t_nazwaUzytkownika.Text,
                haslo = t_haslo.Text,
                imie = string.Empty,
                nazwisko = string.Empty,
                email = string.Empty,
                email_haslo = string.Empty,
                zalogowany_mail = 0
            };

            if (SQLite.sqliteConnection.State == ConnectionState.Closed) SQLite.sqliteConnection.Open();
            SQLite.sqliteCommand = SQLite.sqliteConnection.CreateCommand();
            SQLite.sqliteCommand.CommandText = "INSERT INTO nauczyciel (login, haslo, imie, nazwisko, email, email_haslo, zalogowany_mail) VALUES (@login, @haslo, @imie, @nazwisko, @email, @email_haslo, @zalogowany_mail);";
            SQLite.sqliteCommand.Parameters.AddWithValue("login", nowyNauczyciel.login);
            SQLite.sqliteCommand.Parameters.AddWithValue("haslo", nowyNauczyciel.haslo);
            SQLite.sqliteCommand.Parameters.AddWithValue("imie", nowyNauczyciel.imie);
            SQLite.sqliteCommand.Parameters.AddWithValue("nazwisko", nowyNauczyciel.nazwisko);
            SQLite.sqliteCommand.Parameters.AddWithValue("email", nowyNauczyciel.email);
            SQLite.sqliteCommand.Parameters.AddWithValue("email_haslo", nowyNauczyciel.email_haslo);
            SQLite.sqliteCommand.Parameters.AddWithValue("zalogowany_mail", nowyNauczyciel.zalogowany_mail);

                SQLite.sqliteCommand.ExecuteNonQuery();

                wczytajListeUzytkownikow();
            } catch(Exception ex){
                MessageBox.Show(ex.Message);
                SQLite.Logger(ex.Message);
            }           
        }

        private void czyszczenieListyUzytkownikow()
        {
            while (dgv_listaUzytkownikow.Columns.Count != 0) this.dgv_listaUzytkownikow.Columns.RemoveAt(0);
        }


        //Dodawanie uzytkownika - sprawdzanie dlugosci i wlczanie przycisku
        private void t_nazwaUzytkownika_TextChanged(object sender, EventArgs e)
        {
            b_dodaj.Enabled = sprawdzCzyWypelnioneDane();
        }
        private void t_haslo_TextChanged(object sender, EventArgs e)
        {
            b_dodaj.Enabled = sprawdzCzyWypelnioneDane();
        }
        private bool sprawdzCzyWypelnioneDane()
        {
            return (Convert.ToBoolean(t_nazwaUzytkownika.Text.Length) && Convert.ToBoolean(t_haslo.Text.Length));
        }

        private void dgv_listaUzytkownikow_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            DataGridViewRow row = this.dgv_listaUzytkownikow.Rows[e.RowIndex];
            SQLite.sqliteCommand = SQLite.sqliteConnection.CreateCommand();
            SQLite.sqliteCommand.CommandText = "select * from nauczyciel where nauczycielID=@wybranyUzytkownikID;";
            SQLite.sqliteCommand.Parameters.AddWithValue("wybranyUzytkownikID", row.Cells[0].Value.ToString());
            SQLite.sqliteCommand.ExecuteNonQuery();
            SQLiteDataReader dataReader = SQLite.sqliteCommand.ExecuteReader();
            BAZADANYCH.nauczyciel logujacyUzytkownik = null;
            while (dataReader.Read())
            {
                logujacyUzytkownik = new BAZADANYCH.nauczyciel
                {
                    nauczycielID    = Convert.ToInt32(dataReader["nauczycielID"]),
                    imie            = dataReader["imie"].ToString(),
                    nazwisko        = dataReader["nazwisko"].ToString(),
                    email           = dataReader["email"].ToString(),
                    login           = dataReader["login"].ToString(),
                    haslo           = dataReader["haslo"].ToString(),
                    email_haslo     = dataReader["email_haslo"].ToString(),
                    zalogowany_mail = (Convert.ToInt32(dataReader["zalogowany_mail"]))
                };
                //if (counter > 1) MessageBox.Show("za duzo uzytkownikow o takim ID!"); return;
            }
               
            cDaneDoWatku daneDoWatku = new cDaneDoWatku
            {
               tytulOkna        = logujacyUzytkownik.login,
               haslo            = logujacyUzytkownik.haslo,
               flaga            = false
            };
            
            Task podajHaslo = new Task((dane) =>
            {
                fWalidacjaHaslaKlasy.uruchamianieNowegoWatkuDoWalidacjiHasla(daneDoWatku);
            }, daneDoWatku);

            podajHaslo.Start();
            podajHaslo.Wait();

            if (daneDoWatku.flaga == true) {
                fListaKlas listaKlas = new fListaKlas(logujacyUzytkownik);
                this.Hide();
                listaKlas.ShowDialog();
                this.Close();
            }
            else MessageBox.Show("zle!");

        }

        private void t_nazwaUzytkownika_Enter(object sender, EventArgs e)
        {
            ActiveForm.AcceptButton = b_dodaj;
        }
        
    }
}