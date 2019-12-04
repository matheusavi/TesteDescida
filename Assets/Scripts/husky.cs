using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class husky : MonoBehaviour
{
    Rigidbody2D rigidBody;
    void Start()
    {
        rigidBody = gameObject.GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void Update()
    {
        rigidBody.AddForce(new Vector2(3, 0));
    }

    void OnCollisionStay(Collision col)
    {
        if (col.collider.gameObject.tag == "Ground")
        {
            rigidBody.constraints = RigidbodyConstraints2D.None;

            transform.rotation = Quaternion.FromToRotation(Vector3.up, col.contacts[0].normal);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        rigidBody.constraints = RigidbodyConstraints2D.FreezeRotation;

    }
}
