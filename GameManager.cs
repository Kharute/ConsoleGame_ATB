using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
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
        Title, Basic, Battle, Store      //main 이름 바꿀 꺼 생각해 둘 것.(GameMain이랑 이름 헷갈림)
    }
    public interface ISceneState
    {
        void Action();
    }

    public class Title : ISceneState
    {
        public TitleState state;
        public Title(TitleState state)
        {
            this.state = state;
        }
        public void Action()
        {
            TitleScene titleScene = new TitleScene(state);
            titleScene.State = state;
            titleScene.Action();
        }
    }

    public class Basic : ISceneState
    {
        public void Action()
        {
            //메인 씬 구현
            // title로 되있지만 메인을 바꿀 것.
            //TitleScene titleScene = new TitleScene();
            //titleScene.Action();
        }
    }

    public class Battle : ISceneState
    {
        /*private string name;
        public Battle(string name)
        {
            this.name = name;
        }*/

        public void Action()
        {
            //TitleScene titleScene = new TitleScene();
            /*titleScene.State = TitleState.Start;
            titleScene.Action();*/
            // 상태값 받아서 씬 불러오기
        }
    }

    public class Store : ISceneState
    {
        /*private string name;
        public Store(string name)
        {
            this.name = name;
        }*/

        public void Action()
        {
            /*TitleScene titleScene = new TitleScene();
            titleScene.State = TitleState.Start;
            titleScene.Action();*/
            // 상태값 받아서 씬 불러오기
        }
    }

    class ActScene
    {
        private TitleState subScene { get; set; }
        public Dictionary<GameScene, ISceneState> scene_dic;

        public ActScene(TitleState scene)
        {
            this.subScene = scene;
            scene_dic = new Dictionary<GameScene, ISceneState>
            {
                { GameScene.Title, new Title(subScene) },
                { GameScene.Basic, new Basic() },
                { GameScene.Battle, new Battle() },
                { GameScene.Store, new Store() }
            };
        }
    }

    internal class GameManager
    {
        public void StartGame()
        {
            // 게임 전반적인 흐름을 관여하는 곳.

            // 베이직씬은 맵, 메뉴, 텍스트박스를 렌더링 해주고.
            // 기본상태는 상하좌우, 메뉴는 tab, 대화는 엔터로 한다.
            // 메뉴상태는 


            // Title 씬 먼저 보여준다.
            // 타이틀이 끝나면 베이직씬을 보여준다
            // 베이직 중, 배틀 상태와 스토어 상태를 왔다갔다한다.
            // 게임이 끝나면, 타이틀상태로 돌아가 엔딩 또는 게임오버처리를 한다.
            // 그리고 다시 타이틀 상태가 된다.

            ActScene _scene = new ActScene(TitleState.Start); //씬 초기값을 넣어주고 이후 바꾸는 식으로 재현할 것.
            _scene.scene_dic[GameScene.Title].Action();


            BasicScene bScene = new BasicScene(); //씬 초기값을 넣어주고 이후 바꾸는 식으로 재현할 것.
            bScene.BasicLoad();
            //씬 넣고 싶으면 이렇게 넣으면 됨.
            /*Scene = new Scene(TitleState.GameOver);
            Scene.scene_dic[GameScene.Title].Action();*/
        }
    }
}
