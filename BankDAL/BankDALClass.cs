using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankException;
using BankEntity;
using System.Data.SqlClient;
using System.Data;


namespace BankDAL
{
    public class BankDALClass
    {
        public void Create_New_User(Coustomer c)
        {
            SqlConnection cn = new SqlConnection("Data Source=DESKTOP-5HFRN07\\SQLEXPRESS;Initial Catalog=Bank;Integrated Security=True");
            string sql = $"insert into Customer_Info(AccNumber,Name,MoblieNumber,CRN,IBPassword,BranchName,IFSC,Status) values ({c.AccNumber},'{c.Name}',{c.MoblieNumber},{c.CRN},'{c.IBPassword}','{c.BranchName}','{c.IFSC}','{c.Status}') ";
            SqlCommand cmd = new SqlCommand(sql, cn);
            cn.Open();
            int i = cmd.ExecuteNonQuery();
            cn.Close();
            cn.Dispose();
        }
        public string valdate_User(long AccountNo)
        {
            string rtn = "";
            SqlConnection cn = new SqlConnection("Data Source=DESKTOP-5HFRN07\\SQLEXPRESS;Initial Catalog=Bank;Integrated Security=True");
            string sql = $"select Status from Customer_Info where AccNumber = {AccountNo}";
            SqlCommand cmd = new SqlCommand(sql, cn);
            cn.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                dr.Read();
                rtn = dr[0].ToString();
            }
            cn.Close();
            cn.Dispose();

            return rtn;


        }
        public void User_Reg(Coustomer c)
        {
            string rtn = "";
            SqlConnection cn = new SqlConnection("Data Source=DESKTOP-5HFRN07\\SQLEXPRESS;Initial Catalog=Bank;Integrated Security=True");
            string sql = $"update Customer_Info set IBPassword= '{c.IBPassword}',TxnPassword='{c.TxnPassword}',Address='{c.Address}',EmailID='{c.EmailID}',DOB='{c.DOB}',Balance={c.Balance},Status='Active'  where AccNumber ={c.AccNumber} ";
            SqlCommand cmd = new SqlCommand(sql, cn);
            cn.Open();
            int i = cmd.ExecuteNonQuery();


            cn.Close();
            cn.Dispose();
        }
        public bool Verify_Password(long accno, string Ibpass)
        {
            string rtn = "";
            SqlConnection cn = new SqlConnection("Data Source=DESKTOP-5HFRN07\\SQLEXPRESS;Initial Catalog=Bank;Integrated Security=True");
            string sql = $"select IBPassword from Customer_Info where AccNumber = {accno}";
            SqlCommand cmd = new SqlCommand(sql, cn);
            cn.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                dr.Read();
                rtn = dr[0].ToString();
            }
            if (rtn == Ibpass)
            {
                return true;
            }
            else
            {
                return false;
            }
            cn.Close();
            cn.Dispose();



        }
        public void Fund_Transfer(transations t)
        {
            SqlConnection cn = new SqlConnection("Data Source=DESKTOP-5HFRN07\\SQLEXPRESS;Initial Catalog=Bank;Integrated Security=True");
            string sql = $"insert into Transactions values({t.Sender},{t.Receiver},'{t.Date}',{t.Amount})";
            SqlCommand cmd = new SqlCommand(sql, cn);
            cn.Open();
            //SqlTransaction tran;
            //tran = cn.BeginTransaction();
            try
            {

                int i = cmd.ExecuteNonQuery();


                sql = $"update Customer_Info set Balance =Balance-{t.Amount} where AccNumber = {t.Sender}";
                cmd = new SqlCommand(sql, cn);
                cmd.ExecuteNonQuery();

                sql = $"update Customer_Info set Balance =Balance+{t.Amount} where AccNumber = {t.Receiver}";
                cmd = new SqlCommand(sql, cn);
                cmd.ExecuteNonQuery();


                cn.Close();
                cn.Dispose();
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
                //tran.Rollback();
            }
            finally
            {
                cn.Close();
                cn.Dispose();
            }
        }

    
        public List<transations> View_Stament(long AccountNo)
        {
            SqlConnection cn = new SqlConnection("Data Source=DESKTOP-5HFRN07\\SQLEXPRESS;Initial Catalog=Bank;Integrated Security=True");
            string sql = $"select * from Transactions where Sender={AccountNo} or Receiver={AccountNo}";
            SqlCommand cmd = new SqlCommand(sql, cn);
            cn.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            List<transations> t_list = new List<transations>();
            

            while (dr.Read())
            {
                transations t = new transations();

                t.Sender = (long)dr[0];
                    t.Receiver = (long)dr[1];
                    t.Date = dr[2].ToString();
                    t.Amount = Convert.ToInt64(dr[3]);
                    t_list.Add(t);
               
            }

            
            cn.Close();
            cn.Dispose();
            return t_list;
        }
        public void Update_users(Coustomer c)
        {
            string rtn = "";
            SqlConnection cn = new SqlConnection("Data Source=DESKTOP-5HFRN07\\SQLEXPRESS;Initial Catalog=Bank;Integrated Security=True");
            string sql = $"update Customer_Info set IBPassword= '{c.IBPassword}',Address='{c.Address}',EmailID='{c.EmailID}',MoblieNumber={c.MoblieNumber} where AccNumber ={c.AccNumber} ";
            SqlCommand cmd = new SqlCommand(sql, cn);
            cn.Open();
            int i = cmd.ExecuteNonQuery();


            cn.Close();
            cn.Dispose();
        }
        public void Withdraw(long AccountNo, long amount)
        {
            string rtn = "";
            SqlConnection cn = new SqlConnection("Data Source=DESKTOP-5HFRN07\\SQLEXPRESS;Initial Catalog=Bank;Integrated Security=True");
            string sql = $"update Customer_Info set Balance =Balance-{amount} where AccNumber = {AccountNo}";
            SqlCommand cmd = new SqlCommand(sql, cn);
            cn.Open();
            int i = cmd.ExecuteNonQuery();
            sql = $"insert into Transactions values('65001',{AccountNo},'{DateTime.Now.ToString("MM/dd/yyyy hh:mm tt")}',{amount})";
            cmd = new SqlCommand(sql, cn);
            cmd.ExecuteNonQuery();
            cn.Close();
            cn.Dispose();


        }
        public void Dipsoit(long AccountNo, long amount)
        {
            string rtn = "";
            SqlConnection cn = new SqlConnection("Data Source=DESKTOP-5HFRN07\\SQLEXPRESS;Initial Catalog=Bank;Integrated Security=True");
            string sql = $"update Customer_Info set Balance =Balance+{amount} where AccNumber = {AccountNo}";
            SqlCommand cmd = new SqlCommand(sql, cn);
            cn.Open();
            int i = cmd.ExecuteNonQuery();
            sql = $"insert into Transactions values({AccountNo},'65001','{DateTime.Now.ToString("MM/dd/yyyy hh:mm tt")}',{amount})";
            cmd = new SqlCommand(sql, cn);
            cmd.ExecuteNonQuery();
            cn.Close();
            cn.Dispose();

        }
        public void Freeze_Account(long AccountNo)
        {
            string rtn = "";
            SqlConnection cn = new SqlConnection("Data Source=DESKTOP-5HFRN07\\SQLEXPRESS;Initial Catalog=Bank;Integrated Security=True");
            string sql = $"update Customer_Info set Status = 'Freeze'  where AccNumber = {AccountNo}";
            SqlCommand cmd = new SqlCommand(sql, cn);
            cn.Open();
            int i = cmd.ExecuteNonQuery();

            cn.Close();
            cn.Dispose();
        }
        public void Beneficiary(Beneficiary b)
        {
            SqlConnection cn = new SqlConnection("Data Source=DESKTOP-5HFRN07\\SQLEXPRESS;Initial Catalog=Bank;Integrated Security=True");
            SqlCommand cmd = new SqlCommand("insert_Beneficiary", cn);
            cmd.Parameters.AddWithValue("@Holder_Account_Number", b.Holder_Account_Number);
            cmd.Parameters.AddWithValue("@Payee_Account_Number", b.Payee_Account_Number);
            cmd.Parameters.AddWithValue("@Nickname", b.Nickname);
            cmd.Parameters.AddWithValue("@Branch_Name", b.Branch_Name);
            cmd.Parameters.AddWithValue("@IFSC", b.IFSC);
            cmd.CommandType = CommandType.StoredProcedure;

            cn.Open();
            int i = cmd.ExecuteNonQuery();
            cn.Close();
            cn.Dispose();

        }
        public bool Check_Beneficiary(long h, long p)
        {
            string rtn = "";
            SqlConnection cn = new SqlConnection("Data Source=DESKTOP-5HFRN07\\SQLEXPRESS;Initial Catalog=Bank;Integrated Security=True");
            string sql = $"select Payee_Account_Number from Beneficiary where Holder_Account_Number = {h} and Payee_Account_Number ={p}";
            SqlCommand cmd = new SqlCommand(sql, cn);
            cn.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            long acc = 0;
            if (dr.HasRows)
            {
                dr.Read();
                acc = (long)dr[0];
            }
            cn.Close();
            cn.Dispose();
            if (p == acc)
            {
                return true;
            }
            else
            {
                return false;
            }

        }
        public void Edit_Beneficiary(Beneficiary b)
        {
            SqlConnection cn = new SqlConnection("Data Source=DESKTOP-5HFRN07\\SQLEXPRESS;Initial Catalog=Bank;Integrated Security=True");
            SqlCommand cmd = new SqlCommand("update_Beneficiary", cn);
            cmd.Parameters.AddWithValue("@Holder_Account_Number", b.Holder_Account_Number);
            cmd.Parameters.AddWithValue("@Payee_Account_Number", b.Payee_Account_Number);
            cmd.Parameters.AddWithValue("@Nickname", b.Nickname);
            cmd.Parameters.AddWithValue("@Branch_Name", b.Branch_Name);
            cmd.Parameters.AddWithValue("@IFSC", b.IFSC);
            cmd.CommandType = CommandType.StoredProcedure;

            cn.Open();
            int i = cmd.ExecuteNonQuery();
            cn.Close();
            cn.Dispose();
        }
        public List<Beneficiary> Show_all_Beneficiary(long acc)
        {
            SqlConnection cn = new SqlConnection("Data Source=DESKTOP-5HFRN07\\SQLEXPRESS;Initial Catalog=Bank;Integrated Security=True");
            SqlCommand cmd = new SqlCommand("select * from[dbo].[showall_Beneficiary](@Holder_Account_Number)", cn);
            cmd.Parameters.AddWithValue("@Holder_Account_Number",acc);
            cn.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            List < Beneficiary > blist= new List<Beneficiary>();
            
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    Beneficiary b = new Beneficiary();
                    b.Holder_Account_Number = (long)dr[0];
                    b.Payee_Account_Number = (long)dr[1];
                    b.Nickname = (string)dr[2];
                    b.Branch_Name = (string)dr[3];
                    b.IFSC = (string)dr[4];

                    blist.Add(b);
                }
                      
            }
            return blist;
   

        }
        public void delete_Beneficiary(long AccountNo, long Payee_Account_Number)
        {
            SqlConnection cn = new SqlConnection("Data Source=DESKTOP-5HFRN07\\SQLEXPRESS;Initial Catalog=Bank;Integrated Security=True");
            SqlCommand cmd = new SqlCommand("delete_Beneficiary", cn);
            cmd.Parameters.AddWithValue("@AccountNo", AccountNo);
            cmd.Parameters.AddWithValue("@Payee_Account_Number",Payee_Account_Number);
            cmd.CommandType = CommandType.StoredProcedure;

            cn.Open();
            int i = cmd.ExecuteNonQuery();
            cn.Close();
            cn.Dispose();
        }
    }
}
