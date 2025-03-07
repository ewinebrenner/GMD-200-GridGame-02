using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridMovement : MonoBehaviour
{
    public GridManager grid;
    public Vector2Int gridPos = Vector2Int.zero;
    public Ease ease = Ease.Linear;
    public float moveDuration = 0.5f;
    public AnimationCurve moveCurve;

    private Tween _moveTween;
    void MoveTo(Vector2Int pos)
    {
        //If currently tweening, return early
        if (_moveTween != null && _moveTween.IsActive())
            return;

        gridPos = pos;
        Vector3 targetPos = grid.GetTile(gridPos.x, gridPos.y).transform.position;
        _moveTween = transform.DOMove(targetPos, moveDuration).SetEase(moveCurve);
    }

    IEnumerator Co_MyAnimation()
    {
        yield return transform.DOScale(0.5f, 0.5f).WaitForCompletion();
        yield return transform.DOShakePosition(0.5f).WaitForCompletion();
    }

    void Update()
    {
        //Vector3 targetPos = grid.GetTile(gridPos.x, gridPos.y).transform.position;
        //transform.position = Vector3.MoveTowards(transform.position, targetPos, 5.0f * Time.deltaTime);

        //if (Vector3.Distance(transform.position, targetPos) > 0.01f)
           // return;

        if (Input.GetKey(KeyCode.RightArrow) && gridPos.x < grid.numColumns - 1 && !grid.GetTile(gridPos.x+1,gridPos.y).solid)
        {
            MoveTo(gridPos + new Vector2Int(1, 0));
        }
        if (Input.GetKey(KeyCode.LeftArrow) && gridPos.x > 0 && !grid.GetTile(gridPos.x - 1, gridPos.y).solid)
        {
            MoveTo(gridPos + new Vector2Int(-1, 0));

        }
        if (Input.GetKey(KeyCode.UpArrow) && gridPos.y < grid.numRows - 1 && !grid.GetTile(gridPos.x, gridPos.y+1).solid)
        {
            MoveTo(gridPos + new Vector2Int(0, 1));
        }
        if (Input.GetKey(KeyCode.DownArrow) && gridPos.y > 0 && !grid.GetTile(gridPos.x, gridPos.y - 1).solid)
        {
            MoveTo(gridPos + new Vector2Int(0, -1));
        }
    }
}
