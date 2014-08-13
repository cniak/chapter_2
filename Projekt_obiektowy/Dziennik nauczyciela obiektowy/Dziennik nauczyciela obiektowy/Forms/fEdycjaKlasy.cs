using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Dziennik_nauczyciela_obiektowy.Forms
{
    public partial class fEdycjaKlasy : Form
    {
        klasa zalogowanaKlasa = null;
        nauczyciel zalogowanyNauczyciel = null;
        public fEdycjaKlasy(klasa zalogowanaKlasa)
        {
            this.zalogowanaKlasa = zalogowanaKlasa;
            this.zalogowanaKlasa.wylaczEdycje = true;
            zalogowanyNauczyciel = new nauczyciel(zalogowanaKlasa.NauczycielNR);
            InitializeComponent();
            t_nauczyciel.Text = zalogowanyNauczyciel.Imie + " " + zalogowanyNauczyciel.Nazwisko;
            t_nazwaKlasy.Text = zalogowanaKlasa.Nazwa;
        }

        private void b_zapiszZmiany_Click(object sender, EventArgs e)
        {
            try
            {
                zalogowanaKlasa.Nazwa = t_nazwaKlasy.Text;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            try
            {
                zalogowanaKlasa.Nazwa = t_nazwaKlasy.Text;
                int gospodarzNR = Convert.ToInt32(cb_listaUczniow.SelectedValue.ToString());
                zalogowanaKlasa.wylaczEdycje = false;
                zalogowanaKlasa.aktualizuj("nazwa");
                zalogowanaKlasa.GospodarzNR = gospodarzNR;
                zalogowanaKlasa.wylaczEdycje = true;
            }
            catch (Exception)
            {
                    MessageBox.Show("gospodarz nie zostal zmieniony (pusta wartosc)");
            }
        }

        private void fEdycjaKlasy_Load(object sender, EventArgs e)
        {
            cb_listaUczniow.ValueMember = "Key";
            cb_listaUczniow.DisplayMember = "Value";
            
            List<uczen> listaUczniow = uczen.pobierzWszystkich(zalogowanaKlasa.KlasaID);
            Dictionary<int, string> slownikUczniow = new Dictionary<int, string>();
            foreach (uczen u in listaUczniow)
            {
                slownikUczniow.Add(u.UczenID, u.Imie + " " + u.Nazwisko);
            }
            cb_listaUczniow.DataSource = new BindingSource(slownikUczniow, null);
            cb_listaUczniow.SelectedValue = zalogowanaKlasa.GospodarzNR;
        }

    }
}
