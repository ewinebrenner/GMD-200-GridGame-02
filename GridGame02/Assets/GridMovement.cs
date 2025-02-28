using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridMovement : MonoBehaviour
{
    public GridManager grid;
    public Vector2Int gridPos = Vector2Int.zero;

    void Update()
    {
        Vector3 targetPos = grid.GetTile(gridPos.x, gridPos.y).transform.position;
        transform.position = Vector3.MoveTowards(transform.position, targetPos, 5.0f * Time.deltaTime);

        if (Vector3.Distance(transform.position, targetPos) > 0.01f)
            return;

        if (Input.GetKeyDown(KeyCode.RightArrow) && gridPos.x < grid.numColumns - 1 && !grid.GetTile(gridPos.x+1,gridPos.y).solid)
        {
            gridPos.x++;
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow) && gridPos.x > 0 && !grid.GetTile(gridPos.x - 1, gridPos.y).solid)
        {
            gridPos.x--;
        }
        if (Input.GetKeyDown(KeyCode.UpArrow) && gridPos.y < grid.numRows - 1 && !grid.GetTile(gridPos.x, gridPos.y+1).solid)
        {
            gridPos.y++;
        }
        if (Input.GetKeyDown(KeyCode.DownArrow) && gridPos.y > 0 && !grid.GetTile(gridPos.x, gridPos.y - 1).solid)
        {
            gridPos.y--;
        }
    }
}
