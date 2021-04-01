using System;

namespace Blockchain
{
    public class Bloque
    {
        String Nombre;
        String Motivo;
        String HArchivo;
        DateTime Fecha;
        public Bloque() { 
        }
        public Bloque(string pnom, string pmot, string parch)
        {
            this.Motivo = pmot;
            this.Nombre = pnom;
            this.HArchivo = parch;
            this.Fecha = DateTime.Now;
        }
    }
}
