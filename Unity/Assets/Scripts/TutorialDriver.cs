using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class TutorialDriver : MonoBehaviour {

    float complete = -1;
    float loadDelay = 3;

    Dictionary<string, int> stages =
        new Dictionary<string, int>();
    private int currentStage;
    ShowControls scStart, sc2, sc3;

	// Use this for initialization
	void Start () {
        stages.Add("Start", 1);
        stages.Add("doStage2", 2);
        stages.Add("doStage3", 3);
        stages.Add("doStage4", 4);

        scStart = ShowControls.CreateDocked(
            new ControlItem("Welcome to Muffin the Destroyer!\n\nUse left and right to move Muffins.  Walk right to continue",
                new[] { KeyCode.LeftArrow, KeyCode.RightArrow }));
        sc2 = ShowControls.CreateDocked(
            new ControlItem("Use Z to jump, press Z again in mid-air to stomp!",
                KeyCode.Z));
        sc3 = ShowControls.CreateDocked(
            new ControlItem("Press X to swipe in front of you.",
                KeyCode.X));

        scStart.showDuration = -1;
        sc2.showDuration = -1;
        sc3.showDuration = -1;

        scStart.Show();
	}

    public void Update()
    {
        if (complete != -1 && complete + loadDelay < Time.time)
        {
            Debug.Log("Loading main game");
        }
    }

    public void OnChange(string name)
    {
        Debug.Log("Changing to " + name + " which is " + stages[name]);
        if (stages[name] < currentStage)
            return;
        currentStage = stages[name];
        switch (stages[name])
        {
            case 2:
                scStart.Hide();
                sc2.Show();
                break;
            case 3:
                sc2.Hide();
                sc3.Show();
                break;
            case 4:
                sc3.Hide();
                complete = Time.time;
                // finish tutorial
                break;
        }
    }

    public void OnGUI()
    {
        if (complete != -1)
        {
            GUI.Label(new Rect(0, 0, Screen.width, 400), "Tutorial Complete!  Time for muffins' adventure!");
        }
    }
}
