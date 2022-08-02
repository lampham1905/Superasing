using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class UIManager : MonoBehaviour
{
    public static UIManager ins;
    public GameObject replayGUI;
    private void Awake() {
        ins = this;
    }
    public TextMeshProUGUI numberCurr;
    public TextMeshProUGUI numberEnemy;
    public GameObject QTEGUI;
    
    // QTE 
    public Image QTEImage;
    public Image KameImage;
    public float fillAmount = 0;
    public float fillAmountKame = 0;
    public float timeThreshold = 0;
    void Start()
    {
        
    }

    // Update is called once per frame
    public bool stopQTE = false;
    public bool stopDestroy = true;
    void Update()
    {
        if(stopQTE){
            fillAmountKame += .05f;
            KameImage.fillAmount = fillAmountKame;
        }
        
       if(GameManager.ins.startLastAttack){
         timeThreshold += Time.deltaTime;
        if(timeThreshold > .1f){
            timeThreshold = 0;
            if(!stopQTE){
                fillAmount -= .02f;
            }
        }
        if(fillAmount < 0){
            fillAmount = 0;
        }
        if(fillAmount >= 1){
            stopQTE = true;
            if(stopDestroy){
                stopDestroy = false;
                Enemy.ins.DestroyEnemy();
            }
        }
         QTEImage.fillAmount = fillAmount;
       }
       
    }
    public void UpdateNumberCurrent(int numberCurrent){
        numberCurr.text = numberCurrent.ToString();
    }
    public void UpdateNumberEnemy(int numberEnemyCurrent){
        numberEnemy.text = numberEnemyCurrent.ToString();
    }
    public void ShowReplayGUI(){
        replayGUI.SetActive(true);
        Time.timeScale = 0;
    }
    public void UpdateQTE(float addFillAmount){
        fillAmount+= addFillAmount;
         QTEImage.fillAmount = fillAmount;
        if(fillAmount == 1){
            
        }
    }
    public void ShowQTEGUI(){
        QTEGUI.SetActive(true);
        
    }
    public void HideQTEGUI(){
        QTEGUI.SetActive(false);
    }
    public void HideNumber(){
        numberCurr.enabled = false;
        numberEnemy.enabled = false;
    }
   
}
