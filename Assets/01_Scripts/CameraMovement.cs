using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private float directionForceMin = 0.001f;
    [SerializeField] private float scrollSpeed;
    [SerializeField] private float minOrthographicSize;
    [SerializeField] private float maxOrthographicSize;
    [SerializeField] private Collider2D clampCollider;
    
    private bool _userMoveInput;
    private Vector3 _startPosition;
    private Vector3 _directionForce;
    
    private Camera _camera;
    private void Start()
    {
        _camera = GetComponent<Camera>();
    }
    private void Update()
    {
        ControlCameraPosition();

        ReduceDirectionForce();

        UpdateCameraPosition();

        UpdateCameraScroll();

        ClampCameraToBounds();
    }

    private void ControlCameraPosition()
    {
        var mouseWorldPosition = _camera.ScreenToWorldPoint(Input.mousePosition);
        if (Input.GetMouseButtonDown(1))
            CameraPositionMoveStart(mouseWorldPosition);
        else if (Input.GetMouseButton(1))
            CameraPositionMoveProgress(mouseWorldPosition);
        else
            CameraPositionMoveEnd();
    }
    private void CameraPositionMoveStart(Vector3 startPosition)
    {
        _userMoveInput = true;
        this._startPosition = startPosition;
        _directionForce = Vector2.zero;
    }
    private void CameraPositionMoveProgress(Vector3 targetPosition)
    {
        if (!_userMoveInput)
        {
            CameraPositionMoveStart(targetPosition);
            return;
        }

        _directionForce = _startPosition - targetPosition;
    }
    private void CameraPositionMoveEnd()
    {
        _userMoveInput = false;
    }
    private void ReduceDirectionForce()
    {
        if (_userMoveInput)
            return;

        _directionForce = Vector3.Lerp(_directionForce, Vector3.zero, Time.deltaTime * 15f);
        if (_directionForce.magnitude < directionForceMin)
            _directionForce = Vector3.zero;
    }
    private void UpdateCameraPosition()
    {
        if (_directionForce == Vector3.zero)
            return;

        var currentPosition = transform.position;
        var targetPosition = new Vector3(currentPosition.x + _directionForce.x, transform.position.y, -10);
        transform.position = Vector3.Lerp(currentPosition, targetPosition, 0.5f);
    }
    
    private void UpdateCameraScroll()
    {
        var scrollWheel = Input.GetAxis("Mouse ScrollWheel");
        _camera.orthographicSize -= scrollWheel * scrollSpeed;
        if (_camera.orthographicSize < minOrthographicSize)
            _camera.orthographicSize = minOrthographicSize;
        else if(_camera.orthographicSize > maxOrthographicSize)
            _camera.orthographicSize = maxOrthographicSize;
    }

    private void ClampCameraToBounds()
    {
        if (!clampCollider) return;

        float minX = clampCollider.bounds.min.x;
        float minY = clampCollider.bounds.min.y;
        float maxX = clampCollider.bounds.max.x;
        float maxY = clampCollider.bounds.max.y;

        float cameraHalfHeight = _camera.orthographicSize;
        float cameraHalfWidth = cameraHalfHeight * _camera.aspect;

        float clampedX = Mathf.Clamp(transform.position.x, minX + cameraHalfWidth, maxX - cameraHalfWidth);
        float clampedY = Mathf.Clamp(transform.position.y, minY + cameraHalfHeight, maxY - cameraHalfHeight);

        transform.position = new Vector3(clampedX, clampedY, transform.position.z);
    }
}