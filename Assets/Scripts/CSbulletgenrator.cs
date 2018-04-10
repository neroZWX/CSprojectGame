using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CSbulletgenrator : MonoBehaviour {
    //how many bullets can shoot in per sceond.
    public int shootRate = 10;//每秒可以射击多少子弹.
    public CSFlash flash;
    public GameObject bulletPrefab;
    private float timer = 0;
    private bool isFiring = false;//default is not fire
	// Use this for initialization
	
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0)) {
            isFiring = true;
        }
        if (Input.GetMouseButtonUp(0)) {
            isFiring = false;
        }
        if (isFiring) {
            timer += Time.deltaTime;
            if (timer > 1f / shootRate) {
                Shoot();
                timer = 0;
                //timer -= 1f / shootRate;
            }
        }
	}
    void Shoot() {
        //flash.Flash();
        GameObject.Instantiate(bulletPrefab, transform.position, transform.rotation);
    }
}
