﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AMHQ {
	public class PlayerManager : Singleton<PlayerManager> {

		public GameManager gameManager;

		public override void Initialize(MonoBehaviour parent) {
			gameManager = parent as GameManager;
		}
	}
}