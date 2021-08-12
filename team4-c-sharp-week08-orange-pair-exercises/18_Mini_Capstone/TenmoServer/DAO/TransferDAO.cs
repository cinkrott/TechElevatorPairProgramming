using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using TenmoServer.Models;

namespace TenmoServer.DAO
{
    public class TransferDAO : ITransferDAO
    {
        private readonly string connectionString;       

        
        public string getNameToTransferSql = "SELECT transfer_status_desc, transfer_type_desc, transfer_id, users.username AS users_username_name, " +
            "account_from, account_to, amount, users.user_id AS users_users_id FROM transfers  " +
            "JOIN accounts ON accounts.account_id = transfers.account_to JOIN users ON users.user_id = accounts.user_id " +
            "JOIN transfer_statuses ON transfer_statuses.transfer_status_id = transfers.transfer_status_id JOIN transfer_types " +
            "ON transfer_types.transfer_type_id = transfers.transfer_type_id WHERE account_from = (Select DISTINCT accounts.account_id " +
            "FROM users JOIN accounts ON users.user_id = accounts.user_id JOIN transfers ON accounts.account_id = transfers.account_from " +
            "WHERE users.user_id = 3000) OR account_to = (Select DISTINCT accounts.account_id FROM users JOIN accounts  " +
            "ON users.user_id = accounts.user_id JOIN transfers ON accounts.account_id = transfers.account_from WHERE users.user_id = 3000);";

        public string getNameFromTransferSql = "SELECT transfer_status_desc, transfer_type_desc, transfer_id, users.username AS users_username_name, " +
            "account_from, account_to, amount, users.user_id AS users_users_id FROM transfers  " +
            "JOIN accounts ON accounts.account_id = transfers.account_from JOIN users ON users.user_id = accounts.user_id " +
            "JOIN transfer_statuses ON transfer_statuses.transfer_status_id = transfers.transfer_status_id JOIN transfer_types " +
            "ON transfer_types.transfer_type_id = transfers.transfer_type_id WHERE account_from = (Select DISTINCT accounts.account_id " +
            "FROM users JOIN accounts ON users.user_id = accounts.user_id JOIN transfers ON accounts.account_id = transfers.account_from " +
            "WHERE users.user_id = 3000) OR account_to = (Select DISTINCT accounts.account_id FROM users JOIN accounts  " +
            "ON users.user_id = accounts.user_id JOIN transfers ON accounts.account_id = transfers.account_from WHERE users.user_id = 3000);";         


        public TransferDAO(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public List<Transfer> GetTransferFrom(int userId)
        {
            List<Transfer> transfers = new List<Transfer>();
            

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(getNameFromTransferSql, conn);
                    cmd.Parameters.AddWithValue("@userId", userId);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {

                        Transfer transfer = new Transfer();
                        transfer.TransferId = (int)reader["transfer_id"];
                        transfer.AccountFrom = (int)reader["account_from"];
                        transfer.AccountTo = (int)reader["account_to"];
                        transfer.Amount = (decimal)reader["amount"];
                        transfer.Name = (string)reader["users_username_name"];
                        transfer.UserId = (int)reader["users_users_id"];
                        transfer.TransferStatus = (string)reader["transfer_status_desc"];
                        transfer.TransferType = (string)reader["transfer_type_desc"];
                        transfers.Add(transfer);

                    }
                                    
                }
                return transfers; 
            }
            catch (Exception ex)
            {
                throw;
            }            

        }



        public List<Transfer> GetTransferTo(int userId)
        {
            List<Transfer> transfers = new List<Transfer>();           

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(getNameToTransferSql, conn);
                    cmd.Parameters.AddWithValue("@userId", userId);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {

                        Transfer transfer = new Transfer();
                        transfer.TransferId = (int)reader["transfer_id"];
                        transfer.AccountFrom = (int)reader["account_from"];
                        transfer.AccountTo = (int)reader["account_to"];
                        transfer.Amount = (decimal)reader["amount"];
                        transfer.Name = (string)reader["users_username_name"];
                        transfer.UserId = (int)reader["users_users_id"];
                        transfer.TransferStatus = (string)reader["transfer_status_desc"];
                        transfer.TransferType = (string)reader["transfer_type_desc"];

                        transfers.Add(transfer);

                    }
                }
                return transfers;
            }
            catch (Exception ex)
            {
                throw;
            }

            //return transfers;  //returns the same transfer id

        }
    }
}
