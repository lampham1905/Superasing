using System.Linq.Expressions;
using System.Net;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
   
    public static PlayerController ins;
    private void Awake() {
        ins = this;
    }
    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer renderer;
   public float speed;
   public float laneDistance;
   public float laneIndex = 0;
   public int numberCurrent = 30;
   public int number;
   public bool canSwipeLeft = false;
   public bool canSwipeRight = false;
   public GameObject firstPos;
    public GameObject midPos;
   public GameObject lastPos;

   //attack
   public bool canAttack = true;
   public GameObject firePosition;
   public GameObject bulletPrefabs;
   public bool startLastAttack = false;
   public GameObject bulletLastAttack;
   
   private void Start() {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        renderer = GetComponent<SpriteRenderer>();
        number = numberCurrent;
   }
    private void Update() {
        if(!GameManager.ins.startLastAttack){
                     if(laneIndex == 1){
            //transform.position = Vector3.Slerp(startPos.transform.position, endPos.transform.position, 1);
                if(canSwipeRight){
                    transform.position = Vector3.MoveTowards(transform.position, lastPos.transform.position, speed * Time.deltaTime);
                    //transform.position = Vector3.Slerp(transform.position, lastPos.transform.position, .01f);
                    }
                }  
         if(laneIndex == 0){
               // transform.position = Vector3.Slerp(startPos.transform.position, endPos.transform.position, 0);
               // rb.velocity = new Vector2(-speed, 0);
                if(canSwipeLeft){
                    transform.position = Vector3.MoveTowards(transform.position, firstPos.transform.position, speed * Time.deltaTime);
                    //transform.position = Vector3.Slerp(transform.position, firstPos.transform.position, .01f);
                }
            }
        }
      if(!GameManager.ins.startLastAttack){
          if(SwipeManager.swipeRight){
            canSwipeRight = true;
            canSwipeLeft = false;
         
          laneIndex++;
          if(laneIndex == 2){
            laneIndex = 1;
          }
          
        }
        if(SwipeManager.swipeLeft){
            canSwipeLeft = true;
            canSwipeRight = false;
           
            laneIndex--;
            if(laneIndex == -1){
                laneIndex = 0;
            }
        }
      }
         

     if(startLastAttack){
        transform.position = Vector3.MoveTowards(transform.position, midPos.transform.position, speed * Time.deltaTime);
     }
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.CompareTag("end")){
            number = numberCurrent;
            Destroy(other.gameObject);
             
            if (numberCurrent > Enemy.ins.hp)
            {  
                  Enemy.ins.canMove = true;
                  Enemy.ins.MoveAndAttack();
                  ScrollRad.ins.stop = true;
                  canAttack = true;
                  AutoAttack();
            }   
            else
            {
                Time.timeScale = 0;
                UIManager.ins.ShowReplayGUI();
            } 
            
        }
    }
    public void AutoAttack(){
       // if(canAttack){
            StartCoroutine(AttackCoroutine());
            
       // }
    }
    IEnumerator AttackCoroutine(){

            while(canAttack && !GameManager.ins.startLastAttack){
                yield return new WaitForSeconds(1f);
                Instantiate(bulletPrefabs, firePosition.transform.position, Quaternion.identity);
            }
    }
     public void LastAttack(){
        startLastAttack = true;
        animator.SetTrigger("zoom");
    }
    public void ChangeColor(){
        StartCoroutine(ChangeColorCoroutine());
    }
    IEnumerator ChangeColorCoroutine(){
        renderer.color = Color.red;
        yield return new WaitForSeconds(.1f);
        renderer.color = Color.white;
        yield return new WaitForSeconds(.1f);
        renderer.color = Color.red;
        yield return new WaitForSeconds(.1f);
        renderer.color = Color.white;
    }
    public void DestroyPlayer(){
        StartCoroutine(DestroyPlayerCoroutine());
    }
    IEnumerator DestroyPlayerCoroutine(){
        StartCoroutine(ChangeColorCoroutine());
        yield return new WaitForSeconds(.4f);
        Destroy(this.gameObject);
        UIManager.ins.ShowReplayGUI();
    }
    
  
}
