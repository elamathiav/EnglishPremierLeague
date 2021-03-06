﻿using EnglishPremierLeague.Common.Entities;
using EnglishPremierLeague.Data.Adapters.Parsers;
using EnglishPremierLeague.Data.Adapters.Validators;
using Microsoft.Extensions.Logging;

namespace EnglishPremierLeague.Data.Parsers.DATParser
{
	public class DATParser : IParser
	{
		#region Private fields
		private readonly IValidator _datValidator;
		private readonly ILogger<DATParser> _logger;
		#endregion

		#region Constructor
		public DATParser(IValidator datValidator, ILoggerFactory loggerFactory)
		{
			_datValidator = datValidator;
			_logger = loggerFactory.CreateLogger<DATParser>();
		}
		#endregion

		#region IParser Methods
		public Team Parse(string rowData, bool headerRow)
		{
			try
			{

				Team team;
				if (_datValidator.Validate(rowData, headerRow, out team))
					return team;

				if (headerRow)
					throw new System.Exception("Columns do not match the template");

				return null;
			}
			catch (System.Exception)
			{

				throw;
			}
		} 
		#endregion
	}
}
