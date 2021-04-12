using Microsoft.VisualStudio.TestTools.UnitTesting;
using Blockchain;

namespace TestBlockchain
{
    [TestClass]
    public class TestBloque
    {
        [TestMethod]
        public void TestDeHash_CompararHashes()
        {
            Bloque a = new Bloque(0, "Adrian", "enfermedad", "123abc", "pre123abc", "0000");
            Bloque b = new Bloque(0, "Adrian", "enfermedad", "123abc", "pre123abc", "0000");
            Manager c = new Manager();
            string ha = c.Hash256(a);
            string hb = c.Hash256(b);
            Assert.AreEqual(ha, hb);
        }
    }
}
