using Microsoft.VisualStudio.TestTools.UnitTesting;
using Blockchain;

namespace TestBlockchain
{
    [TestClass]
    public class TestBloque
    {
        [TestMethod]
        public void TestDeBloque_DebeCrearBloque(string pnom, string pmot, string parch)
        {
            var b = new Bloque(pnom, pmot, parch);
        }
    }
}
