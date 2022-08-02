using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Trap : MonoBehaviour
{
   public TextMeshProUGUI numberTrap;
   public  int number;
   
   private void Start() {
        numberTrap.text = "-" + number.ToString();
   }
   private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.CompareTag("player")){
            GameManager.ins.CollectTrap(number);
            Destroy(this.gameObject);
        }
   }
}
