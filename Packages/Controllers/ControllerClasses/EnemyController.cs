using System.Collections.Generic;
using Microsoft.Xna.Framework.Input;

namespace Project.Controllers.ControllerClasses
{
    public class EnemyController : IController
    {
        private Dictionary<Keys, ICommand> controllerMappings;
        private KeyboardState previousKeyboardState;

        public EnemyController(Dictionary<Keys, ICommand> controllerMappings)
        {
            this.controllerMappings = controllerMappings;
            previousKeyboardState = Keyboard.GetState();
        }

        public void Update()
        {
            KeyboardState currentKeyboardState = Keyboard.GetState();
            Keys[] pressedKeys = currentKeyboardState.GetPressedKeys();

            foreach (Keys key in pressedKeys)
            {
                if (controllerMappings.ContainsKey(key) && previousKeyboardState.IsKeyUp(key))
                {
                    controllerMappings[key].Execute();
                }
            }

            previousKeyboardState = currentKeyboardState;
        }
    }
}