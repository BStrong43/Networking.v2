using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class NetInit : MonoBehaviour
{
    public Toggle isServerToggle;
    public InputField IP_input;
    public InputField PortInput;
    public InputField UserName;
    public GameObject Mode;
    public bool isServer = false;
    private static NetInit instance; //Singleton makes for a simpleton
    public static NetInit Instance
    {
        get { return instance; }
    }

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        instance = this;
        //Mode.GetComponent<Dropdown>().value = 1;

    }

    public void Connect()
    {
        isServer = isServerToggle.isOn;
        if (IP_input.text == "" && !isServer)
        {
            Debug.Log("Enter IP");
            return;
        }
        if (PortInput.text == "")
        {
            Debug.Log("Enter Port");
            return;
        }

        if (isServer)
        {

            NetworkManager.Instance.initServer(int.Parse(PortInput.text), UserName.text, (NetworkManager.netMode)Mode.GetComponent<Dropdown>().value);
        }
        else
        {
            NetworkManager.Instance.initClient(IP_input.text, int.Parse(PortInput.text), UserName.text, (NetworkManager.netMode)Mode.GetComponent<Dropdown>().value);
        }
        SceneManager.LoadScene("game");
    }
}
