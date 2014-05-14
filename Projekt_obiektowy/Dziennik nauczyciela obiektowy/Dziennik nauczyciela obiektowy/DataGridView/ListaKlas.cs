using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Dziennik_nauczyciela_obiektowy
{
    public class ListaKlas : Lista<klasa>
    {
        private int nauczycielNR = -1;
        public ListaKlas(Form f, DataGridView dgv, int nauczycielID) : base(f,dgv)
        {
            this.nauczycielNR = nauczycielID;
            dgv.DoubleClick += new System.EventHandler(doubleClick);
            wczytajListe();
            tworzKolumny();
            wczytajWiersze();
            zmianaWiersza(new object(), new EventArgs());
        }

        

        protected override void wczytajListe()
        {
            zbior = klasa.pobierzWszystkich(nauczycielNR);
        }

        protected override void wczytajWiersze()
        {
            usunWiersze();
            foreach (klasa k in zbior)
            {
                DataGridViewRow row = (DataGridViewRow)dgv.RowTemplate.Clone();
                row.CreateCells(dgv, k.KlasaID, k.Nazwa);
                dgv.Rows.Add(row);
            }
            ustawCzyWidocznyDGV();
        }

        protected override void tworzKolumny()
        {
            DataGridViewColumn newCol = new DataGridViewColumn();
            DataGridViewCell cell = new DataGridViewTextBoxCell();
            newCol.CellTemplate = cell;

            newCol.HeaderText = "ID";
            newCol.Name = "ID";
            newCol.Visible = true;
            dgv.Columns.Add(newCol);
            dgv.Columns[0].Visible = false;

            newCol = new DataGridViewColumn();
            cell = new DataGridViewTextBoxCell();
            newCol.CellTemplate = cell;

            newCol.HeaderText = "nazwa";
            newCol.Name = "nazwa";
            newCol.Visible = true;
            dgv.Columns.Add(newCol);
            dgv.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            ustawZawszeWidoczneNazwyKolumny();
        }

        protected override void doubleClick(object sender, EventArgs e)
        {
            
            zbior[ZaznaczonyWiersz].zaloguj(f);
        }
    }
}
