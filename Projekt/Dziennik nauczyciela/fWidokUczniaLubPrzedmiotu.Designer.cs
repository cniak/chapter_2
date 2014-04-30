namespace Dziennik_nauczyciela
{
    partial class fWidokUczniaLubPrzedmiotu
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
            this.tc_widok = new System.Windows.Forms.TabControl();
            this.tp_dodaj = new System.Windows.Forms.TabPage();
            this.tp_edytuj = new System.Windows.Forms.TabPage();
            this.tp_usun = new System.Windows.Forms.TabPage();
            this.l_zaznaczElementDoUsuniecia = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.t_usun = new System.Windows.Forms.TextBox();
            this.dgv_lista = new System.Windows.Forms.DataGridView();
            this.b_usun1 = new System.Windows.Forms.Button();
            this.tc_widok.SuspendLayout();
            this.tp_usun.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_lista)).BeginInit();
            this.SuspendLayout();
            // 
            // tc_widok
            // 
            this.tc_widok.Controls.Add(this.tp_dodaj);
            this.tc_widok.Controls.Add(this.tp_edytuj);
            this.tc_widok.Controls.Add(this.tp_usun);
            this.tc_widok.Dock = System.Windows.Forms.DockStyle.Right;
            this.tc_widok.Location = new System.Drawing.Point(183, 0);
            this.tc_widok.Name = "tc_widok";
            this.tc_widok.SelectedIndex = 0;
            this.tc_widok.Size = new System.Drawing.Size(405, 303);
            this.tc_widok.TabIndex = 0;
            // 
            // tp_dodaj
            // 
            this.tp_dodaj.Location = new System.Drawing.Point(4, 22);
            this.tp_dodaj.Name = "tp_dodaj";
            this.tp_dodaj.Padding = new System.Windows.Forms.Padding(3);
            this.tp_dodaj.Size = new System.Drawing.Size(397, 277);
            this.tp_dodaj.TabIndex = 0;
            this.tp_dodaj.Text = "Dodaj";
            this.tp_dodaj.UseVisualStyleBackColor = true;
            // 
            // tp_edytuj
            // 
            this.tp_edytuj.Location = new System.Drawing.Point(4, 22);
            this.tp_edytuj.Name = "tp_edytuj";
            this.tp_edytuj.Padding = new System.Windows.Forms.Padding(3);
            this.tp_edytuj.Size = new System.Drawing.Size(397, 277);
            this.tp_edytuj.TabIndex = 1;
            this.tp_edytuj.Text = "Edytuj";
            this.tp_edytuj.UseVisualStyleBackColor = true;
            // 
            // tp_usun
            // 
            this.tp_usun.Controls.Add(this.b_usun1);
            this.tp_usun.Controls.Add(this.l_zaznaczElementDoUsuniecia);
            this.tp_usun.Controls.Add(this.label2);
            this.tp_usun.Controls.Add(this.t_usun);
            this.tp_usun.Location = new System.Drawing.Point(4, 22);
            this.tp_usun.Name = "tp_usun";
            this.tp_usun.Padding = new System.Windows.Forms.Padding(3);
            this.tp_usun.Size = new System.Drawing.Size(397, 277);
            this.tp_usun.TabIndex = 2;
            this.tp_usun.Text = "Usun";
            this.tp_usun.UseVisualStyleBackColor = true;
            // 
            // l_zaznaczElementDoUsuniecia
            // 
            this.l_zaznaczElementDoUsuniecia.AutoSize = true;
            this.l_zaznaczElementDoUsuniecia.ForeColor = System.Drawing.Color.Red;
            this.l_zaznaczElementDoUsuniecia.Location = new System.Drawing.Point(6, 50);
            this.l_zaznaczElementDoUsuniecia.Name = "l_zaznaczElementDoUsuniecia";
            this.l_zaznaczElementDoUsuniecia.Size = new System.Drawing.Size(88, 13);
            this.l_zaznaczElementDoUsuniecia.TabIndex = 19;
            this.l_zaznaczElementDoUsuniecia.Text = "Zaznacz element";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 3);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(215, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Zaznacz element i podaj haslo uzytkownika:";
            // 
            // t_usun
            // 
            this.t_usun.BackColor = System.Drawing.SystemColors.Window;
            this.t_usun.Location = new System.Drawing.Point(9, 19);
            this.t_usun.Name = "t_usun";
            this.t_usun.Size = new System.Drawing.Size(172, 20);
            this.t_usun.TabIndex = 3;
            this.t_usun.TextChanged += new System.EventHandler(this.t_usun_TextChanged);
            // 
            // dgv_lista
            // 
            this.dgv_lista.AllowUserToAddRows = false;
            this.dgv_lista.AllowUserToDeleteRows = false;
            this.dgv_lista.AllowUserToOrderColumns = true;
            this.dgv_lista.AllowUserToResizeRows = false;
            this.dgv_lista.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgv_lista.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.dgv_lista.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_lista.Dock = System.Windows.Forms.DockStyle.Left;
            this.dgv_lista.Location = new System.Drawing.Point(0, 0);
            this.dgv_lista.MultiSelect = false;
            this.dgv_lista.Name = "dgv_lista";
            this.dgv_lista.ReadOnly = true;
            this.dgv_lista.RowHeadersVisible = false;
            this.dgv_lista.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv_lista.Size = new System.Drawing.Size(181, 303);
            this.dgv_lista.TabIndex = 18;
            this.dgv_lista.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_lista_CellClick);
            // 
            // b_usun1
            // 
            this.b_usun1.Location = new System.Drawing.Point(106, 45);
            this.b_usun1.Name = "b_usun1";
            this.b_usun1.Size = new System.Drawing.Size(75, 23);
            this.b_usun1.TabIndex = 20;
            this.b_usun1.Text = "Usun";
            this.b_usun1.UseVisualStyleBackColor = true;
            this.b_usun1.Click += new System.EventHandler(this.b_usun1_Click);
            // 
            // fWidokUczniaLubPrzedmiotu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(588, 303);
            this.Controls.Add(this.dgv_lista);
            this.Controls.Add(this.tc_widok);
            this.Name = "fWidokUczniaLubPrzedmiotu";
            this.Text = "fWidokUczniaLubPrzedmiotu";
            this.tc_widok.ResumeLayout(false);
            this.tp_usun.ResumeLayout(false);
            this.tp_usun.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_lista)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.TabControl tc_widok;
        public System.Windows.Forms.TabPage tp_dodaj;
        public System.Windows.Forms.TabPage tp_edytuj;
        public System.Windows.Forms.TabPage tp_usun;
        public System.Windows.Forms.DataGridView dgv_lista;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox t_usun;
        private System.Windows.Forms.Label l_zaznaczElementDoUsuniecia;
        private System.Windows.Forms.Button b_usun1;
    }
}