using System;
using System.Collections.Generic;
using System.Text;

namespace Banking.Backend
{
    public static class Helpers
    {
        public static void Pause()
        {
            Console.WriteLine("Naciśnij dowolny klawisz by powrócić do menu");
            Console.ReadKey();
        }
        public static void Separator()
        {
            Console.WriteLine("".PadRight(Console.WindowWidth - 1, '-'));
        }
        public static void Header()
        {
            Console.Clear();

            Console.WriteLine("┌".PadRight(Console.WindowWidth - 3, '─') + "┐");

            string content = "Aplikacja bankowa v1.1";
            int margin = (Console.WindowWidth - content.Length) / 2;
            Console.WriteLine("│".PadRight(margin - 1) + content + "│".PadLeft(margin - 1));

            Console.WriteLine("└".PadRight(Console.WindowWidth - 3, '─') + "┘");
        }
    }
}
