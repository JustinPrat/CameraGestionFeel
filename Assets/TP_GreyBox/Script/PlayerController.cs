using UnityEngine;

public class PlayerController : MonoBehaviour
{
	public float speed = 10.0f;

	Rigidbody _rigidbody;

	public void Awake()
	{
		_rigidbody = GetComponent<Rigidbody>();
		Cursor.visible = false;
	}

	void FixedUpdate()
    {
		Vector3 camDir = CameraController.Instance.GetCameraDirection();
		camDir.y = 0;
		Vector3 rotatedCamDir = Quaternion.Euler(0, 90, 0) * camDir;

		Vector3 direction = Vector3.zero;
		direction += Input.GetAxisRaw("Horizontal") * rotatedCamDir;
		direction += Input.GetAxisRaw("Vertical") * camDir;
		direction.Normalize();
		_rigidbody.velocity = direction * speed + Vector3.up * _rigidbody.velocity.y;
	}
}
