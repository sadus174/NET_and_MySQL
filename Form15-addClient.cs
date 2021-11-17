using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace NET_and_MySQL
{
    public partial class Form15_addClient : Form
    {
        //Объявляем объект соединения глобально
        MySqlConnection conn;
        public Form15_addClient()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Определяем значение переменных для записи в БД
            string n_fio = textBox1.Text;
            string n_phone = textBox2.Text;

            //Формируем запрос на изменени
            string sql_update_current_stud = $"INSERT INTO t_client (fioClient, phoneClient) " +
                                              $"VALUES ('{n_fio}', '{n_phone}'); " +
                                              $"SELECT idClient FROM t_client WHERE (idClient = LAST_INSERT_ID());";
            // устанавливаем соединение с БД
            conn.Open();
            // объект для выполнения SQL-запроса
            MySqlCommand command = new MySqlCommand(sql_update_current_stud, conn);
            // выполняем запрос
            string id_insert_client = command.ExecuteScalar().ToString();
            SomeClass.new_inserted_id = id_insert_client;
            //MessageBox.Show($"ID нового клиента {id_insert_client}");
            // закрываем подключение к БД
            conn.Close();
            //Закрываем форму
            this.Close();
        }

        private void Form15_addClient_Load(object sender, EventArgs e)
        {
            // строка подключения к БД
            string connStr = "server=caseum.ru;port=33333;user=test_user;database=db_test;password=test_pass;";
            // создаём объект для подключения к БД
            conn = new MySqlConnection(connStr);
        }
    }
}
