using Microsoft.VisualStudio.TestTools.UnitTesting;
using Blockchain;
using System;

namespace Blockchain.Tests
{
    [TestClass]
    public class BlockchainTestForVS
    {
        
        [TestMethod]
        public void TestDeHash_CompararHashes()
        {
            Manager man = Manager.Instance;
            DateTime now = new DateTime(2021, 4, 20, 12, 30, 00, 00, System.DateTimeKind.Utc);
            man.Inicializar(now);
            DateTime pfech = new DateTime(2001, 6, 6, 12, 30, 00, 00, System.DateTimeKind.Utc);
            Bloque a = new Bloque(10, "Adrian", "enfermedad", "123abc", "pre123abc", pfech);
            Bloque b = new Bloque(10, "Adrian", "enfermedad", "123abc", "pre123abc", pfech);
            string ha = man.HashCondicional(a);
            string hb = man.HashCondicional(b);
            Assert.AreEqual("005c897d17fb2fd2feaed4db448862ee63fc2ce2d896fb72f13c3ec095aac5b3", hb);
            Assert.AreEqual("005c897d17fb2fd2feaed4db448862ee63fc2ce2d896fb72f13c3ec095aac5b3", ha);
        }
        [TestMethod]
        public void TestDeHash_CompararHashes2()
        {
            Manager man = Manager.Instance;
            DateTime now = new DateTime(2021, 4, 20, 12, 30, 00, 00, System.DateTimeKind.Utc);
            man.Inicializar(now);
            DateTime pfech = new DateTime(2001, 6, 6, 12, 30, 00, 00, System.DateTimeKind.Utc);
            Bloque a = new Bloque(1, "Adrian", "enfermedad", "123abc", "pre123abc", pfech);
            Bloque b = new Bloque(1, "Adrian", "enfermedad", "123abc", "pre123abc", pfech);
            string ha = man.Hash256(a);
            string hb = man.Hash256(b);
            Assert.AreEqual(ha, hb);
        }
        [TestMethod]
        public void TestDeBlockchain_TraeBloquePorIndice()
        {
            Manager man = Manager.Instance;
            DateTime pfech = new DateTime(2001, 6, 7, 12, 30, 00, 00, System.DateTimeKind.Utc);
            DateTime now = new DateTime(2021, 4, 20, 12, 30, 00, 00, System.DateTimeKind.Utc);
            man.Inicializar(now);
            man.AgregarBloque("manuel", "enfermedad", "certmed.pdf", pfech);
            man.AgregarBloque("jose", "vacaciones", "solicitud.doc", pfech);
            man.AgregarBloque("arturo", "licencia", "licencia.pdf", pfech);
            man.AgregarBloque("arturo", "vacaciones", "boletos.pdf", pfech);
            Bloque block = man.GetBloqueIndice(3);
            Bloque pre = man.GetBloqueIndice(2);
            Assert.AreEqual("arturo", block.GetNombre());
            Assert.AreEqual("licencia", block.GetMotivo());
            Assert.AreEqual("licencia.pdf", block.GetFileHash());
            Assert.AreEqual(3, (int)block.GetIndice());
            Assert.AreEqual(pre.GetHash(), block.GetPrevHash());
        }
        [TestMethod]
        public void TestDeHash_VerificarHashAnterior()
        {
            Manager man = Manager.Instance;
            DateTime ini = new DateTime(2001, 6, 7, 12, 30, 00, 00, System.DateTimeKind.Utc);
            man.Inicializar(ini);
            DateTime pfech = new DateTime(2001, 6, 7, 12, 30, 00, 00, System.DateTimeKind.Utc);
            man.AgregarBloque("manuel", "enfermedad", "certmed.pdf", pfech);
            man.AgregarBloque("jose", "vacaciones", "solicitud.doc", pfech);
            man.AgregarBloque("arturo", "licencia", "licencia.pdf", pfech);
            Bloque b1 = man.GetBloqueIndice(1);
            Bloque b2 = man.GetBloqueIndice(2);
            Bloque b3 = man.GetBloqueIndice(3);
            Assert.AreEqual("fc5ab006e039fa9c10fae52c08d2eba8b52a04b27548b36612586c9c442d4b89", man.GetBloqueIndice(0).GetHash());
            Assert.AreEqual("fc5ab006e039fa9c10fae52c08d2eba8b52a04b27548b36612586c9c442d4b89", b1.GetPrevHash());
            Assert.AreEqual("031df73849ef340b02e8442b9cc9b1afdf78efae02041f2cc4962af80e8ad357", b1.GetHash());
            Assert.AreEqual("031df73849ef340b02e8442b9cc9b1afdf78efae02041f2cc4962af80e8ad357", b2.GetPrevHash());
            Assert.AreEqual("0e20c629b95581c88c80c9735ce68e88789449646c28238dacdba37bed14cfeb", b2.GetHash());
            Assert.AreEqual("0e20c629b95581c88c80c9735ce68e88789449646c28238dacdba37bed14cfeb", b3.GetPrevHash());
        }
        [TestMethod]
        public void TestBloqueToArray()
        {
            Manager man = Manager.Instance;
            DateTime now = new DateTime(2021, 4, 20, 12, 30, 00, 00, System.DateTimeKind.Utc);
            man.Inicializar(now);
            DateTime pfech = new DateTime(2001, 6, 7, 12, 30, 00, 00, System.DateTimeKind.Utc);
            Bloque a = new Bloque(0, "Adrian", "enfermedad", "123abc", "pre123abc", pfech);
            string esp = "10Adrianenfermedad123abcpre123abc6/7/2001 12:30:00 PM";
            Assert.AreEqual(esp, a.ToString());
        }
        [TestMethod]
        public void TestSingleton()
        {
            Manager man = Manager.Instance;
            DateTime now = new DateTime(2021, 4, 20, 12, 30, 00, 00, System.DateTimeKind.Utc);
            man.Inicializar(now);
            Manager m1 = Manager.Instance;
            Manager m2 = Manager.Instance;
            Assert.AreEqual(m1.GetHashCode(), m2.GetHashCode());
        }
        [TestMethod]
        public void TestSingleton2()
        {
            Manager man = Manager.Instance;
            DateTime now = new DateTime(2021, 4, 20, 12, 30, 00, 00, System.DateTimeKind.Utc);
            man.Inicializar(now);
            Manager m1 = Manager.Instance;
            Manager m2 = Manager.Instance;
            m1.AgregarBloque("manuel", "enfermedad", "certmed.pdf");

            Assert.AreEqual(m1.GetBloqueIndice(1).GetHash(), m2.GetBloqueIndice(1).GetHash());
        }
        [TestMethod]
        public void TestBloqueGenesis()
        {
            Manager man = Manager.Instance;
            DateTime ini = new DateTime(2001, 6, 7, 12, 30, 00, 00, System.DateTimeKind.Utc);
            man.Inicializar(ini);
            Bloque gen = man.GetBloqueIndice(0);
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
            Manager man = Manager.Instance;
            DateTime ini = new DateTime(2001, 6, 7, 12, 30, 00, 00, System.DateTimeKind.Utc);
            man.Inicializar(ini);
            DateTime pfech = new DateTime(2001, 6, 15, 12, 30, 00, 00, System.DateTimeKind.Utc);
            man.AgregarBloque("manuel", "enfermedad", "certmed.pdf", pfech);
            man.AgregarBloque("jose", "vacaciones", "solicitud.doc", pfech);
            Bloque b1 = man.GetBloqueIndice(1);
            Bloque b2 = man.GetBloqueIndice(2);            
            Assert.AreEqual("0758d34f16f9de02de5933e3e0fb6d5fcc727b2d69fd8255a2d4ae9ce329dac6", b1.GetHash());
            Assert.AreEqual("05bd8185190ee6bd294fd3993825b6d2e4fe851a1164291e9ecb55b852bbbcf6", b2.GetHash());
            Assert.AreEqual(b1, man.GetBloquePorHash("0758d34f16f9de02de5933e3e0fb6d5fcc727b2d69fd8255a2d4ae9ce329dac6"));
            Assert.AreEqual(b2, man.GetBloquePorHash("05bd8185190ee6bd294fd3993825b6d2e4fe851a1164291e9ecb55b852bbbcf6"));
            
            
        }
        [TestMethod]
        public void TestDeBusquedaPorHash_VerificaNullCuandoNoExisteBloque()
        {
            Manager man = Manager.Instance;
            DateTime ini = new DateTime(2001, 6, 7, 12, 30, 00, 00, System.DateTimeKind.Utc);
            man.Inicializar(ini);
            DateTime pfech = new DateTime(2001, 6, 15, 12, 30, 00, 00, System.DateTimeKind.Utc);
            man.AgregarBloque("manuel", "enfermedad", "certmed.pdf", pfech);
            man.AgregarBloque("jose", "vacaciones", "solicitud.doc", pfech);
            Assert.AreEqual(null, man.GetBloquePorHash("noexisteestehash"));
        }
        [TestMethod]
        public void TestDeBloque()
        {
            Manager man = Manager.Instance;
            DateTime ini = new DateTime(2001, 6, 7, 12, 30, 00, 00, System.DateTimeKind.Utc);
            man.Inicializar(ini);
            ulong p = 42;
            DateTime pfech = new DateTime(2001, 6, 7, 12, 30, 00, 00, System.DateTimeKind.Utc);
            man.AgregarBloque("manuel", "enfermedad", "certmed.pdf", pfech);
            Bloque b1 = man.GetBloqueIndice(1);
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
            Manager man = Manager.Instance;
            DateTime ini = new DateTime(2001, 6, 7, 12, 30, 00, 00, System.DateTimeKind.Utc);
            man.Inicializar(ini);
            DateTime fechapar = new DateTime(2001, 6, 12, 12, 30, 00, 00, System.DateTimeKind.Utc);
            DateTime fechaimpar = new DateTime(2001, 6, 7, 12, 30, 00, 00, System.DateTimeKind.Utc);
            man.AgregarBloque("manuel", "enfermedad", "certmed.pdf", fechapar);
            man.AgregarBloque("jose", "vacaciones", "solicitud.doc", fechaimpar);
            Bloque bloquepar = man.GetBloqueIndice(1);
            Bloque bloqueimpar = man.GetBloqueIndice(2);
            Assert.AreEqual(bloquepar.GetHash()[0], '0');
            Assert.AreEqual(bloquepar.GetHash()[1], '0');
            Assert.AreEqual(bloqueimpar.GetHash()[0], '0');
        }
        [TestMethod]
        public void CadenaCheck()
        {
            Manager man = Manager.Instance;
            ulong p = 58;
            DateTime ini = new DateTime(2001, 6, 7, 12, 30, 00, 00, System.DateTimeKind.Utc);
            man.Inicializar(ini);
            DateTime fechapar = new DateTime(2001, 6, 12, 12, 30, 00, 00, System.DateTimeKind.Utc);
            DateTime fechaimpar = new DateTime(2001, 6, 7, 12, 30, 00, 00, System.DateTimeKind.Utc);
            for(int i=1; i<=100; i++)
            {
                if (i % 2 == 0)
                {
                    man.AgregarBloque(String.Concat("bloque", i.ToString(), "@gmail.com"), "motivo", "motivo.doc", fechapar);
                } else if (i % 2 != 0)
                {
                    man.AgregarBloque(String.Concat("bloque", i.ToString(), "@gmail.com"), "motivo", "motivo.doc", fechaimpar);
                }
            }
            Bloque b50 = man.GetBloqueIndice(50);
            Assert.AreEqual("6/12/2001 12:30:00 PM", b50.GetFecha().ToString());
            Assert.AreEqual("motivo.doc", b50.GetFileHash());
            Assert.AreEqual("00a57c6f990750962dad825676f4f0ac7ffc66365c4decacf68d42f03c304baf", b50.GetHash());
            Assert.AreEqual(50, b50.GetIndice());
            Assert.AreEqual("motivo", b50.GetMotivo());
            Assert.AreEqual("bloque50@gmail.com", b50.GetNombre());
            Assert.AreEqual(p, b50.GetNonce());
            Assert.AreEqual("0cf51538490e85eb2bcbe99886ddd33c504e341a1d342944a1d72ffe9147a31b", b50.GetPrevHash());
            Assert.AreEqual(man.GetBloqueIndice(49), man.GetBloquePorHash("0cf51538490e85eb2bcbe99886ddd33c504e341a1d342944a1d72ffe9147a31b"));
            Assert.AreEqual("bloque100@gmail.com", man.GetBloqueIndice(100).GetNombre());
            Bloque b90 = man.GetBloqueIndice(90);
            Assert.AreEqual("00bffae1ff83525f39ec4f3fd3c0bc9a754f0d28a0438a0ac018f163e7c07452", b90.GetHash());
            Assert.AreEqual(man.Hash256(b90), "00bffae1ff83525f39ec4f3fd3c0bc9a754f0d28a0438a0ac018f163e7c07452");
        }
        [TestMethod]
        public void VerificarCadena()
        {
            Manager man = Manager.Instance;            
            DateTime ini = new DateTime(2001, 6, 7, 12, 30, 00, 00, System.DateTimeKind.Utc);
            man.Inicializar(ini);
            DateTime fechapar = new DateTime(2001, 6, 12, 12, 30, 00, 00, System.DateTimeKind.Utc);
            DateTime fechaimpar = new DateTime(2001, 6, 7, 12, 30, 00, 00, System.DateTimeKind.Utc);
            for (int i = 1; i <= 100; i++)
            {
                if (i % 2 == 0)
                {
                    man.AgregarBloque(String.Concat("bloque", i.ToString(), "@gmail.com"), "motivo", "motivo.doc", fechapar);
                }
                else if (i % 2 != 0)
                {
                    man.AgregarBloque(String.Concat("bloque", i.ToString(), "@gmail.com"), "motivo", "motivo.doc", fechaimpar);
                }
            }
            bool esvalida = man.VerificarCadena();
            Assert.IsTrue(esvalida);
        }
        [TestMethod]
        public void TestDeTraerUltimoBloque()
        {
            Manager man = Manager.Instance;
            DateTime ini = new DateTime(2001, 6, 7, 12, 30, 00, 00, System.DateTimeKind.Utc);
            man.Inicializar(ini);
            DateTime fechapar = new DateTime(2001, 6, 12, 12, 30, 00, 00, System.DateTimeKind.Utc);
            DateTime fechaimpar = new DateTime(2001, 6, 7, 12, 30, 00, 00, System.DateTimeKind.Utc);
            for (int i = 1; i <= 100; i++)
            {
                if (i % 2 == 0)
                {
                    man.AgregarBloque(String.Concat("bloque", i.ToString(), "@gmail.com"), "motivo", "motivo.doc", fechapar);
                }
                else if (i % 2 != 0)
                {
                    man.AgregarBloque(String.Concat("bloque", i.ToString(), "@gmail.com"), "motivo", "motivo.doc", fechaimpar);
                }
            }
            Assert.AreEqual(man.GetBloqueIndice(100), man.GetUltimoBloque());
        }
        [TestMethod]
        public void TestDeCantidadDeBloques()
        {
            Manager man = Manager.Instance;
            DateTime ini = new DateTime(2001, 6, 7, 12, 30, 00, 00, System.DateTimeKind.Utc);
            man.Inicializar(ini);
            DateTime pfech = new DateTime(2001, 6, 7, 12, 30, 00, 00, System.DateTimeKind.Utc);
            man.AgregarBloque("manuel", "enfermedad", "certmed.pdf", pfech);
            man.AgregarBloque("jose", "vacaciones", "solicitud.doc", pfech);
            man.AgregarBloque("arturo", "licencia", "licencia.pdf", pfech);
            Assert.AreEqual(4, man.GetCantidadDeBloques()); 
        }
    }    
}
