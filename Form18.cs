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
    public partial class Form18 : Form
    {
        public Form18()
        {
            InitializeComponent();
        }
        //Объявляем массивы элементов глобально
        TextBox[] textBox;
        Label[] label;
        //Объявляем количество элементов глобально
        int count_lb;
        int count_tb;
        int space;

        private void button1_Click(object sender, EventArgs e)
        {
            //Конвертируем количество элементов в число
            count_lb = Convert.ToInt32(textBox1.Text);
            count_tb = Convert.ToInt32(textBox2.Text);
            //Запоминаем величину отступа
            space = Convert.ToInt32(textBox3.Text);
            //Задаем начальные координаты объектов
            int start_lb = 100;
            int start_tb = 100;
            //Создаём массивы элементов определенный длины
            textBox = new TextBox[count_tb];
            label = new Label[count_lb];
            //В цикле создаём элементы для двух массивов
            for (int i = 0; i < count_lb; i++)
            {
                label[i] = new Label();
                label[i].Name = "n" + i;
                label[i].Text = "Введите число №" + (i+1);
                label[i].Location = new Point(10, start_lb);
                label[i].Width = 150;
                start_lb += space;
            }
            for (int i = 0; i < count_tb; i++)
            {
                textBox[i] = new TextBox();
                textBox[i].Name = "n" + i;
                textBox[i].Text = "n" + i;
                textBox[i].Location = new Point(170, start_tb);
                start_tb += space;
            }
            //Присваеваем созданные элементы на форму. Можно делать и в одном цикле, если количество элементов одинаковое
            for (int i = 0; i < count_lb; i++)
            {
                this.Controls.Add(label[i]);
            }
            for (int i = 0; i < count_tb; i++)
            {
                this.Controls.Add(textBox[i]);
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            //Уравниваем количество меток к количеству текстбоксов
            textBox2.Text = textBox1.Text;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //Объявляем переменную хранящую сумму
            int summ = 0;
            //В цикле крутим и суммируем то, что хранится внутри полей
            for (int i = 0; i < count_tb; i++)
            {
                summ += Convert.ToInt32(textBox[i].Text);
            }
            //Выводим сумму в месседжбокс
            MessageBox.Show(summ.ToString());
        }
    }
}
