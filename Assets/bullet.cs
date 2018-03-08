using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour {

    public float speed = 800;
	// Update is called once per frame
	void Update () {
        Vector3 oriPos = transform.position;
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
        Vector3 direction = transform.position - oriPos;
        float length = (transform.position - oriPos).magnitude;
        RaycastHit hitinfo;//存储碰撞信息
       bool isCollider= Physics.Raycast(oriPos, direction, out hitinfo,length);
        if (isCollider) {
            //如果射线碰撞到物体要做的动作
        }
	}
}
