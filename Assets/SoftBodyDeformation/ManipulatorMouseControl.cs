using UnityEngine;

public class ManipulatorMouseControl : MonoBehaviour
{
    public Camera Camera;
    public float Hardness = 0.5f;
    public float Radius = 0.5f;

    Manipulator _manipulator;
    GameObject _manipulatorAnchor;
    GameObject _manipulatorHandle;

    Vector3 _prevMousePosition;

    bool _dragging;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            var ray = Camera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit, 100f))
            {
                var renderer = hit.collider.GetComponentInChildren<Renderer>();
                if (renderer != null)
                {
                    _manipulator = gameObject.AddComponent<Manipulator>();
                    _manipulatorAnchor = new GameObject("MouseAnchor");
                    _manipulatorAnchor.transform.position = hit.point;

                    _manipulatorHandle = new GameObject("MouseHandle");
                    _manipulatorHandle.transform.position = _manipulatorAnchor.transform.position;

                    _manipulator.Anchor = _manipulatorAnchor.transform;
                    _manipulator.Handle = _manipulatorHandle.transform;
                    _manipulator.Renderer = renderer;
                    _manipulator.Hardness = Hardness;
                    _manipulator.Radius = Radius;

                    _prevMousePosition = Input.mousePosition;
                    _dragging = true;
                }
            }
        }
        else if (_dragging && Input.GetMouseButton(0))
        {
            var mouseDelta = Input.mousePosition - _prevMousePosition;
            _manipulatorHandle.transform.Translate(mouseDelta * 0.01f);
            _prevMousePosition = Input.mousePosition;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            if (_dragging)
            {
                Destroy(_manipulator);
                Destroy(_manipulatorAnchor);
                Destroy(_manipulatorHandle);
            }

            _dragging = false;
        }
    }

    void Reset()
    {
        Camera = GetComponent<Camera>();
    }
}