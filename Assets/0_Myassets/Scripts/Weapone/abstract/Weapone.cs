using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeStage.AntiCheat.ObscuredTypes;
using Photon.Pun;



[RequireComponent(typeof(AudioSource))]
public abstract class Weapone : MonoBehaviourPun
{

    public EquipData equipData;

    public AudioClip fireSound;
    int itemReinforcedStack;
    bool isFireOkay;//?????? ???? ???? ????????
    protected ObscuredInt damage;//??????
    public ObscuredFloat fireRate;//???? ????
    float fireRateCount;

    public bool isOnGround;
    public GameObject nowUsingCharacter;//무기를 사용중인 캐릭터

    protected virtual void Awake()
    {
        if (!photonView.IsMine)
        {
            return;
        }
    }
    protected virtual void Start()
    {
        if (!photonView.IsMine)
        {
            return;
        }
        this.GetComponent<AudioSource>().loop = false;
 
        RandomizeKey();
        isFireOkay = true;
       
       

    }
    void RandomizeKey()
    {
        //3?????? ?????? ?? ????(???? ????)
        damage.RandomizeCryptoKey();
        fireRate.RandomizeCryptoKey();
        Invoke("RandomizeKey", 3);
    }
    // Update is called once per frame
    protected virtual void Update()
    {

        if (!photonView.IsMine) return;
        if (isFireOkay&&Input.GetMouseButton(0)&&fireRateCount>fireRate)
        {
            Fire();
         
            fireRateCount = 0;
        }
        fireRateCount += Time.deltaTime;
        UpdateFunc();
    }
    protected abstract void UpdateFunc();//???????? ???? ?? ?? ?? ????
    

    protected abstract void Fire();//?????? ?????????? ???? ????(?????? ??????)
    public void PlayFireSound()
    {
        this.GetComponent<AudioSource>().clip = fireSound;
        this.GetComponent<AudioSource>().Play();
    }

    public void Atk(Character characterSC)
    {
        
    }
    
}
