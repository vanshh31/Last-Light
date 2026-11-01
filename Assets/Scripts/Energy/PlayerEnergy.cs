
using UnityEngine;

public class PlayerEnergy : MonoBehaviour
{
    [Header("Energy")]
    [SerializeField] private int startingEnergy = 0;

    private int currentEnergy;

    public int CurrentEnergy => currentEnergy;

    private void Awake()
    {
        currentEnergy = startingEnergy;
    }

    public void AddEnergy(int amount)
    {
        if (amount <= 0)
            return;

        currentEnergy += amount;

        Debug.Log("Energy collected: +" + amount
                  + " | Total Energy: " + currentEnergy);
    }

    public bool SpendEnergy(int amount)
    {
        if (amount <= 0 || currentEnergy < amount)
            return false;

        currentEnergy -= amount;

        Debug.Log("Energy spent: -" + amount
                  + " | Remaining Energy: " + currentEnergy);

        return true;
    }
}
