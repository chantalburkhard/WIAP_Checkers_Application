using UnityEngine;

namespace WIAP.Checkers
{
    /// <summary>
    /// Attaches the game object to the BoardAnchor's parent if it exists.
    /// </summary>
    public class BoardAnchorAsParent : MonoBehaviour
    {
        /// <summary>
        /// Called when the script instance is being loaded.
        /// </summary>
        private void Start()
        {
            // If a BoardAnchor instance exists, set the transform's parent to the BoardAnchor's transform.
            if (BoardAnchor.Instance != null) transform.parent = BoardAnchor.Instance.transform;
        }
    }
}
