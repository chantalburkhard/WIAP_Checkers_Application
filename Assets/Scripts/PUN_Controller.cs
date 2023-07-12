using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

namespace Com.WIAP.Checkers{
    public class PUN_Controller : MonoBehaviourPunCallbacks
    {
        //private bool isConnecting = false;

        private void Start()
        {
            ConnectToPhoton();
        }

        public void ConnectToPhoton()
        {
            if (!PhotonNetwork.IsConnected)
            {
                //isConnecting = true;
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

        public override void OnJoinRoomFailed(short returnCode, string message)
        {
            Debug.LogError("Failed to join room: " + message);
        }
    }
}

