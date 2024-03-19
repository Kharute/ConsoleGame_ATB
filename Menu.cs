using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ConsoleGame_ATB.Player;

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
        BaseMenu, PInfoMenu, InvenMenu, PartMenu, EquipMenu, SaveMenu
    };
    enum StoreMenu
    {
        BaseMenu, BuyMenu, SellMenu, Escape
    };

    // BaseMenu = 서브 들어가기 전의 메뉴

    // 재귀 함수 써서 안에 들어가도록
    // esc 누르면 탈출 enter 누르면 진입

    //
    internal class Menu
    {
        private int xSize, ySize;

        public string[] menuStr = new string[35];
        private MainMenu mainMenu;
        private int curCussor = 0;

        public MainMenu MAIN_MENU { get { return mainMenu; } set { mainMenu = MAIN_MENU; } }

        /*enum MainMenu
        {
            PInfoMenu, InvenMenu, PartMenu, EquipMenu, SaveMenu
        }*/
        

        public void Initialize(int mXSize, int mYsize)
        {
            xSize = mXSize; //x = 15;
            ySize = mYsize; //y = 35;
        }
        public void CurssorMove(int cur)
        {
            curCussor = cur;

            string[] str = new string[10];
            for(int i = 0; i < str.Length; i++)
            {
                if(i== curCussor)
                {
                    str[i] = "▷";
                }
                else
                {
                    str[i] = "  ";
                }
            }

            PrintSelect(mainMenu, str);
        }
        // 메인일 땐 메뉴, 상점일 땐 상점, 상태창일 땐 상태창
        private void PrintSelect(MainMenu menu, string[] str)
        {
            switch(menu)
            {
                case MainMenu.BaseMenu:
                    menuStr[3] = $"■          메  뉴          ■";
                    menuStr[5] = $"■   {str[0]}   상태창            ■";
                    menuStr[7] = $"■   {str[1]}   인벤토리          ■";
                    menuStr[9] = $"■   {str[2]}   장비창            ■";
                    menuStr[11] = $"■   {str[3]}   동료              ■";
                    menuStr[13] = $"■   {str[4]}   세이브/로도         ■";
                    break;
                // 플레이어 정보
                // 이름, 레벨(남은 경험치), 스탯(체 공 방 이)
                case MainMenu.PInfoMenu:
                    menuStr[3]  = $"■          정  보          ■";
                    menuStr[5]  = $"■   이름 :               ■";
                    menuStr[7]  = $"■   레벨 :            ■";
                    menuStr[9]  = $"■   스탯              ■";
                    menuStr[10] = $"■     - HP             ■";
                    menuStr[10] = $"■     - ATK            ■";
                    menuStr[11] = $"■     - DEF            ■";
                    menuStr[12] = $"■     - SPD            ■";
                    break;
                // 아이템, 골드
                case MainMenu.InvenMenu:
                    menuStr[3] = $"■         인벤토리       ■";
                    menuStr[5] = $"■   골드 :                ■";
                    menuStr[7] = $"■   아이템 :               ■";
                    menuStr[8] = $"■    {str[0]}  - 아이템1            ■";
                    menuStr[9] = $"■    {str[1]}  - 아이템2            ■";
                    menuStr[10] = $"■   {str[2]}   - 아이템3            ■";
                    menuStr[11] = $"■    {str[3]}  - 아이템4            ■";
                    menuStr[12] = $"■    {str[4]}  - 아이템5            ■";
                    menuStr[13] = $"■    {str[5]}  - 아이템6            ■"; //있는 만큼만 출력해주고 나머진 비움.
                    break;
                case MainMenu.PartMenu:
                    menuStr[3] = $"■         동  료         ■";
                    menuStr[5] = $"■   동료 A               ■";//있는 만큼만 출력해주고 나머진 비움.
                    menuStr[7] = $"■   동료 B               ■";
                    menuStr[9] = $"■   동료 C               ■";
                    menuStr[11] = $"■   동료 D               ■";
                    break;
                case MainMenu.EquipMenu:
                    menuStr[3] = $"■         장비창         ■";
                    menuStr[5] = $"■   {str[0]}  무기 :               ■";
                    menuStr[7] = $"■   {str[1]}  머리 :               ■";
                    menuStr[9] = $"■   {str[2]}  몸   :               ■";
                    menuStr[11] = $"■   {str[3]}  신발 :               ■";  //체크하면 벗는지 물어보고 벗는다.
                    break;
                case MainMenu.SaveMenu:
                    break;
            }
            
        }

        public void fillMenu()
        {
            menuStr[0] = "■■■■■■■■■■■■■■■";
            for(int i = 0;i < menuStr.Length;i++)
            {
                if(i != 0 && i != menuStr.Length-1)
                {
                    menuStr[i] = "■                          ■";
                }
                else
                {
                    menuStr[i] = "■■■■■■■■■■■■■■■";
                }
            }
        }
        //위 아래는 커서만 옮기기
        /*public void setString() 
        {

            menuStr[0] = 
        "■■■■■■■■■■■■■■■";
        "■                          ■";
        "■                          ■";
        "■           메 뉴          ■";
        "■                          ■";
        "■      상태창              ■";
        "■                          ■";
        "■      인벤토리            ■";
        "■                          ■";
        "■      장비창              ■";
        "■                          ■";
        "■      동료                ■";
        "■                          ■";
        "■      저장                ■";
        "■                          ■";
        "■                          ■";
        "■■■■■■■■■■■■■■■";
            menuStr[34] = "■■■■■■■■■■■■■■■";
        }*/

        //y는 줄 단위로 반환 할 예정

        // 메뉴를 계속 프린트.
        public void PrintMenu(int y)
        {
            if(y < Define.Map_SizeY)
                Console.Write(menuStr[y]);
        }
        //메뉴를 눌렀을 경우에, 앞으로 해당 메뉴를 표시.
    }
}
