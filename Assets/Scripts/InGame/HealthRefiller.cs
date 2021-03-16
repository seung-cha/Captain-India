using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthRefiller : MonoBehaviour
{
    public AudioClip audioClip;
    public int amountToHeal;


    private void Update()
    {
        this.gameObject.transform.rotation = Quaternion.Euler(gameObject.transform.rotation.x, gameObject.transform.rotation.y + Mathf.Sin(Time.time) * 360, gameObject.transform.rotation.z);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            PlayerManager.Manager.hp += amountToHeal;

            if(audioClip != null)
            SoundManager.Manager.CrateSoundEffect(audioClip, this.gameObject.transform.position);

            Destroy(this.gameObject);
        }
    }
}
