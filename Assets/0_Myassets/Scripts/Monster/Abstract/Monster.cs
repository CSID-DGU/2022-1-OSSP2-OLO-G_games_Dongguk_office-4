using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.AI;

namespace ActiveCode.CH
{
    public abstract class Monster : MonoBehaviourPun
    {

        
        public Transform spawnerPosition;

        // 몬스터 Status (inspector에서 스탯 범위 설정)
        public MonsterStatus status;

        // 따라갈 Target
        public GameObject target;

        NavMeshAgent nav;        
        
        

        protected virtual void Awake()
        {
            // Status 초기화
            status.Init();
            nav = GetComponent<NavMeshAgent>();
            nav.updateRotation = false;
            nav.updateUpAxis = false;
            
        }
        private void Start()
        {
           
            
            

        }

        protected virtual void Update()
        {

            if (!photonView.IsMine) return;
            //캐릭터 검색해서 배열 받은다음 linq써서 가까운거 타겟으로 잡고, 스포너 위치 기준으로 거리가 일정이상 멀어지면
            //다시 원위치로 이동
            //if(Vector3.Distance(spawnerPosition.position,target.transform.position))
            if (!target)
            {
                target = target = GameObject.FindGameObjectWithTag("Character");
            }
            if (nav.destination != target.transform.position)
            {
                nav.SetDestination(target.transform.position);
            }
            else
            {
                nav.SetDestination(transform.position);
            }
            //스포너 위치 기준으로 탐색

            // 죽음 처리 (OnHit에서 ?)
            if (this.status.hp <= 0)
            {
                this.OnDie();
            }
        }

        public void DecreaseHp(int damage)
        {
            if (damage > 0)
            {
                status.hp -= damage;
            }

            if (status.hp <= 0)
            {
                OnDie();
            }
            
        }

        protected virtual void OnTriggerEnter2D(Collider2D collision)
        {
            
            // 플레이어 공격 collider랑 부딪히면
            //if (collision.CompareTag("playerAttackHitbox"))
            if (collision.tag == "CharacterHitBox") // 일단 master랑 tag 겹침 방지용
            {
                Debug.Log("캐릭터와 충돌");
                OnTriggerEnterWithCharacterBehaviour(collision);
               
            }
        }

        protected abstract void OnTriggerEnterWithCharacterBehaviour(Collider2D collision);

        // 플레이어 공격에 맞았을 때
        protected abstract void OnHit(GameObject weaponObject);
        // 죽었을 때
        protected abstract void OnDie();
    }
}