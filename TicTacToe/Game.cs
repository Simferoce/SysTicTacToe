using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TicTacToe
{
    class Game
    {
        Joueur p1 = new Joueur(1);
        Joueur p2 = new Joueur(2);
        char[,] tab = new char[3, 3];
        Game()
        {
            for (int i = 0; i < tab.GetLength(0) ; i++)
            {
                for (int j = 0; j < tab.GetLength(1); j++)
                {
                    tab[i, j] = ' ';
                }
            }
        }
        public void Run()
        {
            bool isRunning = true;
            Thread t1 = new Thread(new ThreadStart(p1.Run));
            Thread t2 = new Thread(new ThreadStart(p2.Run));
            Thread show = new Thread(new ThreadStart(Show));
            t1.Start();
            t2.Start();
            show.Start();
            int i = 0;
            while (isRunning)
            {
                i++;
                if (i > 1000000000 && !p1.utiliseConsole)
                {
                    Console.WriteLine("Joueur " + p1.NumJoueur + " entrez quelque chose O.o.o.o.o.o.o.O");
                    p1.utiliseConsole = true;
                    i = 0;
                }
            }
            t1.Abort();
            t2.Abort();
        }
        private void Show()
        {
            while (true)
            {
                if (!(p1.utiliseConsole || p2.utiliseConsole))
                {
                    Console.Clear();
                    Console.WriteLine("1");
                    Thread.Sleep(1000);
                }
            }
        }
    }
}
