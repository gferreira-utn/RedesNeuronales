using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using RNA.RedBackPropagation;

namespace RNATest
{
    public partial class frmTestBackPropagation : Form
    {
        RedBackPropagation redBP = new RedBackPropagation();
        public frmTestBackPropagation()
        {
            InitializeComponent();
        }

        private void btnAprender_Click(object sender, EventArgs e)
        {
            double salida = 0;

            redBP.FeedFordward();
            salida = redBP.ObtenerSalida(0);

            lstSalidas.Items.Add(salida);
            lstErroresRed.Items.Add(redBP.CalcularErrorRed());

            redBP.BackPropagate();
            salida = redBP.ObtenerSalida(0);

            lstSalidas.Items.Add(salida);
            lstErroresRed.Items.Add(redBP.CalcularErrorRed());
        }

        private void btnInicializar_Click(object sender, EventArgs e)
        {
            redBP.Inicializa(2, 3, 1, 3);
            redBP.ColocarEntrada(0, double.Parse(txtEntrada1.Text));
            redBP.ColocarEntrada(1, double.Parse(txtEntrada2.Text));
            redBP.ColocarSalidaDeseada(0, double.Parse(this.txtSalida.Text));
        }
    }
}
