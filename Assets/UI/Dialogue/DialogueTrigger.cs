
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    [SerializeField] private Dialogue dialogue;
    [SerializeField] private string[] lines;
    private void OnTriggerEnter2D()
    {
        dialogue.gameObject.SetActive(true);
        dialogue.StartDialogue(lines);
        this.gameObject.SetActive(false);
    }
}
