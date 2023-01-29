using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Jason;
using System;


namespace Kelvin
{
    public class CheckRoomRay : MonoBehaviour
    {
        public static Action checkRoomRay;

        //충돌체 정보 저장
        RaycastHit hitInfo;

        //해당 레이어에만 반응하도록 레이어마스크 설정
        [SerializeField]
        private LayerMask layerMask;

        //레이케스트 최대 거리
        [SerializeField]
        private float range = 500f;

        //정보 습득 가능 할 시 true
        private bool pickUpActivated = false;


        public string roomName_Raycast;
        public GameObject roomObj_Raycast;

        private void Awake()
        {
            //공용으로 넘겨줄 함수
            checkRoomRay = () =>
            {
                GetRayInfoRoom();
                //roomObj_Raycast;

            };
        }

        private void Update()
        {
            GetRayInfoRoom();
            GameManager.instance.CheckRayToGM(roomName_Raycast, roomObj_Raycast);
        }

        //Ray를 쏘고 해당 객체의 이름(Room타입)을 roomName에 저장
        public void GetRayInfoRoom()
        {
            Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hitInfo, range, layerMask);


            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * range, Color.red);

            roomName_Raycast = hitInfo.transform.GetComponent<RoomData>().myRoomData.RoomName;
            roomObj_Raycast = hitInfo.transform.GetComponent<RoomData>().myRoomData.RoomObject;
            //Debug.Log("name==" + hitInfo.collider.name);
        }

        void RoomRayInfoAppear()
        {
            pickUpActivated = true;
        }

        void infoDisappear()
        {
            pickUpActivated = false;
        }
    }
}