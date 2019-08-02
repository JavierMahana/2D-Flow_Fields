using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


public class UnitManager : MonoBehaviour
{
    public List<Unit> AllUnits = new List<Unit>();
    public Agent SampleAgent;

    public Unit SelectedUnit;
    public Map map;

    public void SelectUnit1()
    {
        SelectedUnit.selected = false;
        SelectedUnit = AllUnits[0];
        SelectedUnit.selected = true;
    }
    public void SelectUnit2()
    {
        SelectedUnit.selected = false;
        SelectedUnit = AllUnits[1];
        SelectedUnit.selected = true;
    }
    private void Start()
    {
        Unit[] temp = FindObjectsOfType<Unit>();
        AllUnits = temp.ToList<Unit>();

        SelectedUnit = AllUnits[0];
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, 1000, 1<<8);
            if (hit.collider != null)
            {
                SpawnAgentToSelectedUnit(hit.point);
            }
        }
        if (Input.GetMouseButtonDown(1))
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, 1000, 1 << 8);
            if (hit.collider != null)
            {
                Vector2Int indices = map.WorldPositionToGridInices(hit.point);
                SelectedUnit.flowField = new FlowField(map.grid, indices.x, indices.y);
            }
        }
    }

    private void SpawnAgentToSelectedUnit(Vector2 pos)
    {
        Agent newAgent = Instantiate(SampleAgent, pos, Quaternion.identity, SelectedUnit.transform);
        SelectedUnit.Agents.Add(newAgent);
        newAgent.Daddy = SelectedUnit;
    }

}
