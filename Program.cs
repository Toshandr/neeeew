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
    public static void CopyFiles(string request){ //метод для копирования файла или папки
        try{
            Console.Write("\n Введите название файла или папки которое вы хотите скопировать: ");
            string? fileName = Console.ReadLine();
            if(fileName != null && (Directory.Exists(request + @"\" + fileName) || File.Exists(request + @"\" + fileName))){ //проверка существует ли файл или папка
                FileInfo oldFile = new FileInfo(request + @"\" + fileName); //с помощью объекта буду копировать папку
                Console.Write("\n Введите абсолютный путь, куда вы хотите скопировать: ");
                string? newFileplace = Console.ReadLine();
                if(newFileplace != null && Directory.Exists(newFileplace)){ //проверка существует ли такой путь
                    /*if(Directory.Exists(request + @"\" + fileName)){
                        Console.WriteLine("Хотите ли вы скопировать содержимое папки?(true/false)");
                        string? InDirectory = Console.ReadLine().ToLower();
                        if(InDirectory != null && (InDirectory == "true" || InDirectory == "false")){
                            oldFile.CopyTo(newFileplace + @"\" + fileName, Boolean.Parse(InDirectory));
                            Console.WriteLine("Папка успешно скопирована");
                        }
                    }*/
                    if(File.Exists(request + @"\" + fileName)){
                        File.Copy(request + @"\" + fileName, newFileplace + @"\" + fileName);
                    }
                }
            }
        }
        catch(Exception ex){
            Console.WriteLine(ex.Message);
        }
    }
}
 



