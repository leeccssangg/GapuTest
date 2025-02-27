using Cysharp.Threading.Tasks;
using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalBall : BallBase
{
    private Tween _tween;
    [field: SerializeField] public bool IsDestroyAble { get; private set; }
    public override void OnInit(Vector2 direction)
    {
        Transform.localScale = Vector3.one;
        _tween?.Kill();
        base.OnInit(direction);
        //OnDespawn();
    }
    public override void OnCollisionEnter2D(Collision2D collision)
    {
        base.OnCollisionEnter2D(collision);
        if (!IsDestroyAble) return;
        ICollider iCollider = CachedColliderBall.GetColliderUnit(collision);
        if (iCollider is Barrier) OnColliWithBarrier();
    }
    public override void OnColliWithBarrier()
    {
        DelayDespawn().Forget();
    }
    private async UniTaskVoid DelayDespawn()
    {
        await UniTask.Delay(TimeSpan.FromSeconds(2));
        _tween = this.transform.DOScale(2f, 0.1f).OnComplete(() => BallSpawnManager.Instance.DespawnBall(BallType,this));
    }
    public void SetDestroyable(bool isDestroyable)
    {
        IsDestroyAble = isDestroyable;
    }
}
