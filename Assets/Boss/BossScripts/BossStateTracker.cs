using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossStateTracker : MonoBehaviour
{
   public enum Gunstate
    {
        idle,
        attack
    }

   public enum Hammerstate
    {
        idle,
        attack
    }

    public enum BodyState
    {
        idle,
        attack
    }

    public Gunstate currentGunState;
    public Hammerstate currentHammerState;
    public BodyState currentBodyState;

    public float gunAttackCooldown;
    public float hammerAttackCooldown;
    public float gunInitialCooldown;
    public float hammerInitialCooldown;
    private float gunCurrentAttackCooldown;
    private float hammerCurrentAttackCooldown;

    public Animator bossAnimator;
    private List<string> gunTriggers;
    private List<string> hammerTriggers;
    private List<string> bodyTriggers;

    [SerializeField]
    private int attackTracker;
    public int attackCounts;
    private void Awake()
    {
        gunTriggers = new List<string>();
        hammerTriggers = new List<string>();
        bodyTriggers = new List<string>();
        attackTracker = 0;


        // Add the list of attacks
        gunTriggers.Add("Gun_Sweep_Middle");
        gunTriggers.Add("Gun_Laser_Middle");
        gunTriggers.Add("Gun_Laser_Right");
        gunTriggers.Add("Gun_Smash_Left");

        hammerTriggers.Add("Hammer_Smash_Right");
        hammerTriggers.Add("Hammer_Sweep_Right");
        hammerTriggers.Add("Hammer_Smack_Mid");
        

        bodyTriggers.Add("Boss_Attack_1");
        bodyTriggers.Add("Boss_Attack_2");
    }
    void Start()
    {
        gunCurrentAttackCooldown = gunInitialCooldown;
        hammerCurrentAttackCooldown = hammerInitialCooldown;
    }

    // Update is called once per frame
    void Update()
    {
        CheckGunCooldown();
        CheckHammerCooldown();
    }

    private void LateUpdate()
    {
        CheckForBodyAttack();
    }

    void CheckGunCooldown()
    {
        if (currentGunState == Gunstate.idle && currentBodyState == BodyState.idle && attackTracker <= attackCounts)
        {
            gunCurrentAttackCooldown -= 1 * Time.deltaTime;

            if (gunCurrentAttackCooldown <= 0)
            {
                bossAnimator.SetTrigger(gunTriggers[Random.Range(0, gunTriggers.Count)]);
                attackTracker++;
}

        }
        else
            gunCurrentAttackCooldown = gunAttackCooldown;

    }



    void CheckHammerCooldown()
    {
        if (currentHammerState == Hammerstate.idle && currentBodyState == BodyState.idle && attackTracker <= attackCounts)
        {
            hammerCurrentAttackCooldown -= 1 * Time.deltaTime;

            if (hammerCurrentAttackCooldown <= 0)
            {
                bossAnimator.SetTrigger(hammerTriggers[Random.Range(0, hammerTriggers.Count)]);
                attackTracker++;
            }
        }
        else
            hammerCurrentAttackCooldown = hammerAttackCooldown;
    }


    void CheckForBodyAttack()
    {
        if (attackTracker >= attackCounts && currentHammerState == Hammerstate.idle && currentGunState == Gunstate.idle)
        {
            bossAnimator.SetTrigger(bodyTriggers[Random.Range(0, bodyTriggers.Count)]);
            attackTracker = 0;
        }
    }
    

}
