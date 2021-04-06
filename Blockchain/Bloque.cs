using System;

namespace Blockchain
{
    public class Bloque
    {
        public long Indice { get; set; }
        public String Nombre { get; set; }
        public String Motivo { get; set; }
        public String FileHash { get; set; }
        public String Hash { get; set; }
        public String PrevHash { get; set; }
        public DateTime Fecha { get; set; }
        public Bloque() { 
        }
        public Bloque(long pindi, string pnom, string pmot, string pfhash, string phash, string pprehash)
        {
            this.Fecha = DateTime.Now;
            this.Indice = pindi;
            this.Nombre = pnom;
            this.Motivo = pmot;
            this.FileHash = pfhash;|
            this.Hash = phash;
            this.PrevHash = pprehash;
        }
    }
}
