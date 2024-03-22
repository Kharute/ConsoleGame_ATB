using Microsoft.VisualBasic;
using System;
using System.Runtime;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ConsoleGame_ATB
{
    // PlayerInfo는 레벨, 경험치, 스탯(+장비 스탯)을 보여준다.
    // inventory는 소지금, 장비, 소비창
    // partner는 세명의 이미지, 레벨, 경험치, 스탯 을 보여준다.
    // Equipment는 장착장비, 추가효과를 보여준다.
    // Setting은 세이브, 로드, 종료를 보여준다.

    // Store는 상점을 보여준다.

    //      - 세이브 기능은 뭐 텍스트에서 불러와도 되고... 흠.
    enum MainMenu
    {
        BaseMenu, PartMenu, PInfoMenu, EquipMenu, InvenMenu, SaveMenu
    };
    enum StoreMenu
    {
        BaseMenu, BuyMenu, SellMenu, Escape
    };

    internal class Menu
    {
        public List<List<string>> menuText = new List<List<string>>();

        public string[] menuStr = new string[Define.SizeY_Map];
        Partner[] Partys { get; set; }
        Player Player { get; set; }

        int curMenu = 0;
        int curCussor = 0;

        

        public int[] cursorLength = { 5, 5, 0, 5, 11, 4 };
        private int xSize, ySize;

        //메인메뉴 상태
        public MainMenu mainMenu;

        public MainMenu MAIN_MENU { get { return mainMenu; } set { mainMenu = MAIN_MENU; } }

        //플레이어, 파트너 쟁여온거 끌고와서 업데이트
        public void DicUpdate(Partner[] party, Player player)
        {
            Player = player;
            Partys = party;

            stringAdd();
            /*line_dic._lineDic_Pinfo*/
        }
        public void Init(int mXSize, int mYsize)
        {
            xSize = mXSize; //x = 15;
            ySize = mYsize; //y = 35;
            curMenu = 0;
            curCussor = 0;
        }

        //커서가 이동할 때마다 커서 위치만 잡아주는 함수.
        public void Cussor(List<string> strings, int cussor)
        {
            for (int i = 2; i < strings.Count + 2; i++)
            {
                if (i < menuStr.Length)
                {
                    if (i == cussor + 2)
                    {
                        menuStr[i] = "    ▷";
                    }
                    else
                    {
                        menuStr[i] = "      ";
                    }
                }
            }
        }
        //enter를 눌렀을 때, 다음 메뉴에 있는 내용을 진행하는 함수.


        /*  위치 상세서
        BaseMenu 0 > PartMenu(파티 상태창) 1 > PinfoMenu(해당 캐릭터 인포) 2
        BaseMenu 0 > PartMenu(파티 장비창) 1> EquipMenu(해당 캐릭터 장비) 3 -> 장비해제여부는 text로

        BaseMenu 0 > InvenMenu(인벤토리) 4 -> 장착여부는 text로
        BaseMenu 0 > SaveMenu(세이브/로드) 5 -> 세이브 여부는 text로
         */
        // 메인일 땐 메뉴, 상점일 땐 상점, 상태창일 땐 상태창


        private void PrintSelect(MainMenu menu, string[] str,int cur)
        {
            //리스트에 행 위치랑 단어 적어주고 나머지는 패딩만 해줌
            //string으로 키 잡아주고, 적을 내용 string + 해당 열 int를 class 로 만들어 Dic에 넣는다?
            //상황 보고 DIc에 add 시킬 것
            switch (menu)
            {
                /*case MainMenu.BaseMenu:
                    for (int i = 2; i < Define.SizeY_Map-1; i++)
                    {
                        menuStr[i] = "";
                        if (i != 0 && i < str.Length+2)
                        {
                            menuStr[i] += str[i-2];
                        }
                        
                        if(i < menuText[0].Count+2)
                            menuStr[i] += menuText[0][i-2];
                        else
                            menuStr[i] = "";
                    }
                    break;
                case MainMenu.PartMenu:
                    for (int i = 2; i < Define.SizeY_Map - 1; i++)
                    {
                        menuStr[i] = "";
                        if (i != 0 && i < str.Length + 2)
                        {
                            menuStr[i] += str[i - 2];
                        }

                        if (i < menuText[1].Count + 2)
                            menuStr[i] += menuText[1][i - 2];
                        else
                            menuStr[i] = "";
                    }

                    break;
                case MainMenu.PInfoMenu:
                    for (int i = 2; i < Define.SizeY_Map - 1; i++)
                    {
                        menuStr[i] = "";
                        if (i != 0 && i < str.Length + 2)
                        {
                            menuStr[i] += str[i - 2];
                        }

                        if (i < (menuText[2].Count)/4 + 2)
                            menuStr[i] += menuText[2][(cur -1)*8 + i - 2];
                        else
                            menuStr[i] = "";
                    }

                    break;
                case MainMenu.EquipMenu:
                    for (int i = 2; i < Define.SizeY_Map - 1; i++)
                    {
                        menuStr[i] = "";
                        if (i != 0 && i < str.Length + 2)
                        {
                            menuStr[i] += str[i - 2];
                        }

                        if (i < menuText[3].Count + 2)
                            menuStr[i] += menuText[3][i - 2];
                        else
                            menuStr[i] = "";
                    }
                    break;
                case MainMenu.InvenMenu:
                    for (int i = 2; i < Define.SizeY_Map - 1; i++)
                    {
                        menuStr[i] = "";
                        if (i != 0 && i < str.Length + 2)
                        {
                            menuStr[i] += str[i - 2];
                        }

                        if (i < menuText[4].Count + 2)
                            menuStr[i] += menuText[4][i - 2];
                        else
                            menuStr[i] = "";

                    }
                    break;
                case MainMenu.SaveMenu:
                    for (int i = 2; i < Define.SizeY_Map - 1; i++)
                    {
                        menuStr[i] = "";
                        if (i != 0 && i < str.Length + 2)
                        {
                            menuStr[i] += str[i - 2];
                        }

                        if (i < menuText[5].Count + 2)
                            menuStr[i] += menuText[5][i - 2];
                        else
                            menuStr[i] = "";
                    }
                    break;*/
            }
            //0이면 가운데 정렬
            // 나머진 왼쪽 정렬
            //필요 없을 수 있음.*/
        }

        public void ClearMenu()
        {
            menuStr[0] = "■■■■■■■■■■■■■■■";
            for (int i = 0; i < menuStr.Length; i++)
            {
                if (i != 0 && i != menuStr.Length - 1)
                {
                    menuStr[i] = "                             ";
                }
                else
                {
                    menuStr[i] = "■■■■■■■■■■■■■■■";
                }
            }
        }

        //y는 줄 단위로 반환 할 예정

        // 메뉴를 계속 프린트.
        public string[] PrintMenu()
        {
            ClearMenu();
            //Cussor();

            return menuStr.ToArray();
        }
        //메뉴를 눌렀을 경우에, 앞으로 해당 메뉴를 표시.

        public bool PressCheck(Menu menu)
        {
            ConsoleKeyInfo consoleKey = Console.ReadKey();

            switch (consoleKey.Key)
            {
                // _menu.cursorLength[(int)_menu.MAIN_MENU]-1 이게 최대길이
                // 메뉴 움직이기
                case ConsoleKey.UpArrow:
                    if (menu.curCussor < 1)
                        menu.curCussor = menuText[curMenu].Count - 2;
                    else
                        menu.curCussor--;
                    break;
                case ConsoleKey.DownArrow:
                    if (menu.curCussor >= menuText[curMenu].Count - 2)
                        menu.curCussor = 0;
                    else
                        menu.curCussor++;

                    break;
                case ConsoleKey.Enter:
                    ClearMenu();

                    /*if (mainMenu == MainMenu.BaseMenu)
                    {
                        if (curCussor == 1 || curCussor == 2)
                        // 커서 위치보고 Main 결정해줘야 함.
                        {
                            _menu.mainMenu = MainMenu.PartMenu;        //해당부분 접근자 수정 요함.
                        }
                        else if (_menu.curCussor == 3)
                        {
                            _menu.mainMenu = MainMenu.InvenMenu;
                        }
                        else
                        {
                            _menu.mainMenu = MainMenu.SaveMenu;
                        }

                        _menu.CurssorMove(_menu.MAIN_MENU, _menu.curCussor, true);
                    }
                    else if (_menu.mainMenu == MainMenu.PartMenu)
                    {
                        _menu.mainMenu = MainMenu.PInfoMenu;

                        if (_menu.curCussor == 1)
                            _menu.CurssorMove(_menu.MAIN_MENU, _menu.curCussor, true);
                        else
                            _menu.CurssorMove(_menu.MAIN_MENU, _menu.curCussor, true);
                    }*/
                    // 해당 안에 시드로 들어간다.
                    // 내정보, 동료정보 는 그냥 Esc만 먹히게
                    break;
                case ConsoleKey.Escape:

                    /*if (_menu.MAIN_MENU != MainMenu.BaseMenu)
                    {
                        _menu.mainMenu = MainMenu.BaseMenu;
                        _menu.CurssorMove(_menu.MAIN_MENU, 1, false);
                        break;
                    }
                    else*/
                        return true;
                case ConsoleKey.Tab:
                    return true;

            }

            return false;
        }

        public void stringAdd()
        {
            menuText = new List<List<string>>();

            List<string> sBaseMenu =
            [
                $"         메  뉴   ",
                $"   파티 상태창   ",
                $"   파티 장비창   ",
                $"   인벤토리     ",
                $"   세이브 / 로드  ",
            ];

            menuText.Add(sBaseMenu);

            List<string> sPartMenu = [$"   동  료"];

            for (int i = 0; i < 4; i++)
            {
                sPartMenu.Add($" - {Partys[i].Name}");
            }

            menuText.Add(sPartMenu);

            List<string> sPInfoMenu = new List<string>();
            for (int i = 0; i < 4; i++) //그냥 길이만큼 곱해서 쓸 것 *8
            {
                sPInfoMenu.Add($"   정보");
                sPInfoMenu.Add($"이름 : {Partys[i].Name}");
                sPInfoMenu.Add($"레벨 : {Partys[i].Level} : Req({Partys[i].ReqExp[Partys[i].Level - 1] - Partys[i].Exp})");
                sPInfoMenu.Add($"스탯");
                sPInfoMenu.Add($" - H P : {Partys[i].HP}/{Partys[i].MainStat.MAX_HP}");
                sPInfoMenu.Add($" - ATK : {Partys[i].MainStat.ATK}");
                sPInfoMenu.Add($" - DEF : {Partys[i].MainStat.DEF}");
                sPInfoMenu.Add($" - SPD : {Partys[i].MainStat.SPD}");
            }
            menuText.Add(sPInfoMenu);

            List<string> sEquipMenu = new List<string>();

            for (int i = 0; i < 4; i++) //*5
            {
                sEquipMenu.Add($"       장   비 ");
                sEquipMenu.Add($"이름 : {Partys[i].Name}");
                sEquipMenu.Add($"무기 : {Partys[i].EquipItem[0].Name}");
                sEquipMenu.Add($"머리 : {Partys[i].EquipItem[1].Name}");
                sEquipMenu.Add($" 몸  : {Partys[i].EquipItem[2].Name}");
                sEquipMenu.Add($"신발 : {Partys[i].EquipItem[3].Name}");
            }
            menuText.Add(sEquipMenu);

            List<string> sInvenMenu = new List<string>();
            sInvenMenu.Add($"골드 : {Player.gold}");
            sInvenMenu.Add($"아이템 ");
            for (int i = 0; i < Player.Inventory.Count; i++)
            {
                sInvenMenu.Add($"  - {Player.Inventory[i].Name}");
            }
            menuText.Add(sInvenMenu);

            List<string> sSaveMenu = new List<string>();
            sSaveMenu.Add($"       세이브 ");
            sSaveMenu.Add($"       저장한다 ");
            sSaveMenu.Add($"       로드한다 ");
            sSaveMenu.Add($"       종료한다 ");
            menuText.Add(sSaveMenu);
        }
    }
}
