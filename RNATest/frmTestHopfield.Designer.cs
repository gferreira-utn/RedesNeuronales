namespace RNATest
{
    partial class frmRedHopfield
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
            this.btnInicializar = new System.Windows.Forms.Button();
            this.lblNeuronas = new System.Windows.Forms.Label();
            this.cbmNeuronas = new System.Windows.Forms.ComboBox();
            this.btnAprender = new System.Windows.Forms.Button();
            this.btnIdentificar = new System.Windows.Forms.Button();
            this.lstPatronesAprendidos = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnInicializar
            // 
            this.btnInicializar.Location = new System.Drawing.Point(166, 8);
            this.btnInicializar.Name = "btnInicializar";
            this.btnInicializar.Size = new System.Drawing.Size(75, 23);
            this.btnInicializar.TabIndex = 0;
            this.btnInicializar.Text = "Iniciarlizar";
            this.btnInicializar.UseVisualStyleBackColor = true;
            this.btnInicializar.Click += new System.EventHandler(this.btnInicializar_Click);
            // 
            // lblNeuronas
            // 
            this.lblNeuronas.AutoSize = true;
            this.lblNeuronas.Location = new System.Drawing.Point(13, 13);
            this.lblNeuronas.Name = "lblNeuronas";
            this.lblNeuronas.Size = new System.Drawing.Size(56, 13);
            this.lblNeuronas.TabIndex = 2;
            this.lblNeuronas.Text = "Neuronas:";
            // 
            // cbmNeuronas
            // 
            this.cbmNeuronas.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4"});
            this.cbmNeuronas.Location = new System.Drawing.Point(75, 8);
            this.cbmNeuronas.Name = "cbmNeuronas";
            this.cbmNeuronas.Size = new System.Drawing.Size(85, 21);
            this.cbmNeuronas.TabIndex = 5;
            // 
            // btnAprender
            // 
            this.btnAprender.Location = new System.Drawing.Point(166, 214);
            this.btnAprender.Name = "btnAprender";
            this.btnAprender.Size = new System.Drawing.Size(75, 23);
            this.btnAprender.TabIndex = 4;
            this.btnAprender.Text = "Aprender";
            this.btnAprender.UseVisualStyleBackColor = true;
            this.btnAprender.Click += new System.EventHandler(this.btnAprender_Click);
            // 
            // btnIdentificar
            // 
            this.btnIdentificar.Location = new System.Drawing.Point(16, 214);
            this.btnIdentificar.Name = "btnIdentificar";
            this.btnIdentificar.Size = new System.Drawing.Size(75, 23);
            this.btnIdentificar.TabIndex = 6;
            this.btnIdentificar.Text = "Identificar";
            this.btnIdentificar.UseVisualStyleBackColor = true;
            this.btnIdentificar.Click += new System.EventHandler(this.btnIdentificar_Click);
            // 
            // lstPatronesAprendidos
            // 
            this.lstPatronesAprendidos.FormattingEnabled = true;
            this.lstPatronesAprendidos.Location = new System.Drawing.Point(16, 267);
            this.lstPatronesAprendidos.Name = "lstPatronesAprendidos";
            this.lstPatronesAprendidos.Size = new System.Drawing.Size(225, 160);
            this.lstPatronesAprendidos.TabIndex = 7;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 251);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(104, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "Patrones aprendidos";
            // 
            // frmRedHopfield
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(256, 437);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lstPatronesAprendidos);
            this.Controls.Add(this.btnIdentificar);
            this.Controls.Add(this.btnAprender);
            this.Controls.Add(this.cbmNeuronas);
            this.Controls.Add(this.lblNeuronas);
            this.Controls.Add(this.btnInicializar);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "frmRedHopfield";
            this.Text = "Red de Hopfield";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnInicializar;
        private System.Windows.Forms.Label lblNeuronas;
        private System.Windows.Forms.ComboBox cbmNeuronas;
        private System.Windows.Forms.Button btnAprender;
        private System.Windows.Forms.Button btnIdentificar;
        private System.Windows.Forms.ListBox lstPatronesAprendidos;
        private System.Windows.Forms.Label label1;
    }
}

