using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BG_Scroller : MonoBehaviour
{
    private MeshRenderer render; //MeshRenderer ���� ����
    private float offset; 
    public float speed; //�ӵ� ������ ���� ����

    // Start is called before the first frame update
    void Start()
    {
        render = GetComponent<MeshRenderer>(); //MeshRenderer �ʱ�ȭ
    }

    // Update is called once per frame
    void Update()
    {
        offset += Time.deltaTime * speed; // �ӵ� ����
        render.material.mainTextureOffset = new Vector2(offset, 0); //��Ƽ������ offest ����
    }
}
