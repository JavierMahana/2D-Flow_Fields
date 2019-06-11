using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public Map map;
    public bool gizmos;

    public List<Agent> Agents;

    public FlowField flowField;


    public bool selected;

    private void Start()
    {
        flowField = new FlowField(map.grid, 0,0);
    }


    private void OnDrawGizmos()
    {
        if (flowField != null && flowField.FlowFieldValues != null && gizmos && selected)
        {
            for (int y = 0; y < flowField.FlowFieldValues.GetLength(1); y++)
            {
                for (int x = 0; x < flowField.FlowFieldValues.GetLength(0); x++)
                {
                    Gizmos.color = Color.blue;
                    Vector3 startPos = new Vector3(map.mapPivot.transform.position.x + x * map.mapCell.transform.localScale.x, map.mapPivot.transform.position.y + y * map.mapCell.transform.localScale.y);
                    Vector3 offSet = FlowField.GetNormilizedVectorFromDirectionEnum(flowField.FlowFieldValues[x, y]);

                    if (offSet == Vector3.zero)
                    {
                        if (flowField.FlowFieldValues[x, y] == FlowDirection.INVALID)
                        {
                            Gizmos.color = Color.red;
                            Gizmos.DrawSphere(startPos, .1f);
                        }
                        else
                        {
                            Gizmos.DrawSphere(startPos, .1f);
                        }
                        continue;
                    }
                    Gizmos.color = Color.blue;
                    Gizmos.DrawLine(startPos, startPos + offSet / 2);
                }
            }
        }
    }
}
