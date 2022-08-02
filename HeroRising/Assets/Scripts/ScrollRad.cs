using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollRad : MonoBehaviour
{
    public static ScrollRad ins;
    private void Awake() {
        ins = this;
    }
    public bool stop = false;
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!stop){
            transform.Translate(Vector3.down * speed * Time.deltaTime);
        }
    }
}
