// Classe estática para armazenar os estados do jogo entre as fases
using UnityEngine;

public class GameState {
    public static float speed = 1.0f; // Velocidade da simulação (de 1 a 2)
    public static int lives = 3; // Número inicial de vídas
    public static int score = 0;
    public static int maxScore = 0;
    public static Vector3 checkpoint = new Vector3 (-12.2f, -1.16f, 0f);

    public static void Restart () {
        speed = 1.0f; // Velocidade da simulação (de 1 a 2)
        lives = 3; // Número inicial de vídas
        score = 0;
        maxScore = 0;
        checkpoint = new Vector3 (-12.2f, -1.16f, 0f); //posição inicial do jogador
    }
}