using System;

class Program
{
    static unsafe void Main()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        //  ЧАСТЬ 1: ПЕРЕМЕННЫЕ И УКАЗАТЕЛИ 
        int a = 10, b = 20;
        double c = 3.14;
        char d = 'A';
        float e = 1.5f;

        int* ptrA = &a, ptrB = &b;
        double* ptrC = &c;
        char* ptrD = &d;
        float* ptrE = &e;

        Console.WriteLine("===== Исходные значения =====");
        Console.WriteLine($"a={a} адрес:{(ulong)ptrA}  b={b} адрес:{(ulong)ptrB}");
        Console.WriteLine($"c={c} адрес:{(ulong)ptrC}  d={d} адрес:{(ulong)ptrD}");
        Console.WriteLine($"e={e} адрес:{(ulong)ptrE}");

        *ptrA = 100; *ptrB = 200; *ptrC = 6.28; *ptrD = 'Z'; *ptrE = 9.9f;

        Console.WriteLine("\n===== После изменения через указатели =====");
        Console.WriteLine($"a={a}  b={b}  c={c}  d={d}  e={e}");

        // ЧАСТЬ 2: МАССИВ ЧЕРЕЗ УКАЗАТЕЛЬ
        int[] numbers = { 10, 20, 30, 40, 50 };

        fixed (int* ptrArr = numbers)
        {
            Console.WriteLine("\n Массив через указатель");

            for (int i = 0; i < numbers.Length; i++)
                Console.WriteLine($"  numbers[{i}] = {*(ptrArr + i)},  адрес: {(ulong)(ptrArr + i)}");

            for (int i = 0; i < numbers.Length; i++)
                *(ptrArr + i) *= 2;

            int sum = 0;
            for (int i = 0; i < numbers.Length; i++)
                sum += *(ptrArr + i);

            Console.WriteLine($"\nМассив после *2: {string.Join(", ", numbers)}");
            Console.WriteLine($"Сумма элементов: {sum}");
        }
    }
}