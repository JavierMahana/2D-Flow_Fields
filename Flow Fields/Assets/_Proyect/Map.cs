using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class Map : MonoBehaviour
{
    public bool gizmos;

    public int mapSize = 8;
    public GameObject mapCell;
    public GameObject mapPivot;

    public CuadGrid grid;

    FlowField flowField;
    private bool mapCreated = false;
    void Awake()
    {
        CreateMap();
    }

    [Button]
    private void CreateMap()
    {
        if (!mapCreated)
        {
            for (int y = 0; y < mapSize; y++)
            {
                for (int x = 0; x < mapSize; x++)
                {
                    Vector3 pos = new Vector3(mapPivot.transform.position.x + x * mapCell.transform.localScale.x, mapPivot.transform.position.y + y * mapCell.transform.localScale.y);

                    Instantiate(mapCell, pos, Quaternion.identity, mapPivot.transform);

                }
            }

            grid = new CuadGrid(mapSize);

            mapCreated = true;
        }
    }

    public Vector2Int WorldPositionToGridInices(Vector2 postion)
    {


        int xIdx = int.MaxValue;
        for (int x = 0; x < mapSize; x++)
        {
            float temp = mapPivot.transform.position.x + x * mapCell.transform.localScale.x;
            if (Mathf.Abs(temp - postion.x) <= mapCell.transform.localScale.x/2)
            {
                xIdx = x;
                break;
            }
        }

        int yIdx  =int.MaxValue;
        for (int y = 0; y < mapSize; y++)
        {
            float temp = mapPivot.transform.position.y + y * mapCell.transform.localScale.y;
            if (Mathf.Abs(temp - postion.y) <= mapCell.transform.localScale.y / 2)
            {
                yIdx = y;
                break;
            }
        }

        if (xIdx > mapSize - 1 || yIdx > mapSize - 1)
        {
            Debug.LogError("invalid Index");
        }

        return new Vector2Int(xIdx, yIdx);
    }
}
