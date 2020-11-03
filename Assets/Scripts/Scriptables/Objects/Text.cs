using System;
using UnityEngine;

[Serializable]
public class Text 
{
    public Texture2D image;
    public string speaker;

    [TextArea(2, 10)]
    public string text;

}
