using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeStage.AntiCheat.ObscuredTypes;

[RequireComponent(typeof(AudioSource))]
public abstract class Weapone : MonoBehaviour
{
    public AudioClip fireSound;
    bool isFireOkay;//무기가 사용 가능 상태인지
    protected ObscuredInt damage;//공격력
    protected ObscuredFloat fireRate;//연사 속도
    float fireRateCount;

    void Start()
    {
        this.GetComponent<AudioSource>().loop = false;
        SetValues();
        RandomizeKey();
        isFireOkay = true;
       


    }
    void RandomizeKey()
    {
        //3초마다 메모리 값 변경(치팅 방지)
        damage.RandomizeCryptoKey();
        fireRate.RandomizeCryptoKey();
        Invoke("RandomizeKey", 3);
    }
    // Update is called once per frame
    void Update()
    {
        if (isFireOkay&&Input.GetMouseButton(0)&&fireRateCount>fireRate)
        {
            Fire();
            PlayFireSound();
            fireRateCount = 0;
        }
        fireRateCount += Time.deltaTime;
        UpdateFunc();
    }
    protected abstract void UpdateFunc();//업데이트 호출 시 할 일 지정
    protected abstract void SetValues();//무기 값 설정(데미지, 발사속도 등)

    protected abstract void Fire();//무기를 이용했을때 취할 동작(마우스 좌클릭)
    void PlayFireSound()
    {
        this.GetComponent<AudioSource>().clip = fireSound;
        this.GetComponent<AudioSource>().Play();
    }
    
}
