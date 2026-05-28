using System;

//  Инкапсуляция
// Поля скрыты (private), доступ только через свойства и методы
class BankAccount
{
    private string owner;   // скрытое поле — имя владельца
    private decimal balance; // скрытое поле — баланс

    public BankAccount(string owner, decimal initialBalance)
    {
        this.owner = owner;
        this.balance = initialBalance;
    }

    // Публичный метод — единственный способ пополнить счёт
    public void Deposit(decimal amount)
    {
        if (amount > 0)
            balance += amount;
    }

    // Публичный метод — единственный способ снять деньги
    public void Withdraw(decimal amount)
    {
        if (amount > 0 && amount <= balance)
            balance -= amount;
        else
            Console.WriteLine("  Недостаточно средств!");
    }

    // Публичный метод — показать состояние счёта
    public void PrintInfo()
    {
        Console.WriteLine($"  Владелец: {owner}, Баланс: {balance} руб.");
    }
}

//НАСЛЕДОВАНИЕ
// Базовый класс — общие свойства для любого транспорта
class Vehicle
{
    public string Brand;  // марка
    public int Year;      // год выпуска

    public Vehicle(string brand, int year)
    {
        Brand = brand;
        Year = year;
    }

    // Виртуальный метод — можно переопределить в подклассе
    public virtual void Describe()
    {
        Console.WriteLine($"  Транспорт: {Brand}, год: {Year}");
    }
}

// Подкласс Car наследует Vehicle и добавляет свои поля
class Car : Vehicle
{
    public int Doors; // количество дверей

    public Car(string brand, int year, int doors) : base(brand, year)
    {
        Doors = doors;
    }

    // Переопределяем метод базового класса
    public override void Describe()
    {
        Console.WriteLine($"  Легковой автомобиль: {Brand}, год: {Year}, дверей: {Doors}");
    }
}

// Подкласс Truck наследует Vehicle и добавляет свои поля
class Truck : Vehicle
{
    public double CargoTons; // грузоподъёмность в тоннах

    public Truck(string brand, int year, double cargoTons) : base(brand, year)
    {
        CargoTons = cargoTons;
    }

    // Переопределяем метод базового класса
    public override void Describe()
    {
        Console.WriteLine($"  Грузовик: {Brand}, год: {Year}, грузоподъёмность: {CargoTons} т.");
    }
}

// ПОЛИМОРФИЗМ
// Базовый класс с виртуальным методом Sound()
class Animal
{
    public string Name;

    public Animal(string name)
    {
        Name = name;
    }

    // Виртуальный метод — каждый подкласс реализует по-своему
    public virtual void Sound()
    {
        Console.WriteLine($"  {Name} издаёт звук");
    }
}

class Dog : Animal
{
    public Dog(string name) : base(name) { }

    public override void Sound()
    {
        Console.WriteLine($"  {Name} говорит: Гав!");
    }
}

class Cat : Animal
{
    public Cat(string name) : base(name) { }

    public override void Sound()
    {
        Console.WriteLine($"  {Name} говорит: Мяу!");
    }
}

class Cow : Animal
{
    public Cow(string name) : base(name) { }

    public override void Sound()
    {
        Console.WriteLine($"  {Name} говорит: Муу!");
    }
}

class Program
{
    static void Main()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        // Инкапсуляция
        Console.WriteLine("=== Инкапсуляция ===");
        BankAccount account = new BankAccount("Александра", 1000);
        account.PrintInfo();
        account.Deposit(500);
        account.Withdraw(200);
        account.PrintInfo();
        account.Withdraw(5000); // попытка снять больше, чем есть

        // Наследование
        Console.WriteLine("\n=== Наследование ===");
        Car car = new Car("Toyota", 2022, 4);
        Truck truck = new Truck("Volvo", 2020, 20.5);
        car.Describe();
        truck.Describe();

        // Полиморфизм 
        // Массив базового типа Animal — каждый объект вызывает свою версию Sound()
        Console.WriteLine("\n=== Полиморфизм ===");
        Animal[] animals = { new Dog("Рекс"), new Cat("Мурка"), new Cow("Зорька") };
        foreach (Animal animal in animals)
        {
            animal.Sound(); // один и тот же вызов — разное поведение
        }
    }
}