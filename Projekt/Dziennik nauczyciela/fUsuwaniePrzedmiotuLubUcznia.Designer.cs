namespace Dziennik_nauczyciela
{
    partial class fUsuwaniePrzedmiotuLubUcznia
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
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.b_usun = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.dgv_lista = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_lista)).BeginInit();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(259, 33);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(127, 20);
            this.textBox1.TabIndex = 1;
            // 
            // b_usun
            // 
            this.b_usun.Location = new System.Drawing.Point(311, 59);
            this.b_usun.Name = "b_usun";
            this.b_usun.Size = new System.Drawing.Size(75, 23);
            this.b_usun.TabIndex = 2;
            this.b_usun.Text = "Usun";
            this.b_usun.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(259, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(127, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Podaj haslo uzytkownika:";
            // 
            // dgv_lista
            // 
            this.dgv_lista.AllowUserToAddRows = false;
            this.dgv_lista.AllowUserToDeleteRows = false;
            this.dgv_lista.AllowUserToOrderColumns = true;
            this.dgv_lista.AllowUserToResizeColumns = false;
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
            this.dgv_lista.Size = new System.Drawing.Size(241, 165);
            this.dgv_lista.TabIndex = 17;
            // 
            // fUsuwaniePrzedmiotuLubUcznia
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(405, 165);
            this.Controls.Add(this.dgv_lista);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.b_usun);
            this.Controls.Add(this.textBox1);
            this.Name = "fUsuwaniePrzedmiotuLubUcznia";
            this.Text = "fUsuwaniePrzedmiotuUcznia";
            ((System.ComponentModel.ISupportInitialize)(this.dgv_lista)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button b_usun;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dgv_lista;
    }
}