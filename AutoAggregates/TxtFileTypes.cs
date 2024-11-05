using System.Runtime.Serialization;

namespace AutoAggregates;

public enum TxtFileTypes
{//todo filehay txt in haro dorost konam
	[EnumMember(Value = "DomainEvent.txt")]
	DomainEvent,
	[EnumMember(Value = "CommandHandler.txt")]
	CommandHandler,
	[EnumMember(Value = "CommandValidation.txt")]
	CommandValidation,
	[EnumMember(Value = "EventHandler.txt")]
	EventHandler,
	[EnumMember(Value = "QueriesHandler.txt")]
	QueriesHandler,
	[EnumMember(Value = "ICommandRepository.txt")]
	ICommandRepository,
	[EnumMember(Value = "Command.txt")]
	Command,
	[EnumMember(Value = "Query.txt")]
	Query,
	[EnumMember(Value = "QueryModel.txt")]
	QueryModel,
	[EnumMember(Value = "IQueryRepository.txt")]
	IQueryRepository,
	[EnumMember(Value = "AggregateRoot.txt")]
	AggregateRoot,
	[EnumMember(Value = "Entity.txt")]
	Entity,
	[EnumMember(Value = "ValueObject.txt")]
	ValueObject,
	[EnumMember(Value = "SqlCommandConfiguration.txt")]
	SqlCommandConfiguration,
	[EnumMember(Value = "SqlCommandRepository.txt")]
	SqlCommandRepository,
	[EnumMember(Value = "SqlQueryConfiguration.txt")]
	SqlQueryConfiguration,
	[EnumMember(Value = "SqlQueryRepository.txt")]
	SqlQueryRepository,
	[EnumMember(Value = "ApiController.txt")]
	ApiController,
	[EnumMember(Value = "EndpointProfile.txt")]
	EndpointProfile,
	[EnumMember(Value = "EndpointCommandVm.txt")]
	EndpointCommandVm,
	[EnumMember(Value = "EndpointQueryVm.txt")]
	EndpointQueryVm,
	[EnumMember(Value = "CommandHandlerTest.txt")]
	CommandHandlerTest,
	[EnumMember(Value = "EventHandlerTest.txt")]
	EventHandlerTest,
	[EnumMember(Value = "QueriesHandlerTest.txt")]
	QueriesHandlerTest,
	[EnumMember(Value = "AggregateRootTest.txt")]
	AggregateRootTest,
	[EnumMember(Value = "EntityTest.txt")]
	EntityTest,
	[EnumMember(Value = "ValueObjectTest.txt")]
	ValueObjectTest,
	[EnumMember(Value = "ApiControllerTest.txt")]
	ApiControllerTest
}

