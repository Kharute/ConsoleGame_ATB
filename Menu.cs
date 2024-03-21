using Microsoft.VisualBasic;
using System;
using System.Runtime;

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
/*
    class Line_Dic
    {
        //차라리 처음부터 player 인수 받아서 처리할까?
        //플레이어랑 동료 정보를 배열로 받아야겠는데?
        //이러면 basic에 만들 때 부터, 해당 배열이 있어야 함.
        public Dictionary<int, string> _lineDic_Base;
        public Dictionary<int, string> _lineDic_Part;
        public Dictionary<int, string> _lineDic_Pinfo;
        public Dictionary<int, string> _lineDic_Equip;
        public Dictionary<int, string> _lineDic_Inven;
        public Dictionary<int, string> _lineDic_Save;

        //메소드를 생성해서 string 을 반환
        public Line_Dic()
        {
            _lineDic_Base = new Dictionary<int, string>
            {
                { 0 , "메  뉴" },
                { 1 , "파티 상태창" },
                { 2 , "파티 장비창" },
                { 3 , "인벤토리" },
                { 4 , "세이브 / 로드" },
            };
            _lineDic_Part = new Dictionary<int, string>
            {
                { 0 , "동  료" }
            };
            _lineDic_Pinfo = new Dictionary<int, string>
            {
                {0, "정  보" },
                {1 , "이름 :" },
                {2 , "레벨 :" },
                {3 , "스탯" },
                {4 , "  - HP" },
                {5, "  - ATK" },
                {6, "  - DEF" },
                {7, "  - SPD" }
            };
            _lineDic_Equip = new Dictionary<int, string>
            {
                {0 , "장비창" },
                {1 , "이름" },
                {2 , "무기 :" },
                {3 , "머리 :" },
                {4 , "몸   :" },
                {5 , "신발 :"}
            };
            _lineDic_Inven = new Dictionary<int, string>
            {
                {0 , "인벤토리"},
                {1 , "골드 :" },
                {2 , "아이템 :"}
            };
            _lineDic_Save = new Dictionary<int, string>
            {
                {0, "세이브" },
                {1, "저장한다"},
                {2, "로드한다"},
                {3, "종료한다"},
            };
            // 동료로 지정한 캐릭의 정보가 나와야 됨.

            //체크하면 벗는지 물어보고 벗는다. 인벤토리로 간다. EquipMenu

            //플레이어만의 정보만 출력
            //3번부턴 13번까지 player가 소지한 거 출력해줘야 됨.

            //해당 캐릭터의 내용을 처리해서 보여준다.SaveMenu

        }

    }
*/
    
    // BaseMenu = 서브 들어가기 전의 메뉴

    // 재귀 함수 써서 안에 들어가도록
    // esc 누르면 탈출 enter 누르면 진입

    //
    internal class Menu
    {
        public List<List<string>> strings = new List<List<string>>();

        Partner[] Partys { get; set; }
        Player Player { get; set; }

        public void stringAdd()
        {
            List<string> sBaseMenu =
            [
                $"         메  뉴   ",
                $"   파티 상태창   ",
                $"   파티 장비창   ", 
                $"   인벤토리     ",
                $"   세이브 / 로드  ",
            ];

            strings.Add(sBaseMenu);

            List<string> sPartMenu = [$"   동  료"];

            for (int i = 0; i < 4; i++)
            {
                sPartMenu.Add($" - {Partys[i].Name}");
            }

            strings.Add(sPartMenu);

            List<string> sPInfoMenu = new List<string>();
            for (int i = 0; i < 4; i++) //그냥 길이만큼 곱해서 쓸 것 *8
            {
                sPInfoMenu.Add($"   정보");
                sPInfoMenu.Add($"이름 : {Partys[i].Name}");
                sPInfoMenu.Add($"레벨 : {Partys[i].Level} : Req({Partys[i].ReqExp[Partys[i].Level - 1] - Partys[i].Exp})");
                sPInfoMenu.Add($"스탯");
                sPInfoMenu.Add($" - H P : {Partys[i].curHP}/{Partys[i].MainStat.MAX_HP}");
                sPInfoMenu.Add($" - ATK : {Partys[i].MainStat.ATK}");
                sPInfoMenu.Add($" - DEF : {Partys[i].MainStat.DEF}");
                sPInfoMenu.Add($" - SPD : {Partys[i].MainStat.SPD}");
            }
            strings.Add(sPInfoMenu);

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
            strings.Add(sEquipMenu);

            List<string> sInvenMenu = new List<string>();
            sInvenMenu.Add($"골드 : {Player.gold}");
            sInvenMenu.Add($"아이템 : {Player.gold}");
            for (int i = 0; i < Player.Inventory.Count; i++)
            {
                sInvenMenu.Add($"  - {Player.Inventory[i].Name}");
            }
            strings.Add(sInvenMenu);

            List<string> sSaveMenu = new List<string>();
            sSaveMenu.Add($"       세이브 ");
            sSaveMenu.Add($"       저장한다 ");
            sSaveMenu.Add($"       로드한다 ");
            sSaveMenu.Add($"       종료한다 ");
            strings.Add(sSaveMenu);
        }

        public int[] cursorLength = { 5, 5, 0, 5, 11, 4 };
        private int xSize, ySize;

        public string[] menuStr = new string[35];
        //메인메뉴 상태
        public MainMenu mainMenu;
        public int curCussor = 1;

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
        }

        //커서가 이동할 때마다 커서 위치만 잡아주는 함수.
        public void CurssorMove(MainMenu menu, int cur, bool enter)
        {
            if(!enter)
                curCussor = cur;
            else
                curCussor = 1;
            int clength = (int)menu;// menu의 크기

            string[] str = new string[cursorLength[clength]];
            for (int i = 1; i < str.Length; i++)
            {
                if (i == curCussor)
                    str[i] = "▷";
                else
                    str[i] = "  ";
            }
            PrintSelect(mainMenu, str, cur);
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
                case MainMenu.BaseMenu:
                    for (int i = 2; i < Define.SizeY_Map-1; i++)
                    {
                        menuStr[i] = "";
                        if (i != 0 && i < str.Length+2)
                        {
                            menuStr[i] += str[i-2];
                        }
                        
                        if(i < strings[0].Count+2)
                            menuStr[i] += strings[0][i-2];
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

                        if (i < strings[1].Count + 2)
                            menuStr[i] += strings[1][i - 2];
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

                        if (i < (strings[2].Count)/4 + 2)
                            menuStr[i] += strings[2][(cur -1)*8 + i - 2];
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

                        if (i < strings[3].Count + 2)
                            menuStr[i] += strings[3][i - 2];
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

                        if (i < strings[4].Count + 2)
                            menuStr[i] += strings[4][i - 2];
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

                        if (i < strings[5].Count + 2)
                            menuStr[i] += strings[5][i - 2];
                        else
                            menuStr[i] = "";
                    }
                    break;
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
        public void PrintMenu(int y)
        {
            if (y < Define.SizeY_Map)
                Console.Write(menuStr[y]);
        }
        //메뉴를 눌렀을 경우에, 앞으로 해당 메뉴를 표시.
    }
}
