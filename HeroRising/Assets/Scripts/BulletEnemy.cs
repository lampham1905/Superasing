using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletEnemy : MonoBehaviour
{
    public float speed;
    private Rigidbody2D rb;
    private void Start() {
        rb = GetComponent<Rigidbody2D>();
    }
    private void Update() {
        rb.velocity = new Vector2(0, -speed);
        if(GameManager.ins.startLastAttack){
            Destroy(this.gameObject);
        }
    }
    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.CompareTag("destroyBullet")){
            Destroy(this.gameObject);
        }
       
    }
    private void OnTriggerEnter2D(Collider2D other) {
         if(other.gameObject.CompareTag("head")){
            GameManager.ins.CollectTrap(Enemy.ins.damage);
            Destroy(this.gameObject);
        }
    }
}
