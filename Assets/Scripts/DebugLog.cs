using System.Collections;
using UnityEngine;

namespace WIAP.Checkers
{
    /// <summary>
    /// Displays Unity's debug log messages on the screen using OnGUI.
    /// Based on code from: https://stackoverflow.com/questions/67704820/how-do-i-print-unitys-debug-log-to-the-screen-gui
    /// </summary>
    public class DebugLog : MonoBehaviour
    {
        uint qsize = 15;  // number of messages to keep
        Queue myLogQueue = new Queue();

        /// <summary>
        /// Called when the script starts.
        /// </summary>
        private void Start()
        {
            Debug.Log("Started up logging.");
        }

        /// <summary>
        /// Called when the script is enabled.
        /// Subscribes to the logMessageReceived event.
        /// </summary>
        private void OnEnable()
        {
            Application.logMessageReceived += HandleLog;
        }

        /// <summary>
        /// Called when the script is disabled.
        /// Unsubscribes from the logMessageReceived event.
        /// </summary>
        private void OnDisable()
        {
            Application.logMessageReceived -= HandleLog;
        }

        /// <summary>
        /// Handles log messages and stores them in the queue.
        /// </summary>
        /// <param name="logString">The log message.</param>
        /// <param name="stackTrace">The stack trace (if applicable).</param>
        /// <param name="type">The type of log message (error, warning, etc.).</param>
        private void HandleLog(string logString, string stackTrace, LogType type)
        {
            string formattedLog = "[" + type + "] : " + logString;

            if (!myLogQueue.Contains(formattedLog))
            {
                myLogQueue.Enqueue(formattedLog);

                if (type == LogType.Exception)
                    myLogQueue.Enqueue(stackTrace);

                while (myLogQueue.Count > qsize)
                    myLogQueue.Dequeue();
            }
        }

        /// <summary>
        /// Called to display the log messages on the screen using OnGUI.
        /// Hint: Only working in the unity preview window and not in the actual application after buildung.
        /// </summary>
        private void OnGUI()
        {
            GUILayout.BeginArea(new Rect(Screen.width - 400, 0, 400, Screen.height));
            GUILayout.Label("\n" + string.Join("\n", myLogQueue.ToArray()));
            GUILayout.EndArea();
        }
    }
}