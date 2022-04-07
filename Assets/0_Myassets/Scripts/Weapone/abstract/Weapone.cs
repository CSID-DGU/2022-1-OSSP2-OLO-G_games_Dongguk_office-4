using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeStage.AntiCheat.ObscuredTypes;

[RequireComponent(typeof(AudioSource))]
public abstract class Weapone : MonoBehaviour
{
    public AudioClip fireSound;
    bool isFireOkay;//���Ⱑ ��� ���� ��������
    protected ObscuredInt damage;//���ݷ�
    protected ObscuredFloat fireRate;//���� �ӵ�
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
        //3�ʸ��� �޸� �� ����(ġ�� ����)
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
    protected abstract void UpdateFunc();//������Ʈ ȣ�� �� �� �� ����
    protected abstract void SetValues();//���� �� ����(������, �߻�ӵ� ��)

    protected abstract void Fire();//���⸦ �̿������� ���� ����(���콺 ��Ŭ��)
    void PlayFireSound()
    {
        this.GetComponent<AudioSource>().clip = fireSound;
        this.GetComponent<AudioSource>().Play();
    }
    
}
