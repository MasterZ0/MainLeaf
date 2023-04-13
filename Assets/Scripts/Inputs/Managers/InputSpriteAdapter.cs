using UnityEngine;
using Z3.UIBuilder.Core;
using UnityEngine.UI;
using AdventureGame.Inputs.Data;

namespace AdventureGame.Inputs
{
    /// <summary>
    /// Used to update the sprite or image with the current joystick type
    /// </summary>
    public class InputSpriteAdapter : MonoBehaviour {
        
        [Title("ControllerAutoSprite")]
        [SerializeField] private InputSpriteReference inputSpriteData;
        [Space]
        [SerializeField] private SpriteRenderer spriteRenderer;
        [SerializeField] private Image image;

        private void Start()
        {
            InputManager.OnChangeDevice += OnUpdateDevice;
            InputManager.OnChangeBindings += OnChangeBindings;
            OnUpdateDevice(InputManager.CurrentDevice);
        }

        private void OnDestroy()
        {
            InputManager.OnChangeDevice -= OnUpdateDevice;
            InputManager.OnChangeBindings -= OnChangeBindings;
        }

        private void OnChangeBindings() => OnUpdateDevice(InputManager.CurrentDevice);

        private void OnUpdateDevice(DeviceController inputType)
        {
            Sprite sprite;

            if (inputSpriteData is InputSpriteReferenceDynamic referenceDynamic)
            {
                sprite = InputManager.GetPlayerIcon(referenceDynamic.inputAction, inputType);
            }
            else if (inputSpriteData is InputSpriteReferenceStatic referenceStatic)
            {
                sprite = referenceStatic.inputActions[inputType];
            }
            else
            {
                throw new System.NotImplementedException(inputSpriteData.GetType().Name);
            }

            if (spriteRenderer)
            {
                spriteRenderer.sprite = sprite;
            }

            if (image)
            {
                image.sprite = sprite;
            }
        }
    }
}