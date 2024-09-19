using Unity.Netcode;
using UnityEngine;

public class CameraFollow : NetworkBehaviour
{
    [SerializeField] private Transform playerTransform;
    [SerializeField] private Vector3 offset;
    [SerializeField] private float smoothSpeed = 0.125f;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    void Start()
    {

        if(offset == Vector3.zero)
        {
            offset = transform.position - playerTransform.position;
        }
    }

    private void Update()
    {
        if(!IsOwner) return;

        if (playerTransform == null)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                playerTransform = player.transform;
            }
        }
    }

    void LateUpdate()
    {
        if (!IsOwner) return;

        if(playerTransform != null)
        {
            Vector3 desiredPos = playerTransform.position - offset;
            Vector3 smoothedPos = Vector3.Lerp(transform.position, desiredPos, smoothSpeed);
            transform.position = smoothedPos;
        }
    }

}
