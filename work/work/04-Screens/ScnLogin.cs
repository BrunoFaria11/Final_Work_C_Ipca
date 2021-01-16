using System;
using System.Collections.Generic;
using System.Text;
using work.Data;
using work.Screens;

namespace work.Screens
{
    class ScnLogin
    {
        public static void Login()
        {
            try
            {
                Console.Clear();

                string email = "", password = "";
                int response = -1;

                Employee sessionUser = null;

                while (response != 0)
                {
                    Console.WriteLine("------------------------------   Login   ------------------------------");
                    Console.WriteLine("Insira o seu Email");
                    email = Console.ReadLine();
                    Console.WriteLine("Insira a sua Palavra-Passe");
                    password = Console.ReadLine();

                    EntityEmployees login = new EntityEmployees();

                    sessionUser = login.MakeLogin(email, password);

                    if (sessionUser != null)
                    {
                        response = 0;
                        if(sessionUser.type == Enums.EnumTypeEmployee.Gerente)
                        {
                            InitMenu.MenuGerente(sessionUser);
                        }
                        else if (sessionUser.type == Enums.EnumTypeEmployee.Caixa)
                        {
                            InitMenu.MenuCaixa(sessionUser);
                        }
                        if (sessionUser.type == Enums.EnumTypeEmployee.Repositor)
                        {
                            InitMenu.MenuRepositor(sessionUser);
                        }
                    }
                    else
                    {
                        Console.Clear();

                        Console.WriteLine("Email ou Palavra-Passe incorretos");
                        Console.WriteLine("#######################################################################");
                        Console.WriteLine("#  1 - Tentar login novamente                                         #");
                        Console.WriteLine("#  0 - Sair                                                           #");
                        Console.WriteLine("#######################################################################");
                        Console.WriteLine("Insira uma opção");
                        response = Convert.ToInt32(Console.ReadLine());
                        Console.Clear();
                    }
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
