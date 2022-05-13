using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class WeaponStat
{
    public int equipID;//같은 장비 획득 시 구분용

    [SerializeField]
    int baseAtk;//공격력
    float baseAtkSpeed;//공격 속도
    public int reinfoceCount;//강화상태
    public int atkIncreasePoint;//강화 1당 공격력 증가
    public float atkSpeedIncreateRate;//강화 1당 공격속도 증가율

   

    public int getFinalAtk()
    {
        //강화 수치가 반영된 무기 공격력
        return baseAtk + reinfoceCount * atkIncreasePoint;
    }
    public float getFinalAtkSpeed()
    {
        //강화 수치가 반영된 무기 공격속도
        return baseAtkSpeed * Mathf.Pow((1-atkSpeedIncreateRate),reinfoceCount);
    }
    
   
}
