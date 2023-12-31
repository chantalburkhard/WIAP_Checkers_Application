using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckersPiece : MonoBehaviour
{
    public bool isWhite;
    public bool isKing;

    public bool IsForceToMove(CheckersPiece[,] board, int x, int y)
    {
        if (isWhite || isKing)
        {
            // Top left
            if (x >= 2 && y <= 5)
            {
                CheckersPiece p = board[x - 1, y + 1];
                // If there is a piece, and it is not the same color as ours
                if (p != null && p.isWhite != isWhite)
                {
                    if (board[x - 2, y + 2] == null)
                        return true;
                }
            }
            // Top right
            if (x <= 5 && y <= 5)
            {
                CheckersPiece p = board[x + 1, y + 1];
                // If there is a piece, and it is not the same color as ours
                if (p != null && p.isWhite != isWhite)
                {
                    if (board[x + 2, y + 2] == null)
                        return true;
                }
            }
        }

        if (!isWhite || isKing)
        {
            // Bottom left
            if (x >= 2 && y >= 2)
            {
                CheckersPiece p = board[x - 1, y - 1];
                // If there is a piece, and it is not the same color as ours
                if (p != null && p.isWhite != isWhite)
                {
                    if (board[x - 2, y - 2] == null)
                        return true;
                }
            }
            // Bottom right
            if (x <= 5 && y >= 2)
            {
                CheckersPiece p = board[x + 1, y - 1];
                // If there is a piece, and it is not the same color as ours
                if (p != null && p.isWhite != isWhite)
                {
                    if (board[x + 2, y - 2] == null)
                        return true;
                }
            }
        }
        return false;
    }

    public bool ValidMove(CheckersPiece[,] board, int x1, int y1, int x2, int y2)
    {
        // If you are moving on top of another piece
        if (board[x2, y2] != null)
            return false;

        int deltaMoveX = Mathf.Abs(x1 - x2);
        int deltaMoveY = y2 - y1;

        if (isWhite || isKing)
        {
            if (deltaMoveX == 1)
            {
                if (deltaMoveY == 1)
                    return true;
            }
            else if (deltaMoveX == 2)
            {
                if (deltaMoveY == 2)
                {
                    CheckersPiece p = board[(x1 + x2) / 2, (y1 + y2) / 2];
                    if (p != null && p.isWhite != isWhite)
                        return true;
                }
            }
        }

        if (!isWhite || isKing)
        {
            if (deltaMoveX == 1)
            {
                if (deltaMoveY == -1)
                    return true;
            }
            else if (deltaMoveX == 2)
            {
                if (deltaMoveY == -2)
                {
                    CheckersPiece p = board[(x1 + x2) / 2, (y1 + y2) / 2];
                    if (p != null && p.isWhite != isWhite)
                        return true;
                }
            }
        }

        return false;
    }
}
