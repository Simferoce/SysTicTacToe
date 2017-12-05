using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TicTacToe
{
    class Joueur
    {
        private char id;
        public char Id { get { return id; } }
        public string inputs = "";
        public bool controleWindow = false;
        public Joueur(char id)
        {
            this.id = id;
        }
        public void Run()
        {
            while (true)
            {
                if (controleWindow && inputs == "")
                {
                    inputs = Console.ReadLine();
                }
            }
        }
    }
}
