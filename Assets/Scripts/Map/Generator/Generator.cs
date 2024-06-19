using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Generator : FocusableObject
{
    [Header("Generator")]
    [SerializeField] private bool broken; //Is the Generator broken?
    private int hitCount;

    [SerializeField] private List<GeneratorLight> lights;
    [SerializeField] private float flashDelayMin = 0.1f;
    [SerializeField] private float flashDelayMax = 0.5f;

    [Space]
    [SerializeField] private UnityEvent onRepairEvent;

    private AudioManager AudioManager;

    private void Awake()
    {
        //Gets all the Lights of a Generator
        for (int i = 0; i < transform.childCount; i++)
        {
            Transform child = transform.GetChild(i);
            if (child.name.Contains("Display"))
            {
                lights.Add(child.GetComponent<GeneratorLight>());
            }
        }

        if(broken)
            StartCoroutine(FlashLights());

        AudioManager = GetComponent<AudioManager>();
    }

    protected override void Update()
    {
        base.Update();

        if (broken && focused)
        {
            //Check the player input
            KeyCode alpha = KeyCode.Alpha1;
            for (int i = 1; i < 9; i++)
            {
                if (alpha <= KeyCode.Alpha9)
                {
                    if (Input.GetKeyDown(alpha))
                    {
                        HitButton(i);
                    }
                }
                alpha++;
            }
        }

        if (hitCount == lights.Count && broken)
        {
            Repair();
        }
    }

    //Checks to see if the Button was hit
    private void HitButton(int index)
    {
        GeneratorLight light = lights[index - 1];
        if (light.gameObject.activeSelf && !light.IsHit)
        {
            light.Hit();
            hitCount++;
            AudioManager.PlaySoundEffect("Generator_Hit");
        }
    }

    IEnumerator FlashLights()
    {
        while (true)
        {
            int randomIndex = Random.Range(0, lights.Count); //Used to select a random light from the list
            GeneratorLight light = lights[randomIndex];
            if(!light.IsHit)
                light.Toggle();

            yield return new WaitForSeconds(Random.Range(flashDelayMin, flashDelayMax));
        }
    }

    public void Repair()
    {
        broken = false;
        onRepairEvent.Invoke();
    }
}
