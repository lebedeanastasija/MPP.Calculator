using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using CalculatorConsoleClient.ServiceReference1;

namespace TestConsoleApplication
{

    class Program
    {
        private const int INIT_ACTION = -1;
        private const int EXIT_ACTION = 0;
        private const int ADD_ACTION = 1;
        private const int SUB_ACTION = 2;
        private const int MUL_ACTION = 3;
        private const int DIV_ACTION = 4;
        private const int SQRT_ACTION = 5;
        private static List<int> actions = new List<int>();
        private static ServiceClient client;
        static void Main(string[] args)
        {
            client = new ServiceClient();

            InitActionsList();

            int action = INIT_ACTION;
            double firstOperand = 0;
            double secondOperand = 0;
            double result = 0;
            bool successfulCalculation;

            Console.WriteLine("HELLO, I'M CALCULATOR. LET'S CALC SOMETHING! :)\n");
            while (action != EXIT_ACTION)
            {
                ShowMenu();

                if ((int.TryParse(Console.ReadLine(), out action)) && actions.Contains(action))
                {
                    if (action != EXIT_ACTION)
                    {
                        Console.Write("first operand: ");
                        if (!double.TryParse(Console.ReadLine(), out firstOperand))
                        {
                            ShowError("Value is not correct :(");
                            action = INIT_ACTION;
                            continue;
                        }

                        if (action != SQRT_ACTION)
                        {
                            Console.Write("second operand: ");
                            if (!double.TryParse(Console.ReadLine(), out secondOperand))
                            {
                                ShowError("Value is not correct :(");
                                action = INIT_ACTION;
                                continue;
                            }
                        }

                        if (action == SQRT_ACTION)
                        {
                            successfulCalculation = HandleAction(action, firstOperand, out result);
                        }
                        else
                        {
                            successfulCalculation = HandleAction(action, firstOperand, secondOperand, out result);
                        }

                        if (successfulCalculation)
                        {
                            ShowResult(result);
                        }
                    }
                }
                else
                {
                    ShowError("Command is unknonw :(");
                    action = INIT_ACTION;
                    continue;
                }
            }

            client.Close();
            Console.WriteLine("\nBye, press any key to exit...");
            Console.Read();
        }

        public static void ShowMenu()
        {
            Console.Write(
                "Write the number of command to do:\n\n" +
                "0 - Exit\n" +
                "1 - Add\n" +
                "2 - Substract\n" +
                "3 - Multiply\n" +
                "4 - Divide\n" +
                "5 - Sqrt\n\n" +
                "----------------------------------\n" +
                "command: ");
        }

        public static void ShowResult(double result)
        {
            Console.WriteLine(
                "----------------------------------\n" +
                "RESULT: " + result.ToString() +
              "\n----------------------------------\n");
        }

        public static void ShowError(string message)
        {
            Console.WriteLine(
                "\n----------------------------------\n" +
                  "ERROR: " + message +
                "\n----------------------------------\n");
        }

        public static void InitActionsList()
        {
            actions.Add(EXIT_ACTION);
            actions.Add(ADD_ACTION);
            actions.Add(SUB_ACTION);
            actions.Add(MUL_ACTION);
            actions.Add(DIV_ACTION);
            actions.Add(SQRT_ACTION);
        }

        public static bool HandleAction(int action, double firstOperand, double secondOperand, out double result)
        {
            bool successfulCalculation = true;
            result = 0;
            switch (action)
            {
                case ADD_ACTION:
                    result = client.Add(firstOperand, secondOperand);
                    break;
                case SUB_ACTION:
                    result = client.Substract(firstOperand, secondOperand);
                    break;
                case MUL_ACTION:
                    result = client.Multiply(firstOperand, secondOperand);
                    break;
                case DIV_ACTION:
                    try
                    {
                        result = client.Divide(firstOperand, secondOperand);
                    }
                    catch (FaultException<DividedByZeroFault> ex)
                    {
                        ShowError(ex.Detail.errorMessage);
                        successfulCalculation = false;
                    }
                    break;
            }
            return successfulCalculation;
        }

        public static bool HandleAction(int action, double firstOperand, out double result)
        {
            bool successfulCalculation = true;
            try
            {
                result = client.Sqrt(firstOperand);
            }
            catch (FaultException<InvalidRootOperandFault> ex)
            {
                ShowError(ex.Detail.errorMessage);
                successfulCalculation = false;
            }
            result = 0;
            return successfulCalculation;
        }
    }
}
