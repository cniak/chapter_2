using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Dziennik_nauczyciela_obiektowy
{
    public class ListaPrzedmiotow : Lista<przedmiot>
    {
        private int klasaNR = -1;
        public ListaPrzedmiotow(Form f, DataGridView dgv, int klasaID) : base(f, dgv)
        {
            dgv.DoubleClick += new System.EventHandler(doubleClick);
            this.klasaNR = klasaID;
            wczytajListe();
            tworzKolumny();
            zmianaWiersza(new object(), new EventArgs());
            odswiezDGV();
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

        protected override void wczytajListe()
        {
            zbior = przedmiot.pobierzWszystkich(klasaNR);
        }

        protected override void wczytajWiersze()
        {
            usunWiersze();
            foreach (przedmiot p in zbior)
            {
                DataGridViewRow row = (DataGridViewRow)dgv.RowTemplate.Clone();
                row.CreateCells(dgv, p.PrzedmiotID, p.Nazwa);
                dgv.Rows.Add(row);
            }
            ustawCzyWidocznyDGV();
        }

        protected override void doubleClick(object sender, EventArgs e)
        {
            //nic
        }
    }
}
