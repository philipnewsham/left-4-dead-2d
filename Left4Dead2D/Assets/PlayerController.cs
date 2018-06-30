using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public Text healthText;
    public Slider healthBar;
    public Image healthBarFill;
    private int health = 100;
    private int temporaryHealth = 0;
    private int remainder = 0;
    private bool removingTempHealth;
    private bool isDead;

    public int testDamage;

    void OnEnable()
    {
        TakeDamage(testDamage);
    }
    
    public void TakeDamage(int damage)
    {
        remainder = health < damage ? Mathf.Abs(health - damage) : 0;
        health = Mathf.Clamp(health - damage, 0, 100);
        temporaryHealth = Mathf.Clamp(temporaryHealth - remainder, 0, 100);
        isDead = CheckIfDead();
        UpdateHealthUI();
    }

    bool CheckIfDead()
    {
        return health + temporaryHealth == 0;
    }

    void UseHealthPack()
    {
        temporaryHealth = 0;
        health = health + Mathf.FloorToInt((float)(100 - health) * 0.8f);
        UpdateHealthUI();
    }

    void UsePainPills()
    {
        UpdateTemporaryHealth(50);
    }

    void UseAdrenaline()
    {
        UpdateTemporaryHealth(25);
    }

    void ReturnToLife()
    {
        health = 50;
        temporaryHealth = 0;
        UpdateHealthUI();
    }

    void ReturnFromIncapacitation()
    {
        health = 1;
        temporaryHealth = 29;
        StartCoroutine(ReduceTemporaryHealth());
        UpdateHealthUI();
    }

    void UpdateTemporaryHealth(int hp)
    {
        temporaryHealth = Mathf.Clamp(temporaryHealth + hp, 0, 100 - health);
        StartCoroutine(ReduceTemporaryHealth());
        UpdateHealthUI();
    }

    void UpdateHealthUI()
    {
        healthText.text = string.Format("+{0}", health + temporaryHealth);
        healthBar.value = health + temporaryHealth;

        Color[] healthColours = new Color[3] { Color.green, Color.yellow, Color.red };
        int[] healthValues = new int[3] { 66, 33, 0 };

        for (int i = 0; i < 3; i++)
        {
            if(health >= healthValues[i])
            {
                healthBarFill.color = healthColours[i];
                healthText.color = healthColours[i];
                break;
            }
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            //UseHealthPack();
            //UsePainPills();
            //UseAdrenaline();
            //ReturnToLife();
            ReturnFromIncapacitation();
        }
    }

    IEnumerator ReduceTemporaryHealth()
    {
        if (!removingTempHealth)
        {
            removingTempHealth = true;
            while (temporaryHealth > 0)
            {
                yield return new WaitForSeconds(5.0f);
                temporaryHealth -= 1;
                UpdateHealthUI();
            }
            removingTempHealth = false;
        }
    }
}
