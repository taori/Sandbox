using System;
using System.Threading.Tasks;
using Grpc.Core;
using GrpcDotNetNamedPipes;
using NamedPipe.Common;

namespace NamedPipe.Client
{
	class ClientProgram
	{
		static async Task Main(string[] args)
		{
			try
			{
				Console.WriteLine("Starting service.");
				var channel = new NamedPipeChannel(".", "Test");
				var chatServiceClient = new ChatService.ChatServiceClient(channel);

				Console.WriteLine("Press enter if your message is empty to abort the program.");
				while (true)
				{
					Console.Write("You: ");
					var content = Console.ReadLine();
					if (string.IsNullOrEmpty(content))
						return;

					Console.WriteLine("Sending message ...");
					var response = await chatServiceClient.SendMessageAsync(new ChatRequest() { Name = content });
					Console.WriteLine($"Received: {response.Message}");
				}
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
			}
			finally
			{
				Console.ReadKey();
			}
		}
	}
}
