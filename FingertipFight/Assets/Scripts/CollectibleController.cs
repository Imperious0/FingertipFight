using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Renderer)), RequireComponent(typeof(Collider))]
public class CollectibleController : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem bubbleParticle;
    [SerializeField]
    private int _scoreItem = 5;

    private Renderer m_Renderer;
    private Collider m_Collider;

    private bool isCollectable = true;

    private void Awake()
    {
        m_Renderer = GetComponent<Renderer>();
        m_Collider = GetComponent<Collider>();
    }
    public int Collect()
    {
        isCollectable = false;
        bubbleParticle.Play();
        m_Renderer.enabled = false;
        m_Collider.enabled = false;
        return _scoreItem;
    }
    public void Respawn(Vector3 verticalOffset, Vector3 horizontalOffset, float respawnDuration)
    {
        if(respawnDuration <= 0f)
        {
            //Instant Respawner that override iscollectable
            StartCoroutine(RespawnSelf(verticalOffset, horizontalOffset, respawnDuration));
            return;
        }
        if(!isCollectable)
        {
            StartCoroutine(RespawnSelf(verticalOffset, horizontalOffset, respawnDuration));
        }
    }
    private IEnumerator RespawnSelf(Vector3 verticalOffset, Vector3 horizontalOffset, float respawnDuration)
    {
        float currentTime = Time.time + respawnDuration;
        while(Time.time <= currentTime)
        {
            yield return null;
        }
        isCollectable = true;

        float xCoord = Random.Range(horizontalOffset.x, horizontalOffset.y);
        float zCoord = Random.Range(verticalOffset.x, verticalOffset.y);

        transform.position = new Vector3(xCoord, transform.position.y, zCoord);
        transform.rotation = Quaternion.identity;
        m_Renderer.enabled = true;
        m_Collider.enabled = true;
    }
}
