using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Dziennik_nauczyciela_obiektowy
{
    public class ListaUczniow : Lista<uczen>
    {
        private int klasaNR = -1;

        public ListaUczniow(Form f, DataGridView dgv, int klasaID) : base(f,dgv)
        {
            this.klasaNR = klasaID;
            this.dgv = dgv;
            dgv.DoubleClick += new System.EventHandler(doubleClick);
            wczytajListe();
            tworzKolumny();
            wczytajWiersze();
            zmianaWiersza(new object(), new EventArgs());
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

            newCol.HeaderText = "imie i nazwisko";
            newCol.Name = "imie_i_nazwisko";
            newCol.Visible = true;
            dgv.Columns.Add(newCol);
            dgv.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            ustawZawszeWidoczneNazwyKolumny();
        }

        protected override void wczytajListe()
        {
            zbior = uczen.pobierzWszystkich(this.klasaNR);
        }

        protected override void wczytajWiersze()
        {
            usunWiersze();
            foreach (uczen u in zbior)
            {
                DataGridViewRow row = (DataGridViewRow)dgv.RowTemplate.Clone();
                row.CreateCells(dgv, u.UczenID, u.Imie + " " + u.Nazwisko);
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
