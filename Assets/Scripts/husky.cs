using UnityEngine;
using UnityEngine.UI;

public class Husky : MonoBehaviour
{
    private Rigidbody2D rigidBody;
    public int automaticRotationSpeed;
    public int manualRotationSpeed;
    public Text Text;
    private bool rotating = false;
    private bool onGround = false;

    void Start()
    {
        rigidBody = gameObject.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        AutomaticGroundAligment();

        RotateControl();
    }

    private void RotateControl()
    {

#if UNITY_EDITOR
        //Rotate with mouse
        if (Input.GetMouseButton(0))
        {
            Rotate();
        }
        else
        {
            rotating = false;
        }
#else
        //Rotate with touch
        if (Input.touchCount > 0)
        {
            Rotate();
        }
        else
        {
            rotating = false;
        }
#endif

    }

    private void AutomaticGroundAligment()
    {
        var hit = Physics2D.Raycast(transform.position, -transform.up, 20, LayerMask.GetMask("Ground"));

        if (!rotating && hit && hit.distance > 1.0f)
        {
            if (hit.distance > 1.5f)
                rigidBody.freezeRotation = true;
            else
                rigidBody.freezeRotation = false;

            Debug.DrawLine(transform.position, hit.point, Color.green);
            var rot = Quaternion.FromToRotation(transform.up, hit.normal) * transform.rotation;
            transform.rotation = Quaternion.Lerp(transform.rotation, rot, Time.deltaTime * automaticRotationSpeed);
        }
        else
        {
            rigidBody.freezeRotation = false;
        }
    }

    private void Rotate()
    {
        if (!onGround)
        {
            rotating = true;
            transform.Rotate(new Vector3(0, 0, manualRotationSpeed));
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
            onGround = true;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
            onGround = false;
    }
}


