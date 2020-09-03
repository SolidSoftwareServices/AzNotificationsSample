using System;
using Core.Cqrs.Abstractions.Commands;
using Core.System.Serialization.Json;
using Microsoft.Azure.ServiceBus;

namespace Core.Messaging.Azure.CommandsDispatcher.ServiceBus
{
	internal class ServiceBusMessage
	{
		private const string MsgTypeName = "MsgType";
		public Message Message { get; private set; }

		private ServiceBusMessage()
		{
		}

		public static ServiceBusMessage From(Message innerMessage)
		{
			return new ServiceBusMessage {Message = innerMessage};
		}

		public static ServiceBusMessage From(CqrsCommand command)
		{
			var result = new ServiceBusMessage {Message = new Message(command.ToUtf8Json())};
			result.Message.UserProperties.Add(MsgTypeName, command.GetType().FullName);
			return result;
		}

		public CqrsCommand GetCommand()
		{
			return (CqrsCommand)Message.Body.Utf8JsonToObject(Type.GetType((string)Message.UserProperties[MsgTypeName]));
		}
	}
}
