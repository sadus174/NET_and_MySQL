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
    public partial class Form9 : Form
    {
        // строка подключения к БД
        string connStr = "server=caseum.ru;port=33333;user=test_user;database=db_test;password=test_pass;";
        // создаём объект для подключения к БД
        MySqlConnection conn_db;
        public void GetComboBoxList()
        {
            //Формирование списка статусов
            DataTable list_stud_table = new DataTable();
            MySqlCommand list_stud_command = new MySqlCommand();
            //Открываем соединение
            conn_db.Open();
            //Формируем столбцы для комбобокса списка ЦП
            list_stud_table.Columns.Add(new DataColumn("id", System.Type.GetType("System.Int32")));
            list_stud_table.Columns.Add(new DataColumn("fio", System.Type.GetType("System.String")));
            //Настройка видимости полей комбобокса
            comboBox1.DataSource = list_stud_table;
            comboBox1.DisplayMember = "fio";
            comboBox1.ValueMember = "id";
            //Формируем строку запроса на отображение списка статусов прав пользователя
            string sql_list_users = "SELECT id, fio FROM t_stud";
            list_stud_command.CommandText = sql_list_users;
            list_stud_command.Connection = conn_db;
            //Формирование списка ЦП для combobox'a
            MySqlDataReader list_stud_reader;
            try
            {
                //Инициализируем ридер
                list_stud_reader = list_stud_command.ExecuteReader();
                while (list_stud_reader.Read())
                {
                    DataRow rowToAdd = list_stud_table.NewRow();
                    rowToAdd["id"] = Convert.ToInt32(list_stud_reader[0]);
                    rowToAdd["fio"] = list_stud_reader[1].ToString();
                    list_stud_table.Rows.Add(rowToAdd);
                }
                list_stud_reader.Close();
                conn_db.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка чтения списка ЦП \n\n" + ex, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }
            finally
            {
                conn_db.Close();
            }
        }

        public Form9()
        {
            InitializeComponent();
        }

        private void Form9_Load(object sender, EventArgs e)
        {
            //Инициализируем соединение
            conn_db = new MySqlConnection(connStr);
            //Метод заполнения ComboBox
            GetComboBoxList();
        }
        private void GetList(int id_stud)
        {
            conn_db.Open();
            //Строка запроса
            string commandStr = "SELECT * FROM t_stud WHERE id = " + id_stud.ToString();
            //Команда для получения списка
            MySqlCommand cmd_get_list = new MySqlCommand(commandStr, conn_db);
            //Ридер для хранения списка строк
            MySqlDataReader reader_list = cmd_get_list.ExecuteReader();
            //Читаем ридер
            while (reader_list.Read())
            {
                //Формируем строку для вывода красивого сообщения в MessageBox
                string s = "";
                s += "Код студента: " + reader_list[0].ToString()+ "\n";
                s += "ФИО: " + reader_list[1].ToString()+ "\n";
                s += "Тема курсовой: " + reader_list[4].ToString() + "\n";
                s += "Возраст: " + reader_list[3].ToString();
                MessageBox.Show(s);
            }
            //Закрываем ридер
            reader_list.Close();
            //Закрываем соединение
            conn_db.Close();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            //Получаем из ComboBox звыбранное значение, которое является ID студента
            int id_selected_user = Convert.ToInt32(comboBox1.SelectedValue);
            //Посылаем ID студента в метод вывода информациии
            GetList(id_selected_user);
        }
    }
}
