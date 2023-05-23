using System;
using System.Collections.Generic;

class Blackjack
{
    static void Main(string[] args)
    {
        ConsoleColor defaultcolor = ConsoleColor.Green;

        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine("Welcome to Blackjack!");
        Console.WriteLine();
        
        //Ask if they know how to play blackjack
        Console.Write("Do you know how to play blackjack? (yes or no): ");
        string decision = Console.ReadLine().ToLower();
        if (decision == "no")
        
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Blackjack is a card game where the goal is to get a hand value of 21 or as close to 21 as possible without going over.\nht");
            Console.WriteLine();
            Console.WriteLine("Face cards (jack, queen, king) are worth 10 points, and aces can be worth either 1 or 11 points. An ace is worth 1 point after you recieve more than 2 cards");
            Console.WriteLine();
            Console.WriteLine("The dealer will deal two cards to each player, and then each player can choose to 'hit' (draw another card) or 'stand' (keep their current hand) or 'double down' (Doubling your bet and draw ONLY one more card.");
            Console.WriteLine("If you double down, It will stand you after pulling one more card. It doubles your bet so make sure you have enough money to double down.");
            Console.WriteLine();
            Console.WriteLine("If a player's or the dealers hand goes over 21, they 'bust' if it's the dealer, you win. if it is you, you lose the game. If the player's hand is closer to 21 than the dealer's, you win.");
            Console.WriteLine("If you and the dealer recieve the same number of points a 'push' (Tie) will happen. It returns the money you placed on your bet and restarts the game.");
            Console.WriteLine();
            Console.WriteLine("Hope that explained enough!! Let's get started now!!!");
            Console.WriteLine();
            Console.ForegroundColor = defaultcolor;
        }
        else if (decision == "yes")
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine();
            Console.WriteLine("Great, let's get started!");
            Console.WriteLine();
            Console.ForegroundColor = defaultcolor;
        }
        else
        {
            Console.WriteLine("Invalid choice. Please enter 'yes' or 'no'.");
            Console.WriteLine();
            Console.Clear();
            Main(args); // restart the program
            return;
            Console.Clear();
        }
        
       
        uint money = 1000;       
        Console.WriteLine("Welcome to Blackjack! You have $" + money);

        //Betting
        while (money > 0)
        {   
            Console.WriteLine();
            Console.Write("Enter your bet: ");

            uint bet = 0;
     
            while (uint.TryParse(Console.ReadLine(), out bet) == false)
            {
                Console.Write("You accidentally put a letter, invalid number, negative number, or nothing, please enter your bet again: ");
            }
            
            if (bet > money)
            {
                //if the bet they enter is more than their current amount of money
                Console.WriteLine("You don't have enough money for that son!!!");
                continue;
            }

            if (bet == 0) 
            {
                //No 0s boi
                Console.WriteLine("Can't do 0 bro :/");
                continue;
            }
    
           
            Console.WriteLine("---------------");
            Console.Clear();          
            Console.WriteLine("Welcome to Blackjack! You have $" + money);
            Console.WriteLine($"Current bet: " + bet);

            //creating deck
            List<string> deck = InitializeDeck();
            ShuffleDeck(deck);

            //creating dealer and players hands
            List<string> playerHand = new List<string>();
            List<string> dealerHand = new List<string>();

            //player and dealer uses 2 decks if cards
            playerHand.Add(DealCard(deck));
            dealerHand.Add(DealCard(deck));
            playerHand.Add(DealCard(deck));
            dealerHand.Add(DealCard(deck));

            Console.WriteLine("Your cards:");
            DisplayHand(playerHand);

            Console.WriteLine("Dealer's upcard:");
            Console.WriteLine(dealerHand[0]);

            //check for blackjack
            if (CalculateHandValue(playerHand) == 21)
            {
                Console.WriteLine("Blackjack!");
                money += (uint)(bet * 1.5);
                continue;
            }

            bool playerStands = false;
            while (!playerStands)
            {
                Console.Write("Do you want to hit = h, double down = dd, or stand = s?: ");
                string choice = Console.ReadLine().ToLower();
                if (choice == "hit")
                {
                    playerHand.Add(DealCard(deck));
                    Console.WriteLine("Your cards:");
                    DisplayHand(playerHand);
                    //check for bust
                    if (CalculateHandValue(playerHand) > 21)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.WriteLine("Bust!");
                        Console.ForegroundColor = defaultcolor;
                        money -= bet;
                        break;
                    }
                    //if 21 end
                    else if (CalculateHandValue(playerHand) == 21)
                    {
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.WriteLine("21!");
                        Console.ForegroundColor = defaultcolor;
                        break;
                    }
                }
                //hit
                if (choice == "h")
                {
                    playerHand.Add(DealCard(deck));
                    Console.WriteLine("Your cards:");
                    DisplayHand(playerHand);
                    if (CalculateHandValue(playerHand) > 21)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.WriteLine("Bust!");
                        Console.ForegroundColor = defaultcolor;
                        money -= bet;
                        break;
                    }
                    //if 21 end
                    else if (CalculateHandValue(playerHand) == 21)
                    {
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.WriteLine("21!");
                        Console.ForegroundColor = defaultcolor;
                        break;
                    }
                }
                //stand
                else if (choice == "stand")
                {
                    playerStands = true;
                }
                else if (choice == "s")
                {
                    playerStands = true;
                }
                //regular word hit incase they're weird
                else if (choice == "hit")
                {
                    playerHand.Add(DealCard(deck));
                    Console.WriteLine("Your cards:");
                    DisplayHand(playerHand);
                    if (CalculateHandValue(playerHand) > 21)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.WriteLine("Bust!");
                        Console.ForegroundColor = defaultcolor;
                        money -= bet;
                        break;
                    }
                    //if 21 end
                    else if (CalculateHandValue(playerHand) == 21)
                    {
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.WriteLine("21!");
                        Console.ForegroundColor = defaultcolor;
                        break;
                    }
                }
                //double down function
                else if (choice == "double down")
                {
                    if (playerHand.Count > 2)
                    {
                        //Can only double down within first two cards
                        Console.WriteLine("You can only double down with your first two cards!");
                        continue;
                    }
                    //doubling bet when double down
                    if (bet * 2 > money)
                    {
                        //if player go for a double down but they dont have enough money to double their bet
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("You don't have enough money to double down!");
                        Console.ForegroundColor = defaultcolor;
                        continue;
                    }
                    playerHand.Add(DealCard(deck));
                    Console.WriteLine("Your cards:");
                    DisplayHand(playerHand);
                    if (CalculateHandValue(playerHand) > 21)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.WriteLine("Bust!");
                        Console.ForegroundColor = defaultcolor;
                        money -= bet * 2;
                        break;
                    }
                    else
                    {
                        //Stands after doubling down
                        playerStands = true;
                        bet *= 2;
                    }
                }
                //abbreviation cause i do be lazy fr
                else if (choice == "dd")
                {
                    if (playerHand.Count > 2)
                    {
                        Console.WriteLine("You can only double down with your first two cards!");
                        continue;
                    }
                    //if player go for a double down but they dont have enough money to double their bet
                    if (bet * 2 > money)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("You don't have enough money to double down!");
                        Console.ForegroundColor = defaultcolor;
                        continue;
                    }
                    playerHand.Add(DealCard(deck));
                    Console.WriteLine("Your cards:");
                    DisplayHand(playerHand);
                    if (CalculateHandValue(playerHand) > 21)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.WriteLine("Bust!");
                        Console.ForegroundColor = defaultcolor;
                        money -= bet * 2;
                        break;
                    }
                    else
                    {
                        playerStands = true;
                        bet *= 2;
                    }
                }
                else
                {
                    Console.WriteLine("Invalid choice!");
                }
            }
            
            if (CalculateHandValue(playerHand) <= 21)
            {
                Console.WriteLine("Dealer's cards:");
                DisplayHand(dealerHand);
                //dealer will not go over 17
                while (CalculateHandValue(dealerHand) < 17)
                {
                    dealerHand.Add(DealCard(deck));
                    DisplayHand(dealerHand);
                }
                if (CalculateHandValue(dealerHand) > 21)
                {
                    //see if dealer busted
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine("Dealer busts! You win $" + bet);
                    Console.ForegroundColor = defaultcolor;
                    money += bet;
                }
                else if (CalculateHandValue(dealerHand) < CalculateHandValue(playerHand))
                {
                    //if the player hand has more points than the dealers
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine("You win $" + bet);
                    Console.ForegroundColor = defaultcolor;
                    money += bet;
                }
                //if the dealer hand is higher than the players
                else if (CalculateHandValue(dealerHand) > CalculateHandValue(playerHand))
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine("Dealer wins! You lose $" + bet);
                    Console.ForegroundColor = defaultcolor;
                    money -= bet;
                }
                //if player and the dealer tie, gives push and returns bet money
                else
                {
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    Console.WriteLine("Push!");
                    Console.ForegroundColor = defaultcolor;
                }
            }

            Console.WriteLine("You have $" + money);
        }

        Console.WriteLine("Game over! You're out of money.");
    }

    static List<string> InitializeDeck()
    {
        //setting cards
        List<string> deck = new List<string>();
        string[] suits = { "hearts", "diamonds", "clubs", "spades" };
        string[] values = { "ace", "2", "3", "4", "5", "6", "7", "8", "9", "10", "jack", "queen", "king" };
        foreach (string suit in suits)
        {
            foreach (string value in values)
            {
                deck.Add(value + " of " + suit);
            }
        }
        return deck;
    }
    //shuffles deck and gives random cards
    static void ShuffleDeck(List<string> deck)
    {
        
        Random random = new Random();
        for (int i = 0; i < deck.Count; i++)
        {
            int j = random.Next(i, deck.Count);
            string temp = deck[i];
            deck[i] = deck[j];
            deck[j] = temp;
        }
    }

    static string DealCard(List<string> deck)
    {
        string card = deck[0];
        deck.RemoveAt(0);
        return card;
    }

    static void DisplayHand(List<string> hand)
    {
        //making a total value of points incase the player is too lazy to add the cards themselves
        foreach (string card in hand)
        {
            Console.WriteLine(card);
        }
        Console.WriteLine("Total value: " + CalculateHandValue(hand));
    }

    static int CalculateHandValue(List<string> hand)
    {
        //setting ace values depending on what card you recieved
        int value = 0;
        int numAces = 0;
        foreach (string card in hand)
        {
            string valueStr = card.Substring(0, card.IndexOf(' '));
            if (valueStr == "ace")
            {
                numAces++;
                value += 11;
            }
            else if (valueStr == "2")
            {
                value += 2;
            }
            else if (valueStr == "3")
            {
                value += 3;
            }
            else if (valueStr == "4")
            {
                value += 4;
            }
            else if (valueStr == "5")
            {
                value += 5;
            }
            else if (valueStr == "6")
            {
                value += 6;
            }
            else if (valueStr == "7")
            {
                value += 7;
            }
            else if (valueStr == "8")
            {
                value += 8;
            }
            else if (valueStr == "9")
            {
                value += 9;
            }
            else
            {
                value += 10;
            }
        }
        while (value > 21 && numAces > 0)
        {
            value -= 10;
            numAces--;
        }
        return value;
    }
}