using UnityEngine;
using System.Collections;

public class ApplicationManager : MonoBehaviour {

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

	// Use this for initialization
	void Start () {
        bool ok = NetManager.Instance.Connect("192.168.1.222", 8888);
        if (!ok)
        {
            Debug.Log("连接服务器失败！");
            return;
        }
		Debug.Log("连接服务器成功！");

        NetManager.Instance.StartNetThread();

        LoginController.Instance().RegisterProto();
	}
	
	// Update is called once per frame
	void Update () {
        MessageDispatcher.Instance.DispatchMessage();
	}

    void OnApplicationQuit()
    {
        NetManager.Instance.Disconnect();
    }
}
