using System.Collections;
using System.Collections.Generic;
using strange.extensions.context.impl;
using UnityEngine;

namespace Contexts
{
	public class AppRoot : ContextView
	{
		private void Awake()
		{
			context = new AppContext(this);
		}
	}
}