using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Dziennik_nauczyciela_obiektowy
{

    public partial class fStart : Form
    {
        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true, CallingConvention = CallingConvention.Winapi)]
        public static extern short GetKeyState(int keyCode);
        private cSQLite SQLite = null;
        private ListaNauczycieli listaNauczycieli;
        public fStart()
        {
            InitializeComponent();
            SQLite = new cSQLite();
            SQLite.tworzBaze();
            sprawdzDaneLogowania(new object(), new EventArgs());

            t_nazwaUzytkownika.TextChanged  += new System.EventHandler(sprawdzDaneLogowania);
            t_haslo.TextChanged             += new System.EventHandler(sprawdzDaneLogowania);
        }

        private void fStart_Load(object sender, EventArgs e)
        {
            //Debug.WriteLine("The product name is Field");
            this.Text = "Logowanie do systemu";
            listaNauczycieli = new ListaNauczycieli(this, this.dgv_listaNauczycieli);
            listaNauczycieli.Dgv.BackgroundColor = this.BackColor;
            listaNauczycieli.odswiezDGV(b_usun);
            t_haslo.PasswordChar = '\u25CF';

        }

        private void b_dodaj_Click(object sender, EventArgs e)
        {
            nauczyciel n = new nauczyciel
            {
                Login = t_nazwaUzytkownika.Text,
                Haslo = t_haslo.Text,
                ZalogowanyMail = 0
            };
            if ((n.Login.Length < 5) || (n.Login.Length > 10))
            {
                MessageBox.Show("Login musi miec 5-10 znakow");
                return;
            }

            if ((n.Haslo.Length < 5) || (n.Haslo.Length > 15))
            {
                MessageBox.Show("Haslo musi miec 5-15 znakow");
                return;
            }
            try
            {
                n.dodajDoBazy();
                listaNauczycieli.odswiezDGV(t_nazwaUzytkownika, t_haslo,b_usun);
            }
            catch (Exception)
            {
                MessageBox.Show("Nazwa uzytkownika jest juz uzywana");
            }
        }

        /// <summary>
        /// ustawia button na aktywny gdy login i haslo jest wpisane
        /// </summary>
        public void sprawdzDaneLogowania(object sender, EventArgs e)
        {
            b_dodaj.Enabled = (t_nazwaUzytkownika.Text.Length != 0) && (t_haslo.Text.Length != 0);
            l_capslock.Visible = (((ushort)GetKeyState(0x14)) & 0xffff) != 0;
        }
        
        private void b_usun_Click(object sender, EventArgs e)
        {
            if (listaNauczycieli.zbior[listaNauczycieli.ZaznaczonyWiersz].usun() == true)
            {
                listaNauczycieli.odswiezDGV();
            }
        }
    }
}
