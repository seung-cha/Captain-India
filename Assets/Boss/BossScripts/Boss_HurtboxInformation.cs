using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_HurtboxInformation : MonoBehaviour
{
    int damage;
    public BossStateTracker tracker;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform.tag == "PlayerHitbox")
        {
            if(PlayerManager.Manager.currentPlayerAnimationState == PlayerManager.playerAnimationState.normalZ)
            {
                damage = 1; // = 3
            }
            else if(PlayerManager.Manager.currentPlayerAnimationState == PlayerManager.playerAnimationState.normalX)
            {
                damage = 2; // 12
            }
            else if (PlayerManager.Manager.currentPlayerAnimationState == PlayerManager.playerAnimationState.downZ)
            {
                damage = 5;
            }
            else if (PlayerManager.Manager.currentPlayerAnimationState == PlayerManager.playerAnimationState.upper)
            {
                damage = 5;
            }
            else if (PlayerManager.Manager.currentPlayerAnimationState == PlayerManager.playerAnimationState.airPunch)
            {
                damage = 2;
            }
            else if (PlayerManager.Manager.currentPlayerAnimationState == PlayerManager.playerAnimationState.airKick)
            {
                damage = 2;
            }
            else if (PlayerManager.Manager.currentPlayerAnimationState == PlayerManager.playerAnimationState.beam)
            {
                damage = 3; // = 18
            }
            else if (PlayerManager.Manager.currentPlayerAnimationState == PlayerManager.playerAnimationState.downX)
            {
                damage = 10;
            }

            tracker.currentBossHealth -= damage;
        }
    }
}
