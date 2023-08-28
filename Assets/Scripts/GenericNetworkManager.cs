using System;
using Photon.Pun;
using UnityEngine;

namespace WIAP.Checkers
{
    /// <summary>
    /// Manages network-related functionality for the game.
    /// </summary>
    public class GenericNetworkManager : MonoBehaviour
    {
        /// <summary>
        /// The singleton instance of the GenericNetworkManager.
        /// </summary>
        public static GenericNetworkManager Instance;

        [HideInInspector] public string azureAnchorId = "";
        [HideInInspector] public PhotonView localUser;

        /// <summary>
        /// Called when the script instance is being loaded.
        /// </summary>
        private void Awake()
        {
            // Set the singleton instance to this instance if none exists.
            if (Instance == null)
            {
                Instance = this;
            }
            // If an instance already exists and it's not this one, destroy the existing instance and set this as the new instance.
            else
            {
                if (Instance != this)
                {
                    Destroy(Instance.gameObject);
                    Instance = this;
                }
            }
            // Make this GameObject persistent across scene changes.
            DontDestroyOnLoad(gameObject);
        }

        /// <summary>
        /// Called when the script instance is started.
        /// </summary>
        private void Start()
        {
            ConnectToNetwork();
        }

        /// <summary>
        /// Connects to the network.
        /// </summary>
        private void ConnectToNetwork()
        {
            OnReadyToStartNetwork?.Invoke();
        }

        /// <summary>
        /// Event triggered when the network is ready to start.
        /// </summary>
        public static event Action OnReadyToStartNetwork;
    }
}
