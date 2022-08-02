
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private SpriteRenderer _renderer;
    private Animator animator;
    public static Enemy ins;
    private void Awake() {
        ins = this;
    }
    public int hp; 
    public int hpCurrent;
    public GameObject firePosition;
    public GameObject BulletPrefabs;
    
    public int damage;

    //Move
    public float speed;
    public GameObject firstPos;
    public GameObject midPos;
   public GameObject lastPos;
   public bool canSwipeLeft = false;
   public bool canSwipeRight = false;
   public float laneIndex = 0;
   public float startLaneIndex;
   public bool canMove = false;
   public void AutoAttack(){

   }
   IEnumerator AttackCoroutine(){
        yield return new WaitForSeconds(.5f);
        Instantiate(BulletPrefabs, firePosition.transform.position, Quaternion.identity);
        yield return new WaitForSeconds(1f);
        Instantiate(BulletPrefabs, firePosition.transform.position, Quaternion.identity);
   }
   public void MoveAndAttack(){
        
        StartCoroutine(MoveCoroutine());
   }
   IEnumerator MoveCoroutine(){
          while(canMove && !GameManager.ins.startLastAttack){
            canSwipeRight = true;
            canSwipeLeft = false;
         
            laneIndex++;
            if(laneIndex == 2){
            laneIndex = 1;
            }
            yield return new WaitForSeconds(1f);
            if(!GameManager.ins.startLastAttack){
                StartCoroutine(AttackCoroutine());
            }
            yield return new WaitForSeconds(2f);
            canSwipeLeft = true;
            canSwipeRight = false;
           
            laneIndex--;
            if(laneIndex == -1){
                laneIndex = 0;
            }
            yield return new WaitForSeconds(1f);
            if(!GameManager.ins.startLastAttack){
                StartCoroutine(AttackCoroutine());
            }
            yield return new WaitForSeconds(2f);
          }
       
   }
   private void Start() {
        hpCurrent = hp;
        laneIndex = startLaneIndex;
        _renderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        UIManager.ins.UpdateNumberEnemy(hp);
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
            // Move Loop    
            }
            if(isStartLastAttack){
                transform.position = Vector3.MoveTowards(transform.position, midPos.transform.position, speed * Time.deltaTime);
            }
   }
   private bool islastAttack = true;
   public void TakeDamage(int damage){
        if(hpCurrent > 0){
           
            hpCurrent = hpCurrent - damage;
            ChangeColor();
            UIManager.ins.UpdateNumberEnemy(hpCurrent);
          
        }
        if(hpCurrent <= 0){
                hpCurrent = 0;
            UIManager.ins.UpdateNumberEnemy(hpCurrent);
               if(islastAttack){
                 islastAttack  = false;
                 GameManager.ins.startLastAttack = true;
                 GameManager.ins.LastAttack();
               }
            }
   }  
   public void ChangeColor(){
    StartCoroutine(ChangeColorCoroutine());
   }  
    IEnumerator ChangeColorCoroutine(){
        _renderer.color = Color.red;
        yield return new WaitForSeconds(.1f);
        _renderer.color = Color.white;
        yield return new WaitForSeconds(.1f);
        _renderer.color = Color.red;
        yield return new WaitForSeconds(.1f);
        _renderer.color = Color.white;
    }
    IEnumerator DestroyEnemyCoroutine(){
        yield return new WaitForSeconds(.5f);
        StartCoroutine(ChangeColorCoroutine());
        yield return new WaitForSeconds(.4f);
        this.gameObject.SetActive(false);
        UIManager.ins.ShowReplayGUI();
        UIManager.ins.HideQTEGUI();
    }
    private bool isStartLastAttack = false;
    public void LastAttack(){
        isStartLastAttack = true;
        animator.SetTrigger("zoom");
    }
    public void DestroyEnemy(){
        StartCoroutine(DestroyEnemyCoroutine());
    }
    
}
