using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuplicateBall : BallBase
{
    private Vector2 _normalBallDirection;
    public override void OnInit(Vector2 direction)
    {
        _normalBallDirection = Vector2.zero;
        base.OnInit(direction);
        //OnDespawn();
    }
    public override void OnCollisionEnter2D(Collision2D collision)
    {
        ICollider iCollider = CachedColliderBall.GetColliderUnit(collision);
        if (iCollider is Barrier)
        {
            _normalBallDirection = -GetDirectionFromCollisionPoint(collision);
            OnColliWithBarrier();
            return;
        }
        base.OnCollisionEnter2D(collision);
    }
    public override void OnColliWithBarrier()
    {
        WaitSpawnNormalBall().Forget();
    }
    private async UniTaskVoid WaitSpawnNormalBall()
    {
        Despawn();
        SpawnNormalBall();
        await UniTask.Delay(150);
        SpawnNormalBall();
    }
    private void SpawnNormalBall()
    {
        BallSpawnManager.Instance.SpawnBall(BallType.Normal,transform.position,false, _normalBallDirection);
    }
    private void Despawn()
    {
        BallSpawnManager.Instance.DespawnBall(BallType,this);
    }
}
