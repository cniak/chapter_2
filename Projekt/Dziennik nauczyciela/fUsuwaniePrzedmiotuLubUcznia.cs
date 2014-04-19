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
    public partial class fUsuwaniePrzedmiotuLubUcznia : Form
    {
        int klasaID = -1;
        cSQLite SQLite = null;
        string nazwaKolumny = string.Empty;
        string typDoUsuniecia = string.Empty;


        DataSet lista = null;
        DataTable tabelaListy = null;

        public fUsuwaniePrzedmiotuLubUcznia(string usuwanyTypDanych, int klasaID)
        {
            InitializeComponent();
            SQLite = new cSQLite();
            this.klasaID = klasaID;
            this.typDoUsuniecia = usuwanyTypDanych;
            if (this.typDoUsuniecia == "przedmiot")
            {
                nazwaKolumny = "nazwa";
            }
            else if (this.typDoUsuniecia == "uczen")
            {
                nazwaKolumny = "imie i nazwisko";
            }
            else
            {
                MessageBox.Show("zly string przekazany");
                this.Close();
            }
            this.dgv_lista.BackgroundColor = this.BackColor;
            wczytajListe();
            
        }
        void wczytajListe()
        {
            if (SQLite.sqliteConnection.State == ConnectionState.Closed) SQLite.sqliteConnection.Open();
            SQLite.sqliteCommand = SQLite.sqliteConnection.CreateCommand();
            string daneDoSQL = string.Empty;
            if (typDoUsuniecia == "uczen")
            {
                daneDoSQL = "imie, nazwisko";
            }
            else if (nazwaKolumny == "przedmiot")
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
                                              " FROM " + this.typDoUsuniecia +
                                              " WHERE klasaNR = " + this.klasaID + ";";
            SQLiteDataReader dataReader = SQLite.sqliteCommand.ExecuteReader();
            while (dataReader.Read())
            {
                DataRow drr = tabelaListy.NewRow();
                drr["ID"] = dataReader[typDoUsuniecia + "ID"].ToString();
                if (typDoUsuniecia == "przedmiot")
                {
                    drr[daneDoSQL] = dataReader[daneDoSQL].ToString();
                }
                else
                {
                    drr[daneDoSQL] = dataReader["imie"].ToString() + " " + dataReader["nazwisko"].ToString();
                }
                tabelaListy.Rows.Add(drr);
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
           // dgv_lista.Columns["ID"].Visible = false;

            int suma = 0;
            for (int i = 0; i < dgv_lista.Columns.Count; i++)
            {
                suma += dgv_lista.Columns[i].Width;
            }
            if (suma < dgv_lista.Size.Width)
            {
                dgv_lista.Columns[dgv_lista.Columns.Count - 1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }



            SQLite.sqliteConnection.Close();
        }
    }
}





