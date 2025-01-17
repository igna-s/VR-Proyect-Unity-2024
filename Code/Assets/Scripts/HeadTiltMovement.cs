using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CapsuleCollider))]
public class HeadTiltMovement : MonoBehaviour
{
    public float speed = 3.0f;               // Velocidad de movimiento
    public float tiltThreshold = 15.0f;      // Umbral de inclinación hacia abajo
    public float rayDistance = 1.5f;         // Distancia del SphereCast para detectar el suelo
    public float extraGravity = 50.0f;       // Gravedad extra cuando no está tocando el suelo
    public LayerMask groundLayer;            // Capa que representa el suelo
    public float groundCheckRadius = 0.3f;   // Radio del SphereCast para la detección del suelo
    public float maxVerticalSpeed = 5.0f;    // Máxima velocidad vertical permitida
    public float stepOffset = 0.5f;          // Ajustar para que el personaje suba pequeños escalones
    public float forwardDrag = 1.0f;         // Drag para controlar el movimiento hacia adelante
    public float fallAcceleration = 200.0f;  // Fuerza adicional para asegurar la caída inmediata

    private Rigidbody rb;
    private bool isGrounded;

    void Start()
    {
        // Obtener el componente Rigidbody
        rb = GetComponent<Rigidbody>();

        // Evitar rotación debido a la física
        rb.freezeRotation = true;

        // Desactivar la gravedad por defecto para controlarla manualmente
        rb.useGravity = false;

        // Configurar el modo de detección de colisiones
        rb.collisionDetectionMode = CollisionDetectionMode.Continuous;

        // Asegurar que el drag sea cero
        rb.drag = 0;
        rb.angularDrag = 0;
    }

    void FixedUpdate()
    {
        // Verificar si el personaje está tocando el suelo
        CheckGroundStatus();

        // Obtener la rotación de la cámara principal
        Quaternion cameraRotation = Camera.main.transform.rotation;
        Vector3 eulerAngles = cameraRotation.eulerAngles;

        // Ajustar el ángulo X al rango -180 a 180
        eulerAngles.x = (eulerAngles.x > 180) ? eulerAngles.x - 360 : eulerAngles.x;

        // Controlar el movimiento basado en la inclinación
        if (eulerAngles.x > tiltThreshold)
        {
            // Calcular la dirección hacia adelante en el plano horizontal
            Vector3 forward = new Vector3(Camera.main.transform.forward.x, 0, Camera.main.transform.forward.z).normalized;

            // Calcular el movimiento hacia adelante
            Vector3 forwardMovement = forward * speed;

            // Establecer la nueva velocidad en X y Z (mantener componente Y)
            rb.velocity = new Vector3(forwardMovement.x, rb.velocity.y, forwardMovement.z);

            // Aplicar un pequeño arrastre en el movimiento hacia adelante para suavizarlo
            rb.velocity = Vector3.Lerp(rb.velocity, new Vector3(forwardMovement.x, rb.velocity.y, forwardMovement.z), forwardDrag * Time.deltaTime);
        }
        else
        {
            // Detener el movimiento en X y Z cuando no hay inclinación suficiente
            rb.velocity = new Vector3(0, rb.velocity.y, 0);
        }

        // Aplicar caída instantánea si no está en el suelo
        if (!isGrounded)
        {
            rb.AddForce(Vector3.down * fallAcceleration, ForceMode.Acceleration);
        }
    }

    void CheckGroundStatus()
    {
        RaycastHit hit;

        // Usar un SphereCast para detectar el suelo en lugar de rayos individuales
        if (Physics.SphereCast(transform.position, groundCheckRadius, Vector3.down, out hit, rayDistance, groundLayer))
        {
            isGrounded = true;
            rb.velocity = new Vector3(rb.velocity.x, Mathf.Clamp(rb.velocity.y, -maxVerticalSpeed, float.MaxValue), rb.velocity.z);

            // Ajustar la posición vertical ligeramente si está cerca de un escalón
            if (hit.distance < stepOffset)
            {
                Vector3 position = rb.position;
                position.y = hit.point.y + stepOffset;
                rb.position = position;
            }
        }
        else
        {
            isGrounded = false;
        }
    }
}
