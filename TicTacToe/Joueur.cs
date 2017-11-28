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
        private int numJoueur = 0;
        public int NumJoueur { get { return numJoueur; } }
        public bool utiliseConsole = false;
        private List<string> infoToSave = new List<string>();
        private Thread save;
        public Joueur(int numJoueur)
        {
            this.numJoueur = numJoueur;
            save = new Thread(new ThreadStart(Save));
        }
        private void AddInfo(string info)
        {
            infoToSave.Add(info);
        }
        public void Run()
        {
            save.Start();
            while (true)
            {
                if (utiliseConsole)
                {
                    Console.ReadLine();
                    utiliseConsole = false;
                }
            }
        }
        private void Save()
        {
            while (true)
            {
                if (infoToSave.Count != 0)
                {
                    //Save
                }
            }
        }
    }
}
