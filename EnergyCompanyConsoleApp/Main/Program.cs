using EnergyCompanyConsoleApp.Controller;
using EnergyCompanyConsoleApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EnergyCompanyConsoleApp
{
    internal class Program
    {
        static AppController _appController;
        static void Main(string[] args)
        {
            _appController = new AppController();

            MainMenu();

            Console.WriteLine("OK, EXITING !");
            Thread.Sleep(600);

        }


        static void MainMenu()
        {

            int opitions;
            bool exit = false;

            do
            {
                Console.WriteLine(" #### MAIN MENU ####");
                Console.WriteLine(" #### SELECT AN OPTION:(TYPING THE NUMBER OF THE OPTION) ####");
                Console.WriteLine(" 1 - NEW ENDPOINT: ");
                Console.WriteLine(" 2 - EDIT AN ENDPOINT: ");
                Console.WriteLine(" 3 - DELETE AN ENDPOINT: ");
                Console.WriteLine(" 4 - SHOW ALL ENDPOINTS: ");
                Console.WriteLine(" 5 - FIND ENDPOINT BY SERIAL:");
                Console.WriteLine(" 6 - EXIT ");

                try
                {
                    if (int.TryParse(Console.ReadLine(), out opitions))
                    {
                        switch (opitions)

                        {
                            case 1:
                                NewEndPointForm();
                                break;
                            case 2:
                                EditEndPoint();
                                break;
                            case 3:
                                DeleteEndPoint();
                                break;
                            case 4:
                                ListEndPoint();
                                break;
                            case 5:
                                FindEndpoint();
                                break;
                            case 6:
                                exit = ShowConfirmation("CONFIRM EXIT ? ");
                                break;
                            default:
                                InvalidOption();
                                break;
                        }
                    }
                    else
                    {
                        InvalidOption();
                    }

                }
                catch
                {
                    Console.WriteLine("SORRY, PLEASE TRY AGAIN !");
                }

            } while (!exit);

        }

        #region Methods
        static void NewEndPointForm()
        {
            Console.Clear();
            string error;
            List<string> form = new List<string>();
            bool exitmeter = false;
            int opition;
            Console.WriteLine("### NEW ENDPOINT ###");
            Console.WriteLine("PLEASE, SET THE ENDPOINT SERIAL NUMBER: ");
            form.Add(Console.ReadLine());

            do
            {
                Console.WriteLine("PLEASE, SELECT THE METER MODEL: ");
                Console.WriteLine("1 - NSX1P2W ");
                Console.WriteLine("2 - NSX1P3W ");
                Console.WriteLine("3 - NSX2P3W  ");
                Console.WriteLine("4 - NSX3P4W ");

                if (int.TryParse(Console.ReadLine(), out opition))
                {
                    switch (opition)
                    {
                        case 1:
                            form.Add("16");
                            exitmeter = true;
                            break;
                        case 2:
                            form.Add("17");
                            exitmeter = true;
                            break;
                        case 3:
                            form.Add("18");
                            exitmeter = true;
                            break;
                        case 4:
                            form.Add("19");
                            exitmeter = true;
                            break;
                        default:
                            InvalidOption();
                            break;
                    }
                }
                else
                {
                    InvalidOption();
                }

            } while (!exitmeter);

            Console.WriteLine("PLEASE, SET THE METER NUMBER: ");
            form.Add(Console.ReadLine());

            Console.WriteLine("PLEASE, SET THE FIRMWARE VERSION : ");
            form.Add(Console.ReadLine());

            Console.WriteLine("PLEASE, SELECT SWITCH STATE: ");

            form.Add(SwitchStateForm());
           
            _appController.NewEndpoin(out error, form);

            if (string.IsNullOrEmpty(error))
            {
                Console.WriteLine(" #### ENDPOINT HAS BEEN SAVED! ####");


            }
            else
            {
                Console.WriteLine(" #### ENDPOINT HAS BEEN NOT SAVED! ####");
                Console.WriteLine(error);

            }
            Preskeytocontinue();
        }

        static void ListEndPoint()
        {
            bool haveEndpoints = false;

            Console.Clear();
            foreach (var x in _appController.ListAll())
            {
                EndpointPrint(x);
                haveEndpoints = true;
            }

            if (!haveEndpoints)
            {
                Console.WriteLine("THERE IS NOTHING TO SHOW HERE!!");
            }

            Preskeytocontinue();
        }

        static void FindEndpoint()
        {
            Console.Clear();

            SerialNumberDialog(out string erro);

            if (string.IsNullOrEmpty(erro))
            {
                EndpointPrint(_appController.SelectedEndpoint);
            }
            else
            {
                Console.WriteLine(erro);
            }

            Preskeytocontinue();
        }

        static void DeleteEndPoint()
        {
            Console.Clear();
            SerialNumberDialog(out string erro);

            if (string.IsNullOrEmpty(erro))
            {
                EndpointPrint(_appController.SelectedEndpoint);
                if (ShowConfirmation("CONTINUE TO DELETE THE ENDPOINT ?"))
                {
                    _appController.DeleteEndPoint();
                    Console.WriteLine("ENDPOINT HAS BEEN DELETED!!");
                }
               
            }
            else
            {
                Console.WriteLine(erro);
            }
            Preskeytocontinue();
        }

        static void EditEndPoint()
        {
            Console.Clear();

            SerialNumberDialog(out string erro);

            if (string.IsNullOrEmpty(erro))
            {
                _appController.EditEndPoint(SwitchStateForm());
                Console.WriteLine("ENDPOINT HAS BEEN EDITED!!");
            }
            else
            {
                Console.WriteLine(erro);
            }
            Preskeytocontinue();
        }


  

        #endregion

        #region Dialogs


        static void EndpointPrint(Endpoint x)
        {
            Console.WriteLine($"--------- ENDPOINT {x.SerialNumber}  -----------");
            Console.WriteLine($"Serial Number: {x.SerialNumber}");
            Console.WriteLine($"Meter Model: {(MeterModelEnum)x.MeterModelId}");
            Console.WriteLine($"Meter Number: {x.MeterNumber}");
            Console.WriteLine($"Meter firmware version: {x.MeterFirmwareVersion}");
            Console.WriteLine($"Switch State: {(SwitchState)x.SwitchState}");
            Console.WriteLine("------------------------------------------------");
            Console.WriteLine();
        }

        static bool ShowConfirmation(string mensagem)
        {
            Console.WriteLine($" # {mensagem} #");
            Console.WriteLine($" # Y - YES #");
            Console.WriteLine($" # N - NO #");
            string opition = Console.ReadLine();

            return (opition.ToUpper().Equals("Y") || opition.ToUpper().Equals("Y") ? true : false);

        }

        static void InvalidOption()
        {
            Console.WriteLine("INVALID OPITION, PLEASE TRY AGAIN!");
        }

        static void Preskeytocontinue()
        {
            Console.WriteLine(" #### PRESS ANY KEY TO CONTINUE! ####");
            Console.ReadKey();
            Console.Clear();
        }

        static void SerialNumberDialog(out string erro)
        {
            Console.WriteLine("PLEASE, ENTER A THE ENDPOINT SERIAL NUMBER: ");
            _appController.FindEndpoint(Console.ReadLine(), out erro);
        }

        static string SwitchStateForm()
        {
            string retorno = "";
            int opition;
            bool exit  = false;

            do
            {
                Console.WriteLine("PLEASE, SELECT THE METER MODEL: ");
                Console.WriteLine("1 - DISCONNECTED ");
                Console.WriteLine("2 - CONNECTED ");
                Console.WriteLine("3 - ARMED  ");

                if (int.TryParse(Console.ReadLine(), out opition))
                {
                    switch (opition)
                    {
                        case 1:
                            retorno = "0";
                            exit = true;
                            break;
                        case 2:
                            retorno = "1";
                            exit = true;
                            break;
                        case 3:
                            retorno = "2";
                            exit = true;
                            break;
                        default:
                            InvalidOption();
                            break;
                    }
                }
                else
                {
                    InvalidOption();
                }

            } while (!exit);

            return retorno;
        }

        #endregion
    }
}
