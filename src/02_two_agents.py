import asyncio

from dotenv import load_dotenv
from semantic_kernel.agents import ChatCompletionAgent, ConcurrentOrchestration
from semantic_kernel.agents.runtime import InProcessRuntime

from settings import chat_completion_service_client


async def team_2_agents():
    """
    Simulates a conversation between two agents, Chandler and Joey, who exchange jokes.
    The function performs the following steps:
    1. Loads environment variables.
    2. Creates a ChatCompletionClient using the provided configuration.
    3. Initializes two AssistantAgents, Chandler and Joey, with specific system messages.
    4. Sets up a termination condition where the conversation ends after "FINISH" is mentioned.
    5. Creates a RoundRobinGroupChat team with the two agents and the termination condition.
    6. Runs the conversation task and prints the result.
    7. Resets the team for a new round of jokes and runs the conversation as a stream, printing each message.
    Returns:
        None
    """

    load_dotenv()

    # Create the chandler agent.
    chandler_agent = ChatCompletionAgent(
        name="chandler",
        instructions="""Your name is Chandler, and you live with Joey. You are a specialist in telling short story 
        jokes related to friends. You also hear jokes from Joey, after 2 jokes you can finish the conversation saying 
        'FINISH'.""",
        service=chat_completion_service_client,
    )

    # Create the joey agent.
    joey_agent = ChatCompletionAgent(
        name="joey",
        instructions="""Your name is Joey, and you live with Chandler. You listen to jokes and answer with another joke. "
        After hearing 2 jokes, you can finish the conversation saying 'FINISH'.""",
        service=chat_completion_service_client,
    )

    agents = [chandler_agent, joey_agent]

    runtime = InProcessRuntime()
    runtime.start()

    concurrent_orchestration = ConcurrentOrchestration(members=agents)
    orchestration_result = await concurrent_orchestration.invoke(
        task="Please start the round with a joke.",
        runtime=runtime,
    )

    value = await orchestration_result.get(timeout=20)
    for item in value:
        print(f"# {item.name}: {item.content}")

    await runtime.stop_when_idle()


asyncio.run(team_2_agents())
