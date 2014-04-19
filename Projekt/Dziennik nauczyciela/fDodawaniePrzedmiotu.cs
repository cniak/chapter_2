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
    public partial class fDodawaniePrzedmiotu : Form
    {
        int klasaID = -1;
        cSQLite SQLite = null;
        public fDodawaniePrzedmiotu(int klasaID)
        {
            InitializeComponent();
            SQLite = new cSQLite();
            this.klasaID = klasaID;
            this.b_dodaj.Enabled = false;
        }

        
        private void b_dodaj_Click(object sender, EventArgs e)
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
            }
            catch (Exception)
            {
                MessageBox.Show("Istnieje juz przedmiot o takiej nazwie w tej klasie.");
            }
        }

        private void t_nazwaPrzedmiotu_TextChanged(object sender, EventArgs e)
        {
            b_dodaj.Enabled = (this.t_nazwaPrzedmiotu.Text.Length != 0);
        }
    }
}
