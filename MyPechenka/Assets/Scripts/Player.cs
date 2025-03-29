using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Players : MonoBehaviour
{
    public ControlType controlType;
    public Joystick joystick;
    public Joystick joystick2;
    public float moveSpeed = 5f;
    private Rigidbody2D rb;
    private Vector2 moveInput;
    private Vector2 moveVelocity;
    public int playerNumber;

    private float minMovingSpeed = 0.1f;
    private bool isRunning = false;
    private bool isControlEnabled = true; // ch
    public enum ControlType { PC, Android };

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (!isControlEnabled) return; // ch

        if (controlType == ControlType.PC)
        {
            if (playerNumber == 1)
            {
                moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            }
            if (playerNumber == 2)
            {
                moveInput = new Vector2(Input.GetAxisRaw("Horizontal2"), Input.GetAxisRaw("Vertical2"));
            }
        }
        else if (controlType == ControlType.Android)
        {
            if (playerNumber == 1)
            {
                moveInput = new Vector2(joystick.Horizontal, joystick.Vertical);
            }
            if (playerNumber == 2)
            {
                moveInput = new Vector2(joystick2.Horizontal, joystick2.Vertical);
            }
        }
        moveVelocity = moveInput.normalized * moveSpeed;
    }

    private void FixedUpdate()
    {
        if (!isControlEnabled) return; // ch

        rb.MovePosition(rb.position + moveVelocity * Time.fixedDeltaTime);

        if (Mathf.Abs(moveInput.x) > minMovingSpeed || Mathf.Abs(moveInput.y) > minMovingSpeed)
        {
            isRunning = true;
        }
        else
        {
            isRunning = false;
        }
    }
    public void DisableControl() // ch
    {
        isControlEnabled = false;
        moveInput = Vector2.zero;
        moveVelocity = Vector2.zero;
        Invoke("EnableControl", 5);   //АХТУНГ!!!!!1111!! MAGIC NUMBER
    }

    public void EnableControl()
    {
        isControlEnabled = true;
    }
    public bool IsRunning()
    {
        return isRunning;
    }

    public Vector2 GetMoveInput()
    {
        return moveInput;
    }
}
//public class Players : MonoBehaviour
//{
//    public static Players Instance { get; private set; }
//    public ControlType controlType;
//    public Joystick joystick;
//    public Joystick joystick2;
//    public float moveSpeed = 5f; // Скорость движения игрока
//    private Rigidbody2D rb;
//    private Vector2 moveInput;
//    private Vector2 moveVelocity;
//    public int playerNumber;


//    private float minMovingSpeed = 0.1f;
//    private bool isRunning = false;

//    public enum ControlType { PC, Android };

//    private void Start()
//    {
//        Instance = this;
//        rb = GetComponent<Rigidbody2D>();
//    }
//    private void Update()
//    {
//        if (controlType == ControlType.PC)
//        {
//            if (playerNumber == 1)
//            {
//                moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
//            }
//            if (playerNumber == 2)
//            {
//                moveInput = new Vector2(Input.GetAxisRaw("Horizontal2"), Input.GetAxisRaw("Vertical2"));
//            }
//        }
//        else if (controlType == ControlType.Android)
//        {
//            if (playerNumber == 1)
//            {
//                moveInput = new Vector2(joystick.Horizontal, joystick.Vertical);
//            }
//            if (playerNumber == 2)
//            {
//                moveInput = new Vector2(joystick2.Horizontal, joystick2.Vertical);
//            }
//        }
//        moveVelocity = moveInput.normalized * moveSpeed;
//    }

//    private void FixedUpdate()
//    {

//        rb.MovePosition(rb.position + moveVelocity * Time.fixedDeltaTime);

//        if (Mathf.Abs(moveInput.x) > minMovingSpeed || Mathf.Abs(moveInput.y) > minMovingSpeed)
//        {
//            isRunning = true;
//        }
//        else
//        {
//            isRunning = false;
//        }
//    }

//    public bool IsRunning()
//    {
//        return isRunning;
//    }
//    public Vector2 GetMoveInput()
//    {
//        return moveInput;
//    }
//}
