using System;

namespace Лаб._2
{
    class Program
    {
        static void Menu(DataBase current)
        {
            Console.WriteLine("F - find; A - add; D - delete; E - edit");
            string str = Console.ReadLine();
            switch (str)
            {
                case "F":
                    Console.WriteLine("Key: ");
                    int key = Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine("Ok... Answer:");
                    Console.WriteLine("Value: " + current.Find(key, false, false, "", false, 0));
                    break;
                case "A":
                    Console.WriteLine("Key: ");
                    int key_2 = Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine("Value: ");
                    string res_2 = Console.ReadLine();
                    if(current.Find(key_2, false, false, res_2, true, key_2) != null)
                    {
                        Console.WriteLine("Recorder updated...");
                    }
                    else
                    {
                        Console.WriteLine("Recorder addes...");
                    }
                    break;
                case "D":
                    Console.WriteLine("Key: ");
                    int key_3 = Convert.ToInt32(Console.ReadLine());
                    if (current.Find(key_3, true, false, "", false, key_3) != null)
                    {
                        Console.WriteLine("Recorder deleted...");
                    }
                    else
                    {
                        Console.WriteLine("Recorder none...");
                    }
                    break;
                case "E":
                    Console.WriteLine("Key: ");
                    int key_4 = Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine("Value: ");
                    string res_4 = Console.ReadLine();
                    if (current.Find(key_4, false, true, res_4, false, key_4) != null)
                        Console.WriteLine("Recorder updated...");
                    break;
                default:
                    break;
            }
            Menu(current);
        }

        static void Main(string[] args)
        {
            DataBase current = new DataBase("index_area.txt");
            Menu(current);
        }
    }
}
