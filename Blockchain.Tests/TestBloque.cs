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
            Assert.AreEqual("005c897d17fb2fd2feaed4db448862ee63fc2ce2d896fb72f13c3ec095aac5b3", hb);
            Assert.AreEqual("005c897d17fb2fd2feaed4db448862ee63fc2ce2d896fb72f13c3ec095aac5b3", ha);
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
            Manager a = Manager.InstanceTest;
            DateTime pfech = new DateTime(2001, 6, 7, 12, 30, 00, 00, System.DateTimeKind.Utc);
            a.AgregarBloque("manuel", "enfermedad", "certmed.pdf", pfech);
            a.AgregarBloque("jose", "vacaciones", "solicitud.doc", pfech);
            a.AgregarBloque("arturo", "licencia", "licencia.pdf", pfech);
            Bloque b1 = a.GetBloqueIndice(1);
            Bloque b2 = a.GetBloqueIndice(2);
            Bloque b3 = a.GetBloqueIndice(3);
            Assert.AreEqual("fc5ab006e039fa9c10fae52c08d2eba8b52a04b27548b36612586c9c442d4b89", a.GetBloqueIndice(0).GetHash());
            Assert.AreEqual("fc5ab006e039fa9c10fae52c08d2eba8b52a04b27548b36612586c9c442d4b89", b1.GetPrevHash());
            Assert.AreEqual("031df73849ef340b02e8442b9cc9b1afdf78efae02041f2cc4962af80e8ad357", b1.GetHash());
            Assert.AreEqual("031df73849ef340b02e8442b9cc9b1afdf78efae02041f2cc4962af80e8ad357", b2.GetPrevHash());
            Assert.AreEqual("0e20c629b95581c88c80c9735ce68e88789449646c28238dacdba37bed14cfeb", b2.GetHash());
            Assert.AreEqual("0e20c629b95581c88c80c9735ce68e88789449646c28238dacdba37bed14cfeb", b3.GetPrevHash());
        }
        [TestMethod]
        public void TestBloqueToArray()
        {
            DateTime pfech = new DateTime(2001, 6, 7, 12, 30, 00, 00, System.DateTimeKind.Utc);
            Bloque a = new Bloque(0, "Adrian", "enfermedad", "123abc", "pre123abc", pfech);
            string esp = string.Concat("10Adrianenfermedad123abcpre123abc6/7/2001 12:30:00 PM");
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
            Manager m = Manager.InstanceTest;
            Bloque gen = m.GetBloqueIndice(0);
            Assert.AreEqual(0, gen.GetIndice());
            Assert.AreEqual("00000", gen.GetNombre());
            Assert.AreEqual("00000", gen.GetMotivo());
            Assert.AreEqual("00000", gen.GetFileHash());
            Assert.AreEqual("00000", gen.GetPrevHash());
            Assert.AreEqual("fc5ab006e039fa9c10fae52c08d2eba8b52a04b27548b36612586c9c442d4b89", gen.GetHash());
        }
        [TestMethod]
        public void TestDeBusquedaPorHash()
        {
            Manager a = Manager.InstanceTest;
            DateTime pfech = new DateTime(2001, 6, 6, 12, 30, 00, 00, System.DateTimeKind.Utc);
            a.AgregarBloque("manuel", "enfermedad", "certmed.pdf", pfech);
            a.AgregarBloque("jose", "vacaciones", "solicitud.doc", pfech);
            Bloque b1 = a.GetBloqueIndice(1);
            Bloque b2 = a.GetBloqueIndice(2);
            Assert.AreEqual(b1, a.GetBloquePorHash("031df73849ef340b02e8442b9cc9b1afdf78efae02041f2cc4962af80e8ad357"));
            Assert.AreEqual(b2, a.GetBloquePorHash("0e20c629b95581c88c80c9735ce68e88789449646c28238dacdba37bed14cfeb"));
        }
        [TestMethod]
        public void TestDeBloque()
        {
            Manager a = Manager.InstanceTest;
            ulong p = 42;
            DateTime pfech = new DateTime(2001, 6, 7, 12, 30, 00, 00, System.DateTimeKind.Utc);
            a.AgregarBloque("manuel", "enfermedad", "certmed.pdf", pfech);
            Bloque b1 = a.GetBloqueIndice(1);
            Assert.AreEqual("6/7/2001 12:30:00 PM", b1.GetFecha().ToString());
            Assert.AreEqual("certmed.pdf", b1.GetFileHash());
            Assert.AreEqual("031df73849ef340b02e8442b9cc9b1afdf78efae02041f2cc4962af80e8ad357", b1.GetHash());
            Assert.AreEqual(1, b1.GetIndice());
            Assert.AreEqual("enfermedad", b1.GetMotivo());
            Assert.AreEqual("manuel", b1.GetNombre());
            Assert.AreEqual(p, b1.GetNonce());
            Assert.AreEqual("fc5ab006e039fa9c10fae52c08d2eba8b52a04b27548b36612586c9c442d4b89", b1.GetPrevHash());
        }
        [TestMethod]
        public void TestDiaParImpar()
        {
            Manager a = Manager.InstanceTest;
            DateTime fechapar = new DateTime(2001, 6, 12, 12, 30, 00, 00, System.DateTimeKind.Utc);
            DateTime fechaimpar = new DateTime(2001, 6, 7, 12, 30, 00, 00, System.DateTimeKind.Utc);
            a.AgregarBloque("manuel", "enfermedad", "certmed.pdf", fechapar);
            a.AgregarBloque("jose", "vacaciones", "solicitud.doc", fechaimpar);
            Bloque bloquepar = a.GetBloqueIndice(1);
            Bloque bloqueimpar = a.GetBloqueIndice(2);
            Assert.AreEqual("manuel", bloquepar.GetNombre());
            Assert.AreEqual("0058afa3c18681cbe59f401f3f54f15fa69a3af2ee90870b0341cd905698f1e6", bloquepar.GetHash());
            Assert.AreEqual(bloquepar.GetHash()[0], '0');
            Assert.AreEqual(bloquepar.GetHash()[1], '0');
            Assert.AreEqual("jose", bloqueimpar.GetNombre());
            Assert.AreEqual("0ec995a4321b71254e92651ebeeaa1b2020df695b058480d8dfe1a2da0d444e3", bloqueimpar.GetHash());
            Assert.AreEqual(bloqueimpar.GetHash()[0], '0');
                
            
        }
    }
}
