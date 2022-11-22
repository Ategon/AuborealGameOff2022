using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;
using Assets.Player.Inventory;
using Assets.EventSystem;

public class Dialogue : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textComponent;
    [SerializeField] private string[] lines;
    [SerializeField] private float textSpeed;
    [SerializeField] private InputActionReference mouseButton;

    private float animationTime = 0.5f;
    private int index;
    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        textComponent.text = string.Empty;
        anim = GetComponent<Animator>();
    }




    public void DialogueButton()
    {
        if (textComponent.text == lines[index])
        {
            NextLine();
        }
        else
        {
            StopAllCoroutines();
            textComponent.text = lines[index];
        }
    }

    public void StartDialogue()
    {
        Time.timeScale = 0f;
        index = 0;
        StartCoroutine(TypeLine());
    }

    public void StartDialogue(string[] lines)
    {
        this.lines = lines;
        Invoke("StartDialogue", animationTime);
    }

    private void NextLine()
    {
        if (index < lines.Length-1)
        {
            index++;
            textComponent.text = string.Empty;
            StartCoroutine(TypeLine());
        }
        else
        {
            Time.timeScale = 1f;
            textComponent.text = string.Empty;
            anim.Play("DialogueBoxSlideOut");
        }
    }

    public void DisableThis()
    {
        this.gameObject.SetActive(false);
    }

    IEnumerator TypeLine()
    {
        foreach (char c in lines[index].ToCharArray())
        {
            textComponent.text += c;
            yield return new WaitForSecondsRealtime(textSpeed);
        }
    }



}
