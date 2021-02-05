using System;
using Sprocket.Text.Ascii;

#nullable enable

namespace example
{
    class Program
    {
        static void Main(string[] args)
        {
            Calendar c = new();
            string s = c.Render(new DateTime(2021,11,01));
            Console.WriteLine(s);
        }

    }
}
