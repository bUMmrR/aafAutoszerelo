using NUnit.Framework;
using System;
using aafAutoszerelo;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UnitTestProject1
{
    [TestFixture]
    public class szerelokOsztalyTesztek
    {
        [Test, MaxTime(10)]

        [TestCase("Elektronika;Szabó Emánuel;0,0,0,0")]
        [TestCase("Kerkékcsere;Tőbb Mint Kettő Nevű;0,0,0,0")]
        public void NevLekerdezese(string sor)
        {
            szerelok temp = new szerelok(sor);
            Assert.AreEqual(sor.Split(';')[1], temp.getNev());
        }

        [Test]
        public void hanySzabadOraTeszt()
        {
            szerelok temp = new szerelok("o;teszt;0000000000,0000000000,0000000000,0000000000");
            Assert.AreEqual(50, temp.hanySzabadOra());
        }
        [Test]
        public void hanySzabadOraTeszt2()
        {
            szerelok temp = new szerelok("o;teszt;1111111111,0000000000,0000000000,0000000000");
            Assert.AreEqual(40, temp.hanySzabadOra());
        }

        // kell teszt:  KikVegzikAMunkat, HanyEsMelyikoraFoglalt, SzereloElfoglaltsaga, EgynekHanySzabadOra, OssesSzabadOrakSzama
    }

    [TestFixture]
    public class Form1Tesztek
    {
        static List<szerelok> szereloks = new List<szerelok>();
        public void tesztFajlbeolvasas()
        {
            StreamReader r = new StreamReader("teszt.txt");
            while (!r.EndOfStream)
            {
                szereloks.Add(new szerelok(r.ReadLine()));
            }

        }

        [Test]
        public void tesztFajlbeolvasasTeszt()
        {
            tesztFajlbeolvasas();
            Assert.AreEqual(3, szereloks.Count);
        }



    }

}
