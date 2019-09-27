using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // -- General var. --
    public AsteroidFieldData asteroidFieldData;
    public Creater creater;
    public Rigidbody2D rigidbody;
    public Animator animator;
    public BoxCollider2D boxCollider;
    public PhysiksObject physiksObject;
    public InventoryS inventory;

    public Vector2 posIJ;

    // -- Facingsystem var. --
    bool facingRight;
    public bool goingRight;

    // -- Rotation var.--
    public float rotiatonVolcity;
    public float rotationDrag;
    private float maxGravityForceDistance;

    // -- Grounded var. --
    public bool grounded;
    public bool wasgrounded;

    private void FixedUpdate()
    {
        if (grounded)
        {
            bool g = false;
            foreach (ChunkData chunkData in creater.finishedRendertChunkObjectsIJ)
            {
                if (chunkData.polygonCollider2D.IsTouching(boxCollider))
                {
                    g = true;
                }
            }
            if (!g) { grounded = false; }
        }

        // -- Update rotation --
        rotiatonVolcity *= rotationDrag;
        transform.Rotate(new Vector3(0, 0, rotiatonVolcity));

        // -- Aimator and Facingsystem --
        animator.SetFloat("Speed", rigidbody.velocity.magnitude);
        animator.SetBool("Grounded", grounded);
        if (!goingRight && !facingRight)
        {
            Vector3 theScale = animator.transform.localScale;
            theScale.x *= -1;
            animator.transform.localScale = theScale;
            facingRight = true;
        }
        else if (goingRight && facingRight)
        {
            Vector3 theScale = animator.transform.localScale;
            theScale.x *= -1;
            animator.transform.localScale = theScale;
            facingRight = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        grounded = true;
    }
}
