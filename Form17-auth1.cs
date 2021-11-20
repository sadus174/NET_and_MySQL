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

        //Метод расстановки функционала формы взависимости от роли пользователя, которая передается посредством  поля класса,
        //в которое данное значени в свою очередь попало из запроса
        public void ManagerRole(int role)
        {
            switch (role)
            {
                //И в зависимости от того, какая роль (цифра) хранится в поле класса и передана в метод, показываются те или иные кнопки.
                //Вы можете скрыть их и не отображать вообще, здесь они просто выключены
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
                    //Если по какой то причине в классе ничего не содержится, то всё отключается вообще
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
            //Если авторизации была успешна и в поле класса хранится истина, то делаем движуху:
            if(Auth.auth)
            {
                //Отображаем рабочую форму
                this.Show();
                //Вытаскиваем из класса поля в label'ы
                label5.Text = Auth.auth_id;
                label4.Text = Auth.auth_fio;
                label6.Text = "Успешна!";
                //Красим текст в label в зелёный цвет
                label6.ForeColor = Color.Green;
                //Вызываем метод управления ролями
                ManagerRole(Auth.auth_role);
            }
            //иначе
            else
            {
                //Закрываем форму
                this.Close();
            }
        }
    }
}
