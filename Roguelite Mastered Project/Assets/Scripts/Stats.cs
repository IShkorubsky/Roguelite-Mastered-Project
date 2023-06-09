using UnityEngine;

[CreateAssetMenu(menuName = "CreateClass",fileName = "New Class")]
public class Stats : ScriptableObject
{
    [Header("Class Indicators")]
    [SerializeField] private string className;
    [SerializeField] private GameObject characterAsset;
    
    [Header("Stats")]
    [SerializeField] private float health;
    [SerializeField] private float maxHealth;
    [SerializeField] private float healthRegen;
    [SerializeField] private int moveSpeed;
    [SerializeField] private int attackDamage;
    [SerializeField] private int attackSpeed;
    [SerializeField] private int attackRange;
    [SerializeField] private int rangedAttackSpeed;

    //Encapsulated Fields
    public string ClassName => className;
    public float MAXHealth => maxHealth;
    public float HealthRegen => healthRegen;
    public int MoveSpeed => moveSpeed;
    public int AttackDamage => attackDamage;
    public int AttackSpeed => attackSpeed;
    public int AttackRange => attackRange;
    public int RangedAttackSpeed => rangedAttackSpeed;

    public float Health
    {
        get => health;
        set => health = value;
    }

    /// <summary>
    /// Set health based on maxHealth value
    /// </summary>
    public void SetMaxHealth()
    {
        Health = maxHealth;
    }

    /// <summary>
    /// Take damage based on the desired amount
    /// </summary>
    /// <param name="damageAmount"></param> Desired damage amount
    public void GetDamage(float damageAmount)
    {
        Health -= damageAmount;
    }

    /// <summary>
    /// Regenerate health over time
    /// </summary>
    public void HealthRegeneration()
    {
        Health += healthRegen * Time.deltaTime;
    }
}
