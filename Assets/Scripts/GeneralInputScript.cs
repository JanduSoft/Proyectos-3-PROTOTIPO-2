using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

public class GeneralInputScript : MonoBehaviour
{

    [SerializeField] private int playerID = 0;
    [SerializeField] static private Player player;

    //Check controller ids here: https://guavaman.com/projects/rewired/docs/HowTos.html#identifying-recognized-controllers

        public enum ControllerNames { PS3, PS4, XBOX360, XBOXONE, KEYBOARD_AND_MOUSE, NULL_VALUE };
    public class controllerIds
    {
        public ControllerNames ControllerName;
        public string controllerFullId;
       
        public controllerIds()
        {
            ControllerName = ControllerNames.NULL_VALUE;
            controllerFullId = null;
        }
        public controllerIds(ControllerNames name, string fullID)
        {
            ControllerName = name;
            controllerFullId = fullID;
        }

    }

    public static controllerIds[] controllers = new controllerIds[]
    {
        new controllerIds(ControllerNames.PS3,"71dfe6c8-9e81-428f-a58e-c7e664b7fbed"),
        new controllerIds(ControllerNames.PS4,"cd9718bf-a87a-44bc-8716-60a0def28a9f"),
        new controllerIds(ControllerNames.XBOX360,"d74a350e-fe8b-4e9e-bbcd-efff16d34115"),
        new controllerIds(ControllerNames.XBOXONE,"19002688-7406-4f4a-8340-8d25335406c8"),

        //keep KEYBOARD_AND_MOUSE one as the last element of the array
        new controllerIds(ControllerNames.KEYBOARD_AND_MOUSE,"00000000-0000-0000-0000-000000000000")
    };

    static public controllerIds currentController = null;

    private void Awake()
    {
        if (player==null)
            player = ReInput.players.GetPlayer(playerID);

        // Get last controller from a Player and the determine the type of controller being used
        Controller controller = player.controllers.GetLastActiveController();
        if (controller != null)
        {
            switch (controller.type)
            {
                case ControllerType.Keyboard:
                    // Do something for keyboard
                    break;
                case ControllerType.Joystick:
                    // Do something for joystick
                    break;
                case ControllerType.Mouse:
                    // Do something for mouse
                    break;
                case ControllerType.Custom:
                    // Do something custom controller
                    break;
            }
        }
    }

    private void Update()
    {
        if (player.controllers.GetLastActiveController() != null)
        {
            for (int i = 0; i < controllers.Length; i++)
            {
                //Used to detect which controller is being used
                if (player.controllers.GetLastActiveController().hardwareTypeGuid.ToString() == controllers[i].controllerFullId)
                {
                    currentController = new controllerIds(controllers[i].ControllerName, controllers[i].controllerFullId);
                    break;
                }

                if (i == controllers.Length - 1) currentController = null;
            }

            if (currentController==null)
            {
                //If the controller is not recognized, switch to PC keyboard and mouse
                currentController = new controllerIds(controllers[controllers.Length-1].ControllerName, controllers[controllers.Length-1].controllerFullId);
            }
        }
        else
        {
            currentController = null;
        }
    }

    public static float Input_GetAxis(string axisName)
    {
        return player.GetAxis(axisName);
    }

    public static bool Input_GetKeyUp(string buttonName)
    {
        return player.GetButtonUp(buttonName);
    }

    public static bool Input_GetKeyDown(string buttonName)
    {
        return player.GetButtonDown(buttonName);
    }

    public static bool Input_isKeyPressed(string buttonName)
    {
        return player.GetButton(buttonName);
    }

    public static bool Input_anyKeyDown()
    {
        return player.GetAnyButtonDown();
    }

}
