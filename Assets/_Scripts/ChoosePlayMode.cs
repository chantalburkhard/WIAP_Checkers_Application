using UnityEngine;
using TMPro;
using Photon.Pun;


namespace Com.WIAP.Checkers
{
    public class ChoosePlayMode : MonoBehaviourPunCallbacks
    {
        public TextMeshPro InfoText;
        public GameObject SinglePlayerButton;
        public GameObject MultiPlayerButton;

        void Start()
        {
            ConnectToPhoton();
        }

        public void ConnectToPhoton()
        {
            if(!PhotonNetwork.IsConnected)
            {
                Debug.Log("Connecting to network");
                PhotonNetwork.ConnectUsingSettings();
            }
        }
        public void JoinPhotonRoom()
        {
            PhotonNetwork.JoinRoom("Checkers Room");
        }

        public override void OnJoinedRoom()
        {
            Debug.Log("Joined Room: " + PhotonNetwork.CurrentRoom.Name);
        }

        public override void OnJoinRandomFailed(short returnCode, string message)
        {
            Debug.LogError("Failed to join room: " + message);
            Debug.Log("Please remember to start the Server_Start_Application before trying to connect to the game");
        }

        public void SingleClick()
        {
           
        }

        public void MutliClick()
        {
           
        }
    }
}