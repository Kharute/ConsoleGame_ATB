using System;
using System.Collections.Generic;

namespace ConsoleGame_ATB
{
    public class Pos
    {
        public int X, Y;

        public Pos(int x, int y) { X = x; Y = y; }
    }
    static class Define
    {
        public const int SizeX_Map = 45;
        public const int SizeY_Map = 35;
        public const int SizeX_Menu = 15;
        public const int SizeY_TextBox = 7;
    }

    internal class GameMain
    {
        static void Main(string[] args)
        {
            GameManager gameManager = new GameManager();
            Console.CursorVisible = false;

            Console.SetWindowSize(120, 44);
            Console.SetBufferSize(120, 44);
            gameManager.StartGame();
        }
    }
}
