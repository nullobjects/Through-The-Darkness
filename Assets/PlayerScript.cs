using UnityEngine;

public class PlayerScript : MonoBehaviour {
    private static int coins = 0;

    public static int GetCoins() {
        return coins;
    }
    public static void SetCoins(int value) {
        coins = value;
    }

    public static void AddCoins(int value) {
        coins += value;
        if (coins < 0) {
            Debug.Log("Problem detected coins below 0 bruh.");
        };
    }
}
