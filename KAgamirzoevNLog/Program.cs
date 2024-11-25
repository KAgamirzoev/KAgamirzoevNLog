using System;
using System.IO;
using NLog;
class Program
{
    private static readonly Logger logger = LogManager.GetCurrentClassLogger();

    static void Main()
    {
        try
        {
            Console.WriteLine("Выберите вариант (1-4):");
            int choice = int.Parse(Console.ReadLine());

            switch (choice)
            {
                case 1:
                    Variant1();
                    break;
                case 2:
                    Variant2();
                    break;
                case 3:
                    Variant3();
                    break;
                case 4:
                    Variant4();
                    break;
                default:
                    Console.WriteLine("Неверный вариант.");
                    break;
            }
        }
        catch (Exception ex)
        {
            logger.Error(ex, "Произошла ошибка в главном методе.");
            Console.WriteLine("Произошла ошибка: " + ex.Message);
        }
    }

    // Вариант 1: Работа с БД
    static void Variant1()
    {
        try
        {
            Console.WriteLine("Подключение к БД...");
            throw new InvalidOperationException("База данных недоступна.");
        }
        catch (InvalidOperationException ex)
        {
            logger.Error(ex, "Ошибка подключения к БД.");
            Console.WriteLine("Ошибка: невозможно подключиться к базе данных.");
        }
    }

    // Вариант 2: Переполнение
    static void Variant2()
    {
        try
        {
            byte number = 2;

            while (true)
            {
                checked
                {
                    number *= 2; // Переполнение вызывает OverflowException
                }
            }
        }
        catch (OverflowException ex)
        {
            logger.Error(ex, "Переполнение числа.");
            Console.WriteLine("Ошибка: произошло переполнение числа.");
        }
        catch (Exception ex)
        {
            logger.Error(ex, "Непредвиденная ошибка в расчете.");
            Console.WriteLine("Произошла ошибка: " + ex.Message);
        }
    }

    // Вариант 3: Преобразование типов
    static void Variant3()
    {
        try
        {
            Console.WriteLine("Введите число:");
            int number = int.Parse(Console.ReadLine()); // Ошибка, если не число
            Console.WriteLine("Введите дату:");
            DateTime date = DateTime.Parse(Console.ReadLine()); // Ошибка, если не дата
            Console.WriteLine("Введите символ:");
            char symbol = char.Parse(Console.ReadLine()); // Ошибка, если не символ
        }
        catch (FormatException ex)
        {
            logger.Error(ex, "Ошибка преобразования типов.");
            Console.WriteLine("Ошибка: неверный формат ввода.");
        }
    }

    // Вариант 4: Работа с файлом
    static void Variant4()
    {
        string filePath = "testfile.txt";

        try
        {
            WriteToFile(filePath, "Пример данных для записи");
            Console.WriteLine("Данные успешно записаны.");
        }
        catch (UnauthorizedAccessException ex)
        {
            logger.Error(ex, "Ошибка доступа к файлу: {0}", filePath);
            Console.WriteLine("Ошибка: недостаточно прав для записи.");
        }
        catch (IOException ex)
        {
            logger.Error(ex, "Ошибка ввода-вывода при работе с файлом: {0}", filePath);
            Console.WriteLine("Ошибка при записи данных.");
        }
        catch (Exception ex)
        {
            logger.Error(ex, "Непредвиденная ошибка.");
            Console.WriteLine("Произошла ошибка: " + ex.Message);
        }
    }

    static void WriteToFile(string path, string content)
    {
        if (!Directory.Exists(Path.GetDirectoryName(path)))
        {
            throw new DirectoryNotFoundException("Указанная директория не существует.");
        }

        File.WriteAllText(path, content);
    }
}
