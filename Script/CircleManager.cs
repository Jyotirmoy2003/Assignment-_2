using UnityEngine;
using DG.Tweening;
using Unity.VisualScripting;
using System.Collections;
using System.Collections.Generic;

public class CircleManager : MonoBehaviour
{
    [SerializeField] GameObject circlePrefab;
    [SerializeField] GameEvent GameEndedEvent;
    private bool isDrawing = false;
    private Vector2 startPoint;
    private Vector2 endPoint;
    public int numberOfCirleLeftToDestroy=5;
    private int score=5;

    void Start()
    {

    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isDrawing = true;
            startPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
        else if (Input.GetMouseButtonUp(0))
        {
            isDrawing = false;
            endPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            CheckIntersectionAndRemoveCircles();
            score--;
        }
    }

    //fun to check which circle crosses the line
    private void CheckIntersectionAndRemoveCircles()
    {
        //go through all the circle
        foreach (Transform circleTransform in transform)
        {
            CircleCollider2D circleCollider = circleTransform.GetComponent<CircleCollider2D>();
            //checks if the bounding box (AABB) intersects with the set of frustum planes.
            if (GeometryUtility.TestPlanesAABB(GeometryUtility.CalculateFrustumPlanes(Camera.main), circleCollider.bounds))
            {
                //check if line intersect
                if (DoLinesIntersect(startPoint, endPoint, circleTransform.position, circleCollider.radius))
                {
                    //fade out those circless
                    FadeAndDestroyCircle(circleTransform);
                }
            }
        }
    }

private bool DoLinesIntersect(Vector2 p1, Vector2 p2, Vector2 circleCenter, float circleRadius)
{
    Vector2 d = p2 - p1; // Direction vector of the line
    Vector2 f = p1 - circleCenter; // Vector from the circle's center to the line's start point

    float a = Vector2.Dot(d, d);
    float b = 2 * Vector2.Dot(f, d);
    float c = Vector2.Dot(f, f) - circleRadius * circleRadius;

    float discriminant = b * b - 4 * a * c;
    if (discriminant < 0)
    {
        // No intersection
        return false;
    }
    else
    {
        discriminant = Mathf.Sqrt(discriminant);
        float t1 = (-b - discriminant) / (2 * a);
        float t2 = (-b + discriminant) / (2 * a);

        if ((t1 >= 0 && t1 <= 1) || (t2 >= 0 && t2 <= 1))
        {
            // Line intersects circle
            return true;
        }
        else
        {
            // Line does not intersect circle
            return false;
        }
    }
}


    private void FadeAndDestroyCircle(Transform circleTransform)
    {
        circleTransform.DOScale(0, 0.5f).OnComplete(() => Destroy(circleTransform.gameObject));
        numberOfCirleLeftToDestroy--;
        //if all circles are destroyed
        if(numberOfCirleLeftToDestroy<=0)
        {
            GameEndedEvent.Raise(this,score);
        }
    }

    //listen to event
    //number of circle selected in game
    public void GetNumberOfCirclesInGame(Component sender,object data)
    {
        if(data is int)
        {
            numberOfCirleLeftToDestroy=(int)data;
            score=numberOfCirleLeftToDestroy;
        }
    }
}
