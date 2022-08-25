using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

internal class GameLogic
{
    private bool m_IsIsWin = false;


    private Color[] m_TargetRange =
    {
        Color.BlueViolet, Color.Blue, Color.Red, Color.Yellow,
        Color.Green, Color.Brown, Color.Aqua, Color.White,
    };

    public Color[] ComputerGuess()
    {
        Color[] computerGuess = new Color[4];
        Random rnd = new Random();
        Color nextColor;
        bool flag = true;
        for (int i = 0; i < 4; i++)
        {
            nextColor = m_TargetRange[rnd.Next(m_TargetRange.Length)];
            for (int j = 0; j < computerGuess.Length; j++)
            {
                if (nextColor == computerGuess[j])
                {
                    flag = false;
                }
            }

            if (flag)
            {
                computerGuess[i] = nextColor;
            }
            else
            {
                flag = true;
                i--;
            }
        }

        return computerGuess;
    }

    public List<Button> ScoreCalculation(List<Button> i_Guess, Color[] i_ComputerGuess)
    {
        List<Button> currentTurnScore = new List<Button>();
        byte blackColorCounter = 0, yellowColorCounter = 0;
        for (int i = 0; i < i_Guess.Count; i++)
        {
            for (int j = 0; j < i_ComputerGuess.Length; j++)
            {
                if (i_Guess[i].BackColor == i_ComputerGuess[j])
                {
                    if (i == j)
                    {
                        blackColorCounter++;
                        break;
                    }
                    else
                    {
                        yellowColorCounter++;
                    }
                }
            }
        }

        if (blackColorCounter == 4)
        {
            m_IsIsWin = true;
        }

        for (int i = 0; i < blackColorCounter; i++)
        {
            currentTurnScore.Add(new Button());
            currentTurnScore[i].BackColor = Color.Black;
        }

        for (int i = blackColorCounter; i < yellowColorCounter + blackColorCounter; i++)
        {
            currentTurnScore.Add(new Button());
            currentTurnScore[i].BackColor = Color.Yellow;
        }

        return currentTurnScore;
    }

    public bool IsWin
    {
        get { return m_IsIsWin; }
        set { m_IsIsWin = value; }
    }
}