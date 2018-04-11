using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour {
    private CharacterController characterController;
    private player player;
    private bool havePlayDieAnimation = false;//是否已经播放过死亡动画
    //private Animation anim;
    // Use this for initialization
    void Start() {
        characterController = this.GetComponent<CharacterController>();
        player=this.GetComponent<player>();
       // anim = GetComponent<Animation>();
    }

    // Update is called once per frame
    void Update()
    {
        if(player.hp>0){
            if (characterController.isGrounded == false)
            //播放下落的动画
            {
                //远程调用 玩家与玩家之间的动画同步
                GetComponent<NetworkView>().RPC("PlayState", RPCMode.All, "soldierFalling");
                PlayState("soldierFalling");
            }
            else
            {
                float h = Input.GetAxis("Horizontal");
                float v = Input.GetAxis("Vertical");
                if (Mathf.Abs(h) > 0.03f || Mathf.Abs(v) > 0.03f)
                {
                    //PlayState("soldierWalk");
                    GetComponent<NetworkView>().RPC("PlayState", RPCMode.All, "soldierWalk");
                }
                else
                {
                    // PlayState("soldierIdle");
                    GetComponent<NetworkView>().RPC("PlayState", RPCMode.All, "soldierIdle");
                }
            }
        }else{
            if (havePlayDieAnimation == false) {
                GetComponent<NetworkView>().RPC("PlayState", RPCMode.All, "soldierDieFront");
                havePlayDieAnimation = true;//只播放一次
            }
        }
    }
        [RPC]
    void PlayState(string animName) {
        GetComponent<Animation>().CrossFade(animName, 0.2f);

    }
}

