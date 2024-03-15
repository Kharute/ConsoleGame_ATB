using System;
using System.Collections.Generic;

namespace ConsoleGame_ATB
{
    public class Pos
    {
        public Pos(int y, int x) { Y = y; X = x; }
        public int Y;
        public int X;
    }

    internal class GameMain
    {
        
        class Stat
        {
            private int max_hp;
            private int atk;
            private int def;
            private int spd;
            public int MAX_HP { get { return max_hp; } set { max_hp= MAX_HP; } }
            public int ATK { get { return atk; } set { atk= ATK; } }
            public int DEF { get { return def; } set { def= DEF; } }
            public int SPD { get { return spd; } set { spd= SPD; } }
            public Stat(int _mHP,int _atk,int _def,int _spd)
            {
                max_hp = _mHP;
                atk = _atk;
                def = _def;
                spd = _spd;
            }
        }

        class Player : Unit
        {
            int[] ReqExp = { 50, 200, 500, 1000, 5000 };
            //---------------------------------------
            //클래스 상단에 작성

            private int exp;
            private int level;
            private Stat mainStat;
            private Stat equipStat;
            private List<Item> inventory;
            private List<EquipItem> equipItems;

            public void AddEXP(int _exp)
            {
                exp += _exp;
                if (exp > ReqExp[level-1]) { LevelUp(); }
            }
            public Player()
            {
                level = 1;
                exp = 0;
                mainStat = new Stat(100, 10, 10, 10);
                equipStat = new Stat(0, 0, 0, 0);
                inventory = new List<Item>();
                equipItems = new List<EquipItem>();
            }
            
            public void LevelUp()
            {
                level++;
                exp -= ReqExp[level-1];
                Console.WriteLine("레벨업하였습니다.");
            }
        }

        abstract class Unit
        {
            string name;
            public int CurrentHP;
        }
        abstract class Item
        {
            private int itemID;
            private int objectID;

            public string Name { get; set; }
        }

        class EquipItem : Item
        {
            enum EquipType
            {
                Weapon, Head, Body, Shoes
            }
        }
        



        static void Main(string[] args)
        {
            GameManager gameManager = new GameManager();

            Console.SetWindowSize(120, 44);
            Console.SetBufferSize(120, 44);
            gameManager.StartGame();

            
        }
    }
}
