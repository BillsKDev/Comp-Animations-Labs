using UnityEngine;

public class RayManager : MonoBehaviour
{
    [SerializeField] GameObject target;

    Camera cam;

    void Start()
    {
        cam = Camera.main;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                target.transform.position = hit.point;
            }
        }
    }
}