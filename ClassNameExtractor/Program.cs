using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

class Program
{
	/*
	private static readonly List<string> uniqueFilenamesList = new List<string>
{
	"BAAddresses",
	"BAAgreementsList",
	"BAAgreementsSetupUI",
	"BAAgreementsShow",
	"BAContactExportUI",
	"BAContactsUI",
	"BAContactUI",
	"BACopyAddressUI",
	"BACreditLimitMaintananceUI",
	"BACrossReferenceUI",
	"BAEDISetup",
	"BALiteViewUI",
	"BANotesUI",
	"BAParentLookup",
	"BAUploadDocuments",
	"BusinessAssociatesUI",
	"CodesUI",
	"CompanyStructureSetupUI",
	"CompanyStructureUI",
	"ConfigurationUI",
	"ContractCreditLimitMaintananceUI",
	"CorrectionFactorUI",
	"CreditLimitsUI",
	"CustomerEnrollmentForm",
	"CustomerViewUI",
	"CycleMeasurementMaintenanceUI",
	"CycleMeasurementUI",
	"CycleRecordMaintenanceUI",
	"DailyAllocatedCycleDataUI",
	"DailyReadMeasurement4LibUI",
	"DailyReadMeasurementUI",
	"DemandStatsDefaultUI",
	"DemandStatsUI",
	"EbbBroadCastMessageMailUI",
	"filenames.txt",
	"ImbalanceTradeUI",
	"InfoServiceUI",
	"InterruptExceptionUI",
	"InterruptSetupUI",
	"LocationDivisionSetupUI",
	"MeterBurnByDayUI",
	"MeterHistoryUI",
	"MeterSerialLookUp",
	"ModuleUI",
	"NominationAuditTrail4NomUI",
	"NominationsIDMUI",
	"NonOrificeDeviceLookupUI ",
	"NonOrificeDeviceSetUpUI",
	"NonOrificeMeterUI ",
	"NoticeReadReceiptsUI",
	"PaymentTermsUI",
	"PremiseSLUI",
	"RuleCodeUI",
	"StateCountyCityMaintenanceUI",
	"StatesCountyCityLookupUI",
	"StatesLookUp",
	"SystemLookUp",
	"TargetDataUI",
	"VolumebyDateUI",
	"VolumebyMeter4LibUI",
	"VolumeByMeterForUGIUI",
	"VolumebyMeterUI"
};
	*/

	//private static Dictionary<string, string> uniqueFilenamesList = new Dictionary<string, string>();

	private static readonly List<string> typesList = new List<string>
{
	"BusinessAssociate", "BARole", "BAAddress", "Codes", "Sequences",
	"Country", "State", "City", "County", "BANote", "BACrossReference",
	"BAContact", "BAContactRole", "BADocument", "BAContactTele", "BusinessAssociateDefaults",
	"BAContactComm", "PaymentTerm", "Division", "DecisionUnit", "WellZone", "CodesMaster",
	"CodesModule", "BAAgreement", "AgreementLpXref", "CodesRule", "BACreditLimit",
	"ContractCreditLimit", "ContactNotification", "Season", "RateCode", "NoticeReadreceipt",
	"Contract", "ContractStatus", "ContractCrossReference", "Amendment", "ContractComment",
	"ContractDemand", "ContractPathRate", "ContractQuantity", "Rate", "ErrorLog",
	"Variable", "PriceReferenceIndex", "PriceArea", "PriceAreaDetail", "ContractMeterAssign",
	"ContractScanData", "StorageRatchet", "StorageRatchetDetail", "UomFactors", "UomCodes",
	"ContractWhSettlement", "ContractWHStl", "ContractWhStlChgDtl", "MatrixHdr", "AcaRate",
	"MatrixStartPtDtl", "MatrixEndPtDtl", "Alert", "ContractChargeDef",
	"ContractNonPathQuantity", "AgentAssign", "ImBalGroup", "ImBalGrpDef", "Weather",
	"CntrAllocPf", "ContractQuanitySeason", "ContractSegQty", "ContractSegNetQty", "FuturePrice",
	"FuturePriceDetail", "BidPackageDetail", "StorageRatchetInfo", "GasMeterCdpAlloc",
	"GasMeterCdpAllocDtl", "StorageRatchetStatus", "Point", "Pipeline", "Zone",
	"SiteFacility", "SiteFacilityServices", "Region", "PointCrossReference",
	"RegionFactor", "MeterPointDocument", "NonOrificeDevice", "NonOrificMeter",
	"InfoService", "MeasurementPoint", "MeasurePointNote", "GaPointShipper",
	"MeasurementPointScanData", "Well", "SamplePoint", "SamplePointMeterXref",
	"SampleAnalysisHeader", "Facility", "GasMeter", "GasMeterHistory", "SuppliedVolume",
	"AllocationNetwork", "AllocationNetworkStatus", "AllocationProcess", "MeasurementPointProcess",
	"ProcessProcess", "SuppliedVolumeErrorLog", "MeasurementPointVolume",
	"MeasurementPtFormulae", "MeasurementPointVolumeNGRR", "SuppliedVolumePpa",
	"WhGaApproval", "P1Target", "RegionHeatVCorrFactor", "PseudoMeter",
	"SiteFacilityCis", "SiteFacilityCustomerCis", "SiteFacilityServicesCis",
	"MeasurementPointInfo", "MeasurementPointDQ", "WorkDayControl",
	"PointMarketPoolChoice", "SiteFacilityCisCapAlloc", "EnvGasmeterCum", "DemandStats",
	"MeasurementCyclePointVolume", "GasDayTargetDtl", "GasDayTarget",
	"DemandStatDefault", "MeasPtCycleVolumeDay","CorrectionFactor", "GLAccount",
	"GLMainType", "GLSubType", "GLAccountLinkerXref", "GLStatement", "GLAccountProject",
	"ContractMonthlyStatement", "MonthlyStatement", "GLStatementPriceVolume",
	"GLStatementPriceVolumeDaily", "GLAccountFinancialGastar", "GLAccountFinancialGastarContract",
	"GLAccountGastrXrefSPDetail", "GLAccountGastrXrefSPPGC", "GLAccountGastrXrefSP",
	"GLAccountFinancialInterface", "GLAccountFinancialInterfaceDetails", "GLAccountFinancial",
	"GLAccountFinancialGastarBA", "VendorInvoice", "GLAccountFinancialBalance",
	"GLAccountFinancialEntrry", "Invoice", "InvoiceDetail", "InvoiceAddress",
	"Expense", "ExpenseAddress", "ExpenseDetail", "VendorInvoiceScanData",
	"VendorInvoiceStatus", "GLAccountAllocation", "GLAccountAllocationPct",
	"GLAccountFinancialGastarHDR", "VendorContract", "VendorStatementDetail",
	"VendorContractCharge", "ManualJEDocument", "Formulas", "WorksheetApproval",
	"GLAccountFinancialGastarAct", "GLAccountFinancialGastarBAAct",
	"GLAccountFinancialGastarContractAct", "GLAccountFinancialGastarHDRAct",
	"GLAccountFinancialInterfaceAct", "GLAccountFinancialInterfaceDetailAct",
	"GLStatementAcr", "MonthlyStatementAcr", "VendorInvoiceAdjustment", "VendorInvoiceAddress",
	"ContactSecurityRole", "UserLoginInfo", "UserSecurityScope", "RoleSecurity",
	"EbbUserLogin", "DbaRole", "SessionRole", "SecurityPermission",
	"SecurityRolePermission", "SecurityRolePermissionAudit", "UserLoginHistory",
	"Package", "PackagePath", "PackagePathDetail", "PackageVolumeDetail",
	"Activity", "BidPackage", "BuySell", "Route", "RouteSegment", "BidPackageForReport",
	"BuySellDeal", "BuySellRoute", "MarketerPoolSummary", "RateType", "RateDetail",
	"PointMarketPoolAct", "PointMarketPoolGpp", "MarketerPoolSummaryDetail",
	"PackageImbalTrade", "BidConfirm", "ActivityDetailNonPathPackage",
	"NonPathPackage", "PointBalance", "PointBalanceRdSum", "P1CapacityRequirement",
	"P1TargetDetail", "P1CapacityRequirementDRN", "P1CapacityRequirementDIV",
	"P1CapacityRequirementDec", "PDAAlloc", "PDAAllocDtl", "P1CapacityReqPerContract",
	"GasMeterSch", "PackageVolumeDtlHistory", "PackagePathHistory", "PointMarketPoolIg",
	"P1TargetForecast", "P1CapacityReqPerPoolForecast", "PointMarketPoolInvIgDetail",
	"PointMarketPoolImbalTrade", "PointMarketPoolImbalTradeDetail",
	"SiteFacilityCisTierAlloc", "SiteFacilityCisTierDtl",
	"SiteFacilityCisTierDeliveryDtl", "HddWeatherInput", "CapInputDetail",
	"CapInputHdr", "BuyStorage", "BuyStorageRoute", "BuyStorageDeal",
	"LngSale", "LngSaleWgt", "LngSaleMtr", "Unit","PackagePathDetailHourly",
	"SystemLossMonth", "AuditLog", "EBBMessage", "EbbDocument",
	"UploadDocument", "BidPath", "BidPathDetail", "BidStatusHist",
	"EnvProducerMeter", "MeasurementPtParticFormulae", "MeasurementPtPartic",
	"BidPackageHistory", "BidVolumeDetail", "PipelineNotice", "CapRelOffer",
	"CapRelBid", "CapRelComment", "CapRelBidNonPath", "CapRelAward",
	"CapRelError", "CapRelRecall", "InfoPostingNotice", "InfoPostingNoticeRecp",
	"CapRelRecallLocQty", "IocFootNote", "IocCustomers", "BatchJob",
	"BatchJobLog", "EmailHistory", "ContractPathRateException",
	"DivisionToDecUnit", "MarketerPoolInvoice", "MarketerPoolInvDetail",
	"MarketerPoolInvFormula", "MarketerPoolFormula", "ContractNetQty", "CprRsCode",
	"RatePathOverrideDtl", "PointBalanceFifo", "PointBalanceAdj", "PointBalanceLiqSt",
	"ContractAsset", "ContractAssetFee", "PtWacogBal", "PumperRoute",
	"PointMarketPoolIGDetail", "MeasurementPointVolumeHistory", "PTImbalance",
	"ExcessInjandWd", "PlShSummary", "PlCntrSummary", "PlMeasSummary",
	"PlCntrPtSummary", "WeatherGasDay", "WeatherGasDayM", "CustomerStat",
	"GasDayGasPlan", "PointMarketPoolChoiceDtl", "DemandStatSummary",
	"GasDayTargetOvr", "LNGSalePriceReference", "PointMarketPoolChoiceMovol",
	"BAEnroll", "BAPremiseEnroll", "DailyImbalanceTolerance",
	"DailyImbalanceToleranceDtl", "BAEdiSetup", "SiteFacsCisMtr", "EdiStagingData",
	"MeasurementPointDetail", "GasDayTargetDtlMprt", "NomSplitPct",
	"Interrupt", "BAUpPipeline", "Sundry", "RateCategory"
};

	private static List<string> modelsUsed = new List<string>();
	private static List<string> modelsUsedInMethods = new List<string>();
	private static List<string> uiFilesUsed = new List<string>();

	private static List<string> processedPaths = new List<string>();

	private static bool searchRecursively = false;

	static void Main()
	{
		while (true)
		{
			try
			{
				//var fileName = "bas.txt";

				Console.WriteLine("Please enter the UI file name you wish to check:");
				var fileName = Console.ReadLine();

				if (fileName == null || !IsValidFileName(fileName))
				{
					Console.WriteLine("Not a valid file name.");
					return;
				}

				// Get the full path of the file in the current directory
				string fullPath = Path.Combine(Environment.CurrentDirectory, fileName);

				if (!File.Exists(fullPath))
				{
					Console.WriteLine($"The file path '{fullPath}' does not exist.");
					return;
				}

				Console.WriteLine("Would you like to do a recursive search within all UI files used? (y/n)");
				var answer = Console.ReadLine();
				while (answer != "y" && answer != "n")
				{
					Console.WriteLine("Please enter either a 'y' or a 'n'.");
				}

				if (answer == "y")
					searchRecursively = true;

				Console.WriteLine();
				Console.WriteLine();

				ProcessPath(fullPath);

				modelsUsed.Sort();

				Console.WriteLine();
				Console.WriteLine();

				PrintList($"UI files referenced in this file{(searchRecursively ? " and all referenced files" : "")}:", uiFilesUsed);

				Console.WriteLine();
				Console.WriteLine();

				PrintList($"Models referenced in this file{(searchRecursively ? " and all referenced files" : "")}:", modelsUsed);

				Console.WriteLine();
				Console.WriteLine();

				PrintList("Models referenced in methods from other files:", modelsUsedInMethods);

				Console.WriteLine();
				Console.WriteLine();
				Console.WriteLine("File processed successfully.");
				Console.WriteLine();

				Console.WriteLine("Press any key to close.");
				Console.ReadLine();
				return;
			}
			catch (Exception ex)
			{
				Console.WriteLine();
				Console.WriteLine();
				Console.WriteLine($"An error occurred: {ex.Message}.");
				Console.WriteLine();

				Console.WriteLine("Press any key to close.");
				Console.ReadLine();
				return;
			}
		}
	}

	static void PrintList(string title, List<string> list)
	{
		Console.WriteLine(title);
		foreach (var item in list)
		{
			Console.WriteLine(item);
		}
	}

	static void ProcessPath(string fullPath)
	{
		try
		{
			processedPaths.Add(fullPath);

			Console.WriteLine($"Processing path {fullPath}...");

			var lines = File.ReadAllLines(fullPath);

			// Get namespaces
			var currentNamespaceLine = lines.SingleOrDefault(line => line.Contains("namespace"));

			var currentNamespaceList = GetCurrentNamespaces(lines, currentNamespaceLine);

			var uiNamespaces = lines.Where(line => line.Contains("using ensyte.ui")).Select(line =>
			{
				var words = line.Split(' ');
				var ns = words[1].Trim(';');
				return ns;
			}).ToList();

			var processNamespaces = lines.Where(line => line.Contains("using ensyte.process")).Select(line =>
			{
				var words = line.Split(' ');
				var ns = words[1].Trim(';');
				return ns;
			}).ToList();

			// Extract files from namespaces
			var processFiles = ExtractFileNames(processNamespaces);
			var uiFiles = ExtractFileNames(uiNamespaces);
			var immediateFiles = ExtractFileNames(currentNamespaceList);

			// Add current namespace files to ui or process files
			if (currentNamespaceLine.Contains("ensyte.process"))
			{
				foreach (var item in immediateFiles)
				{
					if (!processFiles.ContainsKey(item.Key))
						processFiles.Add(item.Key, item.Value);
				}
			}
			else if (currentNamespaceLine.Contains("ensyte.ui"))
			{
				foreach (var item in immediateFiles)
				{
					if (!uiFiles.ContainsKey(item.Key))
						uiFiles.Add(item.Key, item.Value);
				}
			}

			var sourceCode = File.ReadAllText(fullPath);

			// Determine which ui files are used by this file and process each of them.
			//Console.WriteLine($"UI files used by {fullPath}:");
			foreach (string key in uiFiles.Keys)
			{
				string pluralClassName = key + "s"; // Assume simple pluralization by adding "s"
				string classNamePattern = $@"\b{key}\b|\b{pluralClassName}\b";

				MatchCollection matches = Regex.Matches(sourceCode, classNamePattern, RegexOptions.IgnoreCase);

				if (matches.Count > 0)
				{
					if (!uiFilesUsed.Contains(key))
						uiFilesUsed.Add(key);
					//Console.WriteLine(key);
					var newPath = uiFiles[key];
					if (!processedPaths.Contains(newPath) && searchRecursively)
						ProcessPath(newPath);
				}
			}
			//Console.WriteLine();

			// Extract method names from used namespace files.
			var processMethods = ExtractMethods(processFiles);
			var uiMethods = ExtractMethods(uiFiles);

			// Determine methods used by searching source code for this file.
			var processMethodsUsed = DetermineMethodsUsed(processMethods, sourceCode);
			var uiMethodsUsed = DetermineMethodsUsed(uiMethods, sourceCode);

			// Create method source code to search for models in.
			var methodSourceCode = CreateMethodSourceCode(processMethodsUsed, uiMethodsUsed, fullPath);

			// Determine models used in current file's source code as well as in referenced methods source code.
			foreach (var model in DetermineModelsUsed(sourceCode))
			{
				if (!modelsUsed.Contains(model)) 
					modelsUsed.Add(model);
			}
			foreach (var model in DetermineModelsUsed(methodSourceCode))
			{
				if (!modelsUsedInMethods.Contains(model) && !modelsUsed.Contains(model)) 
					modelsUsedInMethods.Add(model);
			}
		}
		catch (Exception ex)
		{
			Console.WriteLine($"Problem processing path: {fullPath}.\nMessage: {ex.Message}");
			throw;
		}
	}

	static string CreateMethodSourceCode(Dictionary<string, string> processMethodsUsed, Dictionary<string, string> uiMethodsUsed, string fullPath)
	{
		var methodSourceCode = "";

		// For each process method used add the code that is within the method to the source code.
		Console.WriteLine($"Process methods used by {fullPath}:");
		foreach (var methodName in processMethodsUsed.Keys)
		{
			Console.WriteLine(methodName);
			methodSourceCode += processMethodsUsed[methodName];
		}
		Console.WriteLine();

		// For each ui method used add the code that is within the method to the source code.
		Console.WriteLine($"UI methods used by {fullPath}:");
		foreach (var methodName in uiMethodsUsed.Keys)
		{
			Console.WriteLine(methodName);
			methodSourceCode += uiMethodsUsed[methodName];
		}
		Console.WriteLine();

		return methodSourceCode;
	}

	static List<string> DetermineModelsUsed(string sourceCode)
	{
		var modelsUsed = new List<string>();

		// Determine which models are used in the source code
		foreach (var className in typesList)
		{
			string pluralClassName = className + "s"; // Assume simple pluralization by adding "s"
			string classNamePattern = $@"\b{className}\b|\b{pluralClassName}\b";

			MatchCollection matches = Regex.Matches(sourceCode, classNamePattern, RegexOptions.IgnoreCase);

			if (matches.Count > 0)
			{
				if (!modelsUsed.Contains(className))
					modelsUsed.Add(className);
			}
		}

		return modelsUsed;
	}

	static Dictionary<string, string> DetermineMethodsUsed(Dictionary<string, string> allMethods, string sourceCode)
	{
		var methodsUsed = new Dictionary<string, string>();

		// Determine which methods are used in the source code
		foreach (var key in allMethods.Keys)
		{
			string classNamePattern = $@"\b{key}\b";

			MatchCollection matches = Regex.Matches(sourceCode, classNamePattern, RegexOptions.IgnoreCase);

			if (matches.Count > 0)
			{
				if (!methodsUsed.ContainsKey(key))
					methodsUsed[key] = allMethods[key];
			}
		}

		return methodsUsed;
	}

	static Dictionary<string, string> ExtractMethods(Dictionary<string, string> fileNamesAndPaths)
	{
		var methods = new Dictionary<string, string>();
		var filePaths = fileNamesAndPaths.Values;

		foreach (var filePath in filePaths)
		{
			var sourceCode = File.ReadAllText(filePath);
			var methodNames = FindMethodNames(sourceCode); // Has method name as key, and the method contents as value.
			foreach(var methodName in methodNames)
			{
				if (!methods.ContainsKey(methodName.Key))
					methods.Add(methodName.Key, methodName.Value);
			}
		}

		return methods;
	}

	static Dictionary<string, string> FindMethodNames(string fileContent)
	{
		//string methodPattern = @"(public|protected|private|static|\s)+[\w\<\>\[\]]+\s+(\w+)\s*\([^\)]*\)\s*({?|[^;])";
		string methodPattern = @"public\s+([\w\<\>\[\]]+\s+)+(\w+)\s*\([^\)]*\)\s*{([^{}]+)}";
		Regex methodRegex = new Regex(methodPattern);

		MatchCollection matches = methodRegex.Matches(fileContent);

		var methodLines = matches.Select(match => match.Value).ToList();

		var methodNames = new Dictionary<string, string>();

		foreach (var methodLine in methodLines)
		{
			var curlyIndex = methodLine.IndexOf('{');
			var beforeCurly = methodLine.Substring(0, curlyIndex);
			var afterCurly = methodLine.Substring(curlyIndex);
			var methodName = GetMethodNameFromLine(beforeCurly);
			if (!methodNames.ContainsKey(methodName))
				methodNames.Add(methodName, afterCurly);
		}

		return methodNames;
	}

	static string GetMethodNameFromLine(string methodLine)
	{
		var words = methodLine.Split(' ');

		int parenthIndex = -1;
		var methodSignature = words.SingleOrDefault(word =>
		{
			var tempIndex = word.IndexOf('(');
			if (tempIndex != -1)
			{
				parenthIndex = tempIndex;
				return true;
			}
			return false;
		});

		if (methodSignature == null) throw new Exception("No method signature was found.");

		var methodName = methodSignature.Substring(0, parenthIndex);
		return methodName;
	}

	static List<string> GetCurrentNamespaces(string[] lines, string currentNamespaceLine)
	{
		var errorMessage = "";

		try
		{
			errorMessage += currentNamespaceLine + "\n";

			var namespaceLineWords = currentNamespaceLine.Split(' ');

			var cns = namespaceLineWords[1];

			var currentNamespaceList = new List<string>();

			if (cns.Contains("ensyte.ui"))
			{
				CreateCurrentNamespaces(cns, "ensyte.ui", currentNamespaceList);
			}
			else if (cns.Contains("ensyte.process"))
			{
				CreateCurrentNamespaces(cns, "ensyte.ui", currentNamespaceList);
			}

			return currentNamespaceList;
		}
		catch (Exception ex)
		{
			throw new Exception(errorMessage, ex);
		}
	}

	static void CreateCurrentNamespaces(string fullNamespace, string baseNamespace, List<string> currentNamespaceList)
	{
		var afterEnsyte = "";

		if (fullNamespace != baseNamespace)
			afterEnsyte = fullNamespace.Substring((baseNamespace + ".").Length);

		if (!string.IsNullOrEmpty(afterEnsyte) && afterEnsyte.IndexOf('.') != -1)
		{
			// Assume there is only one dot
			var betweenDot = afterEnsyte.Split('.');
			var parentNamespace = betweenDot[0];
			currentNamespaceList.Add($"{baseNamespace}.{parentNamespace}");
			currentNamespaceList.Add($"{baseNamespace}.{betweenDot[0]}\\{betweenDot[1]}");
		}
		else
		{
			currentNamespaceList.Add(fullNamespace);
		}
	}

	static Dictionary<string, string> ExtractFileNames(List<string> namespaces)
	{
		var uniqueFilenamesList = new Dictionary<string, string>();

		foreach (string namespaceName in namespaces)
		{
			var words = namespaceName.Split('.');
			var newNamespaceName = $"{words[0]}.{words[1]}";

			for (int i = 2; i < words.Length; i++)
			{
				newNamespaceName += "\\" + words[i];
			}

			string namespacePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, namespaceName);
			string newNamespacePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, newNamespaceName);

			if (Directory.Exists(namespacePath))
				GetFileNames(namespacePath, uniqueFilenamesList);

			if (Directory.Exists(newNamespacePath))
				GetFileNames(newNamespacePath, uniqueFilenamesList);
		}

		return uniqueFilenamesList;
	}

	static void GetFileNames(string namespacePath, Dictionary<string, string> uniqueFilenamesList)
	{
		//Console.WriteLine($"Getting file names in {namespacePath}.");
		string[] files = Directory.GetFiles(namespacePath);

		foreach (var file in files)
		{
			var csIndex = file.IndexOf(".cs");
			var xamlIndex = file.IndexOf(".xaml");
			var csProjIndex = file.IndexOf(".csproj");
			var rdlcIndex = file.IndexOf(".rdlc");

			string fileName;


			if (rdlcIndex != -1 || csProjIndex != -1)
			{
				continue;
			}
			else if (xamlIndex != -1)
			{
				if (csIndex == -1) // only examine xaml.cs files, not .xaml files
					continue;

				fileName = file.Remove(xamlIndex);
			}
			else if (csIndex != -1)
			{
				fileName = file.Remove(csIndex);
			}
			else
			{
				fileName = file;
			}

			if (!uniqueFilenamesList.ContainsKey(Path.GetFileName(fileName)))
				uniqueFilenamesList.Add(Path.GetFileName(fileName), file);
		}
	}

	static bool IsValidFileName(string? fileName)
	{
		// Check for null or empty string
		if (string.IsNullOrEmpty(fileName))
		{
			return false;
		}

		// Check for invalid characters in the filename
		char[] invalidChars = Path.GetInvalidFileNameChars();
		if (fileName.IndexOfAny(invalidChars) != -1)
		{
			return false;
		}

		// Check for reserved device names in Windows (e.g., CON, PRN, AUX, etc.)
		string[] reservedNames = { "CON", "PRN", "AUX", "NUL", "COM1", "COM2", "COM3", "COM4", "COM5", "COM6", "COM7", "COM8", "COM9", "LPT1", "LPT2", "LPT3", "LPT4", "LPT5", "LPT6", "LPT7", "LPT8", "LPT9" };
		string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(fileName);
		if (reservedNames.Contains(fileNameWithoutExtension.ToUpper()))
		{
			return false;
		}

		// You can add more custom validation criteria if needed

		// If all checks pass, the filename is considered valid
		return true;
	}
}
