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
    public partial class fWidokUcznia : fWidokUczniaLubPrzedmiotu
    {
        ListaUczniow listaUczniow = null;

        int klasaNR = -1;
        public fWidokUcznia(int klasaID) : base(klasaID, typeof(uczen))
        {
            InitializeComponent();
            klasaNR = klasaID;
            b_dodaj.Enabled  = false;
            b_zapisz.Enabled = false;
            b_usun1.Enabled  = false;
        }

        private void ustawZdarzenia()
        {
            listaUczniow.Dgv.SelectionChanged += new EventHandler((s, e) =>
            {
                if (listaUczniow.Dgv.RowCount != 0)
                {
                    t_imie_edytuj.Text = listaUczniow.zbior[listaUczniow.ZaznaczonyWiersz].Imie;
                    t_nazwisko_edytuj.Text = listaUczniow.zbior[listaUczniow.ZaznaczonyWiersz].Nazwisko;
                    t_pesel_edytuj.Text = listaUczniow.zbior[listaUczniow.ZaznaczonyWiersz].Pesel;
                    t_email_edytuj.Text = listaUczniow.zbior[listaUczniow.ZaznaczonyWiersz].Email;
                    t_nrUcznia_edytuj.Text = listaUczniow.zbior[listaUczniow.ZaznaczonyWiersz].Telefon_ucznia;
                    t_nrRodzica_edytuj.Text = listaUczniow.zbior[listaUczniow.ZaznaczonyWiersz].Telefon_rodzica;
                    b_usun1.Enabled = listaUczniow.Dgv.RowCount != 0;
                }
            });
            t_imie_dodaj.TextChanged     += new System.EventHandler(obslugaPrzyciskuDodaj);
            t_nazwisko_dodaj.TextChanged += new System.EventHandler(obslugaPrzyciskuDodaj);
            t_pesel_dodaj.TextChanged    += new System.EventHandler(obslugaPrzyciskuDodaj);

            t_imie_edytuj.TextChanged     += new System.EventHandler(obslugaPrzyciskuZapisz);
            t_nazwisko_edytuj.TextChanged += new System.EventHandler(obslugaPrzyciskuZapisz);
            t_pesel_edytuj.TextChanged    += new System.EventHandler(obslugaPrzyciskuZapisz);

            t_usun.TextChanged += new System.EventHandler((s, e) =>
            {
                b_usun1.Enabled = (t_usun.TextLength != 0) && (listaUczniow.ZaznaczonyWiersz >= 0);
            });
        }

        private void obslugaPrzyciskuZapisz(object sender, EventArgs e)
        {
            b_zapisz.Enabled = ((t_imie_edytuj.TextLength != 0) && (t_nazwisko_edytuj.TextLength != 0) && (t_pesel_edytuj.TextLength != 0) && (listaUczniow.ZaznaczonyWiersz >= 0));
        }
        private void obslugaPrzyciskuDodaj(object sender, EventArgs e)
        {
            b_dodaj.Enabled = ((t_imie_dodaj.Text.Length != 0) && (t_nazwisko_dodaj.Text.Length != 0) && (t_pesel_dodaj.Text.Length != 0));
        }
        
        private void fWidokUcznia_Load(object sender, EventArgs e)
        {
            this.Text = "Edycja uczniow dla klasy: " + new klasa(klasaNR).Nazwa + "(" + new klasa(klasaNR).Rocznik + ")";
            listaUczniow = new ListaUczniow(this, dgv_lista, this.klasaNR);
            ustawZdarzenia();
            listaUczniow.odswiezDGV(t_imie_dodaj, b_usun1);
        }

        private void b_dodaj_Click(object sender, EventArgs e)
        {
            uczen u = new uczen
            {
                KlasaNR = this.klasaNR,
                Imie = t_imie_dodaj.Text,
                Nazwisko = t_nazwisko_dodaj.Text,
                Pesel = t_pesel_dodaj.Text,
                Email = t_email_dodaj.Text,
                Telefon_ucznia = t_nrUcznia_dodaj.Text,
                Telefon_rodzica = t_nrRodzica_dodaj.Text
            };
            try
            {
                u.dodajDoBazy();
            }
            catch(Exception)
            {
                MessageBox.Show("imie i nazwisko musi byc unikalne w klasie");
            }
            listaUczniow.odswiezDGV(t_imie_dodaj, t_nazwisko_dodaj, t_pesel_dodaj, t_email_dodaj, t_nrUcznia_dodaj, t_nrRodzica_dodaj);
        }

        private void b_zapisz_Click(object sender, EventArgs e)
        {
            listaUczniow.zbior[listaUczniow.ZaznaczonyWiersz].Imie = t_imie_edytuj.Text;
            listaUczniow.zbior[listaUczniow.ZaznaczonyWiersz].Nazwisko = t_nazwisko_edytuj.Text;
            listaUczniow.zbior[listaUczniow.ZaznaczonyWiersz].Email = t_email_edytuj.Text;
            listaUczniow.zbior[listaUczniow.ZaznaczonyWiersz].Telefon_ucznia = t_nrUcznia_edytuj.Text;
            listaUczniow.zbior[listaUczniow.ZaznaczonyWiersz].Telefon_rodzica = t_nrRodzica_edytuj.Text;
            try
            {
                listaUczniow.zbior[listaUczniow.ZaznaczonyWiersz].aktualizuj("*");
                listaUczniow.odswiezDGV();
            }
            catch (Exception)
            {
                MessageBox.Show("imie i nazwisko danego ucznia w klasie musi być unikalne");
            }
        }

        private void b_usun1_Click(object sender, EventArgs e)
        {
            klasa k = new klasa(this.klasaNR);
            nauczyciel n = new nauczyciel(k.NauczycielNR);
            if (t_usun.Text == n.Haslo)
            {
                listaUczniow.zbior[listaUczniow.ZaznaczonyWiersz].usun();
                listaUczniow.odswiezDGV(t_usun, t_imie_edytuj, t_nazwisko_edytuj,t_email_edytuj,t_pesel_edytuj,t_nrUcznia_edytuj, t_nrRodzica_edytuj);
            }
            else
            {
                MessageBox.Show("zle haslo!");
            }
        }


    }
}
