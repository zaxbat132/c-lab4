// using System;
// using System.Collections.Generic;
// using System.IO;
// using System.Text;
//
// namespace Lab4.CollectionsApp
// {
//     internal static class Program
//     {
//         private static void Main()
//         {
//             Console.InputEncoding = Encoding.UTF8;
//             Console.OutputEncoding = Encoding.UTF8;
//             try
//             {
//                 while (true)
//                 {
//                     Console.WriteLine("\nЛабораторная работа 4. Задания 1-5");
//                     Console.WriteLine("1 - Удаление элементов List\n2 - LinkedList в обратном порядке");
//                     Console.WriteLine("3 - Закупки учебных заведений\n4 - Звонкие согласные\n5 - Логины учеников\n0 - Выход");
//                     int task = ReadInt("Задание", 0, 5);
//                     if (task == 0)
//                     {
//                         return;
//                     }
//
//                     Console.WriteLine("1 - Задача 1\n0 - Назад");
//                     if (ReadInt("Номер", 0, 1) == 0)
//                     {
//                         continue;
//                     }
//
//                     try
//                     {
//                         RunTask(task);
//                     }
//                     catch (EndOfStreamException)
//                     {
//                         throw;
//                     }
//                     catch (IOException exception)
//                     {
//                         Console.WriteLine("Ошибка файла: " + exception.Message);
//                     }
//                     catch (UnauthorizedAccessException exception)
//                     {
//                         Console.WriteLine("Нет доступа к файлу: " + exception.Message);
//                     }
//                 }
//             }
//             catch (EndOfStreamException)
//             {
//                 Console.WriteLine("\nВвод завершён.");
//             }
//         }
//
//         private static void RunTask(int task)
//         {
//             switch (task)
//             {
//                 case 1:
//                     RunListTask();
//                     break;
//                 case 2:
//                     RunLinkedListTask();
//                     break;
//                 case 3:
//                     RunCompaniesTask();
//                     break;
//                 case 4:
//                     RunTextTask();
//                     break;
//                 case 5:
//                     RunLoginsTask();
//                     break;
//             }
//         }
//
//         private static void RunListTask()
//         {
//             int count = ReadInt("Количество элементов", 0, 10000);
//             List<int> numbers = new List<int>();
//             for (int i = 0; i < count; i++)
//             {
//                 numbers.Add(ReadInt("Элемент " + (i + 1), int.MinValue, int.MaxValue));
//             }
//
//             int element = ReadInt("Удаляемый элемент E", int.MinValue, int.MaxValue);
//             Tasks.RemoveElements(numbers, element);
//             Console.Write("Результат: ");
//             PrintValues(numbers);
//         }
//
//         private static void RunLinkedListTask()
//         {
//             int count = ReadInt("Количество элементов", 1, 10000);
//             LinkedList<int> numbers = new LinkedList<int>();
//             for (int i = 0; i < count; i++)
//             {
//                 numbers.AddLast(ReadInt("Элемент " + (i + 1), int.MinValue, int.MaxValue));
//             }
//
//             Console.WriteLine("Обратный порядок: " + Tasks.GetReverseOrder(numbers));
//         }
//
//         private static void RunCompaniesTask()
//         {
//             int count = ReadInt("Количество фирм", 0, 1000);
//             List<string> names = new List<string>();
//             HashSet<string> companies = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
//             for (int i = 0; i < count; i++)
//             {
//                 while (true)
//                 {
//                     Console.Write("Фирма " + (i + 1) + ": ");
//                     string name = ReadLine().Trim();
//                     if (name.Length > 0 && companies.Add(name))
//                     {
//                         names.Add(name);
//                         break;
//                     }
//
//                     Console.WriteLine("Введите непустое название без повторов.");
//                 }
//             }
//
//             for (int i = 0; i < names.Count; i++)
//             {
//                 Console.WriteLine((i + 1) + " - " + names[i]);
//             }
//
//             int schoolCount = ReadInt("Количество учебных заведений", 1, 1000);
//             List<HashSet<string>> purchases = new List<HashSet<string>>();
//             for (int i = 0; i < schoolCount; i++)
//             {
//                 Console.WriteLine("Учебное заведение " + (i + 1));
//                 int purchaseCount = ReadInt("Сколько фирм использовано", 0, names.Count);
//                 HashSet<string> school = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
//                 while (school.Count < purchaseCount)
//                 {
//                     int index = ReadInt("Номер фирмы", 1, names.Count) - 1;
//                     if (!school.Add(names[index]))
//                     {
//                         Console.WriteLine("Эта фирма уже указана. Выберите другую.");
//                     }
//                 }
//
//                 purchases.Add(school);
//             }
//
//             HashSet<string> everySchool;
//             HashSet<string> anySchool;
//             HashSet<string> noSchool;
//             Tasks.FindCompanies(companies, purchases, out everySchool, out anySchool, out noSchool);
//             PrintCompanies("Закупали все: ", names, everySchool);
//             PrintCompanies("Закупало хотя бы одно: ", names, anySchool);
//             PrintCompanies("Не закупал никто: ", names, noSchool);
//         }
//
//         private static void RunTextTask()
//         {
//             int count = ReadInt("Количество строк текста", 0, 1000);
//             string[] lines = new string[count];
//             for (int i = 0; i < count; i++)
//             {
//                 Console.Write("Строка " + (i + 1) + " (можно пустую): ");
//                 lines[i] = ReadLine();
//             }
//
//             string path = CreateDataPath("text.txt");
//             Tasks.FillTextFile(path, lines);
//             string letters = Tasks.FindVoicedConsonants(path);
//             Console.WriteLine("Файл: " + path);
//             Console.WriteLine("Звонкие согласные: " + (letters.Length == 0 ? "нет" : letters));
//         }
//
//         private static void RunLoginsTask()
//         {
//             int count = ReadInt("Количество учеников", 0, 100);
//             string[] students = new string[count];
//             Console.WriteLine("Фамилия и имя: один пробел; до 20 и 15 символов соответственно.");
//             Console.WriteLine("Допустимы буквы и одиночный дефис внутри слова. Регистр фамилий учитывается.");
//             for (int i = 0; i < count; i++)
//             {
//                 while (true)
//                 {
//                     Console.Write("Ученик " + (i + 1) + ": ");
//                     string line = ReadLine();
//                     string surname;
//                     string name;
//                     if (Tasks.TryParseStudent(line, out surname, out name))
//                     {
//                         students[i] = line;
//                         break;
//                     }
//
//                     Console.WriteLine("Неверный формат. Пример: Иванова Мария");
//                 }
//             }
//
//             string path = CreateDataPath("students.txt");
//             Tasks.FillStudentsFile(path, students);
//             List<string> logins = Tasks.CreateLogins(path);
//             Console.WriteLine("Файл: " + path);
//             Console.WriteLine("Логины:");
//             foreach (string login in logins)
//             {
//                 Console.WriteLine(login);
//             }
//
//             if (logins.Count == 0)
//             {
//                 Console.WriteLine("(нет учеников)");
//             }
//         }
//
//         private static string CreateDataPath(string fileName)
//         {
//             string directory = Path.Combine(Environment.CurrentDirectory, "lab4_data_" + Guid.NewGuid().ToString("N"));
//             Directory.CreateDirectory(directory);
//             return Path.Combine(directory, fileName);
//         }
//
//         private static void PrintValues(List<int> numbers)
//         {
//             if (numbers.Count == 0)
//             {
//                 Console.WriteLine("(пустой список)");
//                 return;
//             }
//
//             for (int i = 0; i < numbers.Count; i++)
//             {
//                 Console.Write((i == 0 ? "" : " ") + numbers[i]);
//             }
//
//             Console.WriteLine();
//         }
//
//         private static void PrintCompanies(string title, List<string> order, HashSet<string> selected)
//         {
//             Console.Write(title);
//             bool first = true;
//             foreach (string name in order)
//             {
//                 if (selected.Contains(name))
//                 {
//                     Console.Write((first ? "" : ", ") + name);
//                     first = false;
//                 }
//             }
//
//             Console.WriteLine(first ? "нет" : "");
//         }
//
//         private static int ReadInt(string prompt, int minimum, int maximum)
//         {
//             while (true)
//             {
//                 Console.Write(prompt + " (" + minimum + ".." + maximum + "): ");
//                 int value;
//                 if (int.TryParse(ReadLine(), out value) && value >= minimum && value <= maximum)
//                 {
//                     return value;
//                 }
//
//                 Console.WriteLine("Введите целое число в указанном диапазоне.");
//             }
//         }
//
//         private static string ReadLine()
//         {
//             string value = Console.ReadLine();
//             if (value == null)
//             {
//                 throw new EndOfStreamException();
//             }
//
//             return value;
//         }
//     }
// }
