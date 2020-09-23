using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(PlayerInput))]
[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] int forwardSpeed = 10;
    [SerializeField] int strafeSpeed = 10;
    [SerializeField] int jumpForce = 12;
    [SerializeField] List<Transform> lanes;

    PlayerInput input;
    Rigidbody rb;

    private void Awake()
    {
        input = GetComponent<PlayerInput>();
        rb = GetComponent<Rigidbody>();
        Physics.gravity *= 2;
    }

    private void OnEnable()
    {
        input.OnDash += SideDash;
        input.OnJump += Jump;
    }

    private void OnDisable()
    {
        input.OnDash -= SideDash;
        input.OnJump -= Jump;
    }

    int laneIndex;
    private void SideDash()
    {
        int moveTo = input.HorizontalRaw > 0 ? 1 : -1;
        laneIndex += moveTo;

        if (laneIndex < 0)
        {
            laneIndex = 0;
        }
        else if (laneIndex >= lanes.Count)
        {
            laneIndex = lanes.Count - 1;
        }

        rb.DOMoveX(lanes[laneIndex].position.x, .5f);
    }

    bool canJump = true;
    private void Jump()
    {
        if (canJump)
        {
            rb.AddForce(Vector3.up * jumpForce * rb.mass, ForceMode.Impulse);
            canJump = false;
            StartCoroutine(JumpAgainRoutine());
        }
    }

    private IEnumerator JumpAgainRoutine()
    {
        while (!OnGround || rb.velocity.y > -0.1f)
        {
            yield return null;
        }

        canJump = true;
    }

    private bool OnGround
    {
        get
        {
            Collider[] colliders = Physics.OverlapBox(transform.position, Vector3.one, Quaternion.identity);
            foreach (var collider in colliders)
            {
                if (!collider.CompareTag("Player"))
                    return true;
            }

            return false;
        }
    }

    private void FixedUpdate()
    {
        rb.AddForce(Vector3.forward * forwardSpeed, ForceMode.VelocityChange);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = OnGround ? Color.green : Color.red;
        Gizmos.DrawWireSphere(transform.position, 1);
    }
}
