using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

namespace ActiveCode.CH
{
    public class Ghast : Monster
    {
        // 따라갈 Target
        public GameObject target;
        public float speed = 5f;

        protected Rigidbody2D rb;
        protected SpriteRenderer sr;
        protected PhotonView pv;

        protected override void Awake()
        {
            base.Awake();

            this.rb = this.GetComponent<Rigidbody2D>();
            this.sr = this.GetComponent<SpriteRenderer>();
            this.pv = this.GetComponent<PhotonView>();
        }

        protected override void Update()
        {
            base.Update();

            if (this.target)
            {
                this.MoveToTarget();
            }
            else
            {
                //this.target = GameObject.FindGameObjectWithTag("Character"); // footcolider 잡힘
                Character c = GameObject.FindObjectOfType<Character>();
                if (c)
                {
                    this.target = c.gameObject;
                }
            }

            //Debug.Log($"HP: {this.status.hp} / Defense: {this.status.defense} / Damage: {this.status.damage}");
        }

        protected void MoveToTarget()
        {
            Vector2 dir = (this.target.transform.position - this.transform.position).normalized;
            Vector2 velocity = this.speed * Time.deltaTime * dir;

            this.pv.RPC("FlipX", RpcTarget.AllBuffered, dir.x > 0);

            this.rb.MovePosition(this.rb.position + velocity);
        }

        [PunRPC]
        protected void FlipX(bool flipX)
        {
            this.sr.flipX = flipX;
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
            this.status.hp -= 10;
        }
    }
}