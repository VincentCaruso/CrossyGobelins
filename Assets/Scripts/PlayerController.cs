using System.Collections;
using System.Collections.Generic;

using DG.Tweening;

using UnityEngine;

public class PlayerController : MonoBehaviour
{
	[SerializeField]
	GameManager GameManager;

	[SerializeField]
	int CurrentXPos = 0;

	[SerializeField]
	int MaxXPos = 5;

	[SerializeField]
	LayerMask Obstacle;

	[SerializeField]
	GameObject PlayerVisual;

	bool isAlive = true;

	Animator animator;
	AudioSource src;

	public GameObject PS;
	public AudioClip JumpSound;
	public AudioClip CrashSound;

	private void Start()
	{
		animator = GetComponent<Animator>();
		src = GetComponent<AudioSource>();
	}

	// Update is called once per frame
	void Update()
	{
		if (!isAlive)
		{
			return;
		}

		//Mouvement vertical
		if (Input.GetKeyDown(KeyCode.UpArrow))
		{
			Ray r = new Ray(transform.position, Vector3.forward);
			Debug.DrawRay(r.origin, r.direction, Color.red, 0.5f);

			if (!Physics.Raycast(r, out RaycastHit hit, 1f, Obstacle))
			{
				GameManager.GoUp();
				animator.SetTrigger("isJump");
				PlayerVisual.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
				PlaySound();
			}
		}

		//Mouvement horizontal
		if (Input.GetKeyDown(KeyCode.LeftArrow))
		{
			Ray r = new Ray(transform.position, Vector3.left);
			Debug.DrawRay(r.origin, r.direction, Color.red, 0.5f);

			if (!Physics.Raycast(r, out RaycastHit hit, 1f, Obstacle))
			{
				CurrentXPos--;
				animator.SetTrigger("isJump");
				PlayerVisual.transform.rotation = Quaternion.Euler(0f, -90f, 0f);
				PlaySound();
			}
		}

		if (Input.GetKeyDown(KeyCode.RightArrow))
		{
			Ray r = new Ray(transform.position, Vector3.right);
			Debug.DrawRay(r.origin, r.direction, Color.red, 0.5f);

			if (!Physics.Raycast(r, out RaycastHit hit, 1f, Obstacle))
			{
				CurrentXPos++;
				animator.SetTrigger("isJump");
				PlayerVisual.transform.rotation = Quaternion.Euler(0f, 90f, 0f);
				PlaySound();
			}
		}

		if (CurrentXPos >= MaxXPos)
		{
			CurrentXPos = MaxXPos;
		}

		if (CurrentXPos <= -MaxXPos)
		{
			CurrentXPos = -MaxXPos;
		}

		transform.DOMove(new Vector3(CurrentXPos, 0f, 0f), 0.2f).SetDelay(0.1f);
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Enemy")
		{
			isAlive = false;

			//Active la physique et ajouter une force imm?diate
			Rigidbody rb = GetComponent<Rigidbody>();
			rb.isKinematic = false;
			rb.AddForce(Vector3.up * 10f + Random.Range(-2f, 2f) * Vector3.left, ForceMode.Impulse);
			rb.AddTorque(new Vector3(20f, 40f, 30f));

			GameManager.Die();
			print("Player is dead");

			//cr?e le moteur de particules et le met ? la postion du joueur
			GameObject go = Instantiate(PS);
			go.transform.position = transform.position;
			src.PlayOneShot(CrashSound);
			src.pitch = 1f;
		}
	}

	void PlaySound()
    {
		src.PlayOneShot(JumpSound);
		src.pitch = Random.Range(0.8f, 1.2f);
    }
}
