using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    Transform cameraPosition;

    // Start is called before the first frame update
    void Start()
    {
        cameraPosition = FindObjectOfType<CameraHolderIdentifierComponent>().transform;
    }

    void Update()
    {
        transform.parent = cameraPosition;
        transform.rotation = cameraPosition.rotation;
        transform.position = cameraPosition.position;
    }
}
