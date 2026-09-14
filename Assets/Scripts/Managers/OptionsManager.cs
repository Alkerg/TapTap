using UnityEngine;
using TMPro;
public class OptionsManager : MonoBehaviour
{
    [SerializeField]
    private GameObject optionsPanel;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ToggleOptionsPanel()
    {
        optionsPanel.SetActive(!optionsPanel.activeSelf);
    }

}
