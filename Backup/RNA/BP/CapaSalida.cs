using System;
using System.Collections.Generic;
using System.Text;

namespace IdentificadorPatrones.RNA
{
    public class CapaSalida : Capa
    {
        public override void CalcularError()
        {
            for (int n = 0; n < this.neuronas; n++)
            {
                this.Error[n] = (this.ValorDeseado[n] - this.Valor[n]) * this.Valor[n] * (1.0 - this.Valor[n]);
            }
        }
    }
}
