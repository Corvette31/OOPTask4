using System;
using System.Collections.Generic;

namespace OOPTask4
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Player player = new Player();
            DeckOfCards cards = new DeckOfCards();

            Console.WriteLine("Если вы хотите брать по одной карте? (Y\\N)");
            switch (Console.ReadKey().Key)
            {
                case ConsoleKey.Y:
                    TakeOneCard(player, cards);
                    break;
                case ConsoleKey.N:
                    TakeSomeCards(player, cards);
                    break;
                default:
                    Console.WriteLine("\nНе могу понять что вы хотите...");
                    return;
            }

            Console.WriteLine("Ваши карты");
            player.ShowCards();
        }

        static void TakeSomeCards(Player player, DeckOfCards cards)
        {
            int cardsCount;

            Console.Write("\nСколько карт вы хотите взять? : ");

            if (int.TryParse(Console.ReadLine(), out cardsCount) == false)
            {
                Console.WriteLine("\nНе корректное значение!");
                return;
            }

            if (cardsCount > cards._cards.Count)
            {
                Console.WriteLine("Нет столько карт!");
            }

            for (int i = 0; i < cardsCount; i++)
            {
                player.TakeCard(cards);
            }
        }

        static void TakeOneCard(Player player, DeckOfCards cards)
        {
            Console.WriteLine("\nНажмите любую клавишу что бы взять карту, Escape что бы выйти");

            while (Console.ReadKey().Key != ConsoleKey.Escape)
            {
                player.TakeCard(cards);                
            }
        }
    }

    class Card
    {
        private string _suit;
        private string _title;

        public Card(string suit, string title)
        {
            _suit = suit;
            _title = title;
        }

        public void ShowInfo()
        {
            Console.WriteLine($"Карта : {_title} {_suit}");
        }
    }

    class DeckOfCards
    {
        public List<Card> _cards { get; private set; }
        private string[] _suits = { "Бубы", "Черви", "Трефы", "Пики" };
        private string[] _titles = { "6", "7", "8", "9", "10", "Валет", "Дама", "Король", "Туз" };
        Random random;
        public DeckOfCards()
        {
            _cards = new List<Card>();
            random = new Random();

            for (int i = 0; i < _suits.Length; i++)
            {
                for (int j = 0; j < _titles.Length; j++)
                {
                    _cards.Add(new Card(_suits[i], _titles[j]));
                }
            }
        }

        public Card WithdrawCard()
        {
            if (_cards.Count == 0) return null;

            Card card = _cards[random.Next(_cards.Count)];
            _cards.Remove(card);
            return card;
        }
    }

    class Player
    {
        protected List<Card> _playerCards;

        public Player()
        {
            _playerCards = new List<Card>();
        }

        public void TakeCard(DeckOfCards cards)
        {
            var card = cards.WithdrawCard();

            if (card != null)
            {
                _playerCards.Add(card);
                Console.WriteLine("\nКарта взята");
            }
            else
            {
                Console.WriteLine("\nКарт больше нет");
            }
        }

        public void ShowCards()
        {
            foreach (var card in _playerCards)
            {
                card.ShowInfo();
            }
        }
    }
}
