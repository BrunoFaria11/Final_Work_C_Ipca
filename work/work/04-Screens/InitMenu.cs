using System;
using System.Collections.Generic;
using System.Text;
using work.Data;
using work.Screens;

namespace work.Screens
{
    class InitMenu
    {
        public static void initMenu()
        {
            int response = -1;

            while (response != 0)
            {
                Console.WriteLine("#######################################################################");
                Console.WriteLine("#  1 - Login                                                          #");
                Console.WriteLine("#  0 - Sair                                                           #");
                Console.WriteLine("#######################################################################");

                Console.WriteLine("Insira uma opção");
                response = Convert.ToInt32(Console.ReadLine());
                switch (response)
                {
                    case 1:
                        ScnLogin.Login();
                        break;
                    default:
                        break;
                }
            }
        }
        public static void Menu(Employee sessionUser)
        {
            int response = -1;
       
            while (response != 0)
            {
                Console.Clear();

                Console.WriteLine("------------------------------   Menu   -------------------------------");

                Console.WriteLine("#######################################################################");
                Console.WriteLine("#  1 - Funcionarios                                                   #");
                Console.WriteLine("#  0 - Sair                                                           #");
                Console.WriteLine("#######################################################################");

                Console.WriteLine("Insira uma opção");
                response = Convert.ToInt32(Console.ReadLine());
                switch (response)
                {
                    case 1:
                        MenuEmployee.Menu(sessionUser);
                        break;
                    case 2:
                        ScnLogin.Login();
                        break;
                    case 3:
                        ScnLogin.Login();
                        break;
                    case 4:
                        ScnLogin.Login();
                        break;
                    default:
                        Console.Clear();
                        break;
                }
            }
        }
    }
}
