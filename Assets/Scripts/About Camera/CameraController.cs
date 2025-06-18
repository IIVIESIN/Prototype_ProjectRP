using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraController : MonoBehaviour
{
    [Header("카메라 이동 관련")]
    [SerializeField] private bool invertDragDirection;
    [SerializeField] private float dragMoveSpeed;

    [Header("줌 설정")]
    [SerializeField] private float zoomSpeed = 0.5f;
    [SerializeField] private float zoomSmoothness = 8f; // 줌 부드러움 정도
    [SerializeField] private float minZoom = 3.0f;
    [SerializeField] private float maxZoom = 10.0f;

    private Camera _cam;
    private float _targetZoom;
    private float _defaultCameraZPos;
    private Vector3 _dragStartPos;
    private Vector3 _targetPos;
    private Vector3 _remainedDistance;

    private void Awake()
    {
        _cam = GetComponent<Camera>();
        _defaultCameraZPos = _cam.transform.position.z;
        _targetZoom = _cam.orthographicSize;
    }

    private void Update()
    {
        DragToMove();
        HandleZoom();
    }

    private void DragToMove()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _dragStartPos = _cam.ScreenToWorldPoint(Input.mousePosition);
        }

        if (Input.GetMouseButton(0))
        {
            var distance = _dragStartPos - _cam.ScreenToWorldPoint(Input.mousePosition);
            _targetPos = transform.position + distance;
            _targetPos.z = _defaultCameraZPos;
        }

        transform.position = Vector3.Lerp(transform.position, _targetPos, Time.deltaTime * dragMoveSpeed);
    }

    private void HandleZoom()
    {
        float scroll = Input.mouseScrollDelta.y;

        // 타겟 줌 값만 변경 (실제 적용은 ApplySmoothMovement에서)
        _targetZoom = Mathf.Clamp(_targetZoom - scroll * zoomSpeed, minZoom, maxZoom);

        if (Mathf.Abs(_cam.orthographicSize - _targetZoom) > 0.05f)
        {
            // 카메라 위치와 줌 부드럽게 적용
            _cam.orthographicSize = Mathf.Lerp(_cam.orthographicSize, _targetZoom, Time.deltaTime * zoomSmoothness);
        }
        else
        {
            _cam.orthographicSize = _targetZoom;
        }
    }
}