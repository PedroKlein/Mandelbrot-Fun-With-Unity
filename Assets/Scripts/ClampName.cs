using UnityEngine;
using UnityEngine.UI;

public class ClampName : MonoBehaviour
{
    [SerializeField] Text text;

    void Update()
    {
        Vector3 namePos = Camera.main.WorldToScreenPoint(transform.position);
        text.transform.position = namePos;
    }
}
