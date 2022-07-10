using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpawnController : MonoBehaviour
{
    [SerializeField]
    private Transform rewardHolder;
    [SerializeField]
    private GameObject rewardPrefab;
    [SerializeField]
    private GameObject endGameUI;
    [SerializeField]
    private GameSettings gSetting;

    [SerializeField]
    private Vector3 verticalOffset = Vector3.zero;
    [SerializeField]
    private Vector3 horizontalOffset = Vector3.zero;

    private List<GameObject> rewardCollection;


    // Start is called before the first frame update
    void Start()
    {
        initializeCollection();
    }

    private void initializeCollection()
    {
        rewardCollection = new List<GameObject>();
        for (int i = 0; i < gSetting.CollectableSize; i++)
        {
            GameObject go = Instantiate(rewardPrefab, rewardHolder, false);
            go.name = "Collectible " + i;
            go.GetComponent<CollectibleController>().Respawn(verticalOffset, horizontalOffset, 0f);

            rewardCollection.Add(go);
        }
    }
    public int collectObject(string objectName)
    {
        int collectionScore = 0;
        foreach(GameObject go in rewardCollection)
        {
            if (objectName.Equals(go.name))
            {
                collectionScore = go.GetComponent<CollectibleController>().Collect();
                go.GetComponent<CollectibleController>().Respawn(verticalOffset, horizontalOffset, gSetting.CollectableSpawnRate);
                break;
            }
        }
        return collectionScore;
    }
    public void showEndGameUI()
    {
        endGameUI.SetActive(true);
    }
    public void restartGame()
    {
        SceneManager.LoadScene("GameScene");
    }
}
