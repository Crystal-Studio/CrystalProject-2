using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GM_Characters
{
    public List<GM_Character> characters;
}

[System.Serializable]
public class GM_Character
{
    public int id;
    public string characterName;
    public int health;
    public int action;
    public int power;
    public int armor;
    public int initiative;
    public int precision;
    public int movement;

    public List<int> items;
}

[System.Serializable]
public class GM_ItemPlayer
{
    public int id;
    public string itemName;
    public string description;
    public int health;
    public int action;
    public int power;
    public int armor;
    public int initiative;
    public int precision;
    public int movement;
}

[System.Serializable]
public class GM_SpellPlayer
{
    public int id;
    public string spellName;
    public string description;
    public int cost;
    public int power;
    public int precision;
    public int type;
    public int[] mastery;
    public int[] keyWord;
    public int zone;
    public bool cast;
    public int distance;
}

[System.Serializable]
public class GM_Mastery
{
    public int id;
    public string masteryName;
    public int[] keyWord;
    public int health;
    public int action;
    public int power;
    public int armor;
    public int initiative;
    public int precision;
    public int movement;
}

[System.Serializable]
public class GM_KeyWord
{
    public int id;
    public string keyWordName;
    public int time;
}
