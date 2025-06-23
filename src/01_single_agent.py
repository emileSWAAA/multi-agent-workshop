import asyncio

from dotenv import load_dotenv
from semantic_kernel.agents import ChatCompletionAgent

from settings import chat_completion_service_client


async def simple_agent():
    """
    Main function to run the chatbot.
    This function loads environment variables, initializes a ChatCompletionClient
    using a provided configuration, creates an AssistantAgent with a specific role,
    and runs a task to get a joke from the chatbot.
    Returns:
        None
    """

    load_dotenv()

    # Alternative way to initialize the model client
    # client = AzureOpenAIChatCompletionClient(
    #     model="gpt-4o",
    #     api_version="2024-06-01",
    #     azure_endpoint=os.environ.get("AZURE_OPENAI_URL", ""),
    #     api_key=os.environ.get("AZURE_OPENAI_API_KEY", ""),
    # )

    agent = ChatCompletionAgent(
        service=chat_completion_service_client,
        name="Assistant",
        instructions="You are a comedian specialized in telling short story jokes.",
    )

    user_input = "Tell me a short story joke about a cat and a dog."
    print(f"User: {user_input}")
    result = await agent.get_response(messages=[user_input])
    print(f"Agent: {result.content}")


asyncio.run(simple_agent())
