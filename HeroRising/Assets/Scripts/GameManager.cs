using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    
    public bool startLastAttack = false;
    public static GameManager ins;
    private void Awake() {
        ins = this;
    }
    private void Start() {
        Time.timeScale = 1;
        UIManager.ins.UpdateNumberCurrent(PlayerController.ins.numberCurrent);
    }
    public void CollectItem(bool multi, int number){
        if(multi){
            PlayerController.ins.numberCurrent *= number;
        }
        else{
            PlayerController.ins.numberCurrent += number;
        }
        UIManager.ins.UpdateNumberCurrent(PlayerController.ins.numberCurrent); 
    }
    public void CollectTrap(int number){
        PlayerController.ins.ChangeColor();
        PlayerController.ins.numberCurrent -= number;
        UIManager.ins.UpdateNumberCurrent(PlayerController.ins.numberCurrent);
        if(PlayerController.ins.numberCurrent <= 0){
            PlayerController.ins.numberCurrent = 0;
            UIManager.ins.UpdateNumberCurrent(PlayerController.ins.numberCurrent);
            PlayerController.ins.DestroyPlayer();
        }
    }
    public void LoadToSSceneBoss(){
        SceneManager.LoadScene("Boss");
    }
    public void ReplayScene(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void UpdateQTE(float addFillAmount){
        UIManager.ins.fillAmount += addFillAmount;
        if(UIManager.ins.fillAmount == 1){
        
        }
    }
    public void LastAttack(){
        Debug.Log("last attack");
        StartCoroutine(LastAttackCoroutine());
    }
    IEnumerator LastAttackCoroutine(){
        UIManager.ins.HideNumber();
        yield return new WaitForSeconds(1f);
        PlayerController.ins.LastAttack();
        Enemy.ins.LastAttack();
        yield return new WaitForSeconds(2f);
        UIManager.ins.ShowQTEGUI();
    }
    
}
