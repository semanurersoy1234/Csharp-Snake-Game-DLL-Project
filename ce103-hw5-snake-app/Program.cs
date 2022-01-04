using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ce103_hw5_snake_dll;


namespace ce103_hw5_snake_app
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Class1 snakeGameFunctions = new Class1();
            snakeGameFunctions.welcomeArt();
            do
            {
                switch (snakeGameFunctions.mainMenu())
                {
                    case 0:
                        snakeGameFunctions.loadGame();
                        break;
                    //case 1:
                    //    snakeGameFunctions.displayHighScores();
                    //    break;
                    case 2:
                        snakeGameFunctions.controls();
                        break;
                    case 3:
                        snakeGameFunctions.exitYN();
                        break;
                }
            } while (true);
        }
    }
}
