using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShatteredStone : MonoBehaviour
{
    public bool firstStone;
    public bool secondStone;
    public bool thirdStone;
    public bool fourthStone;
    public bool isComplete;
    public GameObject[] stones;
    public Material[] materials;
    public Material originalMaterial;
    private Color color;
    

    void Start()
    {
        color = originalMaterial.color;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (firstStone)
        {
            materials[0].color = color;
        }
        else
            materials[0].color = Color.white;

        if (secondStone)
        {
            materials[1].color = color;
        }
        else
            materials[1].color = Color.white;

        if (thirdStone)
        {
            materials[2].color = color;
        }
        else
            materials[2].color = Color.white;

        if (fourthStone)
        {
            materials[3].color = color;
        }
        else
            materials[3].color = Color.white;


        isComplete = firstStone && secondStone && thirdStone && fourthStone; 
    }
}
