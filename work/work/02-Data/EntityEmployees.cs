using SpreadsheetLight;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace work.Data
{
    class EntityEmployees
    {
        #region Properties
        public List<Employee> listUsers;
        public string Path = Directory.GetCurrentDirectory() + ConfigurationManager.AppSettings["EmployeesPath"];
        #endregion

        #region  Constructor
        public EntityEmployees()
        {
            this.listUsers = new List<Employee>();
        }
        #endregion

        #region Methods
        public override string ToString()
        {
            string result = "   EMAIL    |    TIPO DE FUNCIONARIO    |    PRIMEIRO NOME    |    ULTIMO NOME    |    CONTACTO    |    MORADA    |     DATA DE NASCIMENTO   " + "\n";

            foreach (var item in this.listUsers)
            {
                result += item.email + "  |   " + EnumHelper<Enums.EnumTypeEmployee>.GetDisplayValue(item.type) + "  |   " + item.firstName + "  |   " + item.lastName + "  |   " + item.contact + "  |   " + item.address + "  |   " + item.birthDate + "\n";
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
                this.listUsers.Clear();
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
                this.listUsers.Clear();

                SLDocument sl = new SLDocument();
                FileStream fs = new FileStream(Path, FileMode.Open);

                SLDocument styleSheet2 = new SLDocument(fs);

                SLWorksheetStatistics stats2 = styleSheet2.GetWorksheetStatistics();
                for (int j = 2; j <= stats2.EndRowIndex; j++)
                {
                    var id = styleSheet2.GetCellValueAsInt64(j, 1);
                    var email = styleSheet2.GetCellValueAsString(j, 2);
                    var authHash = styleSheet2.GetCellValueAsString(j, 3);
                    var lastDateLogin = styleSheet2.GetCellValueAsDateTime(j, 4);
                    var type = styleSheet2.GetCellValueAsInt32(j, 5);
                    var firstName = styleSheet2.GetCellValueAsString(j, 6);
                    var lastName = styleSheet2.GetCellValueAsString(j, 7);
                    var address = styleSheet2.GetCellValueAsString(j, 8);

                    var contact = styleSheet2.GetCellValueAsInt64(j, 9);
                    var birthDate = styleSheet2.GetCellValueAsDateTime(j, 10);
                    var initWork = styleSheet2.GetCellValueAsDateTime(j, 11);
                    var salary = styleSheet2.GetCellValueAsDecimal(j, 12);
                    var Added = styleSheet2.GetCellValueAsDateTime(j, 13);
                    var AddedBy = styleSheet2.GetCellValueAsInt64(j, 14);
                    var Updated = styleSheet2.GetCellValueAsDateTime(j, 15);
                    var UpdatedBy = styleSheet2.GetCellValueAsInt64(j, 16);


                    Employee user = new Employee(id, email, authHash, lastDateLogin, type, firstName, lastName, address, contact, birthDate, initWork, salary, Added, AddedBy, Updated, UpdatedBy);

                    this.listUsers.Add(user);
                }

                fs.Close();

            }
            catch (Exception)
            {

                throw;
            }
        }

        public Employee FindEmployees(string Login, string Password)
        {
            if (!this.listUsers.Any())
            {
                List();
            }

            Employee u = this.listUsers.FirstOrDefault(x => x.email.ToLower() == Login.ToLower());

            if (u != null)
            {
                return u;
            }

            return null;
        }

        public Employee FindEmployeesToLogin(string Email, string Password)
        {
            if (!this.listUsers.Any())
            {
                List();
            }

            if (!string.IsNullOrEmpty(Email) && !string.IsNullOrEmpty(Password))
            {
                string hash = GetHash(Email.ToLower(), Password);
                Employee u = this.listUsers.FirstOrDefault(x => x.authHash == hash);

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
        //public bool RemoveUser(string nome)
        //{
        //    Users useroToRemove = FindUsers(nome);

        //    if (useroToRemove != null)
        //    {
        //        this.listUsers.Remove(useroToRemove);
        //        CleanFile();
        //        foreach (var item in this.listUsers)
        //        {
        //            SaveToTxt(item);
        //        }
        //        return true;
        //    }

        //    return false;
        //}

        //public Contacto EditContact(string nome, string novoNome, string novoTelefone, string novaMorada)
        //{
        //    Contacto contactoAEditar = FindContact(nome);

        //    if (RemoveFromContacs(nome))
        //    {
        //        if (contactoAEditar != null)
        //        {
        //            if (!string.IsNullOrEmpty(novoNome))
        //            {
        //                contactoAEditar.nome = novoNome;
        //            }
        //            if (!string.IsNullOrEmpty(novoTelefone))
        //            {
        //                contactoAEditar.numeroDeTelefone = novoTelefone;
        //            }
        //            if (!string.IsNullOrEmpty(novaMorada))
        //            {
        //                contactoAEditar.morada = novaMorada;
        //            }
        //            SaveToTxt(contactoAEditar);
        //            List();
        //            return contactoAEditar;
        //        }
        //    }
        //    return null;
        //}

        public Employee AddEmployee(string email, int type, string firstName, string lastName, string address, long contact, DateTime birthDate, DateTime initWork, decimal salary, string passWord, long userId)
        {
            Employee e = new Employee(email, type, firstName, lastName, address, contact, birthDate, initWork, salary, passWord, userId);
            this.listUsers.Add(e);
            FileStream fs = new FileStream(Path, FileMode.Open);

            MemoryStream ms = new MemoryStream();
            using (SLDocument sl = new SLDocument(fs))
            {
                sl.SetCellValue("B3", "I love ASP.NET MVC");
                sl.SetCellValue("C3", "I love ASP.NET MVC");
                sl.SetCellValue("D3", "I love ASP.NET MVC");

                sl.SaveAs(ms);
            }
            // this is important. Otherwise you get an empty file
            // (because you'd be at EOF after the stream is written to, I think...).
            ms.Position = 0;
            return e;
        }

        //public void SaveToTxt(Users item)
        //{
        //    using (TextWriter tw = new StreamWriter(Path, over))
        //    {
        //        //tw.WriteLine(string.Format($"{item.nome} | {item.morada} | {item.numeroDeTelefone}"));
        //    }
        //}
        #endregion

    }
}
