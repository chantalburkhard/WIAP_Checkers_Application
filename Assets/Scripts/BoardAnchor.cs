using UnityEngine;

namespace WIAP.Checkers
{
    /// <summary>
    /// Represents an anchor point for the game board.
    /// </summary>
    public class BoardAnchor : MonoBehaviour
    {
        /// <summary>
        /// The singleton instance of the BoardAnchor.
        /// </summary>
        public static BoardAnchor Instance;

        /// <summary>
        /// Called when the script instance is being loaded.
        /// </summary>
        private void Start()
        {
            // If there's no existing instance, set this instance as the singleton instance.
            if (Instance == null)
            {
                Instance = this;
            }
            // If an instance already exists and it's not this one, destroy the existing instance and set this as the new instance.
            else
            {
                if (Instance == this) return;
                Destroy(Instance.gameObject);
                Instance = this;
            }
        }
    }
}
