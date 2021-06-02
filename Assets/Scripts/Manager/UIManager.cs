using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class UIManager : MonoBehaviour
{
    public Slider hpSlider;
    public Slider staminaSlider;
    public TextMeshProUGUI countText;
    public GameObject heart;
    public Image Sprite;


    void Start()
    {
        hpSlider.maxValue = PlayerManager.Manager.maxHealth;
        staminaSlider.maxValue = PlayerManager.Manager.maxStamina;
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerManager.Manager == null || PlayerManager.Manager.player == null)
        {
            Sprite.gameObject.SetActive(false);
            hpSlider.gameObject.SetActive(false);
            staminaSlider.gameObject.SetActive(false);
            heart.gameObject.SetActive(false);
        }
        else
        {
            Sprite.gameObject.SetActive(true);
            hpSlider.gameObject.SetActive(true);
            staminaSlider.gameObject.SetActive(true);
            heart.gameObject.SetActive(true);
            hpSlider.value = PlayerManager.Manager.hp;
            staminaSlider.value = PlayerManager.Manager.stamina;
            countText.text = PlayerManager.Manager.reviveCount.ToString();
        }
    }
}
