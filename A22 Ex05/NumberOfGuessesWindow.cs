using System;
using System.Windows.Forms;

namespace WinformsTesting
{
    internal class NumberOfGuessesWindow : Form
    {
        private Button m_ButtonChangeAmountOfGuesses;
        private Button m_ButtonStart;
        private byte m_NumberOfGuesses = 4;

        public NumberOfGuessesWindow()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.m_ButtonChangeAmountOfGuesses = new System.Windows.Forms.Button();
            this.m_ButtonStart = new System.Windows.Forms.Button();
            this.SuspendLayout();

            // m_ButtonChangeAmountOfGuesses
            this.m_ButtonChangeAmountOfGuesses.Location = new System.Drawing.Point(89, 21);
            this.m_ButtonChangeAmountOfGuesses.Name = "m_ButtonChangeAmountOfGuesses";
            this.m_ButtonChangeAmountOfGuesses.Size = new System.Drawing.Size(204, 41);
            this.m_ButtonChangeAmountOfGuesses.TabIndex = 0;
            this.m_ButtonChangeAmountOfGuesses.Text = "Number of chances: 4";
            this.m_ButtonChangeAmountOfGuesses.Click += new EventHandler(numGuessesClick);
            this.m_ButtonChangeAmountOfGuesses.UseVisualStyleBackColor = true;

            // m_ButtonStart
            this.m_ButtonStart.Location = new System.Drawing.Point(253, 124);
            this.m_ButtonStart.Name = "m_ButtonStart";
            this.m_ButtonStart.Size = new System.Drawing.Size(102, 40);
            this.m_ButtonStart.TabIndex = 1;
            this.m_ButtonStart.Text = "Start";
            this.m_ButtonStart.Click += new EventHandler(startClick);
            this.m_ButtonStart.UseVisualStyleBackColor = true;

            // NumberOfGuessesWindow
            this.ClientSize = new System.Drawing.Size(378, 176);
            this.Controls.Add(this.m_ButtonStart);
            this.Controls.Add(this.m_ButtonChangeAmountOfGuesses);
            this.Name = "NumberOfGuessesWindow";
            this.ResumeLayout(false);
        }

        private void numGuessesClick(object sender, EventArgs e)
        {
            m_NumberOfGuesses++;
            if (m_NumberOfGuesses > 10)
            {
                m_NumberOfGuesses = 4;
            }

            m_ButtonChangeAmountOfGuesses.Text = string.Format("Number of chances: {0}", m_NumberOfGuesses);
        }

        private void startClick(object sender, EventArgs e)
        {
            GameWindow game = new GameWindow(m_NumberOfGuesses);
            this.Hide();
            game.ShowDialog();
            this.Close();
        }
    }
}
