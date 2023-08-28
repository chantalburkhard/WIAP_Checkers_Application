using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace WIAP.Checkers
{
    /// <summary>
    /// Manages the game modes for the Checkers game.
    /// </summary>
    public class GameModeManager : MonoBehaviour
    {
        /// <summary>
        /// Loads the single player Checkers scene.
        /// </summary>
        public void SinglePlayer()
        {
            SceneManager.LoadScene("CheckersSingle");
        }

        /// <summary>
        /// Loads the multi-player Checkers scene.
        /// </summary>
        public void MultiPlayer()
        {
            SceneManager.LoadScene("CheckersMulti");
        }
    }
}
