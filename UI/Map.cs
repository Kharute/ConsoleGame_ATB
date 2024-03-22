using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleGame_ATB
{
    internal class Map
    {
        List<List<Pos>> mapPos = new List<List<Pos>>();
        int SizeX { get; set; }
        int SizeY { get; set; }
        public int SizeMenu { get; private set; }

        public void Init(int Xsize, int Ysize, int mSize)
        {
            SizeX = Xsize;
            SizeY = Ysize;
            SizeMenu = mSize;
        }

        public void Update(List<List<Pos>> pos)
        {
            mapPos = pos;
            /*line_dic._lineDic_Pinfo*/
        }
        public bool RigidCheck(Player player, Player.Move mv)
        {
            switch (mv)
            {
                case Player.Move.Left:
                    return PisicChk(player.Positions[0].X - 1, player.Positions[0].Y);
                case Player.Move.Right:
                    return PisicChk(player.Positions[0].X + 1, player.Positions[0].Y);
                case Player.Move.Up:
                    return PisicChk(player.Positions[0].X, player.Positions[0].Y - 1);
                case Player.Move.Down:
                    return PisicChk(player.Positions[0].X, player.Positions[0].Y + 1);
                default:
                    return false;
            }
        }
        private bool PisicChk(int posX , int posY)
        {
            bool posXcheck = posX <= 0 || posX >= SizeX-1;
            bool posYcheck = posY <= 0 || posY >= SizeY-1;

            if (posXcheck || posYcheck)
                return false;
            else
            {
                for(int i = 1; i < mapPos.Count; i++) //0은 플레이어라 제외
                {
                    bool NPC_check = mapPos[i][0].X == posX && mapPos[i][0].Y == posY;
                    if (NPC_check)
                        return false;
                }
            }
            return true;
        }



        //NPC도 뭉탱이로 들고 와야 함.
        public void Render(List<List<Pos>> pPoint, Menu menu, TextBox textBox)
        {
            Console.Clear();
            string[] sss = menu.PrintMenu();

            for (int y = 0; y < SizeY; y++)
            {
                for (int x = 0; x < SizeX; x++)
                {
                    //더블버퍼..?
                    //앞에 값이랑 비교해서 교체해서 사용하란 소린데...
                    char sPrint;
                    //1차 레이어
                    if (x == 0 || x == SizeX - 1 || y == 0 || y == SizeY - 1)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        sPrint = map1[y][x];
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.DarkGray;
                        sPrint = map1[y][x];
                    }

                    //2차 레이어
                    //해당위치가 같다면 렌더.
                    for (int i = 0; i < pPoint.Count; i++)
                    {
                        if (pPoint[i][0].Y == y && pPoint[i][0].X == x)
                        {
                            //적, 나, 상점 구분코드를 넣는다면 바꿀 수 있음. 지금은 임시방편
                            switch (i)
                            {
                                case 0: Console.ForegroundColor = ConsoleColor.Green;
                                    sPrint = '○'; break;
                                case 1: Console.ForegroundColor = ConsoleColor.Yellow;
                                    sPrint = '☆'; break;
                                case 2: Console.ForegroundColor = ConsoleColor.Yellow;
                                    sPrint = '☆'; break;
                                case 3: Console.ForegroundColor = ConsoleColor.Red;
                                    sPrint = '◈'; break;
                                case 4: Console.ForegroundColor = ConsoleColor.DarkBlue;
                                    sPrint = '▨'; break;
                            }
                        }
                    }
                    Console.Write(sPrint);
                }
                // 메뉴 들어가야할 자리.
                // 메뉴 구현

                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine(sss[y]);
            }

            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine();
            
            /*textBox.PrintTextBox(); //값을 넣어서 해결 하자.*/
        }



        // list를 arr로 변경하고 char 비교하는게 빠를꺼 같나?
        // 상수값 받아올 방법 생각하기

        public List<string> map1 = new List<string>
        {
            { "■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■" },
            { "■□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□■" },
            { "■□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□■" },
            { "■□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□■" },
            { "■□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□■" },
            { "■□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□■" },
            { "■□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□■" },
            { "■□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□■" },
            { "■□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□■" },
            { "■□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□■" },
            { "■□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□■" },
            { "■□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□■" },
            { "■□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□■" },
            { "■□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□■" },
            { "■□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□■" },
            { "■□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□■" },
            { "■□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□■" },
            { "■□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□■" },
            { "■□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□■" },
            { "■□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□■" },
            { "■□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□■" },
            { "■□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□■" },
            { "■□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□■" },
            { "■□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□■" },
            { "■□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□■" },
            { "■□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□■" },
            { "■□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□■" },
            { "■□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□■" },
            { "■□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□■" },
            { "■□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□■" },
            { "■□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□■" },
            { "■□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□■" },
            { "■□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□■" },
            { "■□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□■" },
            { "■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■" }
        };
    }


}
