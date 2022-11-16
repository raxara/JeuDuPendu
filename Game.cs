using System.Collections;
using System.Collections.Generic;
using System;
using System.Text.RegularExpressions;

public class Game
{
    //le mot à deviner, selectionné au hasard au début du jeu
    public string word;

    //contenu et couleur du message qui sera recuperé par l'IHMController
    public string curMessage = "";

    public ConsoleColor curMessageColor = ConsoleColor.White;

    //references aux autres objets du programme
    public GameManager gManager;

    private IHMController ihmCtrl
    {
        get { return gManager.ihmCtrl;}
    }

    //nombre d'essais restant
    public int tryLeft = 10;

    //liste des lettres proposées par le joueur
    public List<char> seenLetters = new List<char>();

    //liste des lettres du mot, le boolean indique si elles ont été proposées ou non
    public Dictionary<char, bool> wordLetters = new Dictionary<char, bool>();

    //propriété servant à tester la fin d'une partie, vérifiant si le mot est deviné OU si le joueur est à court d'essais
    public bool gameFinished
    {
        get {
            if (maxTryReached) return true;
            if (wordGuessed) return true;
            return false;
            }
    }

    //propriété vérifiant si le joueur est à court d'essai
    public bool maxTryReached 
    {
        get { return tryLeft <= 0; }
    }

    //propriété vérifiant si le mot a été deviné
    public bool wordGuessed 
    {
        get { foreach (KeyValuePair<char, bool> entry in wordLetters)
        {
            if (!entry.Value)
            {
                return false;
            }
        }
        return true;}
    }

    //constructeur qui lance l'initialisation
    public Game(string word, GameManager gManager)
    {
        this.word = word;
        this.gManager = gManager;
        Init();
    }

    //initialises les diff"rentes variables, puis lance le jeu
    void Init()
    {
        for(int i = 0; i < word.Length; i++)
        {
            if (!wordLetters.ContainsKey(word[i]))
            {
                wordLetters.Add(word[i], false);
            }
        }
        Console.Clear();
        ihmCtrl.DisplayMessage("bienvenue dans le jeu du pendu", ConsoleColor.White);
        ihmCtrl.DisplayMessage("j'ai choisi un mot, a toi de le retrouver", ConsoleColor.White);
        ihmCtrl.displayWord(this);
        gManager.currentGame = this; 
        PlayGame();
    }

    //fonction principale, elle gere toute la logique quand un joueur rentre une lettre ou un mot
    public void CheckUserInput(string input)
    {
        //le joueur a rentré une lettre
        if (input.Length == 1)
        {
            //si la lettre a déjà été proposée
            if (seenLetters.Contains(input[0]))
            {
                SetMessage(input.ToUpper() + " a déjà été proposée", ConsoleColor.Red);
            }
            else
            {
                //la lettre est dans le mot   
                if (wordLetters.ContainsKey(input[0]))
                {
                    SetMessage(input.ToUpper() + " est bien dans le mot à deviner", ConsoleColor.Green);
                    wordLetters[input[0]] = true;
                    if (wordGuessed) WinGame();
                }
                //la lettre n'est pas dans le mot
                else
                {
                    SetMessage(input.ToUpper() + " n'est pas dans le mot à deviner", ConsoleColor.Red);
                    tryLeft--;
                }
                seenLetters.Add(input[0]);
            } 
        }
        //le joueur a rentré un mot, il gagne la partie ou perd un essai
        else
        {
            if (input.Equals(word))
            {
                WinGame();
            }
            else
            {
                SetMessage("ce n'est pas ce mot qu'il fallait deviner", ConsoleColor.Red);
                tryLeft--;
            }
        }
        if (maxTryReached)
        {
            SetMessage("perdu, il fallait deviner " + word, ConsoleColor.Red);
            for (int i = 0; i < word.Length; i++)
            {
                wordLetters[word[i]] = true;
            }
            ihmCtrl.displayWord(this);
        }
    }

    //fonction appelé si le joueur a deviné le mot ou trouvé toutes les lettres
    public void WinGame()
    {
        SetMessage("gagné, vous avez trouvé le bon mot : " + word.ToUpper(), ConsoleColor.Green);
        for (int i = 0; i < word.Length; i++)
        {
            wordLetters[word[i]] = true;
        }
    }

    //pour simplifier la notation dans la fonction CheckUserInput
    public void SetMessage(string m, ConsoleColor c) {
        curMessage = m;
        curMessageColor = c;
    }

    //boucle de jeu
    public void PlayGame() {
            //j'utilise un regex pour verifier que l'input ne comporte pas de caracteres spéciaux
            var regexItem = new Regex("^[a-zA-Z]*$");
            while (!gameFinished) {
                Console.WriteLine("veuillez rentrer une lettre ou un mot");
                string userGuess = Console.ReadLine()!;
                while (!regexItem.IsMatch(userGuess) || userGuess.Equals("")) {
                    ihmCtrl.DisplayGameScreen(this);
                    ihmCtrl.DisplayMessage("veuillez rentrer une lettre ou un mot valide", ConsoleColor.Red);
                    userGuess = Console.ReadLine()!;
                }
                //une fois un input valide renseigné, on lance la fonction
                CheckUserInput(userGuess);
                //puis on affiche le resultat
                ihmCtrl.DisplayGameScreen(this);
            }
            gManager.ResetGame();
        }
}
