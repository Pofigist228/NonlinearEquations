using System;

namespace NonlinearEquations
{
    class Program
    {
        // Исходная функция f(x) = e^x - 2x - 2
        static double F(double x) => Math.Exp(x) - 2 * x - 2;

        // Первая производная f'(x) = e^x - 2
        static double DF(double x) => Math.Exp(x) - 2;

        // Отображение для метода простой итерации phi(x) = ln(2x + 2)
        static double Phi(double x) => Math.Log(2 * x + 2);

        static void Main()  
        {
            double eps = 0.0001;
            Console.WriteLine("Вариант 3: e^x - 2x - 2 = 0");
            Console.WriteLine("Отрезок: [0, 2], eps = 0.0001\n");

            Console.WriteLine("=== Метод бисекции ===");
            Bisection(0, 2, eps);

            Console.WriteLine("\n=== Метод простой итерации ===");
            SimpleIteration(1, eps);

            Console.WriteLine("\n=== Метод Ньютона ===");
            Newton(2, eps);

            Console.WriteLine("\n=== Метод секущих ===");
            Secant(1, 2, eps);
        }

        static void Bisection(double a, double b, double eps)
        {
            int n = 0;
            double c = 0;
            Console.WriteLine("n\t a\t\t b\t\t c\t\t f(c)\t\t b-a");
            while (n < 100)
            {
                c = (a + b) / 2;
                double fc = F(c);
                Console.WriteLine($"{n}\t {a:F6}\t {b:F6}\t {c:F6}\t {fc:E}\t {b - a:F6}");

                if (Math.Abs(fc) < eps || (b - a) / 2 < eps)
                    break;

                if (F(a) * fc < 0)
                    b = c;
                else
                    a = c;

                n++;
            }
            Console.WriteLine($"Корень: {c:F6}, невязка: {F(c):E}, итераций: {n}");
        }

        static void SimpleIteration(double x0, double eps)
        {
            double x = x0, xNext;
            int n = 0;
            Console.WriteLine("n\t x_n\t\t x_{n+1}\t |x_{n+1}-x_n|\t f(x_{n+1})");
            do
            {
                xNext = Phi(x);
                Console.WriteLine($"{n}\t {x:F6}\t {xNext:F6}\t {Math.Abs(xNext - x):E}\t {F(xNext):E}");
                if (Math.Abs(xNext - x) < eps)
                    break;
                x = xNext;
                n++;
            } while (n < 100);
            Console.WriteLine($"Корень: {xNext:F6}, невязка: {F(xNext):E}, итераций: {n}");
        }

        static void Newton(double x0, double eps)
        {
            double x = x0, xNext;
            int n = 0;
            Console.WriteLine("n\t x_n\t\t f(x_n)\t\t f'(x_n)\t x_{n+1}\t |x_{n+1}-x_n|");
            do
            {
                double fx = F(x);
                double dfx = DF(x);
                xNext = x - fx / dfx;
                Console.WriteLine($"{n}\t {x:F6}\t {fx:E}\t {dfx:F6}\t {xNext:F6}\t {Math.Abs(xNext - x):E}");
                if (Math.Abs(xNext - x) < eps)
                    break;
                x = xNext;
                n++;
            } while (n < 100);
            Console.WriteLine($"Корень: {xNext:F6}, невязка: {F(xNext):E}, итераций: {n}");
        }

        static void Secant(double x0, double x1, double eps)
        {
            double xPrev = x0, xCurr = x1, xNext;
            int n = 0;
            Console.WriteLine("n\t x_{n-1}\t x_n\t\t f(x_n)\t\t x_{n+1}\t |x_{n+1}-x_n|");
            do
            {
                double fPrev = F(xPrev);
                double fCurr = F(xCurr);
                xNext = xCurr - fCurr * (xCurr - xPrev) / (fCurr - fPrev);
                Console.WriteLine($"{n}\t {xPrev:F6}\t {xCurr:F6}\t {fCurr:E}\t {xNext:F6}\t {Math.Abs(xNext - xCurr):E}");
                if (Math.Abs(xNext - xCurr) < eps)
                    break;
                xPrev = xCurr;
                xCurr = xNext;
                n++;
            } while (n < 100);
            Console.WriteLine($"Корень: {xNext:F6}, невязка: {F(xNext):E}, итераций: {n}");
        }
    }
}