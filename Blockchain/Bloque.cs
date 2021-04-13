using System;
using System.Text;

namespace Blockchain
{
    public class Bloque
    {
        public long Indice;
        public long GetIndice()
        {
            return this.Indice;
        }
        private void SetIndice(long indi)
        {
            this.Indice = indi;
        }

        public string Nombre;
        public string GetNombre()
        {
            return this.Nombre;
        }
        private void SetNombre(string nom)
        {
            this.Nombre = nom;
        }
        public string Motivo;
        public string GetMotivo()
        {
            return this.Motivo;
        }
        private void SetMotivo(string mot)
        {
            this.Motivo = mot;
        }
        public string FileHash;
        public string GetFileHash()
        {
            return this.FileHash;
        }
        private void SetFileHash(string fhash)
        {
            this.FileHash = fhash;
        }
        public string Hash;
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
        public string PrevHash;
        public string GetPrevHash()
        {
            return this.PrevHash;
        }
        private void SetPrevHash(string prehash)
        {
            this.PrevHash = prehash;
        }
        public DateTime Fecha { get; set; }


        public Bloque()
        {
        }
        public Bloque(long pindi, string pnom, string pmot, string pfhash, string pprehash, string phash)
        {
            this.Fecha = DateTime.Now;
            SetIndice(pindi);
            SetNombre(pnom);
            SetMotivo(pmot);
            SetFileHash(pfhash);
            SetPrevHash(pprehash);
            SetHash(phash);
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
