using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using RNA.RedBackPropagation;
using System.Windows.Forms.DataVisualization.Charting;
using System.Threading;

namespace GeometricRecognizer
{
    public partial class frmGeometricRecognizer : Form
    {
        const int neuronas = 42;
        Button[] pixel;
        double[] figura = new double[neuronas];
        RedBackPropagation redBP = new RedBackPropagation();
        Series series = new Series();

        private class PatronAprendido
        {
            public double[] ValorDeseado;
            public string Descripcion;
            public bool Aprendido;
            public int[] Patron = new int[neuronas];

            public PatronAprendido(string desc, double[] valorDeseado, int[] patron)
            {
                this.Descripcion = desc;
                this.ValorDeseado = valorDeseado;
                this.Aprendido = false;
                this.Patron = patron;
            }

            public override string ToString()
            {
                return Descripcion + " - " + ValorDeseado[0].ToString() + ValorDeseado[1].ToString() + ValorDeseado[2].ToString();
            }
        }

        public frmGeometricRecognizer()
        {
            InitializeComponent();

            pixel = new Button[] {  button1, button2, button3, button4, button5, button6, button7, button8, button9, button10
                        , button11, button12, button13, button14, button15, button16, button17, button18, button19, button20
                        , button21, button22, button23, button24, button25, button26, button27, button28, button29, button30
                        , button31, button32, button33, button34, button35, button36, button37, button38, button39, button40
                        , button41, button42};

            for (int n = 0; n < neuronas; n++)
            {
                pixel[n].Click += new EventHandler(pixel_Click);
                pixel[n].BackColor = Color.White;
                pixel[n].FlatStyle = FlatStyle.Flat;
                figura[n] = 0;
            }

            redBP.Inicializa(neuronas, 42, 3, 0.5);

            series = chtErrores.Series.Add("Error total de la red");

            lstEntrenados.Items.Add(new PatronAprendido("Triángulo", new double[] {0, 0, 1}, new int[]{0, 0, 0, 1, 0, 0, 0, 0, 0, 1, 0, 1, 0, 0, 0, 1, 0, 0, 0, 1, 0, 1, 1, 1, 1, 1, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}));
            lstEntrenados.Items.Add(new PatronAprendido("Recta", new double[] { 0, 1, 0 }, new int[] {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}));
            lstEntrenados.Items.Add(new PatronAprendido("Cuadrado", new double[] { 0, 1, 1 }, new int[] {0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 1, 0, 0, 0, 1, 0, 0, 1, 0, 0, 0, 1, 0, 0, 1, 0, 0, 0, 1, 1, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0}));
            lstEntrenados.Items.Add(new PatronAprendido("Rombo", new double[] { 1, 0, 0 }, new int[] {0, 0, 0, 1, 0, 0, 0, 0, 0, 1, 0, 1, 0, 0, 0, 1, 0, 0, 0, 1, 0, 0, 0, 1, 0, 1, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}));
            lstEntrenados.Items.Add(new PatronAprendido("Rectángulo", new double[] { 1, 0, 1 }, new int[] {0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 0, 0, 0, 0, 0, 0, 0}));

            series.ChartType = SeriesChartType.Line;
        }

        private void pixel_Click(Object sender, EventArgs e)
        {
            ((Button)sender).BackColor = ((Button)sender).BackColor == Color.White ? Color.Black : Color.White;

            for (int b = 0; b < neuronas; b++)
                figura[b] = pixel[b].BackColor == Color.White ? 0 : 1;
        }

        private void btnIdentificar_Click(object sender, EventArgs e)
        {
            string salida = string.Empty;

            for (int n = 0; n < neuronas; n++)
                redBP.ColocarEntrada(n, figura[n]);

            redBP.FeedForward();

            for (int i = 0; i < 3; i++)
                salida += redBP.ObtenerSalida(i).ToString();

            PatronAprendido p = lstEntrenados.Items.Cast<PatronAprendido>().First<PatronAprendido>(patron => patron.ValorDeseado[0] != salida[0] || patron.ValorDeseado[1] != salida[1] || patron.ValorDeseado[2] != salida[2]);

            MessageBox.Show("Patrón identificado: " + p.Descripcion);
        }

        private void btnAprender_Click(object sender, EventArgs e)
        {
            // Entrenamiento de la red de los patrones correspondientes a: Cuadrado, Rombo, Rectángulo, Recta y Triángulo.
            double[] salida = new double[] {-1, -1, -1};
            double error = 0;

            for(int iteraciones = 0; iteraciones < 3000; iteraciones++)
            {
                foreach (PatronAprendido p in lstEntrenados.Items)
                {
                    for (int n = 0; n < neuronas; n++)
                        redBP.ColocarEntrada(n, p.Patron[n]);

                    for (int i = 0; i < 3; i++)
                        redBP.ColocarSalidaDeseada(i, p.ValorDeseado[i]);

                    redBP.FeedForward();

                    for (int i = 0; i < 3; i++)
                        salida[i] = redBP.ObtenerSalida(i);

                    lstSalida.Items.Add(p.Descripcion + " - " + salida[0].ToString() + " " + salida[1].ToString() + " " + salida[2].ToString());

                    redBP.BackPropagate();

                    error += redBP.CalcularErrorRed();

                    if(iteraciones % 100 == 0)
                        this.series.Points.Add(error);
                }
            }

            MessageBox.Show("El entrenamiento a finalizado.", "Reconocedor de figura");
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            foreach(Button b in pixel)
                b.BackColor = Color.White;
        }
    }
}
