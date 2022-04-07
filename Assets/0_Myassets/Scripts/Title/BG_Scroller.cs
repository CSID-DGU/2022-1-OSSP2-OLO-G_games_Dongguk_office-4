using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BG_Scroller : MonoBehaviour
{
    private MeshRenderer render; //MeshRenderer 변수 생성
    private float offset; 
    public float speed; //속도 조절을 위한 변수

    // Start is called before the first frame update
    void Start()
    {
        render = GetComponent<MeshRenderer>(); //MeshRenderer 초기화
    }

    // Update is called once per frame
    void Update()
    {
        offset += Time.deltaTime * speed; // 속도 조절
        render.material.mainTextureOffset = new Vector2(offset, 0); //머티리얼의 offest 변경
    }
}
