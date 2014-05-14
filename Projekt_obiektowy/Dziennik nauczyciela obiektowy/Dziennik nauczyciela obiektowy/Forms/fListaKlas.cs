using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace Dziennik_nauczyciela_obiektowy.Forms
{
    public partial class fListaKlas : Form
    {
        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true, CallingConvention = CallingConvention.Winapi)]
        public static extern short GetKeyState(int keyCode);
        [DllImport("wininet.dll")]
        private extern static bool InternetGetConnectedState(out int conn, int val);
        nauczyciel zalogowanyNauczyciel = null;
        ListaKlas listaKlas = null;
        public fListaKlas(nauczyciel zalogowanyNauczyciel)
        {
            this.zalogowanyNauczyciel = zalogowanyNauczyciel;
            InitializeComponent();
            b_dodaj.Enabled = sprawdzenieCzyWypelnioneDane();
            this.Text = "Wybor klasy (" + zalogowanyNauczyciel.Login + ")";
            this.t_hasloMail.PasswordChar = '\u25CF';
            this.t_loginMail.Text = zalogowanyNauczyciel.Email;
            this.t_hasloMail.Text = zalogowanyNauczyciel.Email_haslo;
            this.dgv_listaKlas.BackgroundColor = this.BackColor;
            this.dgv_listaKlas.BorderStyle = BorderStyle.None;
            this.gb_mailZalogowany.Visible = false;
            this.gb_powiazanieKontaZPoczta.Visible = false;
            this.b_usunKlase.Enabled = false;
            if (!bw_polaczZMailem.IsBusy)
            {
                bw_polaczZMailem.RunWorkerAsync();
            }
        }

        private void fListaKlas_Load(object sender, EventArgs e)
        {
            listaKlas = new ListaKlas(this, dgv_listaKlas, zalogowanyNauczyciel.NauczycielID);
            listaKlas.odswiezDGV(b_usunKlase);
        }
        private bool sprawdzenieCzyWypelnioneDane()
        {
            return ((t_nazwa.Text.Length != 0) && (t_rocznik1.Text.Length != 0) && (l_rocznikInfo.Visible == false));
        } 
        private void obslugaPrzyciskuDodajKlase()
        {
            t_nazwa.TextChanged += ((o, e) =>
            {
                b_dodaj.Enabled = sprawdzenieCzyWypelnioneDane();
                s_statusCapslock.Visible = (((ushort)GetKeyState(0x14)) & 0xffff) != 0;
            });

            t_rocznik1.TextChanged += ((o, e) =>
            {
                b_dodaj.Enabled = sprawdzenieCzyWypelnioneDane();
                s_statusCapslock.Visible = (((ushort)GetKeyState(0x14)) & 0xffff) != 0;
            });

        }
        private void b_dodaj_Click(object sender, EventArgs e)
        {
            klasa k = new klasa
            {
                Nazwa = t_nazwa.Text,
                NauczycielNR = this.zalogowanyNauczyciel.NauczycielID,
                Rocznik = t_rocznik1.Text + @"/" + t_rocznik2.Text,
                GospodarzNR = -1
            };
            k.dodajDoBazy();
            t_nazwa.Text = t_rocznik1.Text = t_rocznik2.Text = string.Empty;
            listaKlas.odswiezDGV(b_usunKlase);
        }

        private void b_usunKlase_Click(object sender, EventArgs e)
        {
            if (listaKlas.zbior[listaKlas.ZaznaczonyWiersz].usun() == true)
            {
                listaKlas.odswiezDGV(b_usunKlase);
            }
        }

        private void b_edytujDane_Click(object sender, EventArgs e)
        {
            fEdycjaNauczyciela edycjaNauczyciela = new fEdycjaNauczyciela(this.zalogowanyNauczyciel);
            this.Hide();
            edycjaNauczyciela.ShowDialog();
            this.Show();
            t_loginMail.Text = zalogowanyNauczyciel.Email;
            t_hasloMail.Text = zalogowanyNauczyciel.Email_haslo;

        }
        private void t_rocznik1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (t_rocznik1.Text.Length == 0) t_rocznik2.Text = string.Empty;
                else t_rocznik2.Text = (Convert.ToInt32(t_rocznik1.Text) + 1).ToString();
                l_rocznikInfo.Visible = false;
            }
            catch (Exception)
            {
                l_rocznikInfo.Visible = true;
            }
            b_dodaj.Enabled = sprawdzenieCzyWypelnioneDane();
        }

        private void t_rocznik2_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (t_rocznik2.Text.Length == 0) t_rocznik1.Text = string.Empty;
                else t_rocznik1.Text = (Convert.ToInt32(t_rocznik2.Text) - 1).ToString();
                l_rocznikInfo.Visible = false;
            }
            catch (Exception)
            {
                l_rocznikInfo.Visible = true;
            }
            b_dodaj.Enabled = sprawdzenieCzyWypelnioneDane();
        }
        private void t_nazwa_TextChanged(object sender, EventArgs e)
        {
            b_dodaj.Enabled = sprawdzenieCzyWypelnioneDane();
        }
        private void b_wyloguj_Click(object sender, EventArgs e)
        {
            zalogowanyNauczyciel.wylaczEdycje = false;
            zalogowanyNauczyciel.Email_haslo = t_hasloMail.Text;
            zalogowanyNauczyciel.wylogujMail(bw_polaczZMailem);
        }
        private void bw_polaczZMailem_DoWork(object sender, DoWorkEventArgs e)
        {
            e.Result = ustawieniePaskaIGrupy();
        }
        private bool ustawieniePaskaIGrupy()
       {
            string lanON = "LAN: ON";
            string lanOFF = "LAN: OFF";
            string mailInfo = " | MAIL: ";
            string mailNiepolaczony = "niepolaczony";
            string mailZleHaslo = "zle haslo";

            if (sprawdzeniePolaczeniaZInternetem() == true)
            {
                this.s_statusInternetu.Text = lanON;
                /// TU TRZEBA SPRAWDZIC!!

                if (zalogowanyNauczyciel.ZalogowanyMail == 1)
                {
                    
                    int result = polaczZMailem();

                    if (result == -1)
                    {
                        // jak cos poszlo nie tak
                        this.s_statusInternetu.Text += mailInfo + mailNiepolaczony;
                        return false;
                    }
                    else if (result == 0)
                    {
                        // jak zle pass
                        this.s_statusInternetu.Text += mailInfo + mailZleHaslo;
                        return false;
                    }
                    else if (result == 1)
                    {
                        // jak sie udalo
                        this.s_statusInternetu.Text += mailInfo + zalogowanyNauczyciel.Email + @"@gmail.com";
                        return true;
                    }   
                }
                else
                {
                    this.s_statusInternetu.Text += mailInfo + mailNiepolaczony;
                    return false;
                }
            }
            else
            {
                this.s_statusInternetu.Text = lanOFF;
                this.s_statusInternetu.Text += mailInfo + mailNiepolaczony;
                return false;
            }
            return false;
        }
        private int polaczZMailem()
       {
           if (zalogowanyNauczyciel.Email.Length == 0) return -1;
           ImapX.ImapClient klient = new ImapX.ImapClient("imap.gmail.com", 993, true, true);
           bool result = klient.Connect();
           if ((t_loginMail.Text.Length == 0) || (t_hasloMail.Text.Length == 0)) return -1;
           if (result)
           {
               return Convert.ToInt32(klient.Login(t_loginMail.Text + @"@gmail.com", t_hasloMail.Text));
           }
           else return -1;
       }
        private bool sprawdzeniePolaczeniaZInternetem()
            {
                int Out;
                return InternetGetConnectedState(out Out, 0);

            }
        private void bw_polaczZMailem_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            bool status = (bool)e.Result;
            if (status == true)
            {
                this.gb_mailZalogowany.Visible = true;
                this.gb_powiazanieKontaZPoczta.Visible = false;
            }
            else
            {
                this.gb_mailZalogowany.Visible = false;
                this.gb_powiazanieKontaZPoczta.Visible = true;
            }
        }
        private void b_zalogujMail_Click(object sender, EventArgs e)
      {
          zalogowanyNauczyciel.wylaczEdycje = false;
          zalogowanyNauczyciel.Email_haslo = t_hasloMail.Text;
          zalogowanyNauczyciel.zalogujMail(bw_polaczZMailem);
          //zalogowanyNauczyciel.ZalogowanyMail = 1;
          l_mail.Text = zalogowanyNauczyciel.Email;
          //if(!bw_polaczZMailem.IsBusy) bw_polaczZMailem.RunWorkerAsync();
      }        
    }
}
