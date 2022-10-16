using UnityEngine;

public class Rotate : MonoBehaviour
{
    public float speed;
    public Vector3 eulerAngles;

    private void Update()
    {
        transform.Rotate(eulerAngles * speed * Time.deltaTime);
    }
}