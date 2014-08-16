using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Dziennik_nauczyciela_obiektowy.Forms
{
    public partial class fWidokUczniaLubPrzedmiotu : Form
    {
        //private int klasaID = -1;
        //ten konstruktor jest tylko to by mogl byc wyswietlany fWidok(Przedmiotu/Ucznia)[Design]
        protected fWidokUczniaLubPrzedmiotu()
        {
            InitializeComponent();
            this.dgv_lista.BackgroundColor = this.BackColor;
            this.t_usun.PasswordChar = '\u25CF';
            this.dgv_lista.BackgroundColor = this.BackColor;
        }
        public fWidokUczniaLubPrzedmiotu(int klasaID, Type t)
        {
            InitializeComponent();
            this.dgv_lista.BackgroundColor = this.BackColor;
            this.t_usun.PasswordChar = '\u25CF';
        }
    }
}
