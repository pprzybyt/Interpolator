using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interpolator
{
    class Program
    {
        private int n;
        private int pointCount;
        private double[] areas;
        private List<double[]> points;
        private double[] values;
        private char[] variables;
        private double[] resultPoint;
        private double denominator;

        public Program()
        {
            Dimension();
            Console.WriteLine();

            this.pointCount = Convert.ToInt16(Math.Pow(2, n - 1));
            this.areas = new double[pointCount];
            this.variables = new char[n];
            this.resultPoint = new double[n];


            for (int i = 0; i < n; i++)
            {
                this.variables[i] = Convert.ToChar(65 + i);
            }

            this.values = new double[pointCount];

            this.points = new List<double[]>();

            for (int i = 0; i < n - 1; i++)
                points.Add(new double[2]);

            InitLines();
            InitValues();
            SearchResult();
        }

        private void Dimension()
        {
            int dimension;
            try
            {
                Console.Write("Type in number of dimensions:  ");
                dimension = int.Parse(Console.ReadLine());
                if (dimension > 1)
                    this.n = dimension;
                else
                {
                    Console.WriteLine("Dimension must be greater than 1!");
                    Dimension();
                }
            }
            catch (FormatException e)
            {
                Console.WriteLine("Dimension must be an inteeger!");
                Dimension();
            }
        }

        private void Calculate()
        {
            denominator = 1;

            for (int i = 0; i < n - 1; i++)
            {
                denominator *= points[i][1] - points[i][0];
            }

            int counter;

            for (int i = 0; i < pointCount; i++)
            {
                areas[i] = 1;
                counter = i;

                for (int j = 0; j < n - 1; j++)
                {
                    areas[i] *= points[j][counter % 2] - resultPoint[j];

                    counter /= 2;
                }

                areas[i] = areas[i] / denominator;

                areas[i] = Math.Abs(areas[i]);

            }

            for (int i = 0; i < pointCount; i++)
            {
                resultPoint[n - 1] += values[i] * areas[pointCount - 1 - i];
            }
        }

        public override string ToString()
        {
            return "Value of interpolation for chosen point is: " + Convert.ToString(Math.Round(resultPoint[n - 1], 2));
        }

        private void SearchResult()
        {
            double value;
            Console.WriteLine("Type in searched point coordinates  ");


            for (int i = 0; i < n - 1; i++)
            {
                Console.Write("{0,-25}  |  {1,-5}", " (" + Convert.ToString(Math.Min(points[i][0], points[i][1])) + ";" + Convert.ToString(Math.Max(points[i][0], points[i][1])) +")",  variables[i] + ": ");
                try
                {
                    value = Double.Parse(Console.ReadLine());

                    while (value <= Math.Min(points[i][0], points[i][1]) || value >= Math.Max(points[i][0], points[i][1]))
                    {
                        Console.WriteLine("Value should be included in range (" + Convert.ToString(Math.Min(points[i][0], points[i][1])) + ";" + Convert.ToString(Math.Max(points[i][0], points[i][1]) + ")"));
                        Console.Write(" " + variables[i] + ":   ");
                        value = Double.Parse(Console.ReadLine());
                    }

                    resultPoint[i] = value;
                }
                catch (FormatException)
                {
                    ExeptionBlock();
                    i--;
                }

            }
            Console.WriteLine();

            Calculate();
        }

        private void ExeptionBlock()
        {
            Console.WriteLine();
            Console.WriteLine("You have to use right format of floating-point number.");
            Console.WriteLine("E.g. xx,x (where x - digit). Please do use \",\" instead of \".\"");
            Console.WriteLine();
        }

        private void InitValues()
        {

            Console.WriteLine("Type in values of defined points:  ");

            DisplayPoints();

            Console.WriteLine();
        }

        private void DisplayPoints()
        {

            int counter;
            string coordinates;

            for (int i = 0; i < pointCount; i++)
            {
                coordinates = "";
                counter = i;

                for (int j = 0; j < n - 1; j++)
                {
                    coordinates += Convert.ToString(points[j][counter % 2]);
                    if (j != n - 2)
                        coordinates += ";";

                    counter /= 2;
                }

                Console.Write("{0,-25}  |  {1,-5}", " (" + coordinates + ") ", variables.Last() + Convert.ToString(i) + ": ");
                try
                {
                    values[i] = Double.Parse(Console.ReadLine());
                }
                catch (FormatException)
                {
                    ExeptionBlock();
                    i--;
                }
            }
        }

        private void InitLines()
        {
            int counter = 0;
            int q = 0;

            if (n > 2)
                Console.WriteLine("Type in coordinates of lines:  ");
            else
                Console.WriteLine("Type in coordinates of points:  ");

            for (int i = 0; i < 2 * (n - 1); i++)
            {
                if (i % 2 == 0)
                    q = 0;
                else
                    q = 1;

                Console.Write(" " + variables[counter] + Convert.ToString(q) + ":   ");
                try
                {
                    points[counter][q] = Double.Parse(Console.ReadLine());
                    if (q == 1 && points[counter][0] == points[counter][1])
                    {
                        Console.WriteLine(+ variables[counter] + "1 must be different from " + variables[counter] + "0");
                        i--;
                        q = 0;
                    }

                }
                catch (FormatException)
                {
                    ExeptionBlock();
                    i--;
                    if (q == 1)
                        counter--;
                }


                if (q == 1)
                    counter++;
            }
            Console.WriteLine();
        }

        static void Main(string[] args)
        {

            Program p = new Program();
            Console.WriteLine(p);
            Console.ReadKey();

        }
    }
}
