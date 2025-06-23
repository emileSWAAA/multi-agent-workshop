import asyncio

from dotenv import load_dotenv
from semantic_kernel.contents.chat_history import ChatHistory

from settings import chat_completion_service_client


async def main():
    """
    Main asynchronous function to initialize the model client and generate a joke.
    This function performs the following steps:
    1. Loads environment variables from a .env file.
    2. Initializes the model client using a configuration.
    3. Sends a request to the model client to generate a joke.
    4. Prints the result.
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

    history = ChatHistory()
    user_input = "Tell me a short story joke about a cat and a dog."

    history.add_user_message(user_input)
    response = await chat_completion_service_client.get_chat_message_content(
        history
    )
    print(f"Assistant: {response}")

    # agent = ChatCompletionAgent(
    #     service=chat_completion_service_client,
    #     name="Assistant",
    #     instructions="You are a comedian specialized in telling short story jokes.",
    # )

    # user_input = "Tell me a short story joke about a cat and a dog."
    # print(f"User: {user_input}")
    # result = await agent.get_response(messages=[user_input])

    # print(result)


asyncio.run(main())
