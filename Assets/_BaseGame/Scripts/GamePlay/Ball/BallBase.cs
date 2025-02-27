using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using TW.Utility.CustomComponent;
using Unity.VisualScripting;
using UnityEngine;

public enum BallType
{
    None = -1,
    Normal,
    Duplicate,
}
public class BallBase : ACachedMonoBehaviour, ICollider, IRigidBody
{
    [field: SerializeField] public BallType BallType { get; private set; }
    [field: SerializeField] public float ForceMagnitude { get; private set; }
    [field: SerializeField] public Rigidbody2D Rigidbody { get; private set; }
    [field: SerializeField] public Collider2D Collider { get; private set; }
    public virtual void OnInit(Vector2 direction)
    {
        if(direction == Vector2.zero)
        {
            Vector2 randomDirection = new Vector2(Random.Range(-0.5f, 0.5f), 1f);
            AddForce(randomDirection);
        }
        else
        {
            AddForce(direction);
        }
    }
    public virtual void OnCollisionEnter2D(Collision2D collision)
    {
        Vector2 collisionDirection = GetDirectionFromCollisionPoint(collision);
        AddForce(-collisionDirection);
    }
    public virtual void OnColliWithBarrier()
    {
        
    }
    public void AddForce(Vector2 force)
    {
        Rigidbody.velocity = Vector2.zero;
        Rigidbody.AddForce(force * ForceMagnitude, ForceMode2D.Impulse);
    }
    public Vector2 GetDirectionFromCollisionPoint(Collision2D collision)
    {
        // Calculate the direction from the collision point to this object
        ContactPoint2D contact = collision.contacts[0];
        Vector2 direction = (contact.point - (Vector2)transform.position).normalized;
        return direction;
    }
}
