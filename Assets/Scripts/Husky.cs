using System;
using UnityEngine;
using UnityEngine.UI;

public class Husky : MonoBehaviour
{
    public int automaticRotationSpeed;
    public int manualRotationSpeed;
    public Text TextTouch;
    public Text TextRotating;
    public Text TextOnGround;
    public Vector2 jumpHeight;
    /// <summary>
    /// Delay for rotating after jumping
    /// </summary>
    public float jumpDelay = .5f;

    private float timeJumped;
    private Rigidbody2D rigidBody;
    private Vector2 initialPosition;
    private bool rotating = false;
    private bool onGround = false;

    void Start()
    {
        rigidBody = gameObject.GetComponent<Rigidbody2D>();
        initialPosition = rigidBody.position;
    }

    void Update()
    {
        AutomaticGroundAligment();

        RotatingControl();

        //Reset to it's initial position
        if (rigidBody.position.y < -100)
        {
            Reset();
        }

#if UNITY_EDITOR
        TextRotating.text = rotating.ToString();
        TextOnGround.text = onGround.ToString();
#endif
    }

    public void Reset()
    {
        rigidBody.position = initialPosition;
        rigidBody.velocity = new Vector2(0, 0);
    }

    private void RotatingControl()
    {


        //#if UNITY_EDITOR 
        //        //Control with mouse on editor
        //        if (Input.GetMouseButton(0))
        //        {
        //            HandleRotation();
        //            HandleJump();
        //        }
        //        else
        //        {
        //            rotating = false;
        //        }
        //#endif
        //Uncomment the code above and comment the section below for testing without a phone

        if (Input.touchCount > 0 && CanAct())
        {
            var theTouch = Input.GetTouch(0);

#if UNITY_EDITOR
            TextTouch.text = theTouch.phase.ToString();
#endif

            if (theTouch.phase == TouchPhase.Stationary || theTouch.phase == TouchPhase.Moved)
            {
                HandleRotation();
            }
            else if (theTouch.phase == TouchPhase.Began)
            {
                HandleJump();
            }
            else
            {
                rotating = false;
            }
        }
        else
        {
            rotating = false;
        }
    }

    private bool CanAct()
    {
        return Time.time - timeJumped > jumpDelay;
    }

    private void AutomaticGroundAligment()
    {
        var hit = Physics2D.Raycast(transform.position, -transform.up, 20, LayerMask.GetMask("Ground"));

        if (!rotating)
        {
            if (hit)
            {
                if (hit.distance > 1.0f)
                {
                    if (hit.distance > 1.5f)
                        rigidBody.freezeRotation = true;
                    else
                        rigidBody.freezeRotation = false;

                    Debug.DrawLine(transform.position, hit.point, Color.green);
                    var rot = Quaternion.FromToRotation(transform.up, hit.normal) * transform.rotation;
                    transform.rotation = Quaternion.Lerp(transform.rotation, rot, Time.deltaTime * automaticRotationSpeed);
                }
            }
            else
            {
                if(transform.rotation.eulerAngles.z > 45 && transform.rotation.eulerAngles.z < 315)
                {
                    if (transform.rotation.z < 175 && transform.rotation.z > 0)
                    {
                        transform.Rotate(new Vector3(0, 0, automaticRotationSpeed * -1));
                    }
                    else
                    {
                        transform.Rotate(new Vector3(0, 0, automaticRotationSpeed));
                    }
                }
            }
        }
        else
        {
            rigidBody.freezeRotation = false;
        }
    }

    private void HandleRotation()
    {

        if (!onGround)
        {
            rotating = true;
            transform.Rotate(new Vector3(0, 0, manualRotationSpeed));
        }
    }

    private void HandleJump()
    {
        if (onGround)
        {
            rigidBody.AddForce(jumpHeight, ForceMode2D.Impulse);
            timeJumped = Time.time;
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


