using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
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



        /// <summary>
        /// A név megszerzése
        /// </summary>
        /// <returns>Név</returns>
        public string getNev()
        {
            return nev;
        }


        /// <summary>
        /// Az addot szerelőnek hány darab szabad órája van
        /// </summary>
        /// <returns>Szabad órák száma</returns>
        public int hanySzabadOra()
        {
            int db = 50;
            for (int i = 0; i < lefoglatOrak.Count; i++)
            {
                db -= lefoglatOrak[i].Count(c => c == '1');
            }
            return db;
        }


        /// <summary>
        /// A szerelőt visszaadja ugyanolyan formában mint a fáljbeolvasás
        /// </summary>
        /// <returns>Szerelő Fáljbeolvasásra készen </returns>
        public override string ToString()
        {
            StringBuilder result = new StringBuilder();
            result.Append($"{munkakor};");
            result.Append($"{nev};");
            foreach (List<char> orak in lefoglatOrak)
            {
                result.Append(string.Join("", orak));
                result.Append(",");
            }
            result.Length--;

            return result.ToString();
        }
    }
}
