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

        public int curMenu = 0;
        public int curCussor = 0;
        int selectParty = 0;
        
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
        public void Cussor(int count, int cussor)
        {
            for (int i = 0; i < menuText[curMenu].Count; i++)
            {
                if (i == cussor + 1)
                {
                    menuStr[i+1] = "    ▷";
                }
                else
                {
                    menuStr[i+1] = "      ";
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


        public void ClearMenu()
        {
            for (int i = 0; i < menuStr.Length; i++)
            {
                if (i != 0 && i != menuStr.Length - 1)
                {
                    menuStr[i] = "";
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
            //menuStr
            int lengthCtl = menuText[curMenu].Count;
            if (lengthCtl > 10)
                lengthCtl = 10;

            Cussor(menuText.Count, curCussor);

            for (int i = 0; i < lengthCtl; i++)
            {
                if (curCussor > 10)
                {
                    menuStr[i + 1] += menuText[curMenu][curCussor + i];
                }
                else
                {
                    menuStr[i + 1] += menuText[curMenu][i];
                }
            }

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
                    if (curMenu == 0)
                    {
                        switch (curCussor)
                        {
                            // 정보보기
                            case 0:
                                selectParty = curCussor;
                                curMenu = 1;
                                break;
                            // 장비아이템 보기
                            case 1:
                                selectParty = curCussor;
                                curMenu = 1;
                                break;
                            // 인벤토리 열기
                            case 2:
                                curMenu = 4;
                                break;
                            // 세이브
                            case 3:
                                curMenu = 5;
                                break;
                            // 뒤로가기
                            case 4:
                                return true;
                        }
                        curCussor = 0;
                    }
                    else if (curMenu == 1)
                    {
                        //selectParty를 보고 2/3으로 감. 0이면 2로 가고
                        //해당 동료의 정보/ 장비창을 보고싶은 경우
                        if(selectParty == 0)
                            curMenu = 2;
                        else
                            curMenu = 3;
                        curCussor = 0;
                    }
                    else if (curMenu == 2)
                    {
                        //해당 캐릭 정보 출력
                        //리턴시킬 것
                        curCussor = 0;
                    }
                    else if (curMenu == 3)
                    {
                        //해당 장비를 벗는 경우.
                        //리턴시킬 것
                        curMenu = 0;
                        curCussor = 0;
                    }
                    else if (curMenu == 4)
                    {
                        //해당 장비를 입는 경우.
                        //리턴시킬 것
                        curMenu = 0;
                        curCussor = 0;
                    }
                    else if (curMenu == 5)
                    {
                        //세이브하는 경우.
                        curMenu = 0;
                        curCussor = 0;
                    }
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
                    if(curMenu == 0)
                        return true;
                    else
                    {
                        curMenu = 0;
                        curCussor = 0;
                        break;
                    }

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
                $"          메  뉴",
                $"   파티 상태창   ",
                $"   파티 장비창   ",
                $"   인벤토리     ",
                $"   세이브 / 로드  ",
                $"   닫는다 "
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
