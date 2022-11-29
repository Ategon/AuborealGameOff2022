using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolTipsTrigger : MonoBehaviour
{
    [SerializeField] private ToolTips toolTips;
    [SerializeField] private string line;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            toolTips.gameObject.SetActive(true);
            toolTips.line = line;
            toolTips.EnterLine();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            toolTips.gameObject.GetComponent<Animator>().Play("ToolTipsFadeOut");
            this.gameObject.SetActive(false);
        }
    }
}
