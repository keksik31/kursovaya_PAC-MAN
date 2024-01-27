using MySql.Data.MySqlClient;
using System;
using System.Data;

namespace Pac_Man
{
    public class DB
    {
        MySqlConnection connection = new MySqlConnection("server=localhost;port=3306;username=root;password=root;database=mojabaza");

        public void openConnection()
        {
            if (connection.State == System.Data.ConnectionState.Closed)
                connection.Open();
        }

        public void closeConnection()
        {
            if (connection.State == System.Data.ConnectionState.Open)
                connection.Close();
        }

        public MySqlConnection getConnection()
        {
            return connection;
        }

        public void InitializeDatabase()
        {
            openConnection();


            closeConnection();
        }

        public bool RegisterUser(string login, string password, string name, string surname)
        {
            if (isUserExists(login))
            {
                // Пользователь с таким логином уже существует, не выполняем регистрацию
                return false;
            }

            openConnection();

            MySqlCommand command = new MySqlCommand("INSERT INTO `users` (`login`, `pass`, `name`, `surname`) VALUES (@login, @pass, @name, @surname)", getConnection());

            command.Parameters.Add("@login", MySqlDbType.VarChar).Value = login;
            command.Parameters.Add("@pass", MySqlDbType.VarChar).Value = password;
            command.Parameters.Add("@name", MySqlDbType.VarChar).Value = name;
            command.Parameters.Add("@surname", MySqlDbType.VarChar).Value = surname;

            try
            {
                if (command.ExecuteNonQuery() == 1)
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                // Обработка ошибок регистрации
                return false;
            }
            finally
            {
                closeConnection();
            }
        }

        private bool isUserExists(string login)
        {
            openConnection();

            DataTable table = new DataTable();

            MySqlDataAdapter adapter = new MySqlDataAdapter();

            MySqlCommand command = new MySqlCommand("SELECT * FROM `users` WHERE `login` = @uL", getConnection());
            command.Parameters.Add("@uL", MySqlDbType.VarChar).Value = login;

            adapter.SelectCommand = command;
            adapter.Fill(table);

            closeConnection();

            return table.Rows.Count > 0;
        }


        public bool ValidateLogin(string login, string password)
        {
            openConnection();

            DataTable table = new DataTable();

            MySqlDataAdapter adapter = new MySqlDataAdapter();

            MySqlCommand command = new MySqlCommand("SELECT * FROM `users` WHERE `login` = @uL AND `pass` = @uP", getConnection());
            command.Parameters.Add("@uL", MySqlDbType.VarChar).Value = login;
            command.Parameters.Add("@uP", MySqlDbType.VarChar).Value = password;

            adapter.SelectCommand = command;
            adapter.Fill(table);

            closeConnection();

            return table.Rows.Count > 0;
        }


        public bool DeleteUser(string login)
        {
            if (!isUserExists(login))
            {
                // Пользователь не существует, не выполняем удаление
                return false;
            }

            openConnection();

            MySqlCommand command = new MySqlCommand("DELETE FROM `users` WHERE `login` = @uL", getConnection());
            command.Parameters.Add("@uL", MySqlDbType.VarChar).Value = login;

            try
            {
                if (command.ExecuteNonQuery() == 1)
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                // Обработка ошибок удаления
                return false;
            }
            finally
            {
                closeConnection();
            }
        }
    }
}
