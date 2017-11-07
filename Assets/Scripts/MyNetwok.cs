using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyNetwok : MonoBehaviour {
    public int connections = 10;
    public int listenport = 8899;
    public bool useNat = false;
    public string ip = "127.0.0.1";
    public GameObject playerPrefab;
    

    void OnGUI()
    {  
       if(Network.peerType==NetworkPeerType.Disconnected)//当当前状态为未连接那么会显示以下的按键。
        if (GUILayout.Button("Create Server"))
        {
            // Create sevice 
            NetworkConnectionError error= Network.InitializeServer(connections, listenport, useNat);
            //print(error);
        }
        if (GUILayout.Button("Connecte Server"))
        {
            NetworkConnectionError error = Network.Connect(ip, listenport);
            print(error);

        }



        else if (Network.peerType == NetworkPeerType.Server)
        {//当前状态为连接服务器那么显示以下内容。
            GUILayout.Label("Server has been created");
        }
        else if (Network.peerType == NetworkPeerType.Client) {
            GUILayout.Label("client connected");
        }
    }
     void OnServerInitialized()//显示在SErVER
    {
        print("Completed initialize");
        //Network.player;//访问到当前的客户端
        int group = int.Parse(Network.player + "");//直接访问network.player会得到客户端的索引
        Network.Instantiate(playerPrefab, new Vector3(0, 10, 0), Quaternion.identity,group);
    }
   void OnPlayerConnected(NetworkPlayer player)//显示在SErVER
    {
        print("A New player has been connected,Index number:" + player);
    }
    void OnConnectedToServer()//显示在CLIENT
    {
        print("Successful connected");
        int group = int.Parse(Network.player + "");//直接访问network.player会得到客户端的索引
        Network.Instantiate(playerPrefab, new Vector3(0, 10, 0), Quaternion.identity, group);
    }
}
