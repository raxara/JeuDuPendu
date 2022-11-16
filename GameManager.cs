using System.Collections;
using System.Collections.Generic;

using System;

public class GameManager
{

    //references aux autres objets du programme
    public IHMController ihmCtrl;

    public Game currentGame;

    //simple liste des mots potentiels du pendu
    public String[] WordList = {
        "zebre",
        "canard",
        "colibri"
    };

    public GameManager() {
        ihmCtrl = new IHMController(this);
        currentGame = new Game(GetRandomWord(), this);
    }

    //choisit un mot au hasard dans la liste 
    public string GetRandomWord()
    {
        Random rnd = new Random();
        int rdnNbr = rnd.Next(1, 101);
        if (WordList.Length <= 0)
        {
            throw new Exception("erreur, pas de mots dans la liste");
        }
        else
        {
            return WordList[rnd.Next(0, WordList.Length)];
        }
    }

    public void NewGame()
    {
        currentGame = new Game(GetRandomWord(), this);
        //currentGame.PlayGame();
    }

    //petite fonction qui demande uniquement un input o ou n pour relancer une partie ou non
    public void ResetGame() {
            Console.WriteLine("voulez vous rejouer ? o (oui) ou n (non)");
            string userInput = Console.ReadLine()!;
            while (!userInput.Equals("o") && !userInput.Equals("n")) {
                Console.WriteLine("veuillez rentrer o pour oui ou n pour non");
                userInput = Console.ReadLine()!;
            }
            if (userInput.Equals("o")) {
                Console.Clear();
                Console.WriteLine("blerp");
                NewGame();
            } else {
                Console.WriteLine("fin du programme, revenez jouer plus tard");
                //System.Environment.Exit(0);
            }
        }
}
