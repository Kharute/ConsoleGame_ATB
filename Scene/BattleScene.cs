using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace ConsoleGame_ATB.Scene
{
    enum BattleState
    {
        Menu, Attack, Item, Escape
    }
    internal class BattleScene
    {
        public List<List<string>> battleText = new List<List<string>>();
        TextBox textBox = new TextBox();
        List<string> images =
            [
                "                                                                            ",
                "                                                                            ",
                "                                                                            ",
                "                                                                            ",
                "                                                                            ",
                "                                                                            ",
                "                                                                            ",
                "                                                                            ",
                "               ＆                                □□□□                   ",
                "         ＆                                      □□□□                   ",
                "                                                 □□□□                   ",
                "                                                 □□□□                   ",
                "              ＆                                                            ",
                "       ＆                                                                   ",
                "                                                                            ",
                "                                                                            ",
                "                                                                            ",
                "                                                                            ",
                "                                                                            ",
                "                                                                            ",
                "                                                                            ",
                "                                                                            ",
                "                                                                            ",
            ];
        Partner[] Partys { get; set; }
        Player Player { get; set; }

        Enemy enemy = new Enemy();
        List<Potion> potion = new List<Potion>();
        int potionCur = 0;
        BattleState battleState = BattleState.Menu;
        int curMenu, curCussor = 0;
        
        bool isEscBtn = false;

        public void Update(Partner[] party, Player player)
        {
            Player = player;
            Partys = party;
            TextAdd();
        }

        public bool BattleLoad()
        {
            Console.Clear();
            
            textBox.init();
            while (battleState != BattleState.Escape)
            {
                Render(images, textBox);

                textBox.PrintTextBox(battleText[curMenu], curCussor);
                battleState = PressCheck();
                //전투냐 탈출이냐 배틀이냐 메뉴이동이냐
                //메뉴랑 커서만 반환하고 전투는 새로 깔아?
                
                switch (battleState)
                {
                    //Menu랑 Escape는 알아서 돈다.
                    case BattleState.Attack:
                        //전투 상황을 반환해야할 듯?
                        for(int i = 0; i< Partys.Length;i++)
                        {
                            if (Partys[i].HP > 0) //공격 가능 조건
                            {
                                // 이겼으면 보상을 얻고 전투 종료
                                // 보상 : 경험치/전체, 아이템,골드/플레이어
                                int dmg = Partys[i].Attack(enemy);
                                
                                if (enemy.HP < 0)
                                {
                                    for (int k = 0; k < Partys.Length; k++)
                                    {
                                        Partys[k].GetReward(enemy);
                                        Render(images, textBox);
                                        textBox.PrintBattleTextBox(3, Partys[k].Name, enemy.Exp);
                                        Thread.Sleep(1000);
                                    }
                                    battleState = BattleState.Escape;
                                    break;
                                }
                                Render(images, textBox);
                                textBox.PrintBattleTextBox(1, Partys[i].Name, dmg);

                                Thread.Sleep(1000);
                            }
                        }
                        EnemyAttack();
                        
                        curMenu = 0;
                        curCussor = 0;
                        break;
                    case BattleState.Item:
                        PotionEffect potionEffect = potion[potionCur]._pEffect;
                        int value = potion[potionCur].Value;
                        Player.UsePotion(Partys[curCussor], potion[potionCur]);
                        Render(images, textBox);
                        switch(potionEffect)
                        {
                            case PotionEffect.Heal:
                                textBox.PrintBattleTextBox(7, Partys[curCussor].Name, value);
                                break;
                            case PotionEffect.ATK_Up:
                                textBox.PrintBattleTextBox(8, Partys[curCussor].Name, value);
                                break;
                            case PotionEffect.DEF_Up:
                                textBox.PrintBattleTextBox(9, Partys[curCussor].Name, value);
                                break;
                            case PotionEffect.SPD_Up:
                                textBox.PrintBattleTextBox(10, Partys[curCussor].Name, value);
                                break;
                        }
                        Thread.Sleep(1000);
                        curMenu = 0;
                        curCussor = 0;
                        break;
                }
            }
            //죽었는지 물어보고 맞으면 사망처리
            if(Player.HP < 0)
                return true;
            else
                return false;
        }

        private void EnemyAttack()
        {
            Random random = new Random();

            int targetNO = random.Next() % Partys.Length;
            int deathCount = 0;
            //타겟이 죽었다면 +1+1+1+1해서 다 죽었으면
            for (int i = 0; i < Partys.Length; i++)
            {
                if (Partys[(targetNO++) % Partys.Length].HP <= 0)
                    deathCount++;
                else
                    break;
            }
            if (deathCount < 4)
            {
                if (enemy.Attack(Partys[(targetNO) % Partys.Length]))
                {
                    deathCount = 0;
                    for (int i = 0; i < Partys.Length; i++)
                    {
                        if (Partys[i].HP == 0)
                            deathCount += 1;
                    }
                }
            }
            if (deathCount >= 4)
            {
                Render(images, textBox);
                textBox.PrintBattleTextBox(4, "", 0);
                Thread.Sleep(1000);
            }
        }

        public void Render(List<string> image, TextBox _textBox)
        {
            Console.Clear();
            Console.SetCursorPosition(0, 0);

            for (int y = 0; y < image.Count; y++)
            {
                Console.WriteLine(image[y]);
            }

            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine();
        }

        public void TextAdd()
        {
            #region sBattleStart
            List<string> sBattleStart =
            [
                $"      전투 시작 ",
                $"        싸운다",
                $"        도망친다."
            ];

            battleText.Add(sBattleStart);
            #endregion sBattleStart
            #region sBattleMenu
            List<string> sBattleMenu = 
            [
                $"      전투 ",
                $"        공격한다.",
                $"        아이템을 사용한다.",
                $"        이전 "
            ];
            battleText.Add(sBattleMenu);
            #endregion sBattleMenu

            //일단 동료는 자동행동하거나 일단 스킵
            //일단 값 다 받고 끝에서 내려갈때 리스트 더 있으면 커서는 유지

            #region sItemMenu
            List<string> sItemMenu = new List<string>();

            sItemMenu.Add("      아이템");

            for (int i = 0; i< Player.Inventory.Count;i++)
            {
                
                if (Player.Inventory[i].ItemID[0] == 'P')
                {
                    sItemMenu.Add(Player.Inventory[i].Name);
                    potion.Add((Potion)Player.Inventory[i]);
                }
                    
            }
            battleText.Add(sItemMenu);
            #endregion sItemMenu

            #region sWhoMenu
            List<string> sWhoMenu = new List<string>();

            sWhoMenu.Add("  누구에게 사용하시겠습니까?");

            for (int i = 0; i < Partys.Length; i++)
            {
                sWhoMenu.Add(Partys[i].Name);
            }
            battleText.Add(sWhoMenu);
            #endregion sWhoMenu

        }

        public BattleState PressCheck()
        {
            //battle랑 상점의 조건트리가 다름.
            ConsoleKeyInfo consoleKey = Console.ReadKey();

            switch (consoleKey.Key)
            {
                case ConsoleKey.UpArrow:
                    if (curCussor < 1)
                        curCussor = battleText[curMenu].Count - 2;
                    else
                        curCussor--;
                    break;
                case ConsoleKey.DownArrow:
                    if (curCussor >= battleText[curMenu].Count - 2)
                        curCussor = 0;
                    else
                        curCussor++;
                    break;
                case ConsoleKey.Enter:
                    if (curMenu == 0)
                    {
                        //메뉴 > 싸운다, 도망친다.
                        switch (curCussor)
                        {
                            case 0:
                                curMenu = 1;
                                break;
                            case 1:
                                return BattleState.Escape;
                        }
                        curCussor = 0;
                    }
                    else if (curMenu == 1)
                    {
                        switch (curCussor)
                        {
                            case 0:
                                curMenu = 0;
                                return BattleState.Attack;
                            case 1:
                                curMenu = 2;
                                break;
                            case 2:
                                curMenu = 0;
                                break;
                        }
                    }
                    else if (curMenu == 2)
                    {
                        potionCur = curCussor;
                        curMenu += 1;
                    }
                    else if (curMenu == 3)
                    {
                        return BattleState.Item;
                    }
                    curCussor = 0;
                    break;
                case ConsoleKey.Escape:
                    if (curMenu == 0)
                        return BattleState.Escape;
                    curMenu = 0;
                    curCussor = 0;
                    break;
            }
            return BattleState.Menu;
            // 엔터면 커서 위치 초기화
        }

    }
}
