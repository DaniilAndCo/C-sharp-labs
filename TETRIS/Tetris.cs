using System;
using System.Collections.Generic;
using System.Threading;
namespace TETRIS
{
	static class TetrisGame
	{
		static private Random randValue = new Random();
		static private int widthOfField_X, widthOfField_Y;
		static private char fieldFiller = ' ';
		static private bool[,] collisionMap;
		static private int widthOfBorder = 1;
		static private char borderFiller = '█';
		static private int margin_X;
		static private int margin_Y;
		static private int velocity;
		static private int score = 0;
		static private int scoreToWin;
		static private char pointFiller = '█';
		static private int[] listOfLengthsOfHorizontalCharacters = {
			0,
			1,
			2,
			3,
			4,
			5
		};
		static private int currentLeftPosition_X, currentPosition_Y;
		static private int previousLeftPosition_X, previousPosition_Y;
		static private int tempIndex = 0;
		static public void SetTetrisGame(int _WidthOfField_X, int _WidthOfField_Y, (int, int) Mode)
		{
			widthOfField_X = _WidthOfField_X;
			widthOfField_Y = _WidthOfField_Y;
			velocity = Mode.Item1;
			scoreToWin = Mode.Item2;
			collisionMap = new bool[widthOfField_Y + 2 * widthOfBorder, widthOfField_X];
			for (int i = 0; i < widthOfField_Y + 2 * widthOfBorder; i++)
			{
				for (int j = 0; j < widthOfField_X; j++)
				{
					if (i == widthOfField_Y + 2 * widthOfBorder - 1)
					{
						collisionMap[i, j] = true;
					}
					else
					{
						collisionMap[i, j] = false;
					}
				}
			}
			return;
		}
		static private void SetDesign(int _Margin_X, int _Margin_Y)
		{
			score = 0;
			margin_X = _Margin_X;
			margin_Y = _Margin_Y;
			return;
		}
		static private void PrintMap()
		{
			Console.ForegroundColor = ConsoleColor.Gray;
			Console.SetCursorPosition(margin_X, margin_Y);
			for (int i = 0; i <= widthOfField_X + widthOfBorder; ++i)
			{
				Console.Write(borderFiller);
			}
			Console.SetCursorPosition(0, 0);
			Console.Write("Score: " + score + " of " + scoreToWin);

			for (int i = 1; i <= widthOfField_Y + widthOfBorder; ++i)
			{
				Console.SetCursorPosition(margin_X, margin_Y + i);
				Console.Write(borderFiller);
				Console.SetCursorPosition(margin_X + widthOfBorder + widthOfField_X, margin_Y + i);
				Console.Write(borderFiller);
			}
			Console.SetCursorPosition(margin_X, margin_Y + widthOfField_Y + widthOfBorder);
			for (int i = 0; i <= widthOfField_X + widthOfBorder; ++i)
			{
				Console.Write(borderFiller);
			}
			return;

		}
		static private void Input()
		{
			previousLeftPosition_X = currentLeftPosition_X;
			previousPosition_Y = currentPosition_Y;
			if (Console.KeyAvailable)
			{
				switch (Console.ReadKey(true).Key)
				{
					case ConsoleKey.Spacebar:
						previousPosition_Y = currentPosition_Y;
						while ( currentPosition_Y != margin_Y + widthOfField_Y + 1)
						{
							++currentPosition_Y;
							if (IsStuck())
								{
								Console.SetCursorPosition(currentLeftPosition_X + listOfLengthsOfHorizontalCharacters[tempIndex] + 1
									, previousPosition_Y);
								for (int i = 0; i <= listOfLengthsOfHorizontalCharacters[tempIndex]; ++i)
								{
									Console.Write('\b');
								}
								for (int i = 0; i <= listOfLengthsOfHorizontalCharacters[tempIndex]; ++i)
								{
									Console.Write(fieldFiller);
								}
								previousPosition_Y = --currentPosition_Y;
									break;
								}
						}					
						break;
					case ConsoleKey.A:
					case ConsoleKey.LeftArrow:
						if (currentLeftPosition_X - margin_X > 1) --currentLeftPosition_X;
						break;
					case ConsoleKey.D:
					case ConsoleKey.RightArrow:
						if (currentLeftPosition_X + listOfLengthsOfHorizontalCharacters[tempIndex] < margin_X + widthOfField_X) ++currentLeftPosition_X;
						break;
				}
			}
			return;
		}
		static private void PrintCharacter()
		{
			Console.SetCursorPosition(currentLeftPosition_X, currentPosition_Y);
			for (int i = 0; i <= listOfLengthsOfHorizontalCharacters[tempIndex]; ++i)
			{
				Console.Write(pointFiller);
			}
		}
		static private void SetCharacter()
		{
			Console.ForegroundColor = (ConsoleColor)randValue.Next(1, 14);
			previousLeftPosition_X = currentLeftPosition_X =
				randValue.Next(margin_X + widthOfBorder, margin_X + widthOfField_X - listOfLengthsOfHorizontalCharacters[tempIndex] + widthOfBorder);
			previousPosition_Y = currentPosition_Y = margin_Y + widthOfBorder;
			return;
		}
		static private bool IsGameOver()
		{
			return (currentPosition_Y == margin_Y + 2 * widthOfBorder
				&& IsStuck()) ? true : false;
		}
		static private void Fall()
		{
			if (currentPosition_Y <= margin_Y + widthOfField_Y && 
				!collisionMap[currentPosition_Y - margin_Y,
				currentLeftPosition_X - margin_X - widthOfBorder])
			{
				++currentPosition_Y;
			}
		}
		static private bool IsStuck()
		{
			for (int i = currentLeftPosition_X; i <=
				currentLeftPosition_X + listOfLengthsOfHorizontalCharacters[tempIndex]; ++i)
			{
				if (collisionMap[currentPosition_Y - margin_Y, i - margin_X - widthOfBorder])
				{
					return true;
				}
			}
			return false;
		}
		static private void IfStuck()
		{
			for (int i = previousLeftPosition_X; i <=
				previousLeftPosition_X + listOfLengthsOfHorizontalCharacters[tempIndex]; ++i)
			{
				collisionMap[previousPosition_Y - margin_Y, i - margin_X - widthOfBorder] = true;
			}
			return;
		}
		static private void CheckDone()
		{
			for (int i = widthOfField_Y; i >= 0; --i)
			{
				if (IsRowDone(ref i))
				{
					DestroyRow(ref i);
					velocity = (int)(velocity * 1.2);
				}
			}
			return;
		}
		static private bool IsRowDone(ref int index)
		{
			bool temp = true;
			for (int j = 0; j < widthOfField_X; j++)
			{
				temp = temp && collisionMap[index, j];
			}
			return temp;
		}
		static private void FallOfRows(ref int index)
		{
			for (int i = index - 1; i > 0; --i)
			{
				for (int j = 0; j < widthOfField_X; ++j)
				{
					if (collisionMap[i, j])
					{
						collisionMap[i, j] = false;
						Console.SetCursorPosition(j + margin_X + widthOfBorder + 1, margin_Y + i);
						Console.Write('\b');
						Thread.Sleep(5);
						Console.Write(fieldFiller);
						collisionMap[i + 1, j] = true;
						Console.SetCursorPosition(j + margin_X + widthOfBorder, margin_Y + i + 1);
						Thread.Sleep(5);
						Console.Write(pointFiller);
					}
				}
			}
			return;
		}
		static private void DestroyRow(ref int index)
		{
			Console.ForegroundColor = ConsoleColor.Gray;
			Console.SetCursorPosition(widthOfField_X + margin_X + widthOfBorder, index + margin_Y);
			for (int i = 0; i < widthOfField_X; ++i)
			{
				collisionMap[index, i] = false;
				Console.Write('\b');
			}
			for (int i = 0; i < widthOfField_X; ++i)
			{
				Console.Write(fieldFiller);
			}
			FallOfRows(ref index);
			score += (widthOfField_Y - index + 1) * 10;
			UpateScore();
			return;
		}
		static private void Clear()
		{
		
			Console.SetCursorPosition(previousLeftPosition_X + listOfLengthsOfHorizontalCharacters[tempIndex] + 1, previousPosition_Y);
			for (int i = 0; i <= listOfLengthsOfHorizontalCharacters[tempIndex]; ++i)
			{
				Console.Write('\b');
			}
			for (int i = 0; i <= listOfLengthsOfHorizontalCharacters[tempIndex]; i++)
			{
				Console.Write(fieldFiller);
			}
			Console.SetCursorPosition(currentLeftPosition_X, currentPosition_Y);
			return;
		}
		static private void UpateScore()
		{
			Console.ForegroundColor = ConsoleColor.Green;
			int distance = "Score outs of ".Length + 
				Convert.ToString(score).Length + Convert.ToString(scoreToWin).Length;
			Console.SetCursorPosition(distance + 1, 0);
			for (int i = 0; i < Convert.ToString(score).Length 
				+ Convert.ToString(scoreToWin).Length + " outs of ".Length; ++i)
			{
				Console.Write('\b');
				Console.Write(' ');
				Console.Write('\b');
			}
			Console.Write(score + " outs of " + scoreToWin);
			return;
		}
		static private void YouWon()
		{
			Console.ForegroundColor = ConsoleColor.Green;
			Console.SetCursorPosition(0, 1);
			Console.Write("Congratulations, you won and got " + score + " points\n" +
				" Press Space to return to the Main Menu...");
			while (Console.ReadKey(true).Key != ConsoleKey.Spacebar)
			{				
			}
			Console.Clear();
			Menu.ShowMenu();
			return;
		}
		static private void YouLose()
		{
			Console.ForegroundColor = ConsoleColor.Red;
			Console.SetCursorPosition(0, 1);
			Console.Write("It's sad but you lose and got only " + score + " points\n" +
				"Press Space to return to the Main Menu...");
			while (Console.ReadKey(true).Key != ConsoleKey.Spacebar) { }
			Console.Clear();
			Menu.ShowMenu();
			return;
		}
		static public void PlayGame()
		{
			Console.CursorVisible = false;
			SetDesign(5, 4);
			PrintMap();
			SetCharacter();
			while (score <= scoreToWin)
			{
				if (IsStuck())
				{
					if (velocity > 50)
					{
						velocity = (int)((double)velocity / 1.01);
					}
					IfStuck();
					Console.SetCursorPosition(previousLeftPosition_X, previousPosition_Y);
					for (int i = 0; i <= listOfLengthsOfHorizontalCharacters[tempIndex]; ++i)
					{
						Console.Write(pointFiller);
					}
					CheckDone();
					tempIndex = randValue.Next(0, listOfLengthsOfHorizontalCharacters.Length - 1);
					SetCharacter();		
				}
				else
				{
					PrintCharacter();
					Input();
					Thread.Sleep(velocity);
					Fall();
					if (IsGameOver()) break;
					Clear();
				}
			}
			if (score >= scoreToWin)
			{
				YouWon();
			}
			else
			{
				YouLose();
			}
		}
	}	
}