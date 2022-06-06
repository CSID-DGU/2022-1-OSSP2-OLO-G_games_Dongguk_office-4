using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    // Start is called before the first frame update

    public int nowhp = 100;
    public GameObject Character;
    public GameObject hand;
    public static CharacterController instance;


    int left, right, up, down;//???? ???? ????
    Vector3 characterMovePos;
    void Start()
    {
        
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        CharacterMoveController();
        handRotationController();
    }
    void CharacterMoveController()
    {
        left = 0; right = 0; up = 0; down = 0;


        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("누름");
            Character.GetComponent<Animator>().SetBool("isDash", true);
        }
        Character.GetComponent<Animator>().SetBool("isDash", false);

        if (Input.GetKey(KeyCode.A))
        {
            left = -1;
        }
        if (Input.GetKey(KeyCode.D))
        {
            right = 1;
        }
        if (Input.GetKey(KeyCode.W))
        {
            up = 1;
        }
        if (Input.GetKey(KeyCode.S))
        {
            down = -1;
        }


        if (Camera.main.ScreenToWorldPoint(Input.mousePosition).x > Character.transform.position.x)
        {
            offset = 0;
            Character.transform.localScale = new Vector3(1, 1, 1);
            if ((left + right) > 0)
            {
                Character.GetComponent<Animator>().SetFloat("moveSpeed", 1.0f);
            }
            else if ((left + right) < 0)
            {
                Character.GetComponent<Animator>().SetFloat("moveSpeed", -1.0f);
            }
        }
        else if (Camera.main.ScreenToWorldPoint(Input.mousePosition).x < Character.transform.position.x)
        {
            offset = 180;
            Character.transform.localScale = new Vector3(-1, 1, 1);
            if ((left + right) > 0)
            {
                Character.GetComponent<Animator>().SetFloat("moveSpeed", -1.0f);
            }
            else if ((left + right) < 0)
            {
                Character.GetComponent<Animator>().SetFloat("moveSpeed", 1.0f);
            }
        }

        characterMovePos = new Vector3(left + right, up + down, 0).normalized;
        if (characterMovePos != Vector3.zero)
        {
            Character.GetComponent<Animator>().SetBool("isMove", true);
        }
        else
        {
            Character.GetComponent<Animator>().SetBool("isMove", false);
        }
        Character.transform.Translate(2.0f * Time.fixedDeltaTime * characterMovePos);


    }
    float offset = 0f;
    void handRotationController()
    {
        //hand.transform.LookAt(Camera.main.ScreenToWorldPoint(Input.mousePosition),new Vector3(0,0,1));
        RotateTowards(Camera.main.ScreenToWorldPoint(Input.mousePosition));
    }
    private void RotateTowards(Vector2 target)
    {
        Vector2 direction = (target - (Vector2)hand.transform.position).normalized;
        var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        hand.transform.rotation = Quaternion.Euler(Vector3.forward * (angle + offset));
    }
}
