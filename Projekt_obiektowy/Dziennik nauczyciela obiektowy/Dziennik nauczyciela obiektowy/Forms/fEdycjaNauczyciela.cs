using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Dziennik_nauczyciela_obiektowy
{
    public partial class fEdycjaNauczyciela : Form
    {
        nauczyciel zalogowanyNauczyciel = null;
        public fEdycjaNauczyciela(nauczyciel zalogowanyNauczyciel)
        {
            this.zalogowanyNauczyciel = zalogowanyNauczyciel;
            zalogowanyNauczyciel.wylaczEdycje = true;
            InitializeComponent();
            t_ID.Text = zalogowanyNauczyciel.NauczycielID.ToString();
            t_ID.Enabled = false;
            t_login.Text = zalogowanyNauczyciel.Login;
            t_nazwisko.Text = zalogowanyNauczyciel.Nazwisko;
            t_email.Text = zalogowanyNauczyciel.Email;
            t_hasloEmail.Text = zalogowanyNauczyciel.Email_haslo;
            t_hasloUzytkownika.Text = zalogowanyNauczyciel.Haslo;
        }

        private void t_login_TextChanged(object sender, EventArgs e)
        {
            zalogowanyNauczyciel.Login = t_login.Text;
        }

        private void t_hasloUzytkownika_TextChanged(object sender, EventArgs e)
        {
            zalogowanyNauczyciel.Haslo = t_hasloUzytkownika.Text;
        }

        private void t_imie_TextChanged(object sender, EventArgs e)
        {
            zalogowanyNauczyciel.Imie = t_imie.Text;
        }

        private void t_nazwisko_TextChanged(object sender, EventArgs e)
        {
            zalogowanyNauczyciel.Nazwisko = t_nazwisko.Text;
        }

        private void t_email_TextChanged(object sender, EventArgs e)
        {
            zalogowanyNauczyciel.Email = t_email.Text;
        }

        private void t_hasloEmail_TextChanged(object sender, EventArgs e)
        {
            zalogowanyNauczyciel.Email_haslo = t_hasloEmail.Text;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            zalogowanyNauczyciel.wylaczEdycje = false;
            zalogowanyNauczyciel.aktualizuj("*");
        }

    }
}
