using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    CharacterController controller;
    public FixedJoystick Fj;
    Vector3 velocity;
    public LayerMask Mask;
    public Transform Ground;
    int SoundControl;

    #region Ses
    public AudioSource source;
    public AudioClip[] StepSounds;
    public AudioClip[] WoodSound;
    public AudioClip JumpSound;
    #endregion
    #region Floats
    public float Distance = 0.3f;
    public float Speed;
    public float WalkSpeed;
    public float RunSpeed;
    public float JumpHeight;
    public float Gravity;
    float Timer;
    public float TimeBetveenSteps;
    #endregion
    #region Bools
    bool isGrounded;
    public bool Walk;
    public bool Run;
    bool isMoving;
    public bool KarakterDurmali = false;
    #endregion

    void Start()
    {
        Time.timeScale = 1;
        controller = GetComponent<CharacterController>();
        source = GetComponent<AudioSource>();
        Speed = WalkSpeed;
    }
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        switch (hit.transform.tag)
        {
            case "CÝM": SoundControl = 0; break;
            case "TAHTA": SoundControl = 1; break;
        }
    }
    void Update()
    {
        #region Movement
        float horizontal = Fj.Horizontal;
        float vertical = Fj.Vertical;

        Vector3 move = transform.right * horizontal + transform.forward * vertical;
        controller.Move(move * Speed * Time.deltaTime);
        #endregion

        #region Jump
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            velocity.y += Mathf.Sqrt(JumpHeight * -3.0f * Gravity);
        }
        #endregion

        #region Gravity
        isGrounded = Physics.CheckSphere(Ground.position, Distance, Mask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = 0f;
        }
        velocity.y += Gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
        #endregion

        #region MOVEMENT BOOLS
        if (Walk == true)
        {
            Speed = WalkSpeed;
        }
        if (Run == true)
        {
            Speed = RunSpeed;
            TimeBetveenSteps = 0.3f;
        }
        else
        {
            TimeBetveenSteps = 0.5f;
        }
        #endregion

        #region FOOTSTEPS
        if (horizontal != 0 || vertical != 0)
        {
            isMoving = true;
        }
        else
        {
            isMoving = false;
        }
        if (isMoving == true && isGrounded == true)
        {
            Timer -= Time.deltaTime;

            if (Timer <= 0)
            {
                switch (SoundControl)
                {
                    case 0: source.clip = StepSounds[Random.Range(0, StepSounds.Length)]; break;
                    case 1: source.clip = WoodSound[Random.Range(0, WoodSound.Length)]; break;
                }
                Timer = TimeBetveenSteps;
                source.pitch = Random.Range(0.85f, 1.15f);
                source.Play();
            }
        }
        else
        {
            Timer = TimeBetveenSteps;
        }
        #endregion
    }
    public void WalkState()
    {
        Walk = true;
        Run = false;
    }
    public void RunState()
    {
        Walk = false;
        Run = true;
    }
    public void Jump()
    {
        if (isGrounded)
        {
            velocity.y += Mathf.Sqrt(JumpHeight * -3.0f * Gravity);
            source.clip = JumpSound;
            source.PlayOneShot(JumpSound);
        }
    }
    IEnumerator PatikaBaslangic()
    {
        yield return new WaitForSeconds(2);
    }
}