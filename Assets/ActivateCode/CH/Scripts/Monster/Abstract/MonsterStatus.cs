using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ActiveCode.CH
{
    [System.Serializable]
    public class MonsterStatus
    {
        // HP
        [HideInInspector]
        public int hp;
        [HideInInspector]
        public int maxHp;

        public int defaultHp;
        [Range(0f, 1f)]
        public float hpRandomness;

        // Damage
        [HideInInspector]
        public int damage;

        public int defaultDamage;
        [Range(0f, 1f)]
        public float damageRandomness;
        
        // Defense
        [HideInInspector]
        public float defense;

        [Range(0f, 1f)]
        public float defaultDefense;
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
            (float min, float max) = this.CalculateMinMax(this.defaultDefense, this.defenseRandomness);

            this.defense = Random.Range(min, max);
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
