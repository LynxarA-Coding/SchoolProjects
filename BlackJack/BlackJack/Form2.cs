using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;

namespace BlackJack
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        public int PlayerBalance;
        public int OldPlayerBalance;
        public string PlayerLogin;
        public int PlayerBet; // current player bet

        public int EnemyPosition; // position to place a new card
        public int PlayerPosition; // position to place a new card

        public int[] Deck = new int[14]; // number of cards left in the deck, 0 - nothing, 1 - 2, 2 - 3 ... 10 - Jack, 11 - Queen, 12 - King, 13 - Ace
        public int[] EnemyHand = new int[9]; // enemy cards in hand
        public int[] PlayerHand = new int[9]; // player cards in hand

        public int EnemyHandCounter;
        public int PlayerHandCounter;

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
        } // generates number 1 to 13

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

            EnemyHandCounter = 0;
            PlayerHandCounter = 0;
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
                switch (temporaryRandomNumber)
                {
                    case 10:
                        EnemyHandCounter += 10;
                        break;
                    case 11:
                        EnemyHandCounter += 10;
                        break;
                    case 12:
                        EnemyHandCounter += 10;
                        break;
                    case 13:
                        EnemyHandCounter += 11;
                        break;
                    default:
                        EnemyHandCounter += temporaryRandomNumber + 1;
                        break;
                }
                textBox5.Text = Convert.ToString(EnemyHandCounter);

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
                switch (temporaryRandomNumber)
                {
                    case 10:
                        PlayerHandCounter += 10;
                        break;
                    case 11:
                        PlayerHandCounter += 10;
                        break;
                    case 12:
                        PlayerHandCounter += 10;
                        break;
                    case 13:
                        PlayerHandCounter += 11;
                        break;
                    default:
                        PlayerHandCounter += temporaryRandomNumber + 1;
                        break;
                }
                textBox6.Text = Convert.ToString(PlayerHandCounter);

                temporaryRandomNumber = RandomNumberGenerator(Random);
            }

            WinChecker();
        }

        public void CardTaker(string handType)
        {
            Random Random = new Random();
            int temporaryCard = RandomNumberGenerator(Random);

            if (handType == "enemy")
            {
                while (Deck[temporaryCard] == 0)
                {
                    temporaryCard = RandomNumberGenerator(Random);
                }

                Deck[temporaryCard]--;

                textBox2.Text = textBox2.Text + ' ' + NumberConverter(temporaryCard);
                switch (temporaryCard)
                {
                    case 10:
                        EnemyHandCounter += 10;
                        break;
                    case 11:
                        EnemyHandCounter += 10;
                        break;
                    case 12:
                        EnemyHandCounter += 10;
                        break;
                    case 13:
                        EnemyHandCounter += 11;
                        break;
                    default:
                        EnemyHandCounter += temporaryCard + 1;
                        break;
                }
                textBox5.Text = Convert.ToString(EnemyHandCounter);

                temporaryCard = RandomNumberGenerator(Random);
            }
            else
            {
                while (Deck[temporaryCard] == 0)
                {
                    temporaryCard = RandomNumberGenerator(Random);
                }

                Deck[temporaryCard]--;

                textBox3.Text = textBox3.Text + ' ' + NumberConverter(temporaryCard);
                switch (temporaryCard)
                {
                    case 10:
                        PlayerHandCounter += 10;
                        break;
                    case 11:
                        PlayerHandCounter += 10;
                        break;
                    case 12:
                        PlayerHandCounter += 10;
                        break;
                    case 13:
                        PlayerHandCounter += 11;
                        break;
                    default:
                        PlayerHandCounter += temporaryCard + 1;
                        break;
                }
                textBox6.Text = Convert.ToString(PlayerHandCounter);

                temporaryCard = RandomNumberGenerator(Random);

                WinChecker();
            }
        } // takes card according to taking type

        public void EnemyAI()
        {
            while (EnemyHandCounter <= 17)
            {
                Thread.Sleep(500);
                CardTaker("enemy");
            }
        } // AI for enemy

        public void PlayerWin()
        {
            MessageBox.Show("Вы победили!", "Поздравляем", MessageBoxButtons.OK, MessageBoxIcon.Information);
            button3.Enabled = true;
            textBox4.Enabled = true;
            PlayerBalance += PlayerBet * 2;
            textBox1.Text = Convert.ToString(PlayerBalance);

            button1.Enabled = true;
            button2.Enabled = true;
            DataSaver();
        } // function for game win

        public void PlayerLost()
        {
            MessageBox.Show("Вы проиграли!", "Сожалеем", MessageBoxButtons.OK, MessageBoxIcon.Information);
            button3.Enabled = true;
            textBox4.Enabled = true;
            PlayerBalance -= PlayerBet;
            textBox1.Text = Convert.ToString(PlayerBalance);

            button1.Enabled = true;
            button2.Enabled = true;
            DataSaver();
        } // function for lost game

        public void PlayerDraw() // draw
        {
            MessageBox.Show("Ничья!", "Бывает", MessageBoxButtons.OK, MessageBoxIcon.Information);
            button3.Enabled = true;
            textBox4.Enabled = true;
            textBox1.Text = Convert.ToString(PlayerBalance);

            button1.Enabled = true;
            button2.Enabled = true;
            DataSaver();
        }

        public void DataSaver()
        {
            PlayerLogin = label1.Text;
            StreamReader SR = new StreamReader("database.txt");
            int fileLength = 0;

            while (!SR.EndOfStream)
            {
                fileLength++;
                SR.ReadLine();
            }
            SR.Close();

            StreamReader SR2 = new StreamReader("database.txt");
            string[] file = new string[fileLength];

            for (int i = 0; i < fileLength; i++)
            {
                file[i] = SR2.ReadLine();

                if (file[i].Contains(Convert.ToString(PlayerLogin)))
                {
                    file[i] = file[i].Replace(Convert.ToString(OldPlayerBalance), Convert.ToString(PlayerBalance));
                }
            }

            SR2.Close();

            File.Delete("database.txt");
            File.WriteAllText("database.txt", "");

            StreamWriter SW = new StreamWriter("database.txt");

            for (int i = 0; i < fileLength; i++)
            {
                SW.WriteLine(file[i]);
            }
            SW.Close();
        } // saves to notepad

        public void WinChecker()

        {
            if (PlayerHandCounter == 21)
            {
                PlayerWin();
            }
            else if (PlayerHandCounter > 21)
            {
                PlayerLost();
            }
        } // first checker

        public void WinCheckerGameEnd() // last checker
        {
            if (EnemyHandCounter > 21)
            {
                PlayerWin();
            }
            else if (EnemyHandCounter == PlayerHandCounter && PlayerHandCounter == 21)
            {
                PlayerDraw();
            }
            else if (EnemyHandCounter == PlayerHandCounter)
            {
                PlayerDraw();
            }
            else if (EnemyHandCounter < PlayerHandCounter)
            {
                PlayerWin();
            }
            else
            {
                PlayerLost();
            }
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            PlayerLogin = label1.Text;
            PlayerBalance = Convert.ToInt32(textBox1.Text);
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
            EnemyHandCounter = 0;
            PlayerHandCounter = 0;
            OldPlayerBalance = PlayerBalance;
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

        private void Button1_Click(object sender, EventArgs e)
        {
            if (button3.Enabled)
            {
                MessageBox.Show("Прежде чем играть, сделайте ставку!", "Недопустимое действие", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                CardTaker("player");
            }
        } // button for player to take a card

        private void button2_Click(object sender, EventArgs e)
        {
            button1.Enabled = false;
            button2.Enabled = false;

            EnemyAI();
            WinCheckerGameEnd();
        } // player turn end button
    }
}
