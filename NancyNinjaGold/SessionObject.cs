using System;
using System.Collections.Generic;

namespace NancyNinjaGold
{
    public class SessionObject
    {
        public int Gold { get; set; }
        public List<string> Activities {get; set; }

        public SessionObject()
        {
            Console.WriteLine("in the contstructor");
            Gold = 0;
            Activities = new List<string>();
        }
    }
}