namespace RNATest
{
    partial class frmTestBackPropagation
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
            this.btnAprender = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtCantEntrada = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtCantOculta = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtCantSalida = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtEntrada1 = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtEntrada2 = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtSalida = new System.Windows.Forms.TextBox();
            this.lstSalidas = new System.Windows.Forms.ListBox();
            this.lblSalidas = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.lstErroresRed = new System.Windows.Forms.ListBox();
            this.btnInicializar = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnAprender
            // 
            this.btnAprender.Location = new System.Drawing.Point(18, 250);
            this.btnAprender.Name = "btnAprender";
            this.btnAprender.Size = new System.Drawing.Size(146, 58);
            this.btnAprender.TabIndex = 0;
            this.btnAprender.Text = "Entrenar";
            this.btnAprender.UseVisualStyleBackColor = true;
            this.btnAprender.Click += new System.EventHandler(this.btnAprender_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 45);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Capa Entrada";
            // 
            // txtCantEntrada
            // 
            this.txtCantEntrada.Location = new System.Drawing.Point(87, 42);
            this.txtCantEntrada.Name = "txtCantEntrada";
            this.txtCantEntrada.Size = new System.Drawing.Size(64, 20);
            this.txtCantEntrada.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(15, 71);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(66, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Capa Oculta";
            // 
            // txtCantOculta
            // 
            this.txtCantOculta.Location = new System.Drawing.Point(87, 68);
            this.txtCantOculta.Name = "txtCantOculta";
            this.txtCantOculta.Size = new System.Drawing.Size(64, 20);
            this.txtCantOculta.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(15, 97);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(66, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Capa Salída";
            // 
            // txtCantSalida
            // 
            this.txtCantSalida.Location = new System.Drawing.Point(87, 94);
            this.txtCantSalida.Name = "txtCantSalida";
            this.txtCantSalida.Size = new System.Drawing.Size(64, 20);
            this.txtCantSalida.TabIndex = 6;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(15, 19);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Neuronas";
            // 
            // txtEntrada1
            // 
            this.txtEntrada1.Location = new System.Drawing.Point(188, 62);
            this.txtEntrada1.Name = "txtEntrada1";
            this.txtEntrada1.Size = new System.Drawing.Size(79, 20);
            this.txtEntrada1.TabIndex = 9;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(185, 42);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(82, 13);
            this.label5.TabIndex = 10;
            this.label5.Text = "Valores Entrada";
            // 
            // txtEntrada2
            // 
            this.txtEntrada2.Location = new System.Drawing.Point(188, 89);
            this.txtEntrada2.Name = "txtEntrada2";
            this.txtEntrada2.Size = new System.Drawing.Size(79, 20);
            this.txtEntrada2.TabIndex = 11;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(289, 42);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(75, 13);
            this.label6.TabIndex = 12;
            this.label6.Text = "Valor deseado";
            // 
            // txtSalida
            // 
            this.txtSalida.Location = new System.Drawing.Point(292, 62);
            this.txtSalida.Name = "txtSalida";
            this.txtSalida.Size = new System.Drawing.Size(73, 20);
            this.txtSalida.TabIndex = 13;
            // 
            // lstSalidas
            // 
            this.lstSalidas.FormattingEnabled = true;
            this.lstSalidas.Location = new System.Drawing.Point(188, 163);
            this.lstSalidas.Name = "lstSalidas";
            this.lstSalidas.Size = new System.Drawing.Size(79, 147);
            this.lstSalidas.TabIndex = 14;
            // 
            // lblSalidas
            // 
            this.lblSalidas.AutoSize = true;
            this.lblSalidas.Location = new System.Drawing.Point(188, 144);
            this.lblSalidas.Name = "lblSalidas";
            this.lblSalidas.Size = new System.Drawing.Size(41, 13);
            this.lblSalidas.TabIndex = 15;
            this.lblSalidas.Text = "Salidas";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(280, 144);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(84, 13);
            this.label8.TabIndex = 16;
            this.label8.Text = "Errores de la red";
            // 
            // lstErroresRed
            // 
            this.lstErroresRed.FormattingEnabled = true;
            this.lstErroresRed.Location = new System.Drawing.Point(285, 163);
            this.lstErroresRed.Name = "lstErroresRed";
            this.lstErroresRed.Size = new System.Drawing.Size(79, 147);
            this.lstErroresRed.TabIndex = 17;
            // 
            // btnInicializar
            // 
            this.btnInicializar.Location = new System.Drawing.Point(18, 173);
            this.btnInicializar.Name = "btnInicializar";
            this.btnInicializar.Size = new System.Drawing.Size(146, 59);
            this.btnInicializar.TabIndex = 18;
            this.btnInicializar.Text = "Inicializar";
            this.btnInicializar.UseVisualStyleBackColor = true;
            this.btnInicializar.Click += new System.EventHandler(this.btnInicializar_Click);
            // 
            // frmTestBackPropagation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(403, 320);
            this.Controls.Add(this.btnInicializar);
            this.Controls.Add(this.lstErroresRed);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.lblSalidas);
            this.Controls.Add(this.lstSalidas);
            this.Controls.Add(this.txtSalida);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txtEntrada2);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtEntrada1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtCantSalida);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtCantOculta);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtCantEntrada);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnAprender);
            this.Name = "frmTestBackPropagation";
            this.Text = "frmTestBackPropagation";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnAprender;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtCantEntrada;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtCantOculta;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtCantSalida;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtEntrada1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtEntrada2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtSalida;
        private System.Windows.Forms.ListBox lstSalidas;
        private System.Windows.Forms.Label lblSalidas;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ListBox lstErroresRed;
        private System.Windows.Forms.Button btnInicializar;
    }
}