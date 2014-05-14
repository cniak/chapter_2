namespace Dziennik_nauczyciela_obiektowy
{
    partial class fStart
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label2 = new System.Windows.Forms.Label();
            this.t_nazwaUzytkownika = new System.Windows.Forms.TextBox();
            this.t_haslo = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.b_dodaj = new System.Windows.Forms.Button();
            this.l_uzupelnijDane = new System.Windows.Forms.Label();
            this.l_capslock = new System.Windows.Forms.Label();
            this.dgv_listaNauczycieli = new System.Windows.Forms.DataGridView();
            this.b_usun = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_listaNauczycieli)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.tableLayoutPanel1);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(299, 149);
            this.groupBox1.TabIndex = 19;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Dodawanie użytkownika";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.label2, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.t_nazwaUzytkownika, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.t_haslo, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.label3, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.b_dodaj, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.l_uzupelnijDane, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.l_capslock, 0, 2);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 16);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 31F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 38F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 8F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(293, 130);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(105, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Nazwa użytkownika:";
            // 
            // t_nazwaUzytkownika
            // 
            this.t_nazwaUzytkownika.Dock = System.Windows.Forms.DockStyle.Top;
            this.t_nazwaUzytkownika.Location = new System.Drawing.Point(149, 3);
            this.t_nazwaUzytkownika.Name = "t_nazwaUzytkownika";
            this.t_nazwaUzytkownika.Size = new System.Drawing.Size(141, 20);
            this.t_nazwaUzytkownika.TabIndex = 1;
            // 
            // t_haslo
            // 
            this.t_haslo.Dock = System.Windows.Forms.DockStyle.Top;
            this.t_haslo.Location = new System.Drawing.Point(149, 34);
            this.t_haslo.Name = "t_haslo";
            this.t_haslo.Size = new System.Drawing.Size(141, 20);
            this.t_haslo.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 31);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(39, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "Hasło:";
            // 
            // b_dodaj
            // 
            this.b_dodaj.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.b_dodaj.Enabled = false;
            this.b_dodaj.Location = new System.Drawing.Point(215, 104);
            this.b_dodaj.Name = "b_dodaj";
            this.b_dodaj.Size = new System.Drawing.Size(75, 23);
            this.b_dodaj.TabIndex = 4;
            this.b_dodaj.Text = "Dodaj";
            this.b_dodaj.UseVisualStyleBackColor = true;
            this.b_dodaj.Click += new System.EventHandler(this.b_dodaj_Click);
            // 
            // l_uzupelnijDane
            // 
            this.l_uzupelnijDane.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.l_uzupelnijDane.AutoSize = true;
            this.l_uzupelnijDane.Location = new System.Drawing.Point(38, 104);
            this.l_uzupelnijDane.Name = "l_uzupelnijDane";
            this.l_uzupelnijDane.Size = new System.Drawing.Size(105, 26);
            this.l_uzupelnijDane.TabIndex = 5;
            this.l_uzupelnijDane.Text = "Uzupełnij dane, aby aktywować przycisk";
            // 
            // l_capslock
            // 
            this.l_capslock.AutoSize = true;
            this.l_capslock.ForeColor = System.Drawing.Color.Red;
            this.l_capslock.Location = new System.Drawing.Point(3, 63);
            this.l_capslock.Name = "l_capslock";
            this.l_capslock.Size = new System.Drawing.Size(63, 13);
            this.l_capslock.TabIndex = 6;
            this.l_capslock.Text = "CAPSLOCK";
            // 
            // dgv_listaNauczycieli
            // 
            this.dgv_listaNauczycieli.AllowUserToAddRows = false;
            this.dgv_listaNauczycieli.AllowUserToDeleteRows = false;
            this.dgv_listaNauczycieli.AllowUserToOrderColumns = true;
            this.dgv_listaNauczycieli.AllowUserToResizeColumns = false;
            this.dgv_listaNauczycieli.AllowUserToResizeRows = false;
            this.dgv_listaNauczycieli.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.dgv_listaNauczycieli.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_listaNauczycieli.Location = new System.Drawing.Point(325, 12);
            this.dgv_listaNauczycieli.MultiSelect = false;
            this.dgv_listaNauczycieli.Name = "dgv_listaNauczycieli";
            this.dgv_listaNauczycieli.ReadOnly = true;
            this.dgv_listaNauczycieli.RowHeadersVisible = false;
            this.dgv_listaNauczycieli.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dgv_listaNauczycieli.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv_listaNauczycieli.Size = new System.Drawing.Size(164, 114);
            this.dgv_listaNauczycieli.TabIndex = 21;

            // 
            // b_usun
            // 
            this.b_usun.Location = new System.Drawing.Point(414, 132);
            this.b_usun.Name = "b_usun";
            this.b_usun.Size = new System.Drawing.Size(75, 23);
            this.b_usun.TabIndex = 20;
            this.b_usun.Text = "Usun";
            this.b_usun.UseVisualStyleBackColor = true;
            this.b_usun.Click += new System.EventHandler(this.b_usun_Click);
            // 
            // fStart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(503, 171);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.dgv_listaNauczycieli);
            this.Controls.Add(this.b_usun);
            this.Name = "fStart";
            this.Text = "fStart";
            this.Load += new System.EventHandler(this.fStart_Load);
            this.groupBox1.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_listaNauczycieli)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox t_nazwaUzytkownika;
        private System.Windows.Forms.TextBox t_haslo;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button b_dodaj;
        private System.Windows.Forms.Label l_uzupelnijDane;
        private System.Windows.Forms.Label l_capslock;
        private System.Windows.Forms.DataGridView dgv_listaNauczycieli;
        private System.Windows.Forms.Button b_usun;

    }
}