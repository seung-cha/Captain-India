using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Create a new speech")]
public class Speech : ScriptableObject
{
    public  Text[] texts;
    // 0 = left ; 1 = right;
   public  int[] value;
}
