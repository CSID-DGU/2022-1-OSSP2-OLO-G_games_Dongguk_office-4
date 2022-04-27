using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Monster : MonoBehaviour
{
    // -----------------------------------

    // HP
    [Range(0f, 1f)]
    public float hpRandomness = 0f;
    public int defaultHp = 100;

    protected int maxHp;
    protected int curHp;

    // -----------------------------------

    // attack damage
    [Range(0f, 1f)]
    public float damageRandomness = 0f;
    public int defaultDamage = 10;

    protected int damage;

    // -----------------------------------

    // defense (dodge rate)
    [Range(0f, 1f)]
    public float defenseRandomness = 0f;
    [Range(0f, 1f)]
    public float defaultDefense = 0.1f;

    protected float defense;

    // -----------------------------------

    protected void Awake()
    {
        // init HP randomly
        int hpBoundMin = (int)(this.defaultHp * (1 - this.hpRandomness));
        int hpBoundMax = (int)(this.defaultHp * (1 + this.hpRandomness));
        this.maxHp = Random.Range(hpBoundMin, hpBoundMax);
        this.curHp = this.maxHp;

        // init damage randomly
        int damageBoundMin = (int)(this.defaultDamage * (1 - this.damageRandomness));
        int damageBoundMax = (int)(this.defaultDamage * (1 + this.damageRandomness));
        this.damage = Random.Range(damageBoundMin, damageBoundMax);

        // init defense randomly
        float defenseBoundMin = this.defaultDefense * (1 - this.defenseRandomness);
        float defenseBoundMax = this.defaultDefense * (1 + this.defenseRandomness);
        this.defense = Random.Range(defenseBoundMin, defenseBoundMax);
    }

    protected void OnCollisionEnter2D(Collision2D collision)
    {
        // if player hitbox (or bullet)
        if (collision.collider.CompareTag("playerAttackHitbox"))
        {
            // check whether dodge or hit
            // [TODO] consider player ¸íÁß·ü
            float dice = Random.Range(0f, 1f);
            if (dice > this.defense)
            {
                this.OnHit(collision.gameObject);
            }
        }
    }

    protected abstract void OnHit(GameObject weaponObject);
}
