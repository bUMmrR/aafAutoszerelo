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

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
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
                Console.WriteLine("asd");
            else if (comboBox2.SelectedIndex == 2)
                Console.WriteLine("asd");
            else if (comboBox2.SelectedIndex == 3)
                Console.WriteLine("asd");
            else if (comboBox2.SelectedIndex == 4)
                label3.Text = EgynekHanySzabadOra(textBox1.Text) != -1 ? EgynekHanySzabadOra(textBox1.Text) + " db órája van szabadon " + textBox1.Text + "-nak/nek" : "Nem található ilyen személy";
            else if (comboBox2.SelectedIndex == 5)
                label3.Text = OssesSzabadOrakSzama() + " db óra van összesen szabadon";

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
