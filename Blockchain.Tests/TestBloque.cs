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
            Manager man = Manager.Instance;
            DateTime now = new DateTime(2021, 4, 20, 12, 30, 00, 00, System.DateTimeKind.Utc);
            man.Inicializar(now);
            DateTime pfech = new DateTime(2001, 6, 6, 12, 30, 00, 00, System.DateTimeKind.Utc);
            Bloque a = new Bloque(10, "Adrian", "enfermedad", "123abc", "pre123abc", pfech);
            Bloque b = new Bloque(10, "Adrian", "enfermedad", "123abc", "pre123abc", pfech);
            string ha = man.HashCondicional(a);
            string hb = man.HashCondicional(b);
            Assert.AreEqual("00ca561f700c5db3305f74ca35d334cd8bed90f5a30b9901f3bbebfbbf10e651", hb);
            Assert.AreEqual("00ca561f700c5db3305f74ca35d334cd8bed90f5a30b9901f3bbebfbbf10e651", ha);
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
            Assert.AreEqual("573f2dd49949e06df958e50cbbe1e8d159d337511fec8970ac87a3cc77ccd77d", man.GetBloqueIndice(0).GetHash());
            Assert.AreEqual("573f2dd49949e06df958e50cbbe1e8d159d337511fec8970ac87a3cc77ccd77d", b1.GetPrevHash());
            Assert.AreEqual("0760bd9bdbcdfa3310a71cd9b47e90ced866695e77054700d93c8d8b2365f8d6", b1.GetHash());
            Assert.AreEqual("0760bd9bdbcdfa3310a71cd9b47e90ced866695e77054700d93c8d8b2365f8d6", b2.GetPrevHash());
            Assert.AreEqual("07d7d016e9d1ac3794bc8a374f04a9023424e09a9fcb83de5e358ad572e46bf8", b2.GetHash());
            Assert.AreEqual("07d7d016e9d1ac3794bc8a374f04a9023424e09a9fcb83de5e358ad572e46bf8", b3.GetPrevHash());
        }
        [TestMethod]
        public void TestBloqueToArray()
        {
            Manager man = Manager.Instance;
            DateTime now = new DateTime(2021, 4, 20, 12, 30, 00, 00, System.DateTimeKind.Utc);
            man.Inicializar(now);
            DateTime pfech = new DateTime(2001, 6, 7, 12, 30, 00, 00, System.DateTimeKind.Utc);
            Bloque a = new Bloque(0, "Adrian", "enfermedad", "123abc", "pre123abc", pfech);
            string esp = string.Concat("10Adrianenfermedad123abcpre123abc06/07/2001 12:30:00");
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
            Assert.AreEqual("573f2dd49949e06df958e50cbbe1e8d159d337511fec8970ac87a3cc77ccd77d", gen.GetHash());
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
            Assert.AreEqual(b1, man.GetBloquePorHash("0abf21bfdad9fb66a86645091a96380331ecfdc1f5cd8b1b8ed102c4bed206c9"));
            Assert.AreEqual(b2, man.GetBloquePorHash("0b458dc43fd73a0ef31a5c02fdd0b98361600e5f3b42612a0908d1a921d4b19d"));
        }
        [TestMethod]
        public void TestDeBloque()
        {
            Manager man = Manager.Instance;
            DateTime ini = new DateTime(2001, 6, 7, 12, 30, 00, 00, System.DateTimeKind.Utc);
            man.Inicializar(ini);
            ulong p = 13;
            DateTime pfech = new DateTime(2001, 6, 7, 12, 30, 00, 00, System.DateTimeKind.Utc);
            man.AgregarBloque("manuel", "enfermedad", "certmed.pdf", pfech);
            Bloque b1 = man.GetBloqueIndice(1);
            Assert.AreEqual("06/07/2001 12:30:00", b1.GetFecha().ToString());
            Assert.AreEqual("certmed.pdf", b1.GetFileHash());
            Assert.AreEqual("0760bd9bdbcdfa3310a71cd9b47e90ced866695e77054700d93c8d8b2365f8d6", b1.GetHash());
            Assert.AreEqual(1, b1.GetIndice());
            Assert.AreEqual("enfermedad", b1.GetMotivo());
            Assert.AreEqual("manuel", b1.GetNombre());
            Assert.AreEqual(p, b1.GetNonce());
            Assert.AreEqual("573f2dd49949e06df958e50cbbe1e8d159d337511fec8970ac87a3cc77ccd77d", b1.GetPrevHash());
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
            Assert.AreEqual("06/12/2001 12:30:00", b50.GetFecha().ToString());
            Assert.AreEqual("motivo.doc", b50.GetFileHash());
            Assert.AreEqual("0077605924c879169947eb3128a91e1db92f73a96641a664728a47a0fabc3d23", b50.GetHash());
            Assert.AreEqual(50, b50.GetIndice());
            Assert.AreEqual("motivo", b50.GetMotivo());
            Assert.AreEqual("bloque50@gmail.com", b50.GetNombre());
            Assert.AreEqual(p, b50.GetNonce());
            Assert.AreEqual("0cf51538490e85eb2bcbe99886ddd33c504e341a1d342944a1d72ffe9147a31b", b50.GetPrevHash());
            Assert.AreEqual(man.GetBloqueIndice(49), man.GetBloquePorHash("0cf51538490e85eb2bcbe99886ddd33c504e341a1d342944a1d72ffe9147a31b"));
            Assert.AreEqual("bloque100@gmail.com", man.GetBloqueIndice(100).GetNombre());


        }

    }
    
}
