using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleGenerator : MonoBehaviour
{
    [SerializeField] GameObject circlePrefab;
    [SerializeField] Transform parentObject;
    [Range(5,10)]
    [SerializeField] int numbreOfCircle=5;
    private Vector2 curretnPosition=new Vector2(0,0);
    private float maxX,maxY,minX,minY;

    void Start()
    {
        GetSceenData();


        SpawnRandomCircle();
    }

    

    void SpawnRandomCircle()
    {
        for(int i=0;i<numbreOfCircle;i++)
        {
        //generate random position
        curretnPosition.x=Random.Range(maxX,minX);
        curretnPosition.y=Random.Range(maxY,minY);
        //spawn Object and Set the parent
        Instantiate(circlePrefab,curretnPosition,Quaternion.identity).transform.SetParent(parentObject);}


    }

    void GetSceenData()
    {
        float screenWidth = Screen.width;
        float screenHeight = Screen.height;

        // Get the maximum and minimum X and Y positions in world space
        Vector3 maxPosition = Camera.main.ScreenToWorldPoint(new Vector3(screenWidth, screenHeight, Camera.main.transform.position.z));
        Vector3 minPosition = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, Camera.main.transform.position.z));

        maxX = maxPosition.x-1;
        maxY = maxPosition.y-1;
        minX = minPosition.x+1;
        minY = minPosition.y+1;
    }

    void SetNumbrOfCircle(int val)
    {
        numbreOfCircle=val;
    }





    //listen to event
    //clicked restart
    public void RestartGame(Component sender,object data)
    {
        if(sender is UIManager)
        {
            Debug.Log("data is "+data);
            numbreOfCircle=(int)data;
             GetSceenData();


            SpawnRandomCircle();
        }
    }
}
