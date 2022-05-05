using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ActiveCode.CH
{
    public abstract class Monster : MonoBehaviour
    {
        // 몬스터 Status (inspector에서 스탯 범위 설정)
        public MonsterStatus status;

        protected virtual void Awake()
        {
            // Status 초기화
            status.Init();
        }

        protected virtual void Update()
        {
            // 죽음 처리 (OnHit에서 ?)
            if (this.status.hp <= 0)
            {
                this.OnDie();
            }
        }

        protected virtual void OnTriggerEnter2D(Collider2D collision)
        {
            // 플레이어 공격 collider랑 부딪히면
            //if (collision.CompareTag("playerAttackHitbox"))
            if (collision.name == "PlayerAttackHitbox") // 일단 master랑 tag 겹침 방지용
            {
                // check whether dodge or hit
                // [TODO] consider player 명중률
                float dice = Random.Range(0f, 1f);
                if (dice > this.status.defense)
                {
                    this.OnHit(collision.gameObject);
                }
            }
        }

        // 플레이어 공격에 맞았을 때
        protected abstract void OnHit(GameObject weaponObject);
        // 죽었을 때
        protected abstract void OnDie();
    }
}