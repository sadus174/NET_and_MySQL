using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NET_and_MySQL
{
    //Класс для передачи значений между формами
    static class SomeClass
    {
        //Статичное поле, которое хранит значение для передачи его между формами
        public static string variable_class;
        //Статичное поле, которое хранит значения ID добавленного клиента на Form15-addClient
        public static string new_inserted_id;
        //Статичное поле, которое хранит значение ID добаленного заказа
        public static string new_inserted_mainOrder_id;

    }

    static class Auth
    {
        //Статичное поле, которое хранит значение статуса авторизации
        public static bool auth;
        //Статичное поле, которое хранит значения ID пользователя
        public static string auth_id;
        //Статичное поле, которое хранит значения ФИО пользователя
        public static string auth_fio;
    }

    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Menu());
        }
    }
}
