using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Ship : MonoBehaviour
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
	private float bulletSpeed;

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
	private AudioClip damagedClip;

	[SerializeField]
	private GameManager gc;

	[SerializeField]
	private Cardboard cardboard;

	void Start()
	{
	}
	
	void Update()
	{
		if(currentHealth <= 0)
		{
			gc.StartCoroutine("EndGameScreen");
		}

		damageDelayTimer -= Time.deltaTime;

		if(Input.GetMouseButtonDown(0) || cardboard.Triggered)
		{
			GameObject bullet = (GameObject)Instantiate(bulletPrefab, cardboard.gameObject.transform.GetChild(0).position + new Vector3(0, -.15f, 0) + cardboard.gameObject.transform.GetChild(0).forward*.5f, bulletInitialRotation);
			bullet.GetComponent<Rigidbody>().AddForce(cardboard.gameObject.transform.GetChild(0).forward*bulletSpeed - bullet.transform.position);
			bullet.transform.rotation = cardboard.HeadPose.Orientation;
			AudioSource.PlayClipAtPoint(bulletAudioClip, transform.position, .75f);
			Destroy(bullet, bulletLifetime);
		}
	}

	void OnCollisionEnter(Collision collision)
	{
		if(collision.collider.tag == "Meteor")
		{
			if(damageDelayTimer < 0)
			{
				AudioSource.PlayClipAtPoint(damagedClip, transform.position, .75f);
				damageDelayTimer = initialDamageDelayTime;
				currentHealth -= 10;
				healthBar.rectTransform.anchoredPosition = new Vector2(healthBar.rectTransform.anchoredPosition.x -(200f*.2f)/2f, healthBar.rectTransform.anchoredPosition.y);
				healthBar.rectTransform.sizeDelta = new Vector2(healthBar.rectTransform.sizeDelta.x -200f*.2f, healthBar.rectTransform.sizeDelta.y);
			}
		}
	}
}
