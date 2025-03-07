using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridMovement : MonoBehaviour
{
    public GridManager grid;
    public Vector2Int gridPos = Vector2Int.zero;
  //  public Ease ease = Ease.Linear;
    public float moveDuration = 0.5f;
    public AnimationCurve moveCurve;

    private Tween _moveTween;
    void MoveTo(Vector2Int pos)
    {
        //If currently tweening, return early
        if (_moveTween != null && _moveTween.IsActive())
            return;

        //If moving into a wall, shake
        if (WallCheck(pos))
        {
            _moveTween = transform.DOShakePosition(0.3f, 0.5f, 20, 0);
            return;
        }

        gridPos = pos;
        Vector3 targetPos = grid.GetTile(gridPos.x, gridPos.y).transform.position;
        _moveTween = transform.DOMove(targetPos, moveDuration).SetEase(moveCurve);
    }

    bool WallCheck(Vector2Int pos)
    {
        //Check bounds of board
        if (pos.x < 0 || pos.y < 0 || pos.x >= grid.numColumns || pos.y >= grid.numRows)
            return true;
        return grid.GetTile(pos.x, pos.y).solid;
    }
    void Update()
    {
        //Old code using MoveTowards instead of DoTween
        //Vector3 targetPos = grid.GetTile(gridPos.x, gridPos.y).transform.position;
        //transform.position = Vector3.MoveTowards(transform.position, targetPos, 5.0f * Time.deltaTime);

        //if (Vector3.Distance(transform.position, targetPos) > 0.01f)
           // return;

        if (Input.GetKey(KeyCode.RightArrow))
        {
            MoveTo(gridPos + new Vector2Int(1, 0));
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            MoveTo(gridPos + new Vector2Int(-1, 0));
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            MoveTo(gridPos + new Vector2Int(0, 1));
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            MoveTo(gridPos + new Vector2Int(0, -1));
        }
    }

    //Example of a coroutine using tweens
    IEnumerator Co_MyAnimation()
    {
        yield return transform.DOScale(0.5f, 0.5f).WaitForCompletion();
        yield return transform.DOShakePosition(0.5f).WaitForCompletion();
    }

}
