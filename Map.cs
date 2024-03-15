using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleGame_ATB
{

    // 케이스 별로 렌더할게 달라져야 하는데,
    internal class Map
    {
        TextBoxInterface textBoxInterface = new TextBoxInterface();
        const char CIRCLE = '\u25cf';
        const char SQURE_B = '\u25fb';
        const char SQURE = '\u25fc';

        
        public int SizeX { get; private set; }
        public int SizeY { get; private set; }
        public int SizeMenu { get; private set; }

        public void Initialize(int Xsize, int Ysize, int mSize)
        {
            SizeX = Xsize;
            SizeY = Ysize;
            SizeMenu = mSize;
        }
        
        public void Render(List<Pos> pPoint)
        {
            // 맵 출력, 메뉴 출력 후

            for (int y = 0; y < SizeY; y++)
            {
                // 맵 구현
                /*for (int x = 0; x < SizeX; x++)
                {
                    Console.ForegroundColor = ConsoleColor.DarkGray;

                    foreach (Pos pos in pPoint)
                        if (pos.Y == y && pos.X == x)
                            Console.ForegroundColor = ConsoleColor.Green;

                    Console.Write(CIRCLE);
                }*/
                // 메뉴 들어가야할 자리.

                // 메뉴 구현

                for (int x = 0; x < SizeMenu; x++)
                {
                    //Console.ForegroundColor = ConsoleColor.DarkBlue;
                    Console.Write("★");
                }
                Console.WriteLine();
            }
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine();
            textBoxInterface.PrintTextBox(); //값을 넣어서 해결 하자.
        }
    }
}
