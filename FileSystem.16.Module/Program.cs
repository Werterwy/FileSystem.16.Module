using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileSystem._16.Module
{
    public class Program
    {
        private static string logFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "log.txt");
        private static string pathlog;
        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("1. Просмотр Содержимого Директории");
                Console.WriteLine("2. Создание Файла/Директории");
                Console.WriteLine("3. Удаление Файла/Директории");
                Console.WriteLine("4. Копирование Файла/Директории");
                Console.WriteLine("5. Перемещение Файла/Директории");
                Console.WriteLine("6. Чтение из Файла");
                Console.WriteLine("7. Запись в Файл");
                Console.WriteLine("8. Выход");

                Console.Write("Выберите действие: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        ShowDirectoryContents();
                        Console.WriteLine("");
                        break;
                    case "2":
                        CreateFileOrDirectory();
                        Console.WriteLine("");
                        break;
                    case "3":
                        DeleteFileOrDirectory();
                        Console.WriteLine("");
                        break;
                    case "4":
                        CopyFileOrDirectory();
                        Console.WriteLine("");
                        break;
                    case "5":
                        MoveFileOrDirectory();
                        Console.WriteLine("");
                        break;
                    case "6":
                        ReadFromFile();
                        Console.WriteLine("");
                        break;
                    case "7":
                        WriteToFile();
                        Console.WriteLine("");
                        break;
                    case "8":
                        Console.WriteLine("");
                        DisplayLog();
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Некорректный выбор. Пожалуйста, введите корректный номер.");
                        break;
                }
            }

        }

        /// <summary>
        /// Просмотр Содержимого Директории:- Приложение должно запрашивать у пользователя путь 
        /// к директории и отображать список всех файлов и поддиректорий в этой директории.
        /// </summary>
        static void ShowDirectoryContents()
        {
            Console.Write("Введите путь к директории: ");
            string path = Console.ReadLine();

            try
            {
                string[] files = Directory.GetFiles(path);
                string[] directories = Directory.GetDirectories(path);
                Console.WriteLine("");
                Console.WriteLine("Файлы:");
                foreach (string file in files)
                {
                    Console.WriteLine(Path.GetFileName(file));
                }
                Console.WriteLine("");
                Console.WriteLine("Директории:");
                foreach (string directory in directories)
                {
                    Console.WriteLine(Path.GetFileName(directory));
                }
                LogAction($"Просмотр содержимого директории: {pathlog}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }
        }

        /// <summary>
        ///  Создание Файла/Директории: - Пользователь может создать новый файл 
        ///  или директорию в указанном месте.
        /// </summary>
        static void CreateFileOrDirectory()
        {
            Console.Write("Введите путь к файлу/директории: ");
            string path = Console.ReadLine();

            Console.Write("Выберите тип (Файл - F, Директория - D): ");
            string type = Console.ReadLine();

            try
            {
                if (type.ToUpper() == "F")
                {
                    File.Create(path).Close();
                    Console.WriteLine("Файл создан успешно.");
                    LogAction($"Создание Файла/Директории: {pathlog}");
                }
                else if (type.ToUpper() == "D")
                {
                    Directory.CreateDirectory(path);
                    Console.WriteLine("Директория создана успешно.");
                    LogAction($"Создание Файла/Директории: {pathlog}");
                }
                else
                {
                    Console.WriteLine("Некорректный выбор типа.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }
        }

        /// <summary>
        /// Удаление Файла/Директории: - Предоставить возможность удаления файла или директории по указанному пути.
        /// </summary>
        static void DeleteFileOrDirectory()
        {
            Console.Write("Введите путь к файлу/директории для удаления: ");
            string path = Console.ReadLine();

            try
            {
                if (File.Exists(path))
                {
                    File.Delete(path);
                    Console.WriteLine("Файл удален успешно.");
                    LogAction($"Удаление Файла/Директории: {pathlog}");
                }
                else if (Directory.Exists(path))
                {
                    Directory.Delete(path, true);
                    Console.WriteLine("Директория удалена успешно.");
                    LogAction($"Удаление Файла/Директориии: {pathlog}");
                }
                else
                {
                    Console.WriteLine("Файл/директория не существует.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }
        }

        /// <summary>
        /// Копирование Файлов и Директорий: - Реализовать функции для копирования файлов и директорий.
        /// </summary>
        static void CopyFileOrDirectory()
        {
            Console.Write("Введите путь к файлу/директории для копирования: ");
            string sourcePath = Console.ReadLine();

            Console.Write("Введите путь назначения: ");
            string destinationPath = Console.ReadLine();

            try
            {
                if (File.Exists(sourcePath))
                {
                    File.Copy(sourcePath, Path.Combine(destinationPath, Path.GetFileName(sourcePath)), true);
                    Console.WriteLine("Файл скопирован успешно.");
                    LogAction($"Копирование Файла/Директории: {pathlog}");
                }
                else if (Directory.Exists(sourcePath))
                {
                    CopyDirectory(sourcePath, Path.Combine(destinationPath, Path.GetFileName(sourcePath)));
                    Console.WriteLine("Директория скопирована успешно.");
                    LogAction($"Копирование Файла/Директории: {pathlog}");
                }
                else
                {
                    Console.WriteLine("Файл/директория не существует.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }
        }

        /// <summary>
        /// Перемещение Файлов и Директорий: - Реализовать функции для перемещения файлов и директорий.
        /// </summary>
        static void MoveFileOrDirectory()
        {
            Console.Write("Введите путь к файлу/директории для перемещения: ");
            string sourcePath = Console.ReadLine();

            Console.Write("Введите путь назначения: ");
            string destinationPath = Console.ReadLine();

            try
            {
                if (File.Exists(sourcePath))
                {
                    File.Move(sourcePath, Path.Combine(destinationPath, Path.GetFileName(sourcePath)));
                    Console.WriteLine("Файл перемещен успешно.");
                    LogAction($"Перемещение Файла/Директории: {pathlog}");
                }
                else if (Directory.Exists(sourcePath))
                {
                    Directory.Move(sourcePath, Path.Combine(destinationPath, Path.GetFileName(sourcePath)));
                    Console.WriteLine("Директория перемещена успешно.");
                    LogAction($"Перемещение Файла/Директории: {pathlog}");
                }
                else
                {
                    Console.WriteLine("Файл/директория не существует.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }
        }

        /// <summary>
        /// Чтение в Файл: - Предоставить возможность чтения текста.
        /// </summary>
        static void ReadFromFile()
        {
            Console.Write("Введите путь к файлу для чтения: ");
            string path = Console.ReadLine();

            try
            {
                if (File.Exists(path))
                {
                    string content = File.ReadAllText(path);
                    Console.WriteLine($"Содержимое файла:\n{content}");
                    LogAction($"Чтение из Файла: {pathlog}");
                }
                else
                {
                    Console.WriteLine("Файл не существует.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }
        }

        /// <summary>
        /// Запись в Файл: - Предоставить возможность записи текста в файл.
        /// </summary>
        static void WriteToFile()
        {
            Console.Write("Введите путь к файлу для записи: ");
            string path = Console.ReadLine();

            Console.Write("Введите текст для записи в файл: ");
            string content = Console.ReadLine();

            try
            {
                File.WriteAllText(path, content);
                Console.WriteLine("Текст записан в файл успешно.");
                LogAction($"Запись в Файл: {pathlog}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }
        }

        static void CopyDirectory(string sourceDir, string destinationDir)
        {
            if (!Directory.Exists(destinationDir))
            {
                Directory.CreateDirectory(destinationDir);
            }

            string[] files = Directory.GetFiles(sourceDir);

            foreach (string file in files)
            {
                string fileName = Path.GetFileName(file);
                string destFile = Path.Combine(destinationDir, fileName);
                File.Copy(file, destFile, true);
            }

            string[] dirs = Directory.GetDirectories(sourceDir);

            foreach (string dir in dirs)
            {
                string dirName = Path.GetFileName(dir);
                string destDir = Path.Combine(destinationDir, dirName);
                CopyDirectory(dir, destDir);
            }
        }

        /// <summary>
        /// Логирование Действий: - Вести лог всех выполненных операций, сохраняя его в отдельный файл.
        /// </summary>
        /// <param name="action"></param>
        static void LogAction(string action)
        {
            try
            {
                string logMessage = $"{DateTime.Now} - {action}";
                File.AppendAllText(logFilePath, logMessage + Environment.NewLine);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при записи в лог: {ex.Message}");
            }
        }

        /// <summary>
        /// Вывод содержимого лог файла.
        /// </summary>
        static void DisplayLog()
        {
            try
            {
                if (File.Exists(logFilePath))
                {
                    string logContent = File.ReadAllText(logFilePath);
                    Console.WriteLine($"Путь к файлу: ({logFilePath}):");
                    Console.WriteLine("Содержимое лога ");
                    Console.WriteLine(logContent);
                }
                else
                {
                    Console.WriteLine($"Лог-файл не найден: {logFilePath}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при чтении лога: {ex.Message}");
            }
        }
    }
}
