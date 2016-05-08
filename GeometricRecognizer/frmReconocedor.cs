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

namespace GeometricRecognizer
{
    public partial class frmGeometricRecognizer : Form
    {
        Button[] pixel;
        double[] figura = new double[30];
        RedBackPropagation redBP = new RedBackPropagation();

        public frmGeometricRecognizer()
        {
            InitializeComponent();

            /*pixel = new Button[] {  button1, button2, button3, button4, button5, button6, button7, button8, button9, button10
                                    , button11, button12, button13, button14, button15, button16, button17, button18, button19, button20
                                    , button21, button22, button23, button24, button25, button26, button27, button28, button29, button30
                                    , button31, button32, button33, button34, button35, button36, button37, button38, button39, button40
                                    , button41, button42, button43, button44, button45, button46, button47, button48, button49, button50
                                    , button51, button52, button53, button54, button55, button56, button57, button58, button59, button60
                                    , button61, button62, button63, button64, button65, button66, button67, button68, button69, button70
                                    , button71, button72, button73, button74, button75, button76, button77, button78, button79, button80
                                    , button81, button82, button83, button84, button85, button86, button87, button88, button89, button90
                                    , button91, button92, button93, button94, button95, button96, button97, button98, button99, button100
                                    , button101, button102, button103, button104, button105, button106, button107, button108, button109, button110};
            */

            pixel = new Button[] {  button1, button2, button3, button4, button5, button6, button7, button8, button9, button10
                        , button11, button12, button13, button14, button15, button16, button17, button18, button19, button20
                        , button21, button22, button23, button24, button25, button26, button27, button28, button29, button30};

            for (int n = 0; n < 30; n++)
            {
                pixel[n].Click += new EventHandler(pixel_Click);
                pixel[n].BackColor = Color.White;
                pixel[n].FlatStyle = FlatStyle.Flat;
            }

            redBP.Inicializa(30, 90, 4, 0.1);
        }

        private void pixel_Click(Object sender, EventArgs e)
        {
            ((Button)sender).BackColor = ((Button)sender).BackColor == Color.White ? Color.Black : Color.White;
        }

        private void btnIdentificar_Click(object sender, EventArgs e)
        {
            string salida = string.Empty;

            this.cargarEntrada();

            redBP.FeedForward();

            for (int i = 0; i < 4; i++)
                salida += redBP.ObtenerSalida(i).ToString();

            MessageBox.Show("Patrón identificado: " + salida);
        }

        private void btnAprender_Click(object sender, EventArgs e)
        {
            Series series = new Series();
            series = chtErrores.Series.Add("Errores " + txtDescripcion.Text);
            string salida = "-1";
            double error = 0;
            int iteraciones = 0;

            for (int i = 0; i < 4; i++)
                redBP.ColocarSalidaDeseada(i, double.Parse(this.txtValorDeseado.Text[i].ToString()));

            this.cargarEntrada();

            while (double.Parse(txtValorDeseado.Text) != double.Parse(salida))
            {
                salida = string.Empty;

                redBP.FeedForward();

                for(int i = 0; i < 4; i++)
                    salida += redBP.ObtenerSalida(i).ToString();

                redBP.BackPropagate();

                error = redBP.CalcularErrorRed();
                series.ChartType = SeriesChartType.Line;
                series.Points.Add(error);

                iteraciones++;
                
                if (iteraciones == 1000000)
                {
                    iteraciones = -1;
                    break;
                }
                
            }

            if (iteraciones != -1)
            {
                lstEntrenados.Items.Add(txtDescripcion.Text + " " + txtValorDeseado.Text);
                MessageBox.Show("Patrón entrenado");
            }
            else
                MessageBox.Show("No se consiguió entrenar el patrón deseado en 1000000 de iteraciones.");
        }

        private void cargarEntrada()
        {
            for (int c = 0; c < 30; c++)
            {
                figura[c] = pixel[c].BackColor == Color.White ? 0 : 1;
                redBP.ColocarEntrada(c, figura[c]);
            }
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            foreach(Button b in pixel)
                b.BackColor = Color.White;

            txtDescripcion.Text = string.Empty;
            txtValorDeseado.Text = string.Empty;
        }
    }
}
