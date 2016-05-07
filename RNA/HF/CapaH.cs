using System;
using System.Collections.Generic;
using System.Text;

namespace RNA.Hopfield
{
    public class CapaH
    {
        // Cantidad de neuronas de la red.
        private int neuronas;
        public int Neuronas
        {
            get { return this.neuronas; }
            set { 
                    this.neuronas = value;
                    // Lo inicializo acá para que cuando cambia la cantidad de neuronas cambie la matriz.
                    this.Pesos = new int[this.neuronas, this.neuronas]; 
                    this.Valor = new int[this.neuronas];
                }
        }

        // Valores de entrada para las neuronas;
        public int[] Valor;

        // Matriz de pesos (debe ser cuadrada).
        public int[,] Pesos;
    }
}
