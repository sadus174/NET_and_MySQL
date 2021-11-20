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
    public partial class Form17_auth2 : Form
    {
        // строка подключения к БД
        string connStr = "server=caseum.ru;port=33333;user=test_user;database=db_test;password=test_pass;";
        //Переменная соединения
        MySqlConnection conn;
        //Логин и пароль к данной форме Вы сможете посмотреть в БД db_test таблице t_user

        //Вычисление хэша строки и возрат его из метода
        static string sha256(string randomString)
        {
            //Тут происходит криптографическая магия. Смысл данного метода заключается в том, что строка залетает в метод
            var crypt = new System.Security.Cryptography.SHA256Managed();
            var hash = new System.Text.StringBuilder();
            byte[] crypto = crypt.ComputeHash(Encoding.UTF8.GetBytes(randomString));
            foreach (byte theByte in crypto)
            {
                hash.Append(theByte.ToString("x2"));
            }
            return hash.ToString();
        }

        //Метод запроса данных пользователя по логину для запоминания их в полях класса
        public void GetUserInfo(string login_user)
        {
            //Объявлем переменную для запроса в БД
            string selected_id_stud = textBox1.Text;
            // устанавливаем соединение с БД
            conn.Open();
            // запрос
            string sql = $"SELECT * FROM t_user WHERE loginUser='{login_user}'";
            // объект для выполнения SQL-запроса
            MySqlCommand command = new MySqlCommand(sql, conn);
            // объект для чтения ответа сервера
            MySqlDataReader reader = command.ExecuteReader();
            // читаем результат
            while (reader.Read())
            {
                // элементы массива [] - это значения столбцов из запроса SELECT
                Auth.auth_id = reader[0].ToString();
                Auth.auth_fio = reader[1].ToString();
                Auth.auth_role = Convert.ToInt32(reader[4].ToString());
            }
            reader.Close(); // закрываем reader
            // закрываем соединение с БД
            conn.Close();
        }
        public Form17_auth2()
        {
            InitializeComponent();
        }

        //Отмена авторизации. Закрытие формы
        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Запрос в БД на предмет того, если ли строка с подходящим логином и паролем
            string sql = "SELECT * FROM t_user WHERE loginUser = @un and  passUser= @up";
            //Открытие соединения
            conn.Open();
            //Объявляем таблицу
            DataTable table = new DataTable();
            //Объявляем адаптер
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            //Объявляем команду
            MySqlCommand command = new MySqlCommand(sql, conn);
            //Определяем параметры
            command.Parameters.Add("@un", MySqlDbType.VarChar, 25);
            command.Parameters.Add("@up", MySqlDbType.VarChar, 25);
            //Присваиваем параметрам значение
            command.Parameters["@un"].Value = textBox1.Text;
            command.Parameters["@up"].Value = sha256(textBox2.Text);
            //Заносим команду в адаптер
            adapter.SelectCommand = command;
            //Заполняем таблицу
            adapter.Fill(table);
            //Закрываем соединение
            conn.Close();
            //Если вернулась больше 0 строк, значит такой пользователь существует
            if (table.Rows.Count > 0)
            {
                //Присваеваем глобальный признак авторизации
                Auth.auth = true;
                //Достаем данные пользователя в случае успеха
                GetUserInfo(textBox1.Text);
                //Закрываем форму
                this.Close();
            }
            else
            {
                //Отобразить сообщение о том, что авторизаия неуспешна
                MessageBox.Show("Неверные данные авторизации!");
            }           
        }

        private void Form17_auth2_Load(object sender, EventArgs e)
        {
            //Инициализируем соединение с подходящей строкой
            conn = new MySqlConnection(connStr);
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            //В текстбокс3 формируется хэш по мере ввода текста во второй текстбокс, используется метод шифрования (хэширования)
            textBox3.Text = sha256(textBox2.Text);
        }
        //**************************************************//
        //                                                  //
        //                                                  //
        //      Тут всякие методы, которые отвечают         //
        //      за быстрое заполнения текстбоксов           //                                 
        //                                                  //
        //**************************************************//

        private void label4_Click(object sender, EventArgs e)
        {
            textBox1.Text = label4.Text;
        }

        private void label5_Click(object sender, EventArgs e)
        {
            textBox2.Text = label5.Text;
        }

        private void label7_Click(object sender, EventArgs e)
        {
            textBox1.Text = label7.Text;
        }

        private void label6_Click(object sender, EventArgs e)
        {
            textBox2.Text = label6.Text;
        }

        private void label9_Click(object sender, EventArgs e)
        {
            textBox1.Text = label9.Text;
        }

        private void label8_Click(object sender, EventArgs e)
        {
            textBox2.Text = label8.Text;
        }
    }
}
