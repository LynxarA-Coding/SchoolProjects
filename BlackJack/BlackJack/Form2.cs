using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BlackJack
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
            PlayerLogin = label1.Text;
            for (int i = 1; i <= 13; i++)
            {
                Deck[i] = 4;
            }
            PlayerBet = 0;

            for (int i = 0; i < 9; i++)
            {
                EnemyHand[i] = 0;
                PlayerHand[i] = 0;
            }

            EnemyPosition = 0;
            PlayerPosition = 0;
        }

        public int PlayerBalance;
        public string PlayerLogin;
        public int PlayerBet; // current player bet

        public int EnemyPosition; // position to place a new card
        public int PlayerPosition; // position to place a new card

        public int[] Deck = new int[14]; // number of cards left in the deck, 0 - nothing, 1 - 2, 2 - 3 ... 10 - Jack, 11 - Queen, 12 - King, 13 - Ace
        public int[] EnemyHand = new int[9]; // enemy cards in hand
        public int[] PlayerHand = new int[9]; // player cards in hand

        public bool BetChecker() // checks bet
        {
            PlayerBalance = Convert.ToInt32(textBox1.Text);
            if ((PlayerBet > PlayerBalance) || (PlayerBet < 0))
            {
                return false;
            }
            else
            {
                return true;
            }

        }

        public string NumberConverter(int number) // converts number to visual card
        {
            if (number < 10)
            {
                return Convert.ToString(number + 1);
            }
            else
            {
                switch (number)
                {
                    case 10:
                        return "Jack";
                        break;
                    case 11:
                        return "Queen";
                        break;
                    case 12:
                        return "King";
                        break;
                    case 13:
                        return "Ace";
                        break;
                    default:
                        return "Error";
                        break;
                }
            }
        }

        public int RandomNumberGenerator(Random Random)
        {
            return Random.Next(1, 13);
        }

        public void HandEraser() // makes no cards in hand
        {
            for (int i = 0; i < 9; i++)
            {
                EnemyHand[i] = 0;
                PlayerHand[i] = 0;
            }

            textBox2.Text = "";
            textBox3.Text = "";

            for (int i = 1; i < 14; i++)
            {
                Deck[i] = 4;
            }
        }

        public void StarterHandGeneration() // generates starter hand
        {
            Random Random = new Random();

            EnemyPosition = 2;
            PlayerPosition = 2;

            int temporaryRandomNumber = RandomNumberGenerator(Random);

            for (int i = 0; i < 2; i++) // enemy hand generator
            {
                while (Deck[temporaryRandomNumber] == 0)
                {
                    temporaryRandomNumber = RandomNumberGenerator(Random);
                }

                Deck[temporaryRandomNumber]--;
                textBox2.Text = textBox2.Text + ' ' + NumberConverter(temporaryRandomNumber);

                temporaryRandomNumber = RandomNumberGenerator(Random);
            }

            temporaryRandomNumber = RandomNumberGenerator(Random);

            for (int i = 0; i < 2; i++) // player hand generator
            {
                while (Deck[temporaryRandomNumber] == 0)
                {
                    temporaryRandomNumber = RandomNumberGenerator(Random);
                }

                Deck[temporaryRandomNumber]--;
                textBox3.Text = textBox3.Text + ' ' + NumberConverter(temporaryRandomNumber);

                temporaryRandomNumber = RandomNumberGenerator(Random);
            }
        }

        private void Form2_Load(object sender, EventArgs e)
        {
             
        }

        private void button3_Click(object sender, EventArgs e) // do a bet
        {
            PlayerBet = Convert.ToInt32(textBox4.Text);

            if (BetChecker())
            {
                textBox4.ReadOnly = true;
                button3.Enabled = false;
            }
            else
            {
                MessageBox.Show("Ставка неверна!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            HandEraser();
            StarterHandGeneration();
        }
    }
}
