using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ActiveCode.CH
{
    [System.Serializable]
    public class MonsterStatus
    {
        // HP

        public int hp;
        public int damage;   // Damage




        [HideInInspector]
        public int maxHp;

        public int defaultHp;
        [Range(0f, 1f)]
        public float hpRandomness;

     
     
       

        public int defaultDamage;
        [Range(0f, 1f)]
        public float damageRandomness;
        
        // Defense
        public int physicDefense;
        public int magicDefense;

        [Range(0f, 1f)]
        public float defaultPhysicDefense;

        [Range(0f, 1f)]
        public float defaultMagicDefense;


        [Range(0f, 1f)]
        public float defenseRandomness;

        // 스탯 초기화
        public void Init()
        {
            InitHp();
            InitDamage();
            InitDefense();
        }

        // HP 결정
        private void InitHp()
        {
            (int min, int max) = this.CalculateMinMax(this.defaultHp, this.hpRandomness);

            this.maxHp = Random.Range(min, max);
            this.hp = this.maxHp;
        }

        // Damage 결정
        private void InitDamage()
        {
            (int min, int max) = this.CalculateMinMax(this.defaultDamage, this.damageRandomness);

            this.damage = Random.Range(min, max);
        }

        // Defense 결정
        private void InitDefense()
        {
            (float minPhysic, float maxPhysic) = this.CalculateMinMax(this.defaultPhysicDefense, this.defenseRandomness);
            (float minMagic, float maxMagic) = this.CalculateMinMax(this.defaultMagicDefense, this.defenseRandomness);

            //this.physicDefense = Random.Range(minPhysic, maxPhysic);
            //this.magicDefense = Random.Range(minMagic, maxMagic);
        }

        // 범위 계산 (int)
        private (int, int) CalculateMinMax(int value, float randomness)
        {
            return (
                (int)(value * (1 - randomness)),
                (int)(value * (1 + randomness))
            );
        }

        // 범위 계산 (float)
        private (float, float) CalculateMinMax(float value, float randomness)
        {
            return (
                value * (1 - randomness),
                value * (1 + randomness)
            );
        }
    }
}
