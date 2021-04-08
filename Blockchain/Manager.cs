using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Security.Cryptography;

namespace Blockchain
{
    
    class Manager
    {
        
        public List<Bloque> BlockChain { get; set; }
        public int i = 0;
        public string prehash;
        public int geti()
        {
            return this.i;
        }
        private void seti(int p_i)
        {
            this.i = p_i;
        }
        private void incrementar_i()
        {
            this.seti(this.geti() + 1);
        }
        public Manager()
        {
            BlockChain = new List<Bloque>();
        }
        public void CrearBlockchain(string pnom, string pmot, string pfhash)
        {
            if (geti() == 0)
            {
                
                Bloque genesis = new Bloque(geti(), pnom, pmot, pfhash, "0", "000000");
                byte[] genbyte = genesis.ToByteArray();
                string genhash = SHA256maker(genbyte);
                genesis.ModHash(genhash);
                BlockChain.Add(genesis);
                incrementar_i();
            } else
            {
                string prehash = BlockChain.ElementAt(geti()-1).GetHash();
                Bloque block = new Bloque(geti(), pnom, pmot, pfhash, "0", prehash);
                byte[] blockbyte = block.ToByteArray();
                string blockhash = SHA256maker(blockbyte);
                block.ModHash(blockhash);
                BlockChain.Add(block);
                incrementar_i();
            }
            
        }

        static string SHA256maker(byte[] cryptobyte)
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
    }
}
