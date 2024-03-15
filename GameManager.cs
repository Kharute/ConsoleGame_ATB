using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ConsoleGame_ATB
{
    public class MyButton
    {
        // 1. event 선언
        public event EventHandler Click;

        public void MouseButtonDown()
        {
            if (this.Click != null)
            {
                // 5. 이벤트 발생
                Click(this, EventArgs.Empty);
            }
        }
    }

    // 여기서 씬 관리 할 것.
    enum GameScene
    {
        Title, Main, Battle, Store
    }
    internal class GameManager
    {
        private Map _map;
        private Player _player;


        private int _mapXSize = 45;
        private int _mapYSize = 35;
        private int _menuSize = 15;

        private const int WAIT_TICK = 1000 / 10;
        private const int WAIT_PRESS_TICK = 1000 / 4;


        public void StartGame()
        {
            // Title 씬 먼저 보여준다.
            // 타이틀이 끝나면 메인을 보여준다
            // 메인 중, 배틀 상태와 스토어 상태를 왔다갔다한다.
            // 게임이 끝나면, 타이틀상태로 돌아가 엔딩 또는 게임오버처리를 한다.
            // 그리고 다시 타이틀 상태가 된다.
            _map = new Map();
            _player = new Player();

            _map.Initialize(_mapXSize, _mapYSize, _menuSize);
            _player.Initialize(10, 12);


            Console.CursorVisible = false;
            int lastTick = System.Environment.TickCount;
            int clockTick = System.Environment.TickCount;

            while (true)
            {
                #region 프레임 관리
                int currentTick = System.Environment.TickCount;
                int elapsedTick = currentTick - lastTick;
                int cClickTick = currentTick - clockTick;

                if (elapsedTick < WAIT_TICK)
                    continue;

                lastTick = currentTick;
                #endregion

                Console.SetCursorPosition(0, 0);

                _map.Render(_player.Positions);

                if (cClickTick < WAIT_PRESS_TICK)
                {
                    continue;
                }

                ConsoleKeyPressCheck();
                clockTick = currentTick;
            }
        }

        private void ConsoleKeyPressCheck()
        {
            if (Console.KeyAvailable)
            {
                ConsoleKeyInfo consoleKey = Console.ReadKey();

                switch (consoleKey.Key)
                {
                    case ConsoleKey.LeftArrow:
                        _player.MovePlayer(Player.Move.Left);
                        break;
                    case ConsoleKey.RightArrow:
                        _player.MovePlayer(Player.Move.Right);
                        break;
                    case ConsoleKey.UpArrow:
                        _player.MovePlayer(Player.Move.Up);
                        break;
                    case ConsoleKey.DownArrow:
                        _player.MovePlayer(Player.Move.Down);
                        break;
                    case ConsoleKey.Tab:
                        // 메뉴로 돌입
                        // 메뉴에서 탈출하면 여기로 오면 됨.
                        break;
                }
            }
        }

    }
}
