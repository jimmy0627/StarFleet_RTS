using UnityEngine;
using UnityEngine.InputSystem;

public class CamerMovement : MonoBehaviour
{
    [SerializeField] private float speed = 8f;
    private Rigidbody2D rb;
    private Vector2 movedir;
    public float zoomSpeed = 0.5f;
    public float minSize = 1f;
    public float maxSize = 10f;
    private Vector3 _origin;
    private Vector3 _difference;

    private Camera _mainCamera;
    private Bounds _cameraBounds;
    private Vector3 _targetPosition;
    private void Awake() => _mainCamera = Camera.main;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        var height = _mainCamera.orthographicSize;
        var width = height * _mainCamera.aspect;

        var minX = Globals.WorldBounds.min.x + width;
        var maxX = Globals.WorldBounds.extents.x - width;

        var minY = Globals.WorldBounds.min.y + height;
        var maxY = Globals.WorldBounds.extents.y - height;

        _cameraBounds = new Bounds();
        _cameraBounds.SetMinMax(
            new Vector3(minX, minY, 0.0f),
            new Vector3(maxX, maxY, 0.0f)
            );
    }
    void Update()
    {
        movedir = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        rb.linearVelocity = movedir * speed;
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll != 0)
        {
            _mainCamera.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize - scroll * zoomSpeed, minSize, maxSize);
        }
    }
    private Vector3 GetCameraBounds()
    {
        return new Vector3(
            Mathf.Clamp(_targetPosition.x, _cameraBounds.min.x, _cameraBounds.max.x),
            Mathf.Clamp(_targetPosition.y, _cameraBounds.min.y, _cameraBounds.max.y),
            transform.position.z
        );
    }
}
