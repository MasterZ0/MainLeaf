using AdventureGame.Shared.NodeCanvas;
using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;

namespace AdventureGame.NodeCanvas.Unity.Physic
{
    [Category(Categories.Transform)]
    [Description("Gets a transform rotation")]
    public class DecomposeQuaternion : ActionTask
    {
        [ParadoxNotion.Design.Header("Input")]
        public BBParameter<Quaternion> quaternionInput;
        
        [ParadoxNotion.Design.Header("Full Rotation")]
        public BBParameter<Vector3> euler;

        [ParadoxNotion.Design.Header("Decomposed Euler")]
        public BBParameter<float> eulerX;
        public BBParameter<float> eulerY;
        public BBParameter<float> eulerZ;
        
        [ParadoxNotion.Design.Header("Decomposed Quaternion")]
        public BBParameter<float> quaternionX;
        public BBParameter<float> quaternionY;
        public BBParameter<float> quaternionZ;
        public BBParameter<float> quaternionW;

        [ParadoxNotion.Design.Header("Parameters")]
        [Tooltip("Returns a negative angle equivalent if it's greater than 180 degrees")]
        public BBParameter<bool> useSignedEuler;

        protected override string info => $"Decompose {quaternionInput}";

        protected override void OnExecute()
        {
            Quaternion quaternion = quaternionInput.value;
            Vector3 eulerAngles = quaternion.eulerAngles;
            
            euler.value = eulerAngles;
            
            eulerX.value = useSignedEuler.value ? GetSignedEuler(eulerAngles.x) : eulerAngles.x;
            eulerY.value = useSignedEuler.value ? GetSignedEuler(eulerAngles.y) : eulerAngles.y;
            eulerZ.value = useSignedEuler.value ? GetSignedEuler(eulerAngles.z) : eulerAngles.z;
            
            quaternionX.value = quaternion.x;
            quaternionY.value = quaternion.y;
            quaternionZ.value = quaternion.z;
            quaternionW.value = quaternion.w;
            
            EndAction(true);
        }

        /// <summary>
        /// Returns relative signed angles
        /// </summary>
        /// <param name="euler">Euler angle in degrees</param>
        /// <returns></returns>
        private static float GetSignedEuler(float euler)
        {
            return euler > 180 ? euler - 360 : euler;
        }
    }
}