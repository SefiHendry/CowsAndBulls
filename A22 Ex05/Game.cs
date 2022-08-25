using System;
using System.Windows.Forms;
using WinformsTesting;

internal class Game
{
    [STAThread]
    public void Run()
    {
        Application.EnableVisualStyles();
        Application.SetCompatibleTextRenderingDefault(false);
        Application.Run(new NumberOfGuessesWindow());
    }
}
