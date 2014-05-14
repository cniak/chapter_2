using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Dziennik_nauczyciela_obiektowy
{
    public class ListaNauczycieli : Lista<nauczyciel>
    {

        /// <summary>
        /// tworzy odwolanie do obiektu DataGridView
        /// </summary>
        public ListaNauczycieli(Form f, DataGridView dgv) : base(f,dgv)
        {
            dgv.DoubleClick += new System.EventHandler(doubleClick);
            wczytajListe();
            tworzKolumny();
            wczytajWiersze();
            zmianaWiersza(new object(), new EventArgs());
        }

        /// <summary>
        /// tworzy kolumny dla Dgv
        /// </summary>
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

            newCol.HeaderText = "login";
            newCol.Name = "login";
            newCol.Visible = true;
            dgv.Columns.Add(newCol);
            dgv.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            ustawZawszeWidoczneNazwyKolumny();
            
        }
        /// <summary>
        /// wczytuje nauczycieli
        /// </summary>
        protected override void wczytajListe()
        {
            zbior = nauczyciel.pobierzWszystkich();
        }

        /// <summary>
        /// tworzy wiersze dla Dgv
        /// </summary>
        protected override void wczytajWiersze()
        {
        usunWiersze();
            foreach (nauczyciel n in zbior)
            {
                DataGridViewRow row = (DataGridViewRow)dgv.RowTemplate.Clone();
                row.CreateCells(dgv, n.NauczycielID, n.Login);
                dgv.Rows.Add(row);
            }
            ustawCzyWidocznyDGV();
        }

        /// <summary>
        /// zdarzenie na podwojne klikniecie
        /// </summary>
        protected override void doubleClick(object sender, EventArgs e)
        {
            zbior[ZaznaczonyWiersz].zaloguj(f);
        }

    }
}