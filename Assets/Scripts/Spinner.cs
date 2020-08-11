using UnityEngine;

public class Spinner : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 360f;
    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);
    }
}
