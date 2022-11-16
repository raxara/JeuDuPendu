using System.Collections;
using System.Collections.Generic;


public class IHMController 
{

    //references aux autres objets du programme
    public GameManager gManager;

    public Game currentGame { get { return gManager.currentGame; } }

    //"sprites" du pendu, dans l'ordre inverse pour suivre le nombre d'essais qui decremente
    public string[] spritesArray = {
        " \n"
        + "  +---+\n"
        + "  |   |\n"
        + "  |   O\n"
        + "  |  /|\\\n"
        + "  |  / \\\n"
        + "  |\n"
        + "========="
        + " \n",

        " \n"
        + "  +---+\n"
        + "  |   |\n"
        + "  |   O\n"
        + "  |  /|\\\n"
        + "  |  /\n"
        + "  |\n"
        + "========="
        + " \n",

        " \n"
        + "  +---+\n"
        + "  |   |\n"
        + "  |   O\n"
        + "  |  /|\\\n"
        + "  |\n"
        + "  |\n"
        + "========="
        + " \n",

        " \n"
        + "  +---+\n"
        + "  |   |\n"
        + "  |   O\n"
        + "  |  /|\n"
        + "  |\n"
        + "  |\n"
        + "========="
        + " \n",

        " \n"
        + "  +---+\n"
        + "  |   |\n"
        + "  |   O\n"
        + "  |   |\n"
        + "  |\n"
        + "  |\n"
        + "========="
        + " \n",

        " \n"
        + "  +---+\n"
        + "  |   |\n"
        + "  |   O\n"
        + "  |\n"
        + "  |\n"
        + "  |\n"
        + "========="
        + " \n",

        " \n"
        + "  +---+\n"
        + "  |   |\n"
        + "  |\n"
        + "  |\n"
        + "  |\n"
        + "  |\n"
        + "========="
        + " \n",

        " \n"
        + "  +---+\n"
        + "  |\n"
        + "  |\n"
        + "  |\n"
        + "  |\n"
        + "  |\n"
        + "========="
        + " \n",
        
        " \n"
        + "  |\n"
        + "  |\n"
        + "  |\n"
        + "  |\n"
        + "  |\n"
        + "========="
        + " \n", 

        " \n"
        + "========="
        + " \n",

        "\n"
    };

    //constructeur
    public IHMController(GameManager gm) {
        this.gManager = gm;
    }

    //affichage d'un message
    public void DisplayMessage(string message, ConsoleColor color) 
    {
        Console.ForegroundColor = color;
        Console.WriteLine(message);
        Console.ForegroundColor = ConsoleColor.White;
    }

    //affichage des lettres deja proposées
    public void displaySeenLetters(List<char> lettersList)
    {
        string partOne = "lettres proposées :";
        string partTwo = "";
        foreach (char c in lettersList)
        {
            partTwo += c + ", ";
        }
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine(partOne + partTwo.ToUpper());
    }

    //affichage du pendu
    public void DisplaySprite(Game currentGame)
    {
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine(spritesArray[currentGame.tryLeft]);
    }

    //affichage du mot a deviner, avec des espaces vides pour les lettres inconnues
    public void displayWord(Game currentGame)
    {
        string result = "";
        for (int i = 0; i < currentGame.word.Length; i++)
        {
            if (currentGame.wordLetters[currentGame.word[i]])
            {
                result += " " + currentGame.word[i]; 
            } 
            else
            {
                result += " _";
            }
        }
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine(result.ToUpper());
    }

    //affichage de la fenetre de jeu
    public void DisplayGameScreen(Game g) {
        Console.Clear();
        DisplayMessage(g.curMessage, g.curMessageColor);
        DisplaySprite(g);
        displaySeenLetters(g.seenLetters);
        displayWord(g);
    }
}
