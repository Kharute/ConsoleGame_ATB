namespace ConsoleGame_ATB
{
    public enum PotionEffect { Heal, ATK_Up, DEF_Up, SPD_Up }
    public enum EquipType { Weapon, Head, Body, Shoes }

    class Item
    {
        protected string? itemID;       //아이템ID
        protected string? _name;
        protected int _price;
        public string ItemID { get { return itemID; } set { itemID = ItemID; } }
        public string Name { get { return _name; } set { _name = Name; } }

        public int Price { get { return _price; } set { _price = Price; } }
    }
    class Potion : Item
    {
        //getPotion(ID) 를 입력하면, 해당 ID에 있는 Potion을 Player의 Inventory에 넣고) 만들면 Dic에서 찾아서
        // itemID랑 사이즈로
        public PotionEffect _pEffect;
        private int _value;
        
        public int Value { get { return _value; } set { _value = Value; } }

        public Potion(string id, string name, int value, PotionEffect pEffect, int price)
        {
            itemID = id;
            _name = name;
            _value = value;
            _pEffect = pEffect;
            _price = price;
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
        public EquipItem(string itemid, string wName, EquipType type, Stat stat, int price)
        {
            itemID = itemid;
            _name = wName;
            _type = type;
            ItemStat = stat;
            _price = price;
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
                { "P_0001", new Potion("P_0001", "레드 포션", 10, PotionEffect.Heal, 10) },
                { "P_0002", new Potion("P_0002", "블루 포션", 10, PotionEffect.Heal, 20) },
                { "P_0003", new Potion("P_0003", "하이 레드 포션", 50, PotionEffect.Heal, 50) },
                { "P_0004", new Potion("P_0004", "하이 블루 포션", 50, PotionEffect.Heal, 100) },
                { "P_0005", new Potion("P_0005", "공격의 물약", 3, PotionEffect.ATK_Up, 20) },
                { "P_0006", new Potion("P_0006", "방어의 물약", 3, PotionEffect.DEF_Up, 20)},
                { "P_0007", new Potion("P_0007", "속도의 물약", 3, PotionEffect.SPD_Up, 20)},
            };
            _equips = new Dictionary<string, EquipItem>
            {
                { "W_0001", new EquipItem("W_0001","뗀석기", EquipType.Weapon, new Stat(0, 1, 0, 0), 0)}         ,
                { "W_0002", new EquipItem("W_0002","청동검", EquipType.Weapon, new Stat(0, 10, 0, 0), 10)}        ,
                { "W_0003", new EquipItem("W_0003","철검", EquipType.Weapon, new Stat(0, 20, 0, 0), 20)}          ,
                { "W_0004", new EquipItem("W_0004","빔샤벨", EquipType.Weapon, new Stat(0, 100, 0, 0), 1000)}       ,
                { "H_0001", new EquipItem("H_0001","그릇", EquipType.Head, new Stat(0, 0, 5, 0), 0) }            ,
                { "H_0002", new EquipItem("H_0002","비니", EquipType.Head, new Stat(0, 0, 10, 0), 20)}            ,
                { "H_0003", new EquipItem("H_0003","투구", EquipType.Head, new Stat(0, 0, 20, -5), 100)}           ,
                { "B_0001", new EquipItem("B_0001","거적떼기", EquipType.Body, new Stat(0, 0, 5, 0), 0)}         ,
                { "B_0002", new EquipItem("B_0002","철기사갑옷", EquipType.Body, new Stat(0, 0, 100, -10), 100)}   ,
                { "S_0001", new EquipItem("S_0001","짚신", EquipType.Shoes, new Stat(0, 0, 1, 5), 0)}            ,
                { "S_0002", new EquipItem("S_0002","아디다스", EquipType.Shoes, new Stat(0, 0, 5, 20), 300)}
            };
        }
    }
}