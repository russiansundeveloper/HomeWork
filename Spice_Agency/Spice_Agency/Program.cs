using System;
using System.Globalization;

namespace Spice_Agency
{
    //интерфейс-задаем значения сторон треугольника
    public interface ISetSide
    {
        void SetSide();
    }

    //интерфейс-получить площадь треугольника
    public interface IGetSquare
    {
        void GetSquare();
    }

    //класс треугольник
    public class Triangle : ISetSide, IGetSquare
    {
        //реализация интерфейсов
        public void SetSide()
        {
            Console.WriteLine("Введите 1ю сторону треугольника");
            side_a = Convert.ToDouble(Console.ReadLine(), CultureInfo.InvariantCulture);
                //double.TryParse(Console.ReadLine(), out side_a);

            Console.WriteLine("Введите 2ю сторону треугольника");
            side_b = Convert.ToDouble(Console.ReadLine(), CultureInfo.InvariantCulture);
                //double.TryParse(Console.ReadLine(), out side_b);

            Console.WriteLine("Введите 3ю сторону треугольника");
            side_c=  Convert.ToDouble(Console.ReadLine(), CultureInfo.InvariantCulture);
                //double.TryParse(Console.ReadLine(), out side_c);
        }

        public void GetSquare()
        {
            Console.WriteLine("вы ввели стороны {0} {1} {2}", side_a, side_b, side_c);

            //вычисления по формуле герона
            //http://2mb.ru/matematika/geometriya/ploshhad-treugolnika-po-trem-storonam/

            double half_p = (side_a + side_b + side_c)/2;
            square = Math.Sqrt(half_p*(half_p - side_a)*(half_p - side_b)*(half_p - side_c));

            /////
            
            if (!Double.IsNaN(square))
            {
                Console.WriteLine("\nПлощадь треугольника равна = {0:F1} у.е.^2", square);
            }
            else{Console.WriteLine("Такого треугольника не существует!");}

        }

        //sides of triangle
        private double side_a;
        private double side_b;
        private double side_c;

        //triangle square
        private double square;

    }

    class Program
    {
     
        //main function of program
        static void Main(string[] args)
        {
            Triangle trng = new Triangle();

            trng.SetSide();
            trng.GetSquare();

            Console.ReadLine();
        }
    }
}
