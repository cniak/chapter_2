using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Threading;
using System.Data.SQLite;

namespace Dziennik_nauczyciela_obiektowy
{
    public class DuzaLista : Lista<uczen_na_lekcji>
    {
        
        ListaPrzedmiotow listaPrzedmiotow = null;
        ListaUczniow listaUczniow = null;
        ListaDat listaDat = null;
        BackgroundWorker bw = null;
        private EtypDanych typDanych;
        /// <param name="listaUczniow">jak sie zmieni element tej listy to ma byc odswiezenie this.dgv</param>
        public DuzaLista(Form f, ListaUczniow listaUczniow, ListaPrzedmiotow listaPrzedmiotow, ListaDat listaDat, DataGridView dgv,EtypDanych etyp) : base(f,dgv)
        {
            bw = new BackgroundWorker();
            bw.DoWork += bw_DoWork;
            bw.ProgressChanged += bw_ProgressChanged;
            bw.RunWorkerCompleted += bw_RunWorkerCompleted;
            
            bw.WorkerReportsProgress = true;
            bw.WorkerSupportsCancellation = true;

            this.typDanych = etyp;
            this.dgv = dgv;
            usunKolumny();
            if (typDanych == EtypDanych.ocena)
            {                
                this.dgv.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_listaOcen_indywidualne_CellEndEdit);
            }
            else
            {
                this.dgv.CellContentClick += dgv_CellContentClick;
                this.dgv.CellValueChanged += dgv_CellValueChanged;
            }
            this.listaUczniow = listaUczniow;
            this.listaDat = listaDat;
            this.listaPrzedmiotow = listaPrzedmiotow;
            //this.listaDat.Mc.DateSelected += new DateRangeEventArgs(zmianaWierszaKalendarza);
            
            this.listaUczniow.Dgv.SelectionChanged += new EventHandler((o, e) =>
            {                
                odswiezDGV();
            });
            
            odswiezDGV();
        }

        public override void odswiezDGV(params object[] elementy)
        {
            if (bw.IsBusy)
            {
                MessageBox.Show("poczekaj na wczytanie kolejki, dopiero zmien");
                return;
            }
            usunWiersze();
            usunKolumny();
            tworzKolumny();
            if (!bw.IsBusy)
            {
                bw.RunWorkerAsync(listaUczniow.ZaznaczonyWiersz);
            }
            else
            {
                MessageBox.Show("poczekaj na wczytanie kolejki, dopiero zmien");
            }
            kolorujKolumny();
        }
        void bw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
        }

        void bw_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            dgv.Rows.Add((DataGridViewRow)e.UserState);
        }

        void bw_DoWork(object sender, DoWorkEventArgs e)
        {
            int zaznaczonyWierszUcznia = (int)e.Argument;
            for (int i = 0;i < listaPrzedmiotow.zbior.Count; i++)
            {
                DataGridViewRow row = new DataGridViewRow();
                //row.CreateCells(dgv, listaPrzedmiotow.zbior[i].PrzedmiotID, listaPrzedmiotow.zbior[i].Nazwa);
                //int c = 2;
                object[] oc = new object[dgv.Columns.Count];
                List<int> sum = new List<int>();
                oc[0] = listaPrzedmiotow.zbior[i].PrzedmiotID;
                oc[1] = listaPrzedmiotow.zbior[i].Nazwa;
                int iloscDostepnychKolumn = 0;
                //object[] srednia = new object[dgv.Columns.Count-1];
                
                for (int c = 2; c < dgv.Columns.Count-1; c++)
                {
                    int przedmiotID = listaPrzedmiotow.zbior[i].PrzedmiotID;
                    string dataKolumny = dgv.Columns[c].HeaderText.ToString() + " 00:00:00.000";
                    lekcja l = new lekcja(dataKolumny, przedmiotID, listaDat.KlasaNR);
                    if (l.LekcjaID < 0)
                    {
                        l.dodajDoBazy();
                    }
                    uczen_na_lekcji unl = new uczen_na_lekcji(listaUczniow.zbior[listaUczniow.ZaznaczonyWiersz].UczenID, l.LekcjaID);
                    if (unl.Uczen_na_lekcjiID < 0)
                    {
                        unl = new uczen_na_lekcji
                        {
                            UczenNR = listaUczniow.zbior[listaUczniow.ZaznaczonyWiersz].UczenID,
                            LekcjaNR = l.LekcjaID,
                            Obecnosc = 0
                        };
                        try
                        {
                            unl.dodajDoBazy();
                        }
                        catch (Exception)
                        {
                            //TODO rzuca jakims mega dziwnym wyjatkiem
                        }
                    }
                    if (typDanych == EtypDanych.ocena)
                    {
                        if (unl.Ocena > 0)
                        {
                            oc[c] = unl.Ocena;
                            if (dgv.Columns[c].DefaultCellStyle.BackColor != Color.Chartreuse)
                            {
                                sum.Add(unl.Ocena);
                                iloscDostepnychKolumn++;
                            }
                        }
                    }
                    else
                    {
                        oc[c] = (unl.Obecnosc != 0);
                        if (dgv.Columns[c].DefaultCellStyle.BackColor != Color.Chartreuse)
                        {
                            sum.Add((unl.Obecnosc != 0) ? 1 : 0);
                            iloscDostepnychKolumn++;
                        }
                    }
                }
                object sredniaLubFrekwencja = 0;
                if (typDanych == EtypDanych.ocena)
                {
                    sredniaLubFrekwencja = ((float)sum.Sum() / iloscDostepnychKolumn).ToString();
                }
                else
                {
                    sredniaLubFrekwencja = ((float)sum.Sum() / iloscDostepnychKolumn) * 100 + " %";
                }
                if(iloscDostepnychKolumn == 0) sredniaLubFrekwencja = " ";
                //oc[dgv.Columns.Count - 1] = obliczSrednia(oc,true);
                oc[dgv.Columns.Count - 1] = sredniaLubFrekwencja;
                row.CreateCells(dgv, oc);
                bw.ReportProgress(i, row);

            }
                 
        }
        private object obliczSrednia(int row)
        {
            int iloscDostepnychKolumn = 0;
            double suma = 0;
            for (int i = 1; i < dgv.Columns.Count - 1; i++)
            {
                if (dgv.Columns[i].DefaultCellStyle.BackColor == Color.Chartreuse) break;
                
                if (typDanych == EtypDanych.ocena)
                {

                    try
                    {
                        
                        int liczba = Convert.ToInt32(dgv[i, row].Value);
                        suma += liczba;
                        if(liczba > 0) iloscDostepnychKolumn++;
                    }
                    catch (Exception) { }
                }
                else
                {
                    int liczba = (Boolean.Equals(dgv[i+1,row].Value, true)) ? 1 : 0;
                    //MessageBox.Show("value = " + liczba.ToString() + "\n + c = " + i + ", r = " + row.ToString());
                    suma += Convert.ToDouble(liczba);
                    iloscDostepnychKolumn++;
                }
            }
            object sredniaFrekwencja = 0;
            if (typDanych == EtypDanych.ocena)
            {
                sredniaFrekwencja = (suma / iloscDostepnychKolumn).ToString();
            }
            else
            {
                iloscDostepnychKolumn--;
                sredniaFrekwencja = (suma / iloscDostepnychKolumn) * 100 + " %";
            }
            if (iloscDostepnychKolumn == 0) return ".";
            return sredniaFrekwencja;
        }
        private void usunKolumny()
        {
            while (this.dgv.Columns.Count > 0) this.dgv.Columns.RemoveAt(dgv.Columns.Count - 1);
        }

        void dgv_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            string dataKolumny = dgv.Columns[e.ColumnIndex].HeaderText.ToString() + " 00:00:00.000";
            int przedmiotID = (int)dgv[0, e.RowIndex].Value;
            lekcja l = new lekcja(dataKolumny, przedmiotID, listaDat.KlasaNR);
            uczen_na_lekcji unl = new uczen_na_lekcji(listaUczniow.zbior[listaUczniow.ZaznaczonyWiersz].UczenID, l.LekcjaID);
            unl.Obecnosc = (dgv[e.ColumnIndex, e.RowIndex].Value.ToString().ToUpper() == "TRUE") ? 1 : 0;
            //dgv[dgv.Columns.Count - 1, e.RowIndex].Value = obliczSrednia(e.RowIndex);
            
        }

        void dgv_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            dgv.CommitEdit(DataGridViewDataErrorContexts.Commit);
            //dgv[dgv.Columns.Count - 1, e.RowIndex].Value = obliczSrednia(new object[]{ e.RowIndex; },false);
            dgv[dgv.Columns.Count-1,e.RowIndex].Value = obliczSrednia(e.RowIndex);
        }


        public EtypDanych TypDanych
        {
            get
            {
                return typDanych;
            }
            set
            {
                typDanych = value;
            }
        }

        private void dgv_listaOcen_indywidualne_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            string dataKolumny = dgv.Columns[e.ColumnIndex].HeaderText.ToString() + " 00:00:00.000";
            int przedmiotID = (int)dgv[0, e.RowIndex].Value;
            lekcja l = new lekcja(dataKolumny, przedmiotID, listaDat.KlasaNR);
            uczen_na_lekcji unl = new uczen_na_lekcji(listaUczniow.zbior[listaUczniow.ZaznaczonyWiersz].UczenID, l.LekcjaID);
            //if (unl.Uczen_na_lekcjiID < 0) unl.dodajDoBazy();
            unl.wylaczEdycje = false;
            int tmp = -1;
            try
            {
                tmp = unl.Ocena;
                unl.Ocena = Convert.ToInt32(dgv[e.ColumnIndex, e.RowIndex].Value.ToString());
            }
            catch (Exception)
            {
                MessageBox.Show("wartosc musi byc z przedzialu <0,5> (0 = usuwanie oceny), akcja zostanie cofnieta");
                unl.Ocena = tmp;
                dgv[e.ColumnIndex, e.RowIndex].Value = unl.Ocena;
            }
            
            if (unl.Ocena == 0) dgv[e.ColumnIndex, e.RowIndex].Value = string.Empty;
            dgv[dgv.Columns.Count - 1, e.RowIndex].Value = obliczSrednia(e.RowIndex).ToString();
        }
        protected override void ustawCzyWidocznyDGV()
        {
            this.dgv.Visible = true;
        }
        protected override void tworzKolumny()
        {
            DateTime wybranyMiesiacRok = (DateTime)listaDat.Cb_miesiace.SelectedValue;
            //this.close
            int miesiac = wybranyMiesiacRok.Month;
            int rok = -1;
            try
            {
                rok = wybranyMiesiacRok.Year;
                if (rok < 1000) throw new Exception();
            }
            catch (Exception)
            {
                miesiac = -1;
            } 

            //typDanych = EtypDanych.obecnosc;
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
            newCol.HeaderText = "nazwa przedmiotu";
            newCol.Name = "nazwa_przedmiotu";
            newCol.Visible = true;
            newCol.ReadOnly = true;
            dgv.Columns.Add(newCol);
            if (miesiac <= 0)
            {
                for (int i = 0; i < listaDat.zbior.Count; i++)
                {
                    if (typDanych == EtypDanych.ocena)
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


                    newCol.HeaderText = listaDat.zbior[i].Dzien.ToShortDateString();
                    newCol.Name = listaDat.zbior[i].Dzien.ToShortDateString();
                    newCol.Visible = true;
                    dgv.Columns.Add(newCol);
                }
            }
            else
            {
                List<DateTime> ld = new List<DateTime>();
                foreach (data d in listaDat.zbior) ld.Add(d.Dzien);
                List<DateTime> ldd = null;
                if (rok > 0) ldd = ld.Select(d => new DateTime(d.Year, d.Month, d.Day)).Where(d => d.Month == wybranyMiesiacRok.Month && d.Year == rok).ToList();
                else ldd = ld.Select(d => new DateTime(d.Year, d.Month, d.Day)).Where(d => d.Month == wybranyMiesiacRok.Month).ToList();
                foreach (DateTime d in ldd)
                {
                    if (typDanych == EtypDanych.ocena)
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


            newCol = new DataGridViewColumn();
            cell = new DataGridViewTextBoxCell();
            newCol.ReadOnly = false;
            newCol.CellTemplate = cell;
            newCol.HeaderText = newCol.Name = (typDanych == EtypDanych.ocena) ? "srednia" : "frekwencja";
            newCol.Visible = true;
            newCol.ReadOnly = true;
            newCol.DefaultCellStyle.BackColor = Color.Beige;
            dgv.Columns.Add(newCol);



        }

        protected override void wczytajListe()
        {
            zbior = uczen_na_lekcji.pobierzWszystkich(listaDat.KlasaNR);
        }

        protected override void wczytajWiersze()
        {
            usunWiersze();
            foreach (przedmiot p in listaPrzedmiotow.zbior)
            {
                DataGridViewRow row = (DataGridViewRow)dgv.RowTemplate.Clone();
                row.CreateCells(dgv, p.PrzedmiotID, p.Nazwa);
                dgv.Rows.Add(row);
            }
            
            for (int r = 0; r < dgv.Rows.Count; r++)
            {
                for (int c = 2; c < dgv.Columns.Count-1; c++)
                {
                    int przedmiotID = (int)dgv[0,r].Value;
                    string dataKolumny = dgv.Columns[c].HeaderText.ToString() + " 00:00:00.000";
                    lekcja l = new lekcja(dataKolumny, przedmiotID, listaDat.KlasaNR);
                    if (l.LekcjaID < 0)
                    {
                        l.dodajDoBazy();
                    }

                    uczen_na_lekcji unl = new uczen_na_lekcji(listaUczniow.zbior[listaUczniow.ZaznaczonyWiersz].UczenID,l.LekcjaID);
                    if (unl.Uczen_na_lekcjiID < 0)
                    {
                        unl = new uczen_na_lekcji
                        {
                            UczenNR = listaUczniow.zbior[listaUczniow.ZaznaczonyWiersz].UczenID,
                            LekcjaNR = l.LekcjaID,
                            Obecnosc = 0,
                        };
                            unl.dodajDoBazy();
                    }
                    if (TypDanych == EtypDanych.ocena)
                    {
                        if (unl.Ocena > 0) dgv[c, r].Value = unl.Ocena;
                    }
                    else
                    {
                        dgv[c, r].Value = (unl.Obecnosc != 0);
                    }
                }
            }
        }

        private void kolorujKolumny()
        {
            DateTime dzis = DateTime.Now;
            for (int i = 2; i < dgv.Columns.Count-1; i++)
            {
                DateTime dzien = new DateTime();
                dzien = DateTime.ParseExact(dgv.Columns[i].HeaderText.ToString(), "yyyy-MM-dd",null);
                if (dzien > dzis)
                {
                    dgv.Columns[i].DefaultCellStyle.BackColor = Color.Chartreuse;
                    dgv.Columns[i].ReadOnly = true;
                }
            }
        }
        protected override void doubleClick(object sender, EventArgs e)
        {
        }
    }
}
