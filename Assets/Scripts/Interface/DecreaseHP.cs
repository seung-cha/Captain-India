using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecreaseHP : MonoBehaviour
{
    public GameObject hpBar;
    public AI refer;
    private float iniValue;
    void Start()
    {
        iniValue = hpBar.transform.localScale.x;
    }

    // Update is called once per frame
    void Update()
    {
        hpBar.transform.localScale = new Vector3(Mathf.InverseLerp(0, refer.maxHealth, refer.health) * iniValue, hpBar.transform.localScale.y, hpBar.transform.localScale.z);
    }
}
