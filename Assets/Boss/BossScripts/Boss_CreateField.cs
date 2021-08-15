using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_CreateField : MonoBehaviour
{
    // Start is called before the first frame update
    public BossStateTracker tracker;
    public Animator bossAnim;

    [Header("Field Properties")]
    public float cooldown;
    public float initialTime;
    private float timer;

    public GameObject[] spawnPoints;
    public GameObject sloField;
    public GameObject accField;

    [Header("Range")]
    public float xMinRange;
    public float xMaxRange;
    public float yMinRange;
    public float yMaxRange;

    public float maxDestroyTimer;
    public float minDestroyTimer;

    private void Start()
    {
        timer = initialTime;
        
    }
    void Update()
    {
        if (tracker.currentBossHealth <= 200)
        {
            timer -= 1 * Time.deltaTime;
            if (tracker.currentBodyState != BossStateTracker.BodyState.attack)
            {
                if (timer <= 0)
                {
                    bossAnim.SetTrigger("Create_Energy_Field");
                    timer = cooldown;
                }
            }
        }     
    }

    void CreateField()
    {
        int b = Random.Range(0, spawnPoints.Length);
        int a = Random.Range(0, 2);

        GameObject obj = (a == 1) ? Instantiate(sloField) : Instantiate(accField);

        Vector3 spawnPoint = spawnPoints[b].transform.position;
        spawnPoint = new Vector3(spawnPoint.x + Random.Range(xMinRange, xMaxRange), spawnPoint.y + Random.Range(yMinRange, yMaxRange), spawnPoint.z);

        obj.transform.position = spawnPoint;

        Destroy(obj, Random.Range(minDestroyTimer, maxDestroyTimer));
    }
}
