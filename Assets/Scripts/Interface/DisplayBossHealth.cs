using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayBossHealth : MonoBehaviour
{
    public Slider bossHP;
    public BossStateTracker trac;
    // Start is called before the first frame update
    void Start()
    {
        bossHP.maxValue = 300;
    }

    // Update is called once per frame
    void Update()
    {
        bossHP.value = trac.currentBossHealth;
    }
}
