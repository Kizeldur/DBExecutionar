using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.Tracing;
using System.Linq;
using MySql.Data.MySqlClient;

namespace BDExecution
{
    public class DBExecutioner
    {
        const string connection =
            " Server=mysql60.hostland.ru;    Database=host1323541_vrn05;   Uid=host1323541_itstep;   Pwd=269f43dc;   ";

        MySqlConnection db = new MySqlConnection(connection);

        public void Connect()
        {
            db = new MySqlConnection(connection);
            db.Open();
        }

        public List<User> GiveMeUserList()
        {
            var commandString = "SELECT * table_user";
            var reader = Execute(commandString);
            List<User> users = new List<User>();
            while (reader.NextResult())
            {
                var newUser = new User
                {
                    Id = reader.GetInt32("Id"),
                    Name = reader.GetString("Name"),
                    Surname = reader.GetString("Surname"),
                    Data = reader.GetString("Data")
                };
                
                users.Add(newUser);
            }
            reader.Close();
            return users;
        }

        public List<Account> GiveMeAccountList()
        {
            var commandString = "SELECT * table_account";
            var reader = Execute(commandString);
            
            List<Account> accounts = new List<Account>();
            while (reader.NextResult())
            {
                var newAccount = new Account()
                {
                    Id = reader.GetInt32("Id"),
                    Login = reader.GetString("Login"),
                    Password = reader.GetString("Password"),
                    IsActive = reader.GetBoolean("IsActive")
                };
                
                accounts.Add(newAccount);
            }
            reader.Close();
            return accounts;
        }

        public  List<Role> GiveMeRoleList()
        {
            var commandString = "SELECT * table_role";
            var reader = Execute(commandString);
            
            List<Role> roles = new List<Role>();
            while (reader.NextResult())
            {
                var newRole = new Role()
                {
                    Id = reader.GetInt32("Id"),
                    RoleName = reader.GetString("RoleName")
                };
                
                roles.Add(newRole);
            }
            reader.Close();
            return roles;
        }
        
        public  List<Account_Role> GiveMeAccountRoleList()
        {
            var commandString = "SELECT * table_account_role";
            var reader = Execute(commandString);
            
            List<Account_Role> roles = new List<Account_Role>();
            while (reader.NextResult())
            {
                var newRole = new Account_Role()
                {
                    AccountId = reader.GetInt32("AccountId"),
                    RoleId = reader.GetInt32("RoleId")
                };
                
                roles.Add(newRole);
            }
            reader.Close();
            return roles;
        }

        public MySqlDataReader Execute(string commandString)
        {
            MySqlCommand command = new MySqlCommand(commandString, db);
            var reader = command.ExecuteReader();
            return reader;
        }

        public IEnumerable<(string, string, bool, string)> GiveMeSomeShitAssAccountRoleList()
        {
            var accounts = GiveMeAccountList();
            var roles = GiveMeRoleList();
            var accs_roles = GiveMeAccountRoleList();

            var bind = from acc_role in accs_roles
                join account in accounts on acc_role.AccountId equals account.Id
                join role in roles on acc_role.RoleId equals role.Id
                select (account.Login, account.Password, account.IsActive, role.RoleName);
            return bind;
        }

        public void UpdateDB()
        {
            var commandString = "Update adawdawd";
            
            MySqlCommand command = new MySqlCommand(commandString, db);
            var reader = command.ExecuteReader();
            reader.Close();
            commandString = "Insert adawaad";
            command = new MySqlCommand(commandString, db);
            reader = command.ExecuteReader();
            reader.Close();
        }
        
         public IEnumerable<(string, string, bool, string)> GiveGiveMeSomeShitAssAccountRoleList(List<Account> accounts, List<Role> roles, List<Account_Role>accs_roles )
        {
            //var accounts = GiveMeAccountList();
            //var roles = GiveMeRoleList();
            //var accs_roles = GiveMeAccountRoleList();

            /*var bind = from acc_role in accs_roles
                from account in accounts
                join role in roles on
                where account.Id == acc_role.AccountId
                where role.Id == acc_role.RoleId
                select (account.Login, account.Password, account.IsActive, role.RoleName);*/
            
            var bind2 = from acc_role in accs_roles
                join account in accounts on acc_role.AccountId equals account.Id
                join role in roles on acc_role.RoleId equals role.Id
                select (account.Login, account.Password, account.IsActive, role.RoleName);

            return bind2;
        }
    }
}