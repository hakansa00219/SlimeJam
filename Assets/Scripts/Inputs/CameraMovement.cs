using UnityEngine;
using UnityEngine.InputSystem;

namespace Input
{
    [RequireComponent(typeof(Camera))]
    public class CameraMovement : MonoBehaviour
    {
        private Camera _camera;
        public float dragSpeed = 1f;
        
        [SerializeField] private InputActionReference mouseDelta;
        [SerializeField] private InputActionReference rightClickHoldInput;
        
        private void Awake() 
        {
            _camera = GetComponent<Camera>();
        }

        private void OnEnable()
        {
            mouseDelta.action.Enable();
            rightClickHoldInput.action.Enable();
        }
        private void OnDisable()
        {
            mouseDelta.action.Disable();
            rightClickHoldInput.action.Disable();
        }

        private void Update()
        {
            if (!(rightClickHoldInput.action.ReadValue<float>() > 0f)) return;
            
            Vector2 delta = mouseDelta.action.ReadValue<Vector2>();
            _camera.transform.position += new Vector3(-delta.x, -delta.y, 0f) * (dragSpeed * Time.deltaTime);
        }
    }
}