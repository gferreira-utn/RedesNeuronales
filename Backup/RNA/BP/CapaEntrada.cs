using System;
using System.Collections.Generic;
using System.Text;

namespace IdentificadorPatrones
{
    public class CapaEntrada : Capa
    {
        public override void CalcularError()
        {
            for (int n = 0; n < this.neuronas; n++)
            {
                this.Error[n] = 0.0;
            }
        }
    }
}
