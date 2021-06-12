using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform targetToFollow;
    [SerializeField] private float offsetCamera=3f;

    private void Update()
    {
        transform.position = new Vector3(transform.position.x, targetToFollow.position.y+offsetCamera, transform.position.z);

        if (shakeTimeRemaining > 0 && isShaking) GlobalShake();
    }

   


    private float shakeTimeRemaining;
    private float shakePower;
    private float shakeFadeTime;
    private float shakeRotation;
    private bool isShaking;
    private float rotationMultiplier;



    public void StartShakeG(float length, float power, float rotationMulti)
    {
        shakeTimeRemaining = length;
        shakePower = power;
        rotationMultiplier = rotationMulti;

        shakeFadeTime = power / length;

        shakeRotation = power * rotationMultiplier;


        isShaking = true;
    }

    private void GlobalShake()
    {
        shakeTimeRemaining -= Time.deltaTime;

        //float x = Random.Range(-1f, 1f) * shakePower;
        float y = Random.Range(0.3f,1f) * shakePower;

        transform.position = transform.position + new Vector3(0, y);
        //transform.rotation = Quaternion.Euler(0f, 0f, shakeRotation * Random.Range(-1f, 1f));

        //shakeRotation = Mathf.MoveTowards(shakeRotation, 0f, shakeFadeTime * rotationMultiplier * Time.deltaTime);

        if (shakeTimeRemaining <= 0)
        {
            transform.rotation = Quaternion.identity;
            isShaking = false;
            
        }
    }


}
