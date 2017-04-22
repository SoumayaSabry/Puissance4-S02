using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Puissance4_GUI;

namespace Puissance4_SABRY_Soumaya
{
    class Program
    {
        static string Interface(Fenetre gui, int nombreDejoueur)
        {
            gui.changerMessage(" PUISSANCE 4");
            gui.changerMessage("Nom du Joueur " + nombreDejoueur + " ==> ");
            string nomJoueur = Console.ReadLine();
            return nomJoueur;
        }
        static void AfficherMatrice(int[,] matrice)
         {
             if (matrice == null)
             {
                 Console.WriteLine("matrice null");
             }
             else
             {
                 if (matrice.Length == 0)
                 {
                     Console.WriteLine("matrice vide");
                 }

                 else
                 {
                     int tailleligne = matrice.GetLength(0) - 1;
                     int taillecolonne = matrice.GetLength(1) - 1;
                     int nbEntiere = 1;
                     Console.WriteLine("      1  2  3  4  5  6  7  ");
                     Console.WriteLine("    ---------------------- ");
                     for (int i1 = 0; i1 <= tailleligne; i1++)
                     {
                         Console.Write(" " + nbEntiere + " | ");
                         nbEntiere++;
                         for (int i2 = 0; i2 <= taillecolonne; i2++)
                         {

                             if (i2 != taillecolonne)
                             {

                                 if (matrice[i1, i2] < 10)
                                 {
                                     Console.Write(" " + matrice[i1, i2] + "|");
                                 }
                                 else Console.Write(matrice[i1, i2] + "|");
                             }
                             else
                             {
                                 if (matrice[i1, i2] < 10)
                                 {
                                     Console.Write(" " + matrice[i1, i2] + "|");
                                 }
                                 else Console.Write(matrice[i1, i2] + "|");
                             }

                         }
                         Console.WriteLine();

                     }
                     Console.WriteLine("    ----------------------");
                 }
             }
         }
        static void DemendeJoueur(int[,] matrice, string nomJoueur, Fenetre gui, int nombreDejoueur)
        {
            bool estChanger = false;
            while (estChanger == false)
            {
                gui.changerMessage("C'est le tour de " + nomJoueur.ToUpper() + " Veuillez choisir un numero de la colonne ==>");
                int choise1 = 0;
                try
                {
                    choise1 = int.Parse(Console.ReadLine());
                }
                catch
                {
                    Console.WriteLine("Erreur !!!!");
                    gui.changerMessage("Mauvais nombre " + nomJoueur.ToUpper() + " Veuillez choisir un numero de la colonne ==>");
                    choise1 = TryCatch();

                }
                while (choise1 < 0 || choise1 > matrice.GetLength(1))
                {
                    gui.changerMessage("Mauvais nombre " + nomJoueur.ToUpper() + " Veuillez choisir un numero de la colonne ==>");
                    choise1 = TryCatch();
                }
                int i = matrice.GetLength(0) - 1;
                int indexChoisi = choise1 - 1;
                while (estChanger == false && i >= 0)
                {
                    if (matrice[i, indexChoisi] == 0)
                    {
                        matrice[i, indexChoisi] = nombreDejoueur;
                        estChanger = true;
                    }
                    i--;
                }
                if (estChanger == false)
                {
                    gui.changerMessage("la colenne est totalement rempli ..." + " Veuillez rechoisi une autre colonne ");
                    Console.ReadKey();

                }
            }

            gui.rafraichirGrille();
        }
        static bool PartieGagant(int[,] matrice, string nomJoueur1, string nomJoueur2, Fenetre gui)
        {
            bool estPartieGagant = false;
            for (int i = (matrice.GetLength(1) - 1); i > 0; i--)
            {
                for (int j = (matrice.GetLength(0) - 1); j > 0; j--)
                {
                    if (matrice[j, i] == 1)
                    {
                        if (1 == matrice[j, (i - 1)])
                        {
                            if (1 == matrice[(j - 1), i])
                            {
                                if (1 == matrice[(j - 1), (i - 1)])
                                {
                                    gui.changerMessage(" Félicitation " + nomJoueur1 + " Vous avez gagné");
                                    estPartieGagant = true;
                                }
                            }
                        }
                    }
                    if (matrice[j, i] == 2)
                    {
                        if (2 == matrice[j, (i - 1)])
                        {
                            if (2 == matrice[(j - 1), i])
                            {
                                if (2 == matrice[(j - 1), (i - 1)])
                                {
                                    gui.changerMessage(" Félicitation " + nomJoueur2 + " vous avez gagné");
                                    estPartieGagant = true;
                                }
                            }
                        }
                    }
                }
            }
            return estPartieGagant;
        }
        static void LigneDispart(int[,] matrice, Fenetre gui)
        {
            int index = 0;
            int index1ereLigne = matrice.GetLength(0) - 1;
            bool estrempli = true;
            while (estrempli == true && index < matrice.GetLength(1))
            {
                if (matrice[index1ereLigne, index] == 0)
                {
                    estrempli = false;
                }
                index++;
            }
            if (estrempli == true)
            {
                gui.changerMessage("la 1ere ligne est rempli totalement alors elle va disparetre, Appuyer sur ENTRE pour Continue... ");
                for (int i = (matrice.GetLength(1) - 1); i >= 0; i--)
                {
                    for (int j = (matrice.GetLength(0) - 1); j > 0; j--)
                    {
                        matrice[j, i] = matrice[(j - 1), i];
                    }
                }
                for (int i = (matrice.GetLength(1) - 1); i > 0; i--)
                {
                    matrice[0, i] = 0;
                }
                Console.ReadKey();
                // AfficherMatrice(matrice);
                gui.rafraichirGrille();
            }
        }
        static void IA(Fenetre gui, int[,] matrice, int level, bool IAsontDeux)
        {
            bool estChanger = false;
            Random rndOrdi = new Random();
            int choiseOrdi;
            while (estChanger == false)
            {
                choiseOrdi = rndOrdi.Next(0,6);
                #region reponse2
                if (level == 2)
                {
                    for (int colonne = (matrice.GetLength(1) - 1); colonne > 0; colonne--)
                    {
                        for (int ligne = (matrice.GetLength(0) - 1); ligne > 0; ligne--)
                        {

                            if (matrice[ligne, colonne] == 2 && matrice[(ligne - 1), colonne] == 2 && matrice[ligne, (colonne - 1)] == 2 && matrice[(ligne - 1), (colonne - 1)] == 0)
                            {
                                choiseOrdi = colonne - 1;
                            }
                            else if (matrice[ligne, colonne] == 2 && matrice[(ligne - 1), colonne] == 2 && matrice[ligne, (colonne + 1)] == 2 && matrice[(ligne - 1), (colonne + 1)] == 0)
                            {
                                choiseOrdi = colonne + 1;
                            }
                            else
                            {
                                if (matrice[ligne, colonne] == 2 && matrice[(ligne - 1), colonne] == 2 && matrice[ligne, (colonne - 1)] == 0 )
                                {
                                    choiseOrdi = colonne - 1;
                                }
                                else if (matrice[ligne, colonne] == 2 && matrice[(ligne - 1), colonne] == 2 && matrice[ligne, (colonne + 1)] == 0 )
                                {
                                    choiseOrdi = colonne + 1;
                                }
                                
                                else if (IAsontDeux == false && matrice[ligne, colonne] == 2 && matrice[(ligne - 1), colonne] == 0)
                                {
                                    choiseOrdi = colonne;
                                }
                            }
                        }
                    }
                }
                #endregion
                #region reponse3
                if ( level == 3)
                {
                    for (int colonne = (matrice.GetLength(1) - 1); colonne > 0; colonne--)
                    {
                        for (int ligne = (matrice.GetLength(0) - 1); ligne > 0; ligne--)
                        {
                            if (matrice[ligne, colonne] == 2 && matrice[(ligne - 1), colonne] == 2 && matrice[ligne, (colonne - 1)] == 2 && matrice[(ligne - 1), (colonne - 1)] == 0)
                            {
                                choiseOrdi = colonne - 1;
                            }
                            else if (matrice[ligne, colonne] == 2 && matrice[(ligne - 1), colonne] == 2 && matrice[ligne, (colonne + 1)] == 2 && matrice[(ligne - 1), (colonne + 1)] == 0)
                            {
                                choiseOrdi = colonne + 1;
                            }
                            else
                            {
                                if (matrice[ligne, colonne] == 1 && matrice[(ligne - 1), colonne] == 1 && matrice[ligne, (colonne - 1)] == 1 && matrice[(ligne - 1), (colonne - 1)] == 0)
                                {
                                    choiseOrdi = colonne - 1;
                                }
                                else if (matrice[ligne, colonne] == 1 && matrice[(ligne - 1), colonne] == 1 && matrice[ligne, (colonne + 1)] == 1 && matrice[(ligne - 1), (colonne + 1)] == 0)
                                {
                                    choiseOrdi = colonne + 1;
                                }
                                else
                                {
                                    if (matrice[ligne, colonne] == 2 && matrice[(ligne - 1), colonne] == 2 && matrice[ligne, (colonne - 1)] == 2 && matrice[(ligne - 1), (colonne - 1)] == 0)
                                    {
                                        choiseOrdi = colonne - 1;
                                    }
                                    else if (matrice[ligne, colonne] == 2 && matrice[(ligne - 1), colonne] == 2 && matrice[ligne, (colonne + 1)] == 2 && matrice[(ligne - 1), (colonne + 1)] == 0)
                                    {
                                        choiseOrdi = colonne + 1;
                                    }
                                }
                            }

                        }
                    }
                }
                #endregion
                gui.changerMessage("La IA a choisi le colonne " + (choiseOrdi + 1 ) + " Appyez sur Enter");
                int i = matrice.GetLength(0) - 1;
                while (estChanger == false && i >= 0)
                {
                    if (matrice[i, choiseOrdi] == 0)
                    {
                        matrice[i, choiseOrdi] = 2;
                        estChanger = true;
                    }
                    i--;
                }
                if (estChanger == false)
                {
                    choiseOrdi = rndOrdi.Next(1, 8);
                }
            }
            gui.rafraichirGrille();


        }
        static void ChoiseDuNiveau(Fenetre gui, int[,] matrice, string nomJoueur2, int reponse, bool IAsontDeux)
        {
            switch (reponse)
            {
                case 1:
                    IA(gui, matrice, reponse, IAsontDeux);
                    break;
                case 2:
                    IA(gui, matrice, reponse, IAsontDeux);
                    break;
                case 3:
                    IA(gui, matrice, reponse,IAsontDeux);
                    break;
            }
        }
        static int TryCatch ()
        {
            int reponse = 0;
            try
            {
                reponse = int.Parse(Console.ReadLine());
            }
            catch
            {
                Console.WriteLine("Erreur");
            }
            return reponse;
        }
        [System.STAThreadAttribute()]
        static void Main(string[] args)
        {
            string repeat = "yes";
            while (repeat.Equals("yes") || repeat.Equals("y"))
            {
                int[,] matriceGame = new int[6, 7];
                Fenetre gui = new Fenetre(matriceGame);
                gui.changerMessage(" Veuillez choisir ===> {1} 1 Joueur {2} 2 Joueur  ");
                int reponse = TryCatch();
                for (int i = 0; i < matriceGame.GetLength(0); i++)
                {
                    for (int i2 = 0; i2 < matriceGame.GetLength(1); i2++)
                    {
                        matriceGame[i, i2] = 0;
                    }
                }
                gui.rafraichirGrille();
                bool estgagant = false;
                int compteur = 0;
                string nomJoueur1 = "Joueur 1 ";
                string nomJoueur2 = "Joueur 2 ";
                int reponse2 = 0;
                switch (reponse)
                {
                    case 1:
                        gui.changerMessage("Veulliez choisir un niveau ==> {1}Facile {2}Moyenne {3}Difficile");
                        reponse2=TryCatch();
                        nomJoueur1 = Interface(gui, 1);
                        nomJoueur2 = "PC";
                        gui.changerMessage("Appuyer sue ENTRE pour Commencer....");
                        Console.ReadKey();
                        break;
                    case 2:
                        nomJoueur1 = Interface(gui, 1);
                        nomJoueur2 = Interface(gui, 2);
                        gui.changerMessage("Appuyer sue ENTRE pour Commencer....");
                        Console.ReadKey();
                        break;
                }
                bool IAsontDeux = false;
                while (compteur != 42 && estgagant == false && (reponse==2 || reponse == 1 ))
                {
                    DemendeJoueur(matriceGame, nomJoueur1, gui, 1);
                    estgagant = PartieGagant(matriceGame, nomJoueur1, nomJoueur2, gui);
                    LigneDispart(matriceGame, gui);
                    compteur++;
                    if (estgagant == false)
                    {
                        switch (reponse)
                        {
                            case 1:
                                ChoiseDuNiveau(gui, matriceGame, nomJoueur2, reponse2, IAsontDeux);
                                break;
                            case 2:
                                DemendeJoueur(matriceGame, nomJoueur2, gui, 2);
                                break;
                        }
                        estgagant = PartieGagant(matriceGame, nomJoueur1, nomJoueur2, gui);
                        LigneDispart(matriceGame, gui);
                        compteur++;
                    }
                    if (compteur == 42)
                    {
                        gui.changerMessage("PARTIE NULL ");
                    }
                    for (int colonne = (matriceGame.GetLength(1) - 1); colonne > 0; colonne--)
                    {
                        for (int ligne = (matriceGame.GetLength(0) - 1); ligne > 0; ligne--)
                        {
                            if (matriceGame[ligne, colonne] == matriceGame[(ligne - 1), colonne] && matriceGame[ligne, colonne] ==2 )
                            {
                                IAsontDeux = true;
                            }
                        }
                    }
                }
                string ilrejoue = " ";
                while (!ilrejoue.Equals("yes") && !ilrejoue.Equals("y") && !ilrejoue.Equals("no") && !ilrejoue.Equals("n"))
                {
                    gui.changerMessage("Vouliez vous rejouer ? [Yes|No]");
                    //Console.WriteLine("Vouliez vous rejouer ? [Yes|No]");
                    ilrejoue = Console.ReadLine().ToLower();
                }
                repeat = ilrejoue;
            }

         //   Console.ReadKey();
        }
    }
}
