using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movment : MonoBehaviour
{
    MovmentData movmentData;
    WhisCmd wCmd;


    private void Start()
    {
        movmentData = GetComponent<MovmentData>();

        // Hide the cursor
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        // Put the camera inside the capsule collider
        movmentData.playerView.position = transform.position;
        movmentData.playerView.position = new Vector3(transform.position.x, transform.position.y + movmentData.playerViewYOffset, transform.position.z);
    }

    private void Update()
    {
        if (Cursor.lockState == CursorLockMode.Locked)
        {
            movmentData.rotX += Input.GetAxis("Mouse Y") * movmentData.xMouseSensitivity * 0.02f;
            movmentData.rotY += Input.GetAxis("Mouse X") * movmentData.yMouseSensitivity * 0.02f;
        }
        

        // Clamp the X rotation
        if (movmentData.rotX < -90) movmentData.rotX = -90f;
        else if (movmentData.rotX > 90) movmentData.rotX = 90f;

        // Rotates the collider
        transform.rotation = Quaternion.Euler(0, movmentData.rotY, 0);
        movmentData.playerView.rotation = Quaternion.Euler(-movmentData.rotX, movmentData.rotY, 0);

        QueueJump();
        GroundMove();

        // Move the controller
        movmentData.characterController.Move(movmentData.playerVelocity * Time.deltaTime);

        // Calculate top velocity 
        Vector3 m_velocity = movmentData.playerVelocity;
        m_velocity.y = 0.0f;

        if (m_velocity.magnitude > movmentData.playerTopVelocity) movmentData.playerTopVelocity = m_velocity.magnitude;

        //Need to move the camera after the player has been moved because otherwise the camera will clip the player if going fast enough and will always be 1 frame behind.
        //Set the camera's position to the transform
        movmentData.playerView.position = new Vector3
            (
            transform.position.x,
            transform.position.y + movmentData.playerViewYOffset,
            transform.position.z
            );

    }

    void GroundMove()
    {
        if (movmentData.characterController.isGrounded)
        {
            Vector3 m_wishDir;

            // Do not apply friction if the player is queueing up the next jump
            if (!movmentData.wishJump) ApplyFriction(1.0f);
            else ApplyFriction(0);

            SetMovementDir();

            m_wishDir = new Vector3(movmentData.whisCmd.rightMove, 0, movmentData.whisCmd.forwardMove);
            m_wishDir = transform.TransformDirection(m_wishDir);
            m_wishDir.Normalize();

            movmentData.moveDirectionNorm = m_wishDir;

            float m_wishSpeed = m_wishDir.magnitude;
            m_wishSpeed *= movmentData.moveSpeed;

            Accelerate(m_wishDir, m_wishSpeed, movmentData.runAcceleration);

            //Reset the gravity velocity.
            movmentData.playerVelocity.y = -movmentData.gravity * Time.deltaTime;

            if (movmentData.wishJump)
            {
                movmentData.playerVelocity.y = movmentData.jumpSpeed;
                movmentData.wishJump = false;
            }
        } 
        else if (!movmentData.characterController.isGrounded)
        {
            AirMove();
        }
    }

    void QueueJump()
    {
        if (Input.GetButtonDown("Jump") && !movmentData.wishJump) movmentData.wishJump = true;
        if (Input.GetButtonUp("Jump")) movmentData.wishJump = false;
    }

    private void SetMovementDir()
    {
        movmentData.whisCmd.forwardMove = Input.GetAxisRaw("Vertical");
        movmentData.whisCmd.rightMove = Input.GetAxisRaw("Horizontal");
    }

    void AirMove()
    {
        Vector3 m_wishDir;
        float m_wishVelocity = movmentData.airAcceleration;
        float acceliration;

        SetMovementDir();

        m_wishDir = new Vector3(movmentData.whisCmd.rightMove, 0, movmentData.whisCmd.forwardMove);
        m_wishDir = transform.TransformDirection(m_wishDir);

        float wishspeed = m_wishDir.magnitude;
        wishspeed *= movmentData.moveSpeed;

        m_wishDir.Normalize();
        movmentData.moveDirectionNorm = m_wishDir;

        // CPM: Aircontrol
        float wishspeed2 = wishspeed;
        if (Vector3.Dot(movmentData.playerVelocity, m_wishDir) < 0) acceliration = movmentData.airDeacceleration;
        else acceliration = movmentData.airAcceleration;

        // If the player is ONLY strafing left or right
        if (movmentData.whisCmd.forwardMove == 0 && movmentData.whisCmd.rightMove != 0)
        {
            if (wishspeed > movmentData.sideStrafeSpeed)
                wishspeed = movmentData.sideStrafeSpeed;
            acceliration = movmentData.sideStrafeAcceleration;
        }

        Accelerate(m_wishDir, wishspeed, acceliration);
        if (movmentData.airControl > 0) AirControl(m_wishDir, wishspeed2);

        // Apply gravity
        movmentData.playerVelocity.y -= movmentData.gravity * Time.deltaTime;
    }

    private void AirControl(Vector3 m_wishDir, float m_wishSpeed)
    {
        float m_zSpeed;
        float m_speed;
        float m_dot;
        float k;

        // Can't control movement if not moving forward or backward
        if (Mathf.Abs(movmentData.whisCmd.forwardMove) < 0.001 || Mathf.Abs(m_wishSpeed) < 0.001)
            return;
        m_zSpeed = movmentData.playerVelocity.y;
        movmentData.playerVelocity.y = 0;

        // Next two lines are equivalent to idTech's VectorNormalize() //
        m_speed = movmentData.playerVelocity.magnitude;
        movmentData.playerVelocity.Normalize();

        m_dot = Vector3.Dot(movmentData.playerVelocity, m_wishDir);
        k = 32;
        k *= movmentData.airControl * m_dot * m_dot * Time.deltaTime;

        // Change direction while slowing down
        if (m_dot > 0)
        {
            movmentData.playerVelocity.x = movmentData.playerVelocity.x * m_speed + m_wishDir.x * k;
            movmentData.playerVelocity.y = movmentData.playerVelocity.y * m_speed + m_wishDir.y * k;
            movmentData.playerVelocity.z = movmentData.playerVelocity.z * m_speed + m_wishDir.z * k;

            movmentData.playerVelocity.Normalize();
            movmentData.moveDirectionNorm = movmentData.playerVelocity;
        }

        movmentData.playerVelocity.x *= m_speed;
        movmentData.playerVelocity.y = m_zSpeed; // Note this line
        movmentData.playerVelocity.z *= m_speed;
    }

    void Accelerate(Vector3 m_wishDir, float m_wishSpeed, float m_acceleration)
    {
        float m_addSpeed;
        float m_accelSpeed;
        float m_currentSpeed;

        m_currentSpeed = Vector3.Dot(movmentData.playerVelocity, m_wishDir);
        m_addSpeed = m_wishSpeed - m_currentSpeed;

        if (m_addSpeed <= 0) return;

        m_accelSpeed = m_acceleration * Time.deltaTime * m_wishSpeed;

        if (m_accelSpeed > m_addSpeed) m_accelSpeed = m_addSpeed;

        movmentData.playerVelocity.x += m_accelSpeed * m_wishDir.x;
        movmentData.playerVelocity.z += m_accelSpeed * m_wishDir.z;
    }

    void ApplyFriction(float m_friction)
    {
        Vector3 m_vectorVec = movmentData.playerVelocity;

        float m_speed;
        float m_newSpeed;
        float m_control;
        float m_drop;

        m_vectorVec.y = 0.0f;
        m_speed = m_vectorVec.magnitude;
        m_drop = 0.0f;

        // Only if the player is on the ground then apply friction 
        if (movmentData.characterController.isGrounded)
        {
            m_control = m_speed < movmentData.runDeacceleration ? movmentData.runDeacceleration : m_speed;
            m_drop = m_control * movmentData.friction * Time.deltaTime * m_friction;
        }

        m_newSpeed = m_speed - m_drop;
        movmentData.playerFriction = m_newSpeed;

        if (m_newSpeed < 0) m_newSpeed = 0;
        if (m_speed > 0) m_newSpeed /= m_speed;

        movmentData.playerVelocity.x *= m_newSpeed;
        movmentData.playerVelocity.z *= m_newSpeed;
    }

}
