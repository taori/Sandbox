using System;
using System.Threading.Tasks;
using Grpc.Core;
using GrpcDotNetNamedPipes;
using NamedPipe.Common;

namespace NamedPipe.Server
{
	class ServerProgram
	{
		static void Main(string[] args)
		{
			var server = new NamedPipeServer("Test");
			ChatService.BindService(server.ServiceBinder, new ChatServiceImplementation());
			PowerChatService.BindService(server.ServiceBinder, new PowerChatServiceImplementation());

			try
			{
				Console.WriteLine("Starting service.");
				server.Start();

				Console.WriteLine("Service running.");
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
				Console.ReadKey();
			}
		}
	}

	public class PowerChatServiceImplementation : PowerChatService.PowerChatServiceBase
	{
		/// <inheritdoc />
		public override Task<ChatReply> SendMessage(ChatRequest request, ServerCallContext context)
		{
			Console.WriteLine($"Received: {request.Name}");
			return Task.FromResult(new ChatReply() { Message = $"Echo {request.Name}" });
		}
	}

	public class ChatServiceImplementation : ChatService.ChatServiceBase
	{
		/// <inheritdoc />
		public override Task<ChatReply> SendMessage(ChatRequest request, ServerCallContext context)
		{
			Console.WriteLine($"Received: {request.Name}");
			return Task.FromResult(new ChatReply() {Message = $"Echo {request.Name}"});
		}
	}
}
