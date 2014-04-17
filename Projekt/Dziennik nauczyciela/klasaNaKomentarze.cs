using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dziennik_nauczyciela
{
    class klasaNaKomentarze
    {
        public void fstart()
        {













            /*
        public partial class fStart : Form
        {
            cSQLite SQLite = null;   
            public fStart()
            {
                InitializeComponent();
            }

            private void fStart_Load(object sender, EventArgs e)
            {
                SQLite = new cSQLite();
                SQLite.tworzenieBazyDanych();
                try
                {
                    cosTamZNeta();
                    //wczytajListeUzytkownikow();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                //cosTamZNeta();
            }



            private void wczytajListeUzytkownikow()
            {

            }
            private void cosTamZNeta()
            {
                czyszczenieListyUzytkownikow();
                if (SQLite.sqliteConnection.State == ConnectionState.Closed) SQLite.sqliteConnection.Open();
                SQLite.sqliteCommand = SQLite.sqliteConnection.CreateCommand();
                SQLite.sqliteCommand.CommandText = "select * from nauczyciel";
                SQLiteDataReader dr = SQLite.sqliteCommand.ExecuteReader();
                // stworzenie listy nauczycieli
                List<BAZADANYCH.nauczyciel> listaUzytkownikow = new List<BAZADANYCH.nauczyciel>();

                while (dr.Read())
                {
                    listaUzytkownikow.Add(new BAZADANYCH.nauczyciel
                    {
                        nauczycielID = Convert.ToInt32(dr["nauczycielID"]),
                        imie = dr["imie"].ToString(),
                        nazwisko = dr["nazwisko"].ToString(),
                        email = dr["email"].ToString(),
                        login = dr["login"].ToString(),
                        haslo = dr["haslo"].ToString()
                    });
                }

                DataSet dsPlayerList = new DataSet();
                DataTable dt = new DataTable("PlayerList");
                dsPlayerList.Tables.Add(dt);

                dt.Columns.Add("ID", typeof(int));
                dt.Columns.Add("Name", typeof(string));
                dt.Columns.Add("Type", typeof(int));
                dt.Columns.Add("IP", typeof(string));

                for (int i = 0; i < listaUzytkownikow.Count; i++)
                {
                    DataRow drr = dt.NewRow();
                    drr["ID"] = listaUzytkownikow[i].nauczycielID;
                    drr["Name"] = listaUzytkownikow[i].login;
                    drr["Type"] = listaUzytkownikow[i].nauczycielID;
                    drr["IP"] = "HAHA " + listaUzytkownikow[i].nauczycielID;
                    dt.Rows.Add(drr);
                }
            
                dt.AcceptChanges();

                this.dgv_listaUzytkownikow.AutoGenerateColumns = false;

                DataGridViewTextBoxColumn col = new DataGridViewTextBoxColumn();
                col.DataPropertyName = "Name";
                col.Name = "Name";
                col.HeaderText = "Name";

                this.dgv_listaUzytkownikow.Columns.Add(col);

                col = new DataGridViewTextBoxColumn();
                col.DataPropertyName = "IP";
                col.Name = "HEH";
                col.HeaderText = "HEH";

                this.dgv_listaUzytkownikow.Columns.Add(col);

                this.dgv_listaUzytkownikow.DataSource = dsPlayerList.Tables["PlayerList"];    
            }
      */
            /*
            private void CalcColumns()
            {
                DataTable table = new DataTable();

                // Create the first column.
                DataColumn priceColumn = new DataColumn();
                priceColumn.DataType = System.Type.GetType("System.Decimal");
                priceColumn.ColumnName = "price";
                priceColumn.DefaultValue = 50;

                // Create the second, calculated, column.
                DataColumn taxColumn = new DataColumn();
                taxColumn.DataType = System.Type.GetType("System.Decimal");
                taxColumn.ColumnName = "tax";
                taxColumn.Expression = "price * 0.0862";

                // Create third column.
                DataColumn totalColumn = new DataColumn();
                totalColumn.DataType = System.Type.GetType("System.Decimal");
                totalColumn.ColumnName = "total";
                totalColumn.Expression = "price + tax";
            
                // Add columns to DataTable.
                table.Columns.Add(priceColumn);
                table.Columns.Add(taxColumn);
                table.Columns.Add(totalColumn);
            
                DataRow row = table.NewRow();
                table.Rows.Add(row);
                DataView view = new DataView(table);
                dgv_listaUzytkownikow.DataSource = view;
            }
            */
            /*
                    private void b_dodaj_Click(object sender, EventArgs e)
                    {
                        DataGridViewTextBoxColumn col = new DataGridViewTextBoxColumn();
                        col.DataPropertyName = "Name";
                        col.Name = "Name";
                        col.HeaderText = "Name";

                        dodajUzytkownika();
                    }

                    private void dodajUzytkownika()
                    {

                        try
                        {
                        BAZADANYCH.nauczyciel nowyNauczyciel = new BAZADANYCH.nauczyciel
                        {
                            login = t_nazwaUzytkownika.Text,
                            haslo = t_haslo.Text,
                            imie = " ",
                            nazwisko = " ",
                            email = " "
                        };

                        if (SQLite.sqliteConnection.State == ConnectionState.Closed) SQLite.sqliteConnection.Open();
                        SQLite.sqliteCommand = SQLite.sqliteConnection.CreateCommand();
                        SQLite.sqliteCommand.CommandText = "INSERT INTO nauczyciel (login, haslo, imie, nazwisko, email) VALUES (@login, @haslo, @imie, @nazwisko, @email);";
                        SQLite.sqliteCommand.Parameters.AddWithValue("login", nowyNauczyciel.login);
                        SQLite.sqliteCommand.Parameters.AddWithValue("haslo", nowyNauczyciel.haslo);
                        SQLite.sqliteCommand.Parameters.AddWithValue("imie", nowyNauczyciel.imie);
                        SQLite.sqliteCommand.Parameters.AddWithValue("nazwisko", nowyNauczyciel.nazwisko);
                        SQLite.sqliteCommand.Parameters.AddWithValue("email", nowyNauczyciel.email);

                            SQLite.sqliteCommand.ExecuteNonQuery();

                            cosTamZNeta();
                            MessageBox.Show("dodalem!" + nowyNauczyciel.login.ToString());
                        } catch(Exception ex){
                            MessageBox.Show(ex.Message);
                        }           
                    }

                    private void czyszczenieListyUzytkownikow()
                    {
                        while (dgv_listaUzytkownikow.Columns.Count != 0) this.dgv_listaUzytkownikow.Columns.RemoveAt(0);
                    }


                    //Dodawanie uzytkownika - sprawdzanie dlugosci i wlczanie przycisku
                    private void t_nazwaUzytkownika_TextChanged(object sender, EventArgs e)
                    {
                        b_dodaj.Enabled = sprawdzCzyWypelnioneDane();
                    }
                    private void t_haslo_TextChanged(object sender, EventArgs e)
                    {
                        b_dodaj.Enabled = sprawdzCzyWypelnioneDane();
                    }
                    private bool sprawdzCzyWypelnioneDane()
                    {
                        return (Convert.ToBoolean(t_nazwaUzytkownika.Text.Length) && Convert.ToBoolean(t_haslo.Text.Length));
                    }
                }
            }
                */

            /*
            private void cosTamZNeta()
                    {
                        DataSet dsPlayerList = new DataSet();
                        DataTable dt = new DataTable("PlayerList");
                        dsPlayerList.Tables.Add(dt);

                        dt.Columns.Add("ID", typeof(int));
                        dt.Columns.Add("Name", typeof(string));
                        dt.Columns.Add("Type", typeof(string));
                        dt.Columns.Add("IP", typeof(string));

                        for (int i = 0; i < 4; i++)
                        {
                            DataRow dr = dt.NewRow();
                            dr["ID"] = 1;
                            dr["Name"] = "Player 1";
                            dr["Type"] = "Standard";
                            dr["IP"] = "127.0.0.1";
                            dt.Rows.Add(dr);
                        }
            

                        dt.AcceptChanges();

                        this.dgv_listaUzytkownikow.AutoGenerateColumns = false;

                        DataGridViewTextBoxColumn col = new DataGridViewTextBoxColumn();
                        col.DataPropertyName = "Name";
                        col.Name = "Name";
                        col.HeaderText = "Name";

                        this.dgv_listaUzytkownikow.Columns.Add(col);

                        col = new DataGridViewTextBoxColumn();
                        col.DataPropertyName = "Type";
                        col.Name = "Type";
                        col.HeaderText = "Type";

                        this.dgv_listaUzytkownikow.Columns.Add(col);

                        this.dgv_listaUzytkownikow.DataSource = dsPlayerList.Tables["PlayerList"];    
                    }

            */
        }

        /*
           private void CalcColumns()
           {
               DataTable table = new DataTable();

               // Create the first column.
               DataColumn priceColumn = new DataColumn();
               priceColumn.DataType = System.Type.GetType("System.Decimal");
               priceColumn.ColumnName = "price";
               priceColumn.DefaultValue = 50;

               // Create the second, calculated, column.
               DataColumn taxColumn = new DataColumn();
               taxColumn.DataType = System.Type.GetType("System.Decimal");
               taxColumn.ColumnName = "tax";
               taxColumn.Expression = "price * 0.0862";

               // Create third column.
               DataColumn totalColumn = new DataColumn();
               totalColumn.DataType = System.Type.GetType("System.Decimal");
               totalColumn.ColumnName = "total";
               totalColumn.Expression = "price + tax";
            
               // Add columns to DataTable.
               table.Columns.Add(priceColumn);
               table.Columns.Add(taxColumn);
               table.Columns.Add(totalColumn);
            
               DataRow row = table.NewRow();
               table.Rows.Add(row);
               DataView view = new DataView(table);
               dgv_listaUzytkownikow.DataSource = view;
           }
           */



        /*
        private void cosTamZNeta()
                {
                    DataSet dsPlayerList = new DataSet();
                    DataTable dt = new DataTable("PlayerList");
                    dsPlayerList.Tables.Add(dt);

                    dt.Columns.Add("ID", typeof(int));
                    dt.Columns.Add("Name", typeof(string));
                    dt.Columns.Add("Type", typeof(string));
                    dt.Columns.Add("IP", typeof(string));

                    for (int i = 0; i < 4; i++)
                    {
                        DataRow dr = dt.NewRow();
                        dr["ID"] = 1;
                        dr["Name"] = "Player 1";
                        dr["Type"] = "Standard";
                        dr["IP"] = "127.0.0.1";
                        dt.Rows.Add(dr);
                    }
            

                    dt.AcceptChanges();

                    this.dgv_listaUzytkownikow.AutoGenerateColumns = false;

                    DataGridViewTextBoxColumn col = new DataGridViewTextBoxColumn();
                    col.DataPropertyName = "Name";
                    col.Name = "Name";
                    col.HeaderText = "Name";

                    this.dgv_listaUzytkownikow.Columns.Add(col);

                    col = new DataGridViewTextBoxColumn();
                    col.DataPropertyName = "Type";
                    col.Name = "Type";
                    col.HeaderText = "Type";

                    this.dgv_listaUzytkownikow.Columns.Add(col);

                    this.dgv_listaUzytkownikow.DataSource = dsPlayerList.Tables["PlayerList"];    
                }

        */

    }
}




/*
private void cosTamZNeta()
        {
            DataSet dsPlayerList = new DataSet();
            DataTable dt = new DataTable("PlayerList");
            dsPlayerList.Tables.Add(dt);

            dt.Columns.Add("ID", typeof(int));
            dt.Columns.Add("Name", typeof(string));
            dt.Columns.Add("Type", typeof(string));
            dt.Columns.Add("IP", typeof(string));

            for (int i = 0; i < 4; i++)
            {
                DataRow dr = dt.NewRow();
                dr["ID"] = 1;
                dr["Name"] = "Player 1";
                dr["Type"] = "Standard";
                dr["IP"] = "127.0.0.1";
                dt.Rows.Add(dr);
            }
            

            dt.AcceptChanges();

            this.dgv_listaUzytkownikow.AutoGenerateColumns = false;

            DataGridViewTextBoxColumn col = new DataGridViewTextBoxColumn();
            col.DataPropertyName = "Name";
            col.Name = "Name";
            col.HeaderText = "Name";

            this.dgv_listaUzytkownikow.Columns.Add(col);

            col = new DataGridViewTextBoxColumn();
            col.DataPropertyName = "Type";
            col.Name = "Type";
            col.HeaderText = "Type";

            this.dgv_listaUzytkownikow.Columns.Add(col);

            this.dgv_listaUzytkownikow.DataSource = dsPlayerList.Tables["PlayerList"];    
        }

*/