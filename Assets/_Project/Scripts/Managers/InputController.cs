using UnityEngine;
using UnityEngine.SceneManagement;

// Extendable wrapper for input
public class InputController : MonoBehaviour
{
    [SerializeField] private InputReader inputReader;

    public static bool IsScreenTouched => isScreenTouched;
    private static bool isScreenTouched;

    [Header("Debug")]
    [SerializeField] private bool useMouse;
    [SerializeField] private Camera mainCamera;
    [SerializeField] private Transform target;
    [SerializeField] private float horizontalMouseSensitivity = 250;
    [SerializeField] private float verticalMouseSensitivity = 250;

    private float xRotation;

    private void Start()
    {
        if(useMouse)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    private void Update()
    {
        isScreenTouched = (Google.XR.Cardboard.Api.IsTriggerPressed
            || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
            || (useMouse && Input.GetMouseButton(0)));

        if(isScreenTouched)
        {
            inputReader.OnAttack();
        }

        if(useMouse)
        {
            UpdateMouseRotation();
            if(Input.GetKeyDown(KeyCode.R))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }    
        }
    }

    private void UpdateMouseRotation()
    {
        float mouseX = Input.GetAxis("Mouse X") * horizontalMouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * verticalMouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        mainCamera.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        target.Rotate(Vector3.up * mouseX);
    }
}
