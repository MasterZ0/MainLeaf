using AdventureGame.Shared.NodeCanvas;
using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;

namespace AdventureGame.NodeCanvas.Unity
{
    [Category(Categories.Animations)]
    [Description("Sets the sprite layer")]
    public class SetSpriteSortingLayer : ActionTask<SpriteRenderer>
    {
        [ParadoxNotion.Design.Header("Layer")]
        public BBParameter<int> sortingLayerIndex;
        public BBParameter<string> sortingLayerName;

        [ParadoxNotion.Design.Header("Parameters")]
        public BBParameter<bool> useName;

        protected override string info => $"Set sprite layer to {(useName.value ? sortingLayerName.value : SortingLayer.layers[sortingLayerIndex.value].name)}";

        protected override void OnExecute()
        {
            string layerName = useName.value 
                ? sortingLayerName.value 
                : SortingLayer.layers[sortingLayerIndex.value].name;

            int layerID = SortingLayer.NameToID(layerName);
            
            if(!SortingLayer.IsValid(layerID))
                EndAction(false);
            
            agent.sortingLayerID = layerID;
            EndAction(true);
        }
    }
}