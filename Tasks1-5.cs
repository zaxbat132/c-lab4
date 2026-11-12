// using System;
// using System.Collections.Generic;
// using System.Globalization;
// using System.IO;
// using System.Text;
//
// namespace Lab4.CollectionsApp
// {
//     public static class Tasks
//     {
//         public static void RemoveElements(List<int> numbers, int element)
//         {
//             int writeIndex = 0;
//             for (int readIndex = 0; readIndex < numbers.Count; readIndex++)
//             {
//                 if (numbers[readIndex] != element)
//                 {
//                     numbers[writeIndex] = numbers[readIndex];
//                     writeIndex++;
//                 }
//             }
//
//             numbers.RemoveRange(writeIndex, numbers.Count - writeIndex);
//         }
//
//         public static string GetReverseOrder(LinkedList<int> numbers)
//         {
//             if (numbers.Count == 0)
//             {
//                 throw new ArgumentException("Список должен быть непустым.", "numbers");
//             }
//
//             StringBuilder result = new StringBuilder();
//             LinkedListNode<int> current = numbers.Last;
//             while (current != null)
//             {
//                 if (result.Length > 0)
//                 {
//                     result.Append(" ");
//                 }
//
//                 result.Append(current.Value);
//                 current = current.Previous;
//             }
//
//             return result.ToString();
//         }
//
//         public static void FindCompanies(HashSet<string> companies,
//             List<HashSet<string>> purchases, out HashSet<string> everySchool,
//             out HashSet<string> anySchool, out HashSet<string> noSchool)
//         {
//             if (purchases.Count == 0)
//             {
//                 throw new ArgumentException("Нужно хотя бы одно учебное заведение.", "purchases");
//             }
//
//             everySchool = new HashSet<string>(companies, companies.Comparer);
//             anySchool = new HashSet<string>(companies.Comparer);
//             foreach (HashSet<string> school in purchases)
//             {
//                 if (!school.IsSubsetOf(companies))
//                 {
//                     throw new ArgumentException("Фирма отсутствует в общем перечне.", "purchases");
//                 }
//
//                 everySchool.IntersectWith(school);
//                 anySchool.UnionWith(school);
//             }
//
//             noSchool = new HashSet<string>(companies, companies.Comparer);
//             noSchool.ExceptWith(anySchool);
//         }
//
//         public static void FillTextFile(string path, string[] lines)
//         {
//             File.WriteAllLines(path, lines, Encoding.UTF8);
//         }
//
//         public static string FindVoicedConsonants(string path)
//         {
//             const string Alphabet = "бвгджзйлмнр";
//             HashSet<char> voiced = new HashSet<char>(Alphabet);
//             HashSet<char> found = new HashSet<char>();
//             using (StreamReader reader = new StreamReader(path, Encoding.UTF8))
//             {
//                 int symbol;
//                 while ((symbol = reader.Read()) != -1)
//                 {
//                     char letter = char.ToLowerInvariant((char)symbol);
//                     if (voiced.Contains(letter))
//                     {
//                         found.Add(letter);
//                     }
//                 }
//             }
//
//             StringBuilder result = new StringBuilder();
//             foreach (char letter in Alphabet)
//             {
//                 if (found.Contains(letter))
//                 {
//                     if (result.Length > 0)
//                     {
//                         result.Append(" ");
//                     }
//
//                     result.Append(letter);
//                 }
//             }
//
//             return result.ToString();
//         }
//
//         public static bool TryParseStudent(string line, out string surname, out string name)
//         {
//             surname = "";
//             name = "";
//             if (line == null)
//             {
//                 return false;
//             }
//
//             string[] parts = line.Split(' ');
//             if (parts.Length != 2 || !IsName(parts[0], 20) || !IsName(parts[1], 15))
//             {
//                 return false;
//             }
//
//             surname = parts[0];
//             name = parts[1];
//             return true;
//         }
//
//         public static void FillStudentsFile(string path, string[] students)
//         {
//             if (students.Length > 100)
//             {
//                 throw new ArgumentException("Допустимо не более 100 учеников.", "students");
//             }
//
//             foreach (string student in students)
//             {
//                 string surname;
//                 string name;
//                 if (!TryParseStudent(student, out surname, out name))
//                 {
//                     throw new ArgumentException("Неверный формат фамилии и имени.", "students");
//                 }
//             }
//
//             using (StreamWriter writer = new StreamWriter(path, false, Encoding.UTF8))
//             {
//                 writer.WriteLine(students.Length);
//                 foreach (string student in students)
//                 {
//                     writer.WriteLine(student);
//                 }
//             }
//         }
//
//         public static List<string> CreateLogins(string path)
//         {
//             Dictionary<string, int> counts = new Dictionary<string, int>(StringComparer.Ordinal);
//             List<string> logins = new List<string>();
//             using (StreamReader reader = new StreamReader(path, Encoding.UTF8))
//             {
//                 int count;
//                 if (!int.TryParse(reader.ReadLine(), out count) || count < 0 || count > 100)
//                 {
//                     throw new InvalidDataException("В первой строке нужно количество от 0 до 100.");
//                 }
//
//                 for (int i = 0; i < count; i++)
//                 {
//                     string surname;
//                     string name;
//                     if (!TryParseStudent(reader.ReadLine(), out surname, out name))
//                     {
//                         throw new InvalidDataException("Неверные данные ученика " + (i + 1) + ".");
//                     }
//
//                     int occurrences;
//                     counts.TryGetValue(surname, out occurrences);
//                     occurrences++;
//                     counts[surname] = occurrences;
//                     string login = surname;
//                     if (occurrences > 1)
//                     {
//                         login += occurrences.ToString(CultureInfo.InvariantCulture);
//                     }
//
//                     logins.Add(login);
//                 }
//
//                 if (reader.ReadLine() != null)
//                 {
//                     throw new InvalidDataException("В файле есть лишние строки.");
//                 }
//             }
//
//             return logins;
//         }
//
//         private static bool IsName(string value, int maximumLength)
//         {
//             if (value.Length == 0 || value.Length > maximumLength)
//             {
//                 return false;
//             }
//
//             for (int i = 0; i < value.Length; i++)
//             {
//                 if (!char.IsLetter(value[i]))
//                 {
//                     if (value[i] != '-' || i == 0 || i == value.Length - 1 || value[i - 1] == '-')
//                     {
//                         return false;
//                     }
//                 }
//             }
//
//             return true;
//         }
//     }
// }
