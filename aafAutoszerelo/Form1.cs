using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace aafAutoszerelo
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        List<szerelok> szereloks = new List<szerelok>();

        private void Form1_Load(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = 0;
            adatbeolvasas("input.txt");
            tablazatFelkeszitise();
            tablazatFeltoltese();
        }

        private void tablazatFeltoltese()
        {
            for (int i = 0; i < dataGridView1.ColumnCount; i++)
            {
                for (int j = 0; j < dataGridView1.RowCount; j++)
                {
                    tablazatbaIras(i, j);
                }
            }
        }

        private void tablazatbaIras(int i, int j)
        {
            int nap = comboBox1.SelectedIndex;
            if (i == 0)
                dataGridView1.Rows[j].Cells[i].Value = szereloks[j].getNev()+ " (" + szereloks[j].munkakor +")";
            else
            {
                if (szereloks[j].lefoglatOrak[nap][i-1] == '1')
                    dataGridView1.Rows[j].Cells[i].Style.BackColor = Color.Red;
                else
                    dataGridView1.Rows[j].Cells[i].Style.BackColor = Color.Green;
            }
        }

        private void tablazatFelkeszitise()
        {
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.RowCount = szereloks.Count;
            dataGridView1.ColumnCount = 11;
            dataGridView1.Columns[0].Width = 200;
            for (int i = 1; i < dataGridView1.ColumnCount; i++)
            {
                dataGridView1.Columns[i].Width = 25;
            }
        }



        private void adatbeolvasas(string path)
        {
            StreamReader r = new StreamReader(path);
            while(!r.EndOfStream)
            {
                szereloks.Add(new szerelok(r.ReadLine()));
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex != 0 && szereloks[e.RowIndex].lefoglatOrak[comboBox1.SelectedIndex][e.ColumnIndex-1] != '1')
            {
                DialogResult dr = MessageBox.Show("Leszeretné foglali ezt a helyet?", "Foglalás", MessageBoxButtons.YesNo);
                if (dr == DialogResult.Yes)
                {
                    dataGridView1[e.ColumnIndex, e.RowIndex].Style.BackColor = Color.Red;
                    szereloks[e.RowIndex].lefoglatOrak[comboBox1.SelectedIndex][e.ColumnIndex - 1] = '1';
                }
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            tablazatFeltoltese();
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox2.SelectedIndex == 0)
                label3.Text = SzereloElfoglaltsaga(textBox1.Text) != null ? textBox1.Text + " szerelő elfoglaltsága: " + SzereloElfoglaltsaga(textBox1.Text) : "Nem található ilyen személy";
            else if (comboBox2.SelectedIndex == 1)
                label3.Text = HanyEsMelyikoraFoglalt(textBox1.Text);
            else if (comboBox2.SelectedIndex == 2)
                label3.Text = KikVegzikAMunkat(textBox1.Text);
            else if (comboBox2.SelectedIndex == 3)
                Console.WriteLine("asd");
            else if (comboBox2.SelectedIndex == 4)
                label3.Text = EgynekHanySzabadOra(textBox1.Text) != -1 ? EgynekHanySzabadOra(textBox1.Text) + " db órája van szabadon " + textBox1.Text + "-nak/nek" : "Nem található ilyen személy";
            else if (comboBox2.SelectedIndex == 5)
                label3.Text = OssesSzabadOrakSzama() + " db óra van összesen szabadon";
        }

        /// <summary>
        /// Viszaadja hogy egy azonos munkakőrt ki végzi el
        /// </summary>
        /// <param name="text">A munkakőr neve</param>
        /// <returns>A munkakőr dolgozoit, vagy hogy nem található ilyen munkakőr</returns>

        private string KikVegzikAMunkat(string text)
        {
            var szereloink = szereloks.Where(c => c.munkakor == text).ToList();
            string nevek = "";
            if (szereloink.Count != 0)
            {
                foreach (var item in szereloink)
                {
                    nevek += item.getNev() + ", ";
                }
                return string.Format("Ők végzik a kiválasztott munkakőrt: " + nevek);
            }
            return "Nem található ilyen munkakőr";
        }


        /// <summary>
        /// Egy adott embernek viszadja a foglalt óráinak számát, illetve hogy melyik napokon vannak lefoglalva
        /// </summary>
        /// <param name="nev">A szerelő neve</param>
        /// <returns></returns>
        private string HanyEsMelyikoraFoglalt(string nev)
        {
            szerelok szerelonk = szereloks.Where(c => c.getNev() == nev).FirstOrDefault();
            int ora;
            string napok = "";
            if (szerelonk != null)
            {
                ora = 50-szerelonk.hanySzabadOra();
                for (int i = 0; i < szerelonk.lefoglatOrak.Count; i++)
                {
                    if (szerelonk.lefoglatOrak[i].Contains('1'))
                    {
                        napok += comboBox1.Items[i] +";";
                    }
                }
                return string.Format(ora+" Foglalt oraja van és ezeken a napokon van lefoglalva: " + napok);
            }
            return "Nem található ilyen személy";
        }

        private string SzereloElfoglaltsaga(string nev)
        {
            szerelok szerelonk = szereloks.Where(c => c.getNev() == nev).FirstOrDefault();
            if (szerelonk != null)
            {
                return szerelonk.munkakor;
            }
            return null;
        }

        private int EgynekHanySzabadOra(string nev)
        {
            szerelok szerelonk = szereloks.Where(c => c.getNev() == nev).FirstOrDefault();
            if (szerelonk != null)
            {
                return szerelonk.hanySzabadOra();
            }
            return -1;
        }

        /// <summary>
        /// Az ősszes szerelő szabad óráit szamolja ki
        /// </summary>
        /// <returns>A szerelők szabad óráit</returns>
        private int OssesSzabadOrakSzama()
        {
            int db = 0;
            for (int i = 0; i < szereloks.Count; i++)
            {
                db += szereloks[i].hanySzabadOra();
            }
            return db;
        }
    }
}