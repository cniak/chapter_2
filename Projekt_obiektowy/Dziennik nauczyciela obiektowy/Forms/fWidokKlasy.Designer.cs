namespace Dziennik_nauczyciela_obiektowy.Forms
{
    partial class fWidokKlasy
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            this.bg_wczytajDaneIndywidualne = new System.ComponentModel.BackgroundWorker();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.klasaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.uczeńToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.przedmiotToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.l_prowadzacy = new System.Windows.Forms.Label();
            this.l_nazwaKlasy = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.l_gospodarz = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.l_sredniaOcenUczniow = new System.Windows.Forms.Label();
            this.b_dodajDzien = new System.Windows.Forms.Button();
            this.mc_kalendarz = new System.Windows.Forms.MonthCalendar();
            this.bg_wczytajPrzedmioty = new System.ComponentModel.BackgroundWorker();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.richTextBox2 = new System.Windows.Forms.RichTextBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label21 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.label17 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.Tytuł = new System.Windows.Forms.Label();
            this.checkedListBox1 = new System.Windows.Forms.CheckedListBox();
            this.Indywidualne = new System.Windows.Forms.TabPage();
            this.label22 = new System.Windows.Forms.Label();
            this.dgv_listaUczniow_indywidualne = new System.Windows.Forms.DataGridView();
            this.cb_miesiace = new System.Windows.Forms.ComboBox();
            this.tabelaIndywidualne = new System.Windows.Forms.TabControl();
            this.Oceny = new System.Windows.Forms.TabPage();
            this.dgv_listaOcen_indywidualne = new System.Windows.Forms.DataGridView();
            this.Obecności = new System.Windows.Forms.TabPage();
            this.dgv_listaObecnosci_indywidualne = new System.Windows.Forms.DataGridView();
            this.Uwagi = new System.Windows.Forms.TabPage();
            this.b_zapiszUwagi_indywidualne = new System.Windows.Forms.Button();
            this.t_uwagi_indywidualne = new System.Windows.Forms.RichTextBox();
            this.Wykresy = new System.Windows.Forms.TabPage();
            this.chart_Wykresy = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cb_typWykresy = new System.Windows.Forms.ComboBox();
            this.label13 = new System.Windows.Forms.Label();
            this.cb_zbiorWykresy = new System.Windows.Forms.ComboBox();
            this.label12 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.cb_przedmiotWykresy = new System.Windows.Forms.ComboBox();
            this.b_pokaz = new System.Windows.Forms.Button();
            this.Dziennik = new System.Windows.Forms.TabPage();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.l_miesiacDziennik = new System.Windows.Forms.Label();
            this.cb_miesiaceDziennik = new System.Windows.Forms.ComboBox();
            this.cb_typ = new System.Windows.Forms.ComboBox();
            this.label14 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.cb_przedmiotDziennik = new System.Windows.Forms.ComboBox();
            this.dgv_dziennik = new System.Windows.Forms.DataGridView();
            this.tabelaGlowna = new System.Windows.Forms.TabControl();
            this.b_odswiezListe = new System.Windows.Forms.Button();
            this.b_wyloguj = new System.Windows.Forms.Button();
            this.menuStrip1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.Indywidualne.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_listaUczniow_indywidualne)).BeginInit();
            this.tabelaIndywidualne.SuspendLayout();
            this.Oceny.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_listaOcen_indywidualne)).BeginInit();
            this.Obecności.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_listaObecnosci_indywidualne)).BeginInit();
            this.Uwagi.SuspendLayout();
            this.Wykresy.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart_Wykresy)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.Dziennik.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_dziennik)).BeginInit();
            this.tabelaGlowna.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.klasaToolStripMenuItem,
            this.uczeńToolStripMenuItem,
            this.przedmiotToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(805, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(220, 24);
            this.menuStrip1.TabIndex = 25;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // klasaToolStripMenuItem
            // 
            this.klasaToolStripMenuItem.Name = "klasaToolStripMenuItem";
            this.klasaToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.klasaToolStripMenuItem.Text = "Klasa";
            this.klasaToolStripMenuItem.Click += new System.EventHandler(this.klasaToolStripMenuItem_Click);
            // 
            // uczeńToolStripMenuItem
            // 
            this.uczeńToolStripMenuItem.Name = "uczeńToolStripMenuItem";
            this.uczeńToolStripMenuItem.Size = new System.Drawing.Size(48, 20);
            this.uczeńToolStripMenuItem.Text = "Uczeń";
            this.uczeńToolStripMenuItem.Click += new System.EventHandler(this.uczeńToolStripMenuItem_Click);
            // 
            // przedmiotToolStripMenuItem
            // 
            this.przedmiotToolStripMenuItem.Name = "przedmiotToolStripMenuItem";
            this.przedmiotToolStripMenuItem.Size = new System.Drawing.Size(66, 20);
            this.przedmiotToolStripMenuItem.Text = "Przedmiot";
            this.przedmiotToolStripMenuItem.Click += new System.EventHandler(this.przedmiotToolStripMenuItem_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40.5F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 59.5F));
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.label2, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.l_prowadzacy, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.l_nazwaKlasy, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.label3, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.l_gospodarz, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.label7, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.l_sredniaOcenUczniow, 1, 3);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(819, 47);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 17F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 31F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(194, 82);
            this.tableLayoutPanel1.TabIndex = 26;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 13);
            this.label1.TabIndex = 22;
            this.label1.Text = "Nazwa klasy:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 13);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(68, 13);
            this.label2.TabIndex = 23;
            this.label2.Text = "Prowadzący:";
            // 
            // l_prowadzacy
            // 
            this.l_prowadzacy.AutoSize = true;
            this.l_prowadzacy.Location = new System.Drawing.Point(81, 13);
            this.l_prowadzacy.Name = "l_prowadzacy";
            this.l_prowadzacy.Size = new System.Drawing.Size(105, 13);
            this.l_prowadzacy.TabIndex = 26;
            this.l_prowadzacy.Text = "[IMIĘ + NAZWISKO]";
            // 
            // l_nazwaKlasy
            // 
            this.l_nazwaKlasy.AutoSize = true;
            this.l_nazwaKlasy.Location = new System.Drawing.Point(81, 0);
            this.l_nazwaKlasy.Name = "l_nazwaKlasy";
            this.l_nazwaKlasy.Size = new System.Drawing.Size(53, 13);
            this.l_nazwaKlasy.TabIndex = 22;
            this.l_nazwaKlasy.Text = "[NAZWA]";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 26);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(61, 13);
            this.label3.TabIndex = 27;
            this.label3.Text = "Gospodarz:";
            // 
            // l_gospodarz
            // 
            this.l_gospodarz.AutoSize = true;
            this.l_gospodarz.Location = new System.Drawing.Point(81, 26);
            this.l_gospodarz.Name = "l_gospodarz";
            this.l_gospodarz.Size = new System.Drawing.Size(110, 17);
            this.l_gospodarz.TabIndex = 28;
            this.l_gospodarz.Text = "[IMIĘ + NAZWISKO | BRAK]";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(3, 43);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(70, 26);
            this.label7.TabIndex = 29;
            this.label7.Text = "Średnia ocen uczniów:";
            // 
            // l_sredniaOcenUczniow
            // 
            this.l_sredniaOcenUczniow.AutoSize = true;
            this.l_sredniaOcenUczniow.Location = new System.Drawing.Point(81, 43);
            this.l_sredniaOcenUczniow.Name = "l_sredniaOcenUczniow";
            this.l_sredniaOcenUczniow.Size = new System.Drawing.Size(50, 13);
            this.l_sredniaOcenUczniow.TabIndex = 30;
            this.l_sredniaOcenUczniow.Text = "[LICZBA]";
            // 
            // b_dodajDzien
            // 
            this.b_dodajDzien.Location = new System.Drawing.Point(12, 198);
            this.b_dodajDzien.Name = "b_dodajDzien";
            this.b_dodajDzien.Size = new System.Drawing.Size(164, 23);
            this.b_dodajDzien.TabIndex = 2;
            this.b_dodajDzien.Text = "Dodaj dzień";
            this.b_dodajDzien.UseVisualStyleBackColor = true;
            this.b_dodajDzien.Click += new System.EventHandler(this.b_dodajDzien_Click);
            // 
            // mc_kalendarz
            // 
            this.mc_kalendarz.Location = new System.Drawing.Point(12, 25);
            this.mc_kalendarz.MaxSelectionCount = 1;
            this.mc_kalendarz.Name = "mc_kalendarz";
            this.mc_kalendarz.TabIndex = 1;
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.b_dodajDzien);
            this.groupBox2.Controls.Add(this.mc_kalendarz);
            this.groupBox2.Location = new System.Drawing.Point(819, 233);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(194, 238);
            this.groupBox2.TabIndex = 27;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Dodaj dzień do bazy danych";
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.textBox3);
            this.tabPage1.Controls.Add(this.textBox2);
            this.tabPage1.Controls.Add(this.richTextBox2);
            this.tabPage1.Controls.Add(this.textBox1);
            this.tabPage1.Controls.Add(this.label21);
            this.tabPage1.Controls.Add(this.label20);
            this.tabPage1.Controls.Add(this.label19);
            this.tabPage1.Controls.Add(this.label18);
            this.tabPage1.Controls.Add(this.button2);
            this.tabPage1.Controls.Add(this.button1);
            this.tabPage1.Controls.Add(this.label17);
            this.tabPage1.Controls.Add(this.label16);
            this.tabPage1.Controls.Add(this.Tytuł);
            this.tabPage1.Controls.Add(this.checkedListBox1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(797, 446);
            this.tabPage1.TabIndex = 4;
            this.tabPage1.Text = "Wyślij wiadomość";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(241, 39);
            this.textBox3.Name = "textBox3";
            this.textBox3.ReadOnly = true;
            this.textBox3.Size = new System.Drawing.Size(404, 20);
            this.textBox3.TabIndex = 13;
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(545, 13);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(100, 20);
            this.textBox2.TabIndex = 8;
            // 
            // richTextBox2
            // 
            this.richTextBox2.Location = new System.Drawing.Point(241, 102);
            this.richTextBox2.Name = "richTextBox2";
            this.richTextBox2.Size = new System.Drawing.Size(404, 96);
            this.richTextBox2.TabIndex = 5;
            this.richTextBox2.Text = "";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(241, 65);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(404, 20);
            this.textBox1.TabIndex = 0;
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(143, 42);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(52, 13);
            this.label21.TabIndex = 12;
            this.label21.Text = "Odbiorcy:";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(500, 16);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(39, 13);
            this.label20.TabIndex = 11;
            this.label20.Text = "Hasło:";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(238, 16);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(87, 13);
            this.label19.TabIndex = 10;
            this.label19.Text = "main@gmail.com";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(138, 16);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(54, 13);
            this.label18.TabIndex = 9;
            this.label18.Text = "Twój mail:";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(570, 204);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 7;
            this.button2.Text = "Wyślij";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(241, 204);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 6;
            this.button1.Text = "Wybierz plik";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(143, 209);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(58, 13);
            this.label17.TabIndex = 4;
            this.label17.Text = "Załącznik:";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(143, 95);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(37, 13);
            this.label16.TabIndex = 3;
            this.label16.Text = "Treść:";
            // 
            // Tytuł
            // 
            this.Tytuł.AutoSize = true;
            this.Tytuł.Location = new System.Drawing.Point(143, 68);
            this.Tytuł.Name = "Tytuł";
            this.Tytuł.Size = new System.Drawing.Size(35, 13);
            this.Tytuł.TabIndex = 2;
            this.Tytuł.Text = "Tytuł:";
            // 
            // checkedListBox1
            // 
            this.checkedListBox1.Dock = System.Windows.Forms.DockStyle.Left;
            this.checkedListBox1.FormattingEnabled = true;
            this.checkedListBox1.Items.AddRange(new object[] {
            "Zaznacz wszystkich",
            "Uczeń 1",
            "Uczeń 2",
            "Uczeń 3",
            "Uczeń 4"});
            this.checkedListBox1.Location = new System.Drawing.Point(3, 3);
            this.checkedListBox1.Name = "checkedListBox1";
            this.checkedListBox1.Size = new System.Drawing.Size(120, 440);
            this.checkedListBox1.TabIndex = 1;
            // 
            // Indywidualne
            // 
            this.Indywidualne.Controls.Add(this.label22);
            this.Indywidualne.Controls.Add(this.dgv_listaUczniow_indywidualne);
            this.Indywidualne.Controls.Add(this.cb_miesiace);
            this.Indywidualne.Controls.Add(this.tabelaIndywidualne);
            this.Indywidualne.Location = new System.Drawing.Point(4, 22);
            this.Indywidualne.Name = "Indywidualne";
            this.Indywidualne.Size = new System.Drawing.Size(797, 446);
            this.Indywidualne.TabIndex = 2;
            this.Indywidualne.Text = "Indywidualne";
            this.Indywidualne.UseVisualStyleBackColor = true;
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(479, 6);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(86, 13);
            this.label22.TabIndex = 6;
            this.label22.Text = "Wybierz miesiąc:";
            // 
            // dgv_listaUczniow_indywidualne
            // 
            this.dgv_listaUczniow_indywidualne.AllowUserToAddRows = false;
            this.dgv_listaUczniow_indywidualne.AllowUserToDeleteRows = false;
            this.dgv_listaUczniow_indywidualne.AllowUserToOrderColumns = true;
            this.dgv_listaUczniow_indywidualne.AllowUserToResizeRows = false;
            this.dgv_listaUczniow_indywidualne.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgv_listaUczniow_indywidualne.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.dgv_listaUczniow_indywidualne.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_listaUczniow_indywidualne.Dock = System.Windows.Forms.DockStyle.Left;
            this.dgv_listaUczniow_indywidualne.Location = new System.Drawing.Point(0, 0);
            this.dgv_listaUczniow_indywidualne.MultiSelect = false;
            this.dgv_listaUczniow_indywidualne.Name = "dgv_listaUczniow_indywidualne";
            this.dgv_listaUczniow_indywidualne.ReadOnly = true;
            this.dgv_listaUczniow_indywidualne.RowHeadersVisible = false;
            this.dgv_listaUczniow_indywidualne.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv_listaUczniow_indywidualne.Size = new System.Drawing.Size(223, 446);
            this.dgv_listaUczniow_indywidualne.TabIndex = 19;
            // 
            // cb_miesiace
            // 
            this.cb_miesiace.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_miesiace.FormattingEnabled = true;
            this.cb_miesiace.Items.AddRange(new object[] {
            "WSZYSTKIE",
            "styczeń",
            "luty",
            "marzec",
            "kwiecień",
            "maj",
            "czerwiec"});
            this.cb_miesiace.Location = new System.Drawing.Point(571, 3);
            this.cb_miesiace.Name = "cb_miesiace";
            this.cb_miesiace.Size = new System.Drawing.Size(219, 21);
            this.cb_miesiace.TabIndex = 5;
            this.cb_miesiace.SelectedIndexChanged += new System.EventHandler(this.cb_miesiace_SelectedIndexChanged);
            // 
            // tabelaIndywidualne
            // 
            this.tabelaIndywidualne.Controls.Add(this.Oceny);
            this.tabelaIndywidualne.Controls.Add(this.Obecności);
            this.tabelaIndywidualne.Controls.Add(this.Uwagi);
            this.tabelaIndywidualne.Location = new System.Drawing.Point(225, 25);
            this.tabelaIndywidualne.Name = "tabelaIndywidualne";
            this.tabelaIndywidualne.SelectedIndex = 0;
            this.tabelaIndywidualne.Size = new System.Drawing.Size(569, 418);
            this.tabelaIndywidualne.TabIndex = 3;
            this.tabelaIndywidualne.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
            // 
            // Oceny
            // 
            this.Oceny.Controls.Add(this.dgv_listaOcen_indywidualne);
            this.Oceny.Location = new System.Drawing.Point(4, 22);
            this.Oceny.Name = "Oceny";
            this.Oceny.Padding = new System.Windows.Forms.Padding(3);
            this.Oceny.Size = new System.Drawing.Size(561, 392);
            this.Oceny.TabIndex = 0;
            this.Oceny.Text = "Oceny";
            this.Oceny.UseVisualStyleBackColor = true;
            // 
            // dgv_listaOcen_indywidualne
            // 
            this.dgv_listaOcen_indywidualne.AllowUserToAddRows = false;
            this.dgv_listaOcen_indywidualne.AllowUserToDeleteRows = false;
            this.dgv_listaOcen_indywidualne.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.ColumnHeader;
            this.dgv_listaOcen_indywidualne.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_listaOcen_indywidualne.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv_listaOcen_indywidualne.Location = new System.Drawing.Point(3, 3);
            this.dgv_listaOcen_indywidualne.Name = "dgv_listaOcen_indywidualne";
            this.dgv_listaOcen_indywidualne.RowHeadersVisible = false;
            this.dgv_listaOcen_indywidualne.Size = new System.Drawing.Size(555, 386);
            this.dgv_listaOcen_indywidualne.TabIndex = 6;
            // 
            // Obecności
            // 
            this.Obecności.Controls.Add(this.dgv_listaObecnosci_indywidualne);
            this.Obecności.Location = new System.Drawing.Point(4, 22);
            this.Obecności.Name = "Obecności";
            this.Obecności.Padding = new System.Windows.Forms.Padding(3);
            this.Obecności.Size = new System.Drawing.Size(561, 392);
            this.Obecności.TabIndex = 1;
            this.Obecności.Text = "Obecności";
            this.Obecności.UseVisualStyleBackColor = true;
            // 
            // dgv_listaObecnosci_indywidualne
            // 
            this.dgv_listaObecnosci_indywidualne.AllowUserToAddRows = false;
            this.dgv_listaObecnosci_indywidualne.AllowUserToDeleteRows = false;
            this.dgv_listaObecnosci_indywidualne.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.ColumnHeader;
            this.dgv_listaObecnosci_indywidualne.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_listaObecnosci_indywidualne.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv_listaObecnosci_indywidualne.Location = new System.Drawing.Point(3, 3);
            this.dgv_listaObecnosci_indywidualne.MultiSelect = false;
            this.dgv_listaObecnosci_indywidualne.Name = "dgv_listaObecnosci_indywidualne";
            this.dgv_listaObecnosci_indywidualne.RowHeadersVisible = false;
            this.dgv_listaObecnosci_indywidualne.Size = new System.Drawing.Size(555, 386);
            this.dgv_listaObecnosci_indywidualne.TabIndex = 4;
            // 
            // Uwagi
            // 
            this.Uwagi.Controls.Add(this.b_zapiszUwagi_indywidualne);
            this.Uwagi.Controls.Add(this.t_uwagi_indywidualne);
            this.Uwagi.Location = new System.Drawing.Point(4, 22);
            this.Uwagi.Name = "Uwagi";
            this.Uwagi.Size = new System.Drawing.Size(561, 392);
            this.Uwagi.TabIndex = 2;
            this.Uwagi.Text = "Uwagi";
            this.Uwagi.UseVisualStyleBackColor = true;
            // 
            // b_zapiszUwagi_indywidualne
            // 
            this.b_zapiszUwagi_indywidualne.Location = new System.Drawing.Point(483, 330);
            this.b_zapiszUwagi_indywidualne.Name = "b_zapiszUwagi_indywidualne";
            this.b_zapiszUwagi_indywidualne.Size = new System.Drawing.Size(75, 23);
            this.b_zapiszUwagi_indywidualne.TabIndex = 1;
            this.b_zapiszUwagi_indywidualne.Text = "Zapisz";
            this.b_zapiszUwagi_indywidualne.UseVisualStyleBackColor = true;
            this.b_zapiszUwagi_indywidualne.Click += new System.EventHandler(this.b_zapiszUwagi_indywidualne_Click);
            // 
            // t_uwagi_indywidualne
            // 
            this.t_uwagi_indywidualne.Dock = System.Windows.Forms.DockStyle.Top;
            this.t_uwagi_indywidualne.Location = new System.Drawing.Point(0, 0);
            this.t_uwagi_indywidualne.Name = "t_uwagi_indywidualne";
            this.t_uwagi_indywidualne.Size = new System.Drawing.Size(561, 324);
            this.t_uwagi_indywidualne.TabIndex = 0;
            this.t_uwagi_indywidualne.Text = "";
            // 
            // Wykresy
            // 
            this.Wykresy.Controls.Add(this.chart_Wykresy);
            this.Wykresy.Controls.Add(this.groupBox1);
            this.Wykresy.Location = new System.Drawing.Point(4, 22);
            this.Wykresy.Name = "Wykresy";
            this.Wykresy.Padding = new System.Windows.Forms.Padding(3);
            this.Wykresy.Size = new System.Drawing.Size(797, 446);
            this.Wykresy.TabIndex = 1;
            this.Wykresy.Text = "Wykresy";
            this.Wykresy.UseVisualStyleBackColor = true;
            // 
            // chart_Wykresy
            // 
            legend2.Name = "Legend1";
            this.chart_Wykresy.Legends.Add(legend2);
            this.chart_Wykresy.Location = new System.Drawing.Point(14, 62);
            this.chart_Wykresy.Name = "chart_Wykresy";
            this.chart_Wykresy.Size = new System.Drawing.Size(771, 365);
            this.chart_Wykresy.TabIndex = 14;
            this.chart_Wykresy.Text = "chart1";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cb_typWykresy);
            this.groupBox1.Controls.Add(this.label13);
            this.groupBox1.Controls.Add(this.cb_zbiorWykresy);
            this.groupBox1.Controls.Add(this.label12);
            this.groupBox1.Controls.Add(this.label15);
            this.groupBox1.Controls.Add(this.cb_przedmiotWykresy);
            this.groupBox1.Controls.Add(this.b_pokaz);
            this.groupBox1.Location = new System.Drawing.Point(9, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(782, 50);
            this.groupBox1.TabIndex = 13;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Wybierz grupę danych";
            // 
            // cb_typWykresy
            // 
            this.cb_typWykresy.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_typWykresy.FormattingEnabled = true;
            this.cb_typWykresy.Location = new System.Drawing.Point(40, 16);
            this.cb_typWykresy.Name = "cb_typWykresy";
            this.cb_typWykresy.Size = new System.Drawing.Size(121, 21);
            this.cb_typWykresy.TabIndex = 15;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(6, 19);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(28, 13);
            this.label13.TabIndex = 14;
            this.label13.Text = "Typ:";
            // 
            // cb_zbiorWykresy
            // 
            this.cb_zbiorWykresy.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_zbiorWykresy.FormattingEnabled = true;
            this.cb_zbiorWykresy.Items.AddRange(new object[] {
            "Klasa",
            "Uczeń 1",
            "Uczeń 2",
            "Uczeń 3",
            "Uczeń 4"});
            this.cb_zbiorWykresy.Location = new System.Drawing.Point(428, 16);
            this.cb_zbiorWykresy.Name = "cb_zbiorWykresy";
            this.cb_zbiorWykresy.Size = new System.Drawing.Size(182, 21);
            this.cb_zbiorWykresy.TabIndex = 11;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(388, 20);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(41, 13);
            this.label12.TabIndex = 9;
            this.label12.Text = "Uczen:";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(167, 19);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(56, 13);
            this.label15.TabIndex = 13;
            this.label15.Text = "Przedmiot:";
            // 
            // cb_przedmiotWykresy
            // 
            this.cb_przedmiotWykresy.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_przedmiotWykresy.FormattingEnabled = true;
            this.cb_przedmiotWykresy.Location = new System.Drawing.Point(229, 15);
            this.cb_przedmiotWykresy.Name = "cb_przedmiotWykresy";
            this.cb_przedmiotWykresy.Size = new System.Drawing.Size(121, 21);
            this.cb_przedmiotWykresy.TabIndex = 12;
            // 
            // b_pokaz
            // 
            this.b_pokaz.Location = new System.Drawing.Point(673, 15);
            this.b_pokaz.Name = "b_pokaz";
            this.b_pokaz.Size = new System.Drawing.Size(103, 23);
            this.b_pokaz.TabIndex = 5;
            this.b_pokaz.Text = "Pokaz";
            this.b_pokaz.UseVisualStyleBackColor = true;
            this.b_pokaz.Click += new System.EventHandler(this.b_zapiszPDFWykresy_Click);
            // 
            // Dziennik
            // 
            this.Dziennik.Controls.Add(this.groupBox3);
            this.Dziennik.Controls.Add(this.dgv_dziennik);
            this.Dziennik.Location = new System.Drawing.Point(4, 22);
            this.Dziennik.Name = "Dziennik";
            this.Dziennik.Padding = new System.Windows.Forms.Padding(3);
            this.Dziennik.Size = new System.Drawing.Size(797, 448);
            this.Dziennik.TabIndex = 0;
            this.Dziennik.Text = "Dziennik";
            this.Dziennik.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.l_miesiacDziennik);
            this.groupBox3.Controls.Add(this.cb_miesiaceDziennik);
            this.groupBox3.Controls.Add(this.cb_typ);
            this.groupBox3.Controls.Add(this.label14);
            this.groupBox3.Controls.Add(this.label11);
            this.groupBox3.Controls.Add(this.cb_przedmiotDziennik);
            this.groupBox3.Location = new System.Drawing.Point(9, 7);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(782, 53);
            this.groupBox3.TabIndex = 5;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Wybierz grupę danych";
            // 
            // l_miesiacDziennik
            // 
            this.l_miesiacDziennik.AutoSize = true;
            this.l_miesiacDziennik.Location = new System.Drawing.Point(356, 21);
            this.l_miesiacDziennik.Name = "l_miesiacDziennik";
            this.l_miesiacDziennik.Size = new System.Drawing.Size(49, 13);
            this.l_miesiacDziennik.TabIndex = 9;
            this.l_miesiacDziennik.Text = "Miesiac: ";
            // 
            // cb_miesiaceDziennik
            // 
            this.cb_miesiaceDziennik.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_miesiaceDziennik.FormattingEnabled = true;
            this.cb_miesiaceDziennik.Location = new System.Drawing.Point(411, 19);
            this.cb_miesiaceDziennik.Name = "cb_miesiaceDziennik";
            this.cb_miesiaceDziennik.Size = new System.Drawing.Size(161, 21);
            this.cb_miesiaceDziennik.TabIndex = 8;
            // 
            // cb_typ
            // 
            this.cb_typ.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_typ.FormattingEnabled = true;
            this.cb_typ.Location = new System.Drawing.Point(40, 18);
            this.cb_typ.Name = "cb_typ";
            this.cb_typ.Size = new System.Drawing.Size(121, 21);
            this.cb_typ.TabIndex = 6;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(6, 23);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(28, 13);
            this.label14.TabIndex = 5;
            this.label14.Text = "Typ:";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(167, 23);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(56, 13);
            this.label11.TabIndex = 4;
            this.label11.Text = "Przedmiot:";
            // 
            // cb_przedmiotDziennik
            // 
            this.cb_przedmiotDziennik.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_przedmiotDziennik.FormattingEnabled = true;
            this.cb_przedmiotDziennik.Location = new System.Drawing.Point(229, 18);
            this.cb_przedmiotDziennik.Name = "cb_przedmiotDziennik";
            this.cb_przedmiotDziennik.Size = new System.Drawing.Size(121, 21);
            this.cb_przedmiotDziennik.TabIndex = 3;
            // 
            // dgv_dziennik
            // 
            this.dgv_dziennik.AllowUserToAddRows = false;
            this.dgv_dziennik.AllowUserToDeleteRows = false;
            this.dgv_dziennik.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_dziennik.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.dgv_dziennik.Location = new System.Drawing.Point(3, 68);
            this.dgv_dziennik.Name = "dgv_dziennik";
            this.dgv_dziennik.ReadOnly = true;
            this.dgv_dziennik.RowHeadersVisible = false;
            this.dgv_dziennik.Size = new System.Drawing.Size(791, 377);
            this.dgv_dziennik.TabIndex = 0;
            // 
            // tabelaGlowna
            // 
            this.tabelaGlowna.Controls.Add(this.Dziennik);
            this.tabelaGlowna.Controls.Add(this.Wykresy);
            this.tabelaGlowna.Controls.Add(this.Indywidualne);
            this.tabelaGlowna.Controls.Add(this.tabPage1);
            this.tabelaGlowna.Dock = System.Windows.Forms.DockStyle.Left;
            this.tabelaGlowna.Location = new System.Drawing.Point(0, 0);
            this.tabelaGlowna.Name = "tabelaGlowna";
            this.tabelaGlowna.SelectedIndex = 0;
            this.tabelaGlowna.Size = new System.Drawing.Size(805, 474);
            this.tabelaGlowna.TabIndex = 28;
            this.tabelaGlowna.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
            // 
            // b_odswiezListe
            // 
            this.b_odswiezListe.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.b_odswiezListe.ForeColor = System.Drawing.Color.Black;
            this.b_odswiezListe.Location = new System.Drawing.Point(819, 172);
            this.b_odswiezListe.Name = "b_odswiezListe";
            this.b_odswiezListe.Size = new System.Drawing.Size(194, 23);
            this.b_odswiezListe.TabIndex = 29;
            this.b_odswiezListe.Text = "Odswiez dane";
            this.b_odswiezListe.UseVisualStyleBackColor = true;
            this.b_odswiezListe.Click += new System.EventHandler(this.b_odswiezListe_Click);
            // 
            // b_wyloguj
            // 
            this.b_wyloguj.Location = new System.Drawing.Point(819, 202);
            this.b_wyloguj.Name = "b_wyloguj";
            this.b_wyloguj.Size = new System.Drawing.Size(194, 23);
            this.b_wyloguj.TabIndex = 30;
            this.b_wyloguj.Text = "Wyloguj";
            this.b_wyloguj.UseVisualStyleBackColor = true;
            this.b_wyloguj.Click += new System.EventHandler(this.b_wyloguj_Click);
            // 
            // fWidokKlasy
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1025, 474);
            this.Controls.Add(this.b_wyloguj);
            this.Controls.Add(this.b_odswiezListe);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.tabelaGlowna);
            this.Controls.Add(this.groupBox2);
            this.Name = "fWidokKlasy";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "fWidokKlasy";
            this.Load += new System.EventHandler(this.fWidokKlasy_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.Indywidualne.ResumeLayout(false);
            this.Indywidualne.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_listaUczniow_indywidualne)).EndInit();
            this.tabelaIndywidualne.ResumeLayout(false);
            this.Oceny.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_listaOcen_indywidualne)).EndInit();
            this.Obecności.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_listaObecnosci_indywidualne)).EndInit();
            this.Uwagi.ResumeLayout(false);
            this.Wykresy.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chart_Wykresy)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.Dziennik.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_dziennik)).EndInit();
            this.tabelaGlowna.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.ComponentModel.BackgroundWorker bg_wczytajDaneIndywidualne;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem klasaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem uczeńToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem przedmiotToolStripMenuItem;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label l_prowadzacy;
        private System.Windows.Forms.Label l_nazwaKlasy;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label l_gospodarz;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label l_sredniaOcenUczniow;
        private System.Windows.Forms.Button b_dodajDzien;
        private System.Windows.Forms.MonthCalendar mc_kalendarz;
        private System.ComponentModel.BackgroundWorker bg_wczytajPrzedmioty;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.RichTextBox richTextBox2;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label Tytuł;
        private System.Windows.Forms.CheckedListBox checkedListBox1;
        private System.Windows.Forms.TabPage Indywidualne;
        public System.Windows.Forms.DataGridView dgv_listaUczniow_indywidualne;
        private System.Windows.Forms.TabPage Wykresy;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox cb_typWykresy;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.ComboBox cb_przedmiotWykresy;
        private System.Windows.Forms.Button b_pokaz;
        private System.Windows.Forms.ComboBox cb_zbiorWykresy;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TabPage Dziennik;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.ComboBox cb_typ;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.ComboBox cb_przedmiotDziennik;
        private System.Windows.Forms.DataGridView dgv_dziennik;
        private System.Windows.Forms.TabControl tabelaGlowna;
        private System.Windows.Forms.TabControl tabelaIndywidualne;
        private System.Windows.Forms.TabPage Oceny;
        private System.Windows.Forms.DataGridView dgv_listaOcen_indywidualne;
        private System.Windows.Forms.TabPage Obecności;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.ComboBox cb_miesiace;
        private System.Windows.Forms.DataGridView dgv_listaObecnosci_indywidualne;
        private System.Windows.Forms.TabPage Uwagi;
        private System.Windows.Forms.Button b_zapiszUwagi_indywidualne;
        private System.Windows.Forms.RichTextBox t_uwagi_indywidualne;
        private System.Windows.Forms.Button b_odswiezListe;
        private System.Windows.Forms.Button b_wyloguj;
        private System.Windows.Forms.Label l_miesiacDziennik;
        private System.Windows.Forms.ComboBox cb_miesiaceDziennik;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart_Wykresy;
    }
}