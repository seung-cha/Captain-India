using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BridgeAppearance : MonoBehaviour
{
    private Animator anim;
    public string clipName;
    public float currentTime;

    private BoxCollider2D boxCol;
    private float[] endPoints;


    private bool isCheckPointAnimation;
    public AnimationClip clip;
    public int[] checkPointsInFrame;
    private float[] lengthInTime;
    private float[] checkPointsInTime;
    private float currentCheckPoint;
    private int index;
    void Start()
    {
        anim = GetComponent<Animator>();
        boxCol = GetComponent<BoxCollider2D>();

        if (!boxCol.isTrigger)
            boxCol.isTrigger = true;
        anim.speed = 0;
        endPoints = new float[2];

        endPoints[0] = boxCol.bounds.min.x;
        endPoints[1] = boxCol.bounds.max.x;

        if(clip != null)
        {
            index = 0;
            isCheckPointAnimation = true;
            currentCheckPoint = 0;
            lengthInTime = new float[2];
            lengthInTime[0] = 0;
            lengthInTime[1] = clip.length;

            checkPointsInTime = new float[checkPointsInFrame.Length];

            for(int i = 0; i < checkPointsInTime.Length; i++)
            {
                checkPointsInTime[i] = checkPointsInFrame[i] * 1/60f;

                checkPointsInTime[i] = Mathf.InverseLerp(lengthInTime[0], lengthInTime[1], checkPointsInTime[i]);
            }

            foreach(float item in checkPointsInTime)
            {
                Debug.Log(item);
            }
        }

    }

    private void Update()
    {
        if (isCheckPointAnimation)
        {
            if (currentTime < currentCheckPoint)
                currentTime = currentCheckPoint;

            if (checkPointsInTime.Length - 1 > index)
            {
                if (currentTime > checkPointsInTime[index])
                {
                    currentCheckPoint = checkPointsInTime[index];
                    index++;
                }
            }
        }
    }
    // Update is called once per frame
    void LateUpdate()
    {
         Debug.Log(currentTime * 60);
         Debug.Log("Current CheckPoint : " + currentCheckPoint);
      
        anim.Play(clipName, 0, currentTime);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
           currentTime = Mathf.InverseLerp(endPoints[0], endPoints[1], collision.transform.position.x);
            
        }       
    }
}
