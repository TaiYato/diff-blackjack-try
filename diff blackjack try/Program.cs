﻿using System;
using System.Collections.Generic;

class Blackjack
{
    static void Main(string[] args)
    {
        int money = 1000;
        Console.WriteLine("Welcome to Blackjack! You have $" + money);

        //Betting
        while (money > 0)
        {
            Console.WriteLine();
            Console.Write("Enter your bet: ");
            int bet = int.Parse(Console.ReadLine());
            if (bet > money)
            {
                Console.WriteLine("You don't have enough money for that son!!!");
                continue;
            }

            List<string> deck = InitializeDeck();
            ShuffleDeck(deck);

            List<string> playerHand = new List<string>();
            List<string> dealerHand = new List<string>();

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
                money += (int)(bet * 1.5);
                continue;
            }

            bool playerStands = false;
            while (!playerStands)
            {
                Console.Write("Do you want to hit, double down or stand? dd = double down: ");
                string choice = Console.ReadLine().ToLower();
                if (choice == "hit")
                {
                    playerHand.Add(DealCard(deck));
                    Console.WriteLine("Your cards:");
                    DisplayHand(playerHand);
                    if (CalculateHandValue(playerHand) > 21)
                    {
                        Console.WriteLine("Bust!");
                        money -= bet;
                        break;
                    }
                    //if 21 end
                    else if (CalculateHandValue(playerHand) == 21)
                    {
                        Console.WriteLine("21!");
                        break;
                    }
                }
                else if (choice == "stand")
                {
                    playerStands = true;
                }
                else if (choice == "double down")
                {
                }
                else if (choice == "dd")
                { 
                    if (playerHand.Count > 2)
                    {
                        Console.WriteLine("You can only double down with your first two cards!");
                        continue;
                    }
                    if (bet * 2 > money)
                    {
                        Console.WriteLine("You don't have enough money to double down!");
                        continue;
                    }
                    playerHand.Add(DealCard(deck));
                    Console.WriteLine("Your cards:");
                    DisplayHand(playerHand);
                    if (CalculateHandValue(playerHand) > 21)
                    {
                        Console.WriteLine("Bust!");
                        money -= bet * 2;
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
                while (CalculateHandValue(dealerHand) < 17)
                {
                    dealerHand.Add(DealCard(deck));
                    DisplayHand(dealerHand);
                }
                if (CalculateHandValue(dealerHand) > 21)
                {
                    Console.WriteLine("Dealer busts! You win $" + bet);
                    money += bet;
                }
                else if (CalculateHandValue(dealerHand) < CalculateHandValue(playerHand))
                {
                    Console.WriteLine("You win $" + bet);
                    money += bet;
                }
                else if (CalculateHandValue(dealerHand) > CalculateHandValue(playerHand))
                {
                    Console.WriteLine("Dealer wins! You lose $" + bet);
                    money -= bet;
                }
                else
                {
                    Console.WriteLine("Push!");
                }
            }

            Console.WriteLine("You have $" + money);
        }

        Console.WriteLine("Game over! You're out of money.");
    }

    static List<string> InitializeDeck()
    {
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
        foreach (string card in hand)
        {
            Console.WriteLine(card);
        }
        Console.WriteLine("Total value: " + CalculateHandValue(hand));
    }

    static int CalculateHandValue(List<string> hand)
    {
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