using System;
using System.Collections.Generic;
using System.Text;

namespace RNA.RedBackPropagation
{
    public class RedBackPropagation
    {
        /* FALTA AGREGAR LA FUNCIÓN DE SALIDA PARA NORMALIZAR LOS VALORES RESPECTO DE LA ENTRADA */
        Capa entrada, oculta, salida;

        public void Inicializa(int entradas, int ocultas, int salidas, double coeficiente)
        {
            // Por ahora solo puedo crear una capa de entrada, una oculta y una de salida.
            entrada = new CapaEntrada();
            entrada.Neuronas = entradas;

            oculta = new CapaOculta();
            oculta.Neuronas = ocultas;
            oculta.CoeficienteEntrenamiento = coeficiente;
            oculta.Bias = new double[ocultas];

            salida = new CapaSalida();
            salida.Neuronas = salidas;
            salida.CoeficienteEntrenamiento = coeficiente;
            salida.Bias = new double[salidas];

            // Configuro las relaciones.
            entrada.ColocaHija(oculta);
            oculta.ColocaPadre(entrada);
            oculta.ColocaHija(salida);
            salida.ColocaPadre(oculta);

            // Inicializo las estructuras.
            entrada.InicializaCapa();
            oculta.InicializaCapa();
            salida.InicializaCapa();

            // Inicializo los pesos.
            oculta.InicializaPesos();
            salida.InicializaPesos();
        }

        /// <summary>
        /// Con este método puedo pasarle la información a la capa de entrada.
        /// </summary>
        /// <param name="neurona">Indica a qué neurona de la capa de entrada le pasaré el valor.</param>
        /// <param name="valor">Valor a colocar en la neurona de entrada indicada.</param>
        public void ColocarEntrada(int neurona, double valor)
        {
            if (neurona >= 0 && neurona < this.entrada.Neuronas)
                this.entrada.Valor[neurona] = valor;
        }

        /// <summary>
        /// Permite obtener el valor de salida de una neurona específica.
        /// </summary>
        /// <param name="neurona">Indice de la neurona de la que se quiere obteenr su valor.</param>
        /// <returns></returns>
        public double ObtenerSalida(int neurona)
        {
            if (neurona >= 0 && neurona < this.salida.Neuronas)
                return this.salida.Valor[neurona] > 0.5 ? 1 : 0;
            else
                return 0.0;
        }

        /// <summary>
        /// Permite indicar el valor de salida deseado para una neurona determinada.
        /// </summary>
        /// <param name="neurona"> Indice de la neurona.</param>
        /// <param name="valor">Valor de salida deseado</param>
        public void ColocarSalidaDeseada(int neurona, double valor)
        {
            if (neurona >= 0 && neurona < this.salida.Neuronas)
                this.salida.ValorDeseado[neurona] = valor;
        }

        /// <summary>
        /// Realiza el cálculo de todos los valores de la red neuronal.
        /// </summary>
        public void FeedForward()
        {
            this.entrada.CalcularNeuronas();
            this.oculta.CalcularNeuronas();
            this.salida.CalcularNeuronas();
        }

        /// <summary>
        /// Modifica y actualiza los valores de los pesos. Calcula el error de la capa de salida y ajusta los pesos de esa capa.
        /// </summary>
        public void BackPropagate()
        {
            this.salida.CalcularError();
            this.salida.AjustarPesos();

            this.oculta.CalcularError();
            this.oculta.AjustarPesos();
        }

        /// <summary>
        /// Permite calcular el error total de la red.
        /// </summary>
        /// <returns></returns>
        public double CalcularErrorRed()
        {
            double error = 0.0;

            for(int n = 0; n < this.salida.Neuronas; n++)
            {
                error += Math.Pow(this.salida.Valor[n] - this.salida.ValorDeseado[n], 2);
            }

            error /= this.salida.Neuronas;

            return error;
        }
    }
}
