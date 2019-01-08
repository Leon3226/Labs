using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Lab1
{
    public class ComplexNumber
    {
        //Это нужно для того чтобы можно было распарсить вводную строку в собственно само комплексное число. Не уверен что вы захотите разбирать херли тут написано, но вас и вряд ли спросят.
        private const string complexRegex = @"(?'complex'(?'real'[-0-9]+)\s*(?'operator'\+|\-)\s*(?'imaginary'[-0-9]+)i)";

        //Тут хранится действительная часть комплексного числа
        public int Real { get; set; }
        //А тут - мнимая, которая стоит перед i
        public int Imaginary { get; set; }

        //Пустой конструктор, чтобы можно было создавать класс без аргументов
        public ComplexNumber()
        {

        }

        //И с параметрами, чтобы создавать его можно было удобнее
        public ComplexNumber(int real, int imaginary)
        {
            Real = real;
            Imaginary = imaginary;
        }

        /*
         * Такс, вот тут подробнее
         * Сначала посмотрите метод Parse
         * Этот метод нужен для того чтобы из строки s получить ComplexNumber
         * Отличие состоит в том, что значение тут не возвращается, а передается через внешний параметр с модификатором out.
         * Возвращает же этот метод булеан, который говорит о том, удачно ли прошёл парсинг
         * Потому он и называется TRYParse.
         * Как его использование выглядит извне, можно посмотреть где-нибудь в коде.
        */
        public static bool TryParse(string s, out ComplexNumber result)
        {
            result = Parse(s);
            return result != null;
        }

        //Принимает строку s и возвращает ComplexNumber если его удалось распарсить, и null, если не удалось. Собственно всё
        //Вид метода, как и метода TryParse сделан именно так потому что примерно так же стилизованы методы Parse и TryParse у системных базовых типов
        //Правда они как правило структуры, а не классы, но не суть, у вас в задании было написано сделать класс.
        public static ComplexNumber Parse(string s)
        {
            var match = Regex.Match(s, complexRegex);
            if (!match.Success)
                return null;

            var real = match?.Groups["real"]?.Value;
            var imaginary = match?.Groups["imaginary"]?.Value;
            var oper = match?.Groups["operator"]?.Value;

            if (int.TryParse(real, out int realNum) && int.TryParse(imaginary, out int imaginaryNum))
            {
                if (oper == "-")
                    imaginaryNum = 0 - imaginaryNum;
                return new ComplexNumber(realNum, imaginaryNum);
            }

            return null;
        }

        // Тут мы переопределяем стандартный метод ToString, чтобы указать нашему классу, что он должен возвращать, когда мы, например, пытаемся вывести его в консоль
        // Вывод будет примерно в формате a + bi
        public override string ToString()
        {
            if (Real == 0 && Imaginary == 0)
                return "0";

            //Вот эта финтифлюшка необязательна, можно было бы обойтись просто строками, но StringBuilder собирает строки куда быстрее и в таких случаях его не использовать считается дурным тоном. 
            var builder = new StringBuilder();

            if (Real != 0)
            {
                //Конструкция $"" инициирует интерполяцию строк, как она тут работает достаточно интуитивно понятно.
                builder.Append($"{Real}");
                if (Imaginary > 0)
                {
                    builder.Append(" + ");
                }
                if (Imaginary < 0)
                {
                    builder.Append(" - ");
                }
            }

            if (Imaginary != 0)
            {
                builder.Append($"{Math.Abs(Imaginary)}i");
            }

            return builder.ToString();
        }

        //Собственно вот это и есть перегрузка операторов. Краткая суть состоит в том, что когда мы применяем оператор (в данном случае +) к операндам c1 и c1 типа ComplexNumber
        // (Простым языком: складываем два комплексных числа), то этот оператор считает результат по нижеописанной логике и возвращает результат в виде ComplexNumber (т.е. результирующее число)
        public static ComplexNumber operator +(ComplexNumber c1, ComplexNumber c2)
        {
            return new ComplexNumber(c1.Real + c2.Real, c1.Imaginary + c2.Imaginary);  
        }
        public static ComplexNumber operator -(ComplexNumber c1, ComplexNumber c2)
        {
            return new ComplexNumber(c1.Real - c2.Real, c1.Imaginary - c2.Imaginary);
        }
        public static ComplexNumber operator *(ComplexNumber c1, ComplexNumber c2)
        {
            var real = c1.Real * c2.Real - c1.Imaginary * c2.Imaginary;
            var imaginary = c1.Real * c2.Imaginary + c1.Imaginary * c2.Real;
            return new ComplexNumber(real, imaginary);
        }
    }
}
