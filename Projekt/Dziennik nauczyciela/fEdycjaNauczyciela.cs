using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SQLite;
using System.Threading;

namespace Dziennik_nauczyciela
{
    public partial class fEdycjaNauczyciela : Form
    {
        cSQLite SQLite = null;
        BAZADANYCH.nauczyciel edytowanyNauczyciel = null;
        public fEdycjaNauczyciela(int nauczycielID, BAZADANYCH.nauczyciel nauczyciel)
        {
            InitializeComponent();
            this.t_hasloEmail.PasswordChar= '\u25CF';
            this.t_hasloUzytkownika.PasswordChar = '\u25CF';
            edytowanyNauczyciel = nauczyciel;
            SQLite = new cSQLite();
            ladowanieDanych(nauczycielID);
        }
        
        private void ladowanieDanych(int ID)
        {
            if (SQLite.sqliteConnection.State == ConnectionState.Closed) SQLite.sqliteConnection.Open();
            SQLite.sqliteCommand = SQLite.sqliteConnection.CreateCommand();
            SQLite.sqliteCommand.CommandText = "select * from nauczyciel where nauczycielID="+ ID +";";
            using (SQLiteDataReader dr = SQLite.sqliteCommand.ExecuteReader())
            {
                while (dr.Read())
                {
                    t_ID.Text = dr["nauczycielID"].ToString();
                    t_imie.Text = dr["imie"].ToString();
                    t_nazwisko.Text = dr["nazwisko"].ToString();
                    t_email.Text = dr["email"].ToString();
                    t_login.Text = dr["login"].ToString();
                    t_hasloUzytkownika.Text = dr["haslo"].ToString();
                    t_hasloEmail.Text = dr["email_haslo"].ToString();
                }
            }
            SQLite.sqliteConnection.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                
                if (SQLite.sqliteConnection.State == ConnectionState.Closed) SQLite.sqliteConnection.Open();
                SQLite.sqliteCommand = SQLite.sqliteConnection.CreateCommand();
                SQLite.sqliteCommand.CommandText = "UPDATE nauczyciel set " +
                                                   "login=@login," +
                                                   "haslo=@haslo," +
                                                   "imie=@imie," +
                                                   "nazwisko=@nazwisko," +
                                                   "email=@email, " +
                                                   "email_haslo=@email_haslo " +
                                                   "WHERE nauczycielID = " + Convert.ToInt32(t_ID.Text) + ";";
                SQLite.sqliteCommand.Parameters.AddWithValue("login", t_login.Text);
                SQLite.sqliteCommand.Parameters.AddWithValue("haslo", t_hasloUzytkownika.Text);
                SQLite.sqliteCommand.Parameters.AddWithValue("imie", t_imie.Text);
                SQLite.sqliteCommand.Parameters.AddWithValue("nazwisko", t_nazwisko.Text);
                SQLite.sqliteCommand.Parameters.AddWithValue("email", t_email.Text);
                SQLite.sqliteCommand.Parameters.AddWithValue("email_haslo", t_hasloEmail.Text);
                SQLite.sqliteCommand.Parameters.AddWithValue("ID", Convert.ToInt32(t_ID.Text));

                // taka dziwna edycja, nie chce mi sie juz poprawiac, moze kiedys :-)
                edytowanyNauczyciel.login = t_login.Text;
                edytowanyNauczyciel.haslo = t_hasloUzytkownika.Text;
                edytowanyNauczyciel.imie = t_imie.Text;
                edytowanyNauczyciel.nazwisko = t_nazwisko.Text;
                edytowanyNauczyciel.email = t_email.Text;
                edytowanyNauczyciel.email_haslo = t_hasloEmail.Text;

                SQLite.sqliteCommand.ExecuteNonQuery();
                SQLite.sqliteConnection.Close();
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
