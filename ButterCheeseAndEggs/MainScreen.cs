using System;
using System.Drawing;
using System.Windows.Forms;

namespace ButterCheeseAndEggs
{
    public partial class MainScreen : Form
    {

        static bool currentPlayer; // false is player 1 and true is player 2 (confusing eh ;p)
        Label[,] labelList;
        const int MAXTURNS = 9;
        int turns;

        public MainScreen()
        {
            InitializeComponent();

            // create a 3 by 3 (table) from labels to click on for the users
            labelList = new Label[3, 3];

            init();
            NewGame();
        }

        private void init()
        {
            //Console.WriteLine("Size: w{0} h{1}", labelWidth, labelHeight);
            var y = 0;
            // loop through label list to create them and set position
            for (var r = 0; r < labelList.GetLength(0); r++)
            {
                var x = 0;
                for (var c = 0; c < labelList.GetLength(1); c++)
                {

                    var label = new Label
                    {
                        Text = "Label",
                        AutoSize = false,
                        BackColor = Color.Tomato,
                        ForeColor = Color.Black,
                        TextAlign = ContentAlignment.MiddleCenter,
                        Size = new Size(ClientSize.Width / 3, ClientSize.Height / 3),
                        Location = new Point(x, y),
                        Cursor = Cursors.Hand,
                        BorderStyle = BorderStyle.FixedSingle,
                    };

                    label.Click += OnClick;

                    Controls.Add(label);
                    labelList[r, c] = label;
                    x += ClientSize.Width / 3;
                }
                y += ClientSize.Height / 3;
            }
        }

        private void OnClick(object sender, EventArgs e)
        {

            if (HasMark((Label)sender))
            {
                return;
            }

            turns++;

            var mark = "";
            // check which players turn it is
            mark = currentPlayer ? "O" : "X";

            var label = (Label)sender;
            label.Text = mark;

            var position = new int[2];
            for (var r = 0; r < labelList.GetLength(0); r++)
            {
                for (var c = 0; c < labelList.GetLength(1); c++)
                {
                    if (!labelList[r, c].Equals(label)) continue;
                    //Console.WriteLine("You clicked coordinate: [{0},{1}]", c, r);
                    labelList[r, c].Size = new Size(ClientSize.Width / 3, ClientSize.Height / 3);
                    position[0] = r;
                    position[1] = c;
                }
            }


            Check3InARow(position);

            // switch turn to other player
            currentPlayer = !currentPlayer;

            if (turns >= MAXTURNS)
            {
                MessageBox.Show("It's a TIE!");
                NewGame();
            }
        }

        private bool HasMark(Label lb)
        {
            return lb.Text == "X" || lb.Text == "O";
        }

        private void Check3InARow(int[] position)
        {
            var marks = new string[3, 3];
            // gather all marks
            for (var i = 0; i < labelList.GetLength(0); i++)
            {
                for (var j = 0; j < labelList.GetLength(1); j++)
                {
                    marks[i, j] = labelList[i, j].Text;
                }
            }

            // this checks horizontal 3 in a row
            for (var i = 0; i < marks.GetLength(0); i++)
            {
                if ((marks[i, 0] == "X" || marks[i, 0] == "O") && marks[i, 0] == marks[i, 1] && marks[i, 1] == marks[i, 2])
                {
                    MessageBox.Show("Player " + (currentPlayer ? "2" : "1") + " Won!");
                    NewGame();
                }
            }

            // this checks vertical 3 in a row
            for (var i = 0; i < marks.GetLength(1); i++)
            {
                if ((marks[0, i] == "X" || marks[0, i] == "O") && marks[0, i] == marks[1, i] && marks[1, i] == marks[2, i])
                {
                    MessageBox.Show("Player " + (currentPlayer ? "2" : "1") + " Won!");
                    NewGame();
                }
            }

            // check diagonal 3 in a row


        }

        private void NewGame()
        {
            // player 1 is the current player
            currentPlayer = false;

            turns = 0;

            foreach (var label in labelList)
            {
                label.Text = "";
            }

        }
    }
}
