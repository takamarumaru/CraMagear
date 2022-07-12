using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

/// <summary>
/// �~�j�}�b�v�쐬�p�J�����B
/// �p�t�H�[�}���X���P�̂��߂ɁA���i��Camera��Disable�ɂ��ĕ`����s���Ă��Ȃ��B
/// </summary>
[ExecuteAlways]
public class MinimapCamera : MonoBehaviour
{
    [SerializeField, Header("�~�j�}�b�v�ɓK�p����}�e���A��")]
    private Material MiniMapMaterial;

    private Camera myCamera;
    /// <summary>
    /// �����̃J�������擾����
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
    /// �~�j�}�b�v�e�N�X�`�����X�V����
    /// 1�t���[�������������J�����ɂ��ăJ��������e�N�X�`���ɏ�������
    /// </summary>
    public void UpdateMinimapTexture()
    {
        myCamera.enabled = true;
        StartCoroutine(CrtDisableCameraAfterAFrame());

        Debug.Log("�`��X�V");

        /// </summary>
        /// ��莞�Ԍ�� Minimap �J�����𖳌��ɂ���
        /// �������Ȃ��ƁAOnRenderImage ���\�b�h�����葱���ăp�t�H�[�}���X����������
        /// �`�󂪕ς��}�b�v�̏ꍇ�ɂ͒���I�ɃJ�������I���ɂ���ȂǁA�ʂ̑΍���l����
        /// </summary>
        /// <returns></returns>

        /// <summary>
        /// 1�t���[����ɃJ�����𖳌��ɂ���
        /// </summary>
        /// <returns></returns>
        //�G�f�B�^�[���EditMode�̎��͖��������Ȃ�
        IEnumerator CrtDisableCameraAfterAFrame()
        {
            yield return new WaitForSeconds(1.0f);
            myCamera.enabled = false;
            Debug.Log("�J�����I�t");
        }
    }

    void Awake()
    {
        MyCamera.depthTextureMode = DepthTextureMode.Depth;
        Debug.Log(MyCamera.depthTextureMode);
    }

    void OnEnable()
    {
        //�~�j�}�b�v�̃e�N�X�`���X�V
        UpdateMinimapTexture();
    }

    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        Graphics.Blit(source, destination,MiniMapMaterial);
        Debug.Log("Texture�@�`��");
    }
}
