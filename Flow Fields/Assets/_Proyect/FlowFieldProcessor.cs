using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class FlowFieldProcessor 
{

    public static FlowDirection[,] CalculateFlowFieldValues(CuadGrid grid, int xDest, int yDest)
    {
        if (!grid.Cells[xDest, yDest])
        {
            Debug.Log("invalid cell");
            return null;
        }

        int gridSize = grid.Size;
        FlowDirection[,] outputDirection = new FlowDirection[gridSize, gridSize];

        grid.ResetNodes();
        

        FlowFieldNode startNode = grid.Nodes[xDest, yDest];
        startNode.costSoFar = 0;
        startNode.FromNodeDirection = FlowDirection.NONE;
        outputDirection[startNode.XCoord, startNode.YCoord] = startNode.FromNodeDirection;

        List<FlowFieldNode> openList = new List<FlowFieldNode>(gridSize * gridSize);
        List<FlowFieldNode> closedList = new List<FlowFieldNode>(gridSize * gridSize);

        openList.Add(startNode);
        FlowFieldNode currentNode;
        List<FlowFieldNode> neighBoursToProcess = new List<FlowFieldNode>();


        while (openList.Count > 0)
        {
            currentNode = SmallestNode(openList);
            openList.Remove(currentNode);
            closedList.Add(currentNode);


            bool neighbourRigth = false;
            bool neighbourUp = false;
            bool neighbourLeft = false;
            bool neighbourDown = false;

            //set adyacent neighbours
            if (currentNode.XCoord < gridSize - 1)
            {
                neighbourRigth = grid.Cells[currentNode.XCoord + 1, currentNode.YCoord];
                if (neighbourRigth)
                {
                    neighBoursToProcess.Add(grid.Nodes[currentNode.XCoord + 1, currentNode.YCoord]);
                }
            }


            if (currentNode.YCoord < gridSize - 1)
            {
                neighbourUp = grid.Cells[currentNode.XCoord, currentNode.YCoord + 1];
                if (neighbourUp)
                {
                    neighBoursToProcess.Add(grid.Nodes[currentNode.XCoord, currentNode.YCoord + 1]);
                }
            }


            if (currentNode.XCoord > 0)
            {
                neighbourLeft = grid.Cells[currentNode.XCoord - 1, currentNode.YCoord];
                if (neighbourLeft)
                {
                    neighBoursToProcess.Add(grid.Nodes[currentNode.XCoord - 1, currentNode.YCoord]);
                }
            }


            if (currentNode.YCoord > 0)
            {
                neighbourDown = grid.Cells[currentNode.XCoord, currentNode.YCoord - 1];
                if (neighbourDown)
                {
                    neighBoursToProcess.Add(grid.Nodes[currentNode.XCoord, currentNode.YCoord - 1]);
                }
            }

            ProcessNeightBours(outputDirection, openList, closedList, currentNode, neighBoursToProcess, false);
            neighBoursToProcess.Clear();


            //set diagonal neighbours
            if (neighbourRigth && neighbourUp && grid.Cells[currentNode.XCoord + 1, currentNode.YCoord + 1])
            {
                neighBoursToProcess.Add(grid.Nodes[currentNode.XCoord + 1, currentNode.YCoord + 1]);
            }
            if (neighbourLeft && neighbourUp && grid.Cells[currentNode.XCoord - 1, currentNode.YCoord + 1])
            {
                neighBoursToProcess.Add(grid.Nodes[currentNode.XCoord - 1, currentNode.YCoord + 1]);
            }
            if (neighbourLeft && neighbourDown && grid.Cells[currentNode.XCoord - 1, currentNode.YCoord - 1])
            {
                neighBoursToProcess.Add(grid.Nodes[currentNode.XCoord - 1, currentNode.YCoord - 1]);
            }
            if (neighbourRigth && neighbourDown && grid.Cells[currentNode.XCoord + 1, currentNode.YCoord - 1])
            {
                neighBoursToProcess.Add(grid.Nodes[currentNode.XCoord + 1, currentNode.YCoord - 1]);
            }

            ProcessNeightBours(outputDirection, openList, closedList, currentNode, neighBoursToProcess, true);
            neighBoursToProcess.Clear();
        }

        return outputDirection;
    }

    private static void ProcessNeightBours(FlowDirection[,] outputDirection, List<FlowFieldNode> openList, List<FlowFieldNode> closedList, FlowFieldNode currentNode, List<FlowFieldNode> neighBoursToProcess, bool diagonalNeightbours)
    {
        foreach (FlowFieldNode neighbour in neighBoursToProcess)
        {
            if (closedList.Contains(neighbour))
            {
                continue;
            }


            int newCostSoFar;
            if (diagonalNeightbours)
            {
                newCostSoFar = currentNode.costSoFar + 14;
            }
            else
            {
                newCostSoFar = currentNode.costSoFar + 10;
            }


            if (openList.Contains(neighbour))
            {

                if (neighbour.costSoFar <= newCostSoFar)
                {
                    continue;
                }
            }

            neighbour.costSoFar = newCostSoFar;

            int xDif = currentNode.XCoord - neighbour.XCoord;
            int yDif = currentNode.YCoord - neighbour.YCoord;
            FlowDirection directionFromNeighbourToCurrent;
            if (xDif == 1 && yDif == 0)
            {
                directionFromNeighbourToCurrent = FlowDirection.RIGHT;
            }
            else if (xDif == 1 && yDif == 1)
            {
                directionFromNeighbourToCurrent = FlowDirection.RIGHT_TOP;
            }
            else if (xDif == 0 && yDif == 1)
            {
                directionFromNeighbourToCurrent = FlowDirection.TOP;
            }
            else if (xDif == -1 && yDif == 1)
            {
                directionFromNeighbourToCurrent = FlowDirection.LEFT_TOP;
            }
            else if (xDif == -1 && yDif == 0)
            {
                directionFromNeighbourToCurrent = FlowDirection.LEFT;
            }
            else if (xDif == -1 && yDif == -1)
            {
                directionFromNeighbourToCurrent = FlowDirection.LEFT_DOWN;
            }
            else if (xDif == 0 && yDif == -1)
            {
                directionFromNeighbourToCurrent = FlowDirection.DOWN;
            }
            else if (xDif == 1 && yDif == -1)
            {
                directionFromNeighbourToCurrent = FlowDirection.RIGTH_DOWN;
            }
            else
            {
                directionFromNeighbourToCurrent = FlowDirection.NONE;
            }
            neighbour.FromNodeDirection = directionFromNeighbourToCurrent;

            outputDirection[neighbour.XCoord, neighbour.YCoord] = neighbour.FromNodeDirection;

            openList.Add(neighbour);
        }
    }

    private static FlowFieldNode SmallestNode(List<FlowFieldNode> list)
    {
        if (list == null)
            return null;

        FlowFieldNode smallestNode = list[0];
        for (int i = 1; i < list.Count; i++)
        {
            if (list[i].costSoFar < smallestNode.costSoFar)
            {
                smallestNode = list[i];
            }
        }
        return smallestNode;
    }
}
