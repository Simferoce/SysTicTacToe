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
        private const int SIZE = 3;
        private bool controleWindow = true;
        private Joueur joueurCourant;
        private const char CHARCTERE_VIDE = ' ';
        private const char PARTIE_FINIE = '0';
        private const char AUCUN_GAGNANT = '1';
        private bool partieFinie = false;
        Joueur p1 = new Joueur('O');
        Joueur p2 = new Joueur('X');
        private Thread joueur1;
        private Thread joueur2;
        char[,] tab = new char[SIZE, SIZE];
        public Game()
        {
            joueur1 = new Thread(new ThreadStart(p1.Run));
            joueur2 = new Thread(new ThreadStart(p2.Run));
            for (int i = 0; i < tab.GetLength(0) ; i++)
            {
                for (int j = 0; j < tab.GetLength(1); j++)
                {
                    tab[i, j] = CHARCTERE_VIDE;
                }
            }
            joueurCourant = p1;
        }
        public void Run()
        {

            joueur1.Start();
            joueur2.Start();
            while (!partieFinie)
            {
                if (controleWindow)
                {
                    Show();
                    Console.WriteLine("Joueur " + joueurCourant.Id + " veuillez entrer un coup. lig,col");
                    DonnerControle(joueurCourant);
                }
                if (joueurCourant.inputs != "")
                {
                    ReprendreControle(joueurCourant);
                    int ligne = 0;
                    int colonne = 0;
                    if (VerifierInput(joueurCourant.inputs, out ligne, out colonne))
                    {
                        tab[ligne, colonne] = joueurCourant.Id;
                        char resultat = VerifierFinGame();
                        if (resultat == '0')
                            FinPartieNulle();
                        else if (resultat == p1.Id || resultat == p2.Id)
                            FinPartieGagnant(resultat == p1.Id ? p1 : p2);
                        else
                        {
                            joueurCourant.inputs = "";
                            joueurCourant = joueurCourant == p1 ? p2 : p1;
                            while (Console.KeyAvailable)
                                Console.ReadKey(true);
                        }
                    }
                    else
                    {
                        Show();
                        Console.WriteLine("Joueur " + joueurCourant.Id + "Veuillez entrer une coordonnée valide. lig,col");
                        joueurCourant.inputs = "";
                        DonnerControle(joueurCourant);
                    }
                }
            }
        }
        private void Show()
        {
            Console.Clear();
            for(int i =0; i < SIZE; ++i)
            {
                Console.Write("|");
                for (int j = 0; j < SIZE; j++)
                {
                    Console.Write(tab[i,j] + " |");
                }
                Console.Write('\n');
            }
        }

        private void DonnerControle(Joueur joueur)
        {
            controleWindow = false;
            joueur.controleWindow = true;
        }

        private void ReprendreControle(Joueur joueur)
        {
            joueurCourant.controleWindow = false;
            controleWindow = true;
        }

        private bool VerifierInput(string input, out int ligne, out int colonne)
        {
            string[] ligCol = input.Split(',');
            ligne = -1;
            colonne = -1;
            if (ligCol.Length != 2)
                return false;
            if (!int.TryParse(ligCol[0], out int lig))
                return false;
            if (!int.TryParse(ligCol[1], out int col))
                return false;
            if (lig < 0 || lig > SIZE - 1)
                return false;
            if (col < 0 || col > SIZE - 1)
                return false;
            if (tab[lig, col] != CHARCTERE_VIDE)
                return false;
            ligne = lig;
            colonne = col;
            return true;
        }

        private char VerifierFinGame()
        {
            bool partieComplete = true;
            bool ligneWin = true;
            char ligne = tab[0, 0];
            bool colonneWin = true;
            char colonne = tab[0, 0];
            bool diagonnaleGaucheWin = true;
            char diagonnaleGauche = tab[0, 0];
            bool diagonnaleDroiteWin = true;
            char diagonnaleDroite = tab[SIZE - 1, 0];
            for (int i = 0; i < SIZE; i++)
            {
                ligneWin = true;
                colonneWin = true;
                ligne = tab[i, 0];
                colonne = tab[0, i];
                if (diagonnaleDroite != tab[SIZE - i - 1, i] || diagonnaleDroite == CHARCTERE_VIDE)
                    diagonnaleDroiteWin = false;
                if (diagonnaleGauche != tab[i, i] || diagonnaleGauche == CHARCTERE_VIDE)
                    diagonnaleGaucheWin = false;
                for (int j = 0; j < SIZE; j++)
                {
                    if (ligne != tab[i, j] || ligne == CHARCTERE_VIDE)
                        ligneWin = false;
                    if (colonne != tab[j, i] || colonne == CHARCTERE_VIDE)
                        colonneWin = false;
                    if (tab[i, j] == CHARCTERE_VIDE)
                        partieComplete = false;
                }
                if (ligneWin)
                    return colonne;
                if (colonneWin)
                    return ligne;
            }
            if (diagonnaleGaucheWin)
                return diagonnaleGauche;
            if (diagonnaleDroiteWin)
                return diagonnaleDroite;
            if (partieComplete)
                return PARTIE_FINIE;
            return AUCUN_GAGNANT;
        }

        private void FinPartieNulle()
        {
            partieFinie = true;
            joueur1.Abort();
            joueur2.Abort();
            Show();
            Console.WriteLine("Partie nulle");
        }
        private void FinPartieGagnant(Joueur joueur)
        {
            partieFinie = true;
            joueur1.Abort();
            joueur2.Abort();
            Show();
            Console.WriteLine("Joueur " + joueur.Id + " est le gagnant!");
        }
    }
}
