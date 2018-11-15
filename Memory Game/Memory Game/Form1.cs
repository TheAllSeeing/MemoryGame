using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

namespace Memory_Game
{
    public partial class Form1 : Form
    {

        Random rndNum = new Random(); //RNG
        int temp; //Keeps first card number to check if it's the same as the next one (lines 36 and 45)
        int WonNumber; //keep the number of a guessed correctly second card, in order for yhe number to be shown after it's dissapeard by timer1.
        Button tempcard; //Keep first card to diable if the player guesses correctly
        Button woncard;  //Keep second card to diable if the player guesses correctly
        int life = 20; //life counter value
        int score; //score value
        bool second = false; //for checking if a card is the first or the second picked that turn
        IList<Button> allButtons = new List<Button> {};
        string[] values = new string[21]; //list of all card's numbers, used to shorten the infini
        int tempScore; //Keep score for every new round to get it back if the round restarts.
        int tempLife; //Keep life count for every new round to get it back if the round restarts.
        bool ImmortalMode; //True if Immortal MOde is Enabled. To make sure if life can't be lost if Immortal Mode is enabled. 
        DialogResult ChangeModes; //Shortens code for change mode dialog.
        string mode; //Contains the mode. For keeping the mode when the game or rounnd restart.

        public void timestart() //Shortcut for startingg the timer because I was lazy
        {
            this.timer1.Enabled = true;
        }
        public void ChangeMode() //Shortcut for change mode dialog.
        {
            ChangeModes = MessageBox.Show("Are you sure you want to to change mode? Your game will restart and your progress will be lost!", "Warning!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
        }
        public void SetBoard() //Enables all buttons, erases their current text, and gives them new numbers. 
        {
            tempScore = score;
            tempLife = life;
            foreach (Button card in groupBox1.Controls)
            {
                if (allButtons.Count != 20)
                {
                    if(allButtons.Contains(card))
                    {
                        allButtons.Add(tempcard);
                        allButtons.Add(woncard);
                    }
                    else
                        allButtons.Add(card);
                }
            }
            IList<int> ValuePlacementList = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20 };
            IList<int> numbers = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 }; //List for possible card values
            for (int i = 0; i < 10; i++) //iterates for the amount of buttons
            {
                int placement = rndNum.Next(0, numbers.Count); //randomizes the card's value position in the value array
                for (int g = 0; g < 2; g++)
                {
                    string cardnumber = Convert.ToString(numbers[placement]); //assign randomized value to variable
                    int valueplacement = rndNum.Next(0, ValuePlacementList.Count); //randomize a position from the list of buttons noyt yest picked
                    int truevalueplacement = ValuePlacementList[valueplacement]; //gets the randomized button number
                    values[truevalueplacement] = cardnumber; //assigns the card's value to the card randomized in the line above
                    ValuePlacementList.RemoveAt(valueplacement);
                }
                numbers.RemoveAt(placement);

            }
            int a = 1;
            if (!ImmortalMode)
                textBox3.Text = Convert.ToString(life);
            foreach (Button card in allButtons)
            {
                card.Enabled = true;
                MessageBox.Show(card.Name, Convert.ToString(a++));
                card.Text = "";
            }
        }
        public void TurnCard(Button card,int CardNumber) //shortcut for turning the card - show numbers, check if it's the seconnd card, and if so chek for identical 
        {
            card.Text =values[CardNumber];
            if (second && tempcard.Name != card.Name)
            {
                timestart();
                if (temp == int.Parse(card.Text)) //check if the cards match
                {
                    woncard = card; //keep card for disabling
                    WonNumber = int.Parse(card.Text); //keep card number to maintain text
                    timer2.Enabled = true; //start timer towards winning (see line 224-234)
                }
                else
                {
                    if (!ImmortalMode)
                    {
                        life -= 1;
                        textBox3.Text = Convert.ToString(life);
                        if (life == 0)
                        {
                            MessageBox.Show("You life counter has reached zero! You lost! Restart?", "You Lost!", MessageBoxButtons.OK);
                            SetBoard();
                        }
                    }
                }
                second = false; //(next card is the first of the next round)
            }

            else
            {
                temp = int.Parse(card.Text); //keep card number to compare with the next cards' number and tomaintain text if round is won
                tempcard = card; //keep card to disable and maintain text if round is won
                second = true; //(next one is second)
            }  
        }
        public Form1()
        {
            InitializeComponent();
        }
        public void button1_Click(object sender, EventArgs e)
        {
            // Top Left number
            TurnCard(button1, 1);
        }

        public void button2_Click(object sender, EventArgs e)
        {
            // Top middle left number
            TurnCard(button2, 2);
        }

        public void button3_Click(object sender, EventArgs e)
        {
            // Top middle number
            TurnCard(button3, 3);
        }

        public void button4_Click(object sender, EventArgs e)
        {
            // MidTop left number
            TurnCard(button4, 4);
        }

        public void button5_Click(object sender, EventArgs e)
        {
            // MidTop middle left numbers
            TurnCard(button5, 5);
        }

        public void button6_Click(object sender, EventArgs e)
        {
            // MidTop middle numbers
            TurnCard(button6, 6);
        }

        public void button7_Click(object sender, EventArgs e)
        {
            // MidBottom left numbers
            TurnCard(button7, 7);
        }

        public void button8_Click(object sender, EventArgs e)
        {
            // MidBottom middle left numbers
            TurnCard(button8, 8);
        }

        public void button9_Click(object sender, EventArgs e)
        {
            // MidBottom middle numbers
            TurnCard(button9, 9);
        }

        public void button10_Click(object sender, EventArgs e)
        {
            // Top middle right numbers
            TurnCard(button10, 10);
        }

        public void button11_Click(object sender, EventArgs e)
        {
            // Top right numbers
            TurnCard(button11, 11);
        }

        public void button12_Click(object sender, EventArgs e)
        {
            // MidTop middle right numbers
            TurnCard(button12, 12);
        }

        public void button13_Click(object sender, EventArgs e)
        {
            // MidTop right numbers
            TurnCard(button13, 13);
        }

        public void button14_Click(object sender, EventArgs e)
        {
            // MidBottom middle right numbers
            TurnCard(button14, 14);
        }

        public void button15_Click(object sender, EventArgs e)
        {
            // Midbottom right numbers
            TurnCard(button15, 15);
        }

        public void button16_Click(object sender, EventArgs e)
        {
            // Bottom left numbers
            TurnCard(button16, 16);
        }

        public void button17_Click(object sender, EventArgs e)
        {
            // Bottom middle left numbers
            TurnCard(button17, 17);
        }

        public void button18_Click(object sender, EventArgs e)
        {
            // Bottom middle numbers
            TurnCard(button18, 18);
        }

        public void button19_Click(object sender, EventArgs e)
        {
            // Bottom middle right numbers
            TurnCard(button19, 19);
        }

        public void button20_Click(object sender, EventArgs e)
        {
            // Bottom right numbers
            TurnCard(button20, 20);
        }

        private void button21_Click(object sender, EventArgs e)
        {
            // Normal Mode
            ChangeMode();
            if (ChangeModes == DialogResult.Yes)
            {
                mode = "normal";
                timer1.Interval = 200;
                life = 20;
                button23.Enabled = true;
                button21.Enabled = false;
                button22.Enabled = true;
                ImmortalMode = false;
                SetBoard();
            }
        }

        private void button22_Click(object sender, EventArgs e)
        {
            // Immortal Mode
            ChangeMode();
            if (ChangeModes == DialogResult.Yes)
            {
                ImmortalMode = true;
                mode = "imm";
                timer1.Interval = 200;
                textBox3.Text = "∞";
                button23.Enabled = true;
                button21.Enabled = true;
                button22.Enabled = false;
                SetBoard();
            }
        }

        private void button23_Click(object sender, EventArgs e)
        {
            // Hard Mode
            ChangeMode();
            if (ChangeModes == DialogResult.Yes)
            {
                mode = "hard";
                timer1.Interval = 90;
                life = 10;
                button23.Enabled = false;
                button21.Enabled = true;
                button22.Enabled = true;
                ImmortalMode = false;
                SetBoard();
            }
        }



        private void button24_Click(object sender, EventArgs e)
        {
            // Restart game:
            DialogResult restartornot = MessageBox.Show("Are you sure you want to restart the game? Your progress will be lost.", "Warning!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if(restartornot == DialogResult.Yes)
            {
                switch(mode)
                {

                    case "normal":
                        life = 20;
                        break;
                    case "imm":
                        life = 300;
                        break;
                    case "hard":
                        life = 10;
                        break;
                    default:
                        MessageBox.Show("not a mode");
                        break;

                }
                SetBoard();
                score = 0;
                textBox4.Text = Convert.ToString(score);
            }

        }

        private void button25_Click(object sender, EventArgs e)
        {
            // Restart Round
            DialogResult restartornot = MessageBox.Show("Are you sure you want to restart this round? Your progress from this round will be lost.", "Warning!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (restartornot == DialogResult.Yes)
            {
                score = tempScore;
                life = tempLife;
                textBox4.Text = Convert.ToString(score);
                textBox3.Text = Convert.ToString(life);
                SetBoard();
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            foreach(Button card in allButtons)
            {
                card.Text = "";
            }
            timer1.Enabled = false;
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            timer2.Enabled = false;
            MessageBox.Show("Congrats!\nYou chose identical cards, and got a point!","Congrats!!",MessageBoxButtons.OK);
            score += 1;
            textBox4.Text = Convert.ToString(score);
            if(score%10==0)
            {
                MessageBox.Show("Congratulations! Yo have discovered all ten pairs... \n\n of this round! Would you like to advance to the next?", "You won!", MessageBoxButtons.OK);
                SetBoard();

            }
            else
            {
                allButtons.Remove(woncard);
                allButtons.Remove(tempcard);
            }
            woncard.Enabled = false;
            tempcard.Enabled = false;
            woncard.Text = Convert.ToString(WonNumber);
            tempcard.Text = Convert.ToString(temp);

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            life = 20;
            score = 0;
            mode = "normal";
            SetBoard();
        }
    }
}
