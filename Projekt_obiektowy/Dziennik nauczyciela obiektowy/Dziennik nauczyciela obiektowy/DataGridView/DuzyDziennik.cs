using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Dziennik_nauczyciela_obiektowy
{
    public class DuzyDziennik : Lista<uczen_na_lekcji>
    {
        //ListaUczniow listaUczniow = null;
        //ListaDat listaDat = null;
        BackgroundWorker bw = null;
        ComboBox cb_miesiac = null;
        ComboBox cb_typ = null;
        ComboBox cb_przedmiot = null;
        List<uczen> listaUczniow = null;
        List<data> listaDat = null;
        klasa zalogowanaKlasa = null;
        //private ETypDanych typDanych;
        public DuzyDziennik(Form f, DataGridView dgv, ComboBox cb_miesiac, ComboBox cb_typ, ComboBox cb_przedmiot, klasa zalogowanaKlasa)
            : base(f, dgv)
        {
            bw = new BackgroundWorker();
            bw.DoWork +=bw_DoWork;
            bw.ProgressChanged +=bw_ProgressChanged;
            bw.RunWorkerCompleted +=bw_RunWorkerCompleted;

            bw.WorkerReportsProgress = true;
            bw.WorkerSupportsCancellation = true;
            this.zalogowanaKlasa = zalogowanaKlasa;

            listaUczniow = uczen.pobierzWszystkich(zalogowanaKlasa.KlasaID);
            listaDat = data.pobierzWszystkich(zalogowanaKlasa.KlasaID);

            this.dgv = dgv;
            this.dgv.ReadOnly = true;
            this.cb_miesiac = cb_miesiac;
            this.cb_przedmiot = cb_przedmiot;
            this.cb_typ = cb_typ;

            this.cb_miesiac.SelectedValueChanged += new EventHandler((o,e) => odswiezDGV());
            this.cb_przedmiot.SelectedValueChanged += new EventHandler((o, e) => odswiezDGV());
            this.cb_typ.SelectedIndexChanged += new EventHandler((o, e) => odswiezDGV());
            odswiezDGV();

        }

        private void bw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            //throw new NotImplementedException();
        }

        private void bw_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            dgv.Rows.Add((DataGridViewRow)e.UserState);
        }

        private void bw_DoWork(object sender, DoWorkEventArgs e)
        {
            // index 0 = przedmiot, index 1 = typ
            int[] przekazanaStruktura = (int[])e.Argument;
            
            for (int i = 0; i < listaUczniow.Count; i++)
            {
                DataGridViewRow row = new DataGridViewRow();
                object[] oc = new object[dgv.Columns.Count];
                oc[0] = listaUczniow[i].UczenID;
                oc[1] = listaUczniow[i].Imie + " " + listaUczniow[i].Nazwisko;
                for (int c = 2; c < dgv.ColumnCount; c++)
                {
                    int przedmiotID = przekazanaStruktura[0];
                    string dataKolumny = dgv.Columns[c].HeaderText.ToString() + " 00:00:00.000";
                    lekcja l = new lekcja(dataKolumny, przedmiotID, zalogowanaKlasa.KlasaID);
                    if (l.LekcjaID < 0)
                    {
                        l.dodajDoBazy();
                    }
                    uczen_na_lekcji unl = new uczen_na_lekcji(listaUczniow[i].UczenID, l.LekcjaID);
                    if (unl.Uczen_na_lekcjiID < 0)
                    {
                        unl = new uczen_na_lekcji
                        {
                            UczenNR = listaUczniow[i].UczenID,
                            LekcjaNR = l.LekcjaID,
                            Obecnosc = 0
                        };
                        try
                        {
                            unl.dodajDoBazy();
                        }
                        catch (Exception) {
                            //TODO rzuca jakims dziwnym wyjatkiem..
                        }
                    }
                    if ((ETypDanych)przekazanaStruktura[1] == ETypDanych.ocena)
                    {
                        //oc[c] = dataKolumny;
                        //oc[c] = unl.Uczen_na_lekcjiID + " " + unl.LekcjaNR + " " + przedmiotID + " " + unl.UczenNR;
                        if (unl.Ocena > 0) oc[c] = unl.Ocena;
                    }
                    else
                    {
                        oc[c] = (unl.Obecnosc != 0);
                    }
                }
                row.CreateCells(dgv, oc);
                bw.ReportProgress(i, row);
            }
        }
        private void usunKolumny()
        {
            while(dgv.Columns.Count != 0) dgv.Columns.RemoveAt(dgv.Columns.Count - 1);
        }
        protected override void ustawCzyWidocznyDGV()
        {
            dgv.Visible = true;
        }
        protected override void tworzKolumny()
        {
            DateTime wybranyMiesiacRok = (DateTime)cb_miesiac.SelectedValue;
            int miesiac = wybranyMiesiacRok.Month;
            int rok = -1;
            try
            {
                rok = wybranyMiesiacRok.Year;
                if (rok < 100) throw new Exception();
            }
            catch (Exception)
            {
                miesiac = -1;
            }

            DataGridViewColumn newCol = new DataGridViewColumn();
            DataGridViewCell cell = new DataGridViewTextBoxCell();
            newCol.CellTemplate = cell;
            newCol.HeaderText = "ID";
            newCol.Name = "ID";
            newCol.Visible = false;
            dgv.Columns.Add(newCol);

            newCol = new DataGridViewColumn();
            cell = new DataGridViewTextBoxCell();
            newCol.CellTemplate = cell;
            newCol.HeaderText = "imie i nazwisko";
            newCol.Name = "imie_i_nazwisko";
            newCol.Visible = true;
            newCol.ReadOnly = true;
            dgv.Columns.Add(newCol);

            if (miesiac <= 0)
            {

                for (int i = 0; i < listaDat.Count; i++)
                {
                    if ((ETypDanych)cb_typ.SelectedValue == ETypDanych.ocena)
                    {
                        newCol = new DataGridViewColumn();
                        cell = new DataGridViewTextBoxCell();
                        newCol.ReadOnly = false;
                        newCol.CellTemplate = cell;
                    }
                    else
                    {
                        newCol = new DataGridViewCheckBoxColumn();
                        DataGridViewCheckBoxCell c = new DataGridViewCheckBoxCell();
                        newCol.CellTemplate = c;
                    }

                    newCol.HeaderText = listaDat[i].Dzien.ToShortDateString();
                    newCol.Name = listaDat[i].Dzien.ToShortDateString();
                    newCol.Visible = true;
                    dgv.Columns.Add(newCol);
                }
            }
            else
            {
                List<DateTime> ld = new List<DateTime>();
                foreach (data d in listaDat) ld.Add(d.Dzien);
                List<DateTime> ldd = null;
                if (rok > 0) ldd = ld.Select(d => new DateTime(d.Year, d.Month, d.Day)).Where(d => d.Month == wybranyMiesiacRok.Month && d.Year == rok).ToList();
                else ldd = ld.Select(d => new DateTime(d.Year, d.Month, d.Day)).Where(d => d.Month == wybranyMiesiacRok.Month).ToList();
                foreach (DateTime d in ldd)
                {
                    if ((ETypDanych)cb_typ.SelectedValue == ETypDanych.ocena)
                    {
                        newCol = new DataGridViewColumn();
                        cell = new DataGridViewTextBoxCell();
                        newCol.ReadOnly = false;
                        newCol.CellTemplate = cell;
                    }
                    else
                    {
                        newCol = new DataGridViewCheckBoxColumn();
                        DataGridViewCheckBoxCell c = new DataGridViewCheckBoxCell();
                        newCol.CellTemplate = c;
                    }
                    newCol.HeaderText = d.ToShortDateString();
                    newCol.Name = d.ToShortDateString();
                    newCol.Visible = true;
                    dgv.Columns.Add(newCol);
                }
            }
            kolorujKolumny();
        }
        private void kolorujKolumny()
        {
            DateTime dzis = DateTime.Now;
            for (int i = 2; i < dgv.Columns.Count; i++)
            {
                DateTime dzien = new DateTime();
                dzien = DateTime.ParseExact(dgv.Columns[i].HeaderText.ToString(), "yyyy-MM-dd", null);
                if (dzien > dzis)
                {
                    dgv.Columns[i].DefaultCellStyle.BackColor = Color.Chartreuse;
                    dgv.Columns[i].ReadOnly = true;
                }
            }
        }
        public override void odswiezDGV(params object[] elementy)
        {
            usunWiersze();
            usunKolumny();
            tworzKolumny();
            if (cb_przedmiot.Items.Count == 0) return;
            if (!bw.IsBusy)
            {
                int[] strukturaDoWatku = new int[]{ Convert.ToInt32(cb_przedmiot.SelectedValue.ToString()), (int)cb_typ.SelectedValue };
                bw.RunWorkerAsync(strukturaDoWatku);
            }
            else
            {
                MessageBox.Show("poczekaj na wczytanie kolejki, dopiero zmien dane");
            }
        }
        protected override void wczytajListe()
        {
            throw new NotImplementedException();
        }

        protected override void wczytajWiersze()
        {
            throw new NotImplementedException();
        }

        protected override void doubleClick(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
