using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleController : MonoBehaviour
{
    [SerializeField]
    private int _scoreItem = 5;

    private bool isCollectable = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public int Collect()
    {
        isCollectable = false;
        GetComponent<Renderer>().enabled = false;
        GetComponent<Collider>().enabled = false;
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

        float xCoord = Random.Range(verticalOffset.x, verticalOffset.y);
        float zCoord = Random.Range(horizontalOffset.x, horizontalOffset.y);

        transform.position = new Vector3(xCoord, transform.position.y, zCoord);
        transform.rotation = Quaternion.identity;
        GetComponent<Renderer>().enabled = true;
        GetComponent<Collider>().enabled = true;
    }
}
