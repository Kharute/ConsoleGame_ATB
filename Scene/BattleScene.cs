using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace ConsoleGame_ATB.Scene
{
    // 델리게이트 체인을 활용하여 만들 것.

    internal class BattleScene
    {
        public List<List<string>> battleText = new List<List<string>>();
        List<string> images;
        Partner[] Partys { get; set; }
        Player Player { get; set; }

        Enemy Enemy = new Enemy();

        int curMenu = 0;
        int curCussor = 0;
        bool isEscBtn = false;
        public void Update(Partner[] party, Player player)
        {
            Player = player;
            Partys = party;
            stringAdd();
        }

        public bool BattleLoad()
        {
            Console.Clear();

            // 배틀 로드 파트
            Enemy enemy = new Enemy();

            // 둘이 싸워서 이기면 탈출
            TextBoxInterface _textBox = new TextBoxInterface();
            _textBox.init();

            images = new List<string>();
            images.Add("                                                                 ");
            images.Add("                                                                 ");
            images.Add("                                                                 ");
            images.Add("                                                                 ");
            images.Add("                                                                 ");
            images.Add("               ＆                                □□□□        ");
            images.Add("         ＆                                      □□□□        ");
            images.Add("                                                 □□□□        ");
            images.Add("                                                 □□□□        ");
            images.Add("              ＆                                                 ");
            images.Add("       ＆                                                        ");
            images.Add("                                                                 ");
            images.Add("                                                                 ");
            images.Add("                                                                 ");
            images.Add("                                                                 ");
            images.Add("                                                                 ");
            images.Add("                                                                 ");
            images.Add("                                                                 ");
            

            while (!isEscBtn)
            {
                Render(images, _textBox);

                _textBox.PrintTextBox(battleText[curMenu], curCussor);
                PressCheck(_textBox);   
            }
            //죽었는지 물어보고 맞으면 사망처리
            if(Player.curHP < 0)
                return true;
            else
                return false;
        }

        public void Render(List<string> image, TextBoxInterface _textBox)
        {
            Console.Clear();
            Console.SetCursorPosition(0, 0);

            for (int y = 0; y < image.Count; y++)
            {
                Console.WriteLine(image[y]);
            }

            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine();
        }

        private int PressCheck(TextBoxInterface textBox)
        {
            ConsoleKeyInfo consoleKey = Console.ReadKey();

            switch (consoleKey.Key)
            {
                case ConsoleKey.UpArrow:
                    if (curCussor < 1)
                        curCussor = battleText.Count();
                    else
                        curCussor--;
                    textBox.PrintTextBox(battleText[curMenu], curCussor);
                    break;
                case ConsoleKey.DownArrow:
                    if (curCussor >= battleText.Count())
                        curCussor = 0;
                    else
                        curCussor++;
                    textBox.PrintTextBox(battleText[curMenu], curCussor);
                    //curCussor++;
                    break;
                case ConsoleKey.Enter:
                    if (curMenu == 0)
                    {
                        switch (curCussor)
                        {
                            case 0:
                                curMenu = 1;
                                textBox.PrintTextBox(battleText[curMenu], curCussor);
                                break;
                            case 1:
                                isEscBtn = true;
                                textBox.PrintTextBox(battleText[curMenu], curCussor);
                                break;
                        }
                        curCussor = 0;
                    }
                    else if (curMenu == 1)
                    {
                        // 전투에 진입
                        switch (curCussor)
                        {
                            case 0:
                                Random rand = new Random();

                                int playerDmg = Player.MainStat.ATK + Player.EquipItem[0].ItemStat.ATK + rand.Next() % 5;
                                int enemyDmg = Enemy.MainStat.ATK - rand.Next() % 5 + 2;

                                Enemy.HP -= playerDmg;
                                //textbox 이외에 출력박스 따로 만들어야할 듯.
                                Render(images, textBox);
                                textBox.PrintBattleTextBox(1, playerDmg, 0);// 공격
                                Thread.Sleep(1500);
                                // 사망체크
                                if (Enemy.HP < 0)
                                {
                                    isEscBtn = true;
                                    int getGold = Enemy.Gold;
                                    int getExp  = Enemy.Exp;
                                    Player.AddGold(getGold);
                                    Player.AddEXP(getExp);
                                    
                                    Render(images, textBox);
                                    textBox.PrintBattleTextBox(3, getGold, getExp);  //승리했다
                                    Thread.Sleep(1000);
                                }
                                // 상대턴
                                else
                                {
                                    Player.curHP -= enemyDmg;
                                    Render(images, textBox);
                                    textBox.PrintBattleTextBox(2, enemyDmg, 0);  //공격받았다.
                                    Thread.Sleep(1000);
                                    if (Player.curHP < 0)
                                    {
                                        //사망체크
                                        isEscBtn = true;
                                        Render(images, textBox);
                                        textBox.PrintBattleTextBox(4, 0, 0); //사망했다.
                                        curMenu = 0;
                                        curCussor = 0;
                                        Thread.Sleep(1000);
                                    }
                                }
                                curMenu = 0;
                                curCussor = 0;
                                textBox.PrintTextBox(battleText[curMenu], curCussor);
                                break;
                            case 1:
                                battleText[2].Remove(Player.Inventory[curCussor].Name);
                                Player.Inventory.Remove(Player.Inventory[curCussor]);
                                // 아이템
                                curMenu = 2;
                                textBox.PrintTextBox(battleText[curMenu], curCussor);
                                break;
                            case 2:
                                // 이전
                                curMenu = 0;
                                textBox.PrintTextBox(battleText[curMenu], curCussor);
                                break;
                        }
                    }

                    //curMenu를 바꿀 것.
                    break;
                case ConsoleKey.Escape:
                    isEscBtn = true;
                    break;
            }
            // 엔터면 커서 위치 초기화
            return 0;
        }

        public void stringAdd()
        {
            #region sBattleStart
            List<string> sBattleStart =
            [
                $"      전투 시작 ",
                $"        싸운다",
                $"        도망친다."
            ];

            battleText.Add(sBattleStart);
            #endregion sBattleStart
            #region sBattleMenu
            List<string> sBattleMenu = 
            [
                $"      전투 ",
                $"        공격한다.",
                $"        아이템을 사용한다.",
                $"        이전 "
            ];
            battleText.Add(sBattleMenu);
            #endregion sBattleMenu
            //일단 동료는 자동행동하거나 일단 스킵
            //일단 값 다 받고 끝에서 내려갈때 리스트 더 있으면 커서는 유지

            #region sItemMenu
            List<string> sItemMenu = new List<string>();

            sBattleMenu.Add("아이템");

            for (int i = 0; i< Player.Inventory.Count;i++)
            {
                sItemMenu.Add(Player.Inventory[i].Name);
            }
            battleText.Add(sItemMenu);
            #endregion sItemMenu
        }
    }
}
