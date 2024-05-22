using UnityEngine;

public static class Calculator
{
    public static int CalcuateDamage(int damage, float accuracy, float avoid, int wall = 0, int condition = 0)
    {
        var rate = (accuracy * 0.01f) * (1f - avoid * 0.01f);
        if (wall > 0)
            rate *= Mathf.Pow(0.5f, wall);
        if (condition > 0)
            rate *= Mathf.Pow(2f, condition);

        var result = Random.Range(0f, 1f);
        if (result <= rate * 0.1f)
            return damage * 2;
        else if (result <= rate)
            return damage;
        return 0;
    }
}
