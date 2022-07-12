using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

/// <summary>
/// ミニマップ作成用カメラ。
/// パフォーマンス改善のために、普段はCameraをDisableにして描画を行っていない。
/// </summary>
[ExecuteAlways]
public class MinimapCamera : MonoBehaviour
{
    [SerializeField, Header("ミニマップに適用するマテリアル")]
    private Material MiniMapMaterial;

    private Camera myCamera;
    /// <summary>
    /// 自分のカメラを取得する
    /// </summary>
    public Camera MyCamera
    {
        get
        {
            if (!myCamera)
            {
                TryGetComponent(out myCamera);
            }
            return myCamera;
        }
    }

    /// <summary>
    /// ミニマップテクスチャを更新する
    /// 1フレームだけ自分をカメラにしてカメラからテクスチャに書き込む
    /// </summary>
    public void UpdateMinimapTexture()
    {
        myCamera.enabled = true;
        StartCoroutine(CrtDisableCameraAfterAFrame());

        Debug.Log("描画更新");

        /// </summary>
        /// 一定時間後に Minimap カメラを無効にする
        /// そうしないと、OnRenderImage メソッドが走り続けてパフォーマンスが悪いため
        /// 形状が変わるマップの場合には定期的にカメラをオンにするなど、別の対策を考える
        /// </summary>
        /// <returns></returns>

        /// <summary>
        /// 1フレーム後にカメラを無効にする
        /// </summary>
        /// <returns></returns>
        //エディター上でEditModeの時は無効化しない
        IEnumerator CrtDisableCameraAfterAFrame()
        {
            yield return new WaitForSeconds(1.0f);
            myCamera.enabled = false;
            Debug.Log("カメラオフ");
        }
    }

    void Awake()
    {
        MyCamera.depthTextureMode = DepthTextureMode.Depth;
        Debug.Log(MyCamera.depthTextureMode);
    }

    void OnEnable()
    {
        //ミニマップのテクスチャ更新
        UpdateMinimapTexture();
    }

    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        Graphics.Blit(source, destination,MiniMapMaterial);
        Debug.Log("Texture　描画");
    }
}
