using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Linq;


[RequireComponent(typeof(Collider2D))]
public class StickOnCollision : MonoBehaviour
{
    [SerializeField, TagSelector]
    private string[] tags;
    [SerializeField]
    private int breakForce = 1000;

    public UnityEvent<GameObject> OnStickEvent;

    [SerializeField, ReadOnly]
    private bool isStick;

    private FixedJoint2D joint;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isStick)
        {
            return;
        }
        if (tags.Any(collision.gameObject.tag.Contains))
        {
            StickToGameObject(collision.gameObject);
        }
    }

    private void StickToGameObject(GameObject gameObject)
    {
        if (joint != null)
        {
            Debug.Log("Joint already exists");
            return;
        }

        Rigidbody2D rigidbody = gameObject?.GetComponent<Rigidbody2D>();
        if (rigidbody == null)
        {
            return;
        }

        joint = this.gameObject.AddComponent(typeof(FixedJoint2D)) as FixedJoint2D;
        joint.breakForce = breakForce;
        joint.enabled = true;
        joint.connectedBody = rigidbody;

        isStick = true;
        OnStickEvent.Invoke(this.gameObject);
    }
}