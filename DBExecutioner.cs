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
		
		public Account  GiveMeSomeShitAssAccount(int Id){
			var commandString = $"SELECT * table_account WHERE ID = {Id.ToString()}";
            var reader = Execute(commandString);
            
            var account = new Account
            {
                Id = reader.GetInt32("Id"),
                Login = reader.GetString("Login"),
                Password = reader.GetString("Password"),
                IsActive = reader.GetBoolean("IsActive")
            };
                
            reader.Close();
            return account;
		}
		
		public bool CheckTheAccountUpdate( SomeTypeToBeWritten bind){
			var account = GiveMeSomeShitAssAccount(bind.accountId);
			
			if(!account){
				throw new Exception();
			}
			if(bind.accountId == account.Id && bind.login == account.Login && bind.password == account.Password && bind.isActive == account.IsActive){
				reader.Close();
				return true;
			}	
                
            reader.Close();
            return false;
		}

        public MySqlDataReader Execute(string commandString)
        {
            MySqlCommand command = new MySqlCommand(commandString, db);
            var reader = command.ExecuteReader();
            return reader;
        }

        public IEnumerable<(int accountid, int roleId, string login,  string password, bool isActive, string roleName)> GiveMeSomeShitAssAccountRoleList()
        {
            var accounts = GiveMeAccountList();
            var roles = GiveMeRoleList();
            var accs_roles = GiveMeAccountRoleList();

            var bind = from acc_role in accs_roles
                join account in accounts on acc_role.AccountId equals account.Id
                join role in roles on acc_role.RoleId equals role.Id
                select (account.Id, role.Id, account.Login,  account.Password, account.IsActive, role.RoleName);
            return bind;
        }

        public void UpdateDB(IEnumerable<(int accountid, int roleId, string login,  string password, bool isActive, string roleName)> bind)
        {
			var commandString = "Update adawdawd";
			
			 foreach(var obj in bind){
				 try{
					if(CheckTheAccountUpdateUpdate(obj)){
						command = $"UPDATE table_account SET Login == {obj.login}, Password = {obj.password}, IsActive = {obj.isActive} Where Id = {obj.isActive}";
						MySqlCommand command = new MySqlCommand(commandString, db);
						command.ExecuteNonQuery();
					}
				 }
				 catch(new Exception()){
					commandString = "INSERT INTO table_account SET column = 1, id=1 ON DUPLICATE KEY UPDATE column = column + 1";
					command = new MySqlCommand(commandString, db);
					
				 }
				 command.ExecuteNonQuery();
			 }	
        }
        
         public IEnumerable<(string, int, string, bool, string, int)> GiveGiveMeSomeShitAssAccountRoleList(List<Account> accounts, List<Role> roles, List<Account_Role>accs_roles )
        {
            
            
            IEnumerable<(int accountid, int roleId, string login,  string password, bool isActive, string roleName) bind2 = from acc_role in accs_roles
                join account in accounts on acc_role.AccountId equals account.Id
                join role in roles on acc_role.RoleId equals role.Id
                select (account.Id, role.Id, account.Login,  account.Password, account.IsActive, role.RoleName);

            return bind2;
        }
    }
}