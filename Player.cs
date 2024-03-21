using System.Reflection.Emit;
using System.Xml.Linq;

namespace ConsoleGame_ATB
{

    //class 유닛에 정보 좀 때려박고
    // 나머지는 상속 받아서 해결한다.

    class Unit
    {
        protected Stat _mainStat;
        protected string _name;
    }

    class Partner : Unit
    {
        public EquipItem[] EquipItem { get; set; } = new EquipItem[4]; //0 무기, 1 머리, 2 옷, 3 신발
        protected Stat _equipStat;

        public int[] ReqExp = { 50, 200, 500, 1000, 5000 };// 요구 경험치

        protected Stat _mainStat;
        new private string _name;
        public int curHP { get; set; }
        public Stat MainStat { get { return _mainStat; } set { _mainStat = MainStat; } }
        public Stat EquipStat { get { return _equipStat; } set { _equipStat = EquipStat; } }

        public int Exp { get; set; }
        public string Name { get { return _name; } set { _name = Name; } }
        public int Level { get; set; }

        public void AddEXP(int _exp)
        {
            Exp += _exp;
            if (Exp > ReqExp[Level - 1]) { LevelUp(); }
        }

        public void LevelUp()
        {
            Level++;
            Exp -= ReqExp[Level - 1];
            // 스탯 증가시켜줘야 함.
            // 콘솔에 보여줄 메소드가 필요함.
            // Console.WriteLine("레벨업하였습니다.");
        }

        public Partner(string name)
        {
            ItemTable itemTable = new ItemTable();

            _name = name;
            Level = 1;
            Exp = 0;
            _mainStat = new Stat(100, 10, 10, 10);
            _equipStat = new Stat(0, 0, 0, 0);
            curHP = _mainStat.MAX_HP;

            EquipItem[0] = itemTable._equips["W_0001"];
            EquipItem[1] = itemTable._equips["H_0001"];
            EquipItem[2] = itemTable._equips["B_0001"];
            EquipItem[3] = itemTable._equips["S_0001"];
        }
    }

    class Player : Partner
    {
        #region 이동 변수/ 메소드

        private int _PosX, _PosY;
        private int PosX { get; set; }
        private int PosY { get; set; }
        public List<Pos> Positions { get { return _positions; } }
        private List<Pos> _positions = new List<Pos>();

        public void Init(int posX, int posY)
        {
            PosX = posX;
            PosY = posY;

            _positions.Add(new Pos(posX, posY));
        }
        public enum Move
        {
            Up = 0,
            Left = 1,
            Down = 2,
            Right = 3
        }

        public void AddGold(int _gold)
        {
            gold += _gold;
        }

        #endregion

        new public EquipItem[] _equipItem { get; set; } = new EquipItem[4]; //0 무기, 1 머리, 2 옷, 3 신발
        public List<Item> Inventory { get; set; } = new List<Item>();
        public int gold;

        new protected Stat _mainStat;
        new private string _name;

        public Player(string name) : base(name)
        {
            Partner _p = new Partner(name);

            _name = name;
            Level = _p.Level;
            Exp = _p.Exp;
            _mainStat = _p.MainStat;
            _equipStat = _p.EquipStat;
            _equipItem = _p.EquipItem;
            curHP = _mainStat.MAX_HP;

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

        public void UsePotion(Item item)
        {
            // 어떤 아이템인지 파라미터로 받고, 아이템의 제일 앞에 있는걸 쓴다.
            bool isUsed = false;
            string itemName = item.Name;

            for (IEnumerator<Item> iter = Inventory.GetEnumerator(); iter.MoveNext();)
            {
                if (item == iter.Current)
                {
                    Inventory.Remove(iter.Current);
                    isUsed = true;
                    break;
                }
            }
            if (isUsed)
            {
                Console.WriteLine($"해당 아이템이 사용되었습니다. : {itemName}");
            }
            else
            {
                Console.WriteLine($"현재 해당 아이템이 없습니다.");
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
            SellItem.Add(iTable._potions["P_0001"]);
            SellItem.Add(iTable._potions["P_0001"]);
            SellItem.Add(iTable._potions["P_0002"]);
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
        private int _PosX, _PosY;
        private int PosX { get; set; }
        private int PosY { get; set; }
        public List<Pos> Positions { get { return _positions; } }
        private List<Pos> _positions = new List<Pos>();


        public int Gold { get; set; }
        public int HP { get; set; }
        public int Exp { get; set; }
        public int Level { get; set; }

        public Stat MainStat { get { return _mainStat; } set { _mainStat = MainStat; } }
        public Enemy()
        {
            Random random = new Random();   
            _name = "고블린";
            _mainStat = new Stat(50, 5, 0, 0);
            HP = _mainStat.MAX_HP;
            Level = 1;
            Exp = 10;
            Gold = 10 + random.Next() % 10;
            PosX = (random.Next() % Define.SizeX_Map-2) +1;
            PosY = (random.Next() % Define.SizeY_Map-2) +1;

            _positions.Add(new Pos(PosX, PosY));
        }
    }

}
