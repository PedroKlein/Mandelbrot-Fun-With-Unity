using UnityEngine;

public class GenerateBackground : MonoBehaviour
{
    [SerializeField] LineRenderer lineRenderer;

    void Start()
    {
        LineRenderer x = Instantiate(lineRenderer, new Vector3(-8,0,0), transform.rotation);
        LineRenderer y = Instantiate(lineRenderer, new Vector3(0, -5, 0), transform.rotation);

        y.SetPosition(0, new Vector3(0, 10, 0));
        x.SetPosition(0, new Vector3(16, 0, 0));
    }
}
