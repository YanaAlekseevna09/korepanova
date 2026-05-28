using System;

// Делегат 1: без параметров и возвращаемого значения — для приветственных сообщений
delegate void Message();

// Делегат 2: принимает два int, возвращает int — для математических операций
delegate int Operation(int x, int y);

// Делегат 3: обобщённый — принимает значение типа K, возвращает тип T
delegate T GenericOperation<T, K>(K val);

class Program
{
    // Методы для делегата Message
    static void Privet() => Console.WriteLine("Мяу, приветствует! привет!");
    static void KakDela() => Console.WriteLine("Мяу, как дела? хорошо!");
    static void Poka() => Console.WriteLine("Мяу, пока!");

    // Методы для делегата Operation
    static int Slozhenie(int x, int y) => x + y;
    static int Vychitanie(int x, int y) => x - y;
    static int Umnozhenie(int x, int y) => x * y;

    // Методы для обобщённого делегата
    static decimal Kvadrat(int n) => n * n;
    static int Udvoenie(int n) => n * 2;

    // Принимает делегат Operation как параметр — не нужно писать отдельный метод для каждой операции
    static void VypolniOperaciyu(int a, int b, Operation op)
    {
        Console.WriteLine($"  Результат: {op(a, b)}");
    }

    // Возвращает нужный делегат по строке — выбор операции без цепочки if/else в Main
    static Operation VyborOperacii(string opType)
    {
        return opType switch
        {
            "slozh" => Slozhenie,
            "vichit" => Vychitanie,
            _ => Umnozhenie
        };
    }

    static void Main()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        //Мультикаст: несколько методов в одном делегате
        Console.WriteLine("Мультикаст Message");
        Message message = Privet;
        message += KakDela; // добавляем второй метод
        message += Poka;    // добавляем третий метод
        message();          // вызываются все три по порядку

        message -= KakDela; // удаляем KakDela из списка
        Console.WriteLine("После удаления KakDela");
        message?.Invoke();  // безопасный вызов через ?.Invoke() на случай null

        //Мультикаст Operation: возвращается результат ПОСЛЕДНЕГО метода
        Console.WriteLine("\nМультикаст Operation (результат последнего)");
        Operation op = Slozhenie;
        op += Umnozhenie;
        op += Vychitanie; // последний в списке — его результат и вернётся
        Console.WriteLine($"  Последний результат (Vychitanie): {op(10, 3)}"); // 10-3=7

        //Делегат как параметр метода
        Console.WriteLine("\nДелегат как параметр");
        VypolniOperaciyu(8, 2, Slozhenie);  // передаём метод Slozhenie
        VypolniOperaciyu(8, 2, Umnozhenie); // передаём метод Umnozhenie

        //Делегат как возвращаемое значение
        Console.WriteLine("\nДелегат из метода");
        Operation selected = VyborOperacii("vichit"); // получаем Vychitanie
        Console.WriteLine($"  Мяу выбрала вычитание: {selected(20, 5)}"); // 15

        //Объединение делегатов через + 
        Console.WriteLine("\nОбъединение делегатов");
        Message mes3 = Privet + KakDela; // объединяем два делегата в один
        mes3();

        //Обобщённый делегат GenericOperation
        Console.WriteLine("\nОбобщённый делегат");
        GenericOperation<decimal, int> squareOp = Kvadrat;
        Console.WriteLine($"  Квадрат 7 = {squareOp(7)}"); // decimal результат

        GenericOperation<int, int> doubleOp = Udvoenie;
        Console.WriteLine($"  Удвоение 7 = {doubleOp(7)}"); // int результат

        //Null-делегат: безопасный вызов через ?.Invoke()
        Console.WriteLine("\nNull делегат");
        Operation? nullOp = null;
        int? result = nullOp?.Invoke(5, 3); // не упадёт, вернёт null
        Console.WriteLine($"  Результат: {(result.HasValue ? result.ToString() : "null — Мяу спит")}");
    }
}