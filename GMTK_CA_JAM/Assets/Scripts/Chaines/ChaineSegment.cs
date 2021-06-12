using UnityEngine;

public class ChaineSegment : MonoBehaviour
{
    public GameObject connectedAbove, connectedBellow;

    void Start()
    {
        connectedAbove = GetComponent<HingeJoint2D>().connectedBody.gameObject;
        var aboveSegment = connectedAbove.GetComponent<ChaineSegment>();
        if (aboveSegment != null)
        {
            aboveSegment.connectedBellow = gameObject;
            var spriteBottomSize = connectedAbove.GetComponent<SpriteRenderer>().bounds.size.y;
            print(spriteBottomSize);
            GetComponent<HingeJoint2D>().connectedAnchor = new Vector2(0, -spriteBottomSize);
        }
        else
        {
            GetComponent<HingeJoint2D>().connectedAnchor = new Vector2(0, 0);
        }
        
    }

}
