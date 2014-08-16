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
    public partial class fWidokPrzedmiotu : fWidokUczniaLubPrzedmiotu
    {
        ListaPrzedmiotow listaPrzedmiotow = null;
        klasa zalogowanaKlasa = null;
        ListaPrzedmiotow kopiaListy = null;
        public fWidokPrzedmiotu(klasa zalogowanaKlasa, ListaPrzedmiotow lP) : base(zalogowanaKlasa.KlasaID, typeof(przedmiot))
        {
            InitializeComponent();
            this.kopiaListy = lP;
            this.zalogowanaKlasa = zalogowanaKlasa;
            b_dodajPrzedmiot.Enabled = false;
        }

        private void ustawZdarzenia()
        {
            this.dgv_lista.SelectionChanged += new EventHandler((s, ee) =>
            {
                if (listaPrzedmiotow.Dgv.RowCount != 0)
                {
                    t_edytujNazwaPrzedmiotu.Text = listaPrzedmiotow.zbior[listaPrzedmiotow.ZaznaczonyWiersz].Nazwa;
                    b_usun1.Enabled = listaPrzedmiotow.Dgv.RowCount != 0;
                }
            });

            t_nazwaPrzedmiotu.TextChanged += new System.EventHandler((s, e) =>
            {
                b_dodajPrzedmiot.Enabled = t_nazwaPrzedmiotu.TextLength != 0;
            });
            t_edytujNazwaPrzedmiotu.TextChanged += new System.EventHandler((s, e) =>
            {
                b_zapiszZmiany.Enabled = t_edytujNazwaPrzedmiotu.TextLength != 0;
            });
            t_usun.TextChanged += new System.EventHandler((s, e) =>
            {
                b_usun1.Enabled = t_usun.TextLength != 0;
            });
        }
        private void b_dodajPrzedmiot_Click(object sender, EventArgs e)
        {
            przedmiot p = new przedmiot
            {
                Nazwa = t_nazwaPrzedmiotu.Text,
                KlasaNR = this.zalogowanaKlasa.KlasaID
            };
            try
            {
                p.dodajDoBazy();
                listaPrzedmiotow.odswiezDGV(t_nazwaPrzedmiotu);
                listaPrzedmiotow.odswiezListyWyboru();
            } catch(Exception ){
                MessageBox.Show("nazwa przedmiotu w klasie musi byc unikalna");
            }
            
        }
        private void fWidokPrzedmiotu_Load(object sender, EventArgs e)
        {
            this.Text = "Edycja przedmiotow dla klasy: " + zalogowanaKlasa.Nazwa + "(" + zalogowanaKlasa.Rocznik + ")";
            //listaPrzedmiotow = new ListaPrzedmiotow(this, dgv_lista, klasaNR, null);
            listaPrzedmiotow = new ListaPrzedmiotow(this, dgv_lista, zalogowanaKlasa.KlasaID, kopiaListy.SlownikListyWyboru);
            ustawZdarzenia();
            listaPrzedmiotow.odswiezDGV(t_nazwaPrzedmiotu,b_usun1);
        }
        private void b_zapiszZmiany_Click(object sender, EventArgs e)
        {
            listaPrzedmiotow.zbior[listaPrzedmiotow.ZaznaczonyWiersz].Nazwa = t_edytujNazwaPrzedmiotu.Text;
            try
            {
                listaPrzedmiotow.zbior[listaPrzedmiotow.ZaznaczonyWiersz].aktualizuj("*");
                listaPrzedmiotow.odswiezDGV();
                listaPrzedmiotow.odswiezListyWyboru();
            }
            catch (Exception)
            {
                MessageBox.Show("taki przedmiot juz istnieje w tej klasie");
            }
        }
        private void b_usun1_Click(object sender, EventArgs e)
        {
            nauczyciel n = new nauczyciel(zalogowanaKlasa.NauczycielNR);
            if (t_usun.Text == n.Haslo)
            {
                listaPrzedmiotow.zbior[listaPrzedmiotow.ZaznaczonyWiersz].usun();
                listaPrzedmiotow.odswiezDGV(t_usun);
                listaPrzedmiotow.odswiezListyWyboru();
            }
            else
            {
                MessageBox.Show("zle haslo!");
            }
        }
    }
}
