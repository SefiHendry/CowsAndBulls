using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinformsTesting
{
    internal class GameWindow : Form
    {
        private readonly GameLogic r_GameLogic = new GameLogic();
        private readonly List<Button> r_HiddenButtons = new List<Button>();
        private readonly GuessesOptionsBoard r_GuessBoard = new GuessesOptionsBoard();
        private readonly List<List<Button>> r_GuessButtons = new List<List<Button>>();
        private readonly List<Button> r_SubmitButtons = new List<Button>();
        private readonly List<List<Button>> r_ResultButtons = new List<List<Button>>();

        private int m_NumberOfGuesses;
        private byte m_CurrentTurn = 0;
        private Color[] m_Target = new Color[4];

        public GameWindow(int i_numberOfGuesses)
        {
            m_NumberOfGuesses = i_numberOfGuesses;
            m_Target = r_GameLogic.ComputerGuess();
            initializeComponent();
            enableCurrentTurnButtons();
        }

        private void initializeComponent()
        {
            drawFirstRow();
            drawGameBoard();
            this.ClientSize = new Size(300, 135 + (45 * m_NumberOfGuesses));
        }

        private void drawFirstRow()
        {
            int gap = 0;
            int startingPosX = 20;
            int startingPosY = 30;

            for (int i = 0; i < 4; i++)
            {
                Button button = new Button();
                button.Location = new System.Drawing.Point(startingPosX + gap, startingPosY);
                button.Size = new System.Drawing.Size(40, 40);
                button.BackColor = Color.Black;
                button.Enabled = false;
                button.Name = string.Format("button{0}", i);
                gap += 45;
                r_HiddenButtons.Add(button);
                this.Controls.Add(button);
            }
        }

        private void drawGameBoard()
        {
            int startingPosY = 90;
            int startingPosX = 20;
            int gap = 0;
            int resultButtonsHeightDifference = 0;

            for (int i = 0; i < m_NumberOfGuesses; i++)
            {
                List<Button> guessButtonsRow = new List<Button>();
                for (int j = 0; j < 4; j++)
                {
                    Button button = new Button();
                    button.Location = new System.Drawing.Point(startingPosX + gap, startingPosY);
                    button.Size = new System.Drawing.Size(40, 40);
                    button.Name = string.Format("button{0}", j);
                    button.Enabled = false;
                    button.Click += new EventHandler(guessButtonClick);
                    button.BackColorChanged += checkGuessButtonsColorState;
                    guessButtonsRow.Add(button);
                    gap += 45;
                    this.Controls.Add(button);
                }

                this.r_GuessButtons.Add(guessButtonsRow);

                // The confirmation button of a guess.
                Button btnConfirm = new Button();
                btnConfirm.Location = new System.Drawing.Point(startingPosX + gap, startingPosY + 15);
                gap += 45;
                btnConfirm.Text = "-->>";
                btnConfirm.Font = new Font(btnConfirm.Font.FontFamily, 6);
                btnConfirm.Size = new System.Drawing.Size(30, 15);
                btnConfirm.Enabled = false;
                btnConfirm.Click += new EventHandler(onClickedSubmit);
                r_SubmitButtons.Add(btnConfirm);
                this.Controls.Add(btnConfirm);

                // Results buttons
                List<Button> resultButtonsRow = new List<Button>();
                for (int j = 0; j < 2; j++)
                {
                    for (int k = 0; k < 2; k++)
                    {
                        Button buttonResult = new Button();
                        buttonResult.Location = new System.Drawing.Point(startingPosX + (16 * k) + gap, startingPosY + resultButtonsHeightDifference);
                        buttonResult.Size = new System.Drawing.Size(15, 15);
                        buttonResult.Enabled = false;
                        resultButtonsRow.Add(buttonResult);
                        this.Controls.Add(buttonResult);
                    }

                    resultButtonsHeightDifference += 20;
                }

                resultButtonsHeightDifference = 0;
                gap = 0;
                this.r_ResultButtons.Add(resultButtonsRow);
                startingPosY += 45;
            }
        }

        private void guessButtonClick(object sender, EventArgs e)
        {
            Button guessButton = sender as Button;
            r_GuessBoard.ClickedButton = guessButton;
            r_GuessBoard.ShowDialog();
        }

        private void onClickedSubmit(object sender, EventArgs e)
        {
            getScoreButtonsColor();
            if (r_GameLogic.IsWin)
            {
                showComputerGuess();

            }

            resetStateOfColorsInGuessBoard();
            disableCurrentTurnButtons();
            if (m_CurrentTurn + 1 <= m_NumberOfGuesses - 1 && !r_GameLogic.IsWin)
            {
                m_CurrentTurn++;
                enableCurrentTurnButtons();
            }
            else
            {
                showComputerGuess();
            }
        }

        private void getScoreButtonsColor()
        {
            List<Button> resultColorButtons = new List<Button>();
            resultColorButtons = r_GameLogic.ScoreCalculation(r_GuessButtons[m_CurrentTurn], m_Target);
            for (int i = 0; i < resultColorButtons.Count; i++)
            {
                r_ResultButtons[m_CurrentTurn][i].BackColor = resultColorButtons[i].BackColor;
            }
        }

        private void popUpWindow(string i_Text)
        {
            System.Windows.Forms.MessageBox.Show(i_Text);
        }

        private void showComputerGuess()
        {
            for (int i = 0; i < m_Target.Length; i++)
            {
                r_HiddenButtons[i].BackColor = m_Target[i];
            }
        }

        private void enableCurrentTurnButtons()
        {
            foreach (Button button in r_GuessButtons[m_CurrentTurn])
            {
                button.Enabled = true;
            }
        }

        private void disableCurrentTurnButtons()
        {
            foreach (Button button in r_GuessButtons[m_CurrentTurn])
            {
                button.Enabled = false;
            }

            r_SubmitButtons[m_CurrentTurn].Enabled = false;
        }

        private void resetStateOfColorsInGuessBoard()
        {
            foreach (Button button in r_GuessBoard.ColorButtons)
            {
                button.Enabled = true;
            }
        }

        private void checkGuessButtonsColorState(object sender, EventArgs e)
        {
            bool IsAllColorsAreSet = true;
            foreach (Button guessButton in r_GuessButtons[m_CurrentTurn])
            {
                if (guessButton.UseVisualStyleBackColor)
                {
                    IsAllColorsAreSet = false;
                }
            }

            r_SubmitButtons[m_CurrentTurn].Enabled = IsAllColorsAreSet;
        }
    }
}
