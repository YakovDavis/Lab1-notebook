using System;
using System.Collections.Generic;

namespace Notebook
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Note> notebook = new List<Note>();
            int exitFlag = 0;

            while (exitFlag == 0)
            {
                exitFlag = CommandInterpreter.RunCommand(Console.ReadLine(), notebook);
            }
        }
    }

    
}
