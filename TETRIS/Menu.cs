using System;
using System.Collections.Generic;
using System.Text;

namespace TETRIS
{
	static class Menu
	{
		static private string[] buttons_0 = { "Play", "Settings", "Exit" };
		static private string[] buttons_1 = { "Size of the field: Small Medium Large ", "Mode: Easy Medium Hardcore", "Back" };
		static private char pointerSymbol = '>';
		static private (int, int) firstButtonPosition = (3, 2);
		static private int pointerPosition = 0;
		static private int index_0 = 0;
		static private int index_1 = 0;
		static private List<(int, int)> sizes = new List<(int, int)>() { (25, 15), (30, 18), (36, 22) };
		static private List<(int, int)> modes = new List<(int, int)>() { (200, 100), (175, 150), (150, 200) };
		static public void ShowMenu()
		{
			Console.ForegroundColor = ConsoleColor.White;
			Console.CursorVisible = false;
			Console.SetCursorPosition(0, 0);
			Console.Write("Tetris written in C# by Daniel Bondarcov");
			for (int i = 0; i < buttons_0.Length; ++i)
			{
				Console.SetCursorPosition(firstButtonPosition.Item1, firstButtonPosition.Item2 + i);
				Console.Write(buttons_0[i]);
			}
			Console.SetCursorPosition(firstButtonPosition.Item1 - 2, firstButtonPosition.Item2);
			Choosing();
			return;
		}
		static private void DeletePointer()
		{
			Console.SetCursorPosition(firstButtonPosition.Item1 - 1, firstButtonPosition.Item2 + pointerPosition);
			Console.Write("\b ");
			return;
		}
		static private void Highlight()
		{
			Console.SetCursorPosition(firstButtonPosition.Item1 - 2, firstButtonPosition.Item2 + pointerPosition);
			Console.Write(pointerSymbol);
			return;
		}
		static private void Choosing()
		{
			ConsoleKey i = new ConsoleKey();
			do
			{
				Highlight();
				i = Console.ReadKey(true).Key;
				DeletePointer();
				switch (i)
				{
					case ConsoleKey.DownArrow:
						if (pointerPosition < buttons_0.Length - 1)
						{
							++pointerPosition;
						}
						break;
					case ConsoleKey.UpArrow:
						if (pointerPosition > 0)
						{
							--pointerPosition;
						}
						break;
				}
			} while (i != ConsoleKey.Enter);
			switch (pointerPosition)
			{
				case 0: Play(); break;
				case 1: Parameters(); break;
				case 2: Exit(); break;
			}
			return;
		}
		static private void Play()
		{
			Console.Clear();
			TetrisGame.SetTetrisGame(sizes[index_0].Item1, sizes[index_0].Item2, modes[index_1]);
			TetrisGame.PlayGame();
			return;
		}
		static private void Exit()
		{
			Environment.Exit(0);
			return;
		}
		static private void Parameters()
		{
			Console.Clear();
			ParametersMenu.ShowParametersMenu();
			return;
		}
		static class ParametersMenu
		{
			static public void ShowParametersMenu()
			{
				Console.SetCursorPosition(0, 0);
				Console.Write("Tetris written in C# by Daniel Bondarcov");
				pointerPosition = 0;
				Console.CursorVisible = false;
				Console.BackgroundColor = ConsoleColor.Black;
				for (int i = 0; i < buttons_1.Length; ++i)
				{
					Console.SetCursorPosition(firstButtonPosition.Item1, firstButtonPosition.Item2 + i);
					Console.Write(buttons_1[i]);
				}
				Console.SetCursorPosition(firstButtonPosition.Item1 - 2, firstButtonPosition.Item2);
				Choosing();
				return;
			}
			static private void Choosing()
			{
				ConsoleKey i = new ConsoleKey();
				do
				{
					Highlight();
					i = Console.ReadKey(true).Key;
					DeletePointer();
					switch (i)
					{
						case ConsoleKey.DownArrow:
							if (pointerPosition < buttons_0.Length - 1)
							{
								++pointerPosition;
							}
							break;
						case ConsoleKey.UpArrow:
							if (pointerPosition > 0)
							{
								--pointerPosition;
							}
							break;
					}
				} while (i != ConsoleKey.Enter);
				switch (pointerPosition)
				{
					case 0: SizeOfField(); break;
					case 1: GameMode(); break;
					case 2: Back(); break;
				}
				return;
			}
			static private void Back()
			{
				Console.Clear();
				pointerPosition = 0;
				ShowMenu();
				return;
			}
			static private void Highlight_0()
			{
				switch (index_0)
				{
					case 0:
						Console.BackgroundColor = ConsoleColor.Green;
						Console.SetCursorPosition(firstButtonPosition.Item1 + 19, firstButtonPosition.Item2);
						Console.Write("Small");
						break;
					case 1:
						Console.BackgroundColor = ConsoleColor.Yellow;
						Console.SetCursorPosition(firstButtonPosition.Item1 + 25, firstButtonPosition.Item2);
						Console.Write("Medium");
						break;
					case 2:
						Console.BackgroundColor = ConsoleColor.Red;
						Console.SetCursorPosition(firstButtonPosition.Item1 + 32, firstButtonPosition.Item2);
						Console.Write("Large");
						break;
				}
				return;
			}
			static private void DeleteHighlighting()
			{
				Console.BackgroundColor = ConsoleColor.Black;
				switch (index_0)
				{
					case 0:
						Console.SetCursorPosition(firstButtonPosition.Item1 + 19, firstButtonPosition.Item2);
						Console.Write("Small");
						break;
					case 1:
						Console.SetCursorPosition(firstButtonPosition.Item1 + 25, firstButtonPosition.Item2);
						Console.Write("Medium");
						break;
					case 2:
						Console.SetCursorPosition(firstButtonPosition.Item1 + 32, firstButtonPosition.Item2);
						Console.Write("Large");
						break;
				}
				return;
			}
			static private void SizeOfField()
			{
				DeletePointer();
				ConsoleKey i = new ConsoleKey();
				do
				{
					Highlight_0();
					i = Console.ReadKey(true).Key;
					DeleteHighlighting();
					switch (i)
					{
						case ConsoleKey.RightArrow:
							if (index_0 < 2)
							{
								++index_0;
							}
							break;
						case ConsoleKey.LeftArrow:
							if (index_0 > 0)
							{
								--index_0;
							}
							break;
					}

				} while (i != ConsoleKey.Enter);
				Choosing();

				return;
			}
			static private void Highlight_1()
			{
				switch (index_1)
				{
					case 0:
						Console.BackgroundColor = ConsoleColor.Green;
						Console.SetCursorPosition(firstButtonPosition.Item1 + 6, firstButtonPosition.Item2 + 1);
						Console.Write("Easy");
						break;
					case 1:
						Console.BackgroundColor = ConsoleColor.Yellow;
						Console.SetCursorPosition(firstButtonPosition.Item1 + 11, firstButtonPosition.Item2 + 1);
						Console.Write("Medium");
						break;
					case 2:
						Console.BackgroundColor = ConsoleColor.Red;
						Console.SetCursorPosition(firstButtonPosition.Item1 + 18, firstButtonPosition.Item2 + 1);
						Console.Write("Hardcore");
						break;
				}
				return;
			}
			static private void DeleteHighlighting_1()
			{
				Console.BackgroundColor = ConsoleColor.Black;
				switch (index_1)
				{
					case 0:
						Console.SetCursorPosition(firstButtonPosition.Item1 + 6, firstButtonPosition.Item2 + 1);
						Console.Write("Easy");
						break;
					case 1:
						Console.SetCursorPosition(firstButtonPosition.Item1 + 11, firstButtonPosition.Item2 + 1);
						Console.Write("Medium");
						break;
					case 2:
						Console.SetCursorPosition(firstButtonPosition.Item1 + 18, firstButtonPosition.Item2 + 1);
						Console.Write("Hardcore");
						break;
				}
				return;
			}
			static private void GameMode()
			{
				DeletePointer();
				ConsoleKey i = new ConsoleKey();
				do
				{
					Highlight_1();
					i = Console.ReadKey(true).Key;
					DeleteHighlighting_1();
					switch (i)
					{
						case ConsoleKey.RightArrow:
							if (index_1 < 2)
							{
								++index_1;
							}
							break;
						case ConsoleKey.LeftArrow:
							if (index_1 > 0)
							{
								--index_1;
							}
							break;
					}

				} while (i != ConsoleKey.Enter);
				Choosing();
				return;
			}
		}
	}
}
