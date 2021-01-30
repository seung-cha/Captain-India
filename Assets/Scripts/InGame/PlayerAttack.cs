using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public Collider2D attackCollider;
    public float Intensity;
    public float duration;
    public int damage;

    private PlayerMovement playerMovement;

    public float shakeIntensity;
    public float shakeFrequency;
    public float shakePercentage;

    public AudioClip[] hitSoundEffect;
    private void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
    }
    public void EnableCollider()
    {
        attackCollider.gameObject.SetActive(true);
    }

    public void DisableCollider()
    {
        attackCollider.gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform != this.gameObject.transform && collision.gameObject.tag == "Enemy")
        {
            AI targetAI;
            EnemyHolder.Manager.enemyAIs.TryGetValue(collision.gameObject, out targetAI);

            if (targetAI != null)
            {
                if (this.gameObject.transform.position.x < collision.gameObject.transform.position.x)
                {
                    Vector2 value = new Vector2(1, 1).normalized;
                    Vector2 staggerDirection = new Vector2(value.x * Intensity, 1);
                    targetAI.staggerDir = staggerDirection;
                }
                else
                {
                    Vector2 value = new Vector2(1, 1).normalized;
                    Vector2 staggerDirection = new Vector2(value.x * -Intensity, 1);
                    targetAI.staggerDir = staggerDirection;
                }


                targetAI.staggerDuration = duration;
                targetAI.health -= damage * PlayerManager.Manager.damageMultiplier ;
                targetAI.isStaggered = true;

                playerMovement.ShakeCamera(shakePercentage, shakeFrequency, shakeIntensity);
                SoundManager.Manager.CrateSoundEffect(hitSoundEffect[Random.Range(0, hitSoundEffect.Length)], collision.gameObject.transform.position);

            }
        }
    }
}
