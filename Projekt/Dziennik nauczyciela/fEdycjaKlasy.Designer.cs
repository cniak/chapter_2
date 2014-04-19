namespace Dziennik_nauczyciela
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
            this.b_zapiszZmiany = new System.Windows.Forms.Button();
            this.t_nauczyciel = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.t_nazwaKlasy = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.l_gospodarzImieNazwisko = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // b_zapiszZmiany
            // 
            this.b_zapiszZmiany.Location = new System.Drawing.Point(81, 127);
            this.b_zapiszZmiany.Name = "b_zapiszZmiany";
            this.b_zapiszZmiany.Size = new System.Drawing.Size(148, 23);
            this.b_zapiszZmiany.TabIndex = 19;
            this.b_zapiszZmiany.Text = "Zapisz zmiany";
            this.b_zapiszZmiany.UseVisualStyleBackColor = true;
            this.b_zapiszZmiany.Click += new System.EventHandler(this.b_zapiszZmiany_Click);
            // 
            // t_nauczyciel
            // 
            this.t_nauczyciel.Enabled = false;
            this.t_nauczyciel.Location = new System.Drawing.Point(129, 6);
            this.t_nauczyciel.Name = "t_nauczyciel";
            this.t_nauczyciel.Size = new System.Drawing.Size(100, 20);
            this.t_nauczyciel.TabIndex = 18;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(12, 9);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(68, 13);
            this.label7.TabIndex = 17;
            this.label7.Text = "Prowadzący:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(11, 67);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(61, 13);
            this.label6.TabIndex = 16;
            this.label6.Text = "Gospodarz:";
            // 
            // t_nazwaKlasy
            // 
            this.t_nazwaKlasy.Location = new System.Drawing.Point(129, 32);
            this.t_nazwaKlasy.Name = "t_nazwaKlasy";
            this.t_nazwaKlasy.Size = new System.Drawing.Size(100, 20);
            this.t_nazwaKlasy.TabIndex = 14;
            this.t_nazwaKlasy.TextChanged += new System.EventHandler(this.t_nazwaKlasy_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 35);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 13);
            this.label1.TabIndex = 13;
            this.label1.Text = "Nazwa klasy:";
            // 
            // l_gospodarzImieNazwisko
            // 
            this.l_gospodarzImieNazwisko.AutoSize = true;
            this.l_gospodarzImieNazwisko.Location = new System.Drawing.Point(126, 67);
            this.l_gospodarzImieNazwisko.Name = "l_gospodarzImieNazwisko";
            this.l_gospodarzImieNazwisko.Size = new System.Drawing.Size(35, 13);
            this.l_gospodarzImieNazwisko.TabIndex = 20;
            this.l_gospodarzImieNazwisko.Text = "label2";
            // 
            // fEdycjaKlasy
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(464, 161);
            this.Controls.Add(this.l_gospodarzImieNazwisko);
            this.Controls.Add(this.b_zapiszZmiany);
            this.Controls.Add(this.t_nauczyciel);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.t_nazwaKlasy);
            this.Controls.Add(this.label1);
            this.Name = "fEdycjaKlasy";
            this.Text = "fEdycjaKlasy";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button b_zapiszZmiany;
        private System.Windows.Forms.TextBox t_nauczyciel;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox t_nazwaKlasy;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label l_gospodarzImieNazwisko;
    }
}