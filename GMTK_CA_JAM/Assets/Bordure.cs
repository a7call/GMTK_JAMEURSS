using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bordure : MonoBehaviour
{
    private Transform targetToFollow;
    // Start is called before the first frame update
    void Start()
    {
        targetToFollow = GameObject.FindGameObjectWithTag("Player1").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(0, targetToFollow.position.y, targetToFollow.position.z);
    }
}
