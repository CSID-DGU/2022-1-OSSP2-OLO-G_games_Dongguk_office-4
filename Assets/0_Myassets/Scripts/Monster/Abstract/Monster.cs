using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.AI;

namespace ActiveCode.CH
{
    public abstract class Monster : MonoBehaviourPun
    {
        public int goldValue;
        public float canSearchDistance;
        public Transform spawnerPosition;

        // 몬스터 Status (inspector에서 스탯 범위 설정)
        public MonsterStatus status;

        // 따라갈 Target
        public GameObject target;

        NavMeshAgent nav;        
        
        

        protected virtual void Awake()
        {
    
            // Status 초기화
            //status.Init();
            nav = GetComponent<NavMeshAgent>();
            nav.updateRotation = false;
            nav.updateUpAxis = false;
            
        }
        private void Start()
        {
            if (!photonView.IsMine) return;
            target = spawnerPosition.gameObject;

         

        }

        protected virtual void Update()
        {

            if (!photonView.IsMine) return;
            
            //캐릭터 검색해서 배열 받은다음 linq써서 가까운거 타겟으로 잡고, 스포너 위치 기준으로 거리가 일정이상 멀어지면
            //다시 원위치로 이동
            //if(Vector3.Distance(spawnerPosition.position,target.transform.position))
            /*
            if (!target)
            {
                foreach(var i in GameObject.FindGameObjectsWithTag("Character"))
                {

                }
                target = GameObject.FindGameObjectWithTag("Character");
            }
            else
            {
                if(Vector2.Distance(target.transform.position,this.transform.position)<canSearchDistance)
                if (nav.destination != target.transform.position)
                {
                    nav.SetDestination(target.transform.position);
                }
                else
                {
                    nav.SetDestination(transform.position);
                }
            }

            */


       

            foreach (var i in GameObject.FindGameObjectsWithTag("Character"))
            {
                if (Vector2.Distance(i.transform.position, transform.position) < canSearchDistance&&!i.GetComponent<Character>().isDead)
                {
                    target = i;
                    
                }
            }

            if (target.tag == "Character")
            {
                if (target.GetComponent<Character>().isDead)
                {
                    //캐릭 죽으면 다시 제자리로 돌아가기
                    target = spawnerPosition.gameObject;
                }
            }

            nav.SetDestination(target.transform.position);
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
                Destroy(gameObject);
            }
            
        }
      
        protected virtual void OnTriggerEnter2D(Collider2D collision)
        {
            this.GetComponent<Rigidbody2D>().sleepMode = RigidbodySleepMode2D.NeverSleep;
    
          
            // 플레이어 공격 collider랑 부딪히면
            //if (collision.CompareTag("playerAttackHitbox"))

        }
        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.tag == "CharacterHitBox") // 일단 master랑 tag 겹침 방지용
            {
                atkTimeCount = 0;
            }
        }

        float atkTimeCount = 0;

        private void OnTriggerStay2D(Collider2D collision)
        {
      
            if (collision.tag == "CharacterHitBox") // 일단 master랑 tag 겹침 방지용
            {
               
                atkTimeCount -= Time.deltaTime;
                if (atkTimeCount < 0)
                {
                    atkTimeCount = 1;
                    Debug.Log("캐릭터와 충돌");
                    OnTriggerEnterWithCharacterBehaviour(collision);
                }
            }
        }
       

        protected abstract void OnTriggerEnterWithCharacterBehaviour(Collider2D collision);

        // 플레이어 공격에 맞았을 때
 
        // 죽었을 때
        protected virtual void OnDie() {
            int r = Random.Range(1, 3);
            if (r == 1)
            {
                // 1/2확률로 골드 드롭
                GameObject gold = Instantiate(Map_1_Manager.instance.goldPrefab) as GameObject;
                gold.GetComponent<DroppedGold>().goldAmount = goldValue;
                gold.transform.position = this.transform.position;
            }
            
        }
    }
}