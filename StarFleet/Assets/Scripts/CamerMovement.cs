using UnityEngine;

public class CamerMovement : MonoBehaviour
{
    [SerializeField]private float speed = 8f;
    private Rigidbody2D rb;
    private Vector2 movedir;
    public float minSize = 1f;
    public float maxSize = 10f;
    public float scrollspeed;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void FixedUpdate()
    {
        float Scroll = Input.GetAxis("Mouse ScrollWheel");
        movedir = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        rb.linearVelocity = movedir * speed;
        if (Scroll != 0f)
        {
            if (Camera.main.orthographic)
            {
                Camera.main.orthographicSize -= Scroll * scrollspeed;
                Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize, minSize, maxSize);
            }
        }
    }
}
