using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CSFlash : MonoBehaviour {
    public Material[] mats;
    public float flashTime = 0.1f;//闪光时间
    private float flashTimer = 0;
    private int index =0;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0)) {
            Flash();
        }
            //FOR flash time
        if (GetComponent<Renderer>().enabled) {
            flashTimer += Time.deltaTime;
            if (flashTimer > flashTime) {
                GetComponent<Renderer>().enabled = false;
            }
        }
	}
    void Flash(){
        index++;
        index %= 4;
        GetComponent<Renderer>().enabled = true;
        GetComponent<Renderer>().material = mats[index];
        flashTimer = 0;
    }
}
