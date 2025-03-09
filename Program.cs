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
}
 



