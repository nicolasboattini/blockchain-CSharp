using System;
using System.Text;

namespace Blockchain
{
    public class Bloque
    {
        public long Indice { get; set; }
        public String Nombre { get; set; }
        public String Motivo { get; set; }
        public String FileHash { get; set; }
        public String Hash;
        public String PrevHash { get; set; }
        public DateTime Fecha { get; set; }

        private void SetHash(string phash)
        {
            this.Hash = phash;
        }
        public string GetHash()
        {
            return this.Hash;
        }
        public void ModHash(string phash)
        {
            this.SetHash(phash);
        }
        public Bloque() {
        }
        public Bloque(long pindi, string pnom, string pmot, string pfhash, string phash, string pprehash)
        {
            this.Fecha = DateTime.Now;
            this.Indice = pindi;
            this.Nombre = pnom;
            this.Motivo = pmot;
            this.FileHash = pfhash;
            this.PrevHash = pprehash;
            this.Hash = phash;
        }

        public override string ToString()
        {   
            return string.Concat(Indice, Nombre, Motivo, FileHash, PrevHash, Fecha.ToString());
        }

        public byte[] ToByteArray()
        {
            var cadena = this.ToString();
            byte[] bytes = Encoding.UTF8.GetBytes(cadena);
            return bytes;
        }
    }
}
