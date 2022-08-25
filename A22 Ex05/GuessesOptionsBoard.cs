using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinformsTesting
{
    internal class GuessesOptionsBoard : Form
    {
        private readonly List<Button> r_ColorButtons = new List<Button>();
        private readonly List<Color> r_Colors = new List<Color>()
        {
            Color.BlueViolet, Color.Blue, Color.Red, Color.Yellow,
            Color.Green, Color.Brown, Color.Aqua, Color.White,
        };

        private Button m_ClickedButton;

        public List<Button> ColorButtons
        {
            get { return r_ColorButtons; }
        }

        public Button ClickedButton
        {
            set { m_ClickedButton = value; }
        }

        public GuessesOptionsBoard()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            int startingPosX = 5;
            int startingPosY = 5;

            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    Button button = new Button();
                    button.Size = new System.Drawing.Size(40, 40);
                    button.Location = new System.Drawing.Point(startingPosX + (45 * i), startingPosY + (45 * j));
                    button.BackColor = r_Colors[(i * 2) + j];
                    r_ColorButtons.Add(button);
                    button.Click += new EventHandler(onClickSaveColor);

                    this.Controls.Add(button);
                }
            }

            this.ClientSize = new System.Drawing.Size(startingPosX + (45 * 4), startingPosY + (45 * 2));
        }

        private void onClickSaveColor(object sender, EventArgs e)
        {
            Button colorButton = sender as Button;
            int index = r_Colors.IndexOf(m_ClickedButton.BackColor);
            if (index != -1)
            {
                r_ColorButtons[index].Enabled = true;
            }

            colorButton.Enabled = false;
            m_ClickedButton.BackColor = colorButton.BackColor;

            this.Hide();
        }
    }
}
