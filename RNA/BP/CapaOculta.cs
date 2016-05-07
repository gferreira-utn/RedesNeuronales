using System;
using System.Collections.Generic;
using System.Text;

namespace RNA.RedBackPropagation
{
    public class CapaOculta : Capa
    {
        public override void CalcularError()
        {
            double sumatoria = 0.0;

            for (int n = 0; n < this.hija.Neuronas; n++)
            {
                for (int m = 0; m < this.hija.Neuronas; m++)
                {
                    sumatoria += this.hija.Error[m] * this.hija.Pesos[n, m];
                }
            }

            for (int n = 0; n < this.neuronas; n++)
            {
                this.Error[n] = sumatoria * this.Valor[n] * (1.0 - this.Valor[n]);
            }
        }
    }
}
