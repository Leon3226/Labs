using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1
{
    public class ConsoleComplexDialog
    {
        public void Run()
        {
            Cycle();
        }

        private void Cycle()
        {
            //Строка текущего ввода
            string input = string.Empty;

            do
            {

                ComplexNumber firstNumber;
                ComplexNumber secondNumber;
                ComplexNumber result;

                //Это локальный метод. Вызывается ниже для вывода всего выражения
                void outResult(char oper)
                {
                    if (firstNumber != null && secondNumber != null && result != null)
                        Console.WriteLine($"{firstNumber} {oper} {secondNumber} = {result}");
                }

                //Мурыжим пользователя пока он не введет первое число
                bool firstIsComplex;
                do
                {
                    Console.WriteLine("Введите первое комплексное число");
                    input = Console.ReadLine();
                    //По поводу работы метода TryParse обращайтесь в сам метод, там описано, как он работает.
                    firstIsComplex = ComplexNumber.TryParse(input, out firstNumber);
                    if (!firstIsComplex)
                    {
                        Console.WriteLine("Это не комплексное число, попробуйте ещё раз");
                    }

                } while (!firstIsComplex);

                //То же самое для второго числа
                bool secondIsComplex;
                do
                {
                    Console.WriteLine("Введите второе комплексное число");
                    input = Console.ReadLine();
                    secondIsComplex = ComplexNumber.TryParse(input, out secondNumber);
                    if (!secondIsComplex)
                    {
                        Console.WriteLine("Это не комплексное число, попробуйте ещё раз");
                    }

                } while (!secondIsComplex);

                //Спрашиваем оператор
                bool isOperationSymbol;
                do
                {
                    Console.WriteLine(@"Введите оператор (+ / - / *)");
                    input = Console.ReadLine();
                    //FirstOrDefault возвращает первый элемент коллекции (в данном случае первый символ, так как применяем к строке).
                    //Если же такового нет, возвращает значение по умолчанию. Оно нам не подойдет тут, так что норм
                    var oper = input.FirstOrDefault();

                    //проверяется какой у нас оператор
                    switch (oper)
                    {
                        case '+':
                            //Нужно для продолжения или непродолжения цикла
                            isOperationSymbol = true;
                            //Вот тут происходит самая магия. У нас ComplexNumber - это пользовательский класс, и тем не менее мы можем применять операторы +, - и прочие к нему
                            //Это происходит за счет перегрузки операторов. Как это работает описано в самом классе ComplexNumber.
                            result = firstNumber + secondNumber;
                            //И собственно выводим результат.
                            outResult(oper);
                            break;
                        case '-':
                            isOperationSymbol = true;
                            result = firstNumber - secondNumber;
                            outResult(oper);
                            break;
                        case '*':
                            isOperationSymbol = true;
                            result = firstNumber * secondNumber;
                            outResult(oper);
                            break;
                        default:
                            Console.WriteLine("Это не оператор или этого оператора нет в списке доступных");
                            isOperationSymbol = false;
                            break;
                    }

                } while (!isOperationSymbol);


            } while (true);
        }
    }
}
