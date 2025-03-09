using System.Text.Json;


namespace neeeew;


class Person
{
    public string Name { get;}
    public int Age { get; set; }
    public Person(string name, int age)
    {
        Name = name;
        Age = age;
    }
}

class Program
{
    static void Main(string[] args)
    {
        Console.Write("Введите абсолютный путь до папки с которой вы хотите начать работать: ");
        string? request = Console.ReadLine();
        if(request != null && Directory.Exists(request)){
            ReadAll(request);
            CopyFiles(request);
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

    public static void recursionFolder(string sourceDir, string targetDir){ //метод для переноса всего католга целиком
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
            Console.Write("\n Введите название файла или папки которое вы хотите скопировать: ");
            string? fileName = Console.ReadLine();
            if(fileName != null && (Directory.Exists(request + @"\" + fileName) || File.Exists(request + @"\" + fileName))){ //проверка существует ли файл или папка
                Console.Write("\n Введите абсолютный путь, куда вы хотите скопировать: ");
                string? newFileplace = Console.ReadLine();
                if(newFileplace != null && Directory.Exists(newFileplace)){ //проверка существует ли такой путь
                    if(Directory.Exists(request + @"\" + fileName)){ // копирование если объект - папка
                        string newfullfolderpath = newFileplace + @"\" + fileName;
                        string oldfullfolderpath = request + @"\" + fileName;
                        recursionFolder(oldfullfolderpath, newfullfolderpath);
                        Console.WriteLine("Копирование выполнено успешно");
                    }
                    if(File.Exists(request + @"\" + fileName)){ // копирование если объект - файл
                        File.Copy(request + @"\" + fileName, newFileplace + @"\" + fileName);
                        Console.WriteLine("Копирование выполнено успешно");
                    }
                }
            }
        }
        catch(Exception ex){
            Console.WriteLine(ex.Message);
        }
    }
}
 



