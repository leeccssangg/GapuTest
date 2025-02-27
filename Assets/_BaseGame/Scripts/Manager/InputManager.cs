using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using TW.Utility.DesignPattern;
using UnityEngine;

public class InputManager : Singleton<InputManager>
{
    [field: SerializeField] public bool IsActive { get; private set; } = false;
    [field: SerializeField] public bool IsCoolDown { get; private set; }
    private Plane Plane { get; set; }
    protected override void Awake()
    {
        base.Awake();
        Plane = new Plane(Vector3.up, Vector3.zero);
    }
    public void SetActive(bool value)
    {
        IsActive = value;
    }
    private void Update()
    {
        if (!IsActive) return;
        HandleInput();
    }

    private void HandleInput()
    {
        if (!IsCoolDown && Input.GetMouseButton(0))
        {
            Vector3 screenPoint = Input.mousePosition;
            Vector3 worldPoint = Camera.main.ScreenToWorldPoint(screenPoint);
            worldPoint.z = 0; // Ensure the Z position is 0 for 2D
            //// Check for overlap with any 2D colliders at the world point
            RaycastHit2D hit = Physics2D.Raycast(worldPoint, Vector2.zero);
            if (hit.collider != null) return;
            SpawnBall(worldPoint);
        }
    }
    private  void SpawnBall(Vector3 position)
    {
        int random = UnityEngine.Random.Range(0, Enum.GetValues(typeof(BallType)).Length - 1);
        BallSpawnManager.Instance.SpawnBall((BallType)random, position, true, Vector2.zero);
        StartCoolDown();

    }
    private void StartCoolDown()
    {
        IsCoolDown = true;
        CoolDownAsync().Forget();
    }
    private async UniTaskVoid CoolDownAsync()
    {
        await UniTask.Delay(TimeSpan.FromSeconds(2));
        IsCoolDown = false;
    }
}
