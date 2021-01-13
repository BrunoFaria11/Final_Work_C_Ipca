using System;
using work.Data;
using work.Screens;

namespace work
{
    class Program
    {
        public Employee SessionUser;
        static void Main(string[] args)
        {
            Console.WriteLine("------------------------------ Bem Vindo ------------------------------");

            InitMenu.initMenu();

        }
    }
}
