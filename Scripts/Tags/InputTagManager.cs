using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class InputTagManager
{
    private static string LeftIndex = "XRI_Left_Trigger";
    private static string LeftGrip = "XRI_Left_Grip";
    private static string RightIndex = "XRI_Right_Trigger";
    private static string RightGrip = "XRI_Right_Grip";
    
    private static string LeftThumbHori = "XRI_Left_Primary2DAxis_Horizontal";
    private static string LeftThumbVert = "XRI_Left_Primary2DAxis_Vertical";
    private static string RightThumbHori = "XRI_Right_Primary2DAxis_Horizontal";
    private static string RightThumbVert = "XRI_Right_Primary2DAxis_Vertical";

    public static float VRInputThreshold => 0.3f;

    public static string GetGrip(HandSide handSide)
    {
        return (handSide == HandSide.Left ? LeftGrip : RightGrip);
    }
    public static string GetIndex(HandSide handSide)
    {
        return (handSide == HandSide.Left ? LeftIndex : RightIndex);
    }
    public static string GetThumbHori(HandSide handSide)
    {
        return (handSide == HandSide.Left ? LeftThumbHori : RightThumbHori);
    }
    public static string GetThumbVert(HandSide handSide)
    {
        return (handSide == HandSide.Left ? LeftThumbVert : RightThumbVert);
    }
}
