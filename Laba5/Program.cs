using System;
using System.Collections.Generic;

namespace Laba5
{

    public class Program
    {
        public static void Main()
        {
            Wallet wallet = new Wallet();
            
            wallet.CreateCurrency();
            wallet.SwithCase();            
        }
    }

    public class Wallet
    {
        Bank bankIT = new Bank();
        MyPrinter myPrinter = new MyPrinter();

        public List<string> currencyList = new List<string>();
        public List<double> countList = new List<double>();
        public List<double> cursList = new List<double>();

        public void SwithCase()
        {
            Console.WriteLine("1. Внести валюту\n2. Вывести валюту\n3. Баланс\n4. О кошельке\n5. Перевод в одну валюту");
            int s = Convert.ToInt32(Console.ReadLine());
            bool end = false;
            switch (s)
            {
                case 1:
                    {
                        Console.Clear();
                        Console.WriteLine("Название валюты:");
                        string currency = Console.ReadLine();
                        Console.WriteLine("Сумма пополнения:");
                        double count = Convert.ToDouble(Console.ReadLine());
                        AddMoney(currency, count);
                        break;
                    }
                case 2:
                    {
                        Console.Clear();
                        Console.WriteLine("Название валюты:");
                        string currency = Console.ReadLine();
                        Console.WriteLine("Сумма вывода:");
                        double count = Convert.ToDouble(Console.ReadLine());
                        RemoveMoney(currency, count);
                        break;
                    }
                case 3:
                    {
                        Console.Clear();
                        Console.WriteLine("Название валюты:");
                        string currency = Console.ReadLine();
                        GetMoney(currency);
                        break;
                    }
                case 4:
                    {
                        Console.Clear();
                        ToString();
                        break;
                    }
                case 5:
                    {
                        Console.Clear();
                        Console.WriteLine("Название валюты:");
                        string currency = Console.ReadLine();
                        GetTotalMoney(currency);
                        break;
                    }
                default:
                    {
                        Console.Clear();
                        end = true;
                        break;
                    }
            }
            if (!end)
            {
                SwithCase();
            }
        }

        public void CreateCurrency()
        {
            currencyList.Add("RUB");
            currencyList.Add("USD");
            currencyList.Add("EUR");
            currencyList.Add("GBP");
            countList.Add(0);
            countList.Add(0);
            countList.Add(0);
            countList.Add(0);
            cursList.Add(1);
            cursList.Add(70);
            cursList.Add(80);
            cursList.Add(90);
        }

        public void AddMoney(string currency, double count)
        {
            if (count < 0)
            {
                Console.WriteLine("Введите сумму больше 0!");
                return;
            }
            if (currencyList.Contains(currency))
            {
                int index = currencyList.FindIndex(x => x == currency);
                countList[index] += count;
                myPrinter.Print("Add", currency, countList[index]);
            }
            else
            {
                Console.WriteLine("Валюта отсутствует в списке, она будет добавлена");
                currencyList.Add(currency);
                countList.Add(count);
                Console.WriteLine("Курс указанной валюты:");
                cursList.Add(Convert.ToInt32(Console.ReadLine()));
                myPrinter.Print("Add", currency, countList.Count - 1);
            }
        }

        public void RemoveMoney(string currency, double count)
        {
            if (count < 0)
            {
                Console.WriteLine("Введите сумму больше 0!");
                return;
            }
            if (currencyList.Contains(currency))
            {
                int index = currencyList.FindIndex(x => x == currency);
                if (countList[index] >= count)
                {
                    countList[index] -= count;
                    myPrinter.Print("Remove", currency, countList[index]);
                }
                else
                    Console.WriteLine("Нет средств");
            }
            else
                Console.WriteLine("Валюты нет в списке!");
        }

        public void GetMoney(string currency)
        {
            if (currencyList.Contains(currency))
            {
                int index = currencyList.FindIndex(x => x == currency);
                Console.WriteLine($"Валюта {currency} = {countList[index]} (курс относительно рубля = {cursList[index]})");
            }
            else
                Console.WriteLine($"Валюта {currency} = 0");
        }

        new public void ToString()
        {
            string result = "{";
            for (int i = 0; i < currencyList.Count; i++)
            {
                if (countList[i] > 0)
                {
                    result += ($" {currencyList[i]} = {countList[i]},");
                }
            }
            if (result[result.Length - 1] == ',')
            {
                result = result.Remove(result.Length - 1);
                result += " ";
            }
            result += "}";
            Console.WriteLine(result);
        }

        public double GetTotalMoney(string currency)
        {
            CursRandom();
            double result = 0;
            if (currencyList.Contains(currency))
            {
                int index = currencyList.FindIndex(x => x == currency);
                for (int i = 0; i < currencyList.Count; i++)
                {
                    result += bankIT.Convert(countList[i], cursList[i], cursList[index]);
                }
                Console.WriteLine($"Общая сумма в {currency} = {result}");
            }
            else
                Console.WriteLine("Валюты нет в списке!");
            return result;
        }

        public void CursRandom()
        {
            for (int i = 1; i < cursList.Count; i++)
            {
                cursList[i] += bankIT.CursRandom(cursList[i]);
            }
        }
    }

    public class Bank
    {
        public double CursRandom(double curs)
        {
            Random rnd = new Random();
            int rndCurs = rnd.Next(1, 10);
            if (rnd.Next(0, 1) == 1)
                rndCurs *= -1;
            return curs * rndCurs / 100;


        }

        public double Convert(double count, double currency1_curs, double currency2_curs)
        {
            double result = count * currency1_curs / currency2_curs;
            return Math.Round(result, 3);
        }
    }

    public class MyPrinter
    {
        public void Print(string operation, string currency, double amount)
        {
            Console.WriteLine($"{operation} {currency} = {amount}");
        }
    }
}
