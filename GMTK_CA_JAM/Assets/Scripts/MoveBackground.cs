using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBackground : MonoBehaviour
{
    [SerializeField] private Transform centerBackground;
    [SerializeField] private float offset;

    private void Update()
    {
        if (transform.position.y >= centerBackground.position.y + offset)
            centerBackground.position = new Vector2(centerBackground.position.x, transform.position.y + offset);

        else if (transform.position.y <= centerBackground.position.y - offset)
            centerBackground.position = new Vector2(centerBackground.position.x, transform.position.y - offset);
    }
}
