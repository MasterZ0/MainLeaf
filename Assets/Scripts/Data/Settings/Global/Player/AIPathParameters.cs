namespace AdventureGame.Data
{
    [System.Serializable]
    public class AIPathParameters
    {
        public float maxSpeed = 10f;
        public float rotationSpeed = 360f;
        public float slowdownDistance = 3f;
        public float endReachedDistance = 2f;
        //pickNextWaypointDist = ?;
    }
}