namespace Dziennik_nauczyciela
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
            this.dgv_listaUzytkownikow = new System.Windows.Forms.DataGridView();
            this.groupBox1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_listaUzytkownikow)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.tableLayoutPanel1);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(299, 149);
            this.groupBox1.TabIndex = 3;
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
            this.tableLayoutPanel1.Controls.Add(this.b_dodaj, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.l_uzupelnijDane, 0, 2);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 16);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 31F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 11F));
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
            this.t_nazwaUzytkownika.TextChanged += new System.EventHandler(this.t_nazwaUzytkownika_TextChanged);
            this.t_nazwaUzytkownika.Enter += new System.EventHandler(this.t_nazwaUzytkownika_Enter);
            // 
            // t_haslo
            // 
            this.t_haslo.Dock = System.Windows.Forms.DockStyle.Top;
            this.t_haslo.Location = new System.Drawing.Point(149, 34);
            this.t_haslo.Name = "t_haslo";
            this.t_haslo.Size = new System.Drawing.Size(141, 20);
            this.t_haslo.TabIndex = 2;
            this.t_haslo.TextChanged += new System.EventHandler(this.t_haslo_TextChanged);
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
            // dgv_listaUzytkownikow
            // 
            this.dgv_listaUzytkownikow.AllowUserToAddRows = false;
            this.dgv_listaUzytkownikow.AllowUserToDeleteRows = false;
            this.dgv_listaUzytkownikow.AllowUserToOrderColumns = true;
            this.dgv_listaUzytkownikow.AllowUserToResizeColumns = false;
            this.dgv_listaUzytkownikow.AllowUserToResizeRows = false;
            this.dgv_listaUzytkownikow.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_listaUzytkownikow.Dock = System.Windows.Forms.DockStyle.Right;
            this.dgv_listaUzytkownikow.Location = new System.Drawing.Point(322, 0);
            this.dgv_listaUzytkownikow.MultiSelect = false;
            this.dgv_listaUzytkownikow.Name = "dgv_listaUzytkownikow";
            this.dgv_listaUzytkownikow.ReadOnly = true;
            this.dgv_listaUzytkownikow.RowHeadersVisible = false;
            this.dgv_listaUzytkownikow.Size = new System.Drawing.Size(179, 239);
            this.dgv_listaUzytkownikow.TabIndex = 2;
            this.dgv_listaUzytkownikow.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_listaUzytkownikow_CellDoubleClick);
            // 
            // fStart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(501, 239);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.dgv_listaUzytkownikow);
            this.Name = "fStart";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Logowanie do systemu";
            this.Load += new System.EventHandler(this.fStart_Load);
            this.groupBox1.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_listaUzytkownikow)).EndInit();
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
        private System.Windows.Forms.DataGridView dgv_listaUzytkownikow;

    }
}

