using UnityEngine;

public class PlayerController : MonoBehaviour
{
	public float speed = 10.0f;
	public Animator m_animator;

	Rigidbody _rigidbody;

	public void Awake()
	{
		_rigidbody = GetComponent<Rigidbody>();
		Cursor.visible = false;
	}

	void Update()
    {
		Vector3 camDir = CameraController.Instance.GetCameraDirection();
		camDir.y = 0;
		Vector3 rotatedCamDir = Quaternion.Euler(0, 90, 0) * camDir;

		Vector3 direction = Vector3.zero;
		direction += rotatedCamDir * (Input.GetAxisRaw("Horizontal") * Time.deltaTime);
		direction += camDir * (Input.GetAxisRaw("Vertical") * Time.deltaTime);
		direction.Normalize();
		_rigidbody.velocity = direction * speed + Vector3.up * _rigidbody.velocity.y;

		if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
		{
			m_animator.SetBool("isRunning", true);
		}
		else
		{
            m_animator.SetBool("isRunning", false);
        }
    }
}
