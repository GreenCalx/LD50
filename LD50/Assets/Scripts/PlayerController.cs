using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public bool dbg;
    [Header("tweaks")]
    public float movespeed = 5.0f;
    public float rotspeed = 10.0f;
    public float height = 0.5f;
    public float heightPadding = 0.05f;
    public float maxGroundAngle = 120f;
    [Header("mand")]
    public LayerMask ground;
    public Camera cam;

    ///// inners
    private Vector2 input;
    private float angle;
    private float groundAngle;
    private Vector3 forward;
    private RaycastHit hitInfo;
    private bool grounded;

    private Transform cam_transform;
    private Quaternion targetRotation;


    // Start is called before the first frame update
    void Start()
    {
        cam_transform = cam.transform;
    }

    // Update is called once per frame
    void Update()
    {
        listenInputs();
        CalculateDirection();
        CalculateForward();
        CalculateGroundAngle();
        CheckGround();

        ApplyGravity();
        DrawDebug();

        if ((Mathf.Abs(input.x) < 1) && ( Mathf.Abs(input.y) < 1) )
            return; // no movement
        
        Rotate();
        Move();
    }

    public void listenInputs()
    {
        input.x = Input.GetAxisRaw("Horizontal") ;
        input.y = Input.GetAxisRaw("Vertical") ;

    }

    public void CalculateDirection(){
        angle = Mathf.Atan2( input.x, input.y);
        angle = Mathf.Rad2Deg * angle;
        angle += cam_transform.eulerAngles.y;
    }

    public void Rotate(){
        targetRotation = Quaternion.Euler( 0, angle, 0);
        transform.rotation = Quaternion.Slerp( transform.rotation, targetRotation, rotspeed * Time.unscaledDeltaTime);
    }

    public void Move(){
        if (groundAngle >= maxGroundAngle)
            return;
        transform.position += forward * movespeed * Time.unscaledDeltaTime;
    }

    public void CalculateForward()
    {
        if (!grounded)
        {
            forward = transform.forward;
            return;
        }

        forward = Vector3.Cross( hitInfo.normal, -transform.right);
    }

    public void CalculateGroundAngle()
    {
        if (!grounded)
        {
            groundAngle = 90;
            return;
        }
        groundAngle = Vector3.Angle(hitInfo.normal, transform.forward);
    }

    public void CheckGround()
    {
        if ( Physics.Raycast(transform.position, -Vector3.up, out hitInfo, height + heightPadding, ground) )
        {
            grounded = true;
        } else {
            grounded = false;
        }
    }

    public void ApplyGravity()
    {
        if (!grounded)
        {
            transform.position += Physics.gravity * Time.unscaledDeltaTime;
        }
    }

    public void DrawDebug()
    {
        if(!dbg) return;

        Debug.DrawLine(transform.position, transform.position + forward * height * 2, Color.blue);
        Debug.DrawLine(transform.position, transform.position - Vector3.up * height, Color.green);
        
    }
}
