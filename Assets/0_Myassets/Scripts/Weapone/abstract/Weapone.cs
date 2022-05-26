using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeStage.AntiCheat.ObscuredTypes;
using Photon.Pun;

[RequireComponent(typeof(AudioSource))]
public abstract class Weapone : MonoBehaviourPun
{
    [HideInInspector]
    public EquipData equipData;

    public AudioClip fireSound;
    int itemReinforcedStack;
    bool isFireOkay;//?????? ???? ???? ????????
    protected ObscuredInt damage;//??????
    protected ObscuredFloat fireRate;//???? ????
    float fireRateCount;

    public bool isOnGround;

    

    void Start()
    {
        this.GetComponent<AudioSource>().loop = false;
        SetValues();
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
            PlayFireSound();
            fireRateCount = 0;
        }
        fireRateCount += Time.deltaTime;
        UpdateFunc();
    }
    protected abstract void UpdateFunc();//???????? ???? ?? ?? ?? ????
    protected abstract void SetValues();//???? ?? ????(??????, ???????? ??)

    protected abstract void Fire();//?????? ?????????? ???? ????(?????? ??????)
    void PlayFireSound()
    {
        this.GetComponent<AudioSource>().clip = fireSound;
        this.GetComponent<AudioSource>().Play();
    }

    public void Atk(Character characterSC)
    {
        
    }
    
}
