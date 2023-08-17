using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Com.WIAP.Checkers
{
    public class CheckersBoard : MonoBehaviour
    {
        public CheckersPiece[,] pieces = new CheckersPiece[8, 8];
        public GameObject whitePiece;
        public GameObject blackPiece;

        private Vector3 boardOffset = new Vector3(-4.0f, 0, -4.0f);
        private Vector3 pieceOffset = new Vector3(0.5f, 0, 0.5f);

        // Start is called before the first frame update
        void Start()
        {
            GenerateBoard();
        }

        private void GenerateBoard()
        {
            // Generate White Team
            for (int y = 0; y < 3; y++)
            {
                bool oddRow = (y % 2 == 0);
                for (int x = 0; x < 8; x += 2)
                {
                    // Generate Piece
                    GeneratePiece((oddRow) ? x : x + 1, y);
                }
            }

            // Generate Black Team
            for (int y = 7; y > 4; y--)
            {
                bool oddRow = (y % 2 == 0);
                for (int x = 0; x < 8; x += 2)
                {
                    // Generate Piece
                    GeneratePiece((oddRow) ? x : x + 1, y);
                }
            }
        }

        private void GeneratePiece(int x, int y)
        {
            bool isPieceWhite = (y > 3) ? false : true;
            GameObject gameObj = Instantiate((isPieceWhite) ? whitePiece : blackPiece) as GameObject;
            gameObj.transform.SetParent(transform);
            CheckersPiece piece = gameObj.GetComponent<CheckersPiece>();
            pieces[x, y] = piece;
            MovePiece(piece, x, y);
        }

        private void MovePiece(CheckersPiece p, int x, int y)
        {
            p.transform.position = (Vector3.right * x) + (Vector3.forward * y) + boardOffset + pieceOffset;
        }
    }
}
