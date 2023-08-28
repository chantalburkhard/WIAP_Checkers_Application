using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;
using System.Collections;

namespace WIAP.Checkers.Test 
{
    public class PUN_Controller : MonoBehaviourPunCallbacks
    {
        /// <summary>
        /// Called when the script starts.
        /// </summary>
        private void Start()
        {
            // Connect to Photon when the script starts.
            ConnectToPhoton();
        }

        /// <summary>
        /// Connects to the Photon server using the configured settings.
        /// </summary>
        public void ConnectToPhoton()
        {
            if (!PhotonNetwork.IsConnected)
            {
                PhotonNetwork.ConnectUsingSettings();
            }
        }

        /// <summary>
        /// Joins a single-player room by creating a new room and then joining it.
        /// </summary>
        public void JoinSinglePlayer()
        {
            if (PhotonNetwork.IsConnectedAndReady)
            {
                PhotonNetwork.CreateRoom("Single Player Checkers", new RoomOptions { MaxPlayers = 2 });
                PhotonNetwork.JoinRoom("Single Player Checkers");
            }
            else
            {
                Debug.LogError("Not connected to photon or not ready for operations.");
            }
        }

        /// <summary>
        /// Joins a multiplayer room named "Checkers Room".
        /// </summary>
        public void JoinMultiplayer()
        {
            if (PhotonNetwork.IsConnectedAndReady)
            {
                PhotonNetwork.JoinRoom("Checkers Room");
            }
            else
            {
                Debug.LogError("Not connected to photon or not ready for operations.");
            }
        }

        /// <summary>
        /// Called when successfully connected to the Photon Master Server.
        /// </summary>
        public override void OnConnectedToMaster()
        {
            Debug.Log("Connected to Photon Master Server");
        }

        /// <summary>
        /// Called when the player successfully joins a room.
        /// Loads the "Checkers" scene after joining.
        /// </summary>
        public override void OnJoinedRoom()
        {
            Debug.Log("Joined Room: " + PhotonNetwork.CurrentRoom.Name);
            LoadGameScene();
        }

        /// <summary>
        /// Called when a room join attempt fails.
        /// </summary>
        /// <param name="returnCode">The error code.</param>
        /// <param name="message">The error message.</param>
        public override void OnJoinRoomFailed(short returnCode, string message)
        {
            Debug.LogError("Failed to join room: " + message);
            Debug.Log("Please remember to start the Server_Start_Application before trying to connect to the game");
        }

        /// <summary>
        /// Loads the "Checkers" scene.
        /// </summary>
        private void LoadGameScene()
        {
            SceneManager.LoadScene("Checkers", LoadSceneMode.Single);
        }
    }
}