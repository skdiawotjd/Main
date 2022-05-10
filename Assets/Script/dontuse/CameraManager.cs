using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    private float PreX, NowX, MoveX, Speed;
    private Camera CurCamera;

    // 화면 비율 고정
    private void Awake()
    {
        CurCamera = GetComponent<Camera>();

        // 카메라 컴포넌트의 Viewport Rect
        Rect rt = CurCamera.rect;

        // 현재 세로 모드 9:16
        float scale_height = ((float)Screen.width / Screen.height) / ((float)9 / 16);
        float scale_width = 1f / scale_height;

        if (scale_height < 1)
        {
            rt.height = scale_height;
            rt.y = (1f - scale_height) / 2f;
        }
        else
        {
            rt.width = scale_width;
            rt.x = (1f - scale_width) / 2f;
        }

        CurCamera.rect = rt;
    }

    void Start()
    {
        Speed = 60.0f;
        PreX = 0.0f;
        NowX = 0.0f;
        MoveX = 0.0f;
    }

    void Update()
    {
        // 화면 이동 로직
        if (Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                PreX = touch.position.x - touch.deltaPosition.x;
            }
            else if (touch.phase == TouchPhase.Moved)
            {
                NowX = touch.position.x - touch.deltaPosition.x;
                MoveX = (PreX - NowX) * Time.deltaTime * Speed;

                if ((CurCamera.transform.position.x + MoveX) >= -540.0f && (CurCamera.transform.position.x + MoveX) <= 540.0f)
                {
                    CurCamera.transform.Translate(MoveX, 0, 0);
                }

                PreX = touch.position.x - touch.deltaPosition.x;                
            }
        } 
    }
}

/*
 * 배경은 카메라 크기의 2배이므로 1배일 때 중앙의 x가 540이다.
 * 따라서 카메라의 x는 -540 ~ 540까지 이동 가능
 */

/*
 * 거의 벽에 붙어있을 때 슬라이드 하면 좀 끊겨보임
 * 아마 (CurCamera.transform.position.x + MoveX) >= -540.0f && (CurCamera.transform.position.x + MoveX) <= 540.0f
 * 이 조건을 만족하지 못하는 상황이 생겨 화면 이동이 부드럽게 진행되지 못하는 듯
 * 
 */
