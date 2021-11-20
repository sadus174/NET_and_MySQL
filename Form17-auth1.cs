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

        private void Form17_auth1_Load(object sender, EventArgs e)
        {
            //Сокрытие текущей формы
            this.Hide();
            //Инициализируем и вызываем форму диалога авторизации
            Form17_auth2 form17_Auth2 = new Form17_auth2();
            //Вызов формы в режиме диалога
            form17_Auth2.ShowDialog();

            if(Auth.auth)
            {
                this.Show();
                label5.Text = Auth.auth_id;
                label4.Text = Auth.auth_fio;
            }
            else
            {
                //Закрываем форму
                this.Close();
            }
        }
    }
}
