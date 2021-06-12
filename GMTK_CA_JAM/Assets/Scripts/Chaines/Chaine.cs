using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chaine : MonoBehaviour
{
    public Rigidbody2D hook1;
    public Rigidbody2D hook2;
    public GameObject[] prefabsChaineSegments;
    public int numLinks = 5;
    // Start is called before the first frame update
    void Start()
    {
        GenerateChaine();
    }

    private void GenerateChaine()
    {
        var prevBody = hook1;
        for(int i=0; i< numLinks; i++)
        {
            int index = Random.Range(0, prefabsChaineSegments.Length);
            var newSegment = Instantiate(prefabsChaineSegments[index]);
            newSegment.transform.parent = transform;
            newSegment.transform.position = transform.position;
            var hj = newSegment.GetComponent<HingeJoint2D>();
            hj.connectedBody = prevBody;
            prevBody = newSegment.GetComponent<Rigidbody2D>();
        }
        var secondJoin = prevBody.gameObject.AddComponent(typeof(HingeJoint2D)) as HingeJoint2D;
        secondJoin.autoConfigureConnectedAnchor = false;
        secondJoin.connectedBody = hook2;
        secondJoin.anchor = new Vector2(0, -prevBody.GetComponent<Collider2D>().bounds.size.y);
        //secondJoin.connectedAnchor = new Vector2(0, -prevBody.GetComponent<Collider2D>().bounds.size.y);
    }


}


