using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using BankDAL;
using BankEntity;
using BankException;


namespace BankBAL
{
    public class BankBalClass
    {

        long _accNumber;
        int _cRN;
        string _iBPassword;
        string _txnPassword;
        string _branchName;
        string _iFSC;
        string _name;
        string _address;
        long _moblieNumber;
        string _emailID;
        string _status;
        string _bOB;
        float _balance;
        long _sender;
        long _receiver;
        string _date;
        float _amount;
        long _payee_Account_Number;
        long _holder_Account_Number;
        string _nickname;



        public long AccNumber
        {
            get { return _accNumber; }
            set
            {
                _accNumber = value;
            }
        }
        public int CRN { get; set; }
        public string IBPassword
        {
            get { return _iBPassword; }
            set
            {
                if (value.Length > 5 && value.Length <= 20)
                {
                    //if (System.Text.RegularExpressions.Regex.IsMatch(value, @"^[A-Za-z0-9]$"))
                    //{

                    //}
                    _iBPassword = value;
                    //throw (new FormatException("IB Password Is Not AlfaNumaric Password"));

                }
                else
                {
                    throw (new FormatException("IB Password Is Too Short Or Too Long"));
                }
            }
        }
        public string TxnPassword
        {
            get { return _txnPassword; }
            set
            {
                if (value.Length > 5 && value.Length <= 20)
                {
                    //if (System.Text.RegularExpressions.Regex.IsMatch(value, @"^[A-Za-z0-9]+$"))
                    //{

                    //}
                    _txnPassword = value;
                    //else
                    //{
                    //    throw (new FormatException("txn Password Is Too Short Or Too Long"));
                    //}

                }
                else
                {
                    throw (new FormatException("Transation Password Is Too Short Or Too Long"));
                }
            }

        }
        public string BranchName { get; set; }
        public string IFSC { get; set; }
        public string Name
        {
            get { return _name; }
            set
            {
                if (value.Length > 5 && value.Length <= 30)
                {
                    _name = value;
                }
                else
                {
                    throw (new FormatException("Name Is Too Short Or Too Long"));
                }
            }
        }

        public string Address
        {
            get { return _address; }
            set
            {
                if (value.Length <= 50)
                {
                    _name = value;
                }
                else
                {
                    throw (new FormatException("Address Is Too Short Or Too Long"));
                }
            }
        }
        public long MoblieNumber
        {
            get { return _moblieNumber; }
            set
            {
                if (value.ToString().Length == 10)
                {
                    _moblieNumber = value;
                }
                else
                {
                    throw (new FormatException("Invalid Mobile Number"));
                }
            }
        }
        public string EmailID
        {
            get { return _emailID; }
            set
            {
                try
                {
                    var mail = new System.Net.Mail.MailAddress(value);
                    _emailID = value;
                }
                catch
                {
                    throw (new FormatException("Invailed Email Id"));
                }
            }
        }
        public string Status { get; set; }
        public string DOB { get; set; }
        public float Balance
        {
            get { return _balance; }
            set
            {
                if (value > 1000)
                {
                    _balance = value;
                }
                else
                {
                    throw (new FormatException("Blance Is Lessthan Minimum Balance"));
                }
            }
        }
        public long Sender
        {
            get; set;
        }
        public long Receiver { get; set; }
        public string Date { get; set; }
        public float Amount
        {
            get { return _amount; }
            set
            {
                if (value > 10 && value < 200000)
                {
                    _amount = value;
                }
                else
                {
                    throw (new FormatException("You Can Send Amount Only Between 10 to 200000"));
                }
            }
        }
        public long Holder_Account_Number
        {
            get { return _holder_Account_Number; }
            set
            {
                _holder_Account_Number = value;
            }
        }
        public long Payee_Account_Number
        {
            get { return _payee_Account_Number; }
            set
            {
                _payee_Account_Number = value;
            }
        }
        public string Nickname
        {
            get { return _nickname; }
            set
            {
                if (value.Length > 5 && value.Length <= 30)
                {
                    _nickname = value;
                }
                else
                {
                    throw (new FormatException("Name Is Too Short Or Too Long"));
                }
            }
        }
        public void Create_New_User(Coustomer c)
        {
            BankDALClass dal = new BankDALClass();
            Console.WriteLine("BAL Layer");
            //Console.WriteLine($"We Welcome You With A Kit \nYour Account Number :{c.AccNumber}\nYour CRN Number : {c.CRN}\nYour Internet Banking Password :{c.IBPassword} ");
            dal.Create_New_User(c);
        }
        public string valdate_User(long AccountNo)
        {
            BankDALClass dal = new BankDALClass();
            string rtn = "";
            rtn = dal.valdate_User(AccountNo);
            return rtn;


        }
        public void User_Reg(Coustomer c)
        {

            BankDALClass dal = new BankDALClass();

            dal.User_Reg(c);

        }
        public bool Verify_Password(long accno, string Ibpass)
        {
            BankDALClass dal = new BankDALClass();
            bool r = dal.Verify_Password(accno, Ibpass);
            return r;
        }
        public void Fund_Transfer(transations t)
        {
            BankDALClass dal = new BankDALClass();
            dal.Fund_Transfer(t);


        }
        public List<transations> View_Stament(long AccountNo)
        {
            BankDALClass dal = new BankDALClass();
            List<transations> t_list = dal.View_Stament(AccountNo);
            return t_list;

        }
        public void Update_users(Coustomer c)
        {
            BankDALClass dal = new BankDALClass();
            dal.Update_users(c);
        }
        public void Withdraw(long AccountNo, long amount)
        {
            BankDALClass dal = new BankDALClass();
            dal.Withdraw(AccountNo, amount);
        }
        public void Dipsoit(long AccountNo, long amount)
        {
            BankDALClass dal = new BankDALClass();
            dal.Dipsoit(AccountNo, amount);
        }
        public void Freeze_Account(long AccountNo)
        {
            BankDALClass dal = new BankDALClass();
            dal.Freeze_Account(AccountNo);
        }
        public void Beneficiary(Beneficiary b)
        {
            BankDALClass dal = new BankDALClass();
            dal.Beneficiary(b);


        }
        public bool Check_Beneficiary(long h, long p)
        {
            BankDALClass dal = new BankDALClass();
            return dal.Check_Beneficiary(h, p);

        }
        public void Edit_Beneficiary(Beneficiary b)
        {
            BankDALClass dal = new BankDALClass();
            dal.Edit_Beneficiary(b);
        }
        public List<Beneficiary> Show_all_Beneficiary(long acc)
        {
            BankDALClass dal = new BankDALClass();
            List<Beneficiary> blist = dal.Show_all_Beneficiary(acc);
            return blist;
        }
        public void delete_Beneficiary(long AccountNo,long Payee_Account_Number)
        {
            BankDALClass dal = new BankDALClass();
            dal.delete_Beneficiary(AccountNo, Payee_Account_Number);
        }

    }
}
