using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	[Header("Animation")]
	Animator playerAnimator;

	[Header("Particle Effects")]
	[SerializeField] ParticleSystem explosionParticle;
	[SerializeField] ParticleSystem dirtParticle;

	[Header("Sound Effects")]
	AudioSource audioSource;
	[SerializeField] AudioClip jumpSound;
	[SerializeField] AudioClip crashSound;

	[Header("Physics")]
	Rigidbody rb;
	[SerializeField] float jumpForce = 10f;
	[SerializeField] float gravityModifier = 1f;


	bool isOnGround = true;
	public bool gameOver = false;
	// Start is called before the first frame update
	void Start()
	{
		rb = GetComponent<Rigidbody>();
		playerAnimator = GetComponent<Animator>();
		audioSource = GetComponent<AudioSource>();
		Physics.gravity *= gravityModifier;
	}

	// Update is called once per frame
	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Space) && isOnGround && !gameOver)
		{
			rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
			isOnGround = false;
			playerAnimator.SetTrigger("Jump_trig");
			dirtParticle.Stop();
			audioSource.PlayOneShot(jumpSound, 1.0f);
		}
	}
	void OnCollisionEnter(Collision other)
	{
		if (other.gameObject.CompareTag("Ground") && !gameOver)
		{
			isOnGround = true;
			dirtParticle.Play();
		}
		if (other.gameObject.CompareTag("Obstacle"))
		{
			gameOver = true;
			Debug.Log("Game Over");
			playerAnimator.SetBool("Death_b", true);
			playerAnimator.SetInteger("DeathType_int", 1);
			explosionParticle.Play();
			dirtParticle.Stop();
			audioSource.PlayOneShot(crashSound, 1.0f);
		}
	}
}
