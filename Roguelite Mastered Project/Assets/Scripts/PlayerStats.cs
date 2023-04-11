using UnityEngine;

//Create a player scriptable object to be used as a holder for stats and creating different classes

[CreateAssetMenu(menuName = "CreateClass",fileName = "New Class")]
public class PlayerStats : ScriptableObject
{
    [Header("Class Indicators")]
    [SerializeField]private string className;
    [SerializeField]private GameObject characterAsset;
    
    [Header("Stats")]
    [SerializeField]private float health;
    [SerializeField]private float maxHealth;
    [SerializeField]private float healthRegen;
    [SerializeField]private int moveSpeed;
    [SerializeField]private int attackSpeed;
    [SerializeField]private int attackRange;

    //Encapsulated Fields
    public string ClassName => className;
    public float Health => health;
    public float MAXHealth => maxHealth;
    public float HealthRegen => healthRegen;
    public int MoveSpeed => moveSpeed;
    public int AttackSpeed => attackSpeed;
    public int AttackRange => attackRange;
    
    /// <summary>
    /// Set health based on maxHealth value
    /// </summary>
    public void SetMaxHealth()
    {
        health = maxHealth;
    }

    /// <summary>
    /// Take damage based on the desired amount
    /// </summary>
    /// <param name="damageAmount"></param> Desired damage amount
    public void TakeDamage(int damageAmount)
    {
        health -= damageAmount;
    }

    /// <summary>
    /// Regenerate health over time
    /// </summary>
    public void HealthRegeneration()
    {
        health += healthRegen * Time.deltaTime;
    }
}
