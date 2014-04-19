using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Dziennik_nauczyciela
{
    public partial class fWidokUcznia : fWidokUczniaLubPrzedmiotu
    {
        
        public fWidokUcznia(int klasaID)
        {
            InitializeComponent();
            base.wczytajDane(klasaID, "uczen");
        }
    }
}
