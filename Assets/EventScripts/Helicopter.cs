using UnityEngine;

public class Helicopter : MonoBehaviour
{
    public Vector3 startPoint = new Vector3(0, 0, 0);  // Set in Inspector
    public Vector3 endPoint = new Vector3(0, 10, 20);  // Set in Inspector
    public float speed = 5f;

    private bool moving = true;

    void Start()
    {
        // Place helicopter at start point
        transform.position = startPoint;
    }

    void Update()
    {
        if (moving)
        {
            transform.position = Vector3.MoveTowards(transform.position, endPoint, speed * Time.deltaTime);

            if (Vector3.Distance(transform.position, endPoint) < 0.1f)
            {
                moving = false;
            }
        }
    }
}
