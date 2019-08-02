using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowField 
{
    private static readonly Vector2 N_RIGHT = Vector2.right;
    private static readonly Vector2 N_RIGHT_UP = new Vector2(0.7071f, 0.7071f);
    private static readonly Vector2 N_UP = Vector2.up;
    private static readonly Vector2 N_LEFT_UP = new Vector2(-0.7071f, 0.7071f);
    private static readonly Vector2 N_LEFT = Vector2.left;
    private static readonly Vector2 N_LEFT_DOWN = new Vector2(-0.7071f, -0.7071f);
    private static readonly Vector2 N_DOWN = Vector2.down;
    private static readonly Vector2 N_RIGHT_DOWN = new Vector2(0.7071f, -0.7071f);

    public static Vector2 GetNormilizedVectorFromDirectionEnum(FlowDirection direction)
    {
        switch (direction)
        {
            case FlowDirection.RIGHT:
                return FlowField.N_RIGHT;

            case FlowDirection.RIGHT_TOP:
                return FlowField.N_RIGHT_UP;

            case FlowDirection.TOP:
                return FlowField.N_UP;
                
            case FlowDirection.LEFT_TOP:
                return FlowField.N_LEFT_UP;
                
            case FlowDirection.LEFT:
                return FlowField.N_LEFT;
                
            case FlowDirection.LEFT_DOWN:
                return FlowField.N_LEFT_DOWN;
                
            case FlowDirection.DOWN:
                return FlowField.N_DOWN;
                
            case FlowDirection.RIGTH_DOWN:
                return FlowField.N_RIGHT_DOWN;

            default:
                return Vector2.zero;
                
        }
    }


    public FlowDirection[,] FlowFieldValues;

    public FlowField(CuadGrid grid, int xDestination, int yDestination)
    {
        int size = grid.Size;
        if (size <= xDestination || size <= yDestination)
        {
            FlowFieldValues = null;
            return;
        }
        FlowFieldValues = FlowFieldProcessor.CalculateFlowFieldValues(grid, xDestination, yDestination);
    }


}
public enum FlowDirection
{
    INVALID = 0,
    RIGHT = 1,
    RIGHT_TOP = 2,
    TOP = 3,
    LEFT_TOP = 4,
    LEFT = 5,
    LEFT_DOWN = 6, 
    DOWN = 7,
    RIGTH_DOWN = 8,
    NONE = 9
}
