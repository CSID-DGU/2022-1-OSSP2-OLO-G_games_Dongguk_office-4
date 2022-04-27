using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMonster : Monster
{
    protected void Update()
    {
        base.Update();

        Debug.Log($"HP: {this.curHp} / Defense: {this.defense} / Damage: {this.damage}");
    }

    protected override void OnDie()
    {
        Debug.Log($"{this.gameObject.name} 는 주금");

        Destroy(this.gameObject);
    }
    protected override void OnHit(GameObject weaponObject)
    {
        Debug.Log($"{this.gameObject.name} 는 {weaponObject.name} 한테 뚜드려 맞음");

        // [TODO] get weapon damage
        this.curHp -= 10;
    }
}
