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
    public partial class Form12 : Form
    {
        public Form12()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Объявляем переменную для передачи значения в другую форму
            string variable = textBox1.Text;
            //Класс SomeClass объявлен в файле Program.cs, в нём объявлено простое поле. Наша задача, присвоить этому полю значение, 
            //а в другой форме его вытащить.
            SomeClass.variable_class = variable;
            Form12_2 frm = new Form12_2();
            frm.ShowDialog();
        }
    }
}
