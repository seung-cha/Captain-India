using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationManager : MonoBehaviour
{
   
   public static PlayerAnimationManager Manager;
    
    public enum State
    {
        Idle,
        running,
        
    }

    public enum Combat
    {
        Idle,
        attack
    }

    public Combat currentCombatState;
    public State currentState;

    private Animator animator;
    private void Awake()
    {
        if (Manager == null)
            Manager = this;

        if (Manager != null && Manager != this)
            Destroy(Manager.gameObject);


        DontDestroyOnLoad(Manager.gameObject);
    }
    void Start()
    {
        animator = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
        currentState = State.Idle;
        currentCombatState = Combat.Idle;
    }

    // Update is called once per frame
    void Update()
    {
        if(currentState == State.running)
            animator.SetBool("isRunning", true);
        else
            animator.SetBool("isRunning", false);

       if (currentCombatState == Combat.attack)
        {
            animator.SetTrigger("isAttacking");
            currentCombatState = Combat.Idle;
        }

    }
}
