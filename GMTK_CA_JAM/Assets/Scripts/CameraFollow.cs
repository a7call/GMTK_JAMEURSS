using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform targetToFollow;

    private void Update()
    {
        transform.position = new Vector3(0, targetToFollow.position.y, transform.position.z);
    }
}
