using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NET_and_MySQL
{
    public partial class Menu : Form
    {
        public Menu()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ExecuteScalar Form1 = new ExecuteScalar();
            Form1.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ExecuteReader Form2 = new ExecuteReader();
            Form2.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form3 Form3 = new Form3();
            Form3.ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form4 Form4 = new Form4();
            Form4.ShowDialog();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Form5 Form5 = new Form5();
            Form5.ShowDialog();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Form6 Form6 = new Form6();
            Form6.ShowDialog();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Form7 Form7 = new Form7();
            Form7.ShowDialog();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            Form8 Form8 = new Form8();
            Form8.ShowDialog();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            Form9 Form9 = new Form9();
            Form9.ShowDialog();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            Form10 Form10 = new Form10();
            Form10.ShowDialog();
            toolStripStatusLabel2.Text = ControlData.ID_STUD;

        }

        private void button11_Click(object sender, EventArgs e)
        {
            Form11 Form11= new Form11();
            Form11.ShowDialog();
        }

        private void button12_Click(object sender, EventArgs e)
        {
            Form12 Form12 = new Form12();
            Form12.ShowDialog();
        }
        private void button13_Click(object sender, EventArgs e)
        {
            Form13 Form13 = new Form13();
            Form13.ShowDialog();
        }

        private void button14_Click(object sender, EventArgs e)
        {
            Form14 Form14 = new Form14();
            Form14.ShowDialog();
        }

        private void button15_Click(object sender, EventArgs e)
        {
            Form15 Form15 = new Form15();
            Form15.ShowDialog();
        }

        private void button16_Click(object sender, EventArgs e)
        {
            Form16 Form16 = new Form16();
            Form16.ShowDialog();
        }

        private void button17_Click(object sender, EventArgs e)
        {
            Form17_auth1 form17_Auth1 = new Form17_auth1();
            form17_Auth1.ShowDialog();
        }

        private void button18_Click(object sender, EventArgs e)
        {
            Form18 form18 = new Form18();
            form18.ShowDialog();    
        }
    }
}
