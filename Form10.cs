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
    public partial class Form10 : Form
    {              
        //Переменная для ID записи в БД, выбранной в гриде. Пока она не содердит значения, лучше его инициализировать с 0
        //что бы в БД не отправлялся null
        string id_selected_rows="0";

        public Form10()
        {
            InitializeComponent();
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

        //Метод получения ID выделенной строки, для последующего вызова его в нужных методах
        public void GetSelectedIDString()
        {
            //Переменная для индекс выбранной строки в гриде
            string index_selected_rows;
            //Индекс выбранной строки
            index_selected_rows = dataGridView1.SelectedCells[0].RowIndex.ToString();
            //ID конкретной записи в Базе данных, на основании индекса строки
            id_selected_rows = dataGridView1.Rows[Convert.ToInt32(index_selected_rows)].Cells[0].Value.ToString();
            //Указываем ID выделенной строки в метке
            toolStripLabel4.Text = id_selected_rows;
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

        //Метод обновления DataGreed
        public void reload_list()
        {
            //Чистим виртуальную таблицу внутри класса
            ControlData.reload_list();
            //Вызываем метод получения записей, который вновь заполнит таблицу
            ControlData.GetListUsers();
        }
                       
        //Собтия открытия (загрузки формы)
        private void Form10_Load(object sender, EventArgs e)
        {            
            //Указываем, что источником данных ДатаГрида является bindingsource, возвращённый из метода класса 
            dataGridView1.DataSource = ControlData.GetListUsers();         
            //Видимость полей в гриде
            dataGridView1.Columns[0].Visible = true;
            dataGridView1.Columns[1].Visible = true;
            dataGridView1.Columns[2].Visible = true;
            dataGridView1.Columns[3].Visible = true;
            dataGridView1.Columns[4].Visible = true;
            //Ширина полей
            dataGridView1.Columns[0].FillWeight = 15;
            dataGridView1.Columns[1].FillWeight = 40;
            dataGridView1.Columns[2].FillWeight = 15;
            dataGridView1.Columns[3].FillWeight = 15;
            dataGridView1.Columns[4].FillWeight = 15;
            //Режим для полей "Только для чтения"
            dataGridView1.Columns[0].ReadOnly = true;
            dataGridView1.Columns[1].ReadOnly = true;
            dataGridView1.Columns[2].ReadOnly = true;
            dataGridView1.Columns[3].ReadOnly = true;
            dataGridView1.Columns[4].ReadOnly = true;
            //Растягивание полей грида
            dataGridView1.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            //Убираем заголовки строк
            dataGridView1.RowHeadersVisible = false;
            //Показываем заголовки столбцов
            dataGridView1.ColumnHeadersVisible = true;
            //Вызываем метод покраски ДатаГрид
            ChangeColorDGV();
        }

        //Метод изменения цвета строк, в зависимости от значения поля записи в таблице
        private void ChangeColorDGV()
        {
            //Отражаем количество записей в ДатаГриде
            int count_rows = dataGridView1.RowCount - 1;
            toolStripLabel2.Text = (count_rows).ToString();
            //Проходимся по ДатаГриду и красим строки в нужные нам цвета, в зависимости от статуса студента
            for (int i = 0; i < count_rows; i++)
            {

                //статус конкретного студента в Базе данных, на основании индекса строки
                int id_selected_status = Convert.ToInt32(dataGridView1.Rows[i].Cells[4].Value);
                //Логический блок для определения цветности
                if (id_selected_status == 1)
                {
                    //Красим в красный
                    dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.Red;
                }               
                if (id_selected_status == 2)
                {
                    //Красим в зелёный
                    dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.Green;
                }
            }
        }

        //Кнопка обновления 
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            //Метод обновления dataGridView, так как он полностью обновляется, покраски строк не будет. 
            reload_list();
            //Красим опять грид
            ChangeColorDGV();
        }

        //удаление записи из БД
        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            ControlData.DeleteUser(id_selected_rows);
        }

        //Отчислить студента
        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            ControlData.ChangeStateStudent("1", id_selected_rows);
            //Метод обновления dataGridView, так как он полностью обновляется, покраски строк не будет. 
            reload_list();
            //Красим опять грид
            ChangeColorDGV();
        }

        //Зачислить студента 
        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            ControlData.ChangeStateStudent("2", id_selected_rows);
            //Метод обновления dataGridView, так как он полностью обновляется, покраски строк не будет. 
            reload_list();
            //Красим опять грид
            ChangeColorDGV();
        }

        //Удаление записей из контекстного меню
        private void удалитьЗаписьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ControlData.DeleteUser(id_selected_rows);
            //Метод обновления dataGridView, так как он полностью обновляется, покраски строк не будет. 
            reload_list();
            //Красим опять грид
            ChangeColorDGV();
        }

        //Контекстное меню
        private void отчислитьСтудентаToolStripMenuItem_Click(object sender, EventArgs e)
        {            
            ControlData.ChangeStateStudent("1", id_selected_rows);
            //Метод обновления dataGridView, так как он полностью обновляется, покраски строк не будет. 
            reload_list();
            //Красим опять грид
            ChangeColorDGV();
        }

        //Контекстное меню
        private void зачислитьСтудентаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ControlData.ChangeStateStudent("2", id_selected_rows);
            //Метод обновления dataGridView, так как он полностью обновляется, покраски строк не будет. 
            reload_list();
            //Красим опять грид
            ChangeColorDGV();
        }
    }
}
