using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ChaineVerletSrc : MonoBehaviour
{
    private LineRenderer lineRenderer;
    private List<RopeSegment> ropeSeg = new List<RopeSegment>();
    private float ropeSegLen = 0.01f;
    private int segmentLength = 30;
    private float lineWidth = 0.15f;
    List<Vector2> colliderPoints = new List<Vector2>();
    private GameObject P1;
    private GameObject P2;
    PolygonCollider2D polygonCollider2D;

    //The Line Manager Class
    LineController lc;
    private List<Vector2> CalculateColliderPoints(List<Vector2> positions)
    {
        //Get The Width of the Line
        float width = lc.GetWidth();

        // m = (y2 - y1) / (x2 - x1)
        float m = (positions[1].y - positions[0].y) / (positions[1].x - positions[0].x);
        float deltaX = (width / 2f) * (m / Mathf.Pow(m * m + 1, 0.5f));
        float deltaY = (width / 2f) * (1 / Mathf.Pow(1 + m * m, 0.5f));

        //Calculate Vertex Offset from Line Point
        Vector2[] offsets = new Vector2[2];
        offsets[0] = new Vector2(-deltaX, deltaY);
        offsets[1] = new Vector2(deltaX, -deltaY);

        List<Vector2> colliderPoints = new List<Vector2> {
            positions[0] + offsets[0],
            positions[1] + offsets[0],
            positions[1] + offsets[1],
            positions[0] + offsets[1]
        };
        return colliderPoints;
    }
    private void Start()
    {
        P1 = GameObject.FindGameObjectWithTag("Player1");
        P2 = GameObject.FindGameObjectWithTag("Player2");
        polygonCollider2D = GetComponent<PolygonCollider2D>();
        lc = GetComponent<LineController>();
        this.lineRenderer = this.GetComponent<LineRenderer>();
        Vector3 ropeStartPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        for (int i = 0; i < segmentLength; i++)
        {
            this.ropeSeg.Add(new RopeSegment(ropeStartPoint));
            ropeStartPoint.y -= ropeSegLen;
        } 
    }
    private void Update()
    {
        if(colliderPoints != null)
        {
            polygonCollider2D.SetPath(0, colliderPoints.ConvertAll(p => (Vector2)transform.InverseTransformPoint(p)));
        }
        this.DrawRope();
        float distance = Vector2.Distance(P1.transform.position,P2.transform.position);
        lineRenderer.material.SetTextureScale("_MainTex", new Vector2(distance * 2, 1));
    }
    private void LateUpdate()
    {
        //Get all the positions from the line renderer
        var pos = new List<Vector3>();
        //Get All positions on the line renderer
        if (lineRenderer.positionCount == segmentLength)
        {

            for (int i = 0; i < segmentLength - 1; i++)
            {
                pos.Add(lineRenderer.GetPosition(i));
            }
        }
        else
        {
            return;
        }
        Vector3[] positions = pos.ToArray();

        //If we have enough points to draw a line
        if (positions.Count() >= 2)
        {

            //Get the number of line between two points
            int numberOfLines = positions.Length - 1;

            //Make as many paths for each different line as we have lines
            polygonCollider2D.pathCount = numberOfLines;

            //Get Collider points between two consecutive points
            for (int i = 0; i < numberOfLines; i++)
            {
                //Get the two next points
                List<Vector2> currentPositions = new List<Vector2> {
                    positions[i],
                    positions[i+1]
                };

                List<Vector2> currentColliderPoints = CalculateColliderPoints(currentPositions);
                polygonCollider2D.SetPath(i, currentColliderPoints.ConvertAll(p => (Vector2)transform.InverseTransformPoint(p)));
                
            }
        }
        else
        {

            polygonCollider2D.pathCount = 0;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        print(collision.gameObject);
    }
    private void DrawRope()
    {
        float lineWith = this.lineWidth;
        lineRenderer.startWidth = lineWidth;
        lineRenderer.endWidth = lineWidth;

        Vector3[] ropePosition = new Vector3[this.segmentLength];
        for (int i = 0; i < this.segmentLength; i++)
        {
            ropePosition[i] = this.ropeSeg[i].posNow;
        }
        lineRenderer.positionCount = ropePosition.Length;
        lineRenderer.SetPositions(ropePosition);
    }
    private void FixedUpdate()
    {
        this.Simulate();
    }
    private void Simulate()
    { 
        //SIMULATION
        Vector2 forceGravity = new Vector2(0, -10);
        for (int i = 0; i < segmentLength; i++)
        {
            var firstSeg = this.ropeSeg[i];
            Vector2 velocity = firstSeg.posNow - firstSeg.posOld;
            firstSeg.posOld = firstSeg.posNow;
            firstSeg.posNow += forceGravity * Time.deltaTime;
            this.ropeSeg[i] = firstSeg;
        }
        //CONSTRAINTS
        for (int i = 0; i < 50; i++)
        {
            this.ApplyConstraints();
        }
    }

    private void ApplyConstraints()
    {
        RopeSegment firstSegment = this.ropeSeg[0];
        RopeSegment lastSegment = this.ropeSeg[segmentLength-1];


        lastSegment.posNow = GameObject.FindGameObjectWithTag("Player2").transform.position;

        firstSegment.posNow = GameObject.FindGameObjectWithTag("Player1").transform.position;
        
        this.ropeSeg[0] = firstSegment;
        this.ropeSeg[segmentLength - 1] = lastSegment;

        for (int i = 0; i < this.segmentLength - 1; i++)
        {
            RopeSegment firstSeg = this.ropeSeg[i];
            RopeSegment secondSeg = this.ropeSeg[i + 1];

            float dist = (firstSeg.posNow - secondSeg.posNow).magnitude;
            float error = Mathf.Abs(dist - this.ropeSegLen);
            Vector2 changeDir = Vector2.zero;

            if (dist > ropeSegLen)
            {
                changeDir = (firstSeg.posNow - secondSeg.posNow).normalized;
            }
            else if (dist < ropeSegLen)
            {
                changeDir = (secondSeg.posNow - firstSeg.posNow).normalized;
            }

            Vector2 changeAmount = changeDir * error;
            if (i != 0)
            {
                firstSeg.posNow -= changeAmount * 0.5f;
                this.ropeSeg[i] = firstSeg;
                secondSeg.posNow += changeAmount * 0.5f;
                this.ropeSeg[i + 1] = secondSeg;
            }
            else
            {
                secondSeg.posNow += changeAmount;
                this.ropeSeg[i + 1] = secondSeg;
            }
        }
       
    }
}

public struct RopeSegment
{
    public Vector2 posNow;
    public Vector2 posOld;

    public RopeSegment(Vector2 pos)
    {
        this.posNow = pos;
        this.posOld = pos;
    }
}
