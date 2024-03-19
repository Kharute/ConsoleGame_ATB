using ConsoleGame_ATB.Scene;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;



namespace ConsoleGame_ATB
{
    
    internal class BasicScene
    {
        public BasicScene() { }

        private ItemTable itemTable;
        private Map _map;
        private Menu _menu;
        private Player _player;
        private NPC_Sell _NPCsell1, _NPCsell2;
        private TextBoxInterface _textBox;

        private const int WAIT_TICK = 1000 / 50;
        private const int WAIT_PRESS_TICK = 1000 / 4;

        //캐릭터들의 포지션 value값
        List<List<Pos>> list_Position = new List<List<Pos>>();

        int _mapXSize = Define.Map_SizeX;
        int _mapYSize = Define.Map_SizeY;
        int _menuSize = Define.Menu_SizeX;

        public void BasicLoad()
        {
            itemTable = new ItemTable();
            _map = new Map();
            _menu = new Menu();
            _player = new Player();
            _NPCsell1 = new NPC_Sell();
            _NPCsell2 = new NPC_Sell();
            _textBox = new TextBoxInterface();


            _map.Initialize(_mapXSize, _mapYSize, _menuSize);
            _menu.Initialize(_menuSize, _mapYSize);
            //_menu.setPotionTable();
            _player.Initialize(_mapXSize/2, _mapYSize/2);
            
            //겹치면 조져지긴 함.
            _NPCsell1.Init(20, 20, itemTable);
            _NPCsell2.Init(20, 30, itemTable);
            _textBox.init();

            list_Position.Add(_player.Positions); // listPositon[0] = _player.Positions
            list_Position.Add(_NPCsell1.Positions); // listPositon[0] = _NPCsell.Positions
            list_Position.Add(_NPCsell2.Positions); // listPositon[0] = _NPCsell.Positions

            /*_NPCsell.SellItem.Add(new Potion("P_0001"));*/

            _menu.fillMenu();
            _menu.CurssorMove(0);
            int lastTick = System.Environment.TickCount;
            int clockTick = System.Environment.TickCount;

            bool basicChk = true;

            while (true)
            {
                #region 프레임 관리
                int currentTick = System.Environment.TickCount;
                int elapsedTick = currentTick - lastTick;

                if (elapsedTick < WAIT_TICK)
                    continue;

                lastTick = currentTick;
                #endregion

                Console.SetCursorPosition(0, 0);
                
                _map.Render(list_Position, _menu, _textBox);
                /*
                int cClickTick = currentTick - clockTick;
                if (cClickTick < WAIT_PRESS_TICK)
                {
                    continue;
                }
                /clockTick = currentTick;*/


                //case 갈라서 a면 맵체크 b면 메뉴체크

                
                if (basicChk)
                {
                    basicChk = MainPressCheck(_textBox);
                }
                else
                {
                    basicChk = MenuPressCheck(_menu);
                }
            }
            // 맵 렌더
        }

        private bool MainPressCheck(TextBoxInterface textBox)
        {
            if (Console.KeyAvailable)
            {
                ConsoleKeyInfo consoleKey = Console.ReadKey();

                switch (consoleKey.Key)
                {
                    case ConsoleKey.LeftArrow:
                        //충돌 체크 우선
                        if (_map.RigidCheck(_player, Player.Move.Left))
                        {
                            _player.MovePlayer(Player.Move.Left);
                        }
                        break;
                    case ConsoleKey.RightArrow:
                        if (_map.RigidCheck(_player, Player.Move.Right))
                        {
                            _player.MovePlayer(Player.Move.Right);
                        }
                        break;
                    case ConsoleKey.UpArrow:
                        if (_map.RigidCheck(_player, Player.Move.Up))
                        {
                            _player.MovePlayer(Player.Move.Up);
                        }
                        break;
                    case ConsoleKey.DownArrow:
                        if (_map.RigidCheck(_player, Player.Move.Down))
                        {
                            _player.MovePlayer(Player.Move.Down);
                        }
                        break;
                    case ConsoleKey.Enter:
                        NPC_Check(list_Position);
                        break;
                    case ConsoleKey.Tab:
                        return false;
                }
                
            }
            return true;
        }
        private bool MenuPressCheck(Menu _menu)
        {
            if (Console.KeyAvailable)
            {
                ConsoleKeyInfo consoleKey = Console.ReadKey();
                int menu = (int)_menu.MAIN_MENU;

                switch (consoleKey.Key)
                {
                    // 메뉴 움직이기
                    case ConsoleKey.UpArrow:
                        // 예외 처리
                        if( _menu.MAIN_MENU == MainMenu.PInfoMenu)
                            _menu.CurssorMove(4);
                        else
                            _menu.CurssorMove(menu - 1);
                        break;
                    case ConsoleKey.DownArrow:

                        if (_menu.MAIN_MENU == MainMenu.SaveMenu)
                            _menu.CurssorMove(0);
                        else
                            _menu.CurssorMove(menu + 1);
                        break;
                    case ConsoleKey.Enter:
                        // 해당 안에 시드로 들어간다.
                        // 내정보, 동료정보 는 그냥 Esc만 먹히게
                        break;
                    case ConsoleKey.Escape:
                        // 탈출할 게 남아있다면, break. 아니면 true
                        /*if()
                        {
                            break;
                        }*/
                    case ConsoleKey.Tab:
                        return true;
                    
                }
            }
            return false;
        }

        private void NPC_Check(List<List<Pos>> pos)
        {
            int _pX = _player.Positions[0].X;
            int _pY = _player.Positions[0].Y;

            for (int i = 1; i < pos.Count; i++)
            {
                bool chk_left = pos[i][0].X - 1 == _pX && pos[i][0].Y == _pY;
                bool chk_right = pos[i][0].X + 1 == _pX && pos[i][0].Y == _pY;
                bool chk_up = pos[i][0].X == _pX && pos[i][0].Y - 1 == _pY;
                bool chk_down = pos[i][0].X == _pX && pos[i][0].Y + 1 == _pY;

                if (chk_left || chk_right || chk_up || chk_down)
                {
                    StoreScene storeScene = new StoreScene();
                    storeScene.StoreLoad();
                    //_textBox.TextBoxChanged();
                }
            }
        }
    }
}
