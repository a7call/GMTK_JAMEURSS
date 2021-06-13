using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class QuitMenu : MonoBehaviour
{
    // Start is called before the first frame update

    public TextMeshProUGUI countText;

    void Start()
    {
        DisplayPoints();
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void DisplayPoints()
    {
        countText.text = GameManager.Instance.nbPoints.ToString();
       
    }
}
