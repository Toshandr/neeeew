using System.Text.Json;


namespace neeeew;



class Program
{

    /*программа работает по такому принципу:
    1.Пользователь выбирает папку старта, откуда он начнёт работу
    2.Пользователь видит все файлы и папки каталога
    3.Выбирает команду(которые пока описаны как методы)
    4.Выбирает файл, не прописывая полный путь до него, то есть пользователь может узнать инфу о файлу только когда перейдёт в его родительский каталог*/

    static void Main(string[] args)
    {
        Console.Write("Введите абсолютный путь до папки с которой вы хотите начать работать: ");
        string? request = Console.ReadLine();
        if(request != null && Directory.Exists(request)){


            // Проба методов, чтобы проверить работу одного из них раскомментируйте нужный


            //ReadAll(request); 
            //CopyFiles(request);
            FileInformation(request);
            //DeleteFiles(request);
        }
    }
    


    public static void ReadAll(string path){ //метод для вывода всех файлов внутри директории
        Console.WriteLine();
        int colFiles = 0;
        foreach(var i in Directory.GetDirectories(path)){
            colFiles++;
            Console.WriteLine($"{colFiles}. {i} \n");
        }
        foreach(var i in Directory.GetFiles(path)){
            colFiles++;
            Console.WriteLine($"{colFiles}. {i} \n");
        }
    }

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
    public static void CopyFiles(string request){ //метод для копирования файла или папки
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
                }
                else{
                    Console.WriteLine("Такого каталога нет");
                }
            }
            else{
                Console.WriteLine("Такого файла нет");
            }
        }
        catch(Exception ex){
            Console.WriteLine(ex.Message);
        }
    }
    public static void FileInformation(string request){//метод для вывода основной информации
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
                    Console.WriteLine("Размер папки: " + dir.EnumerateFiles("", SearchOption.AllDirectories).Sum(fi => fi.Length)/1024 + "КБ"); //находим все файлы в каталоге и берём у каждого файла его размер
                    Console.WriteLine("Расширение: каталог");
                    Console.WriteLine("Абсолютный путь: " + dir.FullName);
                }
            }
            else{
                Console.WriteLine("Такого файла нет.");
            }
        }
        catch(Exception ex){
            Console.WriteLine(ex.Message);
        }
    }
    public static void DeleteFiles(string request){//метод для удаления файла или папок
            Console.Write("\nВведите название файла или папки которые вы хотите скопировать: ");
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
            }

        }

}
 



