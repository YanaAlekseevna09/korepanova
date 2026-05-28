using System;

// Структура Person — хранит данные об одном человеке
struct Person
{
    public string name;
    public int age;
    public string city;

    // Конструктор с тремя параметрами — полная инициализация
    public Person(string name, int age, string city)
    {
        this.name = name;
        this.age = age;
        this.city = city;
    }

    // Конструктор с двумя параметрами — город по умолчанию "Неизвестно"
    public Person(string name, int age) : this(name, age, "Неизвестно") { }

    // Конструктор с одним параметром — возраст 18, город "Неизвестно"
    public Person(string name) : this(name, 18, "Неизвестно") { }

    // Метод вывода информации о человеке
    public void Print()
    {
        Console.WriteLine($"Имя: {name}, Возраст: {age}, Город: {city}");
    }

    // Проверяет, совершеннолетний ли человек
    public bool IsAdult() => age >= 18;

    // Возвращает приветствие от имени человека
    public string Greet() => $"Привет! Меня зовут {name}, мне {age} лет, я из {city}.";

    // Сравнивает возраст двух людей, возвращает имя старшего
    public static string WhoIsOlder(Person a, Person b) =>
        a.age >= b.age ? a.name : b.name;

    // Статический метод — возвращает персону по умолчанию
    public static Person Default() => new Person("AnonymousEmber", 0, "Неизвестно");
}

class Program
{
    // Вспомогательный метод вывода через поля структуры напрямую
    static void PrintPerson(Person p)
    {
        Console.WriteLine($"{p.name} | {p.age} | {p.city}");
    }

    static void Main()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        // Инициализация через конструктор с тремя параметрами
        Person alexandra = new Person("Александра", 28, "Москва");
        alexandra.Print();

        // Инициализация через конструктор с тремя параметрами
        Person dmitry = new Person("Дмитрий", 35, "Санкт-Петербург");
        PrintPerson(dmitry);

        // Инициализация через инициализатор объекта { }
        Person sergey = new Person { name = "Сергей", age = 22, city = "Казань" };
        PrintPerson(sergey);

        // Копирование структуры с изменением части полей через with
        Person andrey = sergey with { name = "Андрей", age = 24 };
        PrintPerson(andrey);

        // Новые персонажи
        Person elena = new Person("Елена", 16, "Новосибирск");
        Person maxim = new Person("Максим", 41, "Екатеринбург");
        Person olga = new Person("Ольга"); // только имя — возраст 18, город неизвестен
        PrintPerson(elena);
        PrintPerson(maxim);
        PrintPerson(olga);

        // Использование нового метода IsAdult()
        Console.WriteLine($"\n{alexandra.name} совершеннолетняя: {alexandra.IsAdult()}");
        Console.WriteLine($"{elena.name} совершеннолетняя: {elena.IsAdult()}");

        // Использование нового метода Greet()
        Console.WriteLine($"\n{dmitry.Greet()}");
        Console.WriteLine($"{maxim.Greet()}");

        // Использование нового статического метода WhoIsOlder()
        Console.WriteLine($"\nКто старше (Дмитрий / Максим): {Person.WhoIsOlder(dmitry, maxim)}");
        Console.WriteLine($"Кто старше (Александра / Елена): {Person.WhoIsOlder(alexandra, elena)}");

        // Персона по умолчанию
        Person def = Person.Default();
        Console.WriteLine($"\nПо умолчанию: {def.name}, {def.age}, {def.city}");
    }
}