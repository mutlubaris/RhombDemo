using UnityEngine;

public class BezierMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed = 2f;
    [SerializeField] LineRenderer lineRenderer;

    private Vector3 nextPosition;
    private int waypointIndex = 0;
    private bool canMove = false;
    private bool routeComplete = false;

    private void OnMouseDown()
    {
        canMove = true;
    }

    private void Update()
    {
        if (canMove)
            Move();
    }

    private void Move()
    {
        //Determine the next position and start moving towards it

        // nextPosition = transform.parent.GetChild(waypointIndex).transform.position;
        nextPosition = lineRenderer.GetPosition(waypointIndex);

        transform.position = Vector3.MoveTowards(transform.position, nextPosition, moveSpeed * Time.deltaTime);

        if (transform.position == nextPosition && !routeComplete)
        {
            if (waypointIndex != lineRenderer.positionCount - 1)
                waypointIndex += 1; //Keep increasing the index until we reach the target
            else
            {
                FindObjectOfType<LevelTracker>().targetsReached += 1;
                print("Targets reached: " + FindObjectOfType<LevelTracker>().targetsReached);
                routeComplete = true;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //If objects collide, tell the LevelTracker to restart the level

        if (other.tag == "Object")
        {
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            canMove = false;

            FindObjectOfType<LevelTracker>().levelFailed = true;
        }

        //Tell the obstacle to stop the object on contact

        if (other.tag == "Obstacle")
        {
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            canMove = false;
        }
    }
}
