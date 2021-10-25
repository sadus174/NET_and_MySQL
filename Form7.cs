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
    public partial class Form7 : Form
    {
        //Класс для подключения к БД
        static class DBUtils
        {
            //Статичный метод, формирующий строку для подключения и возвращающий MySqlConnection
            public static MySqlConnection GetDBConnection()
            {                
                string host = "caseum.ru";
                string port = "33333";
                string database = "db_test";
                string username = "test_user";
                string password = "test_pass";
                string connString = $"server={host};port={port};user={username};database={database};password={password};";
                MySqlConnection conn = new MySqlConnection(connString);
                return conn;
            }
        }

        //Класс для манипулирования данными
        static class GetData
        {
            //Метод для запроса студентов
            static public void SelectStudents(ListBox lv, MySqlConnection conn)
            {
                //Чистим ListBox
                lv.Items.Clear();
                // устанавливаем соединение с БД
                conn.Open();
                // запрос
                string sql = $"SELECT * FROM t_prep";
                // объект для выполнения SQL-запроса
                MySqlCommand command = new MySqlCommand(sql, conn);
                // объект для чтения ответа сервера
                MySqlDataReader reader = command.ExecuteReader();
                // читаем результат
                while (reader.Read())
                {
                    //Присваеваем переменным значения в ридере
                    // элементы массива [] - это значения столбцов из запроса SELECT
                    string id_prepods = reader[0].ToString();
                    string name_prepods = reader[1].ToString();
                    string dolg_prepods = reader[2].ToString();
                    //Выводим в ListBox
                    lv.Items.Add($"{id_prepods}) {name_prepods} - {dolg_prepods}");
                }
                // закрываем reader
                reader.Close();
                // закрываем соединение с БД                
                conn.Close();
            }
        }

        public Form7()
        {
            InitializeComponent();
        }

        private void Form7_Load(object sender, EventArgs e)
        {
            //Объявляем объект MySqlConnection и присваеваем к нему возвращённое соединение из метода класса DBUtils.GetDBConnection()
            MySqlConnection cnt = DBUtils.GetDBConnection();
            GetData.SelectStudents(listBox1, cnt);
        }
    }
}
