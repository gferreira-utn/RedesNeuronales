using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using RNA.Hopfield;

namespace GeometricRecognizer
{
    public partial class frmGeometricRecognizer : Form
    {
        Button[] pixel;
        int[] figura = new int[110];
        RedHopfield red = new RedHopfield();

        public frmGeometricRecognizer()
        {
            InitializeComponent();
                        
            pixel = new Button[] {  button1, button2, button3, button4, button5, button6, button7, button8, button9, button10
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

            for (int n = 0; n < 110; n++)
            {
                pixel[n].Click += new EventHandler(pixel_Click);
                pixel[n].BackColor = Color.White;
                pixel[n].FlatStyle = FlatStyle.Flat;
            }

            red.Inicializar(110);
        }

        private void pixel_Click(Object sender, EventArgs e)
        {
            ((Button)sender).BackColor = ((Button)sender).BackColor == Color.White ? Color.Black : Color.White;
        }

        private void btnIdentificar_Click(object sender, EventArgs e)
        {
            this.cargarEntrada();
            int[] patronReconocido = new int[110];
            string patronRec = string.Empty;

            int resultado = red.IdentificarPatron(out patronReconocido);

            foreach (Object p in lstAprendidos.Items)
            {
                if (((RedHopfield.PatronAprendido)p).patron == patronReconocido)
                    patronRec = ((RedHopfield.PatronAprendido)p).descripcion;
            }            

            if (resultado == 0)
                MessageBox.Show("Patrón reconocido: " + patronRec);
            else
                MessageBox.Show("Patrón no estabilizado. Se reconoce como: " + patronRec);
        }

        private void btnAprender_Click(object sender, EventArgs e)
        {
            this.cargarEntrada();

            red.Aprender(txtDescripción.Text);

            lstAprendidos.Items.Add(red.PatronesAprendidos.Find(p => p.descripcion.Equals(txtDescripción.Text)));
        }

        private void cargarEntrada()
        {
            for (int c = 0; c < 110; c++)
            {
                figura[c] = pixel[c].BackColor == Color.White ? -1 : 1;
            }

            red.ColocarEntrada(figura);
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            foreach(Button b in pixel)
            {
                b.BackColor = Color.White;
            }

            this.txtDescripción.Text = string.Empty;
        }
    }
}
