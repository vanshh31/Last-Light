
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class EnergyPickup : MonoBehaviour
{
    [Header("Energy Settings")]
    [SerializeField] private int energyValue = 1;

    [Header("Visual Settings")]
    [SerializeField] private float rotationSpeed = 90f;
    [SerializeField] private float floatAmplitude = 0.08f;
    [SerializeField] private float floatSpeed = 3f;

    private Vector3 startPosition;

    private void Start()
    {
        startPosition = transform.position;
    }

    private void Update()
    {
        transform.Rotate(
            0f,
            0f,
            rotationSpeed * Time.deltaTime
        );

        float offset = Mathf.Sin(Time.time * floatSpeed)
                       * floatAmplitude;

        transform.position = startPosition
                           + Vector3.up * offset;
    }

    public void SetEnergyValue(int value)
    {
        energyValue = Mathf.Max(1, value);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player"))
            return;

        PlayerEnergy playerEnergy =
            other.GetComponent<PlayerEnergy>();

        if (playerEnergy != null)
        {
            playerEnergy.AddEnergy(energyValue);
            Destroy(gameObject);
        }
    }
}
