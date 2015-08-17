using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Player : Actor
{
	[SerializeField]
	private GameObject bulletPrefab;

	[SerializeField]
	private Image healthBar;

	private int baseHealth = 50;
	private int currentHealth = 50;

	public Cursor cursor;

	[SerializeField]
	private float initialDamageDelayTime;
	private float damageDelayTimer = 0f;

	[SerializeField]
	private AudioClip damagedClip;

	[SerializeField]
	private Cardboard cardboard;

	private Transform head;

	private Vector3 bulletSpawnOffset = new Vector3(0, -.15f, 0);

	private RaycastHit hit;

	void Start()
	{
		head = cardboard.GetComponentInChildren<CardboardHead>().transform;
	}

	void Update()
	{
		if(Physics.Raycast(head.position + head.forward*.5f + bulletSpawnOffset, head.forward, out hit, Mathf.Infinity))
		{
			if(gameManager.betweenLevels && hit.collider.gameObject.layer == 5)
			{
				cursor.target = hit.point;
			}
		}

		if(currentHealth <= 0)
		{
			gameManager.StartCoroutine("EndGameScreen");
		}

		damageDelayTimer -= Time.deltaTime;

		if(Input.GetMouseButtonDown(0) || cardboard.Triggered)
		{
			Projectile projectile = ((GameObject)Instantiate(bulletPrefab, head.position + head.forward*.5f + bulletSpawnOffset, Quaternion.identity)).GetComponent<Projectile>();
			projectile.Initialize(transform.position, head);
		}
	}

	void OnCollisionEnter(Collision collision)
	{
		if(collision.collider.tag == "Enemy")
		{
			if(damageDelayTimer < 0)
			{
				AudioSource.PlayClipAtPoint(damagedClip, transform.position, .75f);
				damageDelayTimer = initialDamageDelayTime;
				currentHealth -= collision.collider.GetComponent<Enemy>().CollisionDamage;
				healthBar.rectTransform.anchoredPosition = new Vector2(healthBar.rectTransform.anchoredPosition.x -(200f*.2f)/2f, healthBar.rectTransform.anchoredPosition.y);
				healthBar.rectTransform.sizeDelta = new Vector2(healthBar.rectTransform.sizeDelta.x -200f*.2f, healthBar.rectTransform.sizeDelta.y);
			}
		}

		if(collision.collider.tag == "EnemyProjectile")
		{
			if(damageDelayTimer < 0)
			{
				AudioSource.PlayClipAtPoint(damagedClip, transform.position, .75f);
				damageDelayTimer = initialDamageDelayTime;
				currentHealth -= collision.collider.GetComponent<EnemyProjectile>().Damage;
				healthBar.rectTransform.anchoredPosition = new Vector2(healthBar.rectTransform.anchoredPosition.x -(200f*.2f)/2f, healthBar.rectTransform.anchoredPosition.y);
				healthBar.rectTransform.sizeDelta = new Vector2(healthBar.rectTransform.sizeDelta.x -200f*.2f, healthBar.rectTransform.sizeDelta.y);
			}
		}
	}
}
