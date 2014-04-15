#pragma strict
@script RequireComponent(GUITexture)
function Start () {

}

function Update () {
	GUI.depth = 5;
	guiTexture.pixelInset = Rect(-Screen.width/2,-Screen.height/2,Screen.width,Screen.height);
}