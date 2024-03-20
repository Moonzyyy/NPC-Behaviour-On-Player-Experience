import openai
from openai import OpenAI
import sys

client = OpenAI(
    # API key linking to <name>'s account
    api_key="<Your OpenAI Key>",
    # 60 seconds (default is 10 minutes)
    timeout=60.0,
    
    
)


def my_function(arg):
    try:
        user_message = ' '.join(arg)
        chat_completion = client.with_options(max_retries=0).chat.completions.create(
                messages=[
                    {
                        "role": "user",
                        "content": user_message
                    }
                ],
                model="gpt-3.5-turbo",
            )
        reply = chat_completion.choices[0].message.content
        return reply
    except openai.APIConnectionError as error:
        print("The server could not be reached.")
        print(error.__cause__)
    except openai.RateLimitError as error:
        print(str(error.status_code) + " status code received.")
    except openai.AuthenticationError as error:
        print("Authentication Error: " + str(error.status_code) + " status code.")
    except openai.APIStatusError as error:
        print("Non-200 status code received.")
        print("Error response: " + str(error.response))
    
if __name__ == "__main__":
    arg_from_csharp = sys.argv[1:]
    print(my_function(arg_from_csharp))
    
    #user_message = ' '.join(arg_from_csharp)
    #print(user_message)