﻿using System.Collections;
using System.IO;
using login_message;
using ProtoBuf;
using UnityEngine;
using System.Collections.Generic;
using System;

public class LoginController : Singteion<LoginController> 
{
    public void RegisterProto()
    {
        NetManager.Instance.RegisterProtoHandler(MsgIDDefineDic.LOGIN_MESSAGE_CMSGACCOUNTLOGINRESPONSE, HandlerLogin);
		NetManager.Instance.RegisterProtoHandler(MsgIDDefineDic.LOGIN_MESSAGE_CMSGACCOUNTREGISTRESPONSE, HandlerRegister);
		NetManager.Instance.RegisterProtoHandler(500, OnClose);

    }
    private void OnAccountRegistResponse(MemoryStream stream)
    {
        CMsgAccountRegistResponse response = ProtoBuf.Serializer.Deserialize<CMsgAccountRegistResponse>(stream);
        long accountid = response.accountid;
        Debug.Log("-------accountid:" + accountid);
        if (accountid == 0)
        {

        }
        else
        {
            ApplicationData.accountid = accountid;
            Application.LoadLevel("role");
        }
    }

    public void SendLoginRequest(string account, string password)
    {
        CMsgAccountLoginRequest request = new CMsgAccountLoginRequest();
        request.account = account;
        request.password = password;
        NetManager.Instance.Send(MsgIDDefineDic.LOGIN_MESSAGE_CMSGACCOUNTLOGINREQUEST, request);
    }

    public void SendRegistRequest(string account, string password)
    {
        CMsgAccountRegistRequest request = new CMsgAccountRegistRequest();
        request.account = account;
        request.password = password;
        NetManager.Instance.Send(MsgIDDefineDic.LOGIN_MESSAGE_CMSGACCOUNTREGISTREQUEST, request);
    }

	public static void OnClose(int msgId, object obj) 
	{
		Debug.Log ("OnClose called!!");
	}

    public static void HandlerLogin(int msgId, object obj)
    {
        CMsgAccountLoginResponse rsp = obj as CMsgAccountLoginResponse;
        if(rsp != null)
        {
            Messenger.Broadcast<CMsgAccountLoginResponse>(MessageNotice.MN_LOGIN, rsp);
        }
    }

	public static void HandlerRegister(int msgId, object obj)
	{
		Debug.Log ("HandlerRegister called!!");
		CMsgAccountRegistResponse rsp = obj as CMsgAccountRegistResponse;
		if(rsp != null)
		{
			Debug.Log ("register result: " + rsp.result);
			//Messenger.Broadcast<CMsgAccountRegistResponse>(MessageNotice.MN_LOGIN, rsp);
		}
	}

}
