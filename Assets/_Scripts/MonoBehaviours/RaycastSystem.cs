using UnityEngine;

public class RaycastSystem : MonoBehaviour
{
    [SerializeField] private LayerMask layerMask;
    private Camera camera;

    private void Start()
    {
        camera = Camera.main;
        InputEvents.FingerTap.AddListener(Tap);
    }

    private void Tap(Vector2 _pos)
    {
        Debug.Log("Tap(Vector2 _pos)");
        var ray = camera.ScreenPointToRay(_pos); // 

        if (Physics.Raycast(ray, out var hit, 10f, layerMask))
        {
            var _notPerfectObject = hit.collider.GetComponent<NotPerfectObject>();
            GlobalEvents.OnTapAtObject?.Invoke(_notPerfectObject);
        }
    }
}