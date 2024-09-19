using Unity.Netcode;
using UnityEngine;
using Cinemachine;

public class Player : NetworkBehaviour
{
    public float speed = 5f;
    public float jumpForce = 7f;
    private Rigidbody rb;

    private bool isGrounded;

    [SerializeField] private CinemachineVirtualCamera vc;
    [SerializeField] private AudioListener audioListener;

    public override void OnNetworkSpawn()
    {
        if (IsOwner)
        {
            rb = GetComponent<Rigidbody>();
            audioListener.enabled = true;
            vc.Priority = 2;
        }
        else
        {
            vc.Priority = 0;
        }
    }

    void Update()
    {
        if (!IsOwner) return;

       OnMove();
    }

    private void OnMove()
    {
        float moveInput = Input.GetAxis("Horizontal");

        rb.velocity = new Vector3(moveInput * speed, rb.velocity.y);

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.velocity = new Vector3(rb.velocity.x, jumpForce);
            isGrounded = false;
            SubmitJumpRequestServerRpc();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!IsOwner) return;

        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true; 
        }
    }

    [ServerRpc]
    void SubmitJumpRequestServerRpc()
    {
        JumpClientRpc();
    }

    [ClientRpc]
    void JumpClientRpc()
    {
        if (!IsOwner)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
    }

}
