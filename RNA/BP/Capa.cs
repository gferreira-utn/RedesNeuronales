using System;
using System.Collections.Generic;
using System.Text;

namespace RNA.RedBackPropagation
{
    public abstract class Capa
    {
        protected int neuronas; // Cantidad de neuronas de la capa.
        public int Neuronas
        {
            get { return this.neuronas; }
            set { this.neuronas = value; }

        }

        protected int neuronasPadre; // Cantidad de neuronas de la capa padre.
        public int NeuronasPadre
        {
            get { return this.neuronasPadre; }
            set { this.neuronasPadre = value; }

        }

        protected int neuronasHija; // Cantidad de neuronas de la capa hija.
        public int NeuronasHija
        {
            get { return this.neuronasHija; }
            set { this.neuronasHija = value; }

        }

        public double[,] Pesos; // Pesos para la neurona.

        protected double[,] cambioPesos; // Valor del cambio de pesos.

        public double[] Valor; // Valor de la neurona;

        public double[] ValorDeseado; // Valores deseados de salida.
        public double[] Error; // Error de la neurona.
        protected double[] bias; // Valor del bias por neurona.
        public double[] Bias
        {
            get { return this.bias; }
            set { this.bias = value; }
        }

        protected double coeficienteEntrenamiento; // Valor del coeficiente de entrenamiento.
        public double CoeficienteEntrenamiento
        {
            get { return this.coeficienteEntrenamiento; }
            set { this.coeficienteEntrenamiento = value; }

        }

        protected Capa padre; // Capa padre de esta capa.
        protected Capa hija; // Capa hija de la capa.

        protected bool tienePadre; // Identificar si la capa tiene padre.
        protected bool tieneHija; // Indicar si la capa tiene hija.

        public Capa()
        {
            // Inicializo las variables.
            this.neuronas = 0;
            this.neuronasHija = 0;
            this.neuronasPadre = 0;
            this.tienePadre = false;
            this.tieneHija = false;
        }

        public void InicializaCapa()
        {
            if (this.tienePadre)
            {
                this.Pesos = new double[this.neuronas, this.neuronasPadre];
                this.cambioPesos = new double[this.neuronas, this.neuronasPadre];
            }
            else
                this.Pesos = new double[this.neuronas, 1];

            this.Valor = new double[this.neuronas];
            this.ValorDeseado = new double[this.neuronas];
            this.Error = new double[this.neuronas];

            // Inicializo todos los valores
            if (this.tienePadre)
            {
                for (int n = 0; n < this.neuronas; n++)
                {
                    for (int m = 0; m < this.neuronasPadre; m++)
                    {
                        Pesos[n, m] = 1.0;
                        cambioPesos[n, m] = 0.0;
                    }
                }
            }

            for (int n = 0; n < this.neuronas; n++)
            {
                this.Valor[0] = 0.0;
                this.ValorDeseado[n] = 0.0;
                this.Error[n] = 0.0;
            }
        }

        public void ColocaPadre(Capa nPadre)
        {
            this.padre = nPadre;
            this.tienePadre = true;
            this.neuronasPadre = this.padre.Neuronas;
            this.Pesos = new double[this.neuronas, this.neuronasPadre];
            this.cambioPesos = new double[this.neuronas, this.neuronasPadre];
        }

        public void ColocaHija(Capa nHija)
        {
            this.hija = nHija;
            this.tieneHija = true;
            this.neuronasHija = this.hija.neuronas;
        }

        /// <summary>
        /// Inicializo los pesos con valores aleatorios.
        /// </summary>
        public void InicializaPesos(double? pesos)
        {
            if(pesos == null)
            { 
                if(this.tienePadre)
                { 
                    // Inicializo los pesos con valores aleatorios
                    Random r = new Random();

                    for (int n = 0; n < this.neuronas; n++)
                    {
                        for (int m = 0; m < this.neuronasPadre; m++)
                        {
                            this.Pesos[n, m] = r.NextDouble() - r.NextDouble();
                        }
                    }
                }
                else
                {
                    // Inicializo los pesos con valores aleatorios
                    Random r = new Random();

                    for (int n = 0; n < this.neuronas; n++)
                    {
                        this.Pesos[n, 0] = r.NextDouble() - r.NextDouble();
                    }
                }
            }
            else
            {
                if(this.tienePadre)
                {
                    for (int n = 0; n < this.neuronas; n++)
                    {
                        for (int m = 0; m < this.neuronasPadre; m++)
                        {
                            this.Pesos[n, m] = (double)pesos;
                        }
                    }
                }
                else
                {
                    for (int n = 0; n < this.neuronas; n++)
                    {
                        this.Pesos[n, 0] = (double)pesos;
                    }
                }
            }
        }

        public void CalcularNeuronas()
        {
            // Calculo el valor de las neuronas
            double valor;

            if (this.tienePadre)
            {
                for (int n = 0; n < this.neuronas; n++)
                {
                    valor = 0.0;

                    for (int m = 0; m < this.neuronasPadre; m++)
                        valor += this.padre.Valor[m] * this.Pesos[n, m];

                    valor = valor - bias[n];

                    // Calculo el valor con la función de activación
                    this.Valor[n] = 1.0 / (1 + Math.Exp(-valor));
                }
            }
            else
            {
                for (int n = 0; n < this.neuronas; n++)
                {
                    valor = 0.0;

                    valor = this.Valor[n] * this.Pesos[n, 0] - this.bias[n];

                    // Calculo el valor con la función de activación
                    this.Valor[n] = 1.0 / (1 + Math.Exp(-valor));
                }
            }
        }

        public abstract void CalcularError();

        public void AjustarPesos()
        {
            double delta = 0.0;

            for (int n = 0; n < this.neuronas; n++)
            {
                for (int m = 0; m < this.NeuronasPadre; m++)
                {
                    delta = this.coeficienteEntrenamiento * this.Error[n] * this.padre.Valor[m];
                    this.cambioPesos[n, m] = delta;
                    this.Pesos[n, m] += delta;
                }
            }
        }
    }
}
