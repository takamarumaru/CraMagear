using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateMagicCircle : MonoBehaviour
{
    [SerializeField, Header("魔法陣のプレハブ")]
    Transform _magicCircle;

    // 魔法陣の引数
    [SerializeField]
    List<Layer> _layers;

    [SerializeField, Header("オーラのプレハブ")]
    Transform _aura;

    [SerializeField, Header("文字のテンプレート")]
    GameObject _character;

    [SerializeField, Header("円形の文字列")]
    List<MagicCircleString> _magicCircleStrings;

    [SerializeField, Header("魔法陣食い込まないための数値")]
    float _adjustment = 1.0f;

    void Awake()
    {
        // 魔法陣を構成する全ての親オブジェクト
        //var parentObj = new GameObject("MagicCircle");
        var parentObj = new GameObject("MagicCircle");

        //当たった場所に出現させるため
        var changeScaleRotatePosition = parentObj.AddComponent<ChangeScaleRotate>();

        //魔法陣設置のため微調整
        changeScaleRotatePosition._popPosition = new Vector3(transform.position.x, transform.position.y + _adjustment, transform.position.z);

        //parentObj.transform.position = new Vector3(0, 1.0f, 0);

        MagicCircle(ref parentObj);

        Aura(ref parentObj);

        Text(ref parentObj);

        //エフェクトを作成した後は自身を削除する
        Destroy(gameObject);
    }

    void Update()
    {
        //  Create();
    }

    public void Create()
    {

    }

    void MagicCircle(ref GameObject magicCircleParentObj)
    {
        // 魔法陣の親オブジェクト
        var parentObj = new GameObject("MagicCircle");
        parentObj.transform.parent = magicCircleParentObj.transform;

        // 親の中に子（レイヤー）を生成
        for (int i = 0; i < _layers.Count; i++)
        {
            // 複数生成する時は親と子の間にGameObjectを作って、その中に子を作る
            GameObject middleObj = new GameObject("MagicCircle " + i);
            middleObj.transform.parent = parentObj.transform;

            for (uint instanceNum = 0; instanceNum < _layers[i].InstanceNum; instanceNum++)
            {
                GameObject childObj = Instantiate(_magicCircle.gameObject);
                VFX_MagicCircle childMagicCircle = childObj.GetComponent<VFX_MagicCircle>();
                if (_layers[i].IsCopy)
                {
                    if (i == 0)
                    {
                        Debug.LogError("Element 0 の IsCopy にチェックは入れられません！");
                    }
                    _layers[i].Initialize(_layers[i - 1]);
                }
                childMagicCircle.Initialize(childMagicCircle, _layers[i]);
                childMagicCircle.transform.parent = _layers[i].InstanceNum == 1 ? parentObj.transform : middleObj.transform;

                // 名前を変更
                childMagicCircle.name += " " + i;
                if (_layers[i].InstanceNum > 1) childMagicCircle.name += "_" + instanceNum;
            }

            // 1つだけ生成する場合は削除する
            if (_layers[i].InstanceNum == 1) Destroy(middleObj);
        }
    }

    void Aura(ref GameObject magicCircleParentObj)
    {
        if (_aura == null) return;

        // 地面から湧き上がるオーラを生成
        GameObject auraObj = Instantiate(_aura.gameObject, magicCircleParentObj.transform);
        VFX_Aura aura = auraObj.GetComponent<VFX_Aura>();
        aura.VFXCommon.SetFloat(_layers[0].Scale / 20.0f, "Scale");
    }

    void Text(ref GameObject magicCircleParentObj)
    {
        // 全ての文字列を格納するオブジェクト
        GameObject parentObj = new GameObject("Text");
        parentObj.transform.parent = magicCircleParentObj.transform;

        for (int strings = 0; strings < _magicCircleStrings.Count; strings++)
        {
            MagicCircleString MCS = _magicCircleStrings[strings];
            float angleRange = MCS.AngleRange;

            // 文字列を格納するオブジェクト
            GameObject childObj = new GameObject(MCS.Str);
            childObj.transform.parent = parentObj.transform;

            for (int instanceNum = 0; instanceNum < MCS.InstanceNum; instanceNum++)
            {
                for (int chara = 0; chara < MCS.Str.Length; chara++)
                {
                    char c = MCS.Str[chara];

                    // 文字を格納するオブジェクト
                    GameObject charObj = Instantiate(_character);
                    charObj.transform.parent = childObj.transform;
                    charObj.transform.position = Vector3.forward * MCS.Radius;
                    charObj.transform.Rotate(new Vector3(90, 0, 0));

                    // 文字を扇型に並べる
                    float angle = chara * 2 * angleRange / (MCS.Str.Length - 1) - angleRange;
                    angle += MCS.OffsetAngle;
                    angle += 360.0f / MCS.InstanceNum * instanceNum;
                    charObj.transform.RotateAround(magicCircleParentObj.transform.position, magicCircleParentObj.transform.up, angle);

                    charObj.SetActive(true);
                    TextMesh textMesh = charObj.GetComponent<TextMesh>();
                    textMesh.text = c.ToString();
                    textMesh.anchor = MCS.Ancor;
                    textMesh.color = MCS.Color;

                    var MCSU = charObj.AddComponent<MagicCircleStringUpdate>();
                    MCSU.Initialize(magicCircleParentObj.transform, MCS.OffsetAngleSpeed);
                }
            }
        }
    }
}
