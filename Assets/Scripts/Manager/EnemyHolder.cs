using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHolder : MonoBehaviour
{
    public static EnemyHolder Manager;
    public Dictionary<GameObject, AI> enemyAIs;

    private void Awake()
    {
        if (Manager == null)
            Manager = this;

        if (Manager != null && Manager != this)
            Destroy(this.gameObject);

        DontDestroyOnLoad(this.gameObject);
        enemyAIs = new Dictionary<GameObject, AI>();
    }
}
