using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonScripts : MonoBehaviour {
    //Make sure to attach these Buttons in the Inspector
    public Button m_instructions, m_back;


    //Jesus christ what terrible coding conventions
    void Start()
    {
        //Button playButton = m_play.GetComponent<Button>();
        Button instructionsButton;
        Button backButton;
        //Calls the TaskOnClick method when you click the Button
        //playButton.onClick.AddListener(TaskOnClick);

        // m_instructions.onClick.AddListener(delegate { TaskWithParameters("Hello"); });

        //if were in our instruction menu, we need the back button hooked up
        if (SceneManager.GetActiveScene().name == "instruction_menu")
        {
            backButton = m_back.GetComponent<Button>();
            backButton.onClick.AddListener(GoBack);
        } else if (SceneManager.GetActiveScene().name == "main_menu")
        {
            instructionsButton = m_instructions.GetComponent<Button>();
            instructionsButton.onClick.AddListener(GoToInstructions);
        }
    }

    void GoToInstructions()
    {
        SceneManager.LoadScene("instruction_menu");
    }

    void GoBack()
    {
        SceneManager.LoadScene("main_menu");
    }

    void TaskWithParameters(string message)
    {
        //Output this to console when the Button is clicked
        Debug.Log(message);
    }
}