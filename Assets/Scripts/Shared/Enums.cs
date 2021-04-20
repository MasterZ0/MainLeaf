public enum SceneIndexes {
    Manager = 0,
    MainMenu = 1,
    Gameplay = 2 
}
public enum EnemyState {
    Begin,
    Patrolling,     // Patrulhar
    Chasing,        // Corre atrás do player
    Attacking,      // Attack
    RunAway,        // Mago - corre do player
    FindPosition,   // Mago - tenta encontra posição
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