using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    public class CheckersBoard : MonoBehaviour
    {
        public static CheckersBoard Instance { set; get; }

        // Public Variables
        public CheckersPiece[,] pieces = new CheckersPiece[8, 8];
        public GameObject whitePiece;
        public GameObject blackPiece;

        public bool isWhite;

        private Vector3 boardOffset = new Vector3(-4.0f, 0, -4.0f);
        private Vector3 pieceOffset = new Vector3(0.5f, 0, 0.5f);

        // Private Variables
        private CheckersPiece selectedPiece;
        private List<CheckersPiece> forcedPieces;

        private bool isWhiteTurn;
        private bool hasKilled;

        private Vector2 mouseOver;
        private Vector2 startDrag;
        private Vector2 endDrag;

        private Client client;

        // Start is called before the first frame update
        private void Start()
        {
            Instance = this;

            client = FindObjectOfType<Client>();
            isWhite = client.isHost;

            // always start with white
            isWhiteTurn = true;
            GenerateBoard();
            forcedPieces = new List<CheckersPiece>();   
        }

        private void Update()
        {
            UpdateMouseOver();

            // Check which turn it is
            if ((isWhite) ? isWhiteTurn : !isWhiteTurn)
            {
                int x = (int)mouseOver.x;
                int y = (int)mouseOver.y;

                if (selectedPiece != null)
                    UpdatePieceDrag(selectedPiece);

                if (Input.GetMouseButtonDown(0))
                    SelectPiece(x, y);

                if (Input.GetMouseButtonUp(0)) 
                    TryMove((int)startDrag.x, (int)startDrag.y, x, y);
            }
        }

        private void UpdateMouseOver()
        {
            if (!Camera.main)
            {
                Debug.Log("Unable to find main camera");
                return;
            }

            RaycastHit hit;
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 25.0f, LayerMask.GetMask("Board")))
            {
                mouseOver.x = (int) (hit.point.x - boardOffset.x);
                mouseOver.y = (int) (hit.point.z - boardOffset.z);
            }
            else
            {
                mouseOver.x = -1;
                mouseOver.y = -1;
            }
        }

        private void UpdatePieceDrag(CheckersPiece p)
        {
            if (!Camera.main)
            {
                Debug.Log("Unable to find main camera");
                return;
            }

            RaycastHit hit;
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 25.0f, LayerMask.GetMask("Board")))
            {
                p.transform.position = hit.point + Vector3.up;
            }
        }

        private void SelectPiece(int x, int y)
        {
            // Out of bounds
            if (x < 0 || x >= 8 || y < 0 || y >= 8)
                return;

            CheckersPiece p = pieces[x, y];

            if (p != null && p.isWhite == isWhite)
            {
                if (forcedPieces.Count == 0)
                {
                    selectedPiece = p;
                    startDrag = mouseOver;
                }
                else
                {
                    // Look for the piece under our forced pieces list
                    if (forcedPieces.Find(fp => fp == p) == null)
                        return;

                    selectedPiece = p;
                    startDrag = mouseOver;
                }
            }
        }

        public void TryMove(int x1, int y1, int x2, int y2)
        {
            forcedPieces = ScanForPossibleMove();

            // Multiplayer Support
            startDrag = new Vector2(x1, y1);
            endDrag = new Vector2(x2, y2);
            selectedPiece = pieces[x1, y1];

            // Check if we are out of bounds
            if (x2 < 0 || x2 >= 8 || y2 < 0 || y2 >= 8)
            {
                if (selectedPiece != null)
                    MovePiece(selectedPiece, x1, y1);

                startDrag = Vector2.zero;
                selectedPiece = null;
                return;
            }

            // Check if  there is a selected Piece
            if (selectedPiece != null)
            {
                // If it has not moved
                if (endDrag == startDrag)
                {
                    MovePiece(selectedPiece, x1, y1);
                    startDrag = Vector2.zero;
                    selectedPiece = null;
                    return;
                }

                // Check if it is a valid move
                if (selectedPiece.ValidMove(pieces, x1, y1, x2, y2))
                {
                    // Did we kill anything
                    // If this is a jump
                    if (Mathf.Abs(x2 - x1) == 2)
                    {
                        CheckersPiece p = pieces[(x1 + x2) / 2, (y1 + y2) / 2];
                        if (p != null)
                        {
                            pieces[(x1 + x2) / 2, (y1 + y2) / 2] = null;
                            DestroyImmediate(p.gameObject);
                            hasKilled = true;
                        }
                    }

                    // Were we supposed to kill anything?
                    if (forcedPieces.Count != 0 && !hasKilled)
                    {
                        MovePiece(selectedPiece, x1, y1);
                        startDrag = Vector2.zero;
                        selectedPiece = null;
                        return;
                    }

                    pieces[x2, y2] = selectedPiece;
                    pieces[x1, y1] = null;
                    MovePiece(selectedPiece, x2, y2);

                    EndTurn();
                }
                else
                {
                    MovePiece(selectedPiece, x1, y1);
                    startDrag = Vector2.zero;
                    selectedPiece = null;
                    return;
                }
            }
        }

        private void EndTurn()
        {
            int x = (int)endDrag.x;
            int y = (int)endDrag.y;

            // Promotions
            if (selectedPiece != null)
            {
                if (selectedPiece.isWhite && !selectedPiece.isKing && y == 7)
                {
                    selectedPiece.isKing = true;
                    selectedPiece.transform.Rotate(Vector3.right * 180);
                }
                else if (!selectedPiece.isWhite && !selectedPiece.isKing && y == 0)
                {
                    selectedPiece.isKing = true;
                    selectedPiece.transform.Rotate(Vector3.right * 180);
                }
            }

            string msg = "CMOV|";
            msg += startDrag.x.ToString() + '|';
            msg += startDrag.y.ToString() + '|';
            msg += endDrag.x.ToString() + '|';
            msg += endDrag.y.ToString();

            client.Send(msg);

            selectedPiece = null;
            startDrag = Vector2.zero;

            if (ScanForPossibleMove(selectedPiece, x, y).Count != 0 && hasKilled)
                return;

            isWhiteTurn = !isWhiteTurn;
            // Switch team after every turn (local play mode)
            //isWhite = !isWhite;
            hasKilled = false;
            CheckVictory();
        }

        private void CheckVictory()
        {
            var ps = FindObjectsOfType<CheckersPiece>();
            bool hasWhite = false, hasBlack = false;

            for (int i = 0; i < ps.Length; i++)
            {
                if (ps[i].isWhite)
                    hasWhite = true;
                else
                    hasBlack = true;
            }

            if (!hasWhite)
                Victory(false);
            if (!hasBlack)
                Victory(true);
        }

        private void Victory(bool isWhite)
        {
            if (isWhite)
                Debug.Log("White team has won");
            else
                Debug.Log("Black team has won");
        }

        private List<CheckersPiece> ScanForPossibleMove()
        {
            forcedPieces = new List<CheckersPiece>();

            // Check all the pieces
            for (int i = 0; i < 8; i++)
                for (int j = 0; j < 8; j++)
                    if (pieces[i, j] != null && pieces[i, j].isWhite == isWhiteTurn)
                        if (pieces[i, j].IsForceToMove(pieces, i, j))
                            forcedPieces.Add(pieces[i, j]);

            return forcedPieces;
        }

        private List<CheckersPiece> ScanForPossibleMove(CheckersPiece p, int x, int y)
        {
            forcedPieces = new List<CheckersPiece>();

            if (pieces[x, y].IsForceToMove(pieces, x, y))
                forcedPieces.Add(pieces[x, y]);

            return forcedPieces;
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
