using Photon.Pun;
using UnityEngine;

namespace WIAP.Checkers
{
    /// <summary>
    /// Represents a Photon user and manages Photon network operations.
    /// </summary>
    public class PhotonUser : MonoBehaviour
    {
        private PhotonView pv;
        private string username;

        /// <summary>
        /// Called when the script instance is started.
        /// </summary>
        private void Start()
        {
            pv = GetComponent<PhotonView>();

            // If this PhotonView is not mine, exit the method.
            if (!pv.IsMine) return;

            username = "User" + PhotonNetwork.NickName;
            pv.RPC("PunRPC_SetNickName", RpcTarget.AllBuffered, username);
        }

        /// <summary>
        /// RPC method to set the nickname of the user.
        /// </summary>
        /// <param name="nName">The new nickname.</param>
        [PunRPC]
        private void PunRPC_SetNickName(string nName)
        {
            gameObject.name = nName;
        }

        /// <summary>
        /// RPC method to share the Azure Anchor ID with other users.
        /// </summary>
        /// <param name="anchorId">The Azure Anchor ID to be shared.</param>
        [PunRPC]
        private void PunRPC_ShareAzureAnchorId(string anchorId)
        {
            GenericNetworkManager.Instance.azureAnchorId = anchorId;

            Debug.Log("\nPhotonUser.PunRPC_ShareAzureAnchorId()");
            Debug.Log("GenericNetworkManager.instance.azureAnchorId: " + GenericNetworkManager.Instance.azureAnchorId);
            Debug.Log("Azure Anchor ID shared by user: " + pv.Controller.UserId);
        }

        /// <summary>
        /// Shares the Azure Anchor ID with other users using an RPC call.
        /// </summary>
        public void ShareAzureAnchorId()
        {
            if (pv != null)
                pv.RPC("PunRPC_ShareAzureAnchorId", RpcTarget.AllBuffered,
                    GenericNetworkManager.Instance.azureAnchorId);
            else
                Debug.LogError("PV is null");
        }
    }
}
