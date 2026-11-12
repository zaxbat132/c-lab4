using System;
using System.IO;
using System.Text;

namespace Lab4.TimeApp
{
    internal static class Program
    {
        private static void Main()
        {
            Console.InputEncoding = Encoding.UTF8;
            Console.OutputEncoding = Encoding.UTF8;
            try
            {
                while (true)
                {
                    Console.WriteLine("\nЛабораторная работа 4. Задания 6-7");
                    Console.WriteLine("6 - Класс Time\n7 - Перегрузка операций\n0 - Выход");
                    int task = ReadInt("Задание", 0, 7);
                    if (task == 0)
                    {
                        return;
                    }

                    if (task != 6 && task != 7)
                    {
                        Console.WriteLine("Выберите 6, 7 или 0.");
                        continue;
                    }

                    Console.WriteLine("1 - Задача 1\n0 - Назад");
                    if (ReadInt("Номер", 0, 1) == 0)
                    {
                        continue;
                    }

                    if (task == 6)
                    {
                        RunClassTask();
                    }
                    else
                    {
                        RunOperatorsTask();
                    }
                }
            }
            catch (EndOfStreamException)
            {
                Console.WriteLine("\nВвод завершён.");
            }
        }

        private static void RunClassTask()
        {
            Time empty = new Time();
            Console.WriteLine("Конструктор без параметров: " + empty);
            Time time = ReadTime();
            Console.WriteLine("Конструктор с параметрами: " + time);
            Time copy = new Time(time);
            Console.WriteLine("Конструктор копирования: " + copy);
            Console.WriteLine("Изменение копии через свойства:");
            copy.Hours = (byte)ReadInt("Новые часы копии", 0, 23);
            copy.Minutes = (byte)ReadInt("Новые минуты копии", 0, 59);
            Console.WriteLine("Копия: " + copy + "; исходный объект: " + time);
            uint minutes = ReadUnsigned("Сколько минут добавить");
            Console.WriteLine("Результат AddMinutes: " + time.AddMinutes(minutes));
            Console.WriteLine("Исходный объект после метода: " + time);
        }

        private static void RunOperatorsTask()
        {
            Time time = ReadTime();
            uint minutes = ReadUnsigned("Количество минут для операций");
            Console.WriteLine("Исходное время: " + time);
            Time copy = new Time(time);
            Time result = ++copy;
            Console.WriteLine("++t: результат " + result + "; t = " + copy);
            copy = new Time(time);
            result = copy++;
            Console.WriteLine("t++: результат " + result + "; t = " + copy);
            copy = new Time(time);
            result = --copy;
            Console.WriteLine("--t: результат " + result + "; t = " + copy);
            copy = new Time(time);
            result = copy--;
            Console.WriteLine("t--: результат " + result + "; t = " + copy);
            byte hours = (byte)time;
            bool bothNonzero = time;
            Console.WriteLine("Явное приведение к byte: " + hours);
            Console.WriteLine("Неявное приведение к bool: " + bothNonzero);
            Console.WriteLine("t + n: " + (time + minutes));
            Console.WriteLine("n + t: " + (minutes + time));
            Console.WriteLine("t - n: " + (time - minutes));
            Console.WriteLine("n - t: " + (minutes - time));
            Console.WriteLine("Время нормализуется в пределах суток (00:00..23:59).");
            Console.WriteLine("В n - t число n означает минуты от полуночи.");
        }

        private static Time ReadTime()
        {
            byte hours = (byte)ReadInt("Часы", 0, 23);
            byte minutes = (byte)ReadInt("Минуты", 0, 59);
            return new Time(hours, minutes);
        }

        private static int ReadInt(string prompt, int minimum, int maximum)
        {
            while (true)
            {
                Console.Write(prompt + " (" + minimum + ".." + maximum + "): ");
                int value;
                if (int.TryParse(ReadLine(), out value) && value >= minimum && value <= maximum)
                {
                    return value;
                }

                Console.WriteLine("Введите целое число в указанном диапазоне.");
            }
        }

        private static uint ReadUnsigned(string prompt)
        {
            while (true)
            {
                Console.Write(prompt + " (0..4294967295): ");
                uint value;
                if (uint.TryParse(ReadLine(), out value))
                {
                    return value;
                }

                Console.WriteLine("Введите целое неотрицательное число не больше 4294967295.");
            }
        }

        private static string ReadLine()
        {
            string value = Console.ReadLine();
            if (value == null)
            {
                throw new EndOfStreamException();
            }

            return value;
        }
    }
}
