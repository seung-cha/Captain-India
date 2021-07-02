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
    [SerializeField]
    private float gunCurrentAttackCooldown;
    [SerializeField]
    private float hammerCurrentAttackCooldown;

    public int currentBossHealth;
    public int level;
    public Animator bossAnimator;
    private List<string> gunTriggers;
    private List<string> hammerTriggers;
    private List<string> bodyTriggers;

    private Dictionary<AttackData, string> gunTriggers_;
    private Dictionary<AttackData, string> hammerTriggers_;
    private Dictionary<AttackData, string> bossTriggers_;
    [SerializeField]
    private int attackTracker;
    public int attackCounts;


    // -1 = left, 0 = mid, 1 = right
    public int playerXPosValue;
    // 1 = top, 0 = mid, -1 = bottom
    public int playerYPosValue;

    public string playerXPosString;
    public string playerYPosString;



    private void Awake()
    {
        gunTriggers = new List<string>();
       // hammerTriggers = new List<string>();
        bodyTriggers = new List<string>();
        attackTracker = 0;


        // Add the list of attacks
      //  gunTriggers.Add("Gun_Sweep_Middle");
     //   gunTriggers.Add("Gun_Laser_Middle");
     //   gunTriggers.Add("Gun_Laser_Right");
     //   gunTriggers.Add("Gun_Smash_Left");

       // hammerTriggers.Add("Hammer_Smash_Right");
       // hammerTriggers.Add("Hammer_Sweep_Right");
       // hammerTriggers.Add("Hammer_Smack_Mid");


        bodyTriggers.Add("Boss_Attack_1");
        bodyTriggers.Add("Boss_Attack_2");


        //
        // Hammer
        //
        hammerTriggers_ = new Dictionary<AttackData, string>();
        hammerTriggers_.Add(new AttackData(1, "All", "Right"), "Hammer_Smash_Right");
        hammerTriggers_.Add(new AttackData(-1, "TMid", "Mid"), "Hammer_Smack_Mid");
        hammerTriggers_.Add(new AttackData(-1, "Bottom", "RMid"), "Hammer_Sweep_Right");
        hammerTriggers_.Add(new AttackData(0, "BMid", "Mid"), "Hammer_Bottom_Mid");
        hammerTriggers_.Add(new AttackData(0, "All", "Mid"), "Hammer_Mid_Mid");
        hammerTriggers_.Add(new AttackData(-1, "TMid", "Left"), "Hammer_Missile_Bottom_Left");

        //
        //
        //


        //
        // Gun
        //
        gunTriggers_ = new Dictionary<AttackData, string>();
        gunTriggers_.Add(new AttackData(-1, "All", "All"), "Gun_Laser_Middle");
        gunTriggers_.Add(new AttackData(-1, "All", "RMid"), "Gun_Laser_Right");
        gunTriggers_.Add(new AttackData(-1, "All", "LMid"), "Gun_Smash_Left");
        gunTriggers_.Add(new AttackData(0, "TMid", "All"), "Gun_Sweep_Middle");
        gunTriggers_.Add(new AttackData(1, "Bottom", "All"), "Gun_Laser_Bottom");

        //
        // Boss attack
        //
        bossTriggers_ = new Dictionary<AttackData, string>();
        bossTriggers_.Add(new AttackData(-1, " ", " "), "Boss_Attack_1");
        bossTriggers_.Add(new AttackData(1, " ", " "), "Boss_Attack_2");
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
        CheckPlayerXPos();
        CheckPlayerYPos();
        CheckCurrentLevel();
    }

    private void LateUpdate()
    {
        CheckForBodyAttack();
    }

    void CheckPlayerXPos()
    {
        // 34 m total (17 one side), 11.33f per sector

        // -1 when player x greater than 
        if (PlayerManager.Manager.player == null)
            return;


        if (PlayerManager.Manager.player.transform.position.x < -5.67)
        {
            playerXPosValue = -1;
            playerXPosString = "Left";
        }
        else if (PlayerManager.Manager.player.transform.position.x >= -5.67 && PlayerManager.Manager.player.transform.position.x <= 5.67)
        {
            playerXPosValue = 0;
            playerXPosString = "Mid";
        }
        else
        {
            playerXPosValue = 1;
            playerXPosString = "Right";
        }
    }

    void CheckPlayerYPos()
    {
        if (PlayerManager.Manager.player == null)
            return;


        if (PlayerManager.Manager.player.transform.position.y > 2)
        {
            playerYPosValue = 1;
            playerYPosString = "Top";
        }
        else if (PlayerManager.Manager.player.transform.position.y <= 2 && PlayerManager.Manager.player.transform.position.y >= -2.7)
        {
            playerYPosValue = 0;
            playerYPosString = "Mid";
        }
        else
        {
            playerYPosValue = -1;
            playerYPosString = "Bottom";
        }
    }

    void CheckGunCooldown()
    {
        if (currentGunState == Gunstate.idle && currentBodyState == BodyState.idle && attackTracker <= attackCounts)
        {
            gunCurrentAttackCooldown -= 1 * Time.deltaTime;

            if (gunCurrentAttackCooldown <= 0)
            {
                List<string> attacks = new List<string>();
                attacks.Clear();
                Dictionary<AttackData, string> attacks_ = new Dictionary<AttackData, string>();
                foreach (KeyValuePair<AttackData, string> infos__ in gunTriggers_)
                {
                    switch (level)
                    {
                        case 0:
                            if (infos__.Key.attackLevel == -1 || infos__.Key.attackLevel == 0)
                                attacks_.Add(infos__.Key, infos__.Value);
                            break;
                        case 1:
                            if (infos__.Key.attackLevel == -1 || infos__.Key.attackLevel == 1)
                                attacks_.Add(infos__.Key, infos__.Value);
                            break;
                        case 2:
                            if (infos__.Key.attackLevel == -1 || infos__.Key.attackLevel == 2)
                                attacks_.Add(infos__.Key, infos__.Value);
                            break;
                    }
                }

                    foreach (KeyValuePair<AttackData, string> infos in attacks_)
                {
                    switch (playerXPosValue)
                    {
                        case -1:

                            if (infos.Key.attackHLevel == "Left" || infos.Key.attackHLevel == "LMid" || infos.Key.attackHLevel == "All")
                                attacks.Add(infos.Value);
                            break;

                        case 0:
                            if (infos.Key.attackHLevel == "LMid" || infos.Key.attackHLevel == "Mid" || infos.Key.attackHLevel == "RMid" || infos.Key.attackHLevel == "All")
                                attacks.Add(infos.Value);
                            break;
                        case 1:
                            if (infos.Key.attackHLevel == "Right" || infos.Key.attackHLevel == "RMid" || infos.Key.attackHLevel == "All")
                                attacks.Add(infos.Value);
                            break;
                    }
                }

                foreach (KeyValuePair<AttackData, string> infos_ in attacks_)
                {
                    switch (playerYPosValue)
                    {
                        case -1:
                            if (infos_.Key.attackVLevel == "Bottom" || infos_.Key.attackVLevel == "BMid" || infos_.Key.attackHLevel == "All")
                            {
                                if (!attacks.Contains(infos_.Value))
                                    attacks.Add(infos_.Value);
                            }
                            break;
                        case 0:
                            if (infos_.Key.attackVLevel == "BMid" || infos_.Key.attackVLevel == "Mid" || infos_.Key.attackVLevel == "TMid" || infos_.Key.attackHLevel == "All")
                            {
                                if (!attacks.Contains(infos_.Value))
                                    attacks.Add(infos_.Value);
                            }
                            break;
                        case 1:
                            if (infos_.Key.attackVLevel == "Top" || infos_.Key.attackVLevel == "TMid" || infos_.Key.attackHLevel == "All")
                            {
                                if (!attacks.Contains(infos_.Value))
                                    attacks.Add(infos_.Value);
                            }
                            break;
                    }
                }
                Debug.Log("Gun Attack stored :" + attacks.Count);
                if (attacks.Count != 0)
                {
                    bossAnimator.SetTrigger(attacks[Random.Range(0, attacks.Count)]);
                    attackTracker++;
                }
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
                // attack;
                // bossAnimator.SetTrigger(hammerTriggers[Random.Range(0, hammerTriggers.Count)]);

                Debug.Log("attack called");
                // Process X axis first
                List<string> attacks = new List<string>();
                attacks.Clear();
                Dictionary<AttackData, string> attacks_ = new Dictionary<AttackData, string>();

                foreach (KeyValuePair<AttackData, string> infos__ in hammerTriggers_)
                {
                    switch (level)
                    {
                        case 0:
                            if (infos__.Key.attackLevel == -1 || infos__.Key.attackLevel == 0)
                                attacks_.Add(infos__.Key, infos__.Value);
                            break;
                        case 1:
                            if (infos__.Key.attackLevel == -1 || infos__.Key.attackLevel == 1)
                                attacks_.Add(infos__.Key, infos__.Value);
                            break;
                        case 2:
                            if (infos__.Key.attackLevel == -1 || infos__.Key.attackLevel == 2)
                                attacks_.Add(infos__.Key, infos__.Value);
                            break;
                    }
                }
                Debug.Log("atttacks_ info stored " + attacks_.Count);
                foreach (KeyValuePair<AttackData, string> infos in attacks_)
                {
                    switch (playerXPosValue)
                    {
                        case -1:

                            if (infos.Key.attackHLevel == "Left" || infos.Key.attackHLevel == "LMid" || infos.Key.attackHLevel == "All")
                                attacks.Add(infos.Value);
                            break;

                        case 0:
                            if (infos.Key.attackHLevel == "LMid" || infos.Key.attackHLevel == "Mid" || infos.Key.attackHLevel == "RMid" || infos.Key.attackHLevel == "All")
                                attacks.Add(infos.Value);
                            break;
                        case 1:
                            if (infos.Key.attackHLevel == "Right" || infos.Key.attackHLevel == "RMid" || infos.Key.attackHLevel == "All")
                                attacks.Add(infos.Value);
                            break;
                    }
                }

                foreach (KeyValuePair<AttackData, string> infos_ in attacks_)
                {
                    switch (playerYPosValue)
                    {
                        case -1:
                            if (infos_.Key.attackVLevel == "Bottom" || infos_.Key.attackVLevel == "BMid" ||  infos_.Key.attackHLevel == "All")
                            {
                                if (!attacks.Contains(infos_.Value))
                                    attacks.Add(infos_.Value);
                            }
                            break;
                        case 0:
                            if (infos_.Key.attackVLevel == "BMid" || infos_.Key.attackVLevel == "Mid" || infos_.Key.attackVLevel == "TMid" || infos_.Key.attackHLevel == "All")
                            {
                                if (!attacks.Contains(infos_.Value))
                                    attacks.Add(infos_.Value);
                            }
                            break;
                        case 1:
                            if (infos_.Key.attackVLevel == "Top" || infos_.Key.attackVLevel == "TMid" || infos_.Key.attackHLevel == "All")
                            {
                                if (!attacks.Contains(infos_.Value))
                                    attacks.Add(infos_.Value);
                            }
                            break;
                    } 
                }

                if (attacks.Count != 0)
                {
                    bossAnimator.SetTrigger(attacks[Random.Range(0, attacks.Count)]);
                    attackTracker++;
                }

            }
          
        }
        else
            hammerCurrentAttackCooldown = hammerAttackCooldown;

    }

    void CheckForBodyAttack()
    {
        if (attackTracker >= attackCounts && currentHammerState == Hammerstate.idle && currentGunState == Gunstate.idle)
        {
            List<string> attacks = new List<string>();
            foreach (KeyValuePair<AttackData, string> infos in bossTriggers_)
            {
                switch (level)
                {
                    case -1:
                        if (infos.Key.attackLevel == -1 || infos.Key.attackLevel == 0 || infos.Key.attackLevel == 1 || infos.Key.attackLevel == 2)
                            attacks.Add(infos.Value);
                        break;

                    case 0:
                        if (infos.Key.attackLevel == -1 || infos.Key.attackLevel == 0)
                            attacks.Add(infos.Value);
                        break;
                    case 1:
                        if (infos.Key.attackLevel == -1 || infos.Key.attackLevel == 1)
                            attacks.Add(infos.Value);
                        break;
                    case 2:
                        if (infos.Key.attackLevel == -1 || infos.Key.attackLevel == 2)
                            attacks.Add(infos.Value);
                        break;
                }
            }

            if(attacks.Count != 0) {            
            bossAnimator.SetTrigger(attacks[Random.Range(0, attacks.Count)]);
            attackTracker = 0;
            }
        }
    }

    void CheckCurrentLevel()
    {
        if(currentBossHealth > 200)
        {
            level = 0;
            bossAnimator.SetFloat("SpeedValue", 1.0f);
        }
        else if ( currentBossHealth <= 200 && currentBossHealth > 50)
        {
            level =1;
            bossAnimator.SetFloat("SpeedValue", 1.2f);
        }
        else if (currentBossHealth <= 50)
        {
            level = 2;
        }
    }

}
    public class AttackData
    {

        // -1 always, 0 lv1, 1 lv2, 2 lv3;
        public int attackLevel;

        // Left, Mid, Right, All, LMid, RMid
        public string attackHLevel;

        // Bottom, Mid, Top, All, BMid, TMid
        public string attackVLevel;

        public AttackData(int bLevel, string vLevel, string hLevel)
        {
            attackLevel = bLevel;
            attackVLevel = vLevel;
            attackHLevel = hLevel;
        }
    }

