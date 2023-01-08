using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CharacterStats))]
public class HealthUI : MonoBehaviour
{
    public GameObject uiPrefab;
    public Transform target;
    
    //float visibleTime = 5f;
    //float lastMadeVisibleTime;

    Transform ui;
    Image healthBarFill;


    // Start is called before the first frame update
    void Start()
    {
        foreach (Canvas canvas in FindObjectsOfType<Canvas>())
        {
            if (GameObject.Find("HealthCanvas"))
            {
                ui = Instantiate(uiPrefab, canvas.transform).transform;
                healthBarFill = ui.GetChild(0).GetComponent<Image>();
				//ui.gameObject.SetActive(false);
				ui.gameObject.SetActive(true);
				break;
            }
        }
        GetComponent<CharacterStats>().OnHealthChanged += OnHealthChanged;
    }

    void OnHealthChanged(int maxHealth, int currentHealth)
    {
        if (ui != null)
        {
            //ui.gameObject.SetActive(true);
            //lastMadeVisibleTime = Time.time;

            float healthPercent = (float)currentHealth / maxHealth;
            healthBarFill.fillAmount = healthPercent;

            if (currentHealth <= 0)
            {
                Destroy(ui.gameObject);
            }
        }
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (ui != null)
        {
            ui.position = target.position;

            /*if (Time.time - lastMadeVisibleTime > visibleTime)
            {
                ui.gameObject.SetActive(false);
            }*/
        }
    }
}
