﻿using EnglishPremierLeague.Common.Entities;
using EnglishPremierLeague.Data.Adapters.Parsers;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.IO;

namespace EnglishPremierLeague.Data.Adapters.CSVAdapter
{
	public class CSVAdapter : DataAdapter
	{
		#region MyRegion
		private readonly IParser _csvParser;
		private readonly ILogger<CSVAdapter> _logger;
		private readonly IFileDetails _fileDetails;
		#endregion

		#region Constructor
		public CSVAdapter(IFileDetails fileDetails, IParser csvParser, ILoggerFactory loggerFactory)
		{
			_fileDetails = fileDetails;
			_csvParser = csvParser;
			_logger = loggerFactory.CreateLogger<CSVAdapter>();
		}
		#endregion

		#region Overidden Methods
		public override IEnumerable<Team> GetRepository()
		{
			try
			{
				List<Team> teamStandings = new List<Team>();

				//Check for file exists or not
				_logger.LogDebug("Checking for File exists : {0}", _fileDetails.FilePath);

				if (!File.Exists(_fileDetails.FilePath))
				{
					_logger.LogDebug("File Found");
					throw new FileNotFoundException();
				}

				if (new FileInfo(_fileDetails.FilePath).Length == 0)
				{
					_logger.LogDebug("Checking for File contents");
					throw new System.Exception("File is Empty");
				}

				_logger.LogDebug("Reading the CSV file");
				using (TextReader csvReader = new StreamReader(_fileDetails.FilePath))
				{
					string line;
					bool headerRow = _fileDetails.ContainsHeader;

					_logger.LogDebug("Parsing data from CSV File");
					while ((line = csvReader.ReadLine()) != null)
					{
						var team = _csvParser.Parse(line, headerRow);
						if (!headerRow && team != null)
							teamStandings.Add(team);

						headerRow = false;
					}

				}
				return teamStandings;
			}
			catch (System.Exception)
			{

				throw;
			}
		} 
		#endregion
	}
}
