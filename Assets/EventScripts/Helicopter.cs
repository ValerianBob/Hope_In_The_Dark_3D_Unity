using UnityEngine;
using UnityEngine.SceneManagement;

public class Helicopter : MonoBehaviour
{
    public string Info;
    public string LootInfo;

    public Vector3 startPoint;
    public Vector3 endPoint;
    public float speed = 5f;

    private bool moving = true;

    void Start()
    {
        transform.position = startPoint;

        SoundsController.Instance.PlayHelicopter(7,true);
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

    public void EndGame()
    {
        Debug.Log("Game Over");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
