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
            Bloque a = new Bloque(100, "Adrian", "enfermedad", "123abc", "pre123abc", "0000");
            Bloque b = new Bloque(100, "Adrian", "enfermedad", "123abc", "pre123abc", "0000");
            Manager c = Manager.Instance;
            string ha = c.Hash256(a);
            string hb = c.Hash256(b);
            Assert.AreEqual(ha, hb);
        }
        [TestMethod]
        public void TestDeHash_CompararHashes2()
        {
            Bloque a = new Bloque(0, "Adrian", "enfermedad", "123abc", "pre123abc", "0000");
            Bloque b = new Bloque(0, "Adrian", "enfermedad", "123abc", "pre123abc", "0000");
            Manager c = Manager.Instance;
            string ha = c.Hash256(a);
            string hb = c.Hash256(b);
            Assert.AreEqual(ha, hb);
        }
        [TestMethod]
        public void TestDeBlockchain_TraeBloquePorIndice()
        {
            Manager a = Manager.Instance;
            a.AgregarBloque("manuel", "enfermedad", "certmed.pdf");
            a.AgregarBloque("jose", "vacaciones", "solicitud.doc");
            a.AgregarBloque("arturo", "licencia", "licencia.pdf");
            a.AgregarBloque("arturo", "vacaciones", "boletos.pdf");
            Bloque block = a.GetBloqueIndice(3);
            Assert.AreEqual("arturo", block.GetNombre());
            Assert.AreEqual("licencia", block.GetMotivo());
            Assert.AreEqual("licencia.pdf", block.GetFileHash());
            Assert.AreEqual(3, (int) block.GetIndice());
            Assert.AreEqual(a.Hash256(block), block.GetHash());
        }
        [TestMethod]
        public void TestDeHash_VerificarHashAnterior()
        {
            Manager a = Manager.Instance;
            a.AgregarBloque("manuel", "enfermedad", "certmed.pdf");
            a.AgregarBloque("jose", "vacaciones", "solicitud.doc");
            a.AgregarBloque("arturo", "licencia", "licencia.pdf");
            Bloque b1 = a.GetBloqueIndice(1);
            Bloque b2 = a.GetBloqueIndice(2);
            Bloque b3 = a.GetBloqueIndice(3);
            Assert.AreEqual(b1.GetHash(), b2.GetPrevHash());
            Assert.AreEqual(b2.GetHash(), b3.GetPrevHash());
        }
        [TestMethod]
        public void TestBloqueToArray()
        {
            Bloque a = new Bloque(0, "Adrian", "enfermedad", "123abc", "pre123abc", "0000");
            string esp = string.Concat("0Adrianenfermedad123abcpre123abc", a.Fecha.ToString());
            Assert.AreEqual(esp, a.ToString());
        }
        [TestMethod]
        public void TestSingleton()
        {
            Manager m1 = Manager.Instance;
            Manager m2 = Manager.Instance;
            Assert.AreEqual(m1.GetHashCode(), m2.GetHashCode());
        }
        [TestMethod]
        public void TestSingleton2()
        {
            Manager m1 = Manager.Instance;
            Manager m2 = Manager.Instance;
            m1.AgregarBloque("manuel", "enfermedad", "certmed.pdf");
            
            Assert.AreEqual(m1.GetBloqueIndice(1).GetHash(), m2.GetBloqueIndice(1).GetHash());
        }
    }
}
