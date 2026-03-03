using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tic_Tac_Toe_Game.Properties;

namespace Tic_Tac_Toe_Game
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        enum enPlayers { Player1, Player2 };
        private enPlayers currentPlayer = enPlayers.Player1;

        enum enWinner { Player1 = 1, Player2, Draw, None };

        private (bool isWinner, int[] winIndexs) check_number_winner(int number, int[] board)
        {
            if (board[0] == number && board[1] == number && board[2] == number)
                return (true, new int[3] { 0, 1, 2 });
            if (board[3] == number && board[4] == number && board[5] == number)
                return (true, new int[3] { 3, 4, 5 });
            if (board[6] == number && board[7] == number && board[8] == number)
                return (true, new int[3] { 6, 7, 8 });
            if (board[0] == number && board[3] == number && board[6] == number)
                return (true, new int[3] { 0, 3, 6 });
            if (board[1] == number && board[4] == number && board[7] == number)
                return (true, new int[3] { 1, 4, 7 });
            if (board[2] == number && board[5] == number && board[8] == number)
                return (true, new int[3] { 2, 5, 8 });
            if (board[2] == number && board[4] == number && board[6] == number)
                return (true, new int[3] { 2, 4, 6 });
            if (board[0] == number && board[4] == number && board[8] == number)
                return (true, new int[3] { 0, 4, 8 });

            return (false, new int[3] { 0, 0, 0 });
        }

        private void colorBoxes(int[] winIndexs)
        {
            int counter = 0;
            foreach (Button ctrl in gpImages.Controls)
            {
                if (counter == (8 - winIndexs[0]) || counter == (8 - winIndexs[1]) || counter == (8 - winIndexs[2]))
                    ctrl.BackColor = Color.FromArgb(255, 135, 208, 135); 

                counter++;
            }
        }

        private enWinner check_winner()
        {
            int[] board = new int[9];
            int counter = 0;
            int owner;

            foreach(Button ctrl in gpImages.Controls)
            {
                owner = (ctrl.Tag.ToString() == "x") ? 1 : (ctrl.Tag.ToString() == "o") ? 2 : 0;
                board[counter] = owner;
                counter++;
            }
            Array.Reverse(board);

            var player1 = check_number_winner(1, board);
            var player2 = check_number_winner(2, board);

            if (player1.isWinner)
            {
                colorBoxes(player1.winIndexs);
                return enWinner.Player1;
            }
            else if (player2.isWinner)
            {
                colorBoxes(player2.winIndexs);
                return enWinner.Player2;
            }
            else
            {
                foreach (int number in board)
                {
                    if (number == 0)
                        return enWinner.None;
                }

                return enWinner.Draw;
            }
        }

        private void switchPlayers()
        {
            enPlayers temp = currentPlayer;
            currentPlayer = (temp == enPlayers.Player1) ? enPlayers.Player2 : enPlayers.Player1;
            lblTurn.Text = (temp == enPlayers.Player1) ? "    Player 2" : "    Player 1";
        }

        private void applyDraw()
        {
            lblTurn.Text = "  Game Over";
            lblWinner.Text = "           Draw";
            gpImages.Enabled = false;
            MessageBox.Show("Game Over: Draw.", "Draw", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void winPlayer1()
        {
            lblTurn.Text = "  Game Over";
            lblWinner.Text = "     Player 1";
            gpImages.Enabled = false;
            MessageBox.Show("Winner: Player 1.", "Game Over", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void winPlayer2()
        {
            lblTurn.Text = "  Game Over";
            lblWinner.Text = "     Player 2";
            gpImages.Enabled = false;
            MessageBox.Show("Winner: Player 2.", "Game Over", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Color penColor = Color.FromArgb(255, 255, 255, 255);

            Pen whitePen = new Pen(penColor);
            whitePen.Width = 10;

            whitePen.StartCap = System.Drawing.Drawing2D.LineCap.Round;
            whitePen.EndCap = System.Drawing.Drawing2D.LineCap.Round;

            e.Graphics.DrawLine(whitePen, 490, 95, 490, 410);
            e.Graphics.DrawLine(whitePen, 640, 95, 640, 410);

            e.Graphics.DrawLine(whitePen, 380, 200, 745, 200);
            e.Graphics.DrawLine(whitePen, 380, 310, 745, 310);
        }

        private void btnRestartGame_Click(object sender, EventArgs e)
        {
            lblTurn.Text = "    Player 1";
            lblWinner.Text = "In Progress";

            currentPlayer = enPlayers.Player1;

            foreach (Button ctrl in gpImages.Controls)
            {
                ctrl.Image = Resources.question;
                ctrl.Tag = "?";
                ctrl.BackColor = Color.FromArgb(0, 0, 0, 0);
            }

            gpImages.Enabled = true;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            btnRestartGame_Click(sender, e);
        }

        private void blocks_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;

            if (btn.Tag.ToString() != "?")
                return;
            else
            {
                switch (currentPlayer)
                {
                    case enPlayers.Player1:
                        btn.Image = Resources.x;
                        btn.Tag = "x";
                        break;
                    case enPlayers.Player2:
                        btn.Image = Resources.o;
                        btn.Tag = "o";
                        break;
                }

                switch (check_winner())
                {
                    case enWinner.Player1:
                        winPlayer1();
                        break;
                    case enWinner.Player2:
                        winPlayer2();
                        break;
                    case enWinner.Draw:
                        applyDraw();
                        break;
                    case enWinner.None:
                        switchPlayers();
                        break;
                }

            }
        }
    }
}
