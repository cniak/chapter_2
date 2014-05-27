using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Dziennik_nauczyciela_obiektowy
{
    public class ListaPrzedmiotow : Lista<przedmiot>
    {
        public enum eListaWyboru
        {
            
        }
        private int klasaNR = -1;
        private Dictionary<string, ComboBox> slownikListyWyboru;
        private List<string> nazwyListWyboru;
        /// <param name="slownikListyWyboru">przekazany slownik to slownik comboboxow, ktore beda zmieniane po aktualizacji daty</param>
        public ListaPrzedmiotow(Form f, DataGridView dgv, int klasaID,Dictionary<string,ComboBox> slownikListyWyboru) : base(f, dgv)
        {
            this.slownikListyWyboru = slownikListyWyboru;
            this.klasaNR = klasaID;
            wczytajListe();
            
            if (dgv != null)
            {
                dgv.DoubleClick += new System.EventHandler(doubleClick);
                tworzKolumny();
                odswiezDGV();
                zmianaWiersza(new object(), new EventArgs());
            }
            if (slownikListyWyboru != null)
            {
                nazwyListWyboru = new List<string>(slownikListyWyboru.Keys);
                /*
                foreach(KeyValuePair<string,ComboBox> cb in slownikListyWyboru)
                {
                    this.slownikListyWyboru[cb.Key].DisplayMember = "nazwa";
                    this.slownikListyWyboru[cb.Key].ValueMember = "przedmiotID";
                    this.slownikListyWyboru[cb.Key].DataSource = zbior;
                }
                 */
                for (int i = 0; i < nazwyListWyboru.Count; i++)
                {
                    this.slownikListyWyboru[nazwyListWyboru[i].ToString()].DisplayMember = "nazwa";
                    this.slownikListyWyboru[nazwyListWyboru[i].ToString()].ValueMember = "przedmiotID";
                    this.slownikListyWyboru[nazwyListWyboru[i].ToString()].DataSource = zbior;
                }
            }
        }
        public void odswiezListyWyboru()
        {
            for (int i = 0; i < nazwyListWyboru.Count; i++)
            {
                this.slownikListyWyboru[nazwyListWyboru[i].ToString()].DataSource = zbior;
            }
        }
        /// <summary>
        /// zawiera liste comboboxów w ktorych mają być wyświetlane przedmioty
        /// </summary>
        public Dictionary<string,ComboBox> SlownikListyWyboru
        {
            get
            {
                return slownikListyWyboru;
            }
            set
            {
            }
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
