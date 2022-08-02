using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class QTEbtn : Button
{
    
     public override void OnPointerDown(PointerEventData eventData)
    {
         base.OnPointerDown(eventData);
        if(GameManager.ins.startLastAttack){
            GameManager.ins.UpdateQTE(.1f);
            Debug.Log("press");
        }
        
}
}
