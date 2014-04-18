using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Dziennik_nauczyciela
{

    public partial class fStart : Form
    {
        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true, CallingConvention = CallingConvention.Winapi)]
        public static extern short GetKeyState(int keyCode);
        cSQLite SQLite = null;

        DataSet listaNauczycieli = null;
        DataTable tabelaListyNauczycieli = null;
        int zaznaczonyUzytkownikDoUsunieciaID = int.MaxValue;
        int indexZaznaczonegoWiersza = -1;
        public fStart()
        {
            InitializeComponent();
            l_capslock.Visible = (((ushort)GetKeyState(0x14)) & 0xffff) != 0;
            b_usun.Enabled = false;
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
            int counter = 0;
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
                counter++;
            }

            if (counter == 0) {
                b_usun.Visible = false;
                dgv_listaUzytkownikow.Visible = false;
                return;
            } else {
                b_usun.Visible = true;
                dgv_listaUzytkownikow.Visible = true;
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
            sprawdzenieCzyZaznaczoneDane();
            t_haslo.Text = string.Empty;
            t_nazwaUzytkownika.Text = string.Empty;
            b_usun.Enabled = false;
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
                //sprawdzenieCzyZaznaczoneDane();
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
            l_uzupelnijDane.Visible = !sprawdzCzyWypelnioneDane();
            b_dodaj.Enabled = sprawdzCzyWypelnioneDane();
            l_capslock.Visible = (((ushort)GetKeyState(0x14)) & 0xffff) != 0;
        }
        private void t_haslo_TextChanged(object sender, EventArgs e)
        {
            l_uzupelnijDane.Visible = !sprawdzCzyWypelnioneDane();
            b_dodaj.Enabled = sprawdzCzyWypelnioneDane();
            l_capslock.Visible = (((ushort)GetKeyState(0x14)) & 0xffff) != 0;

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
        private void b_usun_Click(object sender, EventArgs e)
        {
            if (SQLite.sqliteConnection.State == ConnectionState.Closed) SQLite.sqliteConnection.Open();
            DataGridViewRow row = this.dgv_listaUzytkownikow.Rows[indexZaznaczonegoWiersza];
            SQLite.sqliteCommand = SQLite.sqliteConnection.CreateCommand();
            SQLite.sqliteCommand.CommandText = "select * from nauczyciel where nauczycielID=@wybranyUzytkownikID;";
            SQLite.sqliteCommand.Parameters.AddWithValue("wybranyUzytkownikID", row.Cells[0].Value.ToString());
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
            MessageBox.Show(daneDoWatku.haslo);
            Task podajHaslo = new Task((dane) =>
                {
                    fWalidacjaHaslaKlasy.uruchamianieNowegoWatkuDoWalidacjiHasla(daneDoWatku);
                }, daneDoWatku);

            podajHaslo.Start();
            podajHaslo.Wait();
            SQLite.sqliteConnection.Close();
            if (daneDoWatku.flaga == true)
            {
                usunUzytkownika();
            }
            else MessageBox.Show("zle!");
            sprawdzenieCzyZaznaczoneDane();
            wczytajListeUzytkownikow();
        }

        private void usunUzytkownika()
        {
            try
            {
                if (SQLite.sqliteConnection.State == ConnectionState.Closed) SQLite.sqliteConnection.Open();
                SQLite.sqliteCommand = SQLite.sqliteConnection.CreateCommand();
                SQLite.sqliteCommand.CommandText = "DELETE FROM nauczyciel WHERE nauczycielID =" + zaznaczonyUzytkownikDoUsunieciaID + ";";
                SQLite.sqliteCommand.ExecuteNonQuery();

                wczytajListeUzytkownikow();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                SQLite.Logger(ex.Message);
            }
        }


        private void sprawdzenieCzyZaznaczoneDane()
        {
            if (dgv_listaUzytkownikow.SelectedRows.Count != 0)
            {
                b_usun.Enabled = true;
            }
            else
            {
                b_usun.Enabled = false;
            }

        }

        private void dgv_listaUzytkownikow_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            sprawdzenieCzyZaznaczoneDane();
            if (e.RowIndex >= 0 && e.RowIndex < (dgv_listaUzytkownikow.Rows.Count))
            {
                zaznaczonyUzytkownikDoUsunieciaID = Convert.ToInt32(dgv_listaUzytkownikow[0, e.RowIndex].Value);
                indexZaznaczonegoWiersza = e.RowIndex;
            }
        }

        //wylaczenie obslugi przewijania za pomoca klawiatury dla dgv_listaUzytkownikow
        private void dgv_listaUzytkownikow_KeyDown(object sender, KeyEventArgs e)
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
        
    }
}