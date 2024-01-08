using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using NUnit.Framework.Legacy;

namespace aafAutoszerelo
{
    public class szerelok
    {
        public string munkakor;
        private string nev;
        public List<List<char>> lefoglatOrak = new List<List<char>>();

        public szerelok(string path)
        {
            string[] sv = path.Split(';');
            munkakor = sv[0];
            nev = sv[1];
            string[] sv2 = sv[2].Split(',');
            for (int i = 0; i < sv2.Length; i++)
            {
                lefoglatOrak.Add(sv2[i].ToCharArray().ToList());
            }
        }


        public string getNev()
        {
            return nev;
        }

        public int hanySzabadOra()
        {
            int db = 50;
            for (int i = 0; i < lefoglatOrak.Count; i++)
            {
                db -= lefoglatOrak[i].Count(c => c == '1');
            }
            return db;
        }


    }

    [TestFixture]
    public class szerelokTesztek
    {
        [Test]
        public void munkakorLekerdezese()
        {
            szerelok temp = new szerelok("o;teszt;0,0,0,0,");
            ClassicAssert.AreEqual("teszt", temp.getNev());
        }

        [Test]
        public void hanySzabadOraTeszt()
        {
            szerelok temp = new szerelok("o;teszt;0000000000,0000000000,0000000000,0000000000,");
            ClassicAssert.AreEqual(50, temp.hanySzabadOra());
        }
        [Test]
        public void hanySzabadOraTeszt2()
        {
            szerelok temp = new szerelok("o;teszt;1111111111,0000000000,0000000000,0000000000,");
            ClassicAssert.AreEqual(40, temp.hanySzabadOra());
        }

    }

}
