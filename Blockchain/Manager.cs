using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;

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
        public void CrearBlockchain(string pnom, string pmot, string pfhash, string phash)
        {
            if (geti() == 0)
            {
                Bloque genesis = new Bloque(geti(), pnom, pmot, pfhash, phash, "000000");
                BlockChain.Add(genesis);
                incrementar_i();
            } else
            {
                prehash = BlockChain.ElementAt(geti()-1).Hash;
                Bloque block = new Bloque(geti(), pnom, pmot, pfhash, phash, prehash);
                BlockChain.Add(block);
                incrementar_i();
            }
            
        }
    }
}
