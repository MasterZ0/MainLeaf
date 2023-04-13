using AdventureGame.Shared;
using Z3.UIBuilder.Core;
using System.Collections.Generic;
using UnityEngine;

namespace AdventureGame.Inputs.Data
{
    [CreateAssetMenu(menuName = Shared.MenuPath.Inputs + "Map", fileName = "InputSpriteMap")]
    public class InputSpriteMapData : ScriptableObject
    {
        //[TabGroup("PC")]
        public InputSpriteDeviceMap pc;
        //[TabGroup("Playstation")]
        public InputSpriteDeviceMap playstation;
        //[TabGroup("Xbox")]
        public InputSpriteDeviceMap xbox;
        //[TabGroup("Nintendo")]
        public InputSpriteDeviceMap nintendo;
    }

    [System.Serializable/*, InlineProperty*/, HideLabel]
    public class InputSpriteDeviceMap
    {
        //[PreviewField(60)]
        public Sprite undefined;
        //[PreviewField(60)]
        public Sprite unregistered;
        //[TableList(ShowIndexLabels = true, AlwaysExpanded = true), Searchable(FilterOptions = SearchFilterOptions.ISearchFilterableInterface)]
        public List<InputSpriteActionMap> inputs;
    }

    [System.Serializable]
    public class InputSpriteActionMap //: ISearchFilterable
    {
        public string path;
        //[TableColumnWidth(60, resizable: false), PreviewField(Alignment = ObjectFieldAlignment.Center)]
        public Sprite image;

        public bool IsMatch(string searchString)
        {
            return searchString.ToLower().Contains(path.ToLower());
        }
    }
}
