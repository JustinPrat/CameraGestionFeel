using UnityEngine;

public class PlayerController : MonoBehaviour
{
	private static readonly int IsRunning = Animator.StringToHash("isRunning");
	public float speed = 10.0f;
	public Animator m_animator;

	private bool m_isMoving;
	private Vector3 m_camDir = Vector3.zero;
	private Vector3 m_rotatedCamDir = Vector3.zero;
	
	[SerializeField] private Rigidbody _rigidbody;

	public void Awake()
	{
		Cursor.visible = false;
	}

	void Update()
    {
	    if (!m_isMoving)
	    {
		    CameraController.Instance.SetCameraDirection(false);
	    }
		m_camDir = CameraController.Instance.GetCameraDirection();
		m_camDir.y = 0;
		m_rotatedCamDir = Quaternion.Euler(0, 90, 0) * m_camDir;

		Vector3 direction = Vector3.zero;
		direction += m_rotatedCamDir * (Input.GetAxisRaw("Horizontal") * Time.deltaTime);
		direction += m_camDir * (Input.GetAxisRaw("Vertical") * Time.deltaTime);
		direction.Normalize();
		_rigidbody.velocity = direction * speed + Vector3.up * _rigidbody.velocity.y;

        m_isMoving = Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0;
		m_animator.SetBool(IsRunning, m_isMoving);
    }
}
