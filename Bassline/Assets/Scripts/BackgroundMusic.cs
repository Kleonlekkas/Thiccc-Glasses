using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(AudioSource))]
public class BackgroundMusic : MonoBehaviour {

    public float maxScale; //maximum size the bar can scale to
    public float scaleFactor; //how much the bar scales by per second
    private AudioSource audio; // our note from editor

    public Transform barPrefab; // set up generation of bars
    private bool invert = true;//used to move the background bars up or down on every other update
    private System.Random rand;
    private List<Color32> colors; //list of possible colors the bars can be
    private byte randomColorRange; // generate random colors in darker values

    private List<Transform> listOfBars;
    
    void Start () {
        
        // Make floor invisible
        GetComponent<MeshRenderer>().enabled = false;

        audio = GetComponent<AudioSource>();

        listOfBars = new List<Transform>();
        colors = new List<Color32>();

        for(int i=0; i < 10; i++) { 
            // get new range of colors
            randomColorRange = (byte)Random.Range(60+(i*4), 120+(i*5));
            //populate with dark rainbow colors randomly selected
            colors.Add(new Color32(randomColorRange, 0, 0, 100)); // R
            colors.Add(new Color32(0, randomColorRange, 0, 100)); // G
            colors.Add(new Color32(0, 0, randomColorRange, 100)); // B
            colors.Add(new Color32(randomColorRange, randomColorRange, 0, 100)); // RG
            colors.Add(new Color32(randomColorRange, 0, randomColorRange, 100)); // RB
            colors.Add(new Color32(0, randomColorRange, randomColorRange, 100)); // BG
        }

        //Automatically Generate Bar Prefabs
        for (int i = 1; i < 256; i++)
        {
            Transform tempBar;

            //current width is 300
            tempBar = Instantiate(barPrefab, new Vector3(i * 150.0F, -30, 1), Quaternion.identity);
            //Assign a random color from the color list

            tempBar.GetComponent<Renderer>().material.color = colors[Random.Range(1, colors.Count - 1)];
            //Random.seed = Random.Range(1, 5);
            
            listOfBars.Add(tempBar);
        }
    }
	
	// Update is called once per frame
	void Update () {
        invert = !invert;
        //array of audio data
        float[] spectrum = new float[256];

        AudioListener.GetSpectrumData(spectrum, 0, FFTWindow.Rectangular);

        for (int i = 1; i < spectrum.Length - 1; i++)
        {
            //Debug.DrawLine(new Vector3(i - 1, spectrum[i] + 10, 0), new Vector3(i, spectrum[i + 1] + 10, 0), Color.red);
            //Debug.DrawLine(new Vector3(i - 1, Mathf.Log(spectrum[i - 1]) + 10, 2), new Vector3(i, Mathf.Log(spectrum[i]) + 10, 2), Color.cyan);
            //Debug.DrawLine(new Vector3(Mathf.Log(i - 1), spectrum[i - 1] - 10, 1), new Vector3(Mathf.Log(i), spectrum[i] - 10, 1), Color.green);
            //Debug.DrawLine(new Vector3(Mathf.Log(i - 1), Mathf.Log(spectrum[i - 1]), 3), new Vector3(Mathf.Log(i), Mathf.Log(spectrum[i]), 3), Color.blue);
            if (invert)
            {
                listOfBars[i].transform.localScale += new Vector3(0, (spectrum[i] * 100) * (Time.deltaTime/2), 0);
                listOfBars[i].transform.position += new Vector3(0, (spectrum[i] * 100) / 2 * Time.deltaTime, 0);
            }
            else
            {
                listOfBars[i].transform.localScale -= new Vector3(0, (spectrum[i] * 200) * (Time.deltaTime*2), 0);
                listOfBars[i].transform.position -= new Vector3(0, (spectrum[i] * 200) / 2 * Time.deltaTime, 0);
            }
        }

    }
}
