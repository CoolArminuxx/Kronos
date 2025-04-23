using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Controller : MonoBehaviour
{
    [SerializeField] private float Speed = 5f;
    [SerializeField] private float JumpAmount;
    [SerializeField] private Transform playerOrientation;
    [SerializeField] private AudioSource JumpAudio;
    [SerializeField] private AudioSource[] WalkingAudio;
    public bool NarratorSpeaking;
    private bool canJump;
    public bool isWalking;
    public bool playingWalkAudio;
    private Vector3 playerDirection;
    public Rigidbody rb;
    private float hInput;
    private float vInput;


    private void Start()
    {
        canJump = true;
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    private void Update()
    {
        PlayerInput();
        PlayerMovement();
        PlayerJump();
        PlayerSprint();
        if (isWalking == true && playingWalkAudio == false)
        {
            //StartCoroutine(WalkAudio());
        }
    }

    private void PlayerInput()
    {
        hInput = Input.GetAxisRaw("Horizontal");
        vInput = Input.GetAxisRaw("Vertical");

        if (hInput != 0 || vInput != 0)
        {
            isWalking = true;
        }
        else
        {
            isWalking = false;
            playingWalkAudio = false;
        }
    }

    private void PlayerMovement()
    {
        playerDirection = playerOrientation.forward * vInput + playerOrientation.right * hInput;
        rb.AddForce(playerDirection.normalized * Speed, ForceMode.Force);
    }

    private void PlayerJump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && canJump == true)
        {
            StartCoroutine(JumpDelay());
        }
    }

    private void PlayerSprint()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            Speed = Speed * 2;
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            Speed = 25f;
        }
    }

    private IEnumerator JumpDelay()
    {
        canJump = false;
        rb.AddForce(Vector2.up * JumpAmount, ForceMode.Impulse);
        JumpAudio.Play();
        yield return new WaitForSeconds(1f);
        canJump = true;
    }

    private IEnumerator WalkAudio()
    {
        playingWalkAudio = true;
        int num = Random.Range(0, 4);
        WalkingAudio[num].Play();
        yield return new WaitUntil(() => WalkingAudio[num].time >= WalkingAudio[num].clip.length);
        playingWalkAudio = false;
    }
}
