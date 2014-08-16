using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace Dziennik_nauczyciela_obiektowy
{
    public class Wykresy
    {
        private Chart wykres = null;
        public BackgroundWorker bw = null;
        private ListaDat listaDat = null;
        int zbiorNR = -1;
        int przedmiotNR = -1;
        int typDanych = -1;
        private ComboBox cb_zbior = null;
        private ComboBox cb_przedmiot= null;
        private ComboBox cb_typDanych = null;
        private int klasaNR = -1;

        public Chart Wykres { get { return wykres; } }

        public Wykresy(Chart chart_Wykres, ETypWykresu typWykresu, ListaDat listaDat, int klasaNR, ComboBox cb_zbior, ComboBox cb_przedmiot, ComboBox cb_typDanych)
        {
            this.cb_przedmiot = cb_przedmiot;
            this.cb_zbior = cb_zbior;
            this.cb_typDanych = cb_typDanych;

            this.listaDat = listaDat;
            this.klasaNR = klasaNR;
            try { typDanych = Convert.ToInt32(cb_typDanych.SelectedValue.ToString()); }
            catch { }
            try { przedmiotNR = Convert.ToInt32(cb_przedmiot.SelectedValue.ToString()); }
            catch { }
            try { zbiorNR = Convert.ToInt32(cb_zbior.SelectedValue.ToString()); }
            catch { }
            cb_przedmiot.SelectedIndexChanged += ((o,e) =>
            {
                try { przedmiotNR = Convert.ToInt32(cb_przedmiot.SelectedValue.ToString()); }
                catch { }
            });
            cb_zbior.SelectedIndexChanged += ((o, e) =>
                {
                    try { zbiorNR = Convert.ToInt32(cb_zbior.SelectedValue.ToString()); }
                    catch { }
                });
            cb_typDanych.SelectedValueChanged += ((o, e) =>
                {
                    try { typDanych = Convert.ToInt32(cb_typDanych.SelectedValue.ToString()); }
                    catch { }
                });

            wykres = chart_Wykres;
            bw = new BackgroundWorker();
            bw.DoWork +=bw_DoWork;
            bw.WorkerReportsProgress = true;
            bw.WorkerSupportsCancellation = true;
            
            bw.ProgressChanged +=bw_ProgressChanged;
            bw.RunWorkerCompleted +=bw_RunWorkerCompleted;
        }

        private void bw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            //TODO 3 nie trzeba dawac scrollbara, wszystko sie miesci
            /*
            wykres.ChartAreas[0].CursorX.AutoScroll = true;
            wykres.ChartAreas[0].AxisX.ScaleView.Zoomable = true;
            wykres.ChartAreas[0].AxisX.ScaleView.SizeType = DateTimeIntervalType.Months;
            DateTime dzisiaj = DateTime.Now;
            IEnumerable<int> ilosc = from d in listaDat.zbior
                        group d by new { d.Dzien.Month, d.Dzien.Year } into g
                        select g.Count();
            foreach (int i in ilosc)
            {
                wykres.ChartAreas[0].AxisX.ScaleView.Zoom(0, 10);
            }
            //wykres.ChartAreas[0].AxisX.ScaleView.Zoom(0, );
             */
            
            wykres.Titles.Clear();
            string doWykresu = (typDanych == (int)ETypDanych.ocena) ? "ocen" : "frekwencji";
            wykres.Titles.Add(new Title(
                "Wykres zmian sredniej " + doWykresu + " dla: " + cb_zbior.Text + " z przedmiotu: " + cb_przedmiot.Text,
                Docking.Top,
                new Font("Verdena",10f,FontStyle.Bold),
                Color.Red
        ));
        }

        private void bw_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
                object[] zPostepu = (object[])e.UserState;
                string miesiac = zPostepu[0].ToString();
                double wartosc = Convert.ToDouble(zPostepu[1].ToString());
                wykres.Series["h"].Points.AddXY(miesiac, wartosc);
        }

        private void bw_DoWork(object sender, DoWorkEventArgs e)
        {
            if (sprawdzCzyWybraneDane() == false)
            {
                MessageBox.Show("wykres nie zostanie wczytany (brak danych grupy wejsciowych nt. zbioru, przedmiotu i typu)");
                return;
            }
            var query = from d in listaDat.zbior
                        group d by new { d.Dzien.Month, d.Dzien.Year } into g
                        select new { Miesiac = g.Key, ID = g};
            int sumaOcen = 0;
            int iloscDanych = 0;
            DateTime dzisiaj = DateTime.Now;
            foreach (var group in query)
            {
                var miesiacTabeli = new DateTime(group.Miesiac.Year, group.Miesiac.Month, 1);
                if (miesiacTabeli > dzisiaj) return;
                string miesiac = "";
                foreach (var g in group.ID)
                {
                    lekcja l = new lekcja(g.DataID, przedmiotNR, klasaNR);
                    uczen_na_lekcji unl = new uczen_na_lekcji(zbiorNR, l.LekcjaID);
                    if (typDanych == (int)ETypDanych.ocena)
                    {
                        sumaOcen += unl.Ocena;
                        if (unl.Ocena > 0) iloscDanych++;
                    }
                    else
                    {
                        sumaOcen += unl.Obecnosc;
                        iloscDanych++;
                    }
                    miesiac = g.Dzien.ToString("MMMM", new CultureInfo("pl")) + " " + g.Dzien.ToString("yyyy");
                }
                
                double srednia = Math.Round(((float)sumaOcen)/iloscDanych,2);
                object[] doPostepu = new object[] { miesiac, srednia };
                bw.ReportProgress(100, doPostepu);
            }
        }
        private bool sprawdzCzyWybraneDane()
        {
            return (zbiorNR >= 0) && (klasaNR > 0) && (przedmiotNR > 0);
        }

        public void odswiezWykres()
        {
            //wykres.ChartAreas.Clear();
            try
            {
                wykres.ChartAreas.Clear();
                wykres.Series.RemoveAt(0);
            }
            catch { }
            wykres.ChartAreas.Add("test");

            wykres.Series.Add("h");
            wykres.Series["h"].IsValueShownAsLabel = true;
            wykres.Series["h"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            wykres.Series["h"].IsVisibleInLegend = false;

            
            wykres.ChartAreas[0].AxisX.ScrollBar.ButtonStyle = ScrollBarButtonStyles.SmallScroll;
            wykres.ChartAreas[0].AxisX.MajorGrid.Enabled = false;
            wykres.ChartAreas[0].AxisY.MajorGrid.Enabled = false;
            
            if (!bw.IsBusy)
            {
                bw.RunWorkerAsync();
            }
            
        }
    }
}
/*

          //chart1.ChartAreas.Clear();
          chart_Wykresy.ChartAreas.Add("test");
          chart_Wykresy.Series.Add("h");
          chart_Wykresy.Series["h"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
          chart_Wykresy.Series["h"].IsVisibleInLegend = false;
          chart_Wykresy.ChartAreas[0].AxisX.MajorGrid.Enabled = false;
          chart_Wykresy.ChartAreas[0].AxisY.MajorGrid.Enabled = false;
          chart_Wykresy.Series["h"].Points.AddXY("maj", 1);
          chart_Wykresy.Series["h"].Points.AddXY("czerwiec", 2);
          chart_Wykresy.Series["h"].Points.AddXY("lipiec", 1);
          //chart_Wykresy.ChartAreas.Add("chart");
          //chart_Wykresy.Height = 100;
           
           
          //chart_Wykresy.ChartAreas[0].IsSameFontSizeForAllAxes = true;
          //chart_Wykresy.ChartAreas[0].Position.Size.Height = 100;
          //chart_Wykresy.ChartAreas[0].Position.Y = 10;
          //chart_Wykresy.ChartAreas[0].Position.Height = 100;
            
          //chart_Wykresy.ChartAreas[0].AxisX.MajorGrid.Enabled = false;
          //chart_Wykresy.ChartAreas[0].AxisY.MajorGrid.Enabled = false;
            
          chart_Wykresy.ChartAreas["chart"].AxisX.Minimum = 0;
          chart_Wykresy.ChartAreas["chart"].AxisX.Maximum = 20;
          chart_Wykresy.ChartAreas["chart"].AxisX.Interval = 1;

          chart_Wykresy.ChartAreas["chart"].AxisY.Minimum = 0;
          chart_Wykresy.ChartAreas["chart"].AxisY.Maximum = 100;
          chart_Wykresy.ChartAreas["chart"].AxisY.Interval = 5;
            

          //chart_Wykresy.Series.Add("xxx");           
          //chart_Wykresy.BackGradientStyle = System.Windows.Forms.DataVisualization.Charting.GradientStyle.None;
          //chart_Wykresy.Series["xxx"].Color = Color.Black;
            
          //chart_Wykresy.Series["xxx"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
          //chart_Wykresy.Series["xxx"].Points.AddXY(1, 2);
          //chart_Wykresy.Series["xxx"].Points.AddXY(2, 3);
          //chart_Wykresy.Series["xxx"].Points.AddXY(3, 4);
 * 
 * 
 * int przedmiotID = 5;
            int klasaID = zalogowanaKlasa.KlasaID;
            int uczenNR = 4;
            //tu mam odpowiednio juz daty
            var query = from d in listaDat.zbior
                        group d by new { d.Dzien.Month, d.Dzien.Year} into g
                        select new { Miesiac = g.Key, ID = g };
            int i=1;
            foreach (var group in query)
            {
                string s = group.Miesiac.ToString();
                int sumaOcen = 0;
                foreach (var g in group.ID)
                {
                    lekcja l = new lekcja(g.DataID, przedmiotID, klasaID);
                    uczen_na_lekcji unl = new uczen_na_lekcji(uczenNR, l.LekcjaID);
                    sumaOcen += unl.Ocena;
                }
                chart_Wykresy.Series["h"].Points.AddXY(i, sumaOcen);
                i++;
            }
            
*/