using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    public Transform player;
    public float xAxis, yAxis, zAxis;
    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(player.transform.position.x + xAxis, player.transform.position.y + yAxis, player.transform.position.z + zAxis);
    }
}
