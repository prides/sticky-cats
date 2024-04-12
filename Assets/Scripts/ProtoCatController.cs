using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ProtoCatController : MonoBehaviour
{
    private new Rigidbody2D rigidbody;
    private new Collider2D collider;

    private void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        collider = GetComponent<Collider2D>();
    }

    public void EnableCat()
    {
        collider.isTrigger = false;
        rigidbody.bodyType = RigidbodyType2D.Dynamic;
    }

    public void OnCatStick(GameObject sender)
    {
        ProtoGameManager.Instance.OnCatStick(this);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "playable_area")
        {
            ProtoGameManager.Instance.OnCatOut(this);
        }
    }
}
