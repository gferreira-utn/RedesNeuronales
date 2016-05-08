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
        Thread grafico;

        private class PatronAprendido
        {
            public string ValorDeseado;
            public string Descripcion;
            public bool Aprendido;
            public int[] Patron = new int[neuronas];

            public PatronAprendido(string desc, string valorDeseado, int[] patron)
            {
                this.Descripcion = desc;
                this.ValorDeseado = valorDeseado;
                this.Aprendido = false;
                this.Patron = patron;
            }

            public override string ToString()
            {
                return Descripcion;
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

            redBP.Inicializa(neuronas, 100, 3, 0.05);

            series = chtErrores.Series.Add("Error total de la red");

            lstEntrenados.Items.Add(new PatronAprendido("Triángulo", "001", new int[]{0, 0, 0, 1, 0, 0, 0, 0, 0, 1, 0, 1, 0, 0, 0, 1, 0, 0, 0, 1, 0, 1, 1, 1, 1, 1, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}));
            lstEntrenados.Items.Add(new PatronAprendido("Recta", "010", new int[] {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}));
            lstEntrenados.Items.Add(new PatronAprendido("Cuadrado", "011", new int[] {0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 1, 0, 0, 0, 1, 0, 0, 1, 0, 0, 0, 1, 0, 0, 1, 0, 0, 0, 1, 1, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0}));
            lstEntrenados.Items.Add(new PatronAprendido("Rombo", "100", new int[] {0, 0, 0, 1, 0, 0, 0, 0, 0, 1, 0, 1, 0, 0, 0, 1, 0, 0, 0, 1, 0, 0, 0, 1, 0, 1, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}));
            lstEntrenados.Items.Add(new PatronAprendido("Rectángulo", "101", new int[] {0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 0, 0, 0, 0, 0, 0, 0}));

            series.ChartType = SeriesChartType.Line;

            grafico = new Thread(new ParameterizedThreadStart(ActualizarGrafico));
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

            PatronAprendido p = lstEntrenados.Items.Cast<PatronAprendido>().First<PatronAprendido>(patron => patron.ValorDeseado == salida);

            MessageBox.Show("Patrón identificado: " + p.Descripcion);
        }

        private void btnAprender_Click(object sender, EventArgs e)
        {
            // Entrenamiento de la red de los patrones correspondientes a: Cuadrado, Rombo, Rectángulo, Recta y Triángulo.
            string salida = "-1";
            double error = 0;

            while (((PatronAprendido)lstEntrenados.Items[0]).Aprendido == false || ((PatronAprendido)lstEntrenados.Items[1]).Aprendido == false
                || ((PatronAprendido)lstEntrenados.Items[2]).Aprendido == false || ((PatronAprendido)lstEntrenados.Items[3]).Aprendido == false
                || ((PatronAprendido)lstEntrenados.Items[4]).Aprendido == false)
            { 
                foreach (PatronAprendido p in lstEntrenados.Items)
                {
                    for (int i = 0; i < 3; i++)
                        redBP.ColocarSalidaDeseada(i, double.Parse(p.ValorDeseado[i].ToString()));

                    for (int n = 0; n < neuronas; n++)
                        redBP.ColocarEntrada(n, p.Patron[n]);

                    while (p.ValorDeseado != salida)
                    {
                        salida = string.Empty;

                        redBP.FeedForward();

                        for(int i = 0; i < 3; i++)
                            salida += redBP.ObtenerSalida(i).ToString();

                        if (p.ValorDeseado != salida)
                            redBP.BackPropagate();

                        error = redBP.CalcularErrorRed();
                        /*grafico.Start(error);
                        while (!grafico.IsAlive);
                        grafico.Join();*/
                    }
                }

                // Cuando termina de entrenar a todas tengo que verificar que puede seguir identificando a todas las figuras
                foreach (PatronAprendido p in lstEntrenados.Items)
                {
                    salida = string.Empty;
                    redBP.FeedForward();

                    for (int i = 0; i < 3; i++)
                        salida += redBP.ObtenerSalida(i).ToString();

                    if (p.ValorDeseado == salida)
                        p.Aprendido = true;
                }
            }

            MessageBox.Show("El entrenamiento a finalizado.", "Reconocedor de figura");
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            foreach(Button b in pixel)
                b.BackColor = Color.White;
        }

        public void ActualizarGrafico(object error)
        {           
            if(this.chtErrores.InvokeRequired)
            { 
                this.Invoke(new Action<object>(ActualizarGrafico), new object[] { error });
                return;
            }
            else
                this.series.Points.Add(Convert.ToDouble(error));
        }
    }
}
