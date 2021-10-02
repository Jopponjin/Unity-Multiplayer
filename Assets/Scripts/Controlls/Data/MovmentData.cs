using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Wishe command
public class WhisCmd
{
    public float forwardMove;
    public float rightMove;
    public float upMove;
}

public class MovmentData : MonoBehaviour
{
    public WhisCmd whisCmd;

    [HideInInspector]
    public CharacterController characterController;

    public bool canMove = true;

    // Player view stuff 
    public Camera playerView;                   // Must be a camera
    public float playerViewYOffset = 0.6f;         // The height at which the camera is bound to
    public float xMouseSensitivity = 30.0f;
    public float yMouseSensitivity = 30.0f;

    // Frame occuring factors
    public float gravity = 20.0f;
    public float friction = 6;                      // Ground friction

    // Movement stuff
    public float moveSpeed  = 7.0f;                // Ground move speed.
    public float runAcceleration = 14f;            // Ground acceleration.
    public float runDeacceleration      = 10f;     // Deacceleration that occurs when running on the ground.
    public float airAcceleration        = 2.0f;    // Air acceleration.
    public float airDeacceleration      = 2.0f;    // Deacceleration experienced when opposite strafing.
    public float airControl             = 0.3f;    // How precise air control is.
    public float sideStrafeAcceleration = 50f;     // How fast acceleration occurs to get up to sideStrafeSpeed when side strafing.
    public float sideStrafeSpeed        = 1f;      // What the max speed to generate when side strafing.
    public float jumpSpeed = 8.0f;                 // The speed at which the character's up axis gains when hitting jump.
    [Space]
    //Camera rotationals
    public float rotX = 0f;
    public float rotY = 0f;

    public Vector3 moveDirection = Vector3.zero;
    public Vector3 moveDirectionNorm = Vector3.zero;

    public Vector3 playerVelocity = Vector3.zero;
    public float playerTopVelocity = 0f;

    public float playerFriction = 0.5f;

    public bool isPlayerGrounded = false;
    public bool wishJump = false;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
        whisCmd = new WhisCmd();
    }
}
