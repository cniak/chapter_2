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
            t_imie.Text = zalogowanyNauczyciel.Imie;
            t_login.Text = zalogowanyNauczyciel.Login;
            t_nazwisko.Text = zalogowanyNauczyciel.Nazwisko;
            
            int tmp = zalogowanyNauczyciel.Email.LastIndexOf('@');
            t_email.Text = (tmp > 0) ?zalogowanyNauczyciel.Email.Substring(0, tmp) : zalogowanyNauczyciel.Email;
            t_hasloEmail.Text = zalogowanyNauczyciel.Email_haslo;
            t_hasloUzytkownika.Text = zalogowanyNauczyciel.Haslo;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (t_login.Text.Length != 0 && t_hasloUzytkownika.Text.Length != 0)
            {
                zalogowanyNauczyciel.wylaczEdycje = true;
                string tmpLogin = null;
                
                    tmpLogin = zalogowanyNauczyciel.Login;
                    zalogowanyNauczyciel.Login = t_login.Text;
                
                    zalogowanyNauczyciel.Haslo = t_hasloUzytkownika.Text;
                    zalogowanyNauczyciel.Imie = t_imie.Text;
                    zalogowanyNauczyciel.Nazwisko = t_nazwisko.Text;
                    zalogowanyNauczyciel.Email_haslo = t_hasloEmail.Text;
                    zalogowanyNauczyciel.Email = t_email.Text + @"@gmail.com";
                zalogowanyNauczyciel.wylaczEdycje = false;
                try
                {
                    zalogowanyNauczyciel.aktualizuj("*");
                }
                catch (Exception)
                {
                    zalogowanyNauczyciel.Login = tmpLogin;
                    t_login.Text = tmpLogin;
                    MessageBox.Show("login musi byc unikalny");
                }
                zalogowanyNauczyciel.wylaczEdycje = true;
            }
            else
            {
                MessageBox.Show("Login i haslo nie moga byc puste");
            }
        }
    }
}
