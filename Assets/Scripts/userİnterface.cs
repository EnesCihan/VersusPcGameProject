using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class userİnterface : MonoBehaviour
{
    public Text countDown;
    public Text message;
	public UnityEngine.UI.Button playAgain;
    private float Timer = 60;
    // Start is called before the first frame update
    void Start()
    {
        
    }

	// Update is called once per frame
	private void Update()
	{
		if (Timer > 0)
		{
			countDown.text = ("Time Remaing=" + Mathf.Round(Timer));
			Timer -= Time.deltaTime;
		}
		if (Mathf.Round(Timer) == 0)
		{
			if (FindObjectOfType<slash>().skorRed > FindObjectOfType<slash2>().skorBlue)
			{
				message.text = ("Red  Wins!");
				message.color = Color.red;
			}
			else if (FindObjectOfType<slash>().skorRed < FindObjectOfType<slash2>().skorBlue)
			{
				message.text = ("Blue  Wins!");
				message.color = Color.blue;
			}
			else
			{
				message.text = ("Draw!");
			}
			countDown.text = ("Finish");
			playAgain.gameObject.SetActive(true);
			FindObjectOfType<hareket>().Stop();
			FindObjectOfType<hareket2>().Stop();
			FindObjectOfType<slash>().enabled = false;
			FindObjectOfType<slash2>().enabled = false;
		}
	}
}
