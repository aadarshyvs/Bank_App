﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankBAL;
using BankEntity;
using BankException;

namespace Bank
{
    class BankUI
    {

        static void Main(string[] args)
        {
            

            int choice = -1;
            do
            {
                Console.WriteLine("\t \t \t \t \t Welcome to Zigma Banking Systems");
                Console.WriteLine("\t \t \t \t \t --------------------------------");

                try
                {




                    Console.WriteLine("\t\t\t\t1.User login \t\t\t\t2.Admin login \nEnter Your Choice .........");
                    choice = int.Parse(Console.ReadLine());
                    switch (choice)
                    {
                        case 1:

                            Console.WriteLine("\t\t\t\t1.New User\t\t\t\t2.Existing User\nEnter Your Choice .........");
                            choice = int.Parse(Console.ReadLine());
                            switch (choice)
                            {
                                case 1:
                                    //code for new user
                                    Create_New_User();
                                    break;
                                case 2:
                                    //code for existing user
                                    Existing_User();
                                    break;
                                default:
                                    throw (new FormatException("Invalid choice"));
                                    break;

                            }
                            break;
                        case 2:
                            //Admin Code
                            Admin();

                            break;
                        default:
                            throw (new FormatException("Invalid choice"));
                            break;

                    }


                    Console.WriteLine("Operation Compleated Successfully ..........");






                }
                catch (FormatException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                finally
                {
                    Console.WriteLine("Press Any Key To Continue..........");
                    Console.ReadKey();
                    Console.Clear();
                }
            } while (choice > 0);
        }



        public static void Create_New_User()
        {
            try
            {


                Coustomer c = new Coustomer();
                BankBalClass bal = new BankBalClass();
                Random rand = new Random();
                Console.WriteLine("Welcome To Our Bank");
                Console.WriteLine("Enter Name ");
                c.Name = Console.ReadLine();
                bal.Name = c.Name;
                Console.WriteLine("Enter Mobile Number");
                c.MoblieNumber = long.Parse(Console.ReadLine());
                bal.MoblieNumber = c.MoblieNumber;
                c.AccNumber = rand.Next(100000000, 999999999);
                bal.AccNumber = c.AccNumber;
                c.CRN = rand.Next(10000, 99999);
                bal.CRN = c.CRN;
                c.IBPassword = "Welcome" + c.AccNumber.ToString();
                bal.IBPassword = c.IBPassword;
                c.BranchName = "Chennai";
                bal.BranchName = c.BranchName;
                c.IFSC = "ZIN00001";
                bal.IFSC = c.IFSC;
                c.Status = "Open";
                bal.Status = c.Status;
                Console.WriteLine($"We Welcome You With A Kit \nYour Account Number :{c.AccNumber}\nYour CRN Number : {c.CRN}\nYour Internet Banking Password :{c.IBPassword} ");










                bal.Create_New_User(c);
            }
            catch (FormatException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }


        }
        public static void Existing_User()
        {
            try
            {


                Console.WriteLine("Enter Account Number");
                long AccountNo = int.Parse(Console.ReadLine());

                BankBalClass bal = new BankBalClass();
                string s = bal.valdate_User(AccountNo);
                if (s == "Open")
                {

                    User_Reg(AccountNo);

                }
                else if (s == "Active")
                {
                    int choice;
                    Console.WriteLine("\t1.Fund Transfer\t\t2.View Statement\t\t3.Update Details\t\t4.Beneficiary\nEnter Your Choice .........");
                    choice = int.Parse(Console.ReadLine());
                    switch (choice)
                    {



                        case 1:
                            //Code for Fund Transfer
                            Fund_Transfer(AccountNo);

                            break;
                        case 2:
                            //code for View Statement
                            View_Stament(AccountNo);
                            break;

                        case 3:
                            //code for Update Details
                            Update_users(AccountNo);
                            break;
                        case 4:

                            Beneficiary(AccountNo);
                            break;
                        default:
                            Console.WriteLine("Invalied Choice");
                            break;

                    }


                }
                else if (s == "Freeze")
                {
                    throw (new FormatException("Account Freezed"));
                }
                else
                {
                    throw (new FormatException("Account Not Found"));
                }
            }
            catch (FormatException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public static void User_Reg(long AccountNo)
        {
            try
            {
                BankBalClass bal = new BankBalClass();

                Coustomer c = new Coustomer();
                Console.WriteLine("Enter New Internet Banking Password ");
                c.IBPassword = Console.ReadLine();
                bal.AccNumber = c.AccNumber;
                Console.WriteLine("Enter Transation Password ");
                c.TxnPassword = Console.ReadLine();
                bal.IBPassword = c.IBPassword;
                Console.WriteLine("Enter Address");
                c.Address = Console.ReadLine();
                bal.Address = c.Address;
                Console.WriteLine("Enter Email ID");
                c.EmailID = Console.ReadLine();
                bal.EmailID = c.EmailID;
                Console.WriteLine("Enter Date Of Birth (YYYY/MM/DD) ");
                c.DOB = Console.ReadLine();
                bal.DOB = c.DOB;
                Console.WriteLine("Enter Amount to be Diposited (>5000)");
                c.Balance = int.Parse(Console.ReadLine());

                c.AccNumber = AccountNo;



                bal.Balance = c.Balance;

                bal.User_Reg(c);
            }
            catch (FormatException ex)
            {
                Console.WriteLine(ex);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

        }
        public static void Fund_Transfer(long AccountNo)
        {
            try
            {


                transations t = new transations();
                BankBalClass bal = new BankBalClass();


                t.Sender = AccountNo;

                Console.WriteLine("Enter IB Password");
                string pass = Console.ReadLine();
                bool check = Verify_Password(t.Sender, pass);
                if (check)
                {
                    Console.WriteLine("Enter Reciver Account Number");
                    t.Receiver = int.Parse(Console.ReadLine());
                    string s;
                    s = bal.valdate_User(t.Receiver);
                    if (s == "Open")
                    {
                        throw new FormatException("You Cannot Send TO A Account Which Is Not Registered");
                    }
                    else if (s == "Active")
                    {
                        Console.WriteLine("Enter Amount To Transfer");
                        t.Amount = Convert.ToInt64(Console.ReadLine());

                        t.Date = DateTime.Now.ToString("MM/dd/yyyy hh:mm tt");

                        bal.Fund_Transfer(t);

                    }
                    else if (s == "Freeze")
                    {
                        throw (new FormatException("Account Freezed"));
                    }
                    else
                    {
                        throw (new FormatException("Account Not Found"));
                    }

                }
                else
                {
                    throw new FormatException("INVALID PASSWORD");

                }

            }
            catch (FormatException ex)
            {
                Console.WriteLine(ex);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }


        }
        public static bool Verify_Password(long accno, string Ibpass)
        {
            bool r = false;
            try
            {



                BankBalClass bal = new BankBalClass();
                r = bal.Verify_Password(accno, Ibpass);

            }
            catch (FormatException ex)
            {
                Console.WriteLine(ex);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return r;
        }
        public static void View_Stament(long AccountNo)
        {
            try
            {



                Console.WriteLine("Enter IB Password");
                string pass = Console.ReadLine();
                bool check = Verify_Password(AccountNo, pass);
                if (check)
                {
                    BankBalClass bal = new BankBalClass();
                    List<transations> t_list = bal.View_Stament(AccountNo);
                    foreach (transations tr in t_list)
                    {
                        Console.WriteLine($"{tr.Sender}\t{tr.Receiver}\t{tr.Amount}\t{tr.Date}");
                    }

                }
                else
                {
                    throw (new FormatException("Invalid Password"));
                }
            }
            catch (FormatException ex)
            {
                Console.WriteLine(ex);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
        public static void Update_users(long AccountNo)
        {
            try
            {


                Console.WriteLine("Enter IB Password");
                string pass = Console.ReadLine();
                bool check = Verify_Password(AccountNo, pass);
                if (check)
                {
                    BankBalClass bal = new BankBalClass();
                    Coustomer c = new Coustomer();
                    c.AccNumber = AccountNo;

                    Console.WriteLine("Enter New Internet Banking Password ");
                    c.IBPassword = Console.ReadLine();

                    Console.WriteLine("Enter Email ID");
                    c.EmailID = Console.ReadLine();

                    Console.WriteLine("Enter Mobile Number");
                    c.MoblieNumber = int.Parse(Console.ReadLine());

                    Console.WriteLine("Enter Address");
                    c.Address = Console.ReadLine();


                    bal.Update_users(c);
                }
                else
                {
                    throw (new FormatException("Invalid Password"));
                }
            }
            catch (FormatException ex)
            {
                Console.WriteLine(ex);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

        }
        public static void Admin()
        {
            try
            {
                Console.WriteLine("Enter Admin Id");
                string id = Console.ReadLine();
                Console.WriteLine("Enter Admin Password");
                string Password = Console.ReadLine();

                if (id == "admin" && Password == "admin123")
                {
                    int choice;
                    Console.WriteLine("\t1.WithDraw\t\t\t\t2.Deposit\t\t\t\t3.Freeze Account\nEnter Your Choice .........");
                    choice = int.Parse(Console.ReadLine());
                    switch (choice)
                    {
                        case 1:
                            Withdraw();
                            break;
                        case 2:
                            Dipsoit();
                            break;
                        case 3:
                            Freeze_Account();
                            break;
                        default:
                            throw (new FormatException("Invalid Choice"));
                            break;

                    }

                }

            }

            catch (FormatException ex)
            {
                Console.WriteLine(ex);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
        public static void Withdraw()
        {
            try
            {


                Console.WriteLine("Enter Account Number");
                long AccountNo = int.Parse(Console.ReadLine());
                Console.WriteLine("Enter Amount To Wihdraw");
                long amount = int.Parse(Console.ReadLine());
                BankBalClass bal = new BankBalClass();
                bal.Withdraw(AccountNo, amount);
            }
            catch (FormatException ex)
            {
                Console.WriteLine(ex);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
        public static void Dipsoit()
        {
            try
            {


                Console.WriteLine("Enter Account Number");
                long AccountNo = int.Parse(Console.ReadLine());
                Console.WriteLine("Enter Amount To Dipsoit");
                long amount = int.Parse(Console.ReadLine());
                BankBalClass bal = new BankBalClass();
                bal.Dipsoit(AccountNo, amount);
            }
            catch (FormatException ex)
            {
                Console.WriteLine(ex);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
        public static void Freeze_Account()
        {
            try
            {


                Console.WriteLine("Enter Account Number");
                long AccountNo = int.Parse(Console.ReadLine());
                BankBalClass bal = new BankBalClass();
                bal.Freeze_Account(AccountNo);
            }
            catch (FormatException ex)
            {
                Console.WriteLine(ex);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
        public static void Beneficiary(long AccountNo)
        {
            int choice;
            Console.WriteLine("\t1.Add Beneficiary\t2.Edit Beneficiary\t\t3.View Beneficiary\t\t4.Delete Beneficiary\nEnter Your Choice .........");
            choice = int.Parse(Console.ReadLine());
            switch (choice)
            {
                case 1:
                    Add_Beneficiary(AccountNo);
                    break;
                case 2:
                    Edit_Beneficiary(AccountNo);
                    break;
                case 3:
                    Show_all_Beneficiary(AccountNo);
                    break;
                case 4:
                    delete_Beneficiary(AccountNo);
                    break;
                default:
                    throw (new FormatException("Invalid Choice"));
                    break;

            }


        }

        private static void Add_Beneficiary(long AccountNo)
        {
            Console.WriteLine("Enter IB Password");
            string pass = Console.ReadLine();
            bool check = Verify_Password(AccountNo, pass);
            if (check)
            {
                try
                {
                    Beneficiary b = new Beneficiary();
                    b.Holder_Account_Number = AccountNo;
                    BankBalClass bal = new BankBalClass();
                    Console.WriteLine("Enter Payee Account Number");
                    b.Payee_Account_Number = int.Parse(Console.ReadLine());
                    string s;
                    s = bal.valdate_User(b.Payee_Account_Number);
                    if (s == "Open")
                    {
                        throw new FormatException("You Cannot Send TO A Account Which Is Not Registered");
                    }
                    else if (s == "Active")
                    {
                        Console.WriteLine("Enter Nickname");
                        b.Nickname = Console.ReadLine();
                        bal.Nickname = b.Nickname;

                        Console.WriteLine("Enter Branch Name");
                        b.Branch_Name = Console.ReadLine();
                        bal.BranchName = b.Branch_Name;

                        Console.WriteLine("Enter IFSC");
                        b.IFSC = Console.ReadLine();
                        bal.IFSC = b.IFSC;


                        bal.Beneficiary(b);

                    }
                    else if (s == "Freeze")
                    {
                        throw (new FormatException("Account Freezed"));
                    }
                    else
                    {
                        throw (new FormatException("Account Not Found"));
                    }


                }
                catch (FormatException ex)
                {
                    Console.WriteLine(ex);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }

            }
            else
            {
                throw new FormatException("Invalid Password");
            }
        }
        public static void Edit_Beneficiary(long AccountNo)
        {
            try
            {
                Console.WriteLine("Enter IB Password");
                string pass = Console.ReadLine();
                bool check = Verify_Password(AccountNo, pass);
                if (check)
                {
                    Beneficiary b = new Beneficiary();
                    BankBalClass bal = new BankBalClass();
                    b.Holder_Account_Number = AccountNo;


                    Console.WriteLine("Enter Payee Account Number");
                    b.Payee_Account_Number = int.Parse(Console.ReadLine());
                    if (bal.Check_Beneficiary(b.Holder_Account_Number, b.Payee_Account_Number))
                    {
                        Console.WriteLine("Enter Nickname");
                        b.Nickname = Console.ReadLine();
                        bal.Nickname = b.Nickname;

                        Console.WriteLine("Enter Branch Name");
                        b.Branch_Name = Console.ReadLine();
                        bal.BranchName = b.Branch_Name;

                        Console.WriteLine("Enter IFSC");
                        b.IFSC = Console.ReadLine();
                        bal.IFSC = b.IFSC;

                        bal.Edit_Beneficiary(b);
                    }
                    else
                    {
                        throw new FormatException($"{ b.Payee_Account_Number} Not Found");
                    }
                }
                else
                {
                    throw new FormatException("Invalid Password");
                }

            }
            catch (FormatException ex)
            {
                Console.WriteLine(ex);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }


        }
        public static void Show_all_Beneficiary(long AccountNo)
        {
            try
            {
                Console.WriteLine("Enter IB Password");
                string pass = Console.ReadLine();
                bool check = Verify_Password(AccountNo, pass);
                if (check)
                {
                    BankBalClass bal = new BankBalClass();
                    List<Beneficiary> blist = bal.Show_all_Beneficiary(AccountNo);
                    foreach (var b in blist)
                    {
                        Console.WriteLine($"{b.Payee_Account_Number}\t{b.Nickname}\t{b.Branch_Name}\t{b.IFSC}");
                    }
                }
                else
                {
                    throw new FormatException("Invalid Password");
                }
            }
            
            catch (FormatException ex)
            {
                Console.WriteLine(ex);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
        public static void delete_Beneficiary(long AccountNo)
        {
            try
            {
                Console.WriteLine("Enter IB Password");
                string pass = Console.ReadLine();
                bool check = Verify_Password(AccountNo, pass);
                if (check)
                {
                    BankBalClass bal = new BankBalClass();
                    Console.WriteLine("Enter Payee Account Number");
                   long Payee_Account_Number = int.Parse(Console.ReadLine());

                    if (bal.Check_Beneficiary(AccountNo, Payee_Account_Number))
                    {
                        bal.delete_Beneficiary(AccountNo, Payee_Account_Number);
                    }
                    }
                else
                {
                    throw new FormatException("Invalid Password");
                }
            }

            catch (FormatException ex)
            {
                Console.WriteLine(ex);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }


    }
}


