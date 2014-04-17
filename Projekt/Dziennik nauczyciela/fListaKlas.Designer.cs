namespace Dziennik_nauczyciela
{
    partial class fListaKlas
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
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.s_statusInternetu = new System.Windows.Forms.ToolStripStatusLabel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.t_powtorzHaslo = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.b_dodaj = new System.Windows.Forms.Button();
            this.t_haslo = new System.Windows.Forms.TextBox();
            this.l_capslock = new System.Windows.Forms.Label();
            this.t_nazwa = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.dgv_listaKlas = new System.Windows.Forms.DataGridView();
            this.gb_powiazanieKontaZPoczta = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cb_wymagajHasla = new System.Windows.Forms.CheckBox();
            this.b_zalogujMail = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.t_hasloMail = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.t_loginMail = new System.Windows.Forms.TextBox();
            this.gb_mailZalogowany = new System.Windows.Forms.GroupBox();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.b_wyloguj = new System.Windows.Forms.Button();
            this.l_mail = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.b_edytujDane = new System.Windows.Forms.Button();
            this.b_usunKlase = new System.Windows.Forms.Button();
            this.statusStrip1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_listaKlas)).BeginInit();
            this.gb_powiazanieKontaZPoczta.SuspendLayout();
            this.gb_mailZalogowany.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.s_statusInternetu});
            this.statusStrip1.Location = new System.Drawing.Point(0, 404);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(616, 22);
            this.statusStrip1.TabIndex = 18;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // s_statusInternetu
            // 
            this.s_statusInternetu.Name = "s_statusInternetu";
            this.s_statusInternetu.Size = new System.Drawing.Size(30, 17);
            this.s_statusInternetu.Text = "LAN:";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.t_powtorzHaslo);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.b_dodaj);
            this.groupBox1.Controls.Add(this.t_haslo);
            this.groupBox1.Controls.Add(this.l_capslock);
            this.groupBox1.Controls.Add(this.t_nazwa);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 51);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(354, 130);
            this.groupBox1.TabIndex = 17;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Dodawanie klasy";
            // 
            // t_powtorzHaslo
            // 
            this.t_powtorzHaslo.Location = new System.Drawing.Point(91, 65);
            this.t_powtorzHaslo.Name = "t_powtorzHaslo";
            this.t_powtorzHaslo.Size = new System.Drawing.Size(249, 20);
            this.t_powtorzHaslo.TabIndex = 8;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(6, 68);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(78, 13);
            this.label8.TabIndex = 7;
            this.label8.Text = "Powtórz hasło:";
            // 
            // b_dodaj
            // 
            this.b_dodaj.Location = new System.Drawing.Point(265, 91);
            this.b_dodaj.Name = "b_dodaj";
            this.b_dodaj.Size = new System.Drawing.Size(75, 23);
            this.b_dodaj.TabIndex = 4;
            this.b_dodaj.Text = "Dodaj";
            this.b_dodaj.UseVisualStyleBackColor = true;
            this.b_dodaj.Click += new System.EventHandler(this.b_dodaj_Click);
            // 
            // t_haslo
            // 
            this.t_haslo.Location = new System.Drawing.Point(91, 39);
            this.t_haslo.Name = "t_haslo";
            this.t_haslo.Size = new System.Drawing.Size(249, 20);
            this.t_haslo.TabIndex = 3;
            // 
            // l_capslock
            // 
            this.l_capslock.AutoSize = true;
            this.l_capslock.Location = new System.Drawing.Point(77, 96);
            this.l_capslock.Name = "l_capslock";
            this.l_capslock.Size = new System.Drawing.Size(131, 13);
            this.l_capslock.TabIndex = 6;
            this.l_capslock.Text = "CAPSLOCK jest włączony";
            // 
            // t_nazwa
            // 
            this.t_nazwa.Location = new System.Drawing.Point(92, 13);
            this.t_nazwa.Name = "t_nazwa";
            this.t_nazwa.Size = new System.Drawing.Size(248, 20);
            this.t_nazwa.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 42);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(39, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Hasło:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(43, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Nazwa:";
            // 
            // dgv_listaKlas
            // 
            this.dgv_listaKlas.AllowUserToAddRows = false;
            this.dgv_listaKlas.AllowUserToDeleteRows = false;
            this.dgv_listaKlas.AllowUserToOrderColumns = true;
            this.dgv_listaKlas.AllowUserToResizeColumns = false;
            this.dgv_listaKlas.AllowUserToResizeRows = false;
            this.dgv_listaKlas.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.dgv_listaKlas.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_listaKlas.Location = new System.Drawing.Point(372, 12);
            this.dgv_listaKlas.MultiSelect = false;
            this.dgv_listaKlas.Name = "dgv_listaKlas";
            this.dgv_listaKlas.ReadOnly = true;
            this.dgv_listaKlas.RowHeadersVisible = false;
            this.dgv_listaKlas.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dgv_listaKlas.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv_listaKlas.Size = new System.Drawing.Size(244, 356);
            this.dgv_listaKlas.TabIndex = 16;
            this.dgv_listaKlas.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_listaKlas_CellClick);
            // 
            // gb_powiazanieKontaZPoczta
            // 
            this.gb_powiazanieKontaZPoczta.Controls.Add(this.label3);
            this.gb_powiazanieKontaZPoczta.Controls.Add(this.cb_wymagajHasla);
            this.gb_powiazanieKontaZPoczta.Controls.Add(this.b_zalogujMail);
            this.gb_powiazanieKontaZPoczta.Controls.Add(this.label6);
            this.gb_powiazanieKontaZPoczta.Controls.Add(this.t_hasloMail);
            this.gb_powiazanieKontaZPoczta.Controls.Add(this.label5);
            this.gb_powiazanieKontaZPoczta.Controls.Add(this.label4);
            this.gb_powiazanieKontaZPoczta.Controls.Add(this.t_loginMail);
            this.gb_powiazanieKontaZPoczta.Location = new System.Drawing.Point(12, 187);
            this.gb_powiazanieKontaZPoczta.Name = "gb_powiazanieKontaZPoczta";
            this.gb_powiazanieKontaZPoczta.Size = new System.Drawing.Size(354, 155);
            this.gb_powiazanieKontaZPoczta.TabIndex = 19;
            this.gb_powiazanieKontaZPoczta.TabStop = false;
            this.gb_powiazanieKontaZPoczta.Text = "Powiązanie konta z pocztą e-mail";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(7, 122);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(131, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "CAPSLOCK jest włączony";
            // 
            // cb_wymagajHasla
            // 
            this.cb_wymagajHasla.AutoSize = true;
            this.cb_wymagajHasla.Location = new System.Drawing.Point(91, 91);
            this.cb_wymagajHasla.Name = "cb_wymagajHasla";
            this.cb_wymagajHasla.Size = new System.Drawing.Size(227, 17);
            this.cb_wymagajHasla.TabIndex = 9;
            this.cb_wymagajHasla.Text = "wymagaj hasła przy wysyłaniu wiadomości";
            this.cb_wymagajHasla.UseVisualStyleBackColor = true;
            // 
            // b_zalogujMail
            // 
            this.b_zalogujMail.Location = new System.Drawing.Point(265, 117);
            this.b_zalogujMail.Name = "b_zalogujMail";
            this.b_zalogujMail.Size = new System.Drawing.Size(75, 23);
            this.b_zalogujMail.TabIndex = 7;
            this.b_zalogujMail.Text = "Zaloguj";
            this.b_zalogujMail.UseVisualStyleBackColor = true;
            this.b_zalogujMail.Click += new System.EventHandler(this.b_zalogujMail_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(7, 68);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(39, 13);
            this.label6.TabIndex = 4;
            this.label6.Text = "Hasło:";
            // 
            // t_hasloMail
            // 
            this.t_hasloMail.Location = new System.Drawing.Point(91, 65);
            this.t_hasloMail.Name = "t_hasloMail";
            this.t_hasloMail.Size = new System.Drawing.Size(249, 20);
            this.t_hasloMail.TabIndex = 3;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(275, 34);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(65, 13);
            this.label5.TabIndex = 2;
            this.label5.Text = "@gmail.com";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(7, 38);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(36, 13);
            this.label4.TabIndex = 1;
            this.label4.Text = "Login:";
            // 
            // t_loginMail
            // 
            this.t_loginMail.Location = new System.Drawing.Point(91, 31);
            this.t_loginMail.Name = "t_loginMail";
            this.t_loginMail.Size = new System.Drawing.Size(178, 20);
            this.t_loginMail.TabIndex = 0;
            // 
            // gb_mailZalogowany
            // 
            this.gb_mailZalogowany.Controls.Add(this.checkBox2);
            this.gb_mailZalogowany.Controls.Add(this.b_wyloguj);
            this.gb_mailZalogowany.Controls.Add(this.l_mail);
            this.gb_mailZalogowany.Controls.Add(this.label9);
            this.gb_mailZalogowany.Location = new System.Drawing.Point(9, 190);
            this.gb_mailZalogowany.Name = "gb_mailZalogowany";
            this.gb_mailZalogowany.Size = new System.Drawing.Size(357, 108);
            this.gb_mailZalogowany.TabIndex = 20;
            this.gb_mailZalogowany.TabStop = false;
            this.gb_mailZalogowany.Text = "Powiązanie konta z pocztą e-mail";
            // 
            // checkBox2
            // 
            this.checkBox2.AutoSize = true;
            this.checkBox2.Location = new System.Drawing.Point(16, 39);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(227, 17);
            this.checkBox2.TabIndex = 10;
            this.checkBox2.Text = "wymagaj hasła przy wysyłaniu wiadomości";
            this.checkBox2.UseVisualStyleBackColor = true;
            // 
            // b_wyloguj
            // 
            this.b_wyloguj.Location = new System.Drawing.Point(265, 68);
            this.b_wyloguj.Name = "b_wyloguj";
            this.b_wyloguj.Size = new System.Drawing.Size(75, 23);
            this.b_wyloguj.TabIndex = 7;
            this.b_wyloguj.Text = "wyloguj";
            this.b_wyloguj.UseVisualStyleBackColor = true;
            this.b_wyloguj.Click += new System.EventHandler(this.b_wyloguj_Click);
            // 
            // l_mail
            // 
            this.l_mail.AutoSize = true;
            this.l_mail.Location = new System.Drawing.Point(46, 23);
            this.l_mail.Name = "l_mail";
            this.l_mail.Size = new System.Drawing.Size(157, 13);
            this.l_mail.TabIndex = 2;
            this.l_mail.Text = "przykladowe_konto@gmail.com";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(13, 23);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(36, 13);
            this.label9.TabIndex = 1;
            this.label9.Text = "Login:";
            // 
            // b_edytujDane
            // 
            this.b_edytujDane.Location = new System.Drawing.Point(12, 12);
            this.b_edytujDane.Name = "b_edytujDane";
            this.b_edytujDane.Size = new System.Drawing.Size(354, 23);
            this.b_edytujDane.TabIndex = 21;
            this.b_edytujDane.Text = "Edytuj dane";
            this.b_edytujDane.UseVisualStyleBackColor = true;
            this.b_edytujDane.Click += new System.EventHandler(this.b_edytujDane_Click);
            // 
            // b_usunKlase
            // 
            this.b_usunKlase.Location = new System.Drawing.Point(529, 374);
            this.b_usunKlase.Name = "b_usunKlase";
            this.b_usunKlase.Size = new System.Drawing.Size(75, 23);
            this.b_usunKlase.TabIndex = 22;
            this.b_usunKlase.Text = "Usun";
            this.b_usunKlase.UseVisualStyleBackColor = true;
            this.b_usunKlase.Click += new System.EventHandler(this.b_usunKlase_Click);
            // 
            // fListaKlas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(616, 426);
            this.Controls.Add(this.b_usunKlase);
            this.Controls.Add(this.b_edytujDane);
            this.Controls.Add(this.gb_mailZalogowany);
            this.Controls.Add(this.dgv_listaKlas);
            this.Controls.Add(this.gb_powiazanieKontaZPoczta);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.statusStrip1);
            this.Name = "fListaKlas";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "fListaKlas";
            this.Load += new System.EventHandler(this.fListaKlas_Load);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_listaKlas)).EndInit();
            this.gb_powiazanieKontaZPoczta.ResumeLayout(false);
            this.gb_powiazanieKontaZPoczta.PerformLayout();
            this.gb_mailZalogowany.ResumeLayout(false);
            this.gb_mailZalogowany.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        protected System.Windows.Forms.StatusStrip statusStrip1;
        protected System.Windows.Forms.ToolStripStatusLabel s_statusInternetu;
        protected System.Windows.Forms.GroupBox groupBox1;
        protected System.Windows.Forms.TextBox t_powtorzHaslo;
        protected System.Windows.Forms.Label label8;
        protected System.Windows.Forms.Button b_dodaj;
        protected System.Windows.Forms.TextBox t_haslo;
        protected System.Windows.Forms.Label l_capslock;
        protected System.Windows.Forms.TextBox t_nazwa;
        protected System.Windows.Forms.Label label2;
        protected System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox gb_powiazanieKontaZPoczta;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox cb_wymagajHasla;
        private System.Windows.Forms.Button b_zalogujMail;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox t_hasloMail;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox t_loginMail;
        private System.Windows.Forms.GroupBox gb_mailZalogowany;
        private System.Windows.Forms.CheckBox checkBox2;
        private System.Windows.Forms.Button b_wyloguj;
        private System.Windows.Forms.Label l_mail;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button b_edytujDane;
        private System.Windows.Forms.Button b_usunKlase;
        private System.Windows.Forms.DataGridView dgv_listaKlas;
    }
}