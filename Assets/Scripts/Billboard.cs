using UnityEngine;

public class Billboard : MonoBehaviour
{
	Camera cam;
    private void Awake()
    {
		cam = Camera.main;
    }
    void LateUpdate()
	{
		if (cam == null)
			cam = FindObjectOfType<Camera>();

		if (cam == null)
			return;

		transform.LookAt(cam.transform);
		transform.Rotate(Vector3.up * 180);
	}
}
