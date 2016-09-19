using System;

namespace NancyNumberGame
{
    public class SessionWrapper
    {
        public int Number { get; set; }

        public SessionWrapper()
        {
            Console.WriteLine("In the constructor");
            Console.WriteLine(Number);
        }
    }
}