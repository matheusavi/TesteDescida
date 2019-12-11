using UnityEngine;

public class Husky : MonoBehaviour
{
    private Rigidbody2D rigidBody;
    private bool rotate;
    private Quaternion rotCur;
    public int adjustRotationSpeed;
    void Start()
    {
        rigidBody = gameObject.GetComponent<Rigidbody2D>();

    }

    void Update()
    {
        rigidBody.AddForce(new Vector2(3, 0));

        var hit = Physics2D.Raycast(transform.position, -transform.up, 20, LayerMask.GetMask("Ground"));

        if (hit)
        {
            Debug.DrawLine(transform.position, hit.point, Color.green);

            if (hit.collider.CompareTag("Ground"))
            {
                rotCur = Quaternion.FromToRotation(transform.up, hit.normal) * transform.rotation;
                rotate = true;
            }
        }
        else
        {
            rotate = false;
        }

        //Todo don't rotate while the gameobject is hitting the ground (just adjusts rotation while on air)
        if (rotate)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, rotCur, Time.deltaTime * adjustRotationSpeed);
        }
    }
}


