using System;
using System.Collections.Generic;
using System.Text;

namespace RNA.Hopfield
{
    public class RedHopfield
    {
        /* Funcionamiento:
         * Se deben ingresar los valores de entrada para cada patrón a aprender.
         * Por cada t-upla de valores se debe realizar el proceso de aprendizaje. El mismo consta de obtener los pesos del patrón.
         * Para finalizar el proceso de aprendizaje se deben sumar las matrices de pesos de cada patrón. Así se obtiene la matriz de pesos de la red.
         * Para identificar un patrón, primero se deben ingresar los valores de entrada del patrón a identificar.
         * Luego, se multiplica el vector ingresado por la matriz de pesos de la red.
         * Al vector resultante se le aplica la función transferencia, lo que genera un nuevo vector. El mismo se multiplica nuevamente por la matriz
         * de pesos de la red. Al vector resultante se le aplica la función transferencia. El vector resultado es el del patrón identificado.
         * Por último a dicho vector se lo debe comparar con algunos de los patrones aprendidos, si coincide se dice que se identifico el patrón
         * sino, la red ha fallado.
         */

        /* PARA MEJORAR LAS OPERACIONES ENTRE MATRICES, SE PODRÍA UTILIZAR LA CLASE MATRIZ */

        /* HABRÍA QUE CONSIDERAR ALGÚN MARGEN DE ERROR PARA DAR SIEMPRE UNA RESPUESTA DE ENTRE LAS APRENDIDAS, POR MÁS QUE NO COINCIDA TOTALMENTE?
         * POR EL MOMENTO VOY A TOMAR EL PRIMER PATRÓN CON MAYOR NÚMERO DE ACIERTOS.
         */

        private class Aciertos
        {
            public int[] patron;
            public int Efectividad;

            public Aciertos(int[] patron)
            {
                this.patron = patron;
                this.Efectividad = 0;
            }
        }

        CapaH capa = new CapaH();
        Matriz pesosRed;
        List<Aciertos> patronesAprendidos = new List<Aciertos>(); // Estos son los patrones aprendidos (uno por fila).

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

        public void ColocarEntrada(int[] valores)
        {
            this.capa.Valor = valores;                        
        }

        public void Aprender()
        { 
            // Para aprender, primero se deben colocar los valores de entrada correspondientes.            
            this.pesosRed += this.ObtenerPesosPatron(this.capa.Valor);

            this.patronesAprendidos.Add(new Aciertos(this.capa.Valor));
        }

        private Matriz ObtenerPesosPatron(int[] patron)
        {
            Matriz pesosPatron = new Matriz(this.capa.Neuronas);

            for (int c = 0; c < this.capa.Neuronas; c++)
            {
                for (int f = 0; f < this.capa.Neuronas; f++)
                {
                    pesosPatron.valores[f, c] = f == c ? 0 : patron[f] * patron[c];
                }   
            }

            return pesosPatron;
        }

        public int[] IdentificarPatron()
        {
            int[] patronNuevo = this.CalcularPatron(this.capa.Valor);

            // Ahora tengo que comparar el patrón identificado con los aprendidos.
            for (int f = 0; f < this.capa.Neuronas; f++)
            {
                for (int c = 0; c < this.capa.Neuronas; c++)
                {
                    if (this.patronesAprendidos[f].patron[c] == patronNuevo[c])
                        this.patronesAprendidos[f].Efectividad++;
                }
            }

            // Obtengo el patrón con mayor efectividad
            int maxEfec = -1;
            int[] maxPatron = new int[this.patronesAprendidos.Count];
            foreach (Aciertos a in this.patronesAprendidos)
            {
                if (a.Efectividad > maxEfec)
                {
                    maxEfec = a.Efectividad;
                    maxPatron = a.patron;
                }
            }

            return maxPatron;
        }

        private int[] CalcularPatron(int[] patron)
        {
            /* Acá debería multiplicar el vector ingresado por la matriz de pesos, el resultado nuevamente por la matriz de pesos y así
             * sucesivamente hasta que converja o no encuentra patrón asociado.
             * Se debe multiplicar un mínimo de dos veces para garantizar que el resultado está estabilizado. Si el resultado de las dos operaciones
             * es el mismo salgo de la iteración.
             */
            int[] net1 = new int[this.capa.Neuronas];
            int[] net2 = new int[this.capa.Neuronas];
            int valor = 0;

            for (int c = 0; c < this.capa.Neuronas; c++)
            {
                for (int f = 0; f < this.capa.Neuronas; f++)
                {
                    valor += patron[f] * this.pesosRed.valores[f, c];
                }

                net1[c] = valor > 0 ? 0 : -1;
            }

            valor = 0;

            for (int c = 0; c < this.capa.Neuronas; c++)
            {
                for (int f = 0; f < this.capa.Neuronas; f++)
                {
                    valor += net1[f] * this.pesosRed.valores[f, c];
                }

                net2[c] = valor > 0 ? 0 : -1;
            }

            return net2;
        }
    }
}
