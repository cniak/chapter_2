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
    public partial class fWidokPrzedmiotu : fWidokUczniaLubPrzedmiotu
    {
        public fWidokPrzedmiotu(int klasaID)
        {
            InitializeComponent();
            base.wczytajDane(klasaID, "przedmiot");
            base.wczytajListe();
            this.b_dodajPrzedmiot.Enabled = false;
            this.b_zapiszZmiany.Enabled = false;
        }

        private void b_dodajPrzedmiot_Click(object sender, EventArgs e)
        {
            try
            {
                if (SQLite.sqliteConnection.State == ConnectionState.Closed) SQLite.sqliteConnection.Open();

                SQLite.sqliteCommand = SQLite.sqliteConnection.CreateCommand();
                SQLite.sqliteCommand.CommandText = "INSERT INTO przedmiot (klasaNR, nazwa) VALUES (@klasaNR, @nazwa);";
                SQLite.sqliteCommand.Parameters.AddWithValue("klasaNR", this.klasaID);
                SQLite.sqliteCommand.Parameters.AddWithValue("nazwa", t_nazwaPrzedmiotu.Text);

                SQLite.sqliteCommand.ExecuteNonQuery();

                SQLite.sqliteConnection.Close();
                t_nazwaPrzedmiotu.Text = string.Empty;
                base.wczytajListe();
                t_edytujNazwaPrzedmiotu.Text = string.Empty;
            }
            catch (Exception)
            {
                MessageBox.Show("Istnieje juz przedmiot o takiej nazwie w tej klasie.");
            }
        }

        private void t_nazwaPrzedmiotu_TextChanged(object sender, EventArgs e)
        {
            b_dodajPrzedmiot.Enabled = t_nazwaPrzedmiotu.Text.Length != 0;
        }
        public override void przedmiot_wczytajDaneDoEdycji()
        {
            if (indexZaznaczonegoElementu < 0){
                t_edytujNazwaPrzedmiotu.Text = string.Empty;
                b_zapiszZmiany.Enabled = false;
                return;
            }
            t_edytujNazwaPrzedmiotu.Text = dgv_lista["nazwa", indexZaznaczonegoElementu].Value.ToString();
            b_zapiszZmiany.Enabled = true;
        }

        private void b_zapiszZmiany_Click(object sender, EventArgs e)
        {
            zapiszZmiany();
        }

        public void zapiszZmiany()
        {
            try
            {
                if (SQLite.sqliteConnection.State == ConnectionState.Closed) SQLite.sqliteConnection.Open();
                SQLite.sqliteCommand = SQLite.sqliteConnection.CreateCommand();
                SQLite.sqliteCommand.CommandText = "UPDATE przedmiot SET nazwa ='" + t_edytujNazwaPrzedmiotu.Text + "' WHERE przedmiotID =" + IDZaznaczonegoElementu + ";";
                SQLite.sqliteCommand.ExecuteNonQuery();

                t_edytujNazwaPrzedmiotu.Text = string.Empty;
                indexZaznaczonegoElementu = -1;
                b_zapiszZmiany.Enabled = false;

                wczytajListe();
                SQLite.sqliteConnection.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        
        private void t_edytujNazwaPrzedmiotu_TextChanged(object sender, EventArgs e)
        {
            b_zapiszZmiany.Enabled = ((t_edytujNazwaPrzedmiotu.Text.Length != 0) && (indexZaznaczonegoElementu >= 0));
        }

        private void fWidokPrzedmiotu_Load(object sender, EventArgs e)
        {
            base.wczytajListe();
        }
        
    }
}
