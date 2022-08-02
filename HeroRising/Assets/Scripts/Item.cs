using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Item : MonoBehaviour
{
    public TextMeshProUGUI numberItem;
    public int number;
    public bool multi;
    public GameObject ItemObject;
   private void Start() {
        if(multi){
            numberItem.text = "*" + number.ToString();
        }  
        else numberItem.text = "+" + number.ToString();
   }
    private void OnCollisionEnter2D(Collision2D other) {
         if(other.gameObject.CompareTag("player")){
            GameManager.ins.CollectItem(multi, number);
            Destroy(this.gameObject);
        }
    }
}
