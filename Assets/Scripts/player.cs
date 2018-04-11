using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour {
    public Camera camera;
    public NetworkPlayer ownerPlayer;//who create this character
    public Mouselook xMouselook;
    public Mouselook yMouselook;
    public float hp = 100;//主角的血量
   //private CharacterMotor motor;
    private PlayerAnimation playerAnimation;
    public CSbulletgenrator bulletgenrator;

	// Use this for initialization
	void Start () {
        
        //motor = this.GetComponent<CharacterMotor>();
        playerAnimation = this.GetComponent<PlayerAnimation>();
	}
	
	// Update is called once per frame
	void Update () {
        if (hp <= 0) {
            bool isWin = false;
            if (ownerPlayer != Network.player) {
                isWin = true;
            }
            GameController._instance.ShowGameOver(isWin);
        }
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
    public void TakeDamage(float damage) {
        hp -= damage;
       GetComponent<NetworkView>().RPC("SysncHp", RPCMode.All, hp);
    }
    [RPC]
    public void SysncHP() {
        //同步血量
        this.hp = hp;
    }
}
