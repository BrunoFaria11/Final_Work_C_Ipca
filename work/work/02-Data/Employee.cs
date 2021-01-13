using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using work.Enums;
using work.Data;

namespace work.Data
{
    class Employee : DataEmployee
    {
        #region Properties
        public long id { get; set; }
        public string email { get; set; }
        public string authHash { get; set; }
        public DateTime? lastDateLogin { get; set; }
        public EnumTypeEmployee type { get; set; }
        #endregion

        #region  Constructor
        public Employee(string email, int type, string firstName, string lastName, string address, long contact, DateTime birthDate, DateTime initWork, decimal salary, string passWord, long userId )
        {

            this.id = generateId();
            this.email = email;
            this.authHash = GetHash(passWord);
            this.lastDateLogin = lastDateLogin;
            this.type = (EnumTypeEmployee)type;
            this.firstName = firstName;
            this.lastName = lastName;
            this.address = address;
            this.contact = contact;
            this.birthDate = birthDate;
            this.initWork = initWork;
            this.salary = salary;
            this.Added = DateTime.Now;
            this.AddedBy = userId;
        }

        public Employee(long id,string email, string authHash, DateTime lastDateLogin, int type, string firstName, string lastName, string address, long contact,DateTime birthDate, DateTime initWork, decimal salary, DateTime Added, long AddedBy, DateTime Updated, long UpdatedBy)
        {
            this.id = id;
            this.email = email;
            this.authHash = authHash;
            this.lastDateLogin = lastDateLogin;
            this.type = (EnumTypeEmployee)type;
            this.firstName = firstName;
            this.lastName = lastName;
            this.address = address;
            this.contact = contact;
            this.birthDate = birthDate;
            this.initWork = initWork;
            this.salary = salary;
            this.Added = Added;
            this.AddedBy = AddedBy;
            this.Updated = Updated;
            this.UpdatedBy = UpdatedBy;
        }

        #endregion

        #region Methods
        private string GetHash(string passsWord)
        {
            StringBuilder Sb = new StringBuilder();

            using (SHA256 hash = SHA256Managed.Create())
            {
                Encoding enc = Encoding.UTF8;
                Byte[] result = hash.ComputeHash(enc.GetBytes(string.Concat(this.email, passsWord)));

                foreach (Byte b in result)
                    Sb.Append(b.ToString("x2"));
            }

            return Sb.ToString();
        }

        private long generateId()
        {
            return DateTime.Now.Ticks;
        }

        #endregion

    }


}
