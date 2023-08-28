using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

namespace WIAP.Checkers
{
    /// <summary>
    /// Manages lobby-related operations and room creation using Photon.
    /// </summary>
    public class PhotonLobby : MonoBehaviourPunCallbacks
    {
        public static PhotonLobby Lobby;

        private int roomNumber = 1;
        private int userIdCount;

        /// <summary>
        /// Called when the script instance is being loaded.
        /// </summary>
        private void Awake()
        {
            if (Lobby == null)
            {
                Lobby = this;
            }
            else
            {
                if (Lobby != this)
                {
                    Destroy(Lobby.gameObject);
                    Lobby = this;
                }
            }

            DontDestroyOnLoad(gameObject);

            GenericNetworkManager.OnReadyToStartNetwork += StartNetwork;
        }

        /// <summary>
        /// Called when connected to the Photon master server.
        /// </summary>
        public override void OnConnectedToMaster()
        {
            var randomUserId = Random.Range(0, 999999);
            PhotonNetwork.AutomaticallySyncScene = false;
            PhotonNetwork.AuthValues = new AuthenticationValues();
            PhotonNetwork.AuthValues.UserId = randomUserId.ToString();
            userIdCount++;
            PhotonNetwork.NickName = PhotonNetwork.AuthValues.UserId;
            PhotonNetwork.JoinRandomRoom();
        }

        /// <summary>
        /// Attempts to join a specific room.
        /// </summary>
        public void JoinRoom()
        {
            PhotonNetwork.JoinRoom("Room");
        }

        /// <summary>
        /// Called when successfully joined a room.
        /// </summary>
        public override void OnJoinedRoom()
        {
            base.OnJoinedRoom();

            Debug.Log("Current room name: " + PhotonNetwork.CurrentRoom.Name);
            Debug.Log("Other players in room: " + PhotonNetwork.CountOfPlayersInRooms);
            Debug.Log("Total players in room: " + (PhotonNetwork.CountOfPlayersInRooms + 1));
        }

        /// <summary>
        /// Called when a player leaves the room.
        /// </summary>
        /// <param name="otherPlayer">The player who left the room.</param>
        public override void OnPlayerLeftRoom(Player otherPlayer)
        {
            base.OnPlayerLeftRoom(otherPlayer);
            
            Debug.Log("Player left room");

        }

        /// <summary>
        /// Called when an attempt to join a random room fails.
        /// </summary>
        /// <param name="returnCode">The return code of the operation.</param>
        /// <param name="message">The error message.</param>
        public override void OnJoinRandomFailed(short returnCode, string message)
        {
            CreateRoom();
        }

        /// <summary>
        /// Called when room creation fails.
        /// </summary>
        /// <param name="returnCode">The return code of the operation.</param>
        /// <param name="message">The error message.</param>
        public override void OnCreateRoomFailed(short returnCode, string message)
        {
            Debug.Log("\nPhotonLobby.OnCreateRoomFailed()");
            Debug.LogError("Creating Room Failed");
            CreateRoom();
        }

        /// <summary>
        /// Called when a room is created successfully.
        /// </summary>
        public override void OnCreatedRoom()
        {
            base.OnCreatedRoom();
            roomNumber++;
        }

        /// <summary>
        /// Called when the cancel button is clicked to leave the room.
        /// </summary>
        public void OnCancelButtonClicked()
        {
            PhotonNetwork.LeaveRoom();
        }

        /// <summary>
        /// Starts the network connection to Photon.
        /// </summary>
        private void StartNetwork()
        {
            PhotonNetwork.ConnectUsingSettings();
            Lobby = this;
        }

        /// <summary>
        /// Creates a room with specified options.
        /// </summary>
        private void CreateRoom()
        {
            var roomOptions = new RoomOptions {IsVisible = true, IsOpen = true, MaxPlayers = 4};
            PhotonNetwork.CreateRoom("Room", roomOptions);
        }
    }
}
