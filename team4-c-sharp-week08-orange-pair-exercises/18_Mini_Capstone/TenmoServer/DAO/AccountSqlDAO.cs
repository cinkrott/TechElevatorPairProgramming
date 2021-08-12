using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using TenmoServer.Models;

namespace TenmoServer.DAO
{
    public class AccountSqlDAO : IAccountDAO
    {
        private readonly string connectionString;

        private string accountSql = "Select account_id, balance FROM accounts WHERE user_id = @user_id;";
        private Accounts account = new Accounts();
        private User user = new User();
        private string addBalanceSql = "UPDATE accounts SET balance = balance + @amountToAdd WHERE user_id = @userId";
        private string subtractSql = "UPDATE accounts SET balance = balance - @amountToSubtract WHERE user_id = @userId";
        private string accountFromIdSql = "SELECT account_id FROM accounts WHERE user_id = @userId";
        private string logTransferSql = "INSERT INTO transfers (transfer_type_id, transfer_status_id, account_from, account_to, amount) " +
                                         "VALUES(1001, 2001, @accountFrom, @accountTo, @amount)";
       


        public AccountSqlDAO(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public decimal GetBalance(int userId)
        {
            decimal balance;

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(accountSql, conn);

                    cmd.Parameters.AddWithValue("@user_id", userId);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        
                        account.Account_ID = (int)reader["account_id"];
                        account.Balance = (decimal)reader["balance"];

                    }

                    balance = account.Balance;
                }

            }
            catch (SqlException ex)
            {
                throw;
            }
            return balance;
        }

        public bool AddBalance(int userId, decimal amountToAdd)
        {

            bool result = false;
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(addBalanceSql, conn);

                    cmd.Parameters.AddWithValue("@userId", userId);
                    cmd.Parameters.AddWithValue("@amountToAdd", amountToAdd);
                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        result = true;
                    }
                    else
                    {
                        return result;
                    }
                }

            }
            catch (SqlException ex)
            {
                throw;
            }
            return result;
        }


        public bool Subtract(int userId, decimal amountToSubtract)
        {

            bool result = false;
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(subtractSql, conn);

                    cmd.Parameters.AddWithValue("@userId", userId);
                    cmd.Parameters.AddWithValue("@amountToSubtract", amountToSubtract);
                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        result = true;
                    }
                    else
                    {
                        return result;
                    }
                }

            }
            catch (SqlException ex)
            {
                throw;
            }
            return result;
        }

        public int GetAccountFromId(int userId)
        {
            int result = 0;
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(accountFromIdSql, conn);

                    cmd.Parameters.AddWithValue("@userId", userId);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        account.Account_ID = (int)reader["account_id"];
                    }

                    result = account.Account_ID;
                }

            }
            catch (SqlException ex)
            {
                throw;
            }

            return result;
        }

       
        public bool LogTransfer(int senderAccount, int receiverAccount, decimal amount)
        {
            bool result = false;
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(logTransferSql, conn);

                    cmd.Parameters.AddWithValue("@accountFrom", senderAccount);
                    cmd.Parameters.AddWithValue("@accountTo", receiverAccount);
                    cmd.Parameters.AddWithValue("@amount", amount);
                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        result = true;
                    }
                    else
                    {
                        return result;
                    }
                }

            }
            catch (SqlException ex)
            {
                return result;
            }
            return result;
        }
    }


}

