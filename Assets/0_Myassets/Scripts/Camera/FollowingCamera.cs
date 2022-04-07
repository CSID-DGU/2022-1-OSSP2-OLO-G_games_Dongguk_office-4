using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowingCamera : MonoBehaviour
{
    // target object
    Transform target;
    // smooth following duration
    public float duration = 0.3f;

    // calculated camera velocity by SmoothDamp
    private Vector3 velocity = Vector3.zero;
    // camera z position
    private float zPos;
  

    private void Awake()
    {
        
    }
    private void Start()
    {
        foreach (var i in GameObject.FindGameObjectsWithTag("Character"))
        {
            if (i.GetComponent<Photon.Pun.PhotonView>().IsMine)
            {
                target = i.transform;
                return;
            }
        }
        // init camera z position
        zPos = transform.position.z;
    }


    void FixedUpdate()
    {
                // Define a target position above and behind the target transform
        Vector3 targetPosition = target.TransformPoint(new Vector3(0, 0, -10));

        // Smoothly move the camera towards that target position
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, duration);
    }
}
