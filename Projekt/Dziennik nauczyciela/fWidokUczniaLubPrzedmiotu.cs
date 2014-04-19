using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SQLite;

namespace Dziennik_nauczyciela
{
    public partial class fWidokUczniaLubPrzedmiotu : Form
    {


        protected int klasaID = -1;
        protected cSQLite SQLite = null;
        protected string nazwaKolumny = string.Empty;
        protected string typDanych = string.Empty;

        protected int indexZaznaczonegoElementu = -1;
        protected int IDZaznaczonegoElementu = -1;

        protected DataSet lista = null;
        protected DataTable tabelaListy = null;

        public fWidokUczniaLubPrzedmiotu()
        {
            InitializeComponent();
            this.dgv_lista.BackgroundColor = this.BackColor;
            SQLite = new cSQLite();
            
            this.dgv_lista.BackgroundColor = this.BackColor;
        }
        /// <summary>
        /// jedynie w tej funkcji trzeba podac ID klasy, funkcja odpowiada za wczytanie zawartosci glownego layoutu, jest pewnego typu "konstruktorem"
        /// </summary>
        /// <param name="klasaID"></param>
        /// <param name="typDanych"></param>
        protected void wczytajDane(int klasaID, string typDanych)
        {
            this.klasaID = klasaID;
            this.typDanych = typDanych;
            if (typDanych == "przedmiot")
            {
                nazwaKolumny = "nazwa";
            }
            else if (typDanych == "uczen")
            {
                nazwaKolumny = "imie i nazwisko";
            }
            else
            {
                MessageBox.Show("zly string przekazany");
                this.Close();
            }
        }
        /// <summary>
        /// wczytuje datagridview
        /// </summary>
        protected void wczytajListe()
        {
            czyszczenieListy();
            if (SQLite.sqliteConnection.State == ConnectionState.Closed) SQLite.sqliteConnection.Open();
            SQLite.sqliteCommand = SQLite.sqliteConnection.CreateCommand();
            string daneDoSQL = string.Empty;
            if (typDanych == "uczen")
            {
                daneDoSQL = "imie, nazwisko";
            }
            else if (typDanych == "przedmiot")
            {
                daneDoSQL = "nazwa";
            }
            {
                daneDoSQL = nazwaKolumny;
            }
            //tworzenie kolumn
            this.lista = new DataSet();
            this.tabelaListy = new DataTable("lista");
            lista.Tables.Add(tabelaListy);

            tabelaListy.Columns.Add("ID", typeof(int));
            tabelaListy.Columns.Add(daneDoSQL, typeof(string));



            SQLite.sqliteCommand.CommandText = "SELECT *" +
                                              " FROM " + this.typDanych +
                                              " WHERE klasaNR = " + this.klasaID + ";";
            SQLiteDataReader dataReader = SQLite.sqliteCommand.ExecuteReader();
            int counter = 0;
            while (dataReader.Read())
            {
                DataRow drr = tabelaListy.NewRow();
                drr["ID"] = dataReader[typDanych + "ID"].ToString();
                if (typDanych == "przedmiot")
                {
                    drr[daneDoSQL] = dataReader[daneDoSQL].ToString();
                }
                else
                {
                    drr[daneDoSQL] = dataReader["imie"].ToString() + " " + dataReader["nazwisko"].ToString();
                }
                tabelaListy.Rows.Add(drr);
                counter++;
            }
            if (counter == 0)
            {
                dgv_lista.Visible = false;
                return;
            }
            else
            {
                dgv_lista.Visible = true;
            }
            
            tabelaListy.AcceptChanges();

            this.dgv_lista.AutoGenerateColumns = false;
            DataGridViewTextBoxColumn col = new DataGridViewTextBoxColumn();
            col.DataPropertyName = "ID";
            col.Name = "ID";
            col.HeaderText = "ID";
            col.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.dgv_lista.Columns.Add(col);

            col = new DataGridViewTextBoxColumn();
            col.DataPropertyName = col.Name = col.HeaderText = daneDoSQL;
            col.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.dgv_lista.Columns.Add(col);

            this.dgv_lista.DataSource = lista.Tables["lista"];
            dgv_lista.Columns["ID"].Visible = false;
            
            dgv_lista.Columns[dgv_lista.Columns.Count - 1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            //dgv_lista.Rows[0].Selected = false;
            SQLite.sqliteConnection.Close();
        }
        private void czyszczenieListy()
        {
            while (dgv_lista.Columns.Count != 0) this.dgv_lista.Columns.RemoveAt(0);
        }
        protected void dgv_lista_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            indexZaznaczonegoElementu = e.RowIndex;
            if (e.RowIndex < 0)
            {
                IDZaznaczonegoElementu = -1;
            }
            else
            {
                IDZaznaczonegoElementu = Convert.ToInt32(dgv_lista["ID", indexZaznaczonegoElementu].Value);
            }
            sprawdzPotrzebneDaneDoUsuniecia();
            przedmiot_wczytajDaneDoEdycji();
            
        }
        private void t_usun_TextChanged(object sender, EventArgs e)
        {
            sprawdzPotrzebneDaneDoUsuniecia();
        }
        private void sprawdzPotrzebneDaneDoUsuniecia(){
            b_usun.Enabled = (t_usun.Text.Length != 0 && (indexZaznaczonegoElementu >= 0));
            l_zaznaczElementDoUsuniecia.Visible = !(indexZaznaczonegoElementu >= 0);
            if (t_usun.Text.Length == 0)
            {
                t_usun.BackColor = Color.PaleVioletRed;
            }
            else
            {
                t_usun.BackColor = Color.White;
            }
        }
        private void b_usun_Click(object sender, EventArgs e)
        {
            if (SQLite.sqliteConnection.State == ConnectionState.Closed) SQLite.sqliteConnection.Open();
            try
            {
                SQLite.sqliteCommand = SQLite.sqliteConnection.CreateCommand();
                SQLite.sqliteCommand.CommandText = "DELETE FROM " + typDanych +
                                                   " WHERE " + typDanych + "ID = " + IDZaznaczonegoElementu + ";";
                SQLite.sqliteCommand.ExecuteNonQuery();
                SQLite.sqliteConnection.Close();
                wczytajListe();
                t_usun.Text = string.Empty;
                sprawdzPotrzebneDaneDoUsuniecia();
                dgv_lista_CellClick(sender, new DataGridViewCellEventArgs(0,-1));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public virtual void przedmiot_wczytajDaneDoEdycji()
        {
            
        }

        protected void sprawdzCzyIndexPoprawny(Button zapiszZmiany)
        {
            zapiszZmiany.Enabled = (indexZaznaczonegoElementu > 0);
        }
    }



}
