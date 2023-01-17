using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boundaries : MonoBehaviour {
    public Camera MainCamera;
    [HideInInspector] public Vector2 screenBounds { get; set; }

    private float objectWidth;
    private float objectHeight;

    void Start() {
        screenBounds = MainCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, MainCamera.transform.position.z));
        objectWidth = transform.GetComponentInChildren<SpriteRenderer>().bounds.extents.x;
        objectHeight = transform.GetComponentInChildren<SpriteRenderer>().bounds.extents.y;
    }

    void LateUpdate() {
        Vector3 viewPos = transform.position;
        viewPos.x = Mathf.Clamp(viewPos.x, screenBounds.x * -1 + objectWidth, screenBounds.x - objectWidth);
        viewPos.y = Mathf.Clamp(viewPos.y, screenBounds.y * -1 + objectHeight, screenBounds.y - objectHeight);
        transform.position = viewPos;
    }
}
