﻿using EnglishPremierLeague.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace EnglishPremierLeague.DataAdapters.Validators
{
	public interface IValidator
	{
		bool Validate(string rowData, bool isHeaderRow, out Team team);
	}
}
