using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using RNA.Hopfield;

namespace RNATest
{
    public partial class frmRedHopfield : Form
    {
        RedHopfield redHP = new RedHopfield();
        List<TextBox> valoresNeuronas = new List<TextBox>();
        List<Label> lblValores = new List<Label>();

        public frmRedHopfield()
        {
            InitializeComponent();
        }

        private void btnInicializar_Click(object sender, EventArgs e)
        {
            this.LimpiarControles();

            redHP.Inicializar(Int32.Parse(cbmNeuronas.SelectedItem.ToString()));

            int neuronas = Int32.Parse(cbmNeuronas.SelectedItem.ToString());

            for (int v = 1; v <= neuronas; v++)
            {
                Label label = new Label();
                label.Name = "lblNeurona" + v.ToString();
                label.Text = "Valor Neurona " + v.ToString();
                label.Left = lblNeuronas.Left;
                label.Top = (lblNeuronas.Top * 2 + lblNeuronas.Height) * v;
                label.Tag = v;
                this.Controls.Add(label);

                this.lblValores.Add(label);

                TextBox txtValor = new TextBox();
                txtValor.Name = "txtValor" + v.ToString();
                txtValor.Left = label.Left + label.Width + 10;
                txtValor.Top = label.Top;
                txtValor.Tag = v;
                this.Controls.Add(txtValor);

                this.valoresNeuronas.Add(txtValor);
            }
        }
       
        private void btnAprender_Click(object sender, EventArgs e)
        {
            int[] valores = new int[valoresNeuronas.Count]; 

            foreach(TextBox t in valoresNeuronas)
            {
                valores[Int32.Parse(t.Tag.ToString()) - 1] = Int32.Parse(t.Text);
            }

            redHP.ColocarEntrada(valores);

            redHP.Aprender("Prueba");

            string aprendido = string.Empty;
            for (int n = 0; n < valores.Length; n++ )
                aprendido += valores[n].ToString() + ", ";

            lstPatronesAprendidos.Items.Add(aprendido);
        }

        private void LimpiarControles()
        {
            foreach (TextBox t in valoresNeuronas)
            {
                t.Dispose();
                this.Controls.Remove(t);
            }

            foreach (Label l in lblValores)
            {
                l.Dispose();
                this.Controls.Remove(l);
            }

            if (valoresNeuronas.Count > 0)
                valoresNeuronas.Clear();

            if (lblValores.Count > 0)
                lblValores.Clear();
        }

        private void btnIdentificar_Click(object sender, EventArgs e)
        {
            int[] valores = new int[valoresNeuronas.Count];

            foreach (TextBox t in valoresNeuronas)
            {
                valores[Int32.Parse(t.Tag.ToString()) - 1] = Int32.Parse(t.Text);
            }

            redHP.ColocarEntrada(valores);

            try
            {
                int[] patronReconocido = new int[valoresNeuronas.Count];
                int resultado = redHP.IdentificarPatron(out patronReconocido);

                string patronRec = string.Empty;

                for (int i = 0; i < valoresNeuronas.Count; i++)
                    patronRec += patronReconocido[i].ToString() + ", ";

                if(resultado == 0)
                    MessageBox.Show("Patrón reconocido: " + patronRec.Substring(0, patronRec.Length - 2));
                else
                    MessageBox.Show("Patrón no estabilizado. Se reconoce como: " + patronRec.Substring(0, patronRec.Length - 2));
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}