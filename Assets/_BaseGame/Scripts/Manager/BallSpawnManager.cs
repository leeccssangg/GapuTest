using Pextension;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using TW.Utility.DesignPattern;
using UnityEngine;

public class BallSpawnManager : Singleton<BallSpawnManager>
{
    [field: Title("Normal Ball")]
    private MiniPool<NormalBall> _poolNormalBall = new();
    [field: SerializeField] public Transform NormalBallTfParent { get; private set; }
    [field: SerializeField] public NormalBall NormalBallPrefab { get; private set; }

    [field: Title("Duplicate Ball")]
    private MiniPool<DuplicateBall> _poolDuplicateBall = new();
    [field: SerializeField] public Transform DuplicateBallTfParent { get; private set; }
    [field: SerializeField] public DuplicateBall DuplicateBallPrefab { get; private set; }

    protected override void Awake()
    {
        base.Awake();
        _poolNormalBall.OnInit(NormalBallPrefab, 10, NormalBallTfParent);
        _poolDuplicateBall.OnInit(DuplicateBallPrefab, 10, DuplicateBallTfParent);
    }
    public void DespawnBall(BallType type, BallBase ball)
    {
        switch (type)
        {
            case BallType.Normal:
                _poolNormalBall.Despawn((NormalBall)ball);
                break;
            case BallType.Duplicate:
                _poolDuplicateBall.Despawn((DuplicateBall)ball);
                break;
        }
    }
    public void SpawnBall(BallType type, Vector3 position , bool isDestroyable, Vector2 direction)
    {
        switch (type)
        {
            case BallType.Normal:
                NormalBall normalBall = _poolNormalBall.Spawn(position, Quaternion.identity);
                normalBall.SetDestroyable(isDestroyable);
                normalBall.OnInit(direction);
                break;
            case BallType.Duplicate:
                DuplicateBall duplicateBall = _poolDuplicateBall.Spawn(position, Quaternion.identity);
                duplicateBall.OnInit(direction);
                break;
        }
    }
}
