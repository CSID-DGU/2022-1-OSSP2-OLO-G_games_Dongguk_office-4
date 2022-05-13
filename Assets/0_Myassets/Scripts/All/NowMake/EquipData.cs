using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType { Weapon=0,Head=10,Amor=11,Ring = 20}
[System.Serializable]
public class EquipData : Item
{
    public ItemType itemType;


    public bool isNowEquip;


    public int reinforcedCount=0;//강화단계
    public int weaponBaseDamage=0;//무기 기본 공격력;
    public float weaponBaseAtkSpeed=0;//무기 기본 공속;

    public int addHp=0;
    public int addMp=0;
    public int addStr=0;
    public int addDex=0;
    public int addIntelligent=0;
    public int addLuck=0;
    public int addAtk=0;// 물공 (무기와 다른스탯, 최종 합산임)
    public int addMagic=0;// 마공 (무기와 다른스탯, 최종 합산임)

    public bool getIsNowEquip()
    {
        return isNowEquip;
    }

}
