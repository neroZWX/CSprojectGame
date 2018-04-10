﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour {
    public Camera camera;
    public NetworkPlayer ownerPlayer;//who create this character
    public Mouselook xMouselook;
    public Mouselook yMouselook;
   // private CharacterMotor motor;
    private PlayerAnimation playerAnimation;
    public CSbulletgenrator bulletgenrator;

	// Use this for initialization
	void Start () {
        
        //motor = this.GetComponent<CharacterMotor>();
        playerAnimation = this.GetComponent<PlayerAnimation>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    //
    public void LoseControl() {
        //motor = this.GetComponent<CharacterMotor>();
        playerAnimation = this.GetComponent<PlayerAnimation>();
        camera.gameObject.SetActive(false);//only have one CAMERA;
        //cant move
       // motor.cancontrol= false;
        playerAnimation.enabled = false;//cant run animaiton
        //can shoot
        bulletgenrator.enabled = false;
        //cant control mouse
        xMouselook.enabled= false;
        yMouselook.enabled = false;


    }
    [RPC]//declare this is RPC method to unity
    public void SetOwnerPlayer(NetworkPlayer player) {
        this.ownerPlayer = player;
        if (Network.player!= player) {
            LoseControl();//cant ccontrol if this character isnt created by you
        }
    }
}