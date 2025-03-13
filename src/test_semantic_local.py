import asyncio
import datetime
import os

import dotenv
from azure.core.exceptions import ClientAuthenticationError
from azure.identity import DefaultAzureCredential
from semantic_kernel import Kernel
from semantic_kernel.connectors.ai.open_ai.prompt_execution_settings.azure_chat_prompt_execution_settings import (
    AzureChatPromptExecutionSettings,
)
from semantic_kernel.connectors.ai.open_ai.services.azure_chat_completion import (
    AzureChatCompletion,
)
from semantic_kernel.core_plugins.sessions_python_tool.sessions_python_plugin import (
    SessionsPythonTool,
)
from semantic_kernel.exceptions.function_exceptions import (
    FunctionExecutionException,
)
from semantic_kernel.functions.kernel_arguments import KernelArguments

from settings import llm_config

dotenv.load_dotenv()

pool_management_endpoint = os.getenv("ACA_POOL_MANAGEMENT_ENDPOINT")
azure_openai_endpoint = os.getenv("AZURE_OPENAI_URL")


def auth_callback_factory(scope):
    auth_token = None

    async def auth_callback() -> str:
        """Auth callback for the SessionsPythonTool.
        This is a sample auth callback that shows how to use Azure's DefaultAzureCredential
        to get an access token.
        """
        nonlocal auth_token
        current_utc_timestamp = int(
            datetime.datetime.now(datetime.timezone.utc).timestamp()
        )

        if not auth_token or auth_token.expires_on < current_utc_timestamp:
            credential = DefaultAzureCredential()

            try:
                auth_token = credential.get_token(scope)
            except ClientAuthenticationError as cae:
                err_messages = getattr(cae, "messages", [])
                raise FunctionExecutionException(
                    f"Failed to retrieve the client auth token with messages: {' '.join(err_messages)}"
                ) from cae

        return auth_token.token

    return auth_callback


async def initialize_kernel():
    """Initialize and set up the kernel with chat service and python tool"""
    kernel = Kernel()

    service_id = "chat-completion"

    # Extract settings from llm_config
    deployment_name = llm_config.get("config", {}).get("model", "gpt-4o")
    endpoint = llm_config.get("config", {}).get(
        "azure_endpoint", azure_openai_endpoint
    )
    api_key = llm_config.get("config", {}).get("api_key", None)
    api_version = llm_config.get("config", {}).get("api_version", "2024-06-01")

    # Create the appropriate chat service based on available credentials
    if api_key:
        chat_service = AzureChatCompletion(
            service_id=service_id,
            endpoint=endpoint,
            api_key=api_key,
            api_version=api_version,
            deployment_name=deployment_name,
        )
    else:
        chat_service = AzureChatCompletion(
            service_id=service_id,
            ad_token_provider=auth_callback_factory(
                "https://cognitiveservices.azure.com/.default"
            ),
            endpoint=endpoint,
            api_version=api_version,
            deployment_name=deployment_name,
        )

    # Register chat service with the kernel
    kernel.add_service(chat_service)

    # Register the sessions tool plugin
    sessions_tool = SessionsPythonTool(
        service_url=pool_management_endpoint,
        auth_callback=auth_callback_factory(
            "https://dynamicsessions.io/.default"
        ),
    )

    # Register the Python tool with the correct name
    python_plugin_name = "python"
    kernel.add_plugin(sessions_tool, python_plugin_name)

    # # Test the Python tool
    # print("🧪 Testing Python execution tool...")
    # test_code = "print('Hello from Python tool!')"
    # try:
    #     test_result = await kernel.invoke(
    #         plugin_name=python_plugin_name,
    #         function_name="Run",
    #         arguments={"code": test_code},
    #     )
    #     print(f"✅ Python tool test successful: {test_result}")
    # except Exception as e:
    #     print(f"❌ Python tool test failed: {str(e)}")
    #     print("Continuing anyway...")

    # Create system message prompt that explicitly instructs to use the Python tool
    # Simplified prompt to avoid potential content filter issues
    system_prompt = (
        """You are a helpful assistant that can solve math problems."""
    )

    chat_function = kernel.add_function(
        prompt=system_prompt + "\n\nUser: {{$user_input}}\nAssistant: ",
        plugin_name="ChatBot",
        function_name="Chat",
    )

    # Simplify tool configuration
    settings = AzureChatPromptExecutionSettings(
        service_id=service_id,
        temperature=0.2,  # Lower temperature for more deterministic responses
        tools=[
            {
                "type": "function",
                "function": {
                    "name": f"{python_plugin_name}.run",
                    "description": "Runs Python code",
                    "parameters": {
                        "type": "object",
                        "properties": {
                            "code": {
                                "type": "string",
                                "description": "Python code to execute",
                            }
                        },
                        "required": ["code"],
                    },
                },
            }
        ],
    )

    # Print version info for debugging
    print(f"💡 Using service ID: {service_id}")
    print(f"💡 Using model: {deployment_name}")

    return kernel, chat_function, settings


async def chat_with_kernel(message, kernel, chat_function, settings):
    """Send a message to the kernel and get a response"""
    try:
        print(f"🔄 Sending message to model: '{message}'")
        arguments = KernelArguments(settings=settings, user_input=message)
        answer = await kernel.invoke(
            function=chat_function,
            arguments=arguments,
        )
        return str(answer)
    except Exception as e:
        print(f"❌ Error details: {type(e).__name__}: {str(e)}")
        # Try a simpler approach without tools if the first attempt fails
        try:
            print("🔄 Attempting fallback without tool configuration...")
            simple_settings = AzureChatPromptExecutionSettings(
                service_id=settings.service_id,
                temperature=0.2,
            )
            arguments = KernelArguments(
                settings=simple_settings,
                user_input=f"Calculate {message} (just provide the answer)",
            )
            answer = await kernel.invoke(
                function=chat_function,
                arguments=arguments,
            )
            return (
                str(answer)
                + "\n(Note: Used fallback method without Python execution)"
            )
        except Exception as e2:
            return f"Error: {str(e)}\nFallback error: {str(e2)}"


async def main():
    """Main function for the terminal app"""
    print("🚀 Initializing Semantic Kernel with Python execution tool...")
    kernel, chat_function, settings = await initialize_kernel()
    print("✅ Kernel initialized successfully!")

    print("\n🤖 Welcome to the Semantic Kernel Calculator Terminal App")
    print("Type 'exit' to quit the application")

    # Modify the initial prompt to be simpler
    initial_prompt = (
        "Calculate the result of 30 + 5 and provide just the numerical answer."
    )
    print(f"\nUser: {initial_prompt}")

    response = await chat_with_kernel(
        initial_prompt, kernel, chat_function, settings
    )
    print(f"\nAssistant:\n{response}")

    # Continue with interactive mode
    while True:
        user_input = input("\nUser: ").strip()
        if user_input.lower() in ("exit", "quit", "q"):
            print("Goodbye! 👋")
            break

        response = await chat_with_kernel(
            user_input, kernel, chat_function, settings
        )
        print(f"\nAssistant:\n{response}")


if __name__ == "__main__":
    asyncio.run(main())
