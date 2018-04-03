using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class csbullethole : MonoBehaviour {
    public float speed = 0.3f;
    private float timer = 0;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        timer += Time.deltaTime;
        if (timer > 2) {
            GetComponent<Renderer>().material.color = Color.Lerp(GetComponent<Renderer>().material.color, Color.clear, speed * Time.deltaTime);
        }
        if (timer > 10) {
            Destroy(this.gameObject);
        }
	}
}
