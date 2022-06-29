using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimEndDestroy : MonoBehaviour
{
	//�A�j���[�V�����̒���
	private float _animLength;

	//���݂̎���
	private float _time;

	// Use this for initialization
	void Awake()
	{
		Animator animOne = GetComponent<Animator>();
		AnimatorStateInfo infAnim = animOne.GetCurrentAnimatorStateInfo(0);
		_animLength = infAnim.length;
		_time = 0;
	}

	// Update is called once per frame
	void Update()
    {
		_time += Time.deltaTime;

		//�A�j���[�V�����̒��������Ԃ��o������
		if (_time > _animLength)
		{
			_time = 0;

			Destroy(gameObject);
		}
	}
}
