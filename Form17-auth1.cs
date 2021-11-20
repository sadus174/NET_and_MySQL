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
    public partial class Form17_auth1 : Form
    {
        public Form17_auth1()
        {
            InitializeComponent();
        }

        //Метод расстановки функционала формы взависимости от роли пользователя
        public void ManagerRole(int role)
        {
            switch (role)
            {
                case 1:
                    label8.Text = "Максимальный";
                    label8.ForeColor = Color.Green;
                    button1.Enabled = true;
                    button2.Enabled = true;
                    button3.Enabled = true;
                    break;

                case 2:
                    label8.Text = "Умеренный";
                    label8.ForeColor = Color.YellowGreen;
                    button1.Enabled = false;
                    button2.Enabled = true;
                    button3.Enabled = true;
                    break;

                case 3:
                    label8.Text = "Минимальный";
                    label8.ForeColor = Color.Yellow;
                    button1.Enabled = false;
                    button2.Enabled = false;
                    button3.Enabled = true;
                    break;
                default:
                    label8.Text = "Неопределённый";
                    label8.ForeColor = Color.Red;
                    button1.Enabled = false;
                    button2.Enabled = false;
                    button3.Enabled = false;
                    break;
            }
        }
        private void Form17_auth1_Load(object sender, EventArgs e)
        {
            //Сокрытие текущей формы
            this.Hide();
            //Инициализируем и вызываем форму диалога авторизации
            Form17_auth2 form17_Auth2 = new Form17_auth2();
            //Вызов формы в режиме диалога
            form17_Auth2.ShowDialog();
            //Если авторизации была успешна и в поле класса хранится истина, то
            if(Auth.auth)
            {
                //Отображаем рабочую форму
                this.Show();
                //Вытаскиваем из класса поля в label'ы
                label5.Text = Auth.auth_id;
                label4.Text = Auth.auth_fio;
                label6.Text = "Успешна!";
                label6.ForeColor = Color.Green;
                //Вызываем метод управления ролями
                ManagerRole(Auth.auth_role);


            }
            else
            {
                //Закрываем форму
                this.Close();
            }
        }
    }
}
