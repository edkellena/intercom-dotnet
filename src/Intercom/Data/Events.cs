using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Library.Clients;
using Library.Core;
using Library.Core;
using Library.Data;
using Library.Exceptions;

namespace Library.Data
{
	public class Events : Models
	{
		public List<Event> events { set; get; }

		public Events ()
		{
		}
	}
}