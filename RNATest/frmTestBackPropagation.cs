using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using RNA.RedBackPropagation;
using System.Windows.Forms.DataVisualization.Charting;

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
            double salida = -2;
            double error = 0;
            Series series = new Series();
            series = chtErrores.Series.Add("Errores");

            while (double.Parse(txtSalida.Text) != salida)
            {
                redBP.FeedFordward();
                salida = redBP.ObtenerSalida(0);

                lstSalidas.Items.Add(salida);

                redBP.BackPropagate();
                salida = redBP.ObtenerSalida(0);

                error = redBP.CalcularErrorRed();
                lstErroresRed.Items.Add(error);
                series.ChartType = SeriesChartType.Line;
                series.Points.Add(error);
            }
        }

        private void btnInicializar_Click(object sender, EventArgs e)
        {
            redBP.Inicializa(int.Parse(txtCantEntrada.Text), int.Parse(txtCantOculta.Text), int.Parse(txtCantSalida.Text), double.Parse(txtFactorEntrenamiento.Text));
            redBP.ColocarEntrada(0, double.Parse(txtEntrada1.Text));
            redBP.ColocarEntrada(1, double.Parse(txtEntrada2.Text));
            redBP.ColocarSalidaDeseada(0, double.Parse(this.txtSalida.Text));    
        }
    }
}
