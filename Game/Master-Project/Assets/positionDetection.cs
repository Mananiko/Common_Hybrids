using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class positionDetection : MonoBehaviour
{
    public enum Orientation {
        Left, Right, Top, Bottom, Forward, Backward,
        LeftRight, LeftTop, LeftBottom, LeftForward, LeftBackward,
        RightTop, RightBottom, RightForward, RightBackward,
        TopBottom, TopForward, TopBackward,
        BottomForward, BottomBackward,
        ForwardBackward,

        LeftRightTop, LeftRightBottom, LeftRightForward, LeftRightBackward,
        LeftTopBottom, LeftTopForward, LeftTopBackward,
        LeftBottomForward, LeftBottomBackward,
        LeftForwardBackward,

        RightTopBottom, RightTopForward, RightTopBackward,
        RightBottomForward, RightBottomBackward,
        RightForwardBackward,

        TopBottomForward, TopBottomBackward,
        TopForwardBackward,

        BottomForwardBackward,
       
        All
    }
    public Orientation orientation;
    
}
