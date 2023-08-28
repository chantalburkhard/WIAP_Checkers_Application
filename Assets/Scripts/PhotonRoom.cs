using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

namespace WIAP.Checkers
{
    /// <summary>
    /// Manages room-specific operations and game initialization using Photon.
    /// Needs to be updated in the future to optimize the piece positions, player avatar, etc.
    /// </summary>
    public class PhotonRoom : MonoBehaviourPunCallbacks, IInRoomCallbacks
    {
        public static PhotonRoom Room;

        [SerializeField] private GameObject photonUserPrefab = default;
        [SerializeField] private GameObject CheckersPieceBlackPrefab = default;
        [SerializeField] private GameObject CheckersPieceWhitePrefab = default;
        [SerializeField] private Transform CheckersPieceLocation = default;

        // private PhotonView pv;
        private Player[] photonPlayers;
        private int playersInRoom;
        private int myNumberInRoom;

        /// <summary>
        /// Called when a player enters the room.
        /// </summary>
        /// <param name="newPlayer">The new player who entered the room.</param>
        public override void OnPlayerEnteredRoom(Player newPlayer)
        {
            base.OnPlayerEnteredRoom(newPlayer);
            photonPlayers = PhotonNetwork.PlayerList;
            playersInRoom++;
        }

        /// <summary>
        /// Called when the script instance is being loaded.
        /// </summary>
        private void Awake()
        {
            if (Room == null)
            {
                Room = this;
            }
            else
            {
                if (Room != this)
                {
                    Destroy(Room.gameObject);
                    Room = this;
                }
            }
        }

        /// <summary>
        /// Called when the script instance is enabled.
        /// </summary>
        public override void OnEnable()
        {
            base.OnEnable();
            PhotonNetwork.AddCallbackTarget(this);
        }

        /// <summary>
        /// Called when the script instance is disabled.
        /// </summary>
        public override void OnDisable()
        {
            base.OnDisable();
            PhotonNetwork.RemoveCallbackTarget(this);
        }

        /// <summary>
        /// Called when the script instance is started.
        /// </summary>
        private void Start()
        {
            // Allow prefabs not in a Resources folder
            if (PhotonNetwork.PrefabPool is DefaultPool pool)
            {
                if (photonUserPrefab != null) pool.ResourceCache.Add(photonUserPrefab.name, photonUserPrefab);

                if (CheckersPieceBlackPrefab != null) pool.ResourceCache.Add(CheckersPieceBlackPrefab.name, CheckersPieceBlackPrefab);

                if (CheckersPieceBlackPrefab != null) pool.ResourceCache.Add(CheckersPieceWhitePrefab.name, CheckersPieceWhitePrefab);
            }
        }

        /// <summary>
        /// Called when the local player successfully joins a room.
        /// </summary>
        public override void OnJoinedRoom()
        {
            base.OnJoinedRoom();

            photonPlayers = PhotonNetwork.PlayerList;
            playersInRoom = photonPlayers.Length;
            myNumberInRoom = playersInRoom;
            PhotonNetwork.NickName = myNumberInRoom.ToString();

            StartGame();
        }

        /// <summary>
        /// Initiates the game setup.
        /// </summary>
        private void StartGame()
        {
            CreatePlayer();

            if (!PhotonNetwork.IsMasterClient) return;

            if (BoardAnchor.Instance != null) CreateInteractableObjects();
        }

        /// <summary>
        /// Creates the player's avatar.
        /// </summary>
        private void CreatePlayer()
        {
            var player = PhotonNetwork.Instantiate(photonUserPrefab.name, Vector3.zero, Quaternion.identity);
        }

        /// <summary>
        /// Creates the interactable objects for the game.
        /// </summary>
        private void CreateInteractableObjects()
        {
            var position = CheckersPieceLocation.position;
            // Needs to be updated in the future to optimize the piece positions for a better user experience
            var positionOnTopOfSurface = new Vector3(position.x, position.y, position.z);
            
            // Black Checkers Piece
            var gOBlack = PhotonNetwork.Instantiate(CheckersPieceBlackPrefab.name, positionOnTopOfSurface,
                CheckersPieceLocation.rotation);

            // White Checkers Piece
            var gOWhite = PhotonNetwork.Instantiate(CheckersPieceWhitePrefab.name, positionOnTopOfSurface,
                CheckersPieceLocation.rotation);
        }
    }
}
