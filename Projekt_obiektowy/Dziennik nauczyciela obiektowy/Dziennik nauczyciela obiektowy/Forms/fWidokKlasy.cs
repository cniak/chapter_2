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
    public partial class fWidokKlasy : Form
    {
        enum ladowanie
        {
            dziennik,
            wykresy,
            indywidualne,
            powiadomienia,
            wiadomosci
        }
        private bool[] ladowaneTabele = new bool[Convert.ToInt32(ladowanie.wiadomosci) + 1];
        klasa zalogowanaKlasa = null;
        ListaDat listaDat = null;
        ListaUczniow listaUczniow_indywidualne = null;
        public fWidokKlasy(klasa zalogowanaKlasa)
        {
            this.zalogowanaKlasa = zalogowanaKlasa;
            InitializeComponent();
            tworzPasekInformacji();
        }

        private void tworzPasekInformacji()
        {
            nauczyciel n = new nauczyciel(zalogowanaKlasa.NauczycielNR);
            if (zalogowanaKlasa.GospodarzNR > 0)
            {
                uczen u = new uczen(zalogowanaKlasa.GospodarzNR);
                l_gospodarz.Text = u.Imie + " " + u.Nazwisko;
            }
            else
            {
                l_gospodarz.Text = "nie wybrany";
            }
            l_nazwaKlasy.Text = zalogowanaKlasa.Nazwa;
            l_prowadzacy.Text = n.Imie + " " + n.Nazwisko;
        }

       
        private void fWidokKlasy_Load(object sender, EventArgs e)
        {
           listaDat = new ListaDat(mc_kalendarz, zalogowanaKlasa.KlasaID);
           cb_przedmiotDziennik.DisplayMember = "nazwa";
           cb_przedmiotDziennik.ValueMember = "przedmiotID";
           cb_przedmiotDziennik.DataSource = przedmiot.pobierzWszystkich(zalogowanaKlasa.KlasaID);
        }

        private void klasaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fEdycjaKlasy edycjaKlas = new fEdycjaKlasy(this.zalogowanaKlasa);
            edycjaKlas.ShowDialog();
            edycjaKlas.Hide();
            edycjaKlas.Close();
        }

        private void uczeńToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fWidokUcznia widokUcznia = new fWidokUcznia(zalogowanaKlasa.KlasaID);
            widokUcznia.ShowDialog();
        }

        private void przedmiotToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fWidokPrzedmiotu widokPrzedmiotu = new fWidokPrzedmiotu(zalogowanaKlasa.KlasaID);
            widokPrzedmiotu.ShowDialog();
            cb_przedmiotDziennik.DataSource = przedmiot.pobierzWszystkich(zalogowanaKlasa.KlasaID);
        }

        private void b_dodajDzien_Click(object sender, EventArgs e)
        {
            /*
            data d = new data
            {
                KlasaNR = this.zalogowanaKlasa.KlasaID,
                //Dzien = listaDat.ZaznaczonyElement.
            };
             */
            data d = new data
            {
                KlasaNR = this.zalogowanaKlasa.KlasaID,
                Dzien = listaDat.ZaznaczonyElement
            };
            try
            {
                d.dodajDoBazy();
                listaDat.aktualizujPogrubione();
            }
            catch
            {
                MessageBox.Show("już istnieje taka data w tej klasie");
            }
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedTab == Indywidualne && (ladowaneTabele[(int)ladowanie.indywidualne] == false))
            {
                listaUczniow_indywidualne = new ListaUczniow(this, dgv_listaUczniow_indywidualne, zalogowanaKlasa.KlasaID);
                ladowaneTabele[(int)ladowanie.indywidualne] = true;
            }
        }
    }
}
