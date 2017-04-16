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
            gui.changerMessage("Appuyer sue ENTRE pour Commencer....");
            Console.ReadKey();
            return nomJoueur;
        }
        /* static void AfficherMatrice(int[,] matrice)
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
         }*/
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
                    choise1 = int.Parse(Console.ReadLine());

                }
                while (choise1 < 0 || choise1 > matrice.GetLength(1))
                {
                    gui.changerMessage("Mauvais nombre " + nomJoueur.ToUpper() + " Veuillez choisir un numero de la colonne ==>");
                    choise1 = int.Parse(Console.ReadLine());
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
                Console.ReadKey();
                gui.changerMessage("la 1ere ligne est rempli totalement alors elle va disparetre ");
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
        static void IA(Fenetre gui, int[,] matrice, int level)
        {
            bool estChanger = false;
            Random rndOrdi = new Random();
            while (estChanger == false)
            {
                int choiseOrdi = rndOrdi.Next(1, 8);
                #region reponse2
                if (level == 2 || level == 3)
                {
                    for (int colonne = (matrice.GetLength(1) - 1); colonne > 0; colonne--)
                    {
                        for (int j = (matrice.GetLength(0) - 1); j > 0; j--)
                        {
                            if (matrice[j, colonne] == 2)
                            {
                                if (2 == matrice[j, (colonne - 1)])
                                {
                                    if (2 == matrice[(j - 1), colonne])
                                    {
                                        choiseOrdi = (colonne - 1);
                                    }
                                }
                            }
                            //  if (checkColumns(matrice, j, colonne, 2) && checkLignes(matrice, j, colonne, 2))
                            //      choiseOrdi = colonne - 1;

                            if (matrice[j, colonne] == 2)
                            {
                                if (2 == matrice[j, (colonne - 1)])
                                {
                                    if (2 == matrice[(j - 1), (colonne - 1)])
                                    {
                                        choiseOrdi = colonne;
                                    }
                                }
                            }
                            //  if (checkColumns(matrice, j, colonne+1, 2) && checkLignes(matrice, j, colonne, 2))
                            //      choiseOrdi = colonne;

                            if (matrice[j, colonne] == 2)
                            {
                                if (2 == matrice[j, (colonne - 1)])
                                {
                                    choiseOrdi = colonne;
                                }
                            }
                            if (matrice[j, colonne] == 2)
                            {
                                if (2 == matrice[(j - 1), colonne])
                                {
                                    choiseOrdi = (colonne - 1);
                                }
                            }

                        }
                    }
                }

                #endregion
                gui.changerMessage("La IA a choisi le colonne " + choiseOrdi + " Appyez sur Enter");

                int i = matrice.GetLength(0) - 1;
                int indexChoisi = choiseOrdi - 1;

                while (estChanger == false && i >= 0)
                {
                    if (matrice[i, indexChoisi] == 0)
                    {
                        matrice[i, indexChoisi] = 2;
                        estChanger = true;
                    }
                    i--;
                }
                if (estChanger == false)
                {
                    gui.changerMessage("la colonne est totalement rempli ...");
                }
            }
            gui.rafraichirGrille();
            Console.ReadKey();


        }
        static void ChoiseDuNiveau(Fenetre gui, int[,] matrice, string nomJoueur2, int reponse)
        {

            switch (reponse)
            {
                case 1:
                    IA(gui, matrice, reponse);
                    break;
                case 2:
                    IA(gui, matrice, reponse);
                    break;
                default:
                    IA(gui, matrice, 1);
                    break;
            }
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
                int reponse = 0;
                try
                {
                    reponse = int.Parse(Console.ReadLine());
                }
                catch
                {
                    Console.WriteLine("Erreur");
                }


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
                string nomJoueur1 = " ";
                string nomJoueur2 = " ";
                int reponse2 = 0;
                switch (reponse)
                {
                    case 1:
                        gui.changerMessage("Veulliez choisir un niveau ==> {1}Facile {2}Moyenne {3}Difficile");

                        try
                        {
                            reponse2 = int.Parse(Console.ReadLine());
                        }
                        catch
                        {
                            Console.WriteLine("Erreur");
                        }

                        nomJoueur1 = Interface(gui, 1);
                        nomJoueur2 = "PC";
                        break;
                    case 2:
                        nomJoueur1 = Interface(gui, 1);
                        nomJoueur2 = Interface(gui, 2);
                        break;
                }
                while (compteur != 42 && estgagant == false)
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
                                ChoiseDuNiveau(gui, matriceGame, nomJoueur2, reponse2);
                                break;
                            case 2:
                                DemendeJoueur(matriceGame, nomJoueur2, gui, 2);
                                break;
                            default:
                                IA(gui, matriceGame, 1);
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

                }
                string reponseUtilisateur = " ";
                while (!reponseUtilisateur.Equals("yes") && !reponseUtilisateur.Equals("y") && !reponseUtilisateur.Equals("no") && !reponseUtilisateur.Equals("n"))
                {
                    Console.WriteLine("Vouliez vous rejouer ? [Yes|No]");
                    reponseUtilisateur = Console.ReadLine().ToLower();
                }
                repeat = reponseUtilisateur;
            }

            Console.ReadKey();
        }
        static bool checkColumns(int[,] matrice, int ligne, int colonne, int value)
        {
            bool result = false;
            if (value == matrice[ligne, colonne] && matrice[ligne, colonne] == value)
            {
                result = true;
            }
            return result;
        }
        static bool checkLignes(int[,] matrice, int ligne, int colonne, int value)
        {
            bool result = false;
            if (matrice[ligne, colonne] == matrice[ligne - 1, colonne] && matrice[ligne, colonne] == value)
            {
                result = true;
            }
            return result;
        }
        static void

    }
}
