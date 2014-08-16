using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Dziennik_nauczyciela_obiektowy
{
    public partial class fWalidacjaHasla : Form
    {
        string haslo = null;
        public bool czyPoprawne = false;
        public bool czyAnuluj = false;
        public fWalidacjaHasla(string haslo)
        {
            this.haslo = haslo;
            InitializeComponent();
            t_haslo.PasswordChar = '\u25CF';
        }

        private void t_haslo_TextChanged(object sender, EventArgs e)
        {
            b_polacz.Enabled = (t_haslo.Text.Length != 0);
        }
        private void b_polacz_Click(object sender, EventArgs e)
        {
            czyPoprawne = (t_haslo.Text == haslo);
            this.Hide();
            this.Close();
        }

        private void fWalidacjaHasla_Load(object sender, EventArgs e)
        {

        }

        private void b_anuluj_Click(object sender, EventArgs e)
        {
            czyAnuluj = true;
            this.Hide();
            this.Close();
        }

    }
}
