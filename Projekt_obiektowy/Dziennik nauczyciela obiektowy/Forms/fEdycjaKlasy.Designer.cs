namespace Dziennik_nauczyciela_obiektowy.Forms
{
    partial class fEdycjaKlasy
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
            this.cb_listaUczniow = new System.Windows.Forms.ComboBox();
            this.b_zapiszZmiany = new System.Windows.Forms.Button();
            this.t_nauczyciel = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.t_nazwaKlasy = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // cb_listaUczniow
            // 
            this.cb_listaUczniow.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_listaUczniow.FormattingEnabled = true;
            this.cb_listaUczniow.Location = new System.Drawing.Point(129, 64);
            this.cb_listaUczniow.Name = "cb_listaUczniow";
            this.cb_listaUczniow.Size = new System.Drawing.Size(121, 21);
            this.cb_listaUczniow.TabIndex = 28;
            // 
            // b_zapiszZmiany
            // 
            this.b_zapiszZmiany.Location = new System.Drawing.Point(193, 91);
            this.b_zapiszZmiany.Name = "b_zapiszZmiany";
            this.b_zapiszZmiany.Size = new System.Drawing.Size(57, 23);
            this.b_zapiszZmiany.TabIndex = 27;
            this.b_zapiszZmiany.Text = "Zapisz";
            this.b_zapiszZmiany.UseVisualStyleBackColor = true;
            this.b_zapiszZmiany.Click += new System.EventHandler(this.b_zapiszZmiany_Click);
            // 
            // t_nauczyciel
            // 
            this.t_nauczyciel.Enabled = false;
            this.t_nauczyciel.Location = new System.Drawing.Point(129, 6);
            this.t_nauczyciel.Name = "t_nauczyciel";
            this.t_nauczyciel.Size = new System.Drawing.Size(121, 20);
            this.t_nauczyciel.TabIndex = 26;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(12, 9);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(68, 13);
            this.label7.TabIndex = 25;
            this.label7.Text = "Prowadzący:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(11, 67);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(61, 13);
            this.label6.TabIndex = 24;
            this.label6.Text = "Gospodarz:";
            // 
            // t_nazwaKlasy
            // 
            this.t_nazwaKlasy.Location = new System.Drawing.Point(129, 32);
            this.t_nazwaKlasy.Name = "t_nazwaKlasy";
            this.t_nazwaKlasy.Size = new System.Drawing.Size(121, 20);
            this.t_nazwaKlasy.TabIndex = 23;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 35);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 13);
            this.label1.TabIndex = 22;
            this.label1.Text = "Nazwa klasy:";
            // 
            // fEdycjaKlasy
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(257, 124);
            this.Controls.Add(this.cb_listaUczniow);
            this.Controls.Add(this.b_zapiszZmiany);
            this.Controls.Add(this.t_nauczyciel);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.t_nazwaKlasy);
            this.Controls.Add(this.label1);
            this.Name = "fEdycjaKlasy";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "fEdycjaKlas";
            this.Load += new System.EventHandler(this.fEdycjaKlasy_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cb_listaUczniow;
        private System.Windows.Forms.Button b_zapiszZmiany;
        private System.Windows.Forms.TextBox t_nauczyciel;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox t_nazwaKlasy;
        private System.Windows.Forms.Label label1;
    }
}