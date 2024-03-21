namespace ConsoleGame_ATB
{
    public enum PotionEffect { Heal, ATK_Up, DEF_Up, SPD_Up }
    public enum EquipType { Weapon, Head, Body, Shoes }

    class Item
    {
        protected string itemID;       //아이템ID
        protected string _name;
        public string ItemID { get { return itemID; } set { itemID = ItemID; } }
        public string Name { get { return _name; } set { _name = Name; } }
    }
    class Potion : Item
    {
        //getPotion(ID) 를 입력하면, 해당 ID에 있는 Potion을 Player의 Inventory에 넣고) 만들면 Dic에서 찾아서
        // itemID랑 사이즈로
        private PotionEffect _pEffect;
        private int _value;

        public int Value { get { return _value; } set { _value = Value; } }

        public Potion(string id, string name, int value, PotionEffect pEffect)
        {
            itemID = id;
            _name = name;
            _value = value;
            _pEffect = pEffect;
        }
    }
    

    class EquipItem : Item
    {
        private int objectID;     //고유 ID
        private EquipType _type;
        public Stat ItemStat { get; set; }

        public int ObjectID { get { return objectID; } set { objectID = ObjectID; } }

        //여기에 장비 DIC로 여러 개 등록
        /*public int WeaponAtk { get { return weaponAtk; } set { weaponAtk = WeaponAtk; } }
        public int WeaponDef { get { return weaponDef; } set { weaponDef = WeaponDef; } }*/

        //무기류는 매번 생성할 때마다 1씩 올려줘야됨.
        public EquipItem(string wName, int obj, EquipType type, Stat stat)
        {
            _name = wName;
            objectID = obj;
            _type = type;
            ItemStat = stat;
        }
    }

    class ItemTable
    {
        public Dictionary<string, Potion> _potions;
        public Dictionary<string, EquipItem> _equips;

        public ItemTable()
        {
            _potions = new Dictionary<string, Potion>
            {
                { "P_0001", new Potion("P_0001", "레드 포션", 100, PotionEffect.Heal) },
                { "P_0002", new Potion("P_0002", "블루 포션", 100, PotionEffect.Heal) },
                { "P_0003", new Potion("P_0003", "하이 레드 포션", 100, PotionEffect.Heal) },
                { "P_0004", new Potion("P_0004", "하이 블루 포션", 100, PotionEffect.Heal) },
                { "P_0005", new Potion("P_0005", "공격의 물약", 100, PotionEffect.ATK_Up) },
                { "P_0006", new Potion("P_0006", "방어의 물약", 100, PotionEffect.DEF_Up) },
                { "P_0007", new Potion("P_0007", "속도의 물약", 100, PotionEffect.SPD_Up) },
            };
            _equips = new Dictionary<string, EquipItem>
            {
                { "W_0001", new EquipItem("뗀석기", 0, EquipType.Weapon, new Stat(0, 1, 0, 0))}         ,
                { "W_0002", new EquipItem("청동검", 0, EquipType.Weapon, new Stat(0, 10, 0, 0))}        ,
                { "W_0003", new EquipItem("철검", 0, EquipType.Weapon, new Stat(0, 20, 0, 0))}          ,
                { "W_0004", new EquipItem("빔샤벨", 0, EquipType.Weapon, new Stat(0, 100, 0, 0))}       ,
                { "H_0001", new EquipItem("그릇", 0, EquipType.Head, new Stat(0, 0, 5, 0)) }            ,
                { "H_0002", new EquipItem("비니", 0, EquipType.Head, new Stat(0, 0, 10, 0))}            ,
                { "H_0003", new EquipItem("투구", 0, EquipType.Head, new Stat(0, 0, 20, -5))}           ,
                { "B_0001", new EquipItem("거적떼기", 0, EquipType.Body, new Stat(0, 0, 5, 0))}         ,
                { "B_0002", new EquipItem("철기사갑옷", 0, EquipType.Body, new Stat(0, 0, 100, -10))}   ,
                { "S_0001", new EquipItem("짚신", 0, EquipType.Shoes, new Stat(0, 0, 1, 5))}            ,
                { "S_0002", new EquipItem("아디다스", 0, EquipType.Shoes, new Stat(0, 0, 5, 20))}
            };
        }
    }
}