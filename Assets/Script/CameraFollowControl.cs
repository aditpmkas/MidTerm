using UnityEngine;
using Cinemachine;

public class CameraFollowControl : MonoBehaviour
{
    public CinemachineVirtualCamera virtualCamera;
    private Transform playerTransform;
    private Vector3 lastCameraPosition;

    void Start()
    {
        playerTransform = transform; // Ambil posisi awal player
    }

    void Update()
    {
        if (playerTransform.position.y < -5) // Misalnya, jika jatuh di bawah Y = -5
        {
            if (virtualCamera.Follow != null)
            {
                lastCameraPosition = virtualCamera.transform.position; // Simpan posisi kamera terakhir
                virtualCamera.Follow = null; // Matikan follow
            }
        }
        else
        {
            if (virtualCamera.Follow == null)
            {
                virtualCamera.Follow = playerTransform; // Balikkan follow saat naik lagi
            }
        }
    }
}
