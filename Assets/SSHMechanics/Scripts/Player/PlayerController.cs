using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float gravityScale;

    [SerializeField] private Color bodyColor;
    [SerializeField] private MeshRenderer playerBodyRenderer;

    [SerializeField] private float cameraYOffset;
    [SerializeField] private float mouseSens;
    [SerializeField] private float maxLookAngle;

    private CharacterController _characterController;
    private Vector2 _input;
    private Camera _camera;
    private float _rotationX;

    private void Start()
    {
        _characterController = GetComponent<CharacterController>();
        _camera = Camera.main;
        _camera.transform.position = new Vector3(transform.position.x, transform.position.y + cameraYOffset,
            transform.position.z);
        _camera.transform.parent = transform;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        _input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")).normalized;

        Vector3 movement = new Vector3(_input.x, 0, _input.y) * speed * Time.deltaTime;
        movement = transform.TransformDirection(movement);

        if (!_characterController.isGrounded)
        {
            movement.y -= gravityScale * Time.deltaTime;
        }

        _characterController.Move(movement);

        if(_camera != null)
        {
            _rotationX += -Input.GetAxis("Mouse Y") * mouseSens;
            _rotationX = Mathf.Clamp(_rotationX, -maxLookAngle, maxLookAngle);
            _camera.transform.localRotation = Quaternion.Euler(_rotationX, 0, 0);
            transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * mouseSens, 0);
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            Interact();
        }

    }

    private void Interact()
    {
        Ray ray = _camera.ScreenPointToRay(new Vector2(Screen.width / 2, Screen.height / 2));
        RaycastHit hit;
        if(Physics.Raycast(ray, out hit))
        {
            if(hit.collider.TryGetComponent(out IInteractible interactible))
            {
                interactible.Interact();
            }
        }
    }
}
