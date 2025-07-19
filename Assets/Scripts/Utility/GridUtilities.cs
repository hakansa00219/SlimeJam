using Grid;
using UnityEngine;

namespace Utility
{
    public static class GridUtilities
    {
        public static CellData[,] ResizeArray(this CellData[,] original, int newWidth, int newHeight)
        {
            int oldWidth = original.GetLength(0);
            int oldHeight = original.GetLength(1);

            CellData[,] resized = new CellData[newWidth, newHeight];

            for (int x = 0; x < Mathf.Min(oldWidth, newWidth); x++)
            {
                for (int y = 0; y < Mathf.Min(oldHeight, newHeight); y++)
                {
                    resized[x, y] = original[x, y];
                }
            }

            return resized;
        }
    }
}