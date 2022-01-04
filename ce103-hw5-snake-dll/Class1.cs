/**************************
 * Copyleft (L) 2021 CENG - All Rights Not Reserved
 * You may use, distribute and modify this code.
 **************************/

/**
 * @file ce103-hw5-snake-dll
 * @author Semanur ERSOY
 * @date 04 January 2022
 *
 * @brief <b> HW-5 Functions </b>
 *
 * HW-5 Sample Lib Functions
 *
 * @see http://bilgisayar.mmf.erdogan.edu.tr/en/
 *
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ce103_hw5_snake_dll
{
	public class Class1
	{
		public const int SNAKE_ARRAY_SIZE = 310;
		public const int UP_ARROW = 38;
		public const int LEFT_ARROW = 37;
		public const int RIGHT_ARROW = 39;
		public const int DOWN_ARROW = 40;
		//public const int DOWN_ARROW = ConsoleKey.DownArrow;
		public const int ENTER_KEY = 13;
		public const int EXIT_BUTTON = 27;
		public const int PAUSE_BUTTON = 112;
		const char SNAKE_HEAD = (char)177;
		const char SNAKE_BODY = (char)178;
		const char WALL = (char)219;
		const char FOOD = (char)254;
		const char BLANK = ' ';
		//This should be the same on both operating systems

		/**
		 * @name waitForAnyKey
		 * The easiest way to wait for a keypress in C# is to use the Console. ReadKey() method. The Console. 
		 * 
		 **/
		public ConsoleKey waitForAnyKey()
		{
			ConsoleKey pressed;

			while (!Console.KeyAvailable) ;

			pressed = Console.ReadKey(false).Key;
			//pressed = tolower(pressed);
			return pressed;
		}

		/**
		 * @name getGameSpeed
		 * This method sets the interval value of the timer according to the value in the combobox.
		 * In short, it determines how fast the snake will move. The higher the value, the faster the snake moves.
		 *
		 **/
		public int getGameSpeed()
		{
			int speed = 10;
			Console.Clear();
			Console.SetCursorPosition(10, 5);
			Console.Write("Select The game speed between 1 and 9 and press enter\n");

			int selection = Convert.ToInt32(Console.ReadLine());
			switch (selection)
			{
				case 1:
					speed = 90;
					break;
				case 2:
					speed = 80;
					break;
				case 3:
					speed = 70;
					break;
				case 4:
					speed = 60;
					break;
				case 5:
					speed = 50;
					break;
				case 6:
					speed = 40;
					break;
				case 7:
					speed = 30;
					break;
				case 8:
					speed = 20;
					break;
				case 9:
					speed = 10;
					break;

			}

			return speed;
		}
		/**
		 * @name pauseMenu
		 * If the pressed key is P, the Timer is stopped or continued. Therefore, the game stops or continues.
		 *
		 **/
		public void pauseMenu()
		{


			Console.SetCursorPosition(28, 23);
			Console.WriteLine("**Paused**");

			waitForAnyKey();
			Console.SetCursorPosition(28, 23);
			Console.WriteLine("            ");

			return;
		}

		//This function checks if a key has pressed, then checks if its any of the arrow keys/ p/esc key. It changes direction acording to the key pressed.
		/**
		 * @name checkKeysPressed
		 * It is the method that gives the snake its movement.
		 * @param [in] direction [\b ConsoleKey]
		 *
		 **/
		public ConsoleKey checkKeysPressed(ConsoleKey direction)
		{
			ConsoleKey pressed;

			if (Console.KeyAvailable) //If a key has been pressed
			{

				pressed = Console.ReadKey(false).Key;
				if (direction != pressed)
				{
					if (pressed == ConsoleKey.DownArrow && direction != ConsoleKey.UpArrow)
						direction = pressed;
					else if (pressed == ConsoleKey.UpArrow && direction != ConsoleKey.DownArrow)
						direction = pressed;
					else if (pressed == ConsoleKey.LeftArrow && direction != ConsoleKey.RightArrow)
						direction = pressed;
					else if (pressed == ConsoleKey.RightArrow && direction != ConsoleKey.LeftArrow)
						direction = pressed;
					else if (pressed == ConsoleKey.P)
						pauseMenu();
				}
			}
			return (direction);
		}

		//Cycles around checking if the x y coordinates ='s the snake coordinates as one of this parts

		//One thing to note, a snake of length 4 cannot collide with itself, therefore there is no need to call this function when the snakes length is <= 4
		/**
		 * @name collisionSnake
		 * @param [in] x [\b int]
		 * @param [in] y [\b int]
		 * @param [in] snakeXY [\b int[,]]
		 * @retval [\b collisionSnake]
		 * method of showing the collision of the snake
		 **/
		public bool collisionSnake(int x, int y, int[,] snakeXY, int snakeLength, int detect)
		{
			int i;
			for (i = detect; i < snakeLength; i++) //Checks if the snake collided with itself
			{
				if (x == snakeXY[0, i] && y == snakeXY[1, i])
					return true;
			}
			return false;
		}

		//Generates food & Makes sure the food doesn't appear on top of the snake <- This sometimes causes a lag issue!!! Not too much of a problem tho
		/**
		 * @name generateFood
		 * @param [in] foodXY [\b int[]]
		 * @param [in] width [\b int]
		 * @param [in] height [\b int]
		 * @param [in] snakeXY [\b int[,]]
		 * @param [in] snakeLength [\b int]
		 *new feed-forming method
		 **/
		public int generateFood(int[] foodXY, int width, int height, int[,] snakeXY, int snakeLength)
		{


			do
			{
				Random rnd = new Random();
				foodXY[0] = rnd.Next() % (width - 2) + 2;
				foodXY[1] = rnd.Next() % (height - 6) + 2;
			} while (collisionSnake(foodXY[0], foodXY[1], snakeXY, snakeLength, 0));
			//This should prevent the "Food" from being created on top of the snake. - However the food has a chance to be created ontop of the snake, in which case the snake should eat it...

			Console.SetCursorPosition(foodXY[0], foodXY[1]);
			Console.WriteLine(FOOD);

			return (0);
		}

		/*
		Moves the snake array forward, i.e. 
		This:
		 x 1 2 3 4 5 6
		 y 1 1 1 1 1 1
		Becomes This:
		 x 1 1 2 3 4 5
		 y 1 1 1 1 1 1

		 Then depending on the direction (in this case west - left) it becomes:

		 x 0 1 2 3 4 5
		 y 1 1 1 1 1 1

		 snakeXY[0][0]--; <- if direction left, take 1 away from the x coordinate
		*/

		/**
		 * @name moveSnakeArray 
		 * @param [in] snakeLength [\b int]
		 * @param [in] snakeXY [\b int[,]]
		 * @param [in] direction [\b ConsoleKey]
		 * command that moves the snake
		 **/
		public void moveSnakeArray(int[,] snakeXY, int snakeLength, ConsoleKey direction)
		{
			int i;
			for (i = snakeLength - 1; i >= 1; i--)
			{
				snakeXY[0, i] = snakeXY[0, i - 1];
				snakeXY[1, i] = snakeXY[1, i - 1];
			}

			/*
			because we dont actually know the new snakes head x y, 
			we have to check the direction and add or take from it depending on the direction.
			*/
			switch (direction)
			{
				case ConsoleKey.DownArrow:
					snakeXY[1, 0]++;
					break;
				case ConsoleKey.RightArrow:
					snakeXY[0, 0]++;
					break;
				case ConsoleKey.UpArrow:
					snakeXY[1, 0]--;
					break;
				case ConsoleKey.LeftArrow:
					snakeXY[0, 0]--;
					break;
			}

			return;
		}
		/**
		 * @name move 
		 * @param [in] snakeLength [\b int]
		 * @param [in] snakeXY [\b int[,]]
		 * @param [in] direction [\b ConsoleKey]
		 * command that moves the snake
		 **/
		void move(int[,] snakeXY, int snakeLength, ConsoleKey direction)
		{
			int x;
			int y;

			//Remove the tail ( HAS TO BE DONE BEFORE THE ARRAY IS MOVED!!!!! )
			x = snakeXY[0, snakeLength - 1];
			y = snakeXY[1, snakeLength - 1];

			Console.SetCursorPosition(x, y);
			Console.WriteLine(BLANK);

			//Changes the head of the snake to a body part
			Console.SetCursorPosition(snakeXY[0, 0], snakeXY[1, 0]);
			Console.WriteLine(SNAKE_BODY);

			moveSnakeArray(snakeXY, snakeLength, direction);

			Console.SetCursorPosition(snakeXY[0, 0], snakeXY[1, 0]);
			Console.WriteLine(SNAKE_HEAD);

			Console.SetCursorPosition(1, 1); //Gets rid of the darn flashing underscore.

			return;
		}

		//This function checks if the snakes head his on top of the food, if it is then it'll generate some more food...
		/**
		 * @name eatfood
		 * @param [in] snakeXY [\b int[,]]
		 * @param [in] foodXY [\b int[]]
		 * The method invoked when it is determined that the snake has eaten the bait.
		 * In this method, another one is added to the snake from the last point of the snake. 
		 * That is, the snake is lengthened by one. Then the score is increased (the faster the snake, the higher the score). 
		 * Then the statistics on the screen are updated and a new feed is created.
		 **/
		public bool eatFood(int[,] snakeXY, int[] foodXY)
		{
			if (snakeXY[0, 0] == foodXY[0] && snakeXY[1, 0] == foodXY[1])
			{
				foodXY[0] = 0;
				foodXY[1] = 0; //This should prevent a nasty bug (loops) need to check if the bug still exists.
				return true;
			}

			return false;
		}

		/**
		 * @name collisionDetection 
		 * @param [in] snakeLength [\b int]
		 * @param [in] snakeXY [\b int[,]]
		 * @param [in] consoleWidth [\b int]
		 * command that moves the snake
		 **/
		public int collisionDetection(int[,] snakeXY, int consoleWidth, int consoleHeight, int snakeLength) //Need to Clean this up a bit
		{
			int colision = 0;
			if ((snakeXY[0, 0] == 1) || (snakeXY[1, 0] == 1) || (snakeXY[0, 0] == consoleWidth) || (snakeXY[1, 0] == consoleHeight - 4)) //Checks if the snake collided wit the wall or it's self
				colision = 1;
			else
				if (collisionSnake(snakeXY[0, 0], snakeXY[1, 0], snakeXY, snakeLength, 1)) //If the snake collided with the wall, theres no point in checking if it collided with itself.
				colision = 1;

			return (colision);
		}

		/**
		 * @name refreshInfoBar
		 * @param [in] score [\b int]
		 * @param [in] speed [\b int]
		 * the method that creates the start snake when you want to restart the game again.
		 **/
		public void refreshInfoBar(int score, int speed)
		{
			Console.SetCursorPosition(5, 23);
			Console.WriteLine("Score:" + score);

			Console.SetCursorPosition(5, 24);
			Console.WriteLine("Speed:" + speed);

			Console.SetCursorPosition(52, 23);
			Console.WriteLine("Coder: Semanur Ersoy");

			Console.SetCursorPosition(52, 24);
			Console.WriteLine("Version: 0.5");

			return;
		}

		//**************HIGHSCORE STUFF**************//

		//-> The highscores system seriously needs to be clean. There are some bugs, entering a name etc

		//void createHighScores()
		//{
		//	FILE* file;
		//	int i;

		//	file = fopen("highscores.txt", "w+");

		//	if (file == NULL)
		//	{
		//		Console.WriteLine("FAILED TO CREATE HIGHSCORES!!! EXITING!");
		//		exit(0);
		//	}

		//	for (i = 0; i < 5; i++)
		//	{
		//		fConsole.WriteLine(file, "%d", i + 1);
		//		fConsole.WriteLine(file, "%s", "\t0\t\t\tEMPTY\n");
		//	}

		//	fclose(file);
		//	return;
		//}

		//int getLowestScore()
		//{
		//	FILE* fp;
		//	char str[128];
		//	int lowestScore = 0;
		//	int i;
		//	int intLength;

		//	if ((fp = fopen("highscores.txt", "r")) == NULL)
		//	{
		//		//Create the file, then try open it again.. if it fails this time exit.
		//		createHighScores(); //This should create a highscores file (If there isn't one)
		//		if ((fp = fopen("highscores.txt", "r")) == NULL)
		//			exit(1);
		//	}

		//	while (!feof(fp))
		//	{
		//		fgets(str, 126, fp);
		//	}
		//	fclose(fp);

		//	i = 0;

		//	//Gets the Int length
		//	while (str[2 + i] != '\t')
		//	{
		//		i++;
		//	}

		//	intLength = i;

		//	//Gets converts the string to int
		//	for (i = 0; i < intLength; i++)
		//	{
		//		lowestScore = lowestScore + ((int)str[2 + i] - 48) * pow(10, intLength - i - 1);
		//	}

		//	return (lowestScore);
		//}

		//void inputScore(int score) //This seriously needs to be cleaned up
		//{
		//	FILE* fp;
		//	FILE* file;
		//	char str[20];
		//	int fScore;
		//	int i, s, y;
		//	int intLength;
		//	int scores[5];
		//	int x;
		//	char highScoreName[20];
		//	char highScoreNames[5][20];

		//	char name[20];

		//	int entered = 0;

		//	Console.Clear(); //clear the console

		//	if ((fp = fopen("highscores.txt", "r")) == NULL)
		//	{
		//		//Create the file, then try open it again.. if it fails this time exit.
		//		createHighScores(); //This should create a highscores file (If there isn't one)
		//		if ((fp = fopen("highscores.txt", "r")) == NULL)
		//			exit(1);
		//	}
		//	Console.SetCursorPosition(10, 5);
		//	Console.WriteLine("Your Score made it into the top 5!!!");
		//	Console.SetCursorPosition(10, 6);
		//	Console.WriteLine("Please enter your name: ");
		//	gets(name);

		//	x = 0;
		//	while (!feof(fp))
		//	{
		//		fgets(str, 126, fp);  //Gets a line of text

		//		i = 0;

		//		//Gets the Int length
		//		while (str[2 + i] != '\t')
		//		{
		//			i++;
		//		}

		//		s = i;
		//		intLength = i;
		//		i = 0;
		//		while (str[5 + s] != '\n')
		//		{
		//			//Console.WriteLine("%c",str[5+s]);
		//			highScoreName[i] = str[5 + s];
		//			s++;
		//			i++;
		//		}
		//		//Console.WriteLine("\n");

		//		fScore = 0;
		//		//Gets converts the string to int
		//		for (i = 0; i < intLength; i++)
		//		{
		//			//Console.WriteLine("%c", str[2+i]);
		//			fScore = fScore + ((int)str[2 + i] - 48) * pow(10, intLength - i - 1);
		//		}

		//		if (score >= fScore && entered != 1)
		//		{
		//			scores[x] = score;
		//			strcpy(highScoreNames[x], name);

		//			//Console.WriteLine("%d",x+1);
		//			//Console.WriteLine("\t%d\t\t\t%s\n",score, name);		
		//			x++;
		//			entered = 1;
		//		}

		//		//Console.WriteLine("%d",x+1);
		//		//Console.WriteLine("\t%d\t\t\t%s\n",fScore, highScoreName);
		//		//strcpy(text, text+"%d\t%d\t\t\t%s\n");
		//		strcpy(highScoreNames[x], highScoreName);
		//		scores[x] = fScore;

		//		//highScoreName = "";
		//		for (y = 0; y < 20; y++)
		//		{
		//			highScoreName[y] = 0x00; //NULL
		//		}

		//		x++;
		//		if (x >= 5)
		//			break;
		//	}

		//	fclose(fp);

		//	file = fopen("highscores.txt", "w+");

		//	for (i = 0; i < 5; i++)
		//	{
		//		//Console.WriteLine("%d\t%d\t\t\t%s\n", i+1, scores[i], highScoreNames[i]);
		//		fConsole.WriteLine(file, "%d\t%d\t\t\t%s\n", i + 1, scores[i], highScoreNames[i]);
		//	}

		//	fclose(file);

		//	return;
		//}

		//void displayHighScores() //NEED TO CHECK THIS CODE!!!
		//{
		//	FILE* fp;
		//	char str[128];
		//	int y = 5;

		//	Console.Clear(); //clear the console

		//	if ((fp = fopen("highscores.txt", "r")) == NULL)
		//	{
		//		//Create the file, then try open it again.. if it fails this time exit.
		//		createHighScores(); //This should create a highscores file (If there isn't one)
		//		if ((fp = fopen("highscores.txt", "r")) == NULL)
		//			exit(1);
		//	}

		//	Console.SetCursorPosition(10, y++);
		//	Console.WriteLine("High Scores");
		//	Console.SetCursorPosition(10, y++);
		//	Console.WriteLine("Rank\tScore\t\t\tName");
		//	while (!feof(fp))
		//	{
		//		Console.SetCursorPosition(10, y++);
		//		if (fgets(str, 126, fp))
		//			Console.WriteLine("%s", str);
		//	}

		//	fclose(fp); //Close the file
		//	Console.SetCursorPosition(10, y++);

		//	Console.WriteLine("Press any key to continue...");
		//	waitForAnyKey();
		//	return;
		//}

		////**************END HIGHSCORE STUFF**************//


		/**
		 * @name youWinScreen
		 *code showing win on screen
		 **/
		public void youWinScreen()
		{
			int x = 6, y = 7;
			Console.SetCursorPosition(x, y++);
			Console.WriteLine("'##:::'##::'#######::'##::::'##::::'##:::::'##:'####:'##::: ##:'####:");
			Console.SetCursorPosition(x, y++);
			Console.WriteLine(". ##:'##::'##.... ##: ##:::: ##:::: ##:'##: ##:. ##:: ###:: ##: ####:");
			Console.SetCursorPosition(x, y++);
			Console.WriteLine(":. ####::: ##:::: ##: ##:::: ##:::: ##: ##: ##:: ##:: ####: ##: ####:");
			Console.SetCursorPosition(x, y++);
			Console.WriteLine("::. ##:::: ##:::: ##: ##:::: ##:::: ##: ##: ##:: ##:: ## ## ##:: ##::");
			Console.SetCursorPosition(x, y++);
			Console.WriteLine("::: ##:::: ##:::: ##: ##:::: ##:::: ##: ##: ##:: ##:: ##. ####::..:::");
			Console.SetCursorPosition(x, y++);
			Console.WriteLine("::: ##:::: ##:::: ##: ##:::: ##:::: ##: ##: ##:: ##:: ##:. ###:'####:");
			Console.SetCursorPosition(x, y++);
			Console.WriteLine("::: ##::::. #######::. #######:::::. ###. ###::'####: ##::. ##: ####:");
			Console.SetCursorPosition(x, y++);
			Console.WriteLine(":::..::::::.......::::.......:::::::...::...:::....::..::::..::....::");
			Console.SetCursorPosition(x, y++);

			waitForAnyKey();
			Console.Clear(); //clear the console
			return;
		}

		/**
		 * @name gameOverScreen
		 * method that checks whether the game is over. If the commented if method is removed from the comment,
		 * the game will be over when the snake hits the wall. If it stays in the comment, the snake will be able to pass through the wall.
		 * The bottom if checks if the snake hit its own tail.
		 *
		 **/
		public void gameOverScreen()
		{
			int x = 17, y = 3;

			//http://www.network-science.de/ascii/ <- Ascii Art Gen

			Console.SetCursorPosition(x, y++);
			Console.WriteLine(":'######::::::'###::::'##::::'##:'########:\n");
			Console.SetCursorPosition(x, y++);
			Console.WriteLine("'##... ##::::'## ##::: ###::'###: ##.....::\n");
			Console.SetCursorPosition(x, y++);
			Console.WriteLine(" ##:::..::::'##:. ##:: ####'####: ##:::::::\n");
			Console.SetCursorPosition(x, y++);
			Console.WriteLine(" ##::'####:'##:::. ##: ## ### ##: ######:::\n");
			Console.SetCursorPosition(x, y++);
			Console.WriteLine(" ##::: ##:: #########: ##. #: ##: ##...::::\n");
			Console.SetCursorPosition(x, y++);
			Console.WriteLine(" ##::: ##:: ##.... ##: ##:.:: ##: ##:::::::\n");
			Console.SetCursorPosition(x, y++);
			Console.WriteLine(". ######::: ##:::: ##: ##:::: ##: ########:\n");
			Console.SetCursorPosition(x, y++);
			Console.WriteLine(":......::::..:::::..::..:::::..::........::\n");
			Console.SetCursorPosition(x, y++);
			Console.WriteLine(":'#######::'##::::'##:'########:'########::'####:\n");
			Console.SetCursorPosition(x, y++);
			Console.WriteLine("'##.... ##: ##:::: ##: ##.....:: ##.... ##: ####:\n");
			Console.SetCursorPosition(x, y++);
			Console.WriteLine(" ##:::: ##: ##:::: ##: ##::::::: ##:::: ##: ####:\n");
			Console.SetCursorPosition(x, y++);
			Console.WriteLine(" ##:::: ##: ##:::: ##: ######::: ########::: ##::\n");
			Console.SetCursorPosition(x, y++);
			Console.WriteLine(" ##:::: ##:. ##:: ##:: ##...:::: ##.. ##::::..:::\n");
			Console.SetCursorPosition(x, y++);
			Console.WriteLine(" ##:::: ##::. ## ##::: ##::::::: ##::. ##::'####:\n");
			Console.SetCursorPosition(x, y++);
			Console.WriteLine(". #######::::. ###:::: ########: ##:::. ##: ####:\n");
			Console.SetCursorPosition(x, y++);
			Console.WriteLine(":.......::::::...:::::........::..:::::..::....::\n");

			waitForAnyKey();
			Console.Clear(); //clear the console
			return;
		}

		//Messy, need to clean this function up
		/**
		 * @name startGame
		 * The method that starts the game. It disables the speed selection box, disables the Start button,
		 * sets the game speed and starts the Timer.
		 * As long as the timer is active, the playGame method will run and the snake will constantly move.
		 * @param [in] snakeXY [\b int[,]]
		 * @param [in] foodXY [\b int[]]
		 * @param [in] consoleWidth [\b int]
		 * @param [in] consoleHeight [\b int]
		 * @param [in] snakeLength [\b int]
		 * @param [in] direction [\b ConsoleKey]
		 * @param [in] speed [\b int]
		 * @param [in] score [\b int]
		 **/
		public void startGame(int[,] snakeXY, int[] foodXY, int consoleWidth, int consoleHeight, int snakeLength, ConsoleKey direction, int score, int speed)
		{
			int gameOver = 0;
			ConsoleKey oldDirection = 0;
			bool canChangeDirection = true;
			//int seconds = 1;
			do
			{
				if (canChangeDirection)
				{
					oldDirection = direction;
					direction = checkKeysPressed(direction);
				}

				if (oldDirection != direction)//Temp fix to prevent the snake from colliding with itself
					canChangeDirection = false;

				if (true) //haha, it moves according to how fast the computer running it is...
				{
					//Console.SetCursorPosition(1,1);
					//Console.WriteLine("%d - %d",clock() , endWait);
					move(snakeXY, snakeLength, direction);
					canChangeDirection = true;


					if (eatFood(snakeXY, foodXY))
					{
						generateFood(foodXY, consoleWidth, consoleHeight, snakeXY, snakeLength); //Generate More Food
						snakeLength++;
						switch (speed)
						{
							case 10:
								score += 90;
								break;
							case 20:
								score += 80;
								break;
							case 30:
								score += 70;
								break;
							case 40:
								score += 60;
								break;
							case 50:
								score += 50;
								break;
							case 60:
								score += 40;
								break;
							case 70:
								score += 30;
								break;
							case 80:
								score += 20;
								break;
							case 90:
								score += 10;
								break;
						}


						refreshInfoBar(score, speed);
					}
					Thread.Sleep(speed);

				}

				gameOver = collisionDetection(snakeXY, consoleWidth, consoleHeight, snakeLength);

				if (snakeLength >= SNAKE_ARRAY_SIZE - 5) //Just to make sure it doesn't get longer then the array size & crash
				{
					gameOver = 2;//You Win! <- doesn't seem to work - NEED TO FIX/TEST THIS
					score += 1500; //When you win you get an extra 1500 points!!!
				}

			} while (gameOver != 1);

			switch (gameOver)
			{
				case 1:
					gameOverScreen();

					break;
				case 2:
					youWinScreen();
					break;
			}

			return;
		}
		/**
		 * @name loadEnviroment
		 * @param [in] consoleWidth [\b int]
		 * @param [in] consoleHeight [\b int]
		 * this method actually just draws a box on the screen. The using line states that the panel should be used.
		 **/
		public void loadEnviroment(int consoleWidth, int consoleHeight)//This can be done in a better way... FIX ME!!!! Also i think it doesn't work properly in ubuntu <- Fixed
		{

			int x = 1, y = 1;
			int rectangleHeight = consoleHeight - 4;
			Console.Clear(); //clear the console

			Console.SetCursorPosition(x, y); //Top left corner

			for (; y < rectangleHeight; y++)
			{
				Console.SetCursorPosition(x, y); //Left Wall 
				Console.WriteLine(WALL);

				Console.SetCursorPosition(consoleWidth, y); //Right Wall
				Console.WriteLine(WALL);
			}

			y = 1;
			for (; x < consoleWidth + 1; x++)
			{
				Console.SetCursorPosition(x, y); //Left Wall 
				Console.WriteLine(WALL);

				Console.SetCursorPosition(x, rectangleHeight); //Right Wall
				Console.WriteLine(WALL);
			}


			return;
		}

		/**
		 * @name loadSnake
		 * @param [in] snakeXY [\b int[,]]
		 * @param [in] snakeLength [\b int]
		 *is the code that creates the snake install command
		 **/
		public void loadSnake(int[,] snakeXY, int snakeLength)
		{
			int i;
			/*
			First off, The snake doesn't actually have enough XY coordinates (only 1 - the starting location), thus we use
			these XY coordinates to "create" the other coordinates. For this we can actually use the function used to move the snake.
			This helps create a "whole" snake instead of one "dot", when someone starts a game.
			*/
			//moveSnakeArray(snakeXY, snakeLength); //One thing to note ATM, the snake starts of one coordinate to whatever direction it's pointing...

			//This should print out a snake :P
			for (i = 0; i < snakeLength; i++)
			{
				Console.SetCursorPosition(snakeXY[0, i], snakeXY[1, i]);
				Console.WriteLine(SNAKE_BODY); //Meh, at some point I should make it so the snake starts off with a head...
			}

			return;
		}

		/* NOTE, This function will only work if the snakes starting direction is left!!!! 
		Well it will work, but the results wont be the ones expected.. I need to fix this at some point.. */
		/**
		 * @name prepairSnakeArray
		 * @param [in] snakeXY [\b int[,]]
		 * @param [in] snakeLength [\b int]
		 * snake creation command code
		 **/
		public void prepairSnakeArray(int[,] snakeXY, int snakeLength)
		{
			int i;
			int snakeX = snakeXY[0, 0];
			int snakeY = snakeXY[1, 0];

			for (i = 1; i <= snakeLength; i++)
			{
				snakeXY[0, i] = snakeX + i;
				snakeXY[1, i] = snakeY;
			}

			return;
		}

		//This function loads the enviroment, snake, etc
		/**
		 * @name loadGame
		 * 
		 *
		 **/
		public void loadGame()
		{
			int[,] snakeXY = new int[2, SNAKE_ARRAY_SIZE]; //Two Dimentional Array, the first array is for the X coordinates and the second array for the Y coordinates

			int snakeLength = 4; //Starting Length

			ConsoleKey direction = ConsoleKey.LeftArrow; //DO NOT CHANGE THIS TO RIGHT ARROW, THE GAME WILL INSTANTLY BE OVER IF YOU DO!!! <- Unless the prepairSnakeArray function is changed to take into account the direction....

			int[] foodXY = { 5, 5 };// Stores the location of the food

			int score = 0;
			//int level = 1;

			//Window Width * Height - at some point find a way to get the actual dimensions of the console... <- Also somethings that display dont take this dimentions into account.. need to fix this...
			int consoleWidth = 80;
			int consoleHeight = 25;

			int speed = getGameSpeed();

			//The starting location of the snake
			snakeXY[0, 0] = 40;
			snakeXY[1, 0] = 10;

			loadEnviroment(consoleWidth, consoleHeight); //borders
			prepairSnakeArray(snakeXY, snakeLength);
			loadSnake(snakeXY, snakeLength);
			generateFood(foodXY, consoleWidth, consoleHeight, snakeXY, snakeLength);
			refreshInfoBar(score, speed); //Bottom info bar. Score, Level etc
			startGame(snakeXY, foodXY, consoleWidth, consoleHeight, snakeLength, direction, score, speed);

			return;
		}

		//**************MENU STUFF**************//
		/**
		 * @name menuSelector
		 * @param [in] x [\b int]
		 * @param [in] y [\b int]
		 * @param [in] yStart [\b 
		 * This projection method requires the transform function, selector, to produce one value for each value in the source sequence, source.
		 * @retval [\b int]
		 **/
		public int menuSelector(int x, int y, int yStart)
		{
			ConsoleKey key;
			int i = 0;
			x = x - 2;
			Console.SetCursorPosition(x, yStart);

			Console.WriteLine(">");

			Console.SetCursorPosition(1, 1);


			do
			{
				key = waitForAnyKey();
				//Console.WriteLine("%c %d", key, (int)key);
				if (key == ConsoleKey.UpArrow)
				{
					Console.SetCursorPosition(x, yStart + i);
					Console.WriteLine(" ");

					if (yStart >= yStart + i)
						i = y - yStart - 2;
					else
						i--;
					Console.SetCursorPosition(x, yStart + i);
					Console.WriteLine(">");
				}
				else
					if (key == ConsoleKey.DownArrow)
				{
					Console.SetCursorPosition(x, yStart + i);
					Console.WriteLine(" ");

					if (i + 2 >= y - yStart)
						i = 0;
					else
						i++;
					Console.SetCursorPosition(x, yStart + i);
					Console.WriteLine(">");
				}
				//Console.SetCursorPosition(1,1);
				//Console.WriteLine("%d", key);
			} while (key != ConsoleKey.Enter); //While doesn't equal enter... (13 ASCII code for enter) - note ubuntu is 10
			return (i);
		}
		/**
		 * @name welcomeArt
		 * 
		 * It allows you to upload an image which will be converted into ASCII art Pretty cool stuff.
		 **/
		public void welcomeArt()
		{
			Console.Clear(); //clear the console
							 //Ascii art reference: http://www.chris.com/ascii/index.php?art=animals/reptiles/snakes
			Console.WriteLine("\n");
			Console.WriteLine("\t\t    _________         _________ 			\n");
			Console.WriteLine("\t\t   /         \\       /         \\ 			\n");
			Console.WriteLine("\t\t  /  /~~~~~\\  \\     /  /~~~~~\\  \\ 			\n");
			Console.WriteLine("\t\t  |  |     |  |     |  |     |  | 			\n");
			Console.WriteLine("\t\t  |  |     |  |     |  |     |  | 			\n");
			Console.WriteLine("\t\t  |  |     |  |     |  |     |  |         /	\n");
			Console.WriteLine("\t\t  |  |     |  |     |  |     |  |       //	\n");
			Console.WriteLine("\t\t (o  o)    \\  \\_____/  /     \\  \\_____/ / 	\n");
			Console.WriteLine("\t\t  \\__/      \\         /       \\        / 	\n");
			Console.WriteLine("\t\t    |        ~~~~~~~~~         ~~~~~~~~ 		\n");
			Console.WriteLine("\t\t    ^											\n");
			Console.WriteLine("\t		Welcome To The Snake Game!			\n");
			Console.WriteLine("\t	   Press Any Key To Continue...	\n");
			Console.WriteLine("\n");

			waitForAnyKey();
			return;
		}
		/**
		 * @name controls 
		 * While developing Windows forms or web forms, 
		 * the controls that come with the .Net standard library (button, textbox, etc.) are often sufficient, 
		 * but visual control development specific to the classes developed in the project is required. In these cases,
		 * what we call User Control comes into play.
		 *
		 **/
		public void controls()
		{
			int x = 10, y = 5;
			Console.Clear(); //clear the console
			Console.SetCursorPosition(x, y++);
			Console.WriteLine("Controls\n");
			Console.SetCursorPosition(x++, y++);
			Console.WriteLine("Use the following arrow keys to direct the snake to the food: ");
			Console.SetCursorPosition(x, y++);
			Console.WriteLine("Right Arrow");
			Console.SetCursorPosition(x, y++);
			Console.WriteLine("Left Arrow");
			Console.SetCursorPosition(x, y++);
			Console.WriteLine("Top Arrow");
			Console.SetCursorPosition(x, y++);
			Console.WriteLine("Bottom Arrow");
			Console.SetCursorPosition(x, y++);
			Console.SetCursorPosition(x, y++);
			Console.WriteLine("P & Esc pauses the game.");
			Console.SetCursorPosition(x, y++);
			Console.SetCursorPosition(x, y++);
			Console.WriteLine("Press any key to continue...");
			waitForAnyKey();
			return;
		}
		/**
		 * @name exitYN
		 * 
		 * The exit code to be returned to the operating system. Use 0 (zero) to indicate successful completion of the operation.
		 **/
		public void exitYN()
		{
			ConsoleKey pressed;
			Console.SetCursorPosition(9, 8);
			Console.WriteLine("Are you sure you want to exit(Y/N)\n");

			do
			{
				pressed = waitForAnyKey();
			} while (!(pressed == ConsoleKey.Y || pressed == ConsoleKey.N));

			if (pressed == ConsoleKey.Y)
			{
				Console.Clear(); //clear the console
				Environment.Exit(0);
			}
			return;
		}

		/**
		 * @name mainMenu
		 * 
		 * Initializes a new instance of the class without the specified menu item.
		 *
		 **/
		public int mainMenu()
		{
			int x = 10, y = 5;
			int yStart = y;

			int selected;

			Console.Clear(); //clear the console
							 //Might be better with arrays of strings???
			Console.SetCursorPosition(x, y++);
			Console.WriteLine("New Game\n");
			Console.SetCursorPosition(x, y++);
			Console.WriteLine("High Scores\n");
			Console.SetCursorPosition(x, y++);
			Console.WriteLine("Controls\n");
			Console.SetCursorPosition(x, y++);
			Console.WriteLine("Exit\n");
			Console.SetCursorPosition(x, y++);

			selected = menuSelector(x, y, yStart);

			return (selected);
		}

		//**************END MENU STUFF**************//


	}
}
