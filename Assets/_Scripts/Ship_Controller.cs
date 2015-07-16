using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[System.Serializable]
public class Boundary
{
	public float xMin, xMax, yMin, yMax;
}

public class Ship_Controller : MonoBehaviour
{
	[SerializeField]
	private GameObject bulletPrefab;

	[SerializeField]
	private AudioClip bulletAudioClip;

	[SerializeField]
	private Quaternion bulletInitialRotation;

	[SerializeField]
	private float bulletLifetime;

	[SerializeField]
	private float speed;

	[SerializeField]
	private float bulletSpeed;

	[SerializeField]
	private float tilt;

	[SerializeField]
	private Boundary boundary;

	private Vector3 shipColliderSize;
	private Vector3 bulletColliderSize;

	[SerializeField]
	private float turnSpeed;

	[SerializeField]
	private float doubleTapDelay;
	private float doubleTapTimer;

	private bool singleTap;
	private KeyCode prevKey;
	
	private Rigidbody rigidbody;

	[SerializeField]
	private ParticleSystem clouds;

	[SerializeField]
	private ParticleSystem stars;

	[SerializeField]
	private Transform crosshair;

	[SerializeField]
	private Image healthBar;

	private int baseHealth = 50;
	private int currentHealth = 50;

	public int score = 0;
	public Text scoreText;

	[SerializeField]
	private float initialDamageDelayTime;
	private float damageDelayTimer = 0f;

	[SerializeField]
	private Material mat;

	private bool rotating = false;

	[SerializeField]
	private ParticleSystem ps1;

	[SerializeField]
	private ParticleSystem ps2;

	[SerializeField]
	private AudioClip damagedClip;

	[SerializeField]
	private Game_Controller gc;

	[SerializeField]
	private Cardboard cardboard;

	void Start()
	{
		Screen.sleepTimeout = (int)SleepTimeout.NeverSleep;

		shipColliderSize = GetComponent<BoxCollider>().size;
		bulletColliderSize = bulletPrefab.GetComponent<BoxCollider>().size;
		doubleTapTimer = doubleTapDelay;
		rigidbody = GetComponent<Rigidbody>();
		mat.color = Color.white;
	}
	
	void Update()
	{
		if(currentHealth <= 0)
		{
			ps1.transform.position = transform.position;
			ps1.Emit(100);
			ps2.transform.position = transform.position;
			ps2.Emit(100);

			Destroy(gameObject);
			Destroy(crosshair.gameObject);

			gc.StartCoroutine("EndGameScreen");
		}

		doubleTapTimer -= Time.deltaTime;
		damageDelayTimer -= Time.deltaTime;

		if(Input.GetKeyDown(KeyCode.A))
		{
			if(prevKey == KeyCode.A && doubleTapTimer > 0)
			{
				if(!rotating)
				{
					rotating = true;
					prevKey = KeyCode.None;
					StopCoroutine("Rotate");
					StartCoroutine("Rotate", 10);
					rigidbody.AddForce(new Vector3(-2, 0, 0));
				}
			}
			else
			{
				doubleTapTimer = doubleTapDelay;
				prevKey = KeyCode.A;
			}
		}

		if(Input.GetKeyDown(KeyCode.D))
		{
			if(prevKey == KeyCode.D && doubleTapTimer > 0)
			{
				if(!rotating)
				{
					rotating = true;
					prevKey = KeyCode.None;
					StartCoroutine("Rotate", -10);
					rigidbody.AddForce(new Vector3(2, 0, 0));
				}
			}
			else
			{
				doubleTapTimer = doubleTapDelay;
				prevKey = KeyCode.D;
			}
		}

		if(Input.GetMouseButtonDown(0) || cardboard.Triggered)
		{
			Vector3 shotPosition = transform.position + (crosshair.position - transform.position).normalized*.25f;

			//GameObject bullet = (GameObject)Instantiate(bulletPrefab, shotPosition, bulletInitialRotation);
			GameObject bullet = (GameObject)Instantiate(bulletPrefab, cardboard.gameObject.transform.GetChild(0).position + cardboard.gameObject.transform.GetChild(0).forward*.25f, bulletInitialRotation);
			bullet.GetComponent<Rigidbody>().AddForce(cardboard.gameObject.transform.GetChild(0).forward*bulletSpeed);
			bullet.transform.LookAt(cardboard.gameObject.transform.GetChild(0).forward);
			//bullet.GetComponent<Rigidbody>().AddForce((crosshair.position - shotPosition).normalized*bulletSpeed);
			//bullet.transform.LookAt(crosshair.position);
			AudioSource.PlayClipAtPoint(bulletAudioClip, transform.position, .75f);
			Destroy(bullet, bulletLifetime);
		}
	}

	void FixedUpdate ()
	{
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");
		
		Vector3 movement = new Vector3 (moveHorizontal, moveVertical, 0.0f);
		rigidbody.velocity = movement * speed;

		rigidbody.position = new Vector3 (Mathf.Clamp (rigidbody.position.x, boundary.xMin, boundary.xMax), 
										  Mathf.Clamp (rigidbody.position.y, boundary.yMin, boundary.yMax), 
										  transform.position.z);

		//rigidbody.rotation = Quaternion.Euler (rigidbody.velocity.y * tilt, 0.0f, rigidbody.velocity.x * -tilt);
	}

	IEnumerator Rotate(float angle)
	{
		float totalRot = 0;

		while (Mathf.Abs(totalRot) < 360)
		{
			transform.RotateAround(transform.position, Vector3.forward, angle);
			totalRot += angle;
			yield return null;
		}

		rotating = false;
	}

	void OnCollisionEnter(Collision collision)
	{
		if(collision.collider.tag == "Meteor")
		{
			if(damageDelayTimer < 0)
			{
				AudioSource.PlayClipAtPoint(damagedClip, transform.position, .75f);
				StartCoroutine("FlashRed");
				damageDelayTimer = initialDamageDelayTime;
				currentHealth -= 5;
				healthBar.rectTransform.anchoredPosition = new Vector2(healthBar.rectTransform.anchoredPosition.x -(134f*.1f)/2f, healthBar.rectTransform.anchoredPosition.y);
				healthBar.rectTransform.sizeDelta = new Vector2(healthBar.rectTransform.sizeDelta.x -134f*.1f, healthBar.rectTransform.sizeDelta.y);
			}
		}
	}

	IEnumerator FlashRed()
	{
		float flashDelay = .5f;
		float timer = 2f;
		Color currentColor = Color.red;

		float shakeAmount = 0.7f;
		float shake = 1f;

		Quaternion initialRot = Camera.main.transform.rotation;

		while (timer > 0)
		{
			/*
			if(shake > 0)
			{
				Camera.main.transform.rotation = Quaternion.Euler(initialRot.eulerAngles.x + Random.Range(0, shakeAmount), initialRot.eulerAngles.y + Random.Range(0, shakeAmount), initialRot.eulerAngles.z + Random.Range(0, shakeAmount));
				shake -= .05f;
			}
			else
			{
				if(Camera.main.transform.rotation != initialRot)
					Camera.main.transform.rotation = initialRot;
			}*/

			flashDelay -= .1f;
			timer -= .05f;
			if(flashDelay < 0)
			{
				if(mat.color == Color.white)
					mat.color = Color.red;
				else
					mat.color = Color.white;
				flashDelay = 1f;
			}
			yield return null;
		}

		//Camera.main.transform.rotation = initialRot;
		mat.color = Color.white;
	}
}
