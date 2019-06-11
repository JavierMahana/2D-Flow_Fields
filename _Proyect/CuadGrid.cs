using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuadGrid
{
    public CuadGrid(int size = 8)
    {
        Size = size;

        Cells = new bool[size, size];
        for (int y = 0; y < Cells.GetLength(0); y++)
        {
            for (int x = 0; x < Cells.GetLength(1); x++)
            {
                if (x < 5 && y ==3)
                {
                    Cells[x, y] = false;
                    continue;
                }

                Cells[x, y] = true;
            }
        }


        Nodes = new FlowFieldNode[size, size];
        for (int y = 0; y < Nodes.GetLength(0); y++)
        {
            for (int x = 0; x < Nodes.GetLength(1); x++)
            {
                Nodes[x, y] = new FlowFieldNode(x,y);
            }
        }
        
    }

    public void ResetNodes()
    {
        for (int y = 0; y < Nodes.GetLength(0); y++)
        {
            for (int x = 0; x < Nodes.GetLength(1); x++)
            {
                Nodes[x, y].Reset();
            }
        }
    }

    public int Size { get; private set; }
    public bool[,] Cells;
    public FlowFieldNode[,] Nodes;
}


