using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using work.Data;

namespace work.Screens
{
    class MenuEmployee
    {
        public static void Menu(Employee sessionUser)
        {
            int response = -1;
            Console.Clear();
            Console.WriteLine("--------------------------   Funcionários   ---------------------------");
            Console.WriteLine("#######################################################################");
            Console.WriteLine("#  1 - Ver lista                                                      #");
            Console.WriteLine("#  2 - Gravar Funcionários                                            #");
            Console.WriteLine("#  3 - Procurar                                                       #");
            Console.WriteLine("#  4 - Editar                                                         #");
            Console.WriteLine("#  5 - Editar                                                         #");
            Console.WriteLine("#  6 - Limpar Lista                                                   #");
            Console.WriteLine("#  0 - Sair                                                           #");
            Console.WriteLine("#######################################################################");

            EntityEmployees listEntityEmployees = new EntityEmployees();
            listEntityEmployees.List();
            
            while (response != 0)
            {
                Console.WriteLine("Insira uma opção");
                response = Convert.ToInt32(Console.ReadLine());
                switch (response)
                {
                    case 1:
                        ListEmployees(listEntityEmployees);
                        break;
                    case 2:
                        CreateEmployee(listEntityEmployees, sessionUser);
                        break;
                    case 3:
                        ScnLogin.Login();
                        break;
                    case 4:
                        ScnLogin.Login();
                        break;
                    default:
                        break;
                }
            }
        }

        private static void ListEmployees(EntityEmployees listEntityEmployees)
        {
            Console.WriteLine(listEntityEmployees.ToString());
        }

        private static void CreateEmployee(EntityEmployees listEntityEmployees, Employee sessionUser)
        {
            Console.WriteLine("Insira o Primeiro Nome do Funcionário");
            string firstName = Console.ReadLine();
            while (string.IsNullOrEmpty(firstName))
            {
                Console.WriteLine("Insira o Primeiro Nome do Funcionário novamente");
                firstName = Console.ReadLine();
            }

            Console.WriteLine("Insira o Segundo Nome do Funcionário");
            string lastName = Console.ReadLine();
            while (string.IsNullOrEmpty(firstName ))
            {
                Console.WriteLine("Insira o Segundo Nome do Funcionário novamente");
                lastName = Console.ReadLine();
            }

            Console.WriteLine("Insira o Email do Funcionário");
            string email = Console.ReadLine();
            while (string.IsNullOrEmpty(email ))
            {
                Console.WriteLine("Insira o Email do Funcionário novamente");
                email = Console.ReadLine();
            }


            Console.WriteLine("Insira a Palavra-Passe do Funcionário");
            string passWord = Console.ReadLine();
            while (string.IsNullOrEmpty(passWord ))
            {
                Console.WriteLine("Insira o Email do Funcionário novamente");
                passWord = Console.ReadLine();
            }

            Console.WriteLine("Insira o Tipo do Funcionário (1) - Gerente | (2) - Normal | (3) - Repositor");
            int type = Convert.ToInt32(Console.ReadLine());
            List<int> types = new List<int>{ 1, 2, 3 };
            while (type  == 0 || !types.Contains(type))
            {
                Console.WriteLine("Insira o Tipo do Funcionário (1) - Gerente | (2) - Normal | (3) - Repositor novamente");
                type = Convert.ToInt32(Console.ReadLine());
            }

            Console.WriteLine("Insira a Morada do Funcionário");
            string address = Console.ReadLine();

            Console.WriteLine("Insira a Número de telefone do Funcionário");
            long contact = Convert.ToInt64(Console.ReadLine());

            Console.WriteLine("Insira a Data de nascimento do Funcionário");
            DateTime birthDate = Convert.ToDateTime(Console.ReadLine());

            Console.WriteLine("Insira a Data de quando o Funcionário começou a trabalhar");
            DateTime initWork = Convert.ToDateTime(Console.ReadLine());

            Console.WriteLine("Insira o Salario do Funcionário");
            int salary = Convert.ToInt32(Console.ReadLine());


            listEntityEmployees.AddEmployee(email, type, firstName, lastName, address, contact, birthDate, initWork, salary, passWord, sessionUser.id);

    
        }
    }
}
