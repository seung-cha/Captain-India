using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.Video;

public class Cam_FocusOnVideo : MonoBehaviour
{
   public  CinemachineVirtualCamera cinCam;
    private CinemachineDollyCart dollyCart;
    public BoxCollider2D boxCol;
    private float[] endPoints;
    private float progression;
    public float maxIdleTime;
    private bool isOutside;
    private bool isIdle;
    private float threshHold;

    private GameObject layer;
    private Vector3 lastPosition;

    public CinemachineVirtualCamera alternativeCamera;
    public VideoPlayer vidPlayer;

    void Start()
    {
      //  cinCam = GetComponent<CinemachineVirtualCamera>();
        dollyCart = GetComponent<CinemachineDollyCart>();

      //  if (cinCam == null)
        //    cinCam = GetComponentInChildren<CinemachineVirtualCamera>();

        if (dollyCart == null)
            dollyCart = GetComponentInChildren<CinemachineDollyCart>();

        cinCam.Priority = -1;

        endPoints = new float[2];
        endPoints[0] = boxCol.bounds.min.x;
        endPoints[1] = boxCol.bounds.max.x;

       layer = GameObject.Find("PlayerUI");
    }

    // Update is called once per frame
    void Update()
    {
   
        if (!isOutside)
        {
            if (isIdle)
            {
                threshHold += 1 * Time.deltaTime;
            }
            else
                threshHold = 0;
        }
        else
            threshHold = 0;

        if (threshHold >= 1000)
            threshHold = 1000;

        if (alternativeCamera != null)
        {
            if (threshHold >= maxIdleTime)
            {
                alternativeCamera.Priority = 100;


                if (layer != null)
                    layer.SetActive(false);
            }
            else
            {
                alternativeCamera.Priority = -1;


            }
        }

    }



    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            isOutside = false;

            cinCam.Priority = 99;

            progression = Mathf.InverseLerp(endPoints[0], endPoints[1], collision.transform.position.x);
            dollyCart.m_Position = progression;


            if(collision.transform.position != lastPosition)
            {
                lastPosition = collision.transform.position;

                if (layer != null)
                    layer.SetActive(true);

                isIdle = false;
            }
            else
            {
                isIdle = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            cinCam.Priority = -1;
            isOutside = true;
        }
    }
}
