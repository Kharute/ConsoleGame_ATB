using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ConsoleGame_ATB.Player;

namespace ConsoleGame_ATB
{

    // PlayerInfo는 레벨, 경험치, 스탯(+장비스탯)을 보여준다.
    // inventory는 소지금, 장비, 
    // Equipment는 장착장비, 추가효과를 보여준다.
    // Setting은 세이브, 로드, 종료를 보여준다.
    //      - 세이브 기능은 뭐 텍스트에서 불러와도 되고... 흠.

    enum Menu_
    {
        PlayerInfo, Inventory, Equipment, Setting
    };

    internal class Menu
    {
        // 세로길이는 _mapYSize로 고정임

        // _menuSize를 받아서 할 것
        string[] strings = new string[15];

        //y는 줄 단위로 반환 할 예정

        // 메뉴를 계속 프린트.
        public void PrintMenu(int y)
        {
            
        }
        //메뉴를 눌렀을 경우에, 앞으로 해당 메뉴를 표시.
        public void PressMenu(Menu_ _menu) 
        {

        }
    }
}
