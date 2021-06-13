using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform targetToFollow;
    [SerializeField] private float offsetCamera=3f;

    [SerializeField] private Transform bg1;
    [SerializeField] private Transform bg2;
    [SerializeField] private float size; // Bg transform.localScale.y

    private void Update()
    {
        transform.position = new Vector3(0, targetToFollow.position.y+offsetCamera, transform.position.z);

        if (shakeTimeRemaining > 0 && isShaking) GlobalShake();
    }

    private void FixedUpdate()
    {
        if(transform.position.y >= bg2.position.y)
        {
            bg1.position = new Vector3(bg1.position.x, bg2.position.y + size, bg1.position.z);
            SwitchBg();
        }
    }

    private void SwitchBg()
    {
        Transform temp = bg1;
        bg1 = bg2;
        bg2 = temp;
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

        float x = Random.Range(-1f, 1f) * shakePower;
        float y = Random.Range(-1f,1f) * shakePower;

        transform.position = transform.position + new Vector3(x, y);
        transform.rotation = Quaternion.Euler(0f, 0f, shakeRotation * Random.Range(-1f, 1f));

        shakePower = Mathf.MoveTowards(shakePower, 0f, shakeFadeTime * Time.deltaTime);
        shakeRotation = Mathf.MoveTowards(shakeRotation, 0f, shakeFadeTime * rotationMultiplier * Time.deltaTime);

        if (shakeTimeRemaining <= 0)
        {
            transform.rotation = Quaternion.identity;
            isShaking = false;
            
        }
    }


}
