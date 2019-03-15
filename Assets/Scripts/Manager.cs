using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Manager : MonoBehaviour
{
	PlayerController playerController;
	AIController aiController;
	Damagable playerDamagable, aiDamagable;
	Fighter playerFighter;

	public Healthbar playerHealth;
	public Healthbar enemyHealth;

	bool fadeOut = false;

	public Animator canvasAnimator, fightAnimator;
	public Image fadeImage;

	Vector3 playerHealthPosition;
	Vector3 enemyHealthPosition;

	bool showHealth = false;

	private void Update()
	{
		Color toVal = fadeOut ? Color.white : new Color(1, 1, 1, 0);

		fadeImage.color = Color.Lerp(fadeImage.color, toVal, Time.deltaTime * 4f);

		Vector3 lerpOffset = showHealth ? Vector3.zero : Vector3.up * 400 ;

		playerHealth.transform.localPosition = Vector3.Lerp(playerHealth.transform.localPosition, playerHealthPosition + lerpOffset, Time.deltaTime * 4);
		enemyHealth.transform.localPosition = Vector3.Lerp(enemyHealth.transform.localPosition, enemyHealthPosition + lerpOffset, Time.deltaTime * 4);

		playerHealth.Value = (float)playerDamagable.health / playerDamagable.maxHealth;
		enemyHealth.Value = (float)aiDamagable.health / aiDamagable.maxHealth;

		print(aiDamagable.health / aiDamagable.maxHealth);
	}

	void Start()
    {
		fadeImage.gameObject.SetActive(true);

		playerController = FindObjectOfType<PlayerController>();
		aiController = FindObjectOfType<AIController>();

		aiController.enabled = false;
		playerController.enabled = false;

		enemyHealthPosition = enemyHealth.transform.localPosition;
		playerHealthPosition = playerHealth.transform.localPosition;

		playerDamagable = playerController.GetComponent<Damagable>();
		aiDamagable = aiController.GetComponent<Damagable>();

		playerFighter = playerController.fighter;

		FindObjectOfType<AudioSource>().time = 4f;

		StartCoroutine(StartStage());
    }

	IEnumerator StartStage()
	{
		yield return new WaitForSeconds(1f);

		canvasAnimator.Play("Intro");

		yield return new WaitForSeconds(3f);

		playerFighter.xVelocity = 1;
		aiController.fighter.xVelocity = -1;

		yield return new WaitForSeconds(1);

		aiController.fighter.xVelocity = 0;
		playerFighter.xVelocity = 0;

		yield return new WaitForSeconds(1f);

		showHealth = true;
		fightAnimator.Play("Fight");

		yield return new WaitForSeconds(1f);

		playerController.enabled = true;

		yield return new WaitForSeconds(1f);

		aiController.enabled = true;
	}
}
