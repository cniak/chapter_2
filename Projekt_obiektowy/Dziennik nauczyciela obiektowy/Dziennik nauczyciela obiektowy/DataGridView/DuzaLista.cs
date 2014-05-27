using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Dziennik_nauczyciela_obiektowy
{
    public class DuzaLista : Lista<uczen_na_lekcji>
    {
        
        ListaPrzedmiotow listaPrzedmiotow = null;
        ListaUczniow listaUczniow = null;
        ListaDat listaDat = null;
        private EtypDanych typDanych;
        /// <param name="listaUczniow">jak sie zmieni element tej listy to ma byc odswiezenie this.dgv</param>
        public DuzaLista(Form f, ListaUczniow listaUczniow, ListaPrzedmiotow listaPrzedmiotow, ListaDat listaDat, DataGridView dgv,EtypDanych etyp) : base(f,dgv)
        {
            this.typDanych = etyp;
            this.dgv = dgv;
            if (typDanych == EtypDanych.ocena)
            {
                this.dgv.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_listaOcen_indywidualne_CellEndEdit);
            }
            else
            {
                //this.dgv.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_listaObecnosci_indywidualne_CellValueChanged);
                //this.dgv.CurrentCellDirtyStateChanged += System.Windows.Forms.DataGridViewCellStateChangedEventHandler(this.dgv_listaObecnosci_indywidualne_CellValueChanged);
                //this.dgv.CurrentCellDirtyStateChanged += new EventHandler(dgv_CurrentCellDirtyStateChanged);
                //this.dgv.CellValueChanged += dgv_CellValueChanged;
                this.dgv.CellContentClick += dgv_CellContentClick;
                this.dgv.CellValueChanged += dgv_CellValueChanged;

            }
            
            this.listaUczniow = listaUczniow;
            this.listaDat = listaDat;
            this.listaPrzedmiotow = listaPrzedmiotow;
            //this.listaDat.Mc.DateSelected += new DateRangeEventArgs(zmianaWierszaKalendarza);
            
            this.listaUczniow.Dgv.SelectionChanged += new EventHandler((o, e) =>
            {
                //MessageBox.Show("zmienilem wiersz!");
                odswiezDGV();
            });
            tworzKolumny();
            wczytajWiersze();
            wczytajListe();
             
        }

        void dgv_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            string dataKolumny = dgv.Columns[e.ColumnIndex].HeaderText.ToString() + " 00:00:00.000";
            int przedmiotID = (int)dgv[0, e.RowIndex].Value;
            lekcja l = new lekcja(dataKolumny, przedmiotID, listaDat.KlasaNR);
            uczen_na_lekcji unl = new uczen_na_lekcji(listaUczniow.zbior[listaUczniow.ZaznaczonyWiersz].UczenID, l.LekcjaID);
            unl.Obecnosc = (dgv[e.ColumnIndex, e.RowIndex].Value.ToString().ToUpper() == "TRUE") ? 1 : 0;
            
        }

        void dgv_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            dgv.CommitEdit(DataGridViewDataErrorContexts.Commit);
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
            unl.Ocena = Convert.ToInt32(dgv[e.ColumnIndex, e.RowIndex].Value.ToString());
            if (unl.Ocena == 0) dgv[e.ColumnIndex, e.RowIndex].Value = string.Empty;
        }
        protected override void ustawCzyWidocznyDGV()
        {
            this.dgv.Visible = true;
        }
        protected override void tworzKolumny()
        {
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
            //newCol.ReadOnly = true;
            dgv.Columns.Add(newCol);

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

        protected override void wczytajListe()
        {
            zbior = uczen_na_lekcji.pobierzWszystkich(listaDat.KlasaNR);

        }

        protected override void wczytajWiersze()
        {
            usunWiersze();
            //MessageBox.Show("o:"+typDanych.ToString());
            foreach (przedmiot p in listaPrzedmiotow.zbior)
            {
                DataGridViewRow row = (DataGridViewRow)dgv.RowTemplate.Clone();
                row.CreateCells(dgv, p.PrzedmiotID, p.Nazwa);
                dgv.Rows.Add(row);
            }

            for (int r = 0; r < dgv.Rows.Count; r++)
            {
                for (int c = 2; c < dgv.Columns.Count; c++)
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
                        //dgv[c, r].Value = true;
                    }
                }
            }
        }

        protected override void doubleClick(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
