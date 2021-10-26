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
    class ControlData
    {
        //Определяем параметры подключения
        private const string host = "caseum.ru";
        private const string port = "33333";
        private const string database = "db_test";
        private const string username = "test_user";
        private const string password = "test_pass";
        //Объявляем и инициализируем соединение
        private static readonly MySqlConnection conn = GetDBConnection();
        //DataAdapter представляет собой объект Command , получающий данные из источника данных.
        private static readonly MySqlDataAdapter MyDA = new MySqlDataAdapter();
        //Объявление BindingSource, основная его задача, это обеспечить унифицированный доступ к источнику данных.
        private static readonly BindingSource bSource = new BindingSource();
        //DataSet - расположенное в оперативной памяти представление данных, обеспечивающее согласованную реляционную программную 
        //модель независимо от источника данных.DataSet представляет полный набор данных, включая таблицы, содержащие, упорядочивающие 
        //и ограничивающие данные, а также связи между таблицами.
        private static DataSet ds = new DataSet();
        //Представляет одну таблицу данных в памяти.
        private static DataTable table = new DataTable();


        //Статичный метод, формирующий строку для подключения и возвращающий MySqlConnection
        public static MySqlConnection GetDBConnection()
        {
            //Формируем строку подключения
            string connString = $"server={host};port={port};user={username};database={database};password={password};";
            //Создаём соединение с нашей строкой подключения
            MySqlConnection conn = new MySqlConnection(connString);
            //Возвращаем данное соединение из метода
            return conn;
        }

        //Метод удаления строк
        public static void DeleteUser(string id_stud)
        {
            //Формируем строку запроса на добавление строк
            string sql_delete_user = "DELETE FROM t_stud WHERE id='" + id_stud + "'";
            //Посылаем запрос на обновление данных
            MySqlCommand delete_user = new MySqlCommand(sql_delete_user, conn);
            conn.Open();
            delete_user.ExecuteNonQuery();
            conn.Close();
        }

        //Метод изменения статуса
        public static void ChangeStateStudent(string new_state, string redact_id)
        {
            // устанавливаем соединение с БД
            conn.Open();
            // запрос обновления данных
            string query2 = $"UPDATE t_stud SET id_state='{new_state}' WHERE (id='{redact_id}')";
            // объект для выполнения SQL-запроса
            MySqlCommand command = new MySqlCommand(query2, conn);
            // выполняем запрос
            command.ExecuteNonQuery();
            // закрываем подключение к БД
            conn.Close();
            //Обновляем DataGrid
        }

        //Метод заполнения грида
        public static BindingSource GetListUsers()
        {
            // устанавливаем соединение с БД
            conn.Open();
            //Запрос для вывода строк в БД
            string commandStr = "SELECT id AS 'Код', fio AS 'ФИО', age AS 'Возраст', theme_kurs AS 'Тема курсовой', id_state AS 'Статус' FROM t_stud";
            //Объявляем команду, которая выполнить запрос в соединении conn
            MyDA.SelectCommand = new MySqlCommand(commandStr, conn);
            //Заполняем таблицу записями из БД
            MyDA.Fill(table);
            //Указываем, что источником данных в bindingsource является заполненная выше таблица
            bSource.DataSource = table;           
            //Закрываем соединение
            conn.Close();
            //Возвращаем bindingSource
            return bSource;
        }

        public static void ReloadList()
        {
            //Чистим виртуальную таблицу
            table.Clear();
        }
    }
}
    

