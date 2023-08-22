using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { set; get; }

    public GameObject mainMenu;
    public GameObject hostMenu;
    public GameObject connectMenu;

    public GameObject serverPrefab;
    public GameObject clientPrefab;

    void Start()
    {
        Instance = this;
        hostMenu.SetActive(false);
        connectMenu.SetActive(false);
        DontDestroyOnLoad(gameObject);
    }

    public void ConnectButton()
    {
        mainMenu.SetActive(false);
        connectMenu.SetActive(true);
    }

    public void HostButton()
    {
        try
        {
            Server s = Instantiate(serverPrefab).GetComponent<Server>();
            s.Init();

            Client c = Instantiate(clientPrefab).GetComponent<Client>();
            c.ConnectToServer("127.0.0.1", 6321);
        }
        catch (Exception e)
        {
            Debug.Log(e.Message);
        }
        
        mainMenu.SetActive(false);
        hostMenu.SetActive(true);
    }

    public void ConnectToServerButton()
    {
        string hostAdress = GameObject.Find("HostInput").GetComponent<TMP_InputField>().text;
        Debug.Log(hostAdress);
        // if no value is given in the text field the host address is set to local host
        if (hostAdress == "")
            hostAdress = "127.0.0.1";

        try
        {
            Client c = Instantiate(clientPrefab).GetComponent<Client>();
            c.ConnectToServer(hostAdress, 6321);
            connectMenu.SetActive(false);
        }
        catch (Exception e)
        {
            Debug.Log(e.Message);
        }
    }

    public void BackButton()
    {
        mainMenu.SetActive(true);
        hostMenu.SetActive(false);
        connectMenu.SetActive(false);
    }
}
