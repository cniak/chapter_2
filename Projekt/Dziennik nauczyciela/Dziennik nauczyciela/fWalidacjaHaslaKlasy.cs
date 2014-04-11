using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Dziennik_nauczyciela
{
    public partial class fWalidacjaHaslaKlasy : Form
    {
        private cDaneDoWatku daneDoWatku = null;
        private string haslo             = null;

        public fWalidacjaHaslaKlasy(cDaneDoWatku daneDoWatku)
        {
			//edycja pliku 1
            this.daneDoWatku = daneDoWatku;
            this.haslo = daneDoWatku.haslo;
            InitializeComponent();
            sprawdzanieDlugosciHasla();
            this.Text = "Podaj haslo do: " + daneDoWatku.tytulOkna;
            this.t_haslo.PasswordChar = '\u25CF';
        }

        private void t_haslo_TextChanged(object sender, EventArgs e)
        {
            sprawdzanieDlugosciHasla();
        }
        private void sprawdzanieDlugosciHasla()
        {
            if (t_haslo.Text.Length != 0) b_polacz.Enabled = true;
            else b_polacz.Enabled = false;
        }

        private void b_polacz_Click(object sender, EventArgs e)
        {
            if (this.haslo == t_haslo.Text)
            {
                this.daneDoWatku.flaga = true;
                this.Close();
            }
            else
            {
                this.daneDoWatku.flaga = false;
                this.Close();
            }
        }


        public static void uruchamianieNowegoWatkuDoWalidacjiHasla(object o)
        {
            cDaneDoWatku daneDoWatku = (cDaneDoWatku)o;
            Application.Run(new fWalidacjaHaslaKlasy(daneDoWatku));
        }

        private void t_haslo_Enter(object sender, EventArgs e)
        {
            ActiveForm.AcceptButton = b_polacz;
        }
    }
    
    public class cDaneDoWatku
    {
        public string tytulOkna    { get; set; }
        public string haslo        { get; set; }
        public bool flaga          { get; set; }
    };
}
