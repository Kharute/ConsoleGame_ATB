namespace ConsoleGame_ATB
{

    internal class Player
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

        public void Initialize(int posX, int posY)
        {
            PosX = posX;
            PosY = posY;

            _positions.Add(new Pos(PosX, PosY));
        }

        public void MovePlayer(Move move)
        {
            /*_PosY = _positions[0].Y;
            _PosX = _positions[0].X;*/

            switch (move)
            {
                case Move.Left:
                    _positions[0].X--;
                    break;
                case Move.Right:
                    _positions[0].X++;
                    break;
                case Move.Up:
                    _positions[0].Y--;
                    break;
                case Move.Down:
                    _positions[0].Y++;
                    break;
            }
        }
        #endregion

        int[] ReqExp = { 50, 200, 500, 1000, 5000 };// 요구 경험치

        private string name;
        private int level, exp;
        private Stat mainStat;
        private Stat equipStat;
        public List<EquipItem> equipItem { get; set; } = new List<EquipItem>();

        //플레이어만 줘야됨
        public List<Item> inventory { get; set; } = new List<Item>();
        int gold;

        public Player()
        {
            name = "용사";
            level = 1;
            exp = 0;
            mainStat = new Stat(100, 10, 10, 10);
            equipStat = new Stat(0, 0, 0, 0);
            inventory = new List<Item>();
            equipItem = new List<EquipItem>();

        }

        public void AddEXP(int _exp)
        {
            exp += _exp;
            if (exp > ReqExp[level - 1]) { LevelUp(); }
        }

        public void UsePotion(Item item)
        {
            // 어떤 아이템인지 파라미터로 받고, 아이템의 제일 앞에 있는걸 쓴다.
            bool isUsed = false;
            string itemName = item.Name;
            for (IEnumerator<Item> iter = inventory.GetEnumerator(); iter.MoveNext();)
            {
                if (item == iter.Current)
                {
                    inventory.Remove(iter.Current);
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

        public void LevelUp()
        {
            level++;
            exp -= ReqExp[level - 1];
            Console.WriteLine("레벨업하였습니다.");
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
    class NPC_Quest
    {

    }
    class NPC_Partner
    {

    }

    class Monster
    {
        private Stat stat;

        public Monster()
        {
            
        }
    }

}
