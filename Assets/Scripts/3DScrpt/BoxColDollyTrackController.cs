using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

[RequireComponent(typeof(BoxCollider2D))]
public class BoxColDollyTrackController : MonoBehaviour
{
    private BoxCollider2D boxCol;
    private float[] endPoints;
    private CinemachineVirtualCamera dollyCam;
    private CinemachineDollyCart dollyCart;
    private CinemachineTransposer transposer;
    private float progression;

    public bool enableCameraTilting_X;
    public bool enableCameraTilting_Y;
    public bool enableCameraTilting_Z;
    public float[] value_X = new float[2];
    public float[] value_Y = new float[2];
    public float[] value_Z = new float[2];
    void Start()
    {
        boxCol = GetComponent<BoxCollider2D>();

        dollyCam = GetComponent<CinemachineVirtualCamera>();
        dollyCart = GetComponent<CinemachineDollyCart>();

        if(dollyCam == null)
            dollyCam = GetComponentInChildren<CinemachineVirtualCamera>();

        if(dollyCart == null)
            dollyCart = GetComponentInChildren<CinemachineDollyCart>();

        transposer = dollyCam.GetCinemachineComponent<CinemachineTransposer>();

        dollyCart.m_PositionUnits = CinemachinePathBase.PositionUnits.Normalized;

        if (!boxCol.isTrigger)
            boxCol.isTrigger = true;

        endPoints = new float[2];
        endPoints[0] = boxCol.bounds.min.x;
        endPoints[1] = boxCol.bounds.max.x;

    }

    private void Update()
    {
            if (enableCameraTilting_X)
          transposer.m_FollowOffset = new Vector3 (Mathf.Lerp(value_X[0], value_X[1], progression), transposer.m_FollowOffset.y , transposer.m_FollowOffset.z);
        
        if(enableCameraTilting_Y)
            transposer.m_FollowOffset = new Vector3(transposer.m_FollowOffset.x, Mathf.Lerp(value_Y[0], value_Y[1], progression), transposer.m_FollowOffset.z);

        if (enableCameraTilting_Z)
            transposer.m_FollowOffset = new Vector3(transposer.m_FollowOffset.x, transposer.m_FollowOffset.y, Mathf.Lerp(value_Z[0], value_Z[1], progression));
    }


    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            dollyCam.Priority = 99;
            progression = Mathf.InverseLerp(endPoints[0], endPoints[1], collision.transform.position.x);
            dollyCart.m_Position = progression;      
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag == "Player")
                    dollyCam.Priority = -1;
    }
}
