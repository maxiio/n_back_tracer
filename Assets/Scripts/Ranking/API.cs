﻿using UnityEngine;
using LitJson;
using System;
using System.Collections;
using System.Collections.Generic;

public class API {
	static string hostName = "http://setchi.jp/unity/b/";

	public static void FetchRanking(Action<JsonModel.Record[]> onSuccess) {
		HTTP.Get(hostName + "home/ranking.json", response => {
			onSuccess(JsonMapper.ToObject<JsonModel.Record[]>(response));
		});
	}
	
	public static void RankFirstEntry(string name, int score, Action<JsonModel.PlayerInfo> onSuccess) {
		var form = new Dictionary<string, string>();
		form.Add("name", name ?? "");
		form.Add("score", score.ToString());
		
		HTTP.Post(hostName + "home/rank_first_entry.json", form, response => {
			onSuccess(JsonMapper.ToObject<JsonModel.PlayerInfo>(response));
		});
	}
	
	public static void RankEntry(JsonModel.PlayerInfo playerInfo, int score, Action<JsonModel.CheckRecord> onSuccess) {
		var form = new Dictionary<string, string>();
		form.Add("id", playerInfo.id);
		form.Add("name", playerInfo.name ?? "");
		form.Add("score", score.ToString());
		
		HTTP.Post(hostName + "home/rank_entry.json", form, response => {
			onSuccess(JsonMapper.ToObject<JsonModel.CheckRecord>(response));
		});
	}
}
