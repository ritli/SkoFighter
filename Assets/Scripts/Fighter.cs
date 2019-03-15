using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fighter : MonoBehaviour
{
	public float speed, jumpForce;
	public float jumpTime = 1;
	public AnimationCurve jumpCurve;
	public LayerMask layerMask;

	public bool flipX;

	public float xVelocity;
	public int jump;
	float speedMult = 0;

	bool jumping;
	float jumpKey;
	float groundY;
	float feetLength;
	bool facingRight;
	bool wasHit;

	public Vector3 punchPosition;
	public Vector3 punchHitBox = Vector3.one * 0.5f;
	public Vector3 kickPosition;
	public Vector3 kickHitBox = Vector3.one * 0.75f;

	bool busy = false, onGround = false;
	Animator animator;
	private SpriteRenderer sprite;
	bool jumpAnimPlayed;

	FMODUnity.StudioEventEmitter emitter;
	[FMODUnity.EventRef]
	public string punchEvent;

	public void OnDrawGizmos()
	{
		Gizmos.color = new Color(1, 0, 0, 0.5f);
		Gizmos.DrawCube(transform.position + punchPosition, punchHitBox);

		Gizmos.color = new Color(0, 1, 0, 0.5f);
		Gizmos.DrawCube(transform.position + kickPosition, kickHitBox);
	}

	public void FixedUpdate()
	{
		onGround = OnGround;

		Move();
		Jump();
		//SimulateGravity();
	}

	private void Update()
	{
		AnimationUpdate();
	}

	public void Start()
	{
		var col = GetComponent<BoxCollider2D>();

		feetLength = col.bounds.extents.y;

		animator = GetComponentInChildren<Animator>();
		sprite = animator.GetComponent<SpriteRenderer>();
		animator.GetComponent<AnimationEventHandler>().onAnimationEventTrigger += RecieveAnimationEvent;
	}

	public void SimulateGravity()
	{
		if (!OnGround && !jumping)
		{
			transform.Translate(Vector3.up * Physics2D.gravity * Time.fixedDeltaTime);
		}
	}

	void AnimationUpdate()
	{
		sprite.flipX = flipX ? facingRight : !facingRight;

		animator.SetFloat("Speed", Mathf.Abs(xVelocity));
		animator.SetBool("OnGround", onGround);

		if (wasHit)
		{
			wasHit = false;
			animator.Play("WasHit");
		}

		if (jumping && !jumpAnimPlayed)
		{
			jumpAnimPlayed = true;
			animator.SetTrigger("Jump");
		}
		else if (!jumping)
		{
			jumpAnimPlayed = false;
		}

	}

	public void Move()
	{
		if (xVelocity > 0 && !facingRight)
		{
			facingRight = true;
			speedMult -= speedMult * 0.5f;
		}
		else if (xVelocity < 0 && facingRight)
		{
			facingRight = false;
			speedMult -= speedMult * 0.5f;
		}

		speedMult = Mathf.Clamp01(speedMult + Time.fixedDeltaTime);

		transform.Translate(new Vector3(xVelocity * Time.fixedDeltaTime * speed * speedMult, 0), Space.Self);
	}

	public void RecieveAnimationEvent(int index)
	{
		switch (index)
		{
			case -1:
				busy = false;
				animator.SetInteger("FightState", -1);
				break;
			case 0:
				DealDamageAtPoint(punchPosition, punchHitBox, 1);
				break;
			case 1:
				DealDamageAtPoint(kickPosition, kickHitBox, 3);
				break;
			default:
				break;
		}
	}

	public void StartPunch()
	{
		if (!busy)
		{
			FMODUnity.RuntimeManager.PlayOneShot(punchEvent);

			speedMult -= speedMult * 0.5f;

			animator.SetInteger("FightState", 0);
			busy = true;
		}
	}

	public void StartKick()
	{
		if (!busy)
		{
			speedMult -= speedMult * 0.8f;


			animator.SetInteger("FightState", 1);
			busy = true;
		}
	}

	public void GetHit()
	{
		wasHit = true;
	}

	void DealDamageAtPoint(Vector3 position, Vector3 size, int damage)
	{
		position.x = facingRight ? position.x : -position.x;

		var hits = Physics2D.OverlapBoxAll(transform.position + position, size, 360);

		foreach (var item in hits)
		{
			if (!item.name.Equals(name))
			{
				var damagable = item.GetComponent<Damagable>();

				if (damagable)
				{
					damagable.DealDamage(damage);
				}
			}
		}
	}


	void Jump()
	{
		if (jump > 0)
		{
			if (OnGround)
			{
				jumping = true;
				groundY = transform.position.y;
				jumpKey = 0;

				GetComponent<Rigidbody2D>().gravityScale = 0;
			}

			jump = 0;
		}

		if (jumping)
		{
			transform.position = new Vector3(transform.position.x, groundY + jumpCurve.Evaluate(jumpKey) * jumpForce);
			jumpKey += Time.fixedDeltaTime / jumpTime;

			if (jumpKey > 1 || (jumpKey > 0.5f && OnGround))
			{
				print(jumpKey);

				GetComponent<Rigidbody2D>().gravityScale = 1;
				jumping = false;
			}
		}

	}

	bool OnGround
	{
		get
		{
			Vector3 footpos = transform.position + Vector3.down * feetLength;

			if (Physics2D.Raycast(footpos, Vector3.down * 0.15f, 0.15f, layerMask))
			{
				return true;
			}

			return false;
		}
	}
}
