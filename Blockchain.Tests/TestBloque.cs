using Microsoft.VisualStudio.TestTools.UnitTesting;
using Blockchain;
using System;

namespace Blockchain.Tests
{
    [TestClass]
    public class TestBloque
    {
        [TestMethod]
        public void TestDeHash_CompararHashes()
        {
            DateTime pfech = new DateTime(2001, 06, 06, 12, 30, 00, 00, System.DateTimeKind.Utc);
            Bloque a = new Bloque(10, "Adrian", "enfermedad", "123abc", "pre123abc", pfech);
            Bloque b = new Bloque(10, "Adrian", "enfermedad", "123abc", "pre123abc", pfech);
            Manager c = Manager.Instance;
            string ha = c.HashCondicional(a);
            string hb = c.HashCondicional(b);
            Assert.AreEqual("0806ab65dd0f8b109087464965b7c929bb954bb3450987005fdca46077a3cbd4", hb);
            Assert.AreEqual("0806ab65dd0f8b109087464965b7c929bb954bb3450987005fdca46077a3cbd4", ha);
        }
        [TestMethod]
        public void TestDeHash_CompararHashes2()
        {
            Bloque a = new Bloque(1, "Adrian", "enfermedad", "123abc", "pre123abc");
            Bloque b = new Bloque(1, "Adrian", "enfermedad", "123abc", "pre123abc");
            Manager c = Manager.Instance;
            string ha = c.Hash256(a);
            string hb = c.Hash256(b);
            Assert.AreEqual(ha, hb);
        }
        [TestMethod]
        public void TestDeBlockchain_TraeBloquePorIndice()
        {
            DateTime pfech = new DateTime(2001, 06, 07, 12, 30, 00, 00, System.DateTimeKind.Utc);
            Manager a = Manager.Instance;
            a.AgregarBloque("manuel", "enfermedad", "certmed.pdf", pfech);
            a.AgregarBloque("jose", "vacaciones", "solicitud.doc", pfech);
            a.AgregarBloque("arturo", "licencia", "licencia.pdf", pfech);
            a.AgregarBloque("arturo", "vacaciones", "boletos.pdf", pfech);
            Bloque block = a.GetBloqueIndice(3);
            Bloque pre = a.GetBloqueIndice(2);
            Assert.AreEqual("arturo", block.GetNombre());
            Assert.AreEqual("licencia", block.GetMotivo());
            Assert.AreEqual("licencia.pdf", block.GetFileHash());
            Assert.AreEqual(3, (int) block.GetIndice());
            Assert.AreEqual(pre.GetHash(), block.GetPrevHash());
            

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
            Bloque a = new Bloque(0, "Adrian", "enfermedad", "123abc", "pre123abc");
            string esp = string.Concat(a.GetNonce(), "0Adrianenfermedad123abcpre123abc", a.GetFecha().ToString());
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
        [TestMethod]
        public void TestBloqueGenesis()
        {
            Manager m = Manager.Instance;
            Bloque gen = m.GetBloqueIndice(0);
            Assert.AreEqual(0, gen.GetIndice());
            Assert.AreEqual("00000", gen.GetNombre());
            Assert.AreEqual("00000", gen.GetMotivo());
            Assert.AreEqual("00000", gen.GetFileHash());
            Assert.AreEqual("00000", gen.GetPrevHash());
            string hash = m.Hash256(gen);
            Assert.AreEqual(hash, gen.GetHash());
        }
        [TestMethod]
        public void TestDeBusquedaPorHash()
        {
            Manager a = Manager.Instance;
            a.AgregarBloque("manuel", "enfermedad", "certmed.pdf");
            a.AgregarBloque("jose", "vacaciones", "solicitud.doc");
            Bloque b1 = a.GetBloqueIndice(1);
            Bloque b2 = a.GetBloqueIndice(2);
            Assert.AreEqual(b1, a.GetBloquePorHash(b1.GetHash()));
            Assert.AreEqual(b2, a.GetBloquePorHash(a.Hash256(b2)));
        }
    }
}
