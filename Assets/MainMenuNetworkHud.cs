using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class MainMenuNetworkHud : MonoBehaviour
{
    public InputField ip;
    public InputField port;

    public GameObject panelConnect;
    public GameObject panelDisconnect;

    public void ConnectToServer()
    {
        NetworkManager.singleton.networkAddress = ip.text;
        NetworkManager.singleton.networkPort = int.Parse(port.text);
        NetworkManager.singleton.StartClient();
        panelConnect.SetActive(false);
        panelDisconnect.SetActive(true);
    }

    public void CreateServer()
    {
        NetworkManager.singleton.networkPort = int.Parse(port.text);
        NetworkManager.singleton.StartHost();
        panelConnect.SetActive(false);
        panelDisconnect.SetActive(true);

    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.F12))
            Disconnect();
    }

    public void Disconnect()
    {
        NetworkManager.singleton.StopHost();
        panelConnect.SetActive(true);
        panelDisconnect.SetActive(false);
        Mouse.ShowCursor(true);
    }
}
