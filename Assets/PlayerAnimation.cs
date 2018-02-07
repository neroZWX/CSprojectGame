using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour {
    private CharacterController characterController;
    private Animation anim;
	// Use this for initialization
	void Start () {
        characterController = this.GetComponent<CharacterController>();
        anim = GetComponent<Animation>();
	}

    // Update is called once per frame
    void Update()
    {
        if (characterController.isGrounded == false)
        {
           anim.CrossFade("soldierFalling");
        }
        else {
            float h = Input.GetAxis("Horizontal");
            float v = Input.GetAxis("Vertical");
            if (Mathf.Abs(h) > 0.03f || Mathf.Abs(v) > 0.03f)
            {
                anim.CrossFade("soldierWalk");
            }
            else {
                anim.CrossFade("soldierIdle");
            }
        }
         
    }
	}

