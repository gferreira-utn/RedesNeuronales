using System;
using System.Collections.Generic;
using System.Text;

namespace RNA.Hopfield
{
    public class RedHopfield
    {
        /* Funcionamiento:
         * Se deben ingresar los valores de entrada para cada patr�n a aprender.
         * Por cada t-upla de valores se debe realizar el proceso de aprendizaje. El mismo consta de obtener los pesos del patr�n.
         * Para finalizar el proceso de aprendizaje se deben sumar las matrices de pesos de cada patr�n. As� se obtiene la matriz de pesos de la red.
         * Para identificar un patr�n, primero se deben ingresar los valores de entrada del patr�n a identificar.
         * Luego, se multiplica el vector ingresado por la matriz de pesos de la red.
         * Al vector resultante se le aplica la funci�n transferencia, lo que genera un nuevo vector. El mismo se multiplica nuevamente por la matriz
         * de pesos de la red. Al vector resultante se le aplica la funci�n transferencia. El vector resultado es el del patr�n identificado.
         * Por �ltimo a dicho vector se lo debe comparar con algunos de los patrones aprendidos, si coincide se dice que se identifico el patr�n
         * sino, la red ha fallado.
         */

        /* HABR�A QUE CONSIDERAR ALG�N MARGEN DE ERROR PARA DAR SIEMPRE UNA RESPUESTA DE ENTRE LAS APRENDIDAS, POR M�S QUE NO COINCIDA TOTALMENTE?
         * POR EL MOMENTO VOY A TOMAR EL PRIMER PATR�N CON MAYOR N�MERO DE ACIERTOS.
         */

        public class PatronAprendido
        {
            public int[] patron;
            public int Efectividad;
            public string descripcion;
            public PatronAprendido(int[] pat, string descripcion)
            {
                this.patron = new int[pat.Length];
                for(int i = 0; i < pat.Length; i++)
                    this.patron[i] = pat[i];

                this.Efectividad = 0;
                this.descripcion = descripcion;
            }

            public override string ToString()
            {
                return this.descripcion;
            }
        }

        CapaH capa = new CapaH();
        Matriz pesosRed;
        List<PatronAprendido> patronesAprendidos = new List<PatronAprendido>(); // Estos son los patrones aprendidos (uno por fila).
        public List<PatronAprendido> PatronesAprendidos
        {
            get { return this.patronesAprendidos; }
        }

        private class Matriz
        {
            public int[,] valores;
            static int dimension;
            public int Dimension
            {
                get { return dimension; }
            }

            public Matriz(int dim)
            {
                dimension = dim;
                this.valores = new int[dim, dim];
            }

            public static Matriz operator +(Matriz matrizA, Matriz matrizB)
            {
                Matriz resultado = new Matriz(matrizA.Dimension);

                for (int f = 0; f < dimension; f++)
                {
                    for (int c = 0; c < dimension; c++)
                    {
                        resultado.valores[f, c] = matrizA.valores[f, c] + matrizB.valores[f, c];
                    }
                }

                return resultado;
            }
        }

        public void Inicializar(int neuronas)
        {
            this.capa.Neuronas = neuronas;
            this.pesosRed = new Matriz(this.capa.Neuronas);

            for (int f = 0; f < this.capa.Neuronas; f++)
                for (int c = 0; c < this.capa.Neuronas; c++)
                    this.pesosRed.valores[f, c] = 0;
        }

        /// <summary>
        /// Ingresa los par�metros a las neuronas correspondientes.
        /// </summary>
        /// <param name="valores"></param>
        public void ColocarEntrada(int[] valores)
        {
            this.capa.Valor = valores;                        
        }

        public void Aprender(string descripcion)
        { 
            // Para aprender, primero se deben colocar los valores de entrada correspondientes.            
            this.pesosRed += this.ObtenerPesosPatron(this.capa.Valor);

            this.patronesAprendidos.Add(new PatronAprendido(this.capa.Valor, descripcion));
        }

        /// <summary>
        /// Obtener los pesos del patr�n que, sumado a los pesos de los otros patrones aprendidos, forma los pesos de la red.
        /// </summary>
        /// <param name="patron"></param>
        /// <returns></returns>
        private Matriz ObtenerPesosPatron(int[] patron)
        {
            Matriz pesosPatron = new Matriz(this.capa.Neuronas);

            for (int c = 0; c < this.capa.Neuronas; c++)
            {
                for (int f = 0; f < this.capa.Neuronas; f++)
                {
                    pesosPatron.valores[c, f] = f == c ? 0 : patron[c] * patron[f];
                }   
            }

            return pesosPatron;
        }

        public int IdentificarPatron(out int[] patronIdentificado)
        {
            int[] patronNuevo = new int[this.capa.Neuronas];
            int resultado = this.CalcularPatron(this.capa.Valor, out patronNuevo);

            // Ahora tengo que comparar el patr�n identificado con los aprendidos.
            for (int f = 0; f < this.patronesAprendidos.Count; f++)
            {
                for (int c = 0; c < this.capa.Neuronas; c++)
                {
                    if (this.patronesAprendidos[f].patron[c] == patronNuevo[c])
                        this.patronesAprendidos[f].Efectividad++;
                }
            }

            // Obtengo el patr�n con mayor efectividad
            int maxEfec = -1;
            int[] maxPatron = new int[this.patronesAprendidos.Count];
            foreach (PatronAprendido a in this.patronesAprendidos)
            {
                if (a.Efectividad > maxEfec)
                {
                    maxEfec = a.Efectividad;
                    maxPatron = a.patron;
                }

                a.Efectividad = 0;
            }

            patronIdentificado = maxPatron;

            return resultado;
        }

        /// <summary>
        /// Realiza el c�lculo del patr�n mediante la matriz de pesos de la red.
        /// </summary>
        /// <param name="patron"></param>
        /// <returns></returns>
        private int CalcularPatron(int[] patronEntrada, out int[] patronSalida)
        {
            /* Ac� deber�a multiplicar el vector ingresado por la matriz de pesos, el resultado nuevamente por la matriz de pesos y as�
             * sucesivamente hasta que converja o no encuentra patr�n asociado.
             * Se debe multiplicar un m�nimo de dos veces para garantizar que el resultado est� estabilizado. Si el resultado de las dos operaciones
             * es el mismo es porque se reconoce el patr�n.
             */

            int[] net1 = new int[this.capa.Neuronas];
            int[] net2 = new int[this.capa.Neuronas];
            int iguales = 0;
            int resultado = 0;

            for(int iteracion = 0; iteracion < 5; iteracion++)
            { 
                int valor = 0;

                for (int c = 0; c < this.capa.Neuronas; c++)
                {
                    for (int f = 0; f < this.capa.Neuronas; f++)
                    {
                        valor += patronEntrada[f] * this.pesosRed.valores[f, c];
                    }

                    net1[c] = valor > 0 ? 1 : -1;
                    valor = 0;
                }

                valor = 0;

                for (int c = 0; c < this.capa.Neuronas; c++)
                {
                    for (int f = 0; f < this.capa.Neuronas; f++)
                    {
                        valor += net1[f] * this.pesosRed.valores[f, c];
                    }

                    net2[c] = valor > 0 ? 1 : -1;
                    valor = 0;
                }

                for (int i = 0; i < this.capa.Neuronas; i++)
                {
                    if (net1[i] == net2[i])
                        iguales++;
                    else
                        break;
                }

                if (iguales == this.capa.Neuronas)
                {
                    resultado = 0;
                    break;
                }
                else
                    resultado = -1;
            }

            patronSalida = net1;

            return resultado;
        }
    }
}
