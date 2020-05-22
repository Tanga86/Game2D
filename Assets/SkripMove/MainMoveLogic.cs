using UnityEngine;
using UnityEngine.Events;

public class MainMoveLogic : MonoBehaviour
{
    void OnDrawGizmosSelected()
    {
        if (checkGround == null) return;
        Gizmos.DrawWireSphere(checkGround.position, checkRadius);
        if (checkGround == null) return;
        Gizmos.DrawWireSphere(checkCeiling.position, checkRadiusTwo);
    }
    [Header("State")]
    public float jumpForce = 400f;       // ���������� ����, ����������� ��� ������ ������.
    public float crouchSpeed = 0.36f;    // ���������� maxSpeed, ����������� � ����������� �������� � �������. 1 = 100%
    public float Smoothing = 0.1f;       // �������� ����� ����������
    public bool m_AirControl = false;    // ����� �� ����� ��������� �� ����� ������

    [Header("Other")]
    public LayerMask whatIsGround;       // ����, ������������, ��� �������� �� �����
    public Transform checkGround;
    public Transform checkCeiling;							
	public Collider2D crouchOffCollider; // ���������, ������� ����� �������� ��� ����������

    public float checkRadius = .2f;      // ������ ����� , ����� ����������, �� ����� ��
    public bool isGroud;                 // �� ����� �� �����
    public float checkRadiusTwo = .2f;   //  ������ ����� ����������, ����� ����������, ����� �� ����� ������
    private Rigidbody2D m_Rigidbody2D;
	private bool facingRight = true;   // ��� ����������� ����������� �������� ������
    private Vector3 m_Velocity = Vector3.zero;

	[Header("Events")]
	[Space]

	public UnityEvent OnLandEvent;

	[System.Serializable]
	public class BoolEvent : UnityEvent<bool> { }

	public BoolEvent OnCrouchEvent;
	private bool onCrouching = false;



    private void Awake()
	{
		m_Rigidbody2D = GetComponent<Rigidbody2D>();

		if (OnLandEvent == null)
			OnLandEvent = new UnityEvent();

		if (OnCrouchEvent == null)
			OnCrouchEvent = new BoolEvent();
	}

	private void Update()
	{
		bool wasGrounded = isGroud;
		isGroud = false;

        // ����� ������������, ���� �������� ������ � ������� �������� ����� �������� ���-����, ������������ ��� �����
        // ��� ����� �������, ��������� ����.    
        Collider2D[] colliders = Physics2D.OverlapCircleAll(checkGround.position, checkRadius, whatIsGround);
		for (int i = 0; i < colliders.Length; i++)
		{
			if (colliders[i].gameObject != gameObject)
			{
				isGroud = true;
				if (!wasGrounded)
					OnLandEvent.Invoke();
			}
		}
	}


	public void Move(float move, bool crouch, bool jump)
	{
        // ���� ������, ���������, ����� �� �������� ������
        if (!crouch)
		{
			// If the character has a ceiling preventing them from standing up, keep them crouching
			if (Physics2D.OverlapCircle(checkCeiling.position, checkRadiusTwo, whatIsGround))
			{
				crouch = true;
			}
		}

        // ��������� ������� ������ ���� �� ����������� ��� airControl �������
        if (isGroud || m_AirControl)
		{

			// ���� ������
			if (crouch)
			{
				if (!onCrouching)
				{
					onCrouching = true;
					OnCrouchEvent.Invoke(true);
				}
                // ����������� �������� � ������� ��������� crouchSpeed
                move *= crouchSpeed;
                // ��������� ���� �� ����������� ��� ����������
                if (crouchOffCollider != null)
                crouchOffCollider.enabled = false;
			} else
			{
				// �������� ��������,����� �� ������
				if (crouchOffCollider != null)
					crouchOffCollider.enabled = true;

				if (onCrouching)
				{
					onCrouching = false;
					OnCrouchEvent.Invoke(false);
				}
			}
            // ����������� ���������, ����� ������� ��������
            Vector3 targetVelocity = new Vector2(move * 10f, m_Rigidbody2D.velocity.y);
            // � ����� �������� ��� � ��������� � ���������
            m_Rigidbody2D.velocity = Vector3.SmoothDamp(m_Rigidbody2D.velocity, 
                targetVelocity, ref m_Velocity, Smoothing);

            // ���� input ���������� ������ ������ � ����� ��������� �����... 
            if (move > 0 && !facingRight)
			{
                //  ������ �����������.
                Swap();
			}
            // � ��������� ������, ���� input ���������� ������ �����, 
            //  � ����� ��������� ������ ...
            else if (move < 0 && facingRight)
			{
				//  ������ �����������.
				Swap();
			}

		}
        // ���� ����� ������ ��������
        if (isGroud && jump)
        {
            // ��������� ������������ ���� � ������

                isGroud = false;
                m_Rigidbody2D.AddForce(new Vector2(0f,jumpForce));
            GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);

        }
	}

    //������ �����������
	private void Swap()
	{
        // ����������� ���������� ����������� ���� ������
        facingRight = !facingRight;

        // ������������� ���
        Vector3 scale = transform.localScale;
        scale.x *= -1;
		transform.localScale = scale;
	}
}
