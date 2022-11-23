using System.Collections;

namespace StudyBuddy.WebUi.CustomException;

public class ApiException: Exception
{

    public ApiException(String Message, Exception InnetException) : base(Message,InnetException)
    {
        
    }
    
    public ApiException(String Message) : base(Message)
    {
        
    }
    
}