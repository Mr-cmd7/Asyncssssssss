using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

class Program
{
    static List<object> collection = new List<object>
    {
        "ArrayList", -56109, 3.14, "List", "Sort void", 0.0309, 2.71E-3, 'z', 'F'
    };

    static async Task Main()
    {
        while (true)
        {
            Console.WriteLine("Выберите действие:");
            Console.WriteLine("1 - Вывести информацию о коллекции");
            Console.WriteLine("2 - Вывести сумму всех чисел");
            Console.WriteLine("3 - Вывести строковые и символьные переменные");
            Console.WriteLine("4 - Удалить элементы по индексам");
            Console.WriteLine("5 - Добавить запись по индексу");
            Console.WriteLine("0 - Выйти из программы");

            int choice = int.Parse(Console.ReadLine());

            switch (choice)
            {
                case 1:
                    await PrintCollectionInfoAsync();
                    break;
                case 2:
                    CalculateSumOfNumbers();
                    break;
                case 3:
                    PrintStringsAndChars();
                    break;
                case 4:
                    await DeleteElementsAsync();
                    break;
                case 5:
                    await AddElementAsync();
                    break;
                case 0:
                    return;
                default:
                    Console.WriteLine("Некорректный выбор. Попробуйте снова.");
                    break;
            }
        }
    }

    static async Task PrintCollectionInfoAsync()
    {
        int stringCount = 0;
        int intCount = 0;
        int doubleCount = 0;
        int charCount = 0;

        foreach (var item in collection)
        {
            if (item is string)
                stringCount++;
            else if (item is int)
                intCount++;
            else if (item is double)
                doubleCount++;
            else if (item is char)
                charCount++;
        }

        int totalCount = collection.Count;

        Console.WriteLine($"Количество строк: {stringCount}");
        Console.WriteLine($"Количество целых чисел: {intCount}");
        Console.WriteLine($"Количество дробных чисел: {doubleCount}");
        Console.WriteLine($"Количество символов: {charCount}");
        Console.WriteLine($"Общее количество элементов: {totalCount}");
    }

    static void CalculateSumOfNumbers()
    {
        double sum = 0;
        foreach (var item in collection)
        {
            if (item is int || item is double)
                sum += Convert.ToDouble(item);
        }
        Console.WriteLine($"Сумма всех чисел: {sum}");
    }

    static void PrintStringsAndChars()
    {
        foreach (var item in collection)
        {
            if (item is string || item is char)
                Console.WriteLine(item);
        }
    }

    static async Task DeleteElementsAsync()
    {
        Console.WriteLine("Укажите несколько индексов переменных для удаления (через запятую):");
        string[] indices = Console.ReadLine().Split(',');
        List<int> indicesToRemove = new List<int>();

        foreach (string index in indices)
        {
            if (int.TryParse(index.Trim(), out int idx) && idx >= 0 && idx < collection.Count)
                indicesToRemove.Add(idx);
        }

        indicesToRemove.Sort();
        indicesToRemove.Reverse();

        foreach (int idx in indicesToRemove)
        {
            collection.RemoveAt(idx);
        }
        Console.WriteLine("Удаление завершено.");
    }

    static async Task AddElementAsync()
    {
        Console.WriteLine("Ввести одну запись по индексу (индекс, переменная):");
        string[] input = Console.ReadLine().Split(',');
        if (input.Length == 2 && int.TryParse(input[0].Trim(), out int idx))
        {
            if (idx >= 0 && idx <= collection.Count)
            {
                string value = input[1].Trim();
                if (int.TryParse(value, out int intValue))
                {
                    collection.Insert(idx, intValue);
                }
                else if (double.TryParse(value, out double doubleValue))
                {
                    collection.Insert(idx, doubleValue);
                }
                else
                {
                    collection.Insert(idx, value);
                }
                Console.WriteLine("Добавление завершено.");
            }
            else
            {
                Console.WriteLine("Некорректный индекс.");
            }
        }
        else
        {
            Console.WriteLine("Некорректный формат ввода.");
        }
    }
}
