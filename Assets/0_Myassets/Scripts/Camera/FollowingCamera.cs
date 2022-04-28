using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowingCamera : MonoBehaviour
{
    public static FollowingCamera instance;
    // target object
    public Transform targetCharacterTransform;
    // smooth following duration
    public float duration = 0.3f;

    // calculated camera velocity by SmoothDamp
    private Vector3 velocity = Vector3.zero;
    // camera z position
    private float zPos;
  

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(instance.gameObject);
           
        }
        instance = this;
        
    }
    private void Start()
    {
        
        
        // init camera z position
        zPos = transform.position.z;
    }


    void FixedUpdate()
    {
        
        if (targetCharacterTransform != null)
        {
            Vector3 targetPosition = targetCharacterTransform.TransformPoint(new Vector3(0, 0, -10));

            // Smoothly move the camera towards that target position
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, duration);
            // Define a target position above and behind the target transform
        }


    }
}
