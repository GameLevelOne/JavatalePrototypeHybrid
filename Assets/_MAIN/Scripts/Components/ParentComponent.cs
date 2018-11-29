﻿using System;
using Unity.Entities;
// using UnityEngine;

namespace Javatale.Prototype
{
	[SerializableAttribute]
	public struct Parent : IComponentData 
	{
		public int EntityIndex;
	}

	public class ParentComponent : ComponentDataWrapper<Parent> {}
}
