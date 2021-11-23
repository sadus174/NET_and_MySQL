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
    public partial class Form11_edit : Form
    {
        //Объявляем объект соединения глобально
        MySqlConnection conn;
        public Form11_edit()
        {
            InitializeComponent();
        }

        public void SelectData ()
        {
            //Открываем соединение
            conn.Open();
            //Меняем на форме название, с указанием того студента, которого хотим изменить
            this.Text = $"Меняем пользователя ID: {ControlData.ID_STUD}";
            //Объявляем запрос на вывод данных из таблицы в поля
            string sql_select_current_stud = $"SELECT t_stud.id, t_stud.fio, t_stud.age, t_stud.theme_kurs, t_stud.id_group, t_stud.id_prep, t_stud.id_state FROM t_stud WHERE id = {ControlData.ID_STUD}";
            // объект для выполнения SQL-запроса
            MySqlCommand command = new MySqlCommand(sql_select_current_stud, conn);
            // объект для чтения ответа сервера
            MySqlDataReader reader = command.ExecuteReader();
            // читаем результат
            while (reader.Read())
            {
                // элементы массива [] - это значения столбцов из запроса SELECT
                textBox1.Text = reader[1].ToString();
                textBox2.Text = reader[2].ToString();
                textBox3.Text = reader[3].ToString();
                comboBox1.SelectedValue = reader[6].ToString();
                comboBox2.SelectedValue = reader[5].ToString();
                comboBox3.SelectedValue = reader[4].ToString();

            }
            reader.Close(); // закрываем reader
            // закрываем соединение с БД
            conn.Close();
        }

        public void GetComboBox1()
        {
            //Формирование списка статусов
            DataTable list_stud_table = new DataTable();
            MySqlCommand list_stud_command = new MySqlCommand();
            //Открываем соединение
            conn.Open();
            //Формируем столбцы для комбобокса списка ЦП
            list_stud_table.Columns.Add(new DataColumn("id_state", System.Type.GetType("System.Int32")));
            list_stud_table.Columns.Add(new DataColumn("title_state", System.Type.GetType("System.String")));
            //Настройка видимости полей комбобокса
            comboBox1.DataSource = list_stud_table;
            comboBox1.DisplayMember = "title_state";
            comboBox1.ValueMember = "id_state";
            //Формируем строку запроса на отображение списка статусов прав пользователя
            string sql_list_users = "SELECT id_state, title_state FROM t_state";
            list_stud_command.CommandText = sql_list_users;
            list_stud_command.Connection = conn;
            //Формирование списка ЦП для combobox'a
            MySqlDataReader list_stud_reader;
            try
            {
                //Инициализируем ридер
                list_stud_reader = list_stud_command.ExecuteReader();
                while (list_stud_reader.Read())
                {
                    DataRow rowToAdd = list_stud_table.NewRow();
                    rowToAdd["id_state"] = Convert.ToInt32(list_stud_reader[0]);
                    rowToAdd["title_state"] = list_stud_reader[1].ToString();
                    list_stud_table.Rows.Add(rowToAdd);
                }
                list_stud_reader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка чтения списка ЦП \n\n" + ex, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }
            finally
            {
                conn.Close();
            }
        }
        public void GetComboBox2()
        {
            //Формирование списка статусов
            DataTable list_stud_table = new DataTable();
            MySqlCommand list_stud_command = new MySqlCommand();
            //Открываем соединение
            conn.Open();
            //Формируем столбцы для комбобокса списка ЦП
            list_stud_table.Columns.Add(new DataColumn("id", System.Type.GetType("System.Int32")));
            list_stud_table.Columns.Add(new DataColumn("fio_prep", System.Type.GetType("System.String")));
            //Настройка видимости полей комбобокса
            comboBox2.DataSource = list_stud_table;
            comboBox2.DisplayMember = "fio_prep";
            comboBox2.ValueMember = "id";
            //Формируем строку запроса на отображение списка статусов прав пользователя
            string sql_list_users = "SELECT id, fio_prep FROM t_prep";
            list_stud_command.CommandText = sql_list_users;
            list_stud_command.Connection = conn;
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
                    rowToAdd["fio_prep"] = list_stud_reader[1].ToString();
                    list_stud_table.Rows.Add(rowToAdd);
                }
                list_stud_reader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка чтения списка ЦП \n\n" + ex, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }
            finally
            {
                conn.Close();
            }
        }
        public void GetComboBox3()
        {
            //Формирование списка статусов
            DataTable list_stud_table = new DataTable();
            MySqlCommand list_stud_command = new MySqlCommand();
            //Открываем соединение
            conn.Open();
            //Формируем столбцы для комбобокса списка ЦП
            list_stud_table.Columns.Add(new DataColumn("id_group", System.Type.GetType("System.Int32")));
            list_stud_table.Columns.Add(new DataColumn("title_group", System.Type.GetType("System.String")));
            //Настройка видимости полей комбобокса
            comboBox3.DataSource = list_stud_table;
            comboBox3.DisplayMember = "title_group";
            comboBox3.ValueMember = "id_group";
            //Формируем строку запроса на отображение списка статусов прав пользователя
            string sql_list_users = "SELECT id_group, title_group FROM t_group";
            list_stud_command.CommandText = sql_list_users;
            list_stud_command.Connection = conn;
            //Формирование списка ЦП для combobox'a
            MySqlDataReader list_stud_reader;
            try
            {
                //Инициализируем ридер
                list_stud_reader = list_stud_command.ExecuteReader();
                while (list_stud_reader.Read())
                {
                    DataRow rowToAdd = list_stud_table.NewRow();
                    rowToAdd["id_group"] = Convert.ToInt32(list_stud_reader[0]);
                    rowToAdd["title_group"] = list_stud_reader[1].ToString();
                    list_stud_table.Rows.Add(rowToAdd);
                }
                list_stud_reader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка чтения списка ЦП \n\n" + ex, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }
            finally
            {
                conn.Close();
            }
        }
        private void Form11_edit_Load(object sender, EventArgs e)
        {
            // строка подключения к БД
            string connStr = "server=caseum.ru;port=33333;user=test_user;database=db_test;password=test_pass;";
            // создаём объект для подключения к БД
            conn = new MySqlConnection(connStr);
            //Вызываем методы наполнения ComboBox
            GetComboBox1();
            GetComboBox2();
            GetComboBox3();
            //Вызываем метод установления значений полей
            SelectData();



        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Определяем значение переменных для записи в БД
            string n_fio = textBox1.Text;
            string n_age = textBox2.Text;
            string n_theme = textBox3.Text;
            string n_state = comboBox1.SelectedValue.ToString();
            string n_prep = comboBox2.SelectedValue.ToString();
            string n_group = comboBox3.SelectedValue.ToString();
            //Формируем запрос на изменение
            string sql_update_current_stud = $"UPDATE t_stud SET fio='{n_fio}', age='{n_age}', theme_kurs='{n_theme}', id_group='{n_group}', id_prep='{n_prep}', id_state='{n_state}' " +
                $"WHERE (id='{ControlData.ID_STUD}')";
            // устанавливаем соединение с БД
            conn.Open();
            // объект для выполнения SQL-запроса
            MySqlCommand command = new MySqlCommand(sql_update_current_stud, conn);
            // выполняем запрос
            command.ExecuteNonQuery();
            // закрываем подключение к БД
            conn.Close();
            //Закрываем форму
            this.Close();
        }
    }
}
