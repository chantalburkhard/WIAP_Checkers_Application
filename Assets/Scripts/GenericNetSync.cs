using Photon.Pun;
using UnityEngine;

namespace WIAP.Checkers
{
    /// <summary>
    /// Handles network synchronization for game objects using Photon.
    /// </summary>
    public class GenericNetSync : MonoBehaviourPun, IPunObservable
    {
        [SerializeField] private bool isUser = default;

        private Camera mainCamera;

        private Vector3 networkLocalPosition;
        private Quaternion networkLocalRotation;

        private Vector3 startingLocalPosition;
        private Quaternion startingLocalRotation;

        /// <summary>
        /// Called to serialize and deserialize data for network synchronization.
        /// </summary>
        /// <param name="stream">The Photon stream for data transmission.</param>
        /// <param name="info">Information about the message.</param>
        void IPunObservable.OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
        {
            if (stream.IsWriting)
            {
                stream.SendNext(transform.localPosition);
                stream.SendNext(transform.localRotation);
            }
            else
            {
                networkLocalPosition = (Vector3) stream.ReceiveNext();
                networkLocalRotation = (Quaternion) stream.ReceiveNext();
            }
        }

        /// <summary>
        /// Called when the script instance is started.
        /// </summary>
        private void Start()
        {
            mainCamera = Camera.main;

            if (isUser)
            {
                if (BoardAnchor.Instance != null) transform.parent = FindObjectOfType<BoardAnchor>().transform;

                if (photonView.IsMine) GenericNetworkManager.Instance.localUser = photonView;
            }

            var trans = transform;
            startingLocalPosition = trans.localPosition;
            startingLocalRotation = trans.localRotation;

            networkLocalPosition = startingLocalPosition;
            networkLocalRotation = startingLocalRotation;
        }

        /// <summary>
        /// Called every frame to update network synchronization.
        /// </summary>
        private void Update()
        {
            if (!photonView.IsMine)
            {
                var trans = transform;
                trans.localPosition = networkLocalPosition;
                trans.localRotation = networkLocalRotation;
            }

            if (photonView.IsMine && isUser)
            {
                var trans = transform;
                var mainCameraTransform = mainCamera.transform;
                trans.position = mainCameraTransform.position;
                trans.rotation = mainCameraTransform.rotation;
            }
        }
    }
}
