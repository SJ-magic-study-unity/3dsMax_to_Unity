/************************************************************
description
	dst側にattach.
	
	初期回転位置:
		合わせる必要なし(合わせてはいけない)
		なぜなら、例えば、初期位置を揃えるためにy軸中心に 180deg回転させたとする.
		この状態でscriptが走ると、scriptが反映された回転位置から、180deg回転してしまう。
		
	set Rig to Humanoid
		original modelをLoadした際、最初はGenericになっている.
		これをHumanoidにしないと、
			HumanPoseHandler avatar is not human.
		とErrorになり、以降、Update()にてErrorが出続ける.
************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/************************************************************
************************************************************/
public class CopyPose : MonoBehaviour {
	
	/****************************************
	****************************************/
	public HumanPoseHandler SourcePoseHandler;
	public HumanPoseHandler DstPoseHandler;
	
	public Avatar SourceAvatar;
	public Avatar DstAvatar;
	
	public GameObject SrcGameObject;

	public HumanPose HumanPose;
	public Vector3 pos_ofs;

	private string label = "saijo";
	
	/****************************************
	****************************************/
	void Start()
	{
		/********************
		"xxx.transform"を渡してHandlerを作成するが、
		GetHumanPose/SetHumanPoseで"transform.position"は反映されない.
		
		つまり、modelを配置した初期位置から、同じoffset量の移動となる。
		姿勢はもちろん反映される。
		********************/
		SourcePoseHandler = new HumanPoseHandler(SourceAvatar, SrcGameObject.transform);
		DstPoseHandler = new HumanPoseHandler(DstAvatar, this.transform);
	}
	
	// Update is called once per frame
	void Update () {
		SourcePoseHandler.GetHumanPose(ref HumanPose);
		
		HumanPose.bodyPosition.x += pos_ofs.x;
		HumanPose.bodyPosition.y += pos_ofs.y;
		HumanPose.bodyPosition.z += pos_ofs.z;
		DstPoseHandler.SetHumanPose(ref HumanPose);		
		
		label =		string.Format("{0:0.000000} ,",	HumanPose.bodyPosition.x);
		label +=	string.Format("{0:0.000000} ,",	HumanPose.bodyPosition.y);
		label +=	string.Format("{0:0.000000}",	HumanPose.bodyPosition.z);
	}
	
	void OnGUI()
	{
		GUI.color = Color.black;
		
		/********************
		本scriptは、dst側にattach.
		dstが2つ以上あると、label位置がかぶってしまうので、本出力は、debug用.
		********************/
		// GUI.Label(new Rect(15, 50, 500, 30), label);
	}
}



