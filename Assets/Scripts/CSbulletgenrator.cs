using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CSbulletgenrator : MonoBehaviour {
    //how many bullets can shoot in per sceond.
    public int shootRate = 10;//每秒可以射击多少子弹.
    public CSFlash flash;
    public GameObject bulletPrefab;
    public Camera soldierCamera;
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
        GameObject go = Instantiate(bulletPrefab, transform.position, transform.rotation) as GameObject;
        //得到当前视野的目标
       Vector3 point= soldierCamera.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        RaycastHit hitinfo;
        //子弹碰撞到物体则视野移动被碰撞的物体
        bool isCollider = Physics.Raycast(point, soldierCamera.transform.forward, out hitinfo);
        if (isCollider)
        {
            go.transform.LookAt(hitinfo.point);
        }
        else {//如果子弹没有碰撞到物体
            point += soldierCamera.transform.forward * 1000;
            go.transform.LookAt(point);
        }
    }
}
