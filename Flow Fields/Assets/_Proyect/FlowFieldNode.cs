public class FlowFieldNode 
{
    public FlowFieldNode(int x, int y)
    {
        XCoord = x;
        YCoord = y;
        Reset();
    }
    public void Reset()
    {
        costSoFar = int.MaxValue;
        FromNodeDirection = FlowDirection.INVALID;
    }

    public int costSoFar;
    public FlowDirection FromNodeDirection;
    public int XCoord { private set; get; } 
    public int YCoord { private set; get; }
}
