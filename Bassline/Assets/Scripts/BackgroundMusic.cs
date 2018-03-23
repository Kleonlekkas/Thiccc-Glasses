using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(AudioSource))]
public class BackgroundMusic : MonoBehaviour {

    public float maxScale; //maximum size the bar can scale to
    public float scaleFactor; //how much the bar scales by per second
    private AudioSource audio; // our note from editor

    public Transform barPrefab; // set up generation of bars

    private List<Transform> listOfBars;
    // Use this for initialization
    void Start () {
        audio = GetComponent<AudioSource>();

        listOfBars = new List<Transform>();

        //Automatically Generate Bar Prefabs
        for (int i = 1; i < 256; i++)
        {
            Transform tempBar;

            //current width is 300
            tempBar = Instantiate(barPrefab, new Vector3(i * 300.0F, 0, 0), Quaternion.identity);

            listOfBars.Add(tempBar);
        }
    }
	
	// Update is called once per frame
	void Update () {

        //array of audio data
        float[] spectrum = new float[256];

        AudioListener.GetSpectrumData(spectrum, 0, FFTWindow.Rectangular);

        for (int i = 1; i < spectrum.Length - 1; i++)
        {
            Debug.DrawLine(new Vector3(i - 1, spectrum[i] + 10, 0), new Vector3(i, spectrum[i + 1] + 10, 0), Color.red);
            Debug.DrawLine(new Vector3(i - 1, Mathf.Log(spectrum[i - 1]) + 10, 2), new Vector3(i, Mathf.Log(spectrum[i]) + 10, 2), Color.cyan);
            Debug.DrawLine(new Vector3(Mathf.Log(i - 1), spectrum[i - 1] - 10, 1), new Vector3(Mathf.Log(i), spectrum[i] - 10, 1), Color.green);
            Debug.DrawLine(new Vector3(Mathf.Log(i - 1), Mathf.Log(spectrum[i - 1]), 3), new Vector3(Mathf.Log(i), Mathf.Log(spectrum[i]), 3), Color.blue);

            listOfBars[i].transform.localScale += new Vector3(0, (spectrum[i] * 100) * Time.deltaTime, 0);
            listOfBars[i].transform.position += new Vector3(0, (spectrum[i] * 100) / 2 * Time.deltaTime, 0);
        }

    }
}
