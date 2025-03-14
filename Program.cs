using System.Diagnostics;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.VisualBasic;


namespace neeeew;

//класс

class Program
{

    /*программа работает по такому принципу:
    1.Пользователь выбирает папку старта, откуда он начнёт работу
    2.Пользователь видит все файлы и папки каталога
    3.Выбирает команду(которые пока описаны как методы)
    4.Выбирает файл, не прописывая полный путь до него, то есть пользователь может узнать инфу о файле только когда перейдёт в его родительский каталог*/

    static void Main(string[] args)
    {
        Console.Clear();
        string start = @"C:\Users\MSI ThinGF63\Desktop\neeeew\startingFolder";
        StreamReader starting = new StreamReader(start);
        string? request = "";
        string? path = starting.ReadLine(); //тут путь из файла
        starting.Close();


        if(path == null){
            Console.Write("Введите абсолютный путь до папки с которой вы хотите начать работать: ");
            request = Console.ReadLine();
            if(Directory.Exists(request)){
                readFirst(request);
                Menu(request);
            }
            else{
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Такой директории нет, поверьте данные.");
                Console.ResetColor();
                Console.ReadKey();
            }
        }
        else{
            Console.WriteLine("В прошлый раз ваша работа остановилась: " + path);
            Console.WriteLine("Хотите продолжить оттуда?\nДа/Нет");
            string? accept = Console.ReadLine();
            if (accept!=null){
                switch(accept.ToUpper()){
                    case "ДА":
                        readFirst(path);
                        Menu(path);
                        break;


                    case "НЕТ":
                        Console.Write("Введите абсолютный путь до папки с которой вы хотите начать работать: ");
                        request = Console.ReadLine();
                        if(request != null && Directory.Exists(request)){
                            readFirst(request);
                            Menu(request);
                        };
                        break;
                    default:
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Неверный ввод.");
                        Console.ResetColor();
                        Console.ReadKey();
                        break;
                }
            }
            else{
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Пустой ввод.");
                Console.ResetColor();
                Console.ReadKey();
            }

        }   
    }
    


    public static void Menu(string path){
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("_______________________________________");
        Console.WriteLine("|-------------Меню команд-------------|");
        Console.WriteLine("_______________________________________");
        Console.ResetColor();
        Console.WriteLine("|-----1. Переход в другой каталог-----|");
        Console.WriteLine("|--2. Переход в родительский каталог--|");
        Console.WriteLine("|-----------3. Открыть папку----------|");
        Console.WriteLine("|----------4. Копировать файл---------|");
        Console.WriteLine("|-----------5. Удалить файл-----------|");
        Console.WriteLine("|---------6. Информация о файле-------|");
        Console.WriteLine("|-0. Выйти и сохранить последнюю папку|\n");
        Console.WriteLine("");
        Console.WriteLine("Сейчас мы тут: " + path);
        Console.Write("Введите номер команды: ");
        int? choice = 0;
        try{
            choice = Convert.ToInt32(Console.ReadLine());
        }
        catch(Exception ex){
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(ex.Message);
            Console.ResetColor();
            Console.ReadKey();
        }
        if (choice != null){
            switch(choice){
                case 1:
                    readFirst();
                    break;
                case 2:
                    readingParent(path);
                    break;
                case 3:
                    readingDirectory(path);
                    break;
                case 4:
                    copyFiles(path);
                    break;
                case 5:
                    deleteFiles(path);
                    break;
                case 6:
                    fileInformation(path);
                    break;
                case 0:
                    Save(path);
                    Environment.Exit(0);
                    break;
                
            }
        }
        else{
            Console.WriteLine("Команда пустая.");
        }
    }


    public static void readingParent(string parentpath){
        try{
            string main = Directory.GetParent(parentpath).FullName;
            if(Directory.Exists(main)){
                readFirst(main);
                Menu(main);
            }
            else{
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Неверный ввод.");
                Console.ResetColor();
                Console.ReadKey();
            }
        }
        catch(Exception ex){
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(ex.Message);
            Console.ResetColor();
            Console.ReadKey();
        }
    }

    public static void readFirst(){
        Console.Clear(); //метод для перехода по абсолютному пути
        Console.WriteLine();
        Console.Write("Введите абсолютный путь до директории: ");
        string? path = Console.ReadLine();
        if(path != null && Directory.Exists(path)){
            int colFiles = 0;
            foreach(var i in Directory.GetDirectories(path)){
                colFiles++;
                Console.WriteLine($"{colFiles}. {i} \n");
            }
            foreach(var i in Directory.GetFiles(path)){
                colFiles++;
                Console.WriteLine($"{colFiles}. {i} \n");
            }
            if(colFiles == 0){
                Console.WriteLine("Папка пуста");
            }
            Menu(path);
            }
        else{
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Неверный входные данные.");
            Console.ResetColor();
            Console.ReadKey();

        }
    }



    public static void readFirst(string path){
        Console.Clear(); //метод для перехода по абсолютному пути
        Console.WriteLine();
        int colFiles = 0;
        if(Directory.Exists(path) && path != null){
            foreach(var i in Directory.GetDirectories(path)){
                colFiles++;
                Console.WriteLine($"{colFiles}. {i} \n");
            }
            foreach(var i in Directory.GetFiles(path)){
                colFiles++;
                Console.WriteLine($"{colFiles}. {i} \n");
            }
            Menu(path);
        }
        else{
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Неккоректный ввод.");
            Console.ResetColor();
        }
    }

    public static void Save(string path){
        string start = @"C:\Users\MSI ThinGF63\Desktop\neeeew\startingFolder";
        
        using (StreamWriter writer = new StreamWriter(start, false))
        {
            writer.WriteAsync(path);
            Console.WriteLine($"Путь успешно сохранён в файл: {start}");
        }

    }
    public static void readingDirectory(string parentpath){ //просмотр всех файло директории
        Console.Write("\nВведите название каталога в который хотите перейти: ");
        string? dirname = Console.ReadLine();
        Console.Clear();
        if(dirname != null && Directory.Exists(Path.Combine(parentpath, dirname))){
            int colFiles = 0;
            foreach(var i in Directory.GetDirectories(Path.Combine(parentpath, dirname))){
                colFiles++;
                Console.WriteLine($"{colFiles}. {i} \n");
            }
            foreach(var i in Directory.GetFiles(Path.Combine(parentpath, dirname))){
                colFiles++;
                Console.WriteLine($"{colFiles}. {i} \n");
            }
            if(colFiles == 0){
                Console.WriteLine("Пустая папка.");
            }
            Menu(Path.Combine(parentpath, dirname));
        }
        else{
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Проверьте ввод и убедитесь что это папка.");
            Console.ResetColor();
            Console.ReadKey();
            Console.Clear();
            readFirst(parentpath);
            Menu(parentpath);
        }

    }
    

    //readingFile - будущая заготовка для считывание и изменения файла

    public static void recursionFolder(string sourceDir, string targetDir){ //метод для переноса католга целиком
        Directory.CreateDirectory(targetDir); //создаём в папке назаначения новую папку
        foreach (string filePath in Directory.GetFiles(sourceDir)) //из начальной папки берём все файлы
        {
            string fileName = Path.GetFileName(filePath); //берём имя файла
            string destFilePath = Path.Combine(targetDir, fileName); //прессуем имя файла и путь на новую папку - получаем путь до нового файла
            File.Copy(filePath, destFilePath, true); //переносим файлы в новую папку
        }
        foreach (string dirPath in Directory.GetDirectories(sourceDir)) //берём все подкаталоги
        {
            string dirName = Path.GetFileName(dirPath); //берём имя каждой папки
            string destSubDir = Path.Combine(targetDir, dirName); //прессуем и получаем ссылку на новый подкаталог в целевом каталоге
            recursionFolder(dirPath, destSubDir); //повтороно вызываем метод но уже проходимся по подкаталогам
        }
    }
    public static void copyFiles(string request){ //метод для копирования файла или папки
        try{
            Console.Write("\nВведите название файла или папки которые вы хотите скопировать: ");
            string? fileName = Console.ReadLine();
            if(fileName != null && (Directory.Exists(Path.Combine(request, fileName)) || File.Exists(Path.Combine(request, fileName)))){ //проверка существует ли файл или папка
                Console.Write("\nВведите абсолютный путь, куда вы хотите скопировать: ");
                string? newFileplace = Console.ReadLine();
                if(newFileplace != null && Directory.Exists(newFileplace)){ //проверка существует ли такой путь
                    if(Directory.Exists(Path.Combine(request, fileName))){ // копирование если объект - папка
                        string newfullfolderpath = Path.Combine(newFileplace, fileName);
                        string oldfullfolderpath = Path.Combine(request, fileName);
                        recursionFolder(oldfullfolderpath, newfullfolderpath);
                        Console.WriteLine("Копирование выполнено успешно");
                    }
                    if(File.Exists(Path.Combine(request, fileName))){ // копирование если объект - файл
                        File.Copy(Path.Combine(request, fileName), Path.Combine(newFileplace, fileName));
                        Console.WriteLine("Копирование выполнено успешно");
                    }
                    Console.ReadKey();
                    Console.Clear();
                    readFirst(request);
                    Menu(request);
                }
                else{
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Такого каталога нет.");
                    Console.ResetColor();
                    Console.ReadKey();
                    Console.Clear();
                    readFirst(request);
                    Menu(request);
                }
            }
            else{
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Такого файла нет.");
                Console.ResetColor();
                Console.ReadKey();
                Console.Clear();
                readFirst(request);
                Menu(request);
            }
        }
        catch(Exception ex){
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(ex.Message);
            Console.ResetColor();
            Console.ReadKey();
            Console.Clear();
            readFirst(request);
            Menu(request);
        }
    }
    public static void fileInformation(string request){//метод для вывода основной информации
        try{
            Console.Write("\nВведите название файла о котором вы хотите узнать: ");
            string? fileName = Console.ReadLine();
            if(fileName != null && (File.Exists(Path.Combine(request, fileName)) || Directory.Exists(Path.Combine(request, fileName)))){ //проверка есть ли файл или папка с таким названием
                if(File.Exists(Path.Combine(request, fileName))){// проверяем существует ли такой файл
                    FileInfo file = new FileInfo(Path.Combine(request, fileName));
                    Console.WriteLine("Размер файла: " + file.Length / 1024.0  + "КБ");
                    Console.WriteLine("Расширение файла: " + file.Extension);
                    Console.WriteLine("Абсолютный путь: " + file.FullName);
                }
                if(Directory.Exists(Path.Combine(request, fileName))){ //проверяем существует ли такая папка
                    DirectoryInfo dir = new DirectoryInfo(Path.Combine(request, fileName));
                    Console.WriteLine("Размер папки: " + dir.EnumerateFiles("*", SearchOption.AllDirectories).Sum(fi => fi.Length)/1024 + "КБ"); //находим все файлы в каталоге и берём у каждого файла его размер
                    Console.WriteLine("Расширение: каталог");
                    Console.WriteLine("Абсолютный путь: " + dir.FullName);
                }
                Console.ReadKey();
                Console.Clear();
                readFirst(request);
                Menu(request);
            }
            else{
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Такого файла нет.");
                Console.ResetColor();
                Console.ReadKey();
            }
        }
        catch(Exception ex){
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(ex.Message);
            Console.ResetColor();
            Console.ReadKey();
        }
    }
    public static void deleteFiles(string request){//метод для удаления файла или папок
            Console.Write("\nВведите название файла или папки которые вы хотите удалить: ");
            string? fileName = Console.ReadLine();
            if(fileName != null && (Directory.Exists(Path.Combine(request, fileName)) || File.Exists(Path.Combine(request, fileName)))){
                if(Directory.Exists(Path.Combine(request, fileName))){
                    Directory.Delete(Path.Combine(request, fileName), true);
                    Console.WriteLine("Удаление выполнено успешно.");
                }
                if(File.Exists(Path.Combine(request, fileName))){
                    File.Delete(Path.Combine(request, fileName));
                    Console.WriteLine("Удаление выполнено успешно.");
                }
                Console.ReadKey();
                Console.Clear();
                readFirst(request);
                Menu(request);
            }
            else{
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Проверьте ввод.");
                Console.ResetColor();
                Console.ReadKey();
                Console.Clear();
                readFirst(request);
                Menu(request);
            }

        }
}