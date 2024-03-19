using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleGame_ATB
{
    // Title 씬 먼저 보여준다.
    // 게임이 끝나면, 타이틀상태로 돌아가 엔딩 또는 게임오버처리를 한다.
    // 그리고 다시 타이틀 상태가 된다.

    public enum TitleState
    {
        Start, GameOver, Ending
    }
    interface ITitleState
    {
        void Action();
    }

    public class Start : ITitleState
    {
        /*private string name;
        public Start(string name)
        {
            this.name = name;
        }*/
        public void Action()
        {
            bool isSelect = true, ready = true;
            
            while (isSelect)
            {
                Console.WriteLine("\n\n\n\n\n\n\n\n\n\n\n\n");
                Console.WriteLine("\t\t\t\t\t\t\t게임 시작");
                Console.WriteLine("\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n");

                if (ready)
                {
                    Console.WriteLine("\t\t\t\t\t\t   ▶   시작 한다");
                    Console.WriteLine("\t\t\t\t\t\t        종료 한다");
                }                                     
                else                                  
                {                                     
                    Console.WriteLine("\t\t\t\t\t\t        시작 한다");
                    Console.WriteLine("\t\t\t\t\t\t   ▶   종료 한다");
                }

                ConsoleKeyInfo consoleKey = Console.ReadKey();
                switch (consoleKey.Key)
                {
                    case ConsoleKey.UpArrow:
                    case ConsoleKey.DownArrow:
                        ready = !ready;
                        break;
                    case ConsoleKey.Enter:
                        isSelect = false;
                        break;
                }
                Console.Clear();
            }

            if (!ready)
            {
                Environment.Exit(0);
            }
        }
    }
    public class GameOver : ITitleState
    {
        //private string name;
        /*public GameOver(string name)
        {
            this.name = name;
        }*/
        public void Action()
        {
            Console.WriteLine("\n\n\n\n\n\n\n\n\n\n");
            Console.WriteLine("                             게임 오버                               ");

            Console.WriteLine("\n\n\n\n\n\n\n\n\n\n");
            Console.ReadLine();
            Console.Clear();
        }
    }
    public class Ending : ITitleState
    {
        /*private string name;
        public Ending(string name)
        {
            this.name = name;
        }*/
        public void Action()
        {
            Console.WriteLine("\n\n\n\n\n\n");
            Console.WriteLine("                          Ending.01 모험의 시작                  ");

            Console.WriteLine("\n\n\n\n\n\n");
            Console.ReadLine();
        }
    }

    class TitleDic
    {
        public Dictionary<TitleState, ITitleState> titles_dic;

        public TitleDic()
        {
            titles_dic = new Dictionary<TitleState, ITitleState>
            {
                { TitleState.Start, new Start() },
                { TitleState.GameOver, new GameOver() },
                { TitleState.Ending, new Ending() }
            };
        }

    }

    internal class TitleScene
    {
        public TitleScene(TitleState state)
        {
            this.State = state;
        }

        public TitleState State { get; set; }

        public void Action()
        {
            TitleDic title = new TitleDic();

            title.titles_dic[State].Action();
        }
    }
}
