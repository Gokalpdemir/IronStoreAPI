namespace ETıcaretAPI.Application.Features.AppUsers.Commands.Create
{
    public class CreatedUserCommandResponse
    {
        public bool Succeeded { get; set; }
        public string Message { get; set; }

        public CreatedUserCommandResponse(bool succeeded,string message)
        {
            Succeeded = succeeded;
            Message = message;
        }
        public CreatedUserCommandResponse(bool succeeded)
        {
            Succeeded=succeeded;
        }
        public CreatedUserCommandResponse()
        {

        }
    }
}
