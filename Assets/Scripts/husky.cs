using UnityEngine;

public class Husky : MonoBehaviour
{
    private Rigidbody2D rigidBody;
    public int adjustRotationSpeed;
    void Start()
    {
        rigidBody = gameObject.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        rigidBody.AddForce(new Vector2(3, 0));

        var hit = Physics2D.Raycast(transform.position, -transform.up, 20, LayerMask.GetMask("Ground"));

        if (hit && hit.distance > 1.0f)
        {
            Debug.Log(hit.distance);
            if (hit.distance > 1.5f)
                rigidBody.freezeRotation = true;
            else
                rigidBody.freezeRotation = false;

            Debug.DrawLine(transform.position, hit.point, Color.green);
            var rot = Quaternion.FromToRotation(transform.up, hit.normal) * transform.rotation;
            transform.rotation = Quaternion.Lerp(transform.rotation, rot, Time.deltaTime * adjustRotationSpeed);
        }
        else
        {
            rigidBody.freezeRotation = false;
        }
    }
}


