using System;
using System.Collections.Generic;

namespace OOPTask4
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Player player = new Player();

            Console.WriteLine("Если вы хотите брать по одной карте? (Y\\N)");
            switch (Console.ReadKey().Key)
            {
                case ConsoleKey.Y:
                    player.TakeOneCard();
                    break;
                case ConsoleKey.N:
                    player.TakeSomeCards();
                    break;
                default:
                    Console.WriteLine("Не могу понять что вы хотите...");
                    return;
            }

            Console.WriteLine("Ваши карты");
            player.ShowCards();
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
        private List<Card> _cards;
        private string[] _suits = { "Бубы", "Черви", "Трефы", "Пики" };
        private string[] _titles = { "6", "7", "8", "9", "10", "Валет", "Дама", "Король", "Туз" };
        private Random random;

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

        public int GetCountCard()
        {
            return _cards.Count;
        }
    }

    class Player
    {
        private List<Card> _cards;
        private DeckOfCards _deckOfCards;

        public Player()
        {
            _cards = new List<Card>();
            _deckOfCards = new DeckOfCards();
        }

        public void ShowCards()
        {
            foreach (var card in _cards)
            {
                card.ShowInfo();
            }
        }

        public void TakeOneCard()
        {
            Console.WriteLine("Нажмите любую клавишу что бы взять карту, Escape что бы выйти");

            while (Console.ReadKey().Key != ConsoleKey.Escape)
            {
                TakeCard();
            }
        }

        public void TakeSomeCards()
        {
            int cardsCount;

            Console.Write("\nСколько карт вы хотите взять? : ");

            if (int.TryParse(Console.ReadLine(), out cardsCount) == false)
            {
                Console.WriteLine("\nНе корректное значение!");
                return;
            }

            if (cardsCount > _deckOfCards.GetCountCard())
            {
                Console.WriteLine("Нет столько карт!");
                return;
            }

            for (int i = 0; i < cardsCount; i++)
            {
                TakeCard();
            }
        }

        private void TakeCard()
        {
            var card = _deckOfCards.WithdrawCard();

            if (card != null)
            {
                _cards.Add(card);
                Console.WriteLine("Карта взята");
            }
            else
            {
                Console.WriteLine("Карт больше нет");
            }
        }
    }
}
