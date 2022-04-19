using System;

namespace ConsoleApp1
{
    class ABC
    {
        public static int x;
        static ABC()
        {
            Console.WriteLine("static ABC");
            Console.WriteLine("x="+x);
        }
        public ABC()
        {
            Console.WriteLine("public ABC");
            Console.WriteLine("x=" + x);
        }

        public ABC(int id)
        {
            Console.WriteLine("public ABC(int id)");
            x = id;
            Console.WriteLine("x=" + x);
        }
    }
    class Program
    {
        public enum Color
        {
            Red,
            Blue,
            Green
        };
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            //Object c;
            //Enum.TryParse(typeof(Color), "Green", true, out c);
            //if (c!= null && c.GetType() == typeof(Color))
            //    Console.WriteLine("c: " + c);
            Console.WriteLine("===static==================");
            ABC.x = 1;
            Console.WriteLine("====x=================");
            ABC x = new ABC(8);
            Console.WriteLine("====y=================");
            ABC y = new ABC();
            Console.WriteLine("=====z================");
            ABC z = new ABC(6);
        }
    }
}
