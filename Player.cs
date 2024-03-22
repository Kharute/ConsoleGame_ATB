using System.Reflection.Emit;
using System.Xml.Linq;

namespace ConsoleGame_ATB
{

    //class 유닛에 정보 좀 때려박고
    // 나머지는 상속 받아서 해결한다.

    abstract class Unit
    {
        public Stat _mainStat;
        protected string _name;
        public int HP { get; set; }
    }

    class Partner : Unit
    {
        public EquipItem[] EquipItem { get; set; } = new EquipItem[4]; //0 무기, 1 머리, 2 옷, 3 신발
        protected Stat _equipStat;

        public int[] ReqExp = { 50, 200, 500, 1000, 5000 };// 요구 경험치
        new private string _name;
        public Stat MainStat { get { return _mainStat; } set { _mainStat = MainStat; } }
        public Stat EquipStat { get { return _equipStat; } set { _equipStat = EquipStat; } }

        public int Exp { get; set; }
        public string Name { get { return _name; } set { _name = Name; } }
        public int Level { get; set; }

        public int Attack(Enemy enemy)
        {
            Random rand = new Random();
            int uDmg = _mainStat.ATK + EquipItem[0].ItemStat.ATK + rand.Next() % 5;

            enemy.HP -= uDmg;
            
            return uDmg;
        }
        public virtual string[] GetReward(Enemy enemy)
        {
            string[] ret = new string[4]; //
            Exp += enemy.Exp;
            if (Exp > ReqExp[Level - 1]) { LevelUp(); }
            return ret;
        }

        public void LevelUp()
        {
            Level++;
            Random random = new Random();
            MainStat.ATK += random.Next() % 2;
            MainStat.DEF += random.Next() % 2;
            MainStat.SPD += random.Next() % 2;
            Exp -= ReqExp[Level - 1];
        }

        public Partner(string name)
        {
            ItemTable itemTable = new ItemTable();

            _name = name;
            Level = 1;
            Exp = 0;
            _mainStat = new Stat(100, 10, 10, 10);
            _equipStat = new Stat(0, 0, 0, 0);
            HP = _mainStat.MAX_HP;

            EquipItem[0] = itemTable._equips["W_0001"];
            EquipItem[1] = itemTable._equips["H_0001"];
            EquipItem[2] = itemTable._equips["B_0001"];
            EquipItem[3] = itemTable._equips["S_0001"];
        }

        //사망 체크 되면 TRUE 아님 FALSE 반환
        
    }

    class Player : Partner
    {
        #region 이동 변수/ 메소드

        private int _PosX, _PosY;
        private int PosX { get; set; }
        private int PosY { get; set; }
        public List<Pos> Positions { get { return _positions; } }
        private List<Pos> _positions = new List<Pos>();

        public enum Move
        {
            Up = 0,
            Left = 1,
            Down = 2,
            Right = 3
        }
        #endregion

        public EquipItem[] _equipItem { get; set; } = new EquipItem[4]; //0 무기, 1 머리, 2 옷, 3 신발
        public List<Item> Inventory { get; set; } = new List<Item>();
        public int gold;

        new protected Stat _mainStat;
        new private string _name;
        public override string[] GetReward(Enemy enemy)
        {
            string[] ret = new string[4]; //
            Exp += enemy.Exp;
            if (Exp > ReqExp[Level - 1]) { LevelUp(); }
            return ret;
        }
        public Player(string name) : base(name)
        {
            Partner _p = new Partner(name);

            _name = name;
            Level = _p.Level;
            Exp = _p.Exp;
            _mainStat = _p.MainStat;
            _equipStat = _p.EquipStat;
            _equipItem = _p.EquipItem;
            HP = _mainStat.MAX_HP;

            Inventory = new List<Item>();
        }

        public void MovePlayer(Move move)
        {
            switch (move)
            {
                case Move.Left:
                    Positions[0].X--;
                    break;
                case Move.Right:
                    Positions[0].X++;
                    break;
                case Move.Up:
                    Positions[0].Y--;
                    break;
                case Move.Down:
                    Positions[0].Y++;
                    break;
            }
        }
        public void itemAdd(string s1)
        {
            //Inventory.Add(itemTable._equips["W_0001"]);
        }
        public void AddGold(int _gold)
        {
            gold += _gold;
        }
        public void Init(int posX, int posY)
        {
            PosX = posX;
            PosY = posY;

            _positions.Add(new Pos(posX, posY));
        }
        public void UsePotion(Unit unit, Potion potion)
        {
            // 어떤 아이템인지 파라미터로 받고, 아이템의 제일 앞에 있는걸 쓴다.
            if(potion.ItemID[0] != 'P')
            {
                return;
            }
            
            switch (potion._pEffect)
            {
                case PotionEffect.Heal:
                    unit.HP += potion.Value;
                    break;
                case PotionEffect.ATK_Up:
                    unit._mainStat.ATK += potion.Value;
                    break;
                case PotionEffect.DEF_Up:
                    unit._mainStat.DEF += potion.Value;
                    break;
                case PotionEffect.SPD_Up:
                    unit._mainStat.SPD += potion.Value;
                    break;
            }

            for (IEnumerator<Item> iter = Inventory.GetEnumerator(); iter.MoveNext();)
            {
                if (potion == iter.Current)
                {
                    Inventory.Remove(iter.Current);
                    break;
                }
            }
            
        }
    }

    public class Stat
    {
        private int max_hp;
        private int atk;
        private int def;
        private int spd;
        public int MAX_HP { get { return max_hp; } set { max_hp = MAX_HP; } }
        public int ATK { get { return atk; } set { atk = ATK; } }
        public int DEF { get { return def; } set { def = DEF; } }
        public int SPD { get { return spd; } set { spd = SPD; } }
        public Stat(int _mHP, int _atk, int _def, int _spd)
        {
            max_hp = _mHP;
            atk = _atk;
            def = _def;
            spd = _spd;
        }
    }

    class NPC_Sell
    {
        private int _PosX, _PosY;

        private int PosX { get; set; }
        private int PosY { get; set; }

        private List<Pos> _positions = new List<Pos>();
        public List<Pos> Positions { get { return _positions; } }

        public List<Item> SellItem = new List<Item>();

        public void Init(int posX, int posY, ItemTable iTable)
        {
            PosX = posX;
            PosY = posY;
            _positions.Add(new Pos(PosX, PosY));

            SellItem.Add(iTable._equips["W_0001"]);
            SellItem.Add(iTable._equips["H_0001"]);
            SellItem.Add(iTable._equips["B_0001"]);
            SellItem.Add(iTable._equips["S_0001"]);

            SellItem.Add(iTable._potions["P_0001"]);
            SellItem.Add(iTable._potions["P_0002"]);
            SellItem.Add(iTable._potions["P_0003"]);
            SellItem.Add(iTable._potions["P_0004"]);
        }
    }
    class Gate
    {
        private int _PosX, _PosY;
        private int PosX { get; set; }
        private int PosY { get; set; }
        public List<Pos> Positions { get { return _positions; } }
        private List<Pos> _positions = new List<Pos>();

        public Gate(int posX, int posY)
        {
            PosX = posX;
            PosY = posY;

            _positions.Add(new Pos(PosX, PosY));
        }
    }

    class Enemy : Unit
    {
        private int PosX { get; set; }
        private int PosY { get; set; }
        public List<Pos> Positions { get { return _positions; } }
        private List<Pos> _positions = new List<Pos>();

        public int Gold { get; set; }
        public int Exp { get; set; }
        public int Level { get; set; }

        public Stat MainStat { get { return _mainStat; } set { _mainStat = MainStat; } }

        public Enemy()
        {
            Random random = new Random();   
            _name = "고블린";
            _mainStat = new Stat(100, 5, 0, 0);
            HP = _mainStat.MAX_HP;
            Level = 1;
            Exp = 10;
            Gold = 10 + random.Next() % 10;
            PosX = (random.Next() % Define.SizeX_Map-2) +1;
            PosY = (random.Next() % Define.SizeY_Map-2) +1;

            _positions.Add(new Pos(PosX, PosY));
        }

        public bool Attack(Unit unit)
        {
            Random rand = new Random();
            int eDmg = MainStat.ATK - rand.Next() % 5 + 2;
            unit.HP -= eDmg;

            return unit.HP < 0;
        }
    }

}
