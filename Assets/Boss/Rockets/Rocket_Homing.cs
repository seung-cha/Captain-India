using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket_Homing : MonoBehaviour
{
    public float speed;
    public float rotationSpeed;
    public float yRotationSpeed;
    public GameObject blastEffect;
    Rigidbody2D rb;
    BoxCollider2D boxcol;
    void Start()
    {
        boxcol = GetComponent<BoxCollider2D>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame

    private void FixedUpdate()
    {
        if(PlayerManager.Manager.player != null)
        {
            Vector2 dir = rb.position - (Vector2)PlayerManager.Manager.player.transform.position ;
            dir.Normalize();
            // Debug.Log(dir);
            float rotateAmount = Vector3.Cross(dir, transform.up).z;
            rb.angularVelocity = rotateAmount * rotationSpeed;

            rb.velocity = transform.up * speed;
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player" || collision.tag == "FieldObject")
        {
            if (collision.tag == "Player" && !PlayerManager.Manager.isInvincible)
                PlayerManager.Manager.hp -= 15;

            GameObject obj = Instantiate(blastEffect);
            Vector2 pos = new Vector2(boxcol.bounds.max.x, boxcol.bounds.center.y);
            obj.transform.position = pos;
            Destroy(obj, 5f);
       
            Destroy(this.gameObject);
        }
    }
}
