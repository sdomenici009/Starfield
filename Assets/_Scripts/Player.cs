﻿using UnityEngine;
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

	[SerializeField]
	private float initialDamageDelayTime;
	private float damageDelayTimer = 0f;

	[SerializeField]
	private AudioClip damagedClip;

	[SerializeField]
	private Cardboard cardboard;

	private Transform head;

	private Vector3 bulletSpawnOffset = new Vector3(0, -.15f, 0);

	void Start()
	{
		head = cardboard.GetComponentInChildren<CardboardHead>().transform;
	}
	
	void Update()
	{
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
		if(collision.collider.tag == "Enemy" || collision.collider.tag == "EnemyProjectile")
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