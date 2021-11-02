namespace RstiNotifierBot.BL.Dto.Commands
{
    public class PostCommandResult : CommandResult
    {
        public PostCommandResult(PostDto post) : base(true)
        {
            Post = post;
        }

        public PostDto Post { get; }
    }
}