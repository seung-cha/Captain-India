using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;


public class Cheats : MonoBehaviour
{
    int defaultDamage;
    int defaultJumpCount;
    int defaultHealth;
    int defaultStamina;

    [SerializeField]
    bool damageHackEnabled;
    [SerializeField]
    bool jumpHackEnabled;
    [SerializeField]
    bool healthHackEnabled;
    [SerializeField]
    bool staminaHackEnabled;

   public  TextMeshProUGUI damageHackText;
    public TextMeshProUGUI jumpHackText;
    public TextMeshProUGUI healthHackText;
    public TextMeshProUGUI staminaHackText;

    public GameObject deathPanel;
    void Start()
    {
        defaultDamage = 1;
        defaultJumpCount = 2;
        defaultHealth = 100;
        defaultStamina = 100;
        
    }

    // Update is called once per frame

    private void Update()
    {
        if (jumpHackEnabled)
            PlayerManager.Manager.jumpCount = defaultJumpCount;

        if (healthHackEnabled)
            PlayerManager.Manager.hp = defaultHealth;

        if (staminaHackEnabled)
            PlayerManager.Manager.stamina = defaultStamina;
    }
    void LateUpdate()
    {
        if(damageHackEnabled)       
            PlayerManager.Manager.damageMultiplier = 100;        
    


    }


    public void ToggleDamageHack()
    {
        if (damageHackEnabled)
        {
            damageHackEnabled = false;
            damageHackText.gameObject.SetActive(false);
        }
        else
        {
            damageHackEnabled = true;
            damageHackText.gameObject.SetActive(true);
        }
    }

    public void ToggleJumpHack()
    {
        if (jumpHackEnabled)
        {
            jumpHackEnabled = false;
            jumpHackText.gameObject.SetActive(false);
        }
        else
        {
            jumpHackEnabled = true;
            jumpHackText.gameObject.SetActive(true);
        }
    }

    public void ToggleHealthHack()
    {
        if (healthHackEnabled)
        {
            healthHackEnabled = false;
            healthHackText.gameObject.SetActive(false);
        }
        else
        {
            healthHackEnabled = true;
            healthHackText.gameObject.SetActive(true);
        }
    }

    public void ToggleStaminaHack()
    {
        if (staminaHackEnabled)
        {
            staminaHackEnabled = false;
            staminaHackText.gameObject.SetActive(false);
        }
        else
        {
            staminaHackEnabled = true;
            staminaHackText.gameObject.SetActive(true);
        }
    }

    public void ToMainMenu()
    {
        if (SceneManager.GetActiveScene().buildIndex != 0)
        {
            deathPanel.SetActive(false);
            LoadingScreenManager.Manager.sceneIndex = 0;
            SceneManager.LoadScene(1);

            DialogueManager.Manager.timeLineIndex = -1;
            DialogueManager.Manager.clips = null;
            DialogueManager.Manager.director = null;
            PlayerManager.Manager.canMove = true;
            PlayerManager.Manager.onDialogue = false;
            DialogueManager.Manager.HideDialogue();

            PauseManager.Manager.isPaused = false;
            Time.timeScale = 1.0f;

            PauseManager.Manager.pauseMenu.SetActive(false);
          
            Refill();
        }

    }

    private void Refill()
    {
        PlayerManager.Manager.hp = PlayerManager.Manager.maxHealth;
        PlayerManager.Manager.stamina = PlayerManager.Manager.maxStamina;
        PlayerManager.Manager.enhancementDuration = 0;
        PlayerManager.Manager.reviveCount = 3;
    }
}
