using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {
    public GameObject bgGameObject;
    public GameObject logoGameObject;
    public GameObject menuGameObject;
    public GameObject gameoverpanelGameObject;
    public static GameController _instance;
    public UILabel gameoverlabel;
    public UIInput ipInput;
    public GameObject uiRoot;
    public Transform pos1;
    public Transform pos2;
    public GameObject soldierPrefab;
    public int connections = 10;//最大连接数
    public int listenPort = 8899;
    public AudioSource BGMenuMusic;
    public AudioSource BGPlayMusic;
    //当服务器按钮按下的时候运行
    public void OnButtonCreateServerClick() {
        Network.InitializeServer(connections, listenPort);//创建服务器
        uiRoot.SetActive(false);
        PlayMusic();
    }
    private void Awake()
    {
        _instance = this;
    }
    //当按下链接服务器按钮的时候运行
    public void OnButtonConnectServerClick()
    {
        Network.Connect(ipInput.value, listenPort);//连接服务器
        uiRoot.SetActive(false);
        PlayMusic();

    }
    void PlayMusic() {
        BGMenuMusic.Stop();
        BGPlayMusic.Play();
    }
    void OnServerInitialized()
    {
        //GameObject.Instantiate(soldierPrefab, pos1.position, Quaternion.identity);
        NetworkPlayer player= Network.player;//创建角色
        int group = int.Parse(player + "");
        GameObject go = Network.Instantiate(soldierPrefab, pos1.position, Quaternion.identity, group) as GameObject;
        go.GetComponent<player>().SetOwnerPlayer(Network.player);//这代码 只设置当前创建者中的角色的OWNERPlayer属性。其他客户端这个属性是为空。
        //RPC：远程调用
        go.GetComponent<NetworkView>().RPC("SetOwnerPlayer", RPCMode.AllBuffered, Network.player);//完成一个远程调用，会执行所有连接客户端上的SETOWNERplayer方法；
        Cursor.visible = false;//游戏开始隐藏鼠标
    }
    void OnConnectedToServer() {
        NetworkPlayer player = Network.player;//创建角色
        int group = int.Parse(player + "");
        GameObject go = Network.Instantiate(soldierPrefab, pos2.position, Quaternion.identity, group) as GameObject;//得到角色位置
        //RPC：远程调用
        go.GetComponent<NetworkView>().RPC("SetOwnerPlayer", RPCMode.AllBuffered, Network.player);//完成一个远程调用，会执行所有连接客户端上的SETOWNERplayer方法；
        Cursor.visible = false;//游戏开始隐藏鼠标
    }
   public void OnQuitButtonClick() {
        Application.Quit();//EXIT GAME
    }
    public void ShowGameOver(bool isWin) {
        //Game over interface
        uiRoot.gameObject.SetActive(true);
        bgGameObject.SetActive(false);
        logoGameObject.SetActive(false);
        gameoverpanelGameObject.SetActive(true);
        menuGameObject.SetActive(false);
        if (isWin)
        {
            Cursor.visible = true;
            gameoverlabel.text = "Victory";
        }
        else {
            Cursor.visible = true;
            gameoverlabel.text = "Defeat";
        }
        }
    }

