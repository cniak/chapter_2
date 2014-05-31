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
        Wykresy wykres = null;
        klasa zalogowanaKlasa = null;
        ListaDat listaDat = null;
        ListaPrzedmiotow listaPrzedmiotow = null;
        ListaUczniow listaUczniow_indywidualne = null;
        DuzyDziennik dziennik = null;
        DuzaLista dl_indywidualne_obecnosci = null;
        DuzaLista dl_indywidualne_oceny = null;
        //ListaUczniow listaUczniow_
        public fWidokKlasy(klasa zalogowanaKlasa)
        {
            this.zalogowanaKlasa = zalogowanaKlasa;
            InitializeComponent();
            tworzPasekInformacji();
        }

        private void fWidokKlasy_Load(object sender, EventArgs e)
        {
            string[] rocznik = zalogowanaKlasa.Rocznik.Split('/');
            mc_kalendarz.MinDate = new DateTime(Convert.ToInt32(rocznik[0]), 9, 1);
            mc_kalendarz.MaxDate = new DateTime(Convert.ToInt32(rocznik[1]), 6, 30);

           Dictionary<int, ETypDanych> DtypDanych = new Dictionary<int, ETypDanych>();
           DtypDanych.Add((int)ETypDanych.ocena, ETypDanych.ocena);
           DtypDanych.Add((int)ETypDanych.obecnosc, ETypDanych.obecnosc);
           cb_typ.DisplayMember = "Value";
           cb_typ.ValueMember = "Key";
           cb_typ.DataSource = new BindingSource(DtypDanych, null);

           listaDat = new ListaDat(mc_kalendarz, zalogowanaKlasa, cb_miesiace);
           Dictionary<string,ComboBox> zbiorListWyboru = new Dictionary<string,ComboBox>();
           zbiorListWyboru.Add("cb_przedmiotDziennik", cb_przedmiotDziennik);
           zbiorListWyboru.Add("cb_przedmiotWykresy", cb_przedmiotWykresy);
           listaPrzedmiotow = new ListaPrzedmiotow(this,null,zalogowanaKlasa.KlasaID,zbiorListWyboru);

           InicjaluzujComboBox();
           dziennik = new DuzyDziennik(this, dgv_dziennik, cb_miesiaceDziennik, cb_typ, cb_przedmiotDziennik, zalogowanaKlasa);
           wykres = new Wykresy(chart_Wykresy, ETypWykresu.liniowy, listaDat, zalogowanaKlasa.KlasaID, cb_zbiorWykresy, cb_przedmiotWykresy,cb_typWykresy);
           
        }

        private void InicjaluzujComboBox()
        {
            List<uczen> lu = uczen.pobierzWszystkich(zalogowanaKlasa.KlasaID);
            Dictionary<int, string> dt = new Dictionary<int, string>();
            foreach (uczen u in lu)
            {
                dt.Add(u.UczenID, u.Imie + " " + u.Nazwisko);
            }
            cb_zbiorWykresy.ValueMember = "Key";
            cb_zbiorWykresy.DisplayMember = "Value";
            cb_zbiorWykresy.DataSource = new BindingSource(dt, null); ;

            cb_miesiaceDziennik.ValueMember = "Key";
            cb_miesiaceDziennik.DisplayMember = "Value";
            cb_miesiaceDziennik.DataSource = new BindingSource(ListaDat.ustawMiesiacedlacb(zalogowanaKlasa.KlasaID), null);
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
            //listaUczniow_indywidualne.odswiezDGV();
        }

        private void przedmiotToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fWidokPrzedmiotu widokPrzedmiotu = new fWidokPrzedmiotu(zalogowanaKlasa,listaPrzedmiotow);
            widokPrzedmiotu.ShowDialog();
        }

        private void b_dodajDzien_Click(object sender, EventArgs e)
        {
            data d = new data
            {
                KlasaNR = this.zalogowanaKlasa.KlasaID,
                Dzien = listaDat.ZaznaczonyElement
            };
            try
            {
                //TODO 1 dodac event na odswiezenie duzej listy
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
            if (tabelaGlowna.SelectedTab == Indywidualne && (ladowaneTabele[(int)ladowanie.indywidualne] == false))
            {
                listaUczniow_indywidualne = new ListaUczniow(this, dgv_listaUczniow_indywidualne, zalogowanaKlasa.KlasaID);
                listaUczniow_indywidualne.Dgv.SelectionChanged += new EventHandler((oo, ee) =>
                {
                    t_uwagi_indywidualne.Text = listaUczniow_indywidualne.zbior[listaUczniow_indywidualne.ZaznaczonyWiersz].Uwagi;
                });
                if(listaUczniow_indywidualne.zbior.Count > 0) t_uwagi_indywidualne.Text = listaUczniow_indywidualne.zbior[listaUczniow_indywidualne.ZaznaczonyWiersz].Uwagi;

                ladowaneTabele[(int)ladowanie.indywidualne] = true;
                //listaDat.Cb_miesiace = cb_miesiace;
                if (listaDat.Cb_miesiace == null) listaDat.Cb_miesiace = cb_miesiace;
                dl_indywidualne_obecnosci = new DuzaLista(this, zalogowanaKlasa, dziennik,listaUczniow_indywidualne, listaPrzedmiotow, listaDat, dgv_listaObecnosci_indywidualne,ETypDanych.obecnosc);
                dl_indywidualne_oceny     = new DuzaLista(this, zalogowanaKlasa, dziennik,listaUczniow_indywidualne, listaPrzedmiotow, listaDat, dgv_listaOcen_indywidualne, ETypDanych.ocena);
            }
            if (tabelaGlowna.SelectedTab == Wykresy && (ladowaneTabele[(int)ladowanie.wykresy] == false))
            {
                ladowaneTabele[(int)ladowanie.wykresy] = true;
                cb_typWykresy.DisplayMember = "Value";
                cb_typWykresy.ValueMember = "Key";
                Dictionary<int, string> doTypu = new Dictionary<int, string>();
                doTypu.Add((int)ETypDanych.ocena, "ocena");
                doTypu.Add((int)ETypDanych.obecnosc, "obecnosc");
                cb_typWykresy.DataSource = new BindingSource(doTypu, null);
            }
        }

        private void b_pokazDaneDziennik_Click(object sender, EventArgs e)
        {
            
        }

        private void b_zapiszUwagi_indywidualne_Click(object sender, EventArgs e)
        {
            listaUczniow_indywidualne.zbior[listaUczniow_indywidualne.ZaznaczonyWiersz].wylaczEdycje = false;
                listaUczniow_indywidualne.zbior[listaUczniow_indywidualne.ZaznaczonyWiersz].Uwagi = t_uwagi_indywidualne.Text;
            listaUczniow_indywidualne.zbior[listaUczniow_indywidualne.ZaznaczonyWiersz].wylaczEdycje = true;
        }

        private void b_odswiezListe_Click(object sender, EventArgs e)
        {
            fWidokKlasy widokKlasy = new fWidokKlasy(zalogowanaKlasa);
            this.Hide();
            widokKlasy.ShowDialog();
            this.Close();
        }

        private void b_wyloguj_Click(object sender, EventArgs e)
        {
            nauczyciel n = new nauczyciel(zalogowanaKlasa.NauczycielNR);
            fListaKlas listaKlas = new fListaKlas(n);
            this.Hide();
            listaKlas.ShowDialog();
            this.Close();
        }

        private void cb_miesiace_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (dl_indywidualne_oceny != null)
            {
                dl_indywidualne_obecnosci.odswiezDGV();
                dl_indywidualne_oceny.odswiezDGV();
            }
        }

        private void b_zapiszPDFWykresy_Click(object sender, EventArgs e)
        {
                wykres.odswiezWykres();   
        }

    }
}
