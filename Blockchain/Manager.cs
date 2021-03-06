using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Blockchain
{
    public class Manager
    {
        public List<Bloque> BlockChain { get; set; }
        public int i;
        private static Manager instance = null;
        
        protected Manager(DateTime fechatest)
        {
            Inicializar(fechatest);
        }
        public static Manager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Manager(DateTime.Now);
                }

                return instance;
            }
        }        
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
        public void Inicializar(DateTime fechatest)
        {
            BlockChain = new List<Bloque>();
            this.i = 1;
            Bloque gen = new Bloque(0, "00000", "00000", "00000", "00000", fechatest);
            gen.ModHash(Hash256(gen));
            BlockChain.Add(gen);
        }
        public void AgregarBloque(string pnom, string pmot, string pfhash)
        {
            AgregarBloque(pnom, pmot, pfhash, DateTime.Now);

        }
        public void AgregarBloque(string pnom, string pmot, string pfhash, DateTime pfech)
        {
            string prehash = GetBloqueIndice(GetI() - 1).GetHash();
            Bloque block = new Bloque(GetI(), pnom, pmot, pfhash, prehash, pfech);
            string nhash = HashCondicional(block);
            block.ModHash(nhash);
            BlockChain.Add(block);
            Incrementar_i();
        }
        public Bloque GetUltimoBloque()
        {
            return BlockChain.ElementAt(GetI()-1);
        }
        public int GetCantidadDeBloques()
        {
            return GetI();
        }
        public bool VerificarCadena()
        {            
            bool rehash;
            bool check = true;
            bool hashanterior;
            for (int y=0; y<GetI(); y++)
            {                
                if (y == 0)
                {                    
                    hashanterior = (GetBloqueIndice(y).GetPrevHash() == "00000");
                }
                else
                {
                    hashanterior = (GetBloqueIndice(y).GetPrevHash() == GetBloqueIndice(y - 1).GetHash());
                }
                rehash = (GetBloqueIndice(y).GetHash() == Hash256(GetBloqueIndice(y)));

                check = ((rehash & hashanterior) & check);                
            }
            return check;
        }       
        public string HashCondicional(Bloque block)
        {
            string phash = Hash256(block);            
            if (block.GetFecha().Day % 2 == 0)
            {
                while (phash[0] != '0' | phash[1] != '0')
                {
                    block.IncNonce(); 
                    phash = Hash256(block);                    
                }
                return phash;
            } else
            {
                while (phash[0] != '0')
                {
                    block.IncNonce();
                    phash = Hash256(block);
                }
                return phash;
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
        public Bloque GetBloquePorHash(string p_hash)
        {
            for (int i=1; i<GetI(); i++)
            {
                if (GetBloqueIndice(i).GetHash() == p_hash)
                {
                    return GetBloqueIndice(i);
                }
            }
            return null;
        }
    }
}
