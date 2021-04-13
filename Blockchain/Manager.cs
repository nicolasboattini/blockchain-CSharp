using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;

namespace Blockchain
{
    
    public class Manager
    {
        private static Manager instance = null;
        public string mensaje = "";
        protected Manager()
        {
            BlockChain = new List<Bloque>();
            this.i = 0;
            AgregarBloque("a", "b", "c");
        }

        public static Manager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Manager();
                }

                return instance;
            }
        }
        public List<Bloque> BlockChain { get; set; }
        public int i = 0;
        public int GetI()
        {
            return this.i;
        }
        private void Seti(int p_i)
        {
            this.i = p_i;
        }
        public void Incrementar_i()
        {
            this.Seti(this.GetI() + 1);
        }
        
        public void AgregarBloque(string pnom, string pmot, string pfhash)
        {
            if (GetI() == 0)
            {
                
                Bloque genesis = new Bloque(GetI(), pnom, pmot, pfhash, "00000", "0");
                genesis.ModHash(Hash256(genesis));
                BlockChain.Add(genesis);
                Incrementar_i();
            } else
            {
                string prehash = GetBloqueIndice(GetI() - 1).GetHash();
                Bloque block = new Bloque(GetI(), pnom, pmot, pfhash, prehash, "0");
                block.ModHash(Hash256(block));
                BlockChain.Add(block);
                Incrementar_i();
            }
            
        }
        public string Hash256(Bloque pblock)
        {
            return HashByteToString(pblock.ToByteArray());
        }
        public string HashByteToString(byte[] cryptobyte)
        {
            var crypt = new SHA256Managed();
            var hash = new StringBuilder();
            byte[] crypto = crypt.ComputeHash(cryptobyte);
            foreach (byte theByte in crypto)
            {
                hash.Append(theByte.ToString("x2"));
            }
            return hash.ToString();
        }
        public Bloque GetBloqueIndice(int p_ind)
        {
            return BlockChain.ElementAt(p_ind);
        }
       
    }
}
