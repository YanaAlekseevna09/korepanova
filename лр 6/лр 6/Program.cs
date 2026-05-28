using System;
using System.Text.RegularExpressions;

namespace RegexLab
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.WriteLine("ЛАБОРАТОРНАЯ РАБОТА №6: РЕГУЛЯРНЫЕ ВЫРАЖЕНИЯ\n");

            // Текст для анализа — отрывок из стихотворения Есенина "Берёза"
            string text = @"Белая берёза
Под моим окном
Принакрылась снегом,
Точно серебром.
На пушистых ветках
Снежною каймой
Распустились кисти
Белой бахромой.
И стоит берёза
В сонной тишине,
И горят снежинки
В золотом огне.
А заря, лениво
Обходя кругом,
Обсыпает ветки
Новым серебром.";

            // Разбиваем текст на строки, убирая пустые
            string[] lines = text.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
            Console.WriteLine($"Загружен текст (Есенин, 'Берёза'). Строк: {lines.Length}, символов: {text.Length}\n");

            // ЧАСТЬ 2: ПОДСЧЁТ СЛОВ
            // \b — граница слова, [а-яА-ЯёЁa-zA-Z]+ — одна или более букв
            Console.WriteLine("ЧАСТЬ 2: ПОДСЧЁТ СЛОВ");
            MatchCollection words = Regex.Matches(text, @"\b[а-яА-ЯёЁa-zA-Z]+\b");
            Console.WriteLine($"Всего слов: {words.Count}\nПервые 5: {string.Join(", ", GetFirstWords(words, 5))}\n");

            // ЧАСТЬ 3: СЛОВОСОЧЕТАНИЯ
            // Ищем слова с предлогом "под" и конструкцию "слово и слово"
            Console.WriteLine("ЧАСТЬ 3: СЛОВОСОЧЕТАНИЯ");
            ShowMatches(text, @"\bпод\s+[а-яА-ЯёЁ]+\b", "с предлогом 'под'");
            ShowMatches(text, @"\b[а-яА-ЯёЁ]+\s+и\s+[а-яА-ЯёЁ]+\b", "'слово и слово'");

            // ЧАСТЬ 4: СИМВОЛЫ
            // Подсчёт знаков препинания, гласных и согласных букв
            Console.WriteLine("\nЧАСТЬ 4: СИМВОЛЫ");
            Console.WriteLine($"Знаков препинания: {Regex.Matches(text, @"[.,;:!?-]").Count}");
            Console.WriteLine($"Гласных: {Regex.Matches(text, @"[аеёиоуыэюяАЕЁИОУЫЭЮЯ]").Count}");
            Console.WriteLine($"Согласных: {Regex.Matches(text, @"[бвгджзйклмнпрстфхцчшщБВГДЖЗЙКЛМНПРСТФХЦЧШЩ]").Count}");

            // ЧАСТЬ 5: СТРОКИ, НАЧИНАЮЩИЕСЯ С "И"
            // ^ — начало строки, RegexOptions.IgnoreCase — без учёта регистра
            Console.WriteLine("\nЧАСТЬ 5: СТРОКИ, НАЧИНАЮЩИЕСЯ С 'И'");
            FindLinesStartingWith(lines, "И");

            // ЧАСТЬ 6: СТРОКИ, ОКАНЧИВАЮЩИЕСЯ НА "," ИЛИ "."
            // $ — конец строки
            Console.WriteLine("\nЧАСТЬ 6: СТРОКИ, ОКАНЧИВАЮЩИЕСЯ НА ',' ИЛИ '.'");
            FindLinesEndingWith(lines, @"[,.]");

            // ЧАСТЬ 7: ЗАМЕНА "снег/снежн" -> "ЛЁДН"
            // \b — граница слова, RegexOptions.IgnoreCase — заменяем в любом регистре
            Console.WriteLine("\nЧАСТЬ 7: ЗАМЕНА 'снег' -> 'ЛЁД' и 'снежн' -> 'ЛЁДН'");
            string modified = Regex.Replace(text, @"\bснежн", "ЛЁДН", RegexOptions.IgnoreCase);
            modified = Regex.Replace(modified, @"\bснег\w*", "ЛЁД", RegexOptions.IgnoreCase);
            Console.WriteLine($"Результат (первые 150 символов):\n{modified.Substring(0, Math.Min(150, modified.Length))}...\n");

            // ЧАСТЬ 8: СЛОВА С БОЛЬШОЙ БУКВЫ
            // [А-ЯЁ] — заглавная буква, [а-яё]* — ноль или более строчных
            Console.WriteLine("ЧАСТЬ 8: СЛОВА С БОЛЬШОЙ БУКВЫ И СЛОВА ИЗ 5 БУКВ");
            ShowMatches(text, @"\b[А-ЯЁ][а-яё]*\b", "слова с большой буквы", 3);
            ShowMatches(text, @"\b[а-яА-ЯёЁ]{5}\b", "слова из 5 букв");

            // ЧАСТЬ 9: СЛОВА, НАЧИНАЮЩИЕСЯ НА "бе" (новая часть)
            // Ищем слова, начинающиеся с сочетания "бе" — без учёта регистра
            Console.WriteLine("\nЧАСТЬ 9: СЛОВА, НАЧИНАЮЩИЕСЯ НА 'БЕ'");
            ShowMatches(text, @"\b[бБ][еЕ][а-яА-ЯёЁ]*\b", "слова на 'бе'");

            // ЧАСТЬ 10: ПОВТОРЯЮЩИЕСЯ СЛОВА ПОДРЯД (новая часть)
            // (\b\w+\b) — первое слово, \s+ — пробел, \1 — то же слово снова
            Console.WriteLine("\nЧАСТЬ 10: ПОВТОРЯЮЩИЕСЯ СЛОВА ПОДРЯД");
            var doubles = Regex.Matches(text, @"\b(\w+)\s+\1\b", RegexOptions.IgnoreCase);
            Console.WriteLine(doubles.Count > 0
                ? $"Найдено повторений: {doubles.Count}"
                : "Повторяющихся слов не найдено");

            // ЧАСТЬ 11: САМОЕ ДЛИННОЕ СЛОВО (новая часть)
            // Перебираем все слова и находим наибольшее по длине
            Console.WriteLine("\nЧАСТЬ 11: САМОЕ ДЛИННОЕ СЛОВО");
            string longest = "";
            foreach (Match m in words)
                if (m.Value.Length > longest.Length) longest = m.Value;
            Console.WriteLine($"Самое длинное слово: '{longest}' ({longest.Length} букв)");

            // ИТОГИ
            Console.WriteLine("\n" + new string('=', 50));
            Console.WriteLine($"ИТОГИ: строк {lines.Length} | символов {text.Length} | слов {words.Count}");
            Console.WriteLine(new string('=', 50));
        }

        // Выводит найденные совпадения по шаблону с описанием
        static void ShowMatches(string text, string pattern, string description, int max = 10)
        {
            var matches = Regex.Matches(text, pattern, RegexOptions.IgnoreCase);
            Console.WriteLine($"\n{description}: {matches.Count}");
            for (int i = 0; i < Math.Min(matches.Count, max); i++)
                Console.WriteLine($"   {matches[i].Value}");
        }

        // Возвращает первые count слов из коллекции совпадений
        static string[] GetFirstWords(MatchCollection matches, int count)
        {
            var result = new string[Math.Min(matches.Count, count)];
            for (int i = 0; i < result.Length; i++) result[i] = matches[i].Value;
            return result;
        }

        // Находит строки, начинающиеся с указанного слова
        static void FindLinesStartingWith(string[] lines, string word)
        {
            int found = 0;
            foreach (var line in lines)
                if (Regex.IsMatch(line, $@"^{word}.*", RegexOptions.IgnoreCase))
                { Console.WriteLine($"   {line}"); found++; }
            if (found == 0) Console.WriteLine("   Не найдено");
        }

        // Находит строки, оканчивающиеся на указанные символы
        static void FindLinesEndingWith(string[] lines, string chars)
        {
            int found = 0;
            foreach (var line in lines)
                if (Regex.IsMatch(line.Trim(), $@"{chars}$"))
                { Console.WriteLine($"   {line}"); found++; }
            if (found == 0) Console.WriteLine("   Не найдено");
        }
    }
}
