using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
namespace uOSC
{

[RequireComponent(typeof(uOscServer))]
public class ServerTest : MonoBehaviour
{
    public Camera camera;
    public GameObject cube;
    //変数
//一秒当たりの回転角度
	public  float angle = 30f;
 
//回転の中心をとるために使う変数
	private Vector3 targetPos;
    void Start()
    {
        var server = GetComponent<uOscServer>();
        server.onDataReceived.AddListener(OnDataReceived);
//targetに、"Sample"の名前のオブジェクトのコンポーネントを見つけてアクセスする
		Transform target = cube.transform;
//変数targetPosにSampleの位置情報を取得
		targetPos = target.position;

//自分をZ軸を中心に0～360でランダムに回転させる
		//camera.transform.Rotate(new Vector3(0, 0, Random.Range(0,360)),Space.World);	
    }
void Update () {
 
//	Sampleを中心に自分を現在の上方向に、毎秒angle分だけ回転する。
		Vector3 axis = transform.TransformDirection(Vector3.up);
		camera.transform.RotateAround(targetPos, axis ,angle * Time.deltaTime);
	}

    void OnDataReceived(Message message)
    {
        // address
        var msg = message.address + ": ";

        // timestamp
        msg += "(" + message.timestamp.ToLocalTime() + ") ";

        // values
        int i=0;
        foreach (var value in message.values)
        {
            if(i==0)
            {
                Colors(value.GetString());
            }
            msg += value.GetString() + " ";
        }

        //Debug.Log(msg);
    }
    private void Colors(string value)
    {
        float num = float.Parse(value);
        if(num!=60)
        {
          //Debug.Log(num);
          camera.backgroundColor = new Color(10*num*num,200*num*num,100*num*num);
          //float val = num;
        cube.transform.GetComponent<MeshRenderer>().materials[0].color = new Color(10*num*num,200*num*num,100*num*num);

          if(num<=0){
              //val = Random.Range(0.1f,1f);

          }
            //iTween.ScaleTo (cube.gameObject, iTween.Hash ("x", 10*num, "y", 10*num, "z", 10*num, "delay", 0.25f));
        }
    }
}

}