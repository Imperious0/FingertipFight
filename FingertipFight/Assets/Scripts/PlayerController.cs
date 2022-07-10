using TMPro;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private GameSettings gSetting;

    [SerializeField]
    private MotionCapturer mCapture;

    private Rigidbody playerRigidbody;

    [SerializeField]
    private TextMeshProUGUI scoreText;
    [SerializeField]
    private TextMeshProUGUI endGameScoreText;

    private int _myScore = 0;

    Vector3 currentMovement = Vector3.zero;

    bool currentlyIdle = true;
    bool gameSuccess = false;
    public int MyScore { get => _myScore; set { _myScore = value; scoreText.text = "SCORE: " + _myScore; endGameScoreText.text = scoreText.text; /* Invoke Refresh UI */ } }

    // Start is called before the first frame update
    void Awake()
    {
        playerRigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if (gameSuccess)
            return;

        currentMovement = mCapture.getMovementVector();
     
        if(currentMovement.magnitude > 0.1f)
        {
            float angle = Vector2.SignedAngle(currentMovement, Vector3.down);
            transform.rotation = Quaternion.Euler(0, angle, 0);

            playerRigidbody.velocity = transform.forward * gSetting.PlayerMovementSpeed;
            currentlyIdle = false;
        }
        else
        {
            if (!currentlyIdle)
            {
                currentlyIdle = true;
                playerRigidbody.velocity = Vector3.zero;
                playerRigidbody.angularVelocity = Vector3.zero;
                currentMovement = Vector3.zero;
            }
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Collectable"))
        {
            MyScore += GameObject.Find("GameManager").GetComponent<SpawnController>().collectObject(collision.gameObject.name);
            if(MyScore >= gSetting.GameSuccessPoint)
            {
                gameSuccess = true;
                GameObject.Find("GameManager").GetComponent<SpawnController>().showEndGameUI();
            }
        }
    }
}
