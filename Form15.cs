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
    public partial class Form15 : Form
    {
        //Объявляем соединение
        MySqlConnection conn;
        //DataAdapter представляет собой объект Command , получающий данные из источника данных.
        private MySqlDataAdapter MyDA = new MySqlDataAdapter();
        //Объявление BindingSource, основная его задача, это обеспечить унифицированный доступ к источнику данных.
        private BindingSource bSource = new BindingSource();
        //DataSet - расположенное в оперативной памяти представление данных, обеспечивающее согласованную реляционную программную 
        //модель независимо от источника данных.DataSet представляет полный набор данных, включая таблицы, содержащие, упорядочивающие 
        //и ограничивающие данные, а также связи между таблицами.
        private DataSet ds = new DataSet();
        //Представляет одну таблицу данных в памяти.
        private DataTable table = new DataTable();
        //Переменная для ID записи в БД, выбранной в гриде. Пока она не содердит значения, лучше его инициализировать с 0
        //что бы в БД не отправлялся null
        string id_selected_rows = "0";
        //ID выбранного клиента
        string id_selected_clients = "0";
        //Переменная которая хранить имя товара 
        string titleItems_selected_rows = "";
        //Переменная которая хранит стоимость товара
        string priceItems_selected_rows = "";
        //Перемененная отвечающая за понимание, создан ли заказ
        bool issetOrder = false;

        public void GetComboBox1()
        {
            //Формирование списка статусов
            DataTable list_stud_table = new DataTable();
            MySqlCommand list_stud_command = new MySqlCommand();
            //Открываем соединение
            conn.Open();
            //Формируем столбцы для комбобокса списка ЦП
            list_stud_table.Columns.Add(new DataColumn("id_MainCateg", System.Type.GetType("System.Int32")));
            list_stud_table.Columns.Add(new DataColumn("title_MainCateg", System.Type.GetType("System.String")));
            //Настройка видимости полей комбобокса
            comboBox1.DataSource = list_stud_table;
            comboBox1.DisplayMember = "title_MainCateg";
            comboBox1.ValueMember = "id_MainCateg";
            //Формируем строку запроса на отображение списка статусов прав пользователя
            string sql_list_users = "SELECT id_MainCateg, title_MainCateg FROM t_mainCateg";
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
                    rowToAdd["id_MainCateg"] = Convert.ToInt32(list_stud_reader[0]);
                    rowToAdd["title_MainCateg"] = list_stud_reader[1].ToString();
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
        public void GetComboBox2(string id_MainCateg)
        {
            //Формирование списка статусов
            DataTable list_stud_table = new DataTable();
            MySqlCommand list_stud_command = new MySqlCommand();
            //Открываем соединение
            conn.Open();
            //Формируем столбцы для комбобокса списка ЦП
            list_stud_table.Columns.Add(new DataColumn("id_SubCateg", System.Type.GetType("System.Int32")));
            list_stud_table.Columns.Add(new DataColumn("title_SubCateg", System.Type.GetType("System.String")));
            //Настройка видимости полей комбобокса
            comboBox2.DataSource = list_stud_table;
            comboBox2.DisplayMember = "title_SubCateg";
            comboBox2.ValueMember = "id_SubCateg";
            //Формируем строку запроса на отображение списка статусов прав пользователя
            string sql_list_users = $"SELECT id_SubCateg, title_SubCateg FROM t_subCateg WHERE id_MainCateg = {id_MainCateg}";
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
                    rowToAdd["id_SubCateg"] = Convert.ToInt32(list_stud_reader[0]);
                    rowToAdd["title_SubCateg"] = list_stud_reader[1].ToString();
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
            list_stud_table.Columns.Add(new DataColumn("idClient", System.Type.GetType("System.Int32")));
            list_stud_table.Columns.Add(new DataColumn("fioClient", System.Type.GetType("System.String")));
            //Настройка видимости полей комбобокса
            comboBox3.DataSource = list_stud_table;
            comboBox3.DisplayMember = "fioClient";
            comboBox3.ValueMember = "idClient";
            //Формируем строку запроса на отображение списка статусов прав пользователя
            string sql_list_users = "SELECT idClient, fioClient FROM t_client";
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
                    rowToAdd["idClient"] = Convert.ToInt32(list_stud_reader[0]);
                    rowToAdd["fioClient"] = list_stud_reader[1].ToString();
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

        public Form15()
        {
            InitializeComponent();
        }

        private void Form14_Load(object sender, EventArgs e)
        {
            // строка подключения к БД
            string connStr = "server=caseum.ru;port=33333;user=test_user;database=db_test;password=test_pass;";
            // создаём объект для подключения к БД
            conn = new MySqlConnection(connStr);
            //Вызов заполнения первого Combobox
            GetComboBox1();
            //Заполнение ComboBox'a с клиентами
            GetComboBox3();
            comboBox3.Text = "";
            //Установка пустой строки по умолчанию в ComboBox1
            comboBox1.Text = "";
            //Вывод списка всех товаров
            GetFirstListUsers();
            //Видимость полей в гриде
            dataGridView1.Columns[0].Visible = true;
            dataGridView1.Columns[1].Visible = true;
            dataGridView1.Columns[2].Visible = true;
            dataGridView1.Columns[3].Visible = false;
            //Ширина полей
            dataGridView1.Columns[0].FillWeight = 10;
            dataGridView1.Columns[1].FillWeight = 70;
            dataGridView1.Columns[2].FillWeight = 20;
            //Режим для полей "Только для чтения"
            dataGridView1.Columns[0].ReadOnly = true;
            dataGridView1.Columns[1].ReadOnly = true;
            dataGridView1.Columns[2].ReadOnly = true;
            //Растягивание полей грида
            dataGridView1.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            //Убираем заголовки строк
            dataGridView1.RowHeadersVisible = false;
            //Показываем заголовки столбцов
            dataGridView1.ColumnHeadersVisible = true;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Включение ComboBox2
            comboBox2.Enabled = true;
            //Заполнение Combobox2 теми подкатегориями, которые относятся к выбранной категории
            GetComboBox2(comboBox1.SelectedValue.ToString());
            //Установка пустой строки по умолчанию в ComboBox2
            comboBox2.Text = "";
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Чистим виртуальную таблицу
            table.Clear();
            //Вызываем метод наполнения ДатаГрид только теми объектами, которые подходят по условию
            GetListUsers(comboBox2.SelectedValue.ToString());
            //Видимость полей в гриде
            dataGridView1.Columns[0].Visible = true;
            dataGridView1.Columns[1].Visible = true;
            dataGridView1.Columns[2].Visible = true;
            dataGridView1.Columns[3].Visible = false;
            //Ширина полей
            dataGridView1.Columns[0].FillWeight = 10;
            dataGridView1.Columns[1].FillWeight = 70;
            dataGridView1.Columns[2].FillWeight = 20;
            //Режим для полей "Только для чтения"
            dataGridView1.Columns[0].ReadOnly = true;
            dataGridView1.Columns[1].ReadOnly = true;
            dataGridView1.Columns[2].ReadOnly = true;
            //Растягивание полей грида
            dataGridView1.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            //Убираем заголовки строк
            dataGridView1.RowHeadersVisible = false;
            //Показываем заголовки столбцов
            dataGridView1.ColumnHeadersVisible = true;
        }
        public void GetFirstListUsers()
        {
            //Запрос для вывода строк в БД
            string commandStr = $"SELECT id_items AS Код, title_items AS 'Название товара', prrice_items AS 'Цена товара', imgUrl FROM t_items";
            //Открываем соединение
            conn.Open();
            //Объявляем команду, которая выполнить запрос в соединении conn
            MyDA.SelectCommand = new MySqlCommand(commandStr, conn);
            //Заполняем таблицу записями из БД
            MyDA.Fill(table);
            //Указываем, что источником данных в bindingsource является заполненная выше таблица
            bSource.DataSource = table;
            //Указываем, что источником данных ДатаГрида является bindingsource 
            dataGridView1.DataSource = bSource;
            //Закрываем соединение
            conn.Close();

        }
        //Метод наполнения виртуальной таблицы и присвоение её к датагриду
        public void GetListUsers(string idSubCateg)
        {
            //Запрос для вывода строк в БД
            string commandStr = $"SELECT id_items AS Код, title_items AS 'Название товара', prrice_items AS 'Цена товара', imgUrl FROM t_items WHERE id_SubCateg = {idSubCateg}";
            //Открываем соединение
            conn.Open();
            //Объявляем команду, которая выполнить запрос в соединении conn
            MyDA.SelectCommand = new MySqlCommand(commandStr, conn);
            //Заполняем таблицу записями из БД
            MyDA.Fill(table);
            //Указываем, что источником данных в bindingsource является заполненная выше таблица
            bSource.DataSource = table;
            //Указываем, что источником данных ДатаГрида является bindingsource 
            dataGridView1.DataSource = bSource;
            //Закрываем соединение
            conn.Close();
       
        }
        //Метод обновления DataGreed
        public void reload_list()
        {
            //Чистим виртуальную таблицу
            table.Clear();
            //Вызываем метод получения записей, который вновь заполнит таблицу
            GetListUsers(comboBox2.SelectedValue.ToString());
        }
        //Метод получения ID выделенной строки для последующего вызова его в нужных методах
        public void GetSelectedIDString()
        {
            //Переменная для индекс выбранной строки в гриде
            string index_selected_rows;
            //Индекс выбранной строки
            index_selected_rows = dataGridView1.SelectedCells[0].RowIndex.ToString();
            //ID конкретной записи в Базе данных, на основании индекса строки
            id_selected_rows = dataGridView1.Rows[Convert.ToInt32(index_selected_rows)].Cells[0].Value.ToString();
            //название товара конкретной записи в Базе данных, на основании индекса строки
            titleItems_selected_rows = dataGridView1.Rows[Convert.ToInt32(index_selected_rows)].Cells[1].Value.ToString();
            //стоимость товара конкретной записи в Базе данных, на основании индекса строки
            priceItems_selected_rows = dataGridView1.Rows[Convert.ToInt32(index_selected_rows)].Cells[2].Value.ToString();

        }
        //Выделение всей строки по ПКМ
        private void dataGridView1_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (!e.RowIndex.Equals(-1) && !e.ColumnIndex.Equals(-1) && e.Button.Equals(MouseButtons.Right))
            {
                dataGridView1.CurrentCell = dataGridView1[e.ColumnIndex, e.RowIndex];
                //dataGridView1.CurrentRow.Selected = true;
                dataGridView1.CurrentCell.Selected = true;
                //Метод получения ID выделенной строки в глобальную переменную
                GetSelectedIDString();
            }
        }

        //Выделение всей строки по ЛКМ
        private void dataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            //Магические строки
            dataGridView1.CurrentCell = dataGridView1[e.ColumnIndex, e.RowIndex];
            dataGridView1.CurrentRow.Selected = true;
            //Метод получения ID выделенной строки в глобальную переменную
            GetSelectedIDString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //Объявляем форму
            Form15_addClient formNewClient = new Form15_addClient();
            //Открываем форму в режиме диалога
            formNewClient.ShowDialog();
            //Вызываем обновление данных в комбобоксе с клиентами
            GetComboBox3();
            //устанавливаем в комбобоксе строки со значением добавленного только что клиента
            comboBox3.SelectedValue = Convert.ToInt32(SomeClass.new_inserted_id);
            toolStripStatusLabel1.Text = $"Добавлен клиент в БД с ID {SomeClass.new_inserted_id}";
        }
        
        //Метод добавления заказа в главную таблицу заказов
        public void InsertOrderMain()
        {
            //Определяем значение переменных для записи в БД
            string dataOrder = DateTime.Now.ToString();
            string idClient = id_selected_clients;
            string summOrder = "0";

            //Формируем запрос на вставку с возвратом последного вставленного ID
            string sql_update_current_stud = $"INSERT INTO t_order (dataOrder, idClient, sumOrder) " +
                                              $"VALUES ('{dataOrder}', '{idClient}', '{summOrder}'); " +
                                              $"SELECT idOrder FROM t_order WHERE (idOrder = LAST_INSERT_ID());";
            // устанавливаем соединение с БД
            conn.Open();
            // объект для выполнения SQL-запроса
            MySqlCommand command = new MySqlCommand(sql_update_current_stud, conn);
            // выполняем запрос
            string new_inserted_mainOrder_id = command.ExecuteScalar().ToString();
            //Записываем возвращённый последний добавленный ID заказа в глобальную переменную
            SomeClass.new_inserted_mainOrder_id = new_inserted_mainOrder_id;
            //Пишем инфу в статус бар
            toolStripStatusLabel1.Text = $"Добавлен заказ в БД с ID {SomeClass.new_inserted_mainOrder_id}";
            // закрываем подключение к БД
            conn.Close();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            //Устанавливаем в переменную ИД выбранного клиента для формирования заказа
            id_selected_clients = comboBox3.SelectedValue.ToString();
            //Создание заказа с запоминанием ID этого самого заказа, она нужна для формирования позиций в составе зказа
            InsertOrderMain();
            //Изменение переменной отчечающей за понимание, создан ли заказ
            issetOrder = true;

        }

        //Добавление позиций во временную корзину
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //Индекс добавленной строки
            int rowNumber = dataGridView2.Rows.Add();
            //Распихивание данных по полям грида
            dataGridView2.Rows[rowNumber].Cells[0].Value = id_selected_rows;
            dataGridView2.Rows[rowNumber].Cells[1].Value = titleItems_selected_rows;
            dataGridView2.Rows[rowNumber].Cells[2].Value = "1";
            dataGridView2.Rows[rowNumber].Cells[3].Value = priceItems_selected_rows;
        }

        //Кнопка для записи в БД информации о позициях заказа и обновление итоговой суммы заказа
        private void button3_Click(object sender, EventArgs e)
        {
            if (issetOrder)
            {
                //переменная хранящая итоговую сумму заказа
                double sumOrder = 0;
                //Определяем количество товаров в DataGridView2
                int countPosition = dataGridView2.Rows.Count;
                //Определяем цикл для добавление позиций заказа в таблицу
                conn.Open();
                for (int i = 0; i < countPosition; i++)
                {
                    string idItems = dataGridView2.Rows[i].Cells[0].Value.ToString();
                    string countItems = dataGridView2.Rows[i].Cells[2].Value.ToString();
                    double priceItems = Convert.ToDouble(dataGridView2.Rows[i].Cells[3].Value);

                    string idOrder = SomeClass.new_inserted_mainOrder_id;
                    //Подсчёт итоговой суммы
                    sumOrder += Convert.ToInt32(countItems) * priceItems;
                    //Формирование запросов на добавение позиций заказа
                    string query = $"INSERT INTO t_positionOrders (idItems, countItems, idMainOrders) " +
                        $"VALUES ('{idItems}', '{countItems}', {idOrder})";
                    // объект для выполнения SQL-запроса
                    MySqlCommand command = new MySqlCommand(query, conn);
                    // выполняем запрос
                    command.ExecuteNonQuery();
                    // закрываем подключение к БД
                }
                conn.Close();

                //Обновление итоговой суммы заказа
                toolStripStatusLabel1.Text = $"Итоговая сумма заказа №{SomeClass.new_inserted_mainOrder_id} составляет {sumOrder}";
                //Открываем подключение к БД
                conn.Open();
                // запрос обновления данных
                string query2 = $"UPDATE t_order SET sumOrder='{sumOrder}' WHERE (idOrder='{SomeClass.new_inserted_mainOrder_id}')";
                // объект для выполнения SQL-запроса
                MySqlCommand comman1 = new MySqlCommand(query2, conn);
                // выполняем запрос
                comman1.ExecuteNonQuery();
                // закрываем подключение к БД
                conn.Close();
            }
            else
            {
                MessageBox.Show("Заказ не создан. Создайте заказ!");
            }
        }
    }
}
