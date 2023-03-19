using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move2D : MonoBehaviour
{

    public float speed = 5.0f;

    private Vector2 targetPosition;
    private bool isMoving = false;

    void FixedUpdate()
    {
        if (isMoving)
        {
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

            if (transform.position == (Vector3)targetPosition)
            {
                isMoving = false;
            }
        }
    }

    public void MoveTo(Vector2 position)
    {
        RaycastHit2D hit = Physics2D.Linecast(transform.position, position);

        if (hit.collider == null)
        {
            targetPosition = position;
            isMoving = true;
        }
    }
    
}
