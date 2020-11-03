using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "LoadingScreen")]
public class LoadingScreenInfo : ScriptableObject
{
    public Texture2D background;
    
    [TextArea (2, 5)]
    public string text;
}
