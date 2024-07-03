using UnityEngine;

public class PlayerStats : CharacterStats
{
    private Player player;

    protected override void Start()
    {
        base.Start();

        player = GetComponent<Player>();
        LoadPlayerStats();
    }

    public override void TakeDamage(int _damage)
    {
        base.TakeDamage(_damage);
    }

    protected override void Die()
    {
        base.Die();
        player.Die();
    }

    public void IncreaseHealthByTen()
    {
        maxHealth.SetBaseValue(maxHealth.GetBaseValue() + 10);
        currentHealth += 10; // Optionally increase current health when max health is increased
        
        // Save the updated stats to PlayerPrefs
        SavePlayerStats();
    }

    public void DecreaseHealthByTen()
    {
        maxHealth.SetBaseValue(maxHealth.GetBaseValue() - 10);
        currentHealth -= 10; // Optionally decrease current health when max health is decreased

        // Save the updated stats to PlayerPrefs
        SavePlayerStats();
    }

    public void IncreaseDamageByThirty()
    {
        damage.SetBaseValue(damage.GetBaseValue() + 30);

        // Save the updated stats to PlayerPrefs
        SavePlayerStats();
    }

    public void DecreaseDamageByThirty()
    {
        damage.SetBaseValue(damage.GetBaseValue() - 30);

        // Save the updated stats to PlayerPrefs
        SavePlayerStats();
    }

    private void SavePlayerStats()
    {
        PlayerPrefs.SetInt("MaxHealth", maxHealth.GetBaseValue());
        PlayerPrefs.SetInt("CurrentHealth", currentHealth);
        PlayerPrefs.SetInt("Damage", damage.GetBaseValue());
        PlayerPrefs.Save();
    }

    // Load saved stats when the game starts or the player object is created
    private void LoadPlayerStats()
    {
        maxHealth.SetBaseValue(PlayerPrefs.GetInt("MaxHealth", maxHealth.GetBaseValue()));
        currentHealth = PlayerPrefs.GetInt("CurrentHealth", currentHealth);
        damage.SetBaseValue(PlayerPrefs.GetInt("Damage", damage.GetBaseValue()));
    }
}
    