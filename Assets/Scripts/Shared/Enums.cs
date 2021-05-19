public enum SceneIndexes {
    MainMenu = 0,
    Gameplay = 1 
}
public enum EnemyState {
    Begin,
    Patrolling,     // Patrulhar
    Chasing,        // Corre atr�s do player
    Attacking,      // Attack
    RunAway,        // Mago - corre do player
    FindPosition,   // Mago - tenta encontra posi��o
    Dead,           // Inimigo Morreu
    Victory,        // Player morreu
}
public enum GameState {
    Initializing,   // Contagem regressiva
    Playing,        // Gameplay
    Win,            // Tempo acabou
    PlayerDied      // Player morreu
}
public enum ItemType {
    Arrow, 
    Life
}
public enum Music {
    MainMenu,
    Gameplay,
    None
}