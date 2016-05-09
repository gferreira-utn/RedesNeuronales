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
        Series series = new Series();

        public frmTestBackPropagation()
        {
            InitializeComponent();
            series = chtErrores.Series.Add("Errores");
            series.ChartType = SeriesChartType.Line;
        }

        private void btnAprender_Click(object sender, EventArgs e)
        {
            double salida = -2;
            double error = 0;
            double entrada1 = 0;
            double entrada2 = 0;
            double salidaPatron = 0;

            for(int iteracion = 0; iteracion < 2000; iteracion++)
            { 
                for(int p = 0; p < 4; p++)
                {
                    entrada1 = p == 0 || p == 2 ? 0 : 1;
                    entrada2 = p == 0 || p == 1 ? 0 : 1;
                    salidaPatron = entrada1 == 0 && entrada2 == 0 ? 0 : entrada1 == 1 && entrada2 == 0 ? 1 : entrada1 == 0 && entrada2 == 1 ? 1 : 0;
                    redBP.ColocarEntrada(0, entrada1);
                    redBP.ColocarEntrada(1, entrada2);
                    redBP.ColocarSalidaDeseada(0, salidaPatron);

                    redBP.FeedForward();
                    salida = redBP.ObtenerSalida(0);

                    lstSalidas.Items.Add(entrada1 + "XOR" + entrada2 + " = " + salida.ToString());

                    redBP.BackPropagate();

                    error = redBP.CalcularErrorRed();
                    lstErroresRed.Items.Add(error);
                    series.Points.Add(error);
                }
            }
        }

        private void btnInicializar_Click(object sender, EventArgs e)
        {
            redBP.Inicializa(int.Parse(txtCantEntrada.Text), int.Parse(txtCantOculta.Text), int.Parse(txtCantSalida.Text), double.Parse(txtFactorEntrenamiento.Text));  
        }

        private void btnEntradas_Click(object sender, EventArgs e)
        {
            redBP.ColocarEntrada(0, double.Parse(txtEntrada1.Text));
            redBP.ColocarEntrada(1, double.Parse(txtEntrada2.Text));
            redBP.ColocarSalidaDeseada(0, double.Parse(this.txtSalida.Text));
        }

        private void btnIdentificar_Click(object sender, EventArgs e)
        {
            redBP.ColocarEntrada(0, double.Parse(txtEntrada1.Text));
            redBP.ColocarEntrada(1, double.Parse(txtEntrada2.Text));
            redBP.FeedForward();
            MessageBox.Show("Patrón identificado: " + redBP.ObtenerSalida(0).ToString());
        }
    }
}
