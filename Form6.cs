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
    public partial class Form6 : Form
    {
        //Простой метод принимающий в качества параметра любой ListBox и выводящий в него список преподавателей
        public void GetListPrepods(ListBox lv)
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

                lv.Items.Add($"{id_prepods}) {name_prepods} - {dolg_prepods}");


            }
            reader.Close(); // закрываем reader
            // закрываем соединение с БД
            conn.Close();

        }
        //Простой метод добавляющий в таблицу записи, в качестве параметров принимает ФИО и Предмет
        public bool DeletePrepods(string id_prepods)
        {
            //определяем переменную, хранящую количество вставленных строк
            int InsertCount = 0;
            //Объявляем переменную храняющую результат операции
            bool result = false;
            // открываем соединение
            conn.Open();
            // запрос удаления данных
            string query = $"DELETE FROM t_prep WHERE (id='{id_prepods}')";
            try
            {
                // объект для выполнения SQL-запроса
                MySqlCommand command = new MySqlCommand(query, conn);
                // выполняем запрос
                InsertCount = command.ExecuteNonQuery();
                // закрываем подключение к БД
            }
            catch
            {
                //Если возникла ошибка, то запрос не вставит ни одной строки
                InsertCount = 0;
            }
            finally
            {
                //Но в любом случае, нужно закрыть соединение
                conn.Close();
                //Ессли количество вставленных строк было не 0, то есть вставлена хотя бы 1 строка
                if (InsertCount != 0)
                {
                    //то результат операции - истина
                    result = true;
                }
            }
            //Вернём результат операции, где его обработает алгоритм
            return result;
        }
        //Объявляем соединения с БД
        MySqlConnection conn;

        public Form6()
        {
            InitializeComponent();
        }

        private void Form6_Load(object sender, EventArgs e)
        {
            // строка подключения к БД
            string connStr = "server=caseum.ru;port=33333;user=test_user;database=db_test;password=test_pass;";
            // создаём объект для подключения к БД
            conn = new MySqlConnection(connStr);
            //Вызов метода обновления списка преподавателей с передачей в качестве параметра ListBox
            GetListPrepods(listBox1);
            //Формируем колонки в элементе listView
            

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            string id_del = textBox1.Text;
            if (DeletePrepods(id_del))
            {
                GetListPrepods(listBox1);
            }
            //Иначе произошла какая то ошибка и покажем пользователю уведомление
            else
            {
                MessageBox.Show("Произошла ошибка.", "Ошибка");
            }
        }


    }
}
