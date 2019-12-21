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

        if (hit && hit.collider.CompareTag("Ground") && hit.distance > 1.5f)
        {
            Debug.Log(hit.distance);
            Debug.DrawLine(transform.position, hit.point, Color.green);
            var rotCur = Quaternion.FromToRotation(transform.up, hit.normal) * transform.rotation;
            transform.rotation = Quaternion.Lerp(transform.rotation, rotCur, Time.deltaTime * adjustRotationSpeed);
        }
    }
}


