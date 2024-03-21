using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleGame_ATB.Scene
{
    internal class StoreScene
    {
        public List<List<string>> storeText = new List<List<string>>();
        private const int WAIT_TICK = 1000 / 50;
        public int curMenu = 0;
        public int curCussor = 0;
        bool escButton = false;

        Player Player { get; set; }
        public NPC_Sell NPC_SELL { get; set; }

        Menu _menu;
        TextBoxInterface textBox;

        string[] s = new string[Define.SizeY_Map];
        List<string> image = new List<string>()
        {
            {"..............................:-!!=.!**!*!!!!!!!**!**=:~, ~!!:.,......................~!-."},
            {"..............................;*;;*~-===*!!!!!!*$=*!!;!=;,*!;= ;......................:;-."},
            {".............................,-!!*$!!;;;;*=!!*=;::::~~::;*=!!! =......................;;-."},
            {".............................!*!=:~:::~~::;=*!::::~~~:::::!$!!.=......................;;-."},
            {".............................!*=~::::::~~::;;:::::~~~~:::::;$*:*......................;;-."},
            {".............................~=~::::::::~:::::::::~~~::::::::$;=......................;;-."},
            {".............................~::::::::::;::;:::;;~~:::::::::::=:......................;;-."},
            {"............,=:*!............=::::::::::*$!:;:*$=*~:::::::::::~~......................::-."},
            {"..........!*;   ,~..........-!::::::::::=$$!;*=$=$;:::::::::~::!......................:;-."},
            {".,,.,~:;;;.      =..........=:::::::::::*,,:~-----*:::::::::::~:~.....................~;-."},
            {".;.,!...        .:..........!~::::::::::*.       ,:;:::::::::~~~=...................-=;!-."},
            {";               ,!.........:~~:::~::::::!.       .,=:::::::~~~-,;...................-!!!-."},
            {":             .,=-.........=.~:~~~::::::;.        ,!::::::~~~~. -:....................;;,."},
            {"~        .,,..-;!..........~  ~~-~~::::;~.        .-*~::~~,-...-,$*===*!;;;!!****====$===="},
            {";.   ,.,.,,..;;,:.........~.,,.. -~;~~~*,          ,!~~~~-   ~~~:~#$$=*!!;:~~~~~~~~~~----~"},
            {"=,,..,.;##;--,..-!~~~~~~~~#-~~~, .-!...!.          .-!~~    -~~:~:*-----------------------"},
            {",=-!=$!-,!;     ,=-!=!!!**~~::~~-- !.,!-::*    ==-  ,!.. .~-~:~::~*,......................"},
            {"..:,.    ..     ,!,!=....!-::::::~-!~:!.- .-  ~  ;  .~;,~~::::::::::...............~=$,..."},
            {"..~-            .:.*#...-!~:::::::;;:$,              ,*:~~::::::::~=-............*=-  :..."},
            {"..~:             --*#..,!-::::::::=:*~         ,,    .-*::::::::::;~:,..........;.   :~..."},
            {"..,;             -!=#..-~::::::::;!!~.   $@,   @@     ,!;:::::::::*;*-..........;.   :...."},
            {"..,!.            ,=*$.-$::::::::;==,,    !#    ~-      ,$:::::::::::!*,..........*$. ;...."},
            {"...!             .*=$,#:::::::::;*~.                   .-*::!:::::::;*~............; ~-..."},
            {"...!             .:*$$~::::::::;*~.         *!          ,~!;*;::::!#,~$~...........~- ;..."},
            {".-;             . ~$-:::::::::!$-                        ,;!!=;:::!!, ,!:...........! -~.."},
            {"=-                ,!;;::::::!$*.            ,~   ,       .:~**:*::;#*.   !..........,~ ;.."},
            {"               .  !.*$*!*$$;,.!.         !-:@@@#=.        ~.,==,=;:!$,    *,.........;  ;."},
            {"                 ~  .,,,,... .;,          -@@@@@.         -:.,~~.;=!$-.    :.........,: -~"},
            {"                ,;           .,-          .@@@@@         .-=, .,.,.-$*.    *..........;. :"},
            {"             .,.,-           ,,*           #@@@$         ,:*~,  ...        !,..........:  "},
            {"          ..,,,,-*          .,==-          ~@@#.        .,$!=,.            *...........;  "},
            {"    ...,,,,.,,~;!:~        .:=*!=,           .          ,*!!*$,,           :...........: ."},
            {",.,.,.,,.,-~;*:---~=-,,,,~*;!!*!*;.                    ,;**!!*#~,.        !,....----~~~*;*"},
            {":~-~::;!**!~-------=!~~~----;!*!!*;,                 .,!*!!!!!$~*:-,   .,*,....---~~::::~~"},
            {"-~~~-------------,-=!-~~-----;!!!**!-,.            .,~=!!!!!*!=---:****!~-,....~!;;!!!!!!!"},
            {",,------------,...-$!-!;-----*!*!!!!=;-.,........,,~*=!!!!!!!!$::::;;;::::---,,~*;!!!!!!!!"},
            {"....,,,,,,,........$!--~:::;;$;!!!!!!!=$;:~----~:*$#=!!!!!!!!!$!!!*******=*====**!!*******"},
            {".......,--~~::;;:;!*!*=***!;;;!!!!!!!!=$*$$*$$$$$===$=!**!!!!!=*********==*****!=**======="},
            {":;!!*===*************===*******!*!**!=$===$:$==$===$:-=*!*!!!;------------------!!;::~~~~!"},
            {"!*!!;:~-,,,...........,--------=!===;;:#;=;=$$$===$;,.~=;!;!!=--------,,,,,,,,,,---------*"},
            {"........................,-----;!*=$**;,;#=$$==$$$*-,,-:*~!!!*~----,,............,--------*"},
            {"............................---~-==!:-!:-;*==*;-.,-~;;--*-*!*-----,..............--------;"},
            {"............................-----*=*..~-~::::::::~-~,,-,;-;*-------...............-------~"},
            {"............................-----*=~ .;. .........,:. .-;!~--------................,------"},
            {"...........................,-----*$:~,;...........,;-:;~ !---------.................,-----"}
        };

        //string 값 계산해서 좌우에 space padding

        int SizeX { get; set; }
        int SizeY { get; set; }

        public void Init(int Xsize, int Ysize)
        {
            SizeX = image[0].Length;
            SizeY = image.Count();

            s = image.ToArray();

        }

        void Render(Menu menu, TextBoxInterface textBox)
        {
            for (int y = 0; y < SizeY; y++)
            {
                for (int x = 0; x < SizeX; x++)
                {
                    if(y < Define.SizeY_Map && x < SizeX)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkGray;
                        Console.Write(s[y][x]);
                    }
                }

                Console.ForegroundColor = ConsoleColor.Yellow;
                menu.PrintMenu(y);

                if (y < Define.SizeY_Map)
                {
                    Console.WriteLine();
                }
            }
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine();

            textBox.PrintTextBox(storeText[curMenu], curCussor); //값을 넣어서 해결 하자.
        }

        public void StoreLoad()
        {
            Init(Define.SizeX_Map, Define.SizeY_Map);
            _menu = new Menu();                            // 상점 메뉴 별도
            textBox = new TextBoxInterface(); // 텍스트 박스 별도
            textBox.init();

            while (!escButton)
            {
                Console.SetCursorPosition(0, 0);

                Render(_menu, textBox);
                curCussor = PressCheck(curCussor);
                Console.Clear();
            }
        }

        //커서 위치 알려줘
        private int PressCheck(int curCussor)
        {
            ConsoleKeyInfo consoleKey = Console.ReadKey();

            switch (consoleKey.Key)
            {
                // 메뉴 움직이기
                case ConsoleKey.UpArrow:
                    // 예외 처리
                    if (curCussor < 1)
                        curCussor = storeText.Count();
                    else
                        curCussor--;
                    textBox.PrintTextBox(storeText[curMenu], curCussor);
                    break;  
                case ConsoleKey.DownArrow:
                    if (curCussor >= storeText.Count())
                        curCussor = 0;
                    else
                        curCussor++;
                    textBox.PrintTextBox(storeText[curMenu], curCussor);
                    break;
                case ConsoleKey.Enter:
                    if(curMenu == 0)
                    {
                        switch (curCussor)
                        {
                            case 0:
                                curMenu = 1;
                                textBox.PrintTextBox(storeText[curMenu], curCussor);
                                break;
                            case 1:
                                curMenu = 2;
                                textBox.PrintTextBox(storeText[curMenu], curCussor);
                                break;
                            case 2:
                                escButton = true;
                                textBox.PrintTextBox(storeText[curMenu], curCussor);
                                return curCussor;
                        }
                        curCussor = 0;
                    }
                    else if(curMenu == 1)
                    {
                        //해당 물건을 사는 경우.
                        Player.Inventory.Add(NPC_SELL.SellItem[curCussor]);
                        storeText[2].Add(NPC_SELL.SellItem[curCussor].Name);
                        storeText[1].Remove(NPC_SELL.SellItem[curCussor].Name);
                        NPC_SELL.SellItem.Remove(NPC_SELL.SellItem[curCussor]);
                        curMenu = 0;
                        curCussor = 0;
                    }
                    else if (curMenu == 2)
                    {
                        //해당 물건을 파는 경우.
                        NPC_SELL.SellItem.Add(Player.Inventory[curCussor]);
                        storeText[1].Add(Player.Inventory[curCussor].Name);
                        storeText[2].Remove(Player.Inventory[curCussor].Name);
                        Player.Inventory.Remove(Player.Inventory[curCussor]);
                        curMenu = 0;
                        curCussor = 0;
                    }
                    textBox.PrintTextBox(storeText[curMenu], curCussor);
                    break;
                case ConsoleKey.Escape:
                    escButton = true;
                    textBox.PrintTextBox(storeText[curMenu], curCussor);
                    break;
                default:
                    break;
            }

            return curCussor;
        }

        public void stringAdd()
        {
            List<string> sStoreStart =
            [
                $"       떼미이ㅣㅣㅣㅣㅣㅣ 물건판다ㅏㅏㅏㅏ ",
                $"       산다",
                $"       판다",
                $"       도망친다.     "
            ];
            storeText.Add(sStoreStart);

            List<string> sStoreBuy =
            [
                $"       머살꺼야ㅑㅑㅑㅑㅑㅑ",
            ];
            for (int i = 0; i < NPC_SELL.SellItem.Count; i++)
            {
                sStoreBuy.Add($"{NPC_SELL.SellItem[i].Name}");
            }

            /// 물건 없으면 없다고 반환.
            
            storeText.Add(sStoreBuy);

            List<string> sStoreSell =
            [
                $"       머팔꺼야ㅑㅑㅑㅑㅑㅑ ",
            ];
            for (int i = 0; i < Player.Inventory.Count; i++)
            {
                sStoreSell.Add($"{Player.Inventory[i].Name}");
            }
            storeText.Add(sStoreSell);
        }

        public void Update(NPC_Sell nPC_Sell, Player player)
        {
            Player = player;
            NPC_SELL = nPC_Sell;

            stringAdd();
            /*line_dic._lineDic_Pinfo*/
        }

    }
}

/*
 우리는 시드 가르기를 할꺼에요
상점 창 안에 
대화한다, 산다, 판다, 나간다
넣고
산다, 판다 안에는 
산다 = 떼미 아이템 리스트
판다 = 내 아이템리스트 
나열.

A 안에 
B, C, D F
 */
