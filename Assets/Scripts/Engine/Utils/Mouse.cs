using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Mouse  {

    public static void ShowCursor(bool visible)
    {
        Cursor.visible = visible;
        Cursor.lockState = visible ? CursorLockMode.None : CursorLockMode.Locked;
    }

}
