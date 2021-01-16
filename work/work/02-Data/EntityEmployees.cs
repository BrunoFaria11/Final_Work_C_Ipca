using Nancy.Json;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SpreadsheetLight;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using work.Enums;

namespace work.Data
{
    class EntityEmployees
    {
        #region Properties
        public List<Employee> listEmployees;
        public string Path = Directory.GetCurrentDirectory() + ConfigurationManager.AppSettings["EmployeesPath"];
        #endregion

        #region  Constructor
        public EntityEmployees()
        {
            this.listEmployees = new List<Employee>();
        }
        #endregion

        #region Methods
        public override string ToString()
        {
            string result = "   EMAIL   |   TIPO DE FUNCIONARIO   |   PRIMEIRO NOME   |   ULTIMO NOME   |   CONTACTO   |   MORADA   |    DATA DE NASCIMENTO   " + "\n";

            foreach (var item in this.listEmployees)
            {
                result += item.email + " |  " + EnumHelper<Enums.EnumTypeEmployee>.GetDisplayValue(item.type) + " |  " + item.firstName + " |  " + item.lastName + " |  " + item.contact + " |  " + item.address + " |  " + item.birthDate + "\n";
            }

            return result;
        }
        public Employee MakeLogin(string Email, string Password)
        {
            try
            {
                return FindEmployeesToLogin(Email, Password);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool ClearList()
        {
            try
            {
                this.listEmployees.Clear();
                using (TextWriter tw = new StreamWriter(Path))
                {
                    tw.WriteLine("");
                }
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void List()
        {
            try
            {
                this.listEmployees.Clear();

                using (StreamReader streamReader = new StreamReader(Path))
                {
                    while (!streamReader.EndOfStream)
                    {
                        string lineTXT = streamReader.ReadLine();
                        long id = Convert.ToInt64(lineTXT.Split("|")[0]);
                        string firstName = lineTXT.Split("|")[1];
                        string lastName = lineTXT.Split("|")[2];
                        string address = lineTXT.Split("|")[3];
                        long contact = Convert.ToInt64(lineTXT.Split("|")[4]);
                        DateTime birthDate = Convert.ToDateTime(lineTXT.Split("|")[5]);
                        DateTime initWork = Convert.ToDateTime(lineTXT.Split("|")[6]);
                        decimal salary = Convert.ToDecimal(lineTXT.Split("|")[7]);
                        string email = lineTXT.Split("|")[8];
                        string authHash = lineTXT.Split("|")[9];
                        DateTime lastDateLogin = Convert.ToDateTime(lineTXT.Split("|")[10]);
                        EnumTypeEmployee type = (EnumTypeEmployee)Convert.ToInt32(lineTXT.Split("|")[11]);
                        DateTime Added = Convert.ToDateTime(lineTXT.Split("|")[12]);
                        long AddedBy = Convert.ToInt64(lineTXT.Split("|")[13]);
                        DateTime Updated = Convert.ToDateTime(lineTXT.Split("|")[14]);
                        long UpdatedBy = Convert.ToInt64(lineTXT.Split("|")[15]);

                        listEmployees.Add(new Employee(id, email, authHash, lastDateLogin, type, firstName, lastName, address, contact, birthDate, initWork, salary, Added, AddedBy, Updated, UpdatedBy));
                    }
                }

            }
            catch (Exception ex)
            {
            }
        }

        public Employee FindEmployeesToEdit(string email)
        {
            if (!this.listEmployees.Any())
            {
                List();
            }

            Employee u = this.listEmployees.FirstOrDefault(x => x.email.ToLower() == email.ToLower());

            if (u != null)
            {
                return u;
            }

            return null;
        }

        public Employee FindEmployeesToLogin(string Email, string Password)
        {
            if (!this.listEmployees.Any())
            {
                List();
            }

            if (!string.IsNullOrEmpty(Email) && !string.IsNullOrEmpty(Password))
            {
                string hash = GetHash(Email.ToLower(), Password);
                Employee u = this.listEmployees.FirstOrDefault(x => x.authHash == hash);

                if (u != null)
                {
                    return u;
                }
            }
            return null;
        }

        private string GetHash(string login, string passsWord)
        {
            StringBuilder Sb = new StringBuilder();

            using (SHA256 hash = SHA256Managed.Create())
            {
                Encoding enc = Encoding.UTF8;
                Byte[] result = hash.ComputeHash(enc.GetBytes(string.Concat(login, passsWord)));

                foreach (Byte b in result)
                    Sb.Append(b.ToString("x2"));
            }

            return Sb.ToString();
        }
        public bool RemoveUser(string email)
        {
            Employee employeeToRemove = FindEmployeesToEdit(email);

            if (employeeToRemove != null)
            {
                this.listEmployees.Remove(employeeToRemove);
                CleanFile();
                foreach (var item in this.listEmployees)
                {
                    SaveToTxt(item);
                }
                return true;
            }

            return false;
        }

        public Employee EditEmployee(string email, int type, string firstName, string lastName, string address, long contact, decimal salary, long userId)
        {
            Employee employeeToEdit = FindEmployeesToEdit(email);

            if (employeeToEdit != null)
            {
                if (removeFromEmployees(employeeToEdit.id))
                {
                    SaveToTxt(employeeToEdit);
                    List();
                    return employeeToEdit;
                }
            }
            return null;
        }

        public Employee AddEmployee(string email, int type, string firstName, string lastName, string address, long contact, DateTime birthDate, DateTime initWork, decimal salary, string passWord, long userId)
        {
            try
            {
                Employee e = new Employee(email, (EnumTypeEmployee)type, firstName, lastName, address, contact, birthDate, initWork, salary, passWord, userId);

                SaveToTxt(e);

                return e;
            }
            catch (Exception ex)
            {

                throw;
            }

        }

        public bool removeFromEmployees(long Id)
        {
            Employee contactoToRemove = this.listEmployees.Where(x => x.id == Id).FirstOrDefault();

            if (contactoToRemove != null)
            {
                this.listEmployees.Remove(contactoToRemove);
                CleanFile();
                foreach (var item in this.listEmployees)
                {
                    SaveToTxt(item);
                }
                return true;
            }

            return false;
        }
        public void CleanFile()
        {
            using (TextWriter tw = new StreamWriter(Path))
            {
                tw.Write("");
            }
        }
        public void SaveToTxt(Employee e)
        {

            using (TextWriter StreamWriter = new StreamWriter(Path, true))
            {
                StreamWriter.WriteLine(e.id + "|" + e.firstName + "|" + e.lastName + "|" + e.address + "|" + e.contact + "|" + e.birthDate + "|" + e.initWork + "|" + e.salary + "|" + e.email + "|" + e.authHash + "|" + e.lastDateLogin + "|"
                    + (int)e.type + "|" + e.Added + "|" + e.AddedBy + "|" + e.Updated + "|" + e.UpdatedBy + "\n");
            }
        }
        #endregion

    }
}
