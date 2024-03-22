using ConsoleGame_ATB.Scene;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleGame_ATB
{
    internal class BasicScene
    {
        private ItemTable itemTable;
        private Map _map;
        public Menu _menu;
        private Player _player;
        private NPC_Sell _NPCsell1, _NPCsell2;

        private TextBox _textBox;
        private Partner[] _players = new Partner[4]; //플레이어 파티 정보

        //캐릭터들의 포지션 value값
        List<List<Pos>> list_Position = new List<List<Pos>>();

        int _mapXSize = Define.SizeX_Map;
        int _mapYSize = Define.SizeY_Map;
        int _menuSize = Define.SizeX_Menu;

        public void RandomIncount()
        {
            // 이동할 때마다 랜덤 인카운트 부여.
            // 특정확률로 전투 실시.

            Random random = new Random();
            int randInt = random.Next() % 100;
            if (5 > randInt)
            {
                BattleScene bScene = new BattleScene();

                bScene.Update(_players, _player);
                bScene.BattleLoad();
            }
        }

        public void BasicLoad()
        {
            _map = new Map();
            _menu = new Menu();
            itemTable = new ItemTable();
            _textBox = new TextBox();

            _player = new Player("용사 아리스");
            _player.Inventory.Add(itemTable._equips["W_0001"]);
            _player.Inventory.Add(itemTable._potions["P_0001"]);
            _player.Inventory.Add(itemTable._potions["P_0002"]);
            _player.Inventory.Add(itemTable._potions["P_0003"]);
            _player.Inventory.Add(itemTable._potions["P_0004"]);
            _player.Inventory.Add(itemTable._potions["P_0005"]);
            _player.Inventory.Add(itemTable._potions["P_0006"]);

            _players[0] = _player;
            _players[1] = new Partner("동료 A");
            _players[2] = new Partner("동료 B");
            _players[3] = new Partner("동료 C");

            _NPCsell1 = new NPC_Sell();
            _NPCsell2 = new NPC_Sell();

            _map.Init(_mapXSize, _mapYSize, _menuSize);
            _menu.Init(_menuSize, _mapYSize);

            //겹치면 조져지긴 함.
            _player.Init(_mapXSize / 2, _mapYSize / 2);
            _NPCsell1.Init(20, 20, itemTable);
            _NPCsell2.Init(20, 30, itemTable);
            
            _textBox.init();

            list_Position.Add(_player.Positions);   // listPositon[0] = _player.Positions
            list_Position.Add(_NPCsell1.Positions); // listPositon[1] = _NPCsell1.Positions
            list_Position.Add(_NPCsell2.Positions); // listPositon[2] = _NPCsell2.Positions

            _map.Update(list_Position);
            _menu.DicUpdate(_players, _player);

            _menu.ClearMenu();

            //메뉴에 파티정보 업데이트
            bool basicChk = true;

            while (true)
            {
                _menu.stringAdd();
                Console.SetCursorPosition(0, 0);
                _map.Render(list_Position, _menu, _textBox);

                if (basicChk)
                    basicChk = MainPressCheck(_textBox);
                else
                    basicChk = MenuPressCheck(_menu);
            }
        }

        private bool MainPressCheck(TextBox textBox)
        {
            ConsoleKeyInfo consoleKey = Console.ReadKey();

            switch (consoleKey.Key)
            {
                case ConsoleKey.LeftArrow:
                    //충돌 체크 우선
                    if (_map.RigidCheck(_player, Player.Move.Left))
                    {
                        _player.MovePlayer(Player.Move.Left);
                        RandomIncount();
                    }
                    break;
                case ConsoleKey.RightArrow:
                    if (_map.RigidCheck(_player, Player.Move.Right))
                    {
                        _player.MovePlayer(Player.Move.Right);
                        RandomIncount();
                    }
                    break;
                case ConsoleKey.UpArrow:
                    if (_map.RigidCheck(_player, Player.Move.Up))
                    {
                        _player.MovePlayer(Player.Move.Up);
                        RandomIncount();
                    }  
                    break;
                case ConsoleKey.DownArrow:
                    if (_map.RigidCheck(_player, Player.Move.Down))
                    { 
                        _player.MovePlayer(Player.Move.Down);
                        RandomIncount();
                    }
                    break;
                case ConsoleKey.Enter:
                    NPC_Check(list_Position);
                    break;
                case ConsoleKey.Tab:
                    return false;
            }
            
            
            return true;
        }
        private bool MenuPressCheck(Menu _menu)
        {
            return _menu.PressCheck(_menu);
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

                    //위치로 누구인지 판별한다.
                    storeScene.Update(_NPCsell1, _player);
                    storeScene.StoreLoad();
                    //_textBox.TextBoxChanged();
                }
            }
        }
    }
}
