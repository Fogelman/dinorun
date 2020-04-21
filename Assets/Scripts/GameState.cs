// Classe estática para armazenar os estados do jogo entre as fases
using UnityEngine;

public class GameState {
    public static int difficulty = 1; // Nível de dificuldades (de 1 a 3)
    public static float speed = 1.0f; // Velocidade da simulação (de 1 a 2)
    public static int lives = 3; // Número inicial de vídas
    public static int round = 0; // Número da partida
    public static int score = 0;
    public static int maxScore = 0;
    public static Vector3 checkpoint = new Vector3 (-12.2f, -12.2f, 0f);

    public static void Restart () {
        checkpoint = new Vector3 (-12.2f, -12.2f, 0f);
        difficulty = 1; // Nível de dificuldades (de 1 a 3)
        speed = 1.0f; // Velocidade da simulação (de 1 a 2)
        lives = 3; // Número inicial de vídas
        round = 0; // Número da partida
        score = 0;
        maxScore = 0;
    }
}