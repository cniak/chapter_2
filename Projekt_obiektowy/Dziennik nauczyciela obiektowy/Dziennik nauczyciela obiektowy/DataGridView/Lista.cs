using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Dziennik_nauczyciela_obiektowy
{
    public abstract class Lista<T>
    {
        public List<T> zbior;
        protected DataGridView dgv = null;
        protected int zaznaczonyWiersz = -1;
        protected Form f;

        public Lista(Form f, DataGridView dgv)
        {
            this.dgv = dgv;
            this.f = f;
            dgv.BorderStyle = BorderStyle.None;
            dgv.AllowUserToAddRows = false;
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv.SelectionChanged += new EventHandler(zmianaWiersza);
            ustawCzyWidocznyDGV();
        }
        public DataGridView Dgv
        {
            get
            {
                return dgv;
            }
        }
        public int ZaznaczonyWiersz
        {
            get
            {
                return zaznaczonyWiersz;
            }
            set
            {
                zaznaczonyWiersz = value;
            }
        }
        /// <summary>
        /// przez podawane parametry ustawiane sa czy: maja byc widoczne(label), aktywne(button), zresetowane (textBox)
        /// </summary>
        public void odswiezDGV(params object[] elementy)
        {
            wczytajListe();
            wczytajWiersze();
            List<Button> lB = new List<Button>();
            List<Label> lL = new List<Label>();
            List<TextBox> lT = new List<TextBox>();
            foreach (object element in elementy)
            {
                Button b = element as Button;
                if (b != null)
                {
                    lB.Add(b);
                    continue;
                }
                Label l = element as Label;
                if (l != null)
                {
                    lL.Add(l);
                    continue;
                }
                TextBox t = element as TextBox;
                if (t != null)
                {
                    lT.Add(t);
                    continue;
                }
            }
            for (int i = 0; i < lB.Count; i++)
            {
                lB[i].Enabled = zbior.Count != 0;
            }
            for (int i = 0; i < lL.Count; i++)
            {
                lL[i].Visible = zbior.Count == 0;
            }
            for (int i = 0; i < lT.Count; i++)
            {
                lT[i].Text = string.Empty;
            }
        }
        protected abstract void tworzKolumny();
        protected abstract void wczytajListe();
        protected abstract void wczytajWiersze();
        protected void usunWiersze()
        {
            while (dgv.Rows.Count != 0) dgv.Rows.RemoveAt(dgv.Rows.Count - 1);
        }
        /// <summary>
        /// zdarzenie na zmiane wiersza
        /// </summary>
        protected void zmianaWiersza(object sender, EventArgs e)
        {
            if (dgv.RowCount != 0)
            {
                try
                {
                    zaznaczonyWiersz = dgv.CurrentRow.Index;
                }
                catch
                {
                    MessageBox.Show(".");   
                }
            }
        }

        /// <summary>
        /// jezeli ilosc wierszy = 0, nie wyswietla
        /// </summary>
        protected void ustawCzyWidocznyDGV()
        {
            dgv.Visible = (dgv.RowCount != 0);
        }

        /// <summary>
        /// zdarzenie na podwojne klikniecie
        /// </summary>
        protected abstract void doubleClick(object sender, EventArgs e);
        /// <summary>
        /// ustawia zawsze widoczne nazwykolumny
        /// </summary>
        protected void ustawZawszeWidoczneNazwyKolumny()
        {
            for (int i = 0; i < dgv.Columns.Count; i++)
            {
                dgv.Columns[i].Frozen = true;
                DataGridViewCellStyle style = new DataGridViewCellStyle();
                style.BackColor = Color.WhiteSmoke;
                dgv.Columns[i].DefaultCellStyle = style;
            }
        }
    }
}
