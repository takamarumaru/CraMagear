using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

[RequireComponent(typeof(Camera))]
public class DepthTexture : MonoBehaviour
{
    [SerializeField]
    private Shader _shader;
    [SerializeField]
    private float _outlineThreshold = 0.01f;
    [SerializeField]
    private Color _outlineColor = Color.white;
    [SerializeField]
    private float _outlineThick = 1.0f;

    private Material _material;

    private void Awake()
    {
        Initialize();

    }

    private void Update()
    {
#if UNITY_EDITOR
        SetMaterialProperties();
#endif
    }

    private void Initialize()
    {
        var camera = GetComponent<Camera>();
        camera.depthTextureMode |= DepthTextureMode.Depth;

        if (camera.allowMSAA || camera.allowHDR)
        {
            return;
        }

        _material = new Material(_shader);
        SetMaterialProperties();

        var commandBuffer = new CommandBuffer();
        int tempTextureIdentifier = Shader.PropertyToID("_PostEffectTempTexture");
        commandBuffer.GetTemporaryRT(tempTextureIdentifier, -1, -1);
        commandBuffer.Blit(BuiltinRenderTextureType.CurrentActive, tempTextureIdentifier);
        commandBuffer.Blit(tempTextureIdentifier, BuiltinRenderTextureType.CurrentActive, _material);
        commandBuffer.ReleaseTemporaryRT(tempTextureIdentifier);
        camera.AddCommandBuffer(CameraEvent.AfterEverything, commandBuffer);
    }

    private void SetMaterialProperties()
    {
        if (_material != null)
        {
            _material.SetFloat("_OutlineThreshold", _outlineThreshold);
            _material.SetColor("_OutlineColor", _outlineColor);
            _material.SetFloat("_OutlineThick", _outlineThick);
        }
    }

    //// Start is called before the first frame update
    //void Start()
    //{
    //    // ���Ƃ��΃��C�g��Shadow Type��No Shadows�̂Ƃ��Ȃǂ�
    //    // ���ꂪ�ݒ肳��Ă��Ȃ��ƃf�v�X�e�N�X�`������������Ȃ�
    //    GetComponent<Camera>().depthTextureMode |= DepthTextureMode.Depth;

    //    _material = new Material(_shader);
    //}
    //private void OnRenderImage(RenderTexture source, RenderTexture dest)
    //{
    //    Graphics.Blit(source, dest, _material);
    //}

}
