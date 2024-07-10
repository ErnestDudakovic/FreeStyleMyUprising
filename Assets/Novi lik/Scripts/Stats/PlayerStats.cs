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
        damage.SetBaseValue(damage.GetBaseValue() + 5);

        // Save the updated stats to PlayerPrefs
        SavePlayerStats();
    }

    public void DecreaseDamageByThirty()
    {
        damage.SetBaseValue(damage.GetBaseValue() - 5);

        // Save the updated stats to PlayerPrefs
        SavePlayerStats();
    }

    public void IncreaseArmorByFive()
    {
        armor.SetBaseValue(armor.GetBaseValue() + 3);
        SavePlayerStats();
    }

    public void DecreaseArmorByFive()
    {
        armor.SetBaseValue(armor.GetBaseValue() - 3);
        SavePlayerStats();
    }

    public void IncreaseDashSpeedByHalf()
    {
        // Assuming dash speed is represented as a float in the player class
        player.dashSpeed += 0.5f;
        SavePlayerStats();
    }

    public void DecreaseDashSpeedByHalf()
    {
        // Assuming dash speed is represented as a float in the player class
        player.dashSpeed -= 0.5f;
        SavePlayerStats();
    }

    public void IncreaseCritChanceByOne()
    {
        critChance.SetBaseValue(critChance.GetBaseValue() + 1);
        SavePlayerStats();
    }

    public void DecreaseCritChanceByOne()
    {
        critChance.SetBaseValue(critChance.GetBaseValue() - 1);
        SavePlayerStats();
    }

    public void IncreaseCritPowerByTen()
    {
        critPower.SetBaseValue(critPower.GetBaseValue() + 10);
        SavePlayerStats();
    }

    public void DecreaseCritPowerByTen()
    {
        critPower.SetBaseValue(critPower.GetBaseValue() - 10);
        SavePlayerStats();
    }

 private void SavePlayerStats()
{
    PlayerPrefs.SetInt("PlayerMaxHealth", (int)maxHealth.GetBaseValue());
    PlayerPrefs.SetInt("PlayerCurrentHealth", (int)currentHealth);
    PlayerPrefs.SetInt("PlayerDamage", (int)damage.GetBaseValue());
    PlayerPrefs.SetInt("PlayerArmor", (int)armor.GetBaseValue());
    
    // Change to SetFloat for float values
    PlayerPrefs.SetFloat("PlayerDashSpeed", player.dashSpeed);
    PlayerPrefs.SetFloat("PlayerCritChance", critChance.GetBaseValue());
    PlayerPrefs.SetFloat("PlayerCritPower", critPower.GetBaseValue());
    
    PlayerPrefs.Save();
}

    private void LoadPlayerStats()
    {
        maxHealth.SetBaseValue(PlayerPrefs.GetInt("PlayerMaxHealth", (int)maxHealth.GetBaseValue()));
        currentHealth = PlayerPrefs.GetInt("PlayerCurrentHealth", (int)currentHealth);
        damage.SetBaseValue(PlayerPrefs.GetInt("PlayerDamage", (int)damage.GetBaseValue()));
        armor.SetBaseValue(PlayerPrefs.GetInt("PlayerArmor", (int)armor.GetBaseValue()));
        player.dashSpeed = PlayerPrefs.GetFloat("PlayerDashSpeed", player.dashSpeed);
        critChance.SetBaseValue(PlayerPrefs.GetInt("PlayerCritChance", critChance.GetBaseValue()));
        critPower.SetBaseValue(PlayerPrefs.GetInt("PlayerCritPower", critPower.GetBaseValue()));
    }


        public int GetCurrentHealth()
    {
        return (int)currentHealth;
    }

    public int GetCurrentDamage()
    {
        return (int)damage.GetBaseValue();
    }

    public int GetCurrentArmor()
    {
        return (int)armor.GetBaseValue();
    }

    public float GetCurrentDashSpeed()
    {
        // Assuming player.dashSpeed is a float field
        return player.dashSpeed;
    }

    public int GetCurrentCritChance()
    {
        return (int)critChance.GetBaseValue();
    }

    public int GetCurrentCritPower()
    {
        return (int)critPower.GetBaseValue();
    }
}
