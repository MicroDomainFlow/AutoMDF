using Humanizer;

namespace AutoAggregates;
internal static class CreateFiles
{
	public static void CreateAggregate(string currentDirectory, string aggregateName)
	{
		// جایگزینی {Singular} و {Pluralize} با مفرد و جمع
		string singularAggregateName = aggregateName.Singularize(inputIsKnownToBePlural: false);
		string pluralizeAggregateName = aggregateName.Pluralize(inputIsKnownToBeSingular: false);
		bool result = false;
		//پیدا کردن namespace
		switch (currentDirectory)
		{
			case var cd when cd.Contains("EventBus.Messages") && cd.EndsWith("Aggregates"):
				string folderPath = CreateFilesHelpers.CreateFolder(pluralizeAggregateName, cd);
				string outputPathFolder = CreateFilesHelpers.CreateFolder("Events", folderPath);
				CreateFilesHelpers.CreateCsFileFromText(singularAggregateName, TxtFileTypes.DomainEvent, $"{singularAggregateName}CreatedEvent.cs", outputPathFolder, null);
				Console.WriteLine(cd);
				break;

			case var cd when !cd.Contains("Tests.Unit") && cd.Contains("ApplicationService") && cd.EndsWith("Aggregates"):
				string applicationServiceAggregatesFolderPath = CreateFilesHelpers.CreateFolder(pluralizeAggregateName, cd);

				var applicationServiceProjectName = CreateFilesHelpers.FindProjectNameFormDirectoryPath(cd);

				List<CreateFolder> applicationServiceFolders = new List<CreateFolder>()
				{
					new CreateFolder("CommandHandlers", applicationServiceAggregatesFolderPath),
					new CreateFolder("EventHandlers", applicationServiceAggregatesFolderPath),
					new CreateFolder("QueriesHandlers", applicationServiceAggregatesFolderPath),
				};
				var applicationServiceFoldersPath = CreateFilesHelpers.CreateFolder(applicationServiceFolders);

				List<CreateCsFile> applicationServiceFiles = new()
				{
					new CreateCsFile(singularAggregateName, TxtFileTypes.CommandHandler, $"Create{singularAggregateName}CommandHandler.cs", applicationServiceFoldersPath[0], applicationServiceProjectName),
						new CreateCsFile(singularAggregateName, TxtFileTypes.CommandValidation, $"Create{singularAggregateName}CommandValidation.cs", applicationServiceFoldersPath[0], applicationServiceProjectName),
					new CreateCsFile(singularAggregateName, TxtFileTypes.EventHandler, $"{singularAggregateName}CreatedEventHandler.cs", applicationServiceFoldersPath[1], applicationServiceProjectName),
					new CreateCsFile(singularAggregateName, TxtFileTypes.QueriesHandler, $"GetAll{singularAggregateName}QueryHandler.cs", applicationServiceFoldersPath[2], applicationServiceProjectName)
				};
				CreateFilesHelpers.CreateCsFileFromText(applicationServiceFiles);
				Console.WriteLine(cd);
				break;

			case var cd when cd.Contains(".Tests.Unit") && cd.Contains("ApplicationService") && cd.EndsWith("Aggregates"):
				string applicationServiceTestAggregatesFolderPath = CreateFilesHelpers.CreateFolder(pluralizeAggregateName, cd);

				var applicationServiceTestProjectName = CreateFilesHelpers.FindProjectNameFormDirectoryPath(cd);

				List<CreateFolder> applicationServiceTestFolders = new List<CreateFolder>()
				{
					new CreateFolder("CommandHandlers", applicationServiceTestAggregatesFolderPath),
					new CreateFolder("EventHandlers", applicationServiceTestAggregatesFolderPath),
					new CreateFolder("QueriesHandlers", applicationServiceTestAggregatesFolderPath),
				};
				var applicationServiceTestFoldersPath = CreateFilesHelpers.CreateFolder(applicationServiceTestFolders);

				List<CreateCsFile> applicationServiceTestFiles = new()
				{
					new CreateCsFile(singularAggregateName, TxtFileTypes.CommandHandlerTest, $"Create{singularAggregateName}CommandHandlerTests.cs", applicationServiceTestFoldersPath[0], applicationServiceTestProjectName),
					new CreateCsFile(singularAggregateName, TxtFileTypes.EventHandlerTest, $"{singularAggregateName}CreatedEventHandlerTests.cs", applicationServiceTestFoldersPath[1], applicationServiceTestProjectName),
					new CreateCsFile(singularAggregateName, TxtFileTypes.QueriesHandlerTest, $"GetAll{singularAggregateName}QueryHandlerTests.cs", applicationServiceTestFoldersPath[2], applicationServiceTestProjectName)
				};
				CreateFilesHelpers.CreateCsFileFromText(applicationServiceTestFiles);
				Console.WriteLine(cd);
				break;

			case var cd when cd.Contains("Contracts") && cd.EndsWith("Aggregates"):
				string contractsAggregatesFolderPath = CreateFilesHelpers.CreateFolder(pluralizeAggregateName, cd);

				var contractsProjectName = CreateFilesHelpers.FindProjectNameFormDirectoryPath(cd);
				List<CreateFolder> contractFolders =
				[
					new CreateFolder("CommandRepositories", contractsAggregatesFolderPath),
					new CreateFolder("Commands", contractsAggregatesFolderPath),
					new CreateFolder("Queries", contractsAggregatesFolderPath),
					new CreateFolder("QueryRepositories", contractsAggregatesFolderPath),
				];
				var contractsFoldersPath = CreateFilesHelpers.CreateFolder(contractFolders);
				var queryModelsFolder = CreateFilesHelpers.CreateFolder("Models", contractsFoldersPath[2]);
				List<CreateCsFile> contractFiles = [
					new(singularAggregateName,TxtFileTypes.ICommandRepository,$"I{singularAggregateName}CommandRepository",contractsFoldersPath[0],contractsProjectName),
					new(singularAggregateName,TxtFileTypes.Command,$"Create{singularAggregateName}Command.cs",contractsFoldersPath[1],contractsProjectName),
					new(singularAggregateName,TxtFileTypes.Query,$"GetAll{singularAggregateName}Query.cs",contractsFoldersPath[2],contractsProjectName),
					new(singularAggregateName,TxtFileTypes.QueryModel,$"{singularAggregateName}QueryDto.cs",queryModelsFolder,contractsProjectName),
					new(singularAggregateName,TxtFileTypes.IQueryRepository,$"I{singularAggregateName}QueryRepository.cs",contractsFoldersPath[3],contractsProjectName),
					];
				CreateFilesHelpers.CreateCsFileFromText(contractFiles);
				Console.WriteLine(cd);
				break;

			case var cd when cd.Contains("Domain") && !cd.Contains("Tests.Unit") && cd.EndsWith("Aggregates"):
				string domainAggregatesFolderPath = CreateFilesHelpers.CreateFolder(pluralizeAggregateName, cd);

				var domainProjectName = CreateFilesHelpers.FindProjectNameFormDirectoryPath(cd);
				List<CreateFolder> domainFolders =
				[
					new ("Entities", domainAggregatesFolderPath),
					new ("ValueObjects", domainAggregatesFolderPath),
				];
				var domainFoldersPath = CreateFilesHelpers.CreateFolder(domainFolders);

				List<CreateCsFile> domainFiles = [
					new(singularAggregateName,TxtFileTypes.AggregateRoot,$"{singularAggregateName}.cs",domainAggregatesFolderPath,domainProjectName),
					new(singularAggregateName,TxtFileTypes.Entity,$"{singularAggregateName}.cs",domainFoldersPath[0],domainProjectName),
					new(singularAggregateName,TxtFileTypes.ValueObject,$"{singularAggregateName}.cs",domainFoldersPath[1],domainProjectName),
				];
				CreateFilesHelpers.CreateCsFileFromText(domainFiles);
				Console.WriteLine(cd);
				break;
			case var cd when cd.Contains("Domain") && cd.Contains("Tests.Unit") && cd.EndsWith("Aggregates"):
				string domainTestAggregatesFolderPath = CreateFilesHelpers.CreateFolder(pluralizeAggregateName, cd);

				var domainTestProjectName = CreateFilesHelpers.FindProjectNameFormDirectoryPath(cd);
				List<CreateFolder> domainTestFolders =
				[
					new ("Entities", domainTestAggregatesFolderPath+"Entity"),
					new ("ValueObjects", domainTestAggregatesFolderPath+"Title"),
				];
				var domainTestFoldersPath = CreateFilesHelpers.CreateFolder(domainTestFolders);

				List<CreateCsFile> domainTestFiles = [
					new(singularAggregateName,TxtFileTypes.AggregateRootTest,$"{singularAggregateName}Tests.cs",domainTestAggregatesFolderPath,domainTestProjectName),
					new(singularAggregateName,TxtFileTypes.EntityTest,$"{singularAggregateName}Tests.cs",domainTestFoldersPath[0],domainTestProjectName),
					new(singularAggregateName,TxtFileTypes.ValueObjectTest,$"{singularAggregateName}Tests.cs",domainTestFoldersPath[1],domainTestProjectName),
				];
				CreateFilesHelpers.CreateCsFileFromText(domainTestFiles);
				Console.WriteLine(cd);
				break;
			case var cd when cd.Contains("Infrastructure") && cd.Contains("Commands") && cd.EndsWith("Aggregates"):
				string sqlCommandAggregatesFolderPath = CreateFilesHelpers.CreateFolder(pluralizeAggregateName, cd);

				var sqlCommandProjectName = CreateFilesHelpers.FindProjectNameFormDirectoryPath(cd);

				var sqlCommandFolderPath = CreateFilesHelpers.CreateFolder("Configurations", sqlCommandAggregatesFolderPath);

				List<CreateCsFile> sqlCommandFiles = [
					new (singularAggregateName, TxtFileTypes.SqlCommandConfiguration, $"{singularAggregateName}Configuration.cs", sqlCommandFolderPath, sqlCommandProjectName),
					new(singularAggregateName,TxtFileTypes.SqlCommandRepository, $"{singularAggregateName}CommandEntityFrameworkRepository.cs", sqlCommandAggregatesFolderPath, sqlCommandProjectName)
					];
				CreateFilesHelpers.CreateCsFileFromText(sqlCommandFiles);
				Console.WriteLine(cd);
				break;
			case var cd when cd.Contains("Infrastructure") && cd.Contains("Queries") && cd.EndsWith("Aggregates"):
				string sqlQueryAggregatesFolderPath = CreateFilesHelpers.CreateFolder(pluralizeAggregateName, cd);

				var sqlQueryProjectName = CreateFilesHelpers.FindProjectNameFormDirectoryPath(cd);

				var sqlQueryFolderPath = CreateFilesHelpers.CreateFolder("Configurations", sqlQueryAggregatesFolderPath);

				List<CreateCsFile> sqlQueryFiles = [
					new (singularAggregateName, TxtFileTypes.SqlQueryConfiguration, $"{singularAggregateName}Configuration.cs", sqlQueryFolderPath, sqlQueryProjectName),
					new(singularAggregateName,TxtFileTypes.SqlQueryRepository, $"{singularAggregateName}QueryEntityFrameworkRepository.cs", sqlQueryAggregatesFolderPath, sqlQueryProjectName)
				];
				CreateFilesHelpers.CreateCsFileFromText(sqlQueryFiles);
				Console.WriteLine(cd);
				break;
			case var cd when cd.Contains("Endpoints") && cd.Contains("API") && !cd.EndsWith("Tests.Unit"):
				var endpointProjectName = CreateFilesHelpers.FindProjectNameFormDirectoryPath(cd);
				if (cd.EndsWith("Controllers"))
				{
					CreateFilesHelpers.CreateCsFileFromText(singularAggregateName, TxtFileTypes.ApiController, $"{pluralizeAggregateName}Controller.cs", cd, endpointProjectName);
				}
				else if (cd.EndsWith("ProfilesViewModel"))
				{
					CreateFilesHelpers.CreateCsFileFromText(singularAggregateName, TxtFileTypes.EndpointProfile, $"{singularAggregateName}Profile.cs", cd, endpointProjectName);
				}
				else if (cd.EndsWith("ViewModels"))
				{
					var viewModelFolder = CreateFilesHelpers.CreateFolder(pluralizeAggregateName, cd);
					List<CreateCsFile> endpointVmFiles = [
						new(singularAggregateName, TxtFileTypes.EndpointCommandVm, $"{singularAggregateName}CommandVm.cs", Path.Combine(viewModelFolder), endpointProjectName),
						new(singularAggregateName, TxtFileTypes.EndpointQueryVm, $"{singularAggregateName}QueryVm.cs", Path.Combine(viewModelFolder), endpointProjectName),
						];
					CreateFilesHelpers.CreateCsFileFromText(endpointVmFiles);
				}
				Console.WriteLine(cd);
				break;
			case var cd when cd.Contains("Endpoints") && cd.Contains("API") && cd.EndsWith("Tests.Unit"):
				var endpointTestProjectName = CreateFilesHelpers.FindProjectNameFormDirectoryPath(cd);
				if (cd.EndsWith("Controllers"))
				{
					CreateFilesHelpers.CreateCsFileFromText(singularAggregateName, TxtFileTypes.ApiControllerTest, $"{pluralizeAggregateName}ControllerTests.cs", cd, endpointTestProjectName);
				}
				Console.WriteLine(cd);
				break;
		}
	}
}
