using System;
using System.Collections.Generic;

namespace ConsoleGame_ATB
{
    public class Pos
    {
        public int X, Y;

        public Pos(int y, int x) { Y = y; X = x; }
    }
    static class Define
    {
        public const int Map_SizeX = 45;
        public const int Map_SizeY = 35;
        public const int Menu_SizeX = 15;
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
