namespace Dziennik_nauczyciela
{
    partial class fDodawaniePrzedmiotu
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
            this.t_nazwaPrzedmiotu = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.b_dodaj = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // t_nazwaPrzedmiotu
            // 
            this.t_nazwaPrzedmiotu.Location = new System.Drawing.Point(13, 31);
            this.t_nazwaPrzedmiotu.MaxLength = 25;
            this.t_nazwaPrzedmiotu.Name = "t_nazwaPrzedmiotu";
            this.t_nazwaPrzedmiotu.Size = new System.Drawing.Size(267, 20);
            this.t_nazwaPrzedmiotu.TabIndex = 0;
            this.t_nazwaPrzedmiotu.TextChanged += new System.EventHandler(this.t_nazwaPrzedmiotu_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(97, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Nazwa przedmiotu:";
            // 
            // b_dodaj
            // 
            this.b_dodaj.Location = new System.Drawing.Point(205, 58);
            this.b_dodaj.Name = "b_dodaj";
            this.b_dodaj.Size = new System.Drawing.Size(75, 23);
            this.b_dodaj.TabIndex = 3;
            this.b_dodaj.Text = "Dodaj";
            this.b_dodaj.UseVisualStyleBackColor = true;
            this.b_dodaj.Click += new System.EventHandler(this.b_dodaj_Click);
            // 
            // fDodawaniePrzedmiotu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(292, 93);
            this.Controls.Add(this.b_dodaj);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.t_nazwaPrzedmiotu);
            this.Name = "fDodawaniePrzedmiotu";
            this.Text = "fDodawaniePrzedmiotu";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox t_nazwaPrzedmiotu;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button b_dodaj;
    }
}