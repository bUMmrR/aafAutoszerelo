using NUnit.Framework;
using System;
using aafAutoszerelo;

namespace UnitTestProject1
{
    [TestFixture]
    public class szerelokTesztek
    {
        [Test]
        public void munkakorLekerdezese()
        {
            szerelok temp = new szerelok("o;teszt;0,0,0,0,");
            Assert.AreEqual("teszt", temp.getNev());
        }

        [Test]
        public void hanySzabadOraTeszt()
        {
            szerelok temp = new szerelok("o;teszt;0000000000,0000000000,0000000000,0000000000,");
            Assert.AreEqual(50, temp.hanySzabadOra());
        }
        [Test]
        public void hanySzabadOraTeszt2()
        {
            szerelok temp = new szerelok("o;teszt;1111111111,0000000000,0000000000,0000000000,");
            Assert.AreEqual(40, temp.hanySzabadOra());
        }

        // kell teszt:  KikVegzikAMunkat, HanyEsMelyikoraFoglalt, SzereloElfoglaltsaga, EgynekHanySzabadOra, OssesSzabadOrakSzama

    }
}
