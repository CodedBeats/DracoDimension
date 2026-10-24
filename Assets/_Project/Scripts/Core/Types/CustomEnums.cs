// numbers are saved in assets: never change or reuse
public enum ItemCategory
{
    Weapon = 0,
    Armor = 1,
    Consumable = 2,
    Material = 3,
    Key = 4,
}

// numbers are saved in assets: never change or reuse (spaced by 10 so tiers can be slotted between)
public enum ItemRarity
{
    None = 0,
    Common = 10,
    Uncommon = 20,
    Rare = 30,
    Epic = 40,
    Legendary = 50,
    Mythic = 60,
    Dimensional = 70,
    Cosmic = 80
}

// numbers are saved in assets: never change or reuse
public enum EquipSlot
{
    Weapon = 0,
    Head = 1,
    Torso = 2,
    Shield = 3,
    Arms = 4,
    Legs = 5,
    Feet = 6,
    Accessory = 7,
}

// numbers are saved in assets: never change or reuse
public enum WeaponType
{
    None = 0,
    Sword = 1,
    Spear = 2,
    Knife = 3,
    Wand = 4,
    Whip = 5,
    Staff = 6,
    Claw = 7,
    Fan = 8,
    Hammer = 9,
    Axe = 10,
    Bow = 11,
    Boomerang = 12,
}

// numbers are saved in assets: never change or reuse
public enum Handedness
{
    OneHanded = 0,
    TwoHanded = 1,
}

// numbers are saved in assets: never change or reuse
public enum WeaponTargeting
{
    Single = 0,
    Group = 1,
    All = 2,
    AllDecreasing = 3,
}

// numbers are saved in assets: never change or reuse
public enum Element
{
    None = 0,
    Fire = 1,
    Ice = 2,
    Wind = 3,
    Earth = 4,
    Lightning = 5,
    Light = 6,
    Dark = 7,
    Water = 8,
    Nature = 9,
}

// numbers are saved in assets: never change or reuse
public enum StatType
{
    MaxHP = 0,
    MaxMP = 1,
    Strength = 2,
    Agility = 3,
    Resilience = 4,
    Wisdom = 5,
    Deftness = 6,
    Charm = 7,
    Attack = 8,
    Defense = 9,
    MagicalMight = 10,
    MagicalMending = 11,
    Evasion = 12,
    BlockChance = 13,
}

// numbers are saved in assets: never change or reuse
public enum StatusEffect
{
    Poison = 0,
    Sleep = 1,
    Paralysis = 2,
    Confusion = 3,
    Silence = 4,
    Fear = 5,
    Charm = 6,
    Curse = 7,
}

// numbers are saved in assets: never change or reuse
public enum UseContext
{
    Field = 0,
    Battle = 1,
    Both = 2,
}

// numbers are saved in assets: never change or reuse
public enum TargetType
{
    Self = 0,
    SingleAlly = 1,
    AllAllies = 2,
    SingleEnemy = 3,
    EnemyGroup = 4,
    AllEnemies = 5,
}

// numbers are saved in assets: never change or reuse
public enum MaterialGroup
{
    Ore = 0,
    Plant = 1,
    MonsterPart = 2,
    Gem = 3,
    Cloth = 4,
    Misc = 5,
}

// numbers are saved in assets: never change or reuse
public enum AbilityKind
{
    Spell = 0,
    Ability = 1,
    Passive = 2,
}

// numbers are saved in assets: never change or reuse
public enum DimensionLock
{
    None = 0,
    Restricted = 1,
    Bound = 2,
}

// numbers are saved in assets: never change or reuse
public enum SkillLineKind
{
    Weapon = 0,
    Shield = 1,
    Signature = 2,
    Affinity = 3,
}

// numbers are saved in assets: never change or reuse
public enum SettlementService
{
    Storage = 0,
    Inn = 1,
    Shop = 2,
    Recruitment = 3,
    VocationChange = 4,
}

// numbers are saved in assets: never change or reuse
public enum AttackDelivery
{
    Melee = 0,
    Reach = 1,
    Ranged = 2,
    Magic = 3,
    Seismic = 4,
}

// numbers are saved in assets: never change or reuse
public enum Presence
{
    Grounded = 0,
    Hovering = 1,
    Airborne = 2,
    Spectral = 3,
    Warded = 4,
    Burrowed = 5,
    Submerged = 6,
}