using UnityEngine;

public class Goal : MonoBehaviour
{
    [SerializeField] GameObject goalTextObj;
    [SerializeField] Gameplay gameplayComponent;

    const string playerTag = "Player";

    private void Start()
    {
        CloseText();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(playerTag))
        {
            //PlayerController player = other.GetComponent<PlayerController>();
            goalTextObj.SetActive(true);
            gameplayComponent.ShowGameOverPanel();
        }
    }

    public void CloseText()
    {
        goalTextObj.SetActive(false);
    }
}
