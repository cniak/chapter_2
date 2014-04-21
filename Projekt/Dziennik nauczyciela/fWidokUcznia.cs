using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Dziennik_nauczyciela
{
    public partial class fWidokUcznia : fWidokUczniaLubPrzedmiotu
    {
        delegate void ObslugaPrzycisku(object o, EventArgs e);
        public fWidokUcznia(int klasaID)
        {
            InitializeComponent();
            base.wczytajDane(klasaID, "uczen");
            b_dodaj.Enabled = false;
        }

        private void b_dodaj_Click(object sender, EventArgs e)
        {
            if (SQLite.sqliteConnection.State == ConnectionState.Closed) SQLite.sqliteConnection.Open();

            
            SQLite.sqliteCommand = SQLite.sqliteConnection.CreateCommand();
            SQLite.sqliteCommand.CommandText = "INSERT INTO uczen (klasaNR, imie, nazwisko, pesel, email, telefon_ucznia, telefon_rodzica) " +
                                               "VALUES(@klasaNR,@imie,@nazwisko,@pesel,@email,@telefon_ucznia,@telefon_rodzica);";
            SQLite.sqliteCommand.Parameters.AddWithValue("klasaNR", this.klasaID);
            SQLite.sqliteCommand.Parameters.AddWithValue("imie", t_imie_dodaj.Text);
            SQLite.sqliteCommand.Parameters.AddWithValue("nazwisko", t_nazwisko_dodaj.Text);
            SQLite.sqliteCommand.Parameters.AddWithValue("pesel", t_pesel_dodaj.Text);
            SQLite.sqliteCommand.Parameters.AddWithValue("email", t_email_dodaj.Text);
            SQLite.sqliteCommand.Parameters.AddWithValue("telefon_ucznia", t_nrUcznia_dodaj.Text);
            SQLite.sqliteCommand.Parameters.AddWithValue("telefon_rodzica", t_nrRodzica_dodaj.Text);
            try
            {
                SQLite.sqliteCommand.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                SQLite.Logger(ex.Message);
            }

            wyczyscDaneDodaj();
            wczytajListe();
        }

        private void wyczyscDaneEdytuj()
        {
            t_imie_edytuj.Text =
            t_nazwisko_edytuj.Text =
            t_pesel_edytuj.Text =
            t_email_edytuj.Text = 
            t_nrUcznia_edytuj.Text =
            t_nrRodzica_edytuj.Text = string.Empty;
            b_zapisz.Enabled = false;
        }
        private void wyczyscDaneDodaj()
        {
            t_imie_dodaj.Text =
            t_nazwisko_dodaj.Text =
            t_pesel_dodaj.Text =
            t_email_dodaj.Text =
            t_nrUcznia_dodaj.Text =
            t_nrRodzica_dodaj.Text = string.Empty;
            b_dodaj.Enabled = false;
        }

        //jest tutaj t_imie, a tak na prawde jest obluga wszystkich textboxow z pola dodaj
        private void t_imie_dodaj_TextChanged(object sender, EventArgs e)
        {
            b_dodaj.Enabled = sprawdzCzyWypelnioneDaneDoDodania();
        }
        private bool sprawdzCzyWypelnioneDaneDoDodania()
        {
            return ((t_imie_dodaj.Text.Length != 0) && (t_nazwisko_dodaj.Text.Length != 0) && (t_pesel_dodaj.Text.Length != 0));
        }

        private void fWidokUcznia_Load(object sender, EventArgs e)
        {
            base.wczytajListe();
        }
    }
}
